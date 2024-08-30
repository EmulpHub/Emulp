using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Slot_Equiped : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public SingleEquipment.type type;

    public GameObject bg;

    public Image graphic;

    public Window_Equipment window;

    public SingleEquipment equipment;

    public Image contour, bg_img;

    public void Start()
    {
        Init();
    }

    public void Init()
    {
        baseScale = transform.localScale;
    }

    public static SingleEquipment.type typeInMouseOver;

    public Color32 contourColor;

    public void Update()
    {
        bool isNull = equipment == null;

        if (!isNull)
        {
            graphic.sprite = equipment.Graphic;
        }

        //bg_img.color = new Color32(contourColor.r, contourColor.g, contourColor.b, (byte)(bg_img.color.a * 255));
        /*
        if (GoneBig)
        {
            contour.DOColor(contourWhenBig, 0.2f);
        }
        else
        {
            contour.DOColor(contourColor, 0.2f);
        }*/

        graphic.gameObject.SetActive(!isNull);
        bg.gameObject.SetActive(isNull);

        DragAndDropUpdate();
    }

    #region DragAndDrop

    public Vector2 MousePosWhenClicked;

    public Color contourWhenBig;

    public void DragAndDropUpdate()
    {
        if (MouseOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                MousePosWhenClicked = CursorInfo.Instance.positionInWorld;
            }

            if (Input.GetMouseButton(0) && window.Drag_Equipment == null
                && CursorInfo.Instance.positionInWorld != MousePosWhenClicked && equipment != null && V.game_state != V.State.fight)
            {
                Anim_Click();
                DragThis = true;
                window.CreateDragGraphic(equipment); //Start Drag 
            }

            if (DescriptionStatic.CurrentDescription == null && equipment != null)
            {
                Slot_Inventory.ShowEquipmentInfoDescOrTitle(equipment, transform.position);
                //DisplayEquipmentInfo(equipment, transform.position);
            }
        }

        if (DragThis && !Input.GetMouseButton(0) && equipment != null)
        {
            window.EraseDragGraphic(false);

            if (window.Slot_MouseOver != this)
            {
                Equipment_Management.UnEquipEquipment(type);
            }

            DragThis = false;
        }

        if (!DragThis &&
            window.Drag_Equipment != null && MatchingEquipment(window.Drag_Equipment.Type, type))
        {
            Anim_Big();
        }
        else
        {
            Anim_Normal();
        }
    }

    public static bool MatchingEquipment(SingleEquipment.type wantedType, SingleEquipment.type targetType)
    {
        return wantedType == targetType ||
            (wantedType == SingleEquipment.type.object_equipment && targetType == SingleEquipment.type.object_equipment_2) ||
            (targetType == SingleEquipment.type.object_equipment && wantedType == SingleEquipment.type.object_equipment_2);
    }

    public static string GetTitleDesc((string title, string description) v, SingleEquipment e)
    {
        return v.title;
    }

    public static string DisplayEquipmentInfo(SingleEquipment e, Vector3 position, float distance = 1)
    {
        (string title, string description) v = Window_Equipment.ShowEquipment_Description(e);

        string t = GetTitleDesc(v, e);

        //Display_description_equipment.Display_Description(t, position, distance, e);

        Description_text.Display(t,v.description,position,distance);

        return t;
    }

    bool DragThis = false;
    bool MouseOver = false;

    #endregion

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        typeInMouseOver = type;

        Anim_MouseOver();

        MouseOver = true;

        window.Slot_MouseOver = this;
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Anim_Exit();

        MouseOver = false;

        if (window.Slot_MouseOver == this)
        {
            window.Slot_MouseOver = null;
        }

        if (equipment == null)
            return;

        (string title, string description) v = Window_Equipment.ShowEquipment_Description(equipment);

        string title = GetTitleDesc(v, equipment);

        Description_text.EraseDispay(title);
        Main_UI.Display_Title_Erase(title);
    }

    [HideInInspector]
    public float lastTimeClicked = -1;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Anim_Click();

        if (Time.time - lastTimeClicked <= 0.3f)
        {
            DoubleClick();
            lastTimeClicked = 0;
        }
        else
        {
            SimpleClick();
            lastTimeClicked = Time.time;
        }
    }

    public void SimpleClick()
    {
        if (window.showedEquipment == equipment)
        {
            window.ShowEquipment(null);
        }
        else
        {
            window.ShowEquipment(equipment);
        }
    }

    public void DoubleClick()
    {
        if (V.game_state == V.State.fight)
        {
            window.SendFightError();
            return;
        }

        if (equipment != null)
            Equipment_Management.UnEquipEquipment(type);
    }

    #region animation

    Vector3 baseScale;

    public float multiplierMouseOver, anim_speed, shakeVector;

    public void Anim_MouseOver()
    {
        transform.DOKill();

        transform.localScale = baseScale * multiplierMouseOver;
    }

    public void Anim_Exit()
    {
        transform.DOKill();

        transform.DOScale(baseScale.x, anim_speed);
    }

    public void Anim_Click()
    {
        transform.DOKill();

        transform.localScale = baseScale;

        transform.DOPunchScale(new Vector3(shakeVector, shakeVector, 0), anim_speed, 1);
    }

    bool GoneBig;

    public void Anim_Big()
    {
        transform.SetSiblingIndex(transform.parent.childCount - 1);

        transform.DOKill();

        transform.DOScale(baseScale.x * multiplierMouseOver * 1.1f, anim_speed);

        GoneBig = true;
    }

    public void Anim_Normal()
    {
        if (!GoneBig)
            return;


        transform.DOScale(baseScale.x, anim_speed);
        GoneBig = false;
    }

    #endregion
}

/*
 *        (string title, string description) v = Window_Equipment.ShowEquipment_Description(equipment);

        Main_UI.Display_Description(v.title, v.description, transform.position, 1, Main_UI.DescBoxType.informative, SpellGestion.Class.warrior);
 */
