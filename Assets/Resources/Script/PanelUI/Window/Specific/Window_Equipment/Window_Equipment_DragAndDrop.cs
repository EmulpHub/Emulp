using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Window_Equipment : Window
{
    public GameObject Drag_Graphic_Prefab;

    [HideInInspector]
    public GameObject Drag_Graphic;

    [HideInInspector]
    public SingleEquipment Drag_Equipment;

    public void DragAndDropUpdate()
    {
        if (Drag_Equipment != null && Drag_Graphic != null)
        {
            if (V.game_state == V.State.fight)
                EraseDragGraphic(false);
            else
                UpdateDragGraphicPos();
        }
    }

    public void CreateDragGraphic(SingleEquipment newEquipment)
    {
        Drag_Equipment = newEquipment;

        if (Drag_Graphic != null)
        {
            Destroy(Drag_Graphic.gameObject);
        }

        Drag_Graphic = Instantiate(Drag_Graphic_Prefab.gameObject);

        Drag_Graphic.transform.GetChild(2).GetComponent<Image>().sprite = Drag_Equipment.Graphic;

        UpdateDragGraphicPos();
    }

    public void UpdateDragGraphicPos()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Drag_Graphic.gameObject.transform.position = new Vector3(pos.x, pos.y, 0);
    }

    public Slot_Equiped Slot_MouseOver;

    public bool GoodDragTarget()
    {

        return Slot_MouseOver != null && Drag_Equipment != null && Slot_Equiped.MatchingEquipment(Slot_MouseOver.type, Drag_Equipment.Type);
    }

    public void EraseDragGraphic(bool Equip)
    {
        if (GoodDragTarget() && Equip)
        {
            Equipment_Management.EquipEquipment(Drag_Equipment, true, false, true, Slot_Equiped.typeInMouseOver);
        }

        Drag_Equipment = null;

        if (Drag_Graphic != null)
        {
            Destroy(Drag_Graphic.gameObject);
        }
    }

    public void EraseDragGraphic()
    {
        EraseDragGraphic(true);
    }
}
