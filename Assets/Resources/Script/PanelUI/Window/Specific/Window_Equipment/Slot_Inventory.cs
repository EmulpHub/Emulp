using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Slot_Inventory : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool NewItem;

    public Window_Equipment window;

    [HideInInspector]
    public Equipment_InventoryManagement parent_script;

    public SingleEquipment equipment;

    public Image Graphic;

    public Image contour, bg;

    public Color32 contourWhenBig, contourWhenBig_Low, normalcolor, highcolor;

    public GameObject ExclamationPoint;

    public void Init()
    {
        //normalcolor = equipment.GetColor();
        highcolor = equipment.GetColor_High();

        bg.color = new Color32(normalcolor.r, normalcolor.g, normalcolor.b, (byte)(bg.color.a * 255));

        Graphic.sprite = equipment.Graphic;

        baseScale = transform.localScale;

        UpdateNewAnimation();
    }

    public void UpdateNewAnimation()
    {
        ExclamationPoint.gameObject.SetActive(NewItem);
    }

    public void Update()
    {
        Color32 c = normalcolor;

        if (window.showedEquipment == equipment)
        {
            c = contourWhenBig;

            contour.DOColor(contourWhenBig, 0.2f);
        }
        else
        {
            contour.DOColor(normalcolor, 0.2f);
        }

        bg.DOColor(new Color32(c.r, c.g, c.b, (byte)(bg.color.a * 255)), 0.2f);

        DragAndDropUpdate();

        MouseIsOver_Management();
    }

    public void MouseIsOver_Management()
    {
        if (!MouseOver)
            return;

        ShowTitle();
    }

    #region DragAndDrop

    public Vector2 MousePosWhenClicked;

    public void DragAndDropUpdate()
    {
        if (MouseOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                MousePosWhenClicked = CursorInfo.Instance.positionInWorld;
            }

            if (Input.GetMouseButton(0) && window.Drag_Equipment == null
                && CursorInfo.Instance.positionInWorld != MousePosWhenClicked && V.game_state != V.State.fight)
            {
                Anim_Click();
                DragThis = true;
                window.CreateDragGraphic(equipment); //Start Drag 
            }
        }

        if (DragThis && !Input.GetMouseButton(0))
        {
            bool destroyThisObject = window.GoodDragTarget();

            window.EraseDragGraphic();

            EraseTitle();

            if (destroyThisObject)
                DestroyThis();

            DragThis = false;

        }
    }

    [HideInInspector]
    public float lastTimeClicked = -1;

    bool DragThis = false;
    bool MouseOver = false;

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (Equipment_Management.NewEquipment.Contains(equipment))
        {
            Equipment_Management.NewEquipment.Remove(equipment);
            NewItem = false;

            UpdateNewAnimation();
        }

        window.HighlighSlot = this;

        Anim_Enter();

        MouseOver = true;
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Anim_Exit();

        MouseOver = false;

        if (window.HighlighSlot = this)
            window.HighlighSlot = null;
    }

    #endregion

    #region Click

    public void OnPointerClick(PointerEventData pointerEventData)
    {

        Anim_Click();

        EraseTitleImmediate();

        if (Time.time - lastTimeClicked <= 0.3f)
        {
            DoubleClick();
            lastTimeClicked = 0;
        }
        else
        {
            SimpleClick();
        }

        lastTimeClicked = Time.time;
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
        if (window.Drag_Equipment != null)
            return;

        if (V.game_state == V.State.fight)
        {
            window.SendFightError();
            return;
        }

        Equipment_Management.EquipEquipment(equipment);

        DestroyThis();
    }

    #endregion

    public void DestroyThis()
    {
        parent_script.RemoveSlot(this);

        Destroy(this.gameObject);
    }

    #region animation

    Vector3 baseScale;

    public float multiplierMouseOver, anim_speed, shakeVector;

    public void Anim_Enter()
    {
        //transform.DOKill();

        transform.localScale = baseScale * multiplierMouseOver;

        ShowTitle();
    }

    public void Anim_Exit()
    {
        EraseTitle();

        //transform.DOKill();

        transform.DOScale(baseScale.x, anim_speed);
    }

    public void Anim_Click()
    {
        //transform.DOKill();

        transform.localScale = baseScale;

        transform.DOPunchScale(new Vector3(shakeVector, shakeVector, 0), anim_speed, 1);
    }

    #endregion

    public void ShowTitle()
    {
        if (!Equipment_Management.TitleInventoryCanBeShowed())
            return;

        ShowEquipmentInfoDescOrTitle(equipment, transform.position);
    }

    public static void ShowEquipmentInfoDescOrTitle(SingleEquipment e, Vector3 pos)
    {
        /*
        if (e.effects_type.Count == 0)
        {
            //V.Color RarColor = equipment.getColorFromRarity();

            Main_UI.Display_Title(e.GetTitle(), pos, 1.5f);
            Main_UI.Display_Description_Erase();

        }
        else
        {
            Main_UI.Display_Title_Erase();

            Slot_Equiped.DisplayEquipmentInfo(e, pos, 1);
        }*/
        Slot_Equiped.DisplayEquipmentInfo(e, pos, 0.2f);
    }

    public void EraseTitle()
    {
        Invoke("EraseTitleDelayed", 0.1f);
    }

    public void EraseTitleDelayed()
    {
        if (MouseOver || (window.HighlighSlot != this && window.HighlighSlot != null))
            return;

        EraseTitleImmediate();
    }

    public void EraseTitleImmediate()
    {
        Main_UI.Display_Title_Erase();
        Description_text.EraseDispay();
    }
}
