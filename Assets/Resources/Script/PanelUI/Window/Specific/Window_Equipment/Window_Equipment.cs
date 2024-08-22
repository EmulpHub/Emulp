using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Window_Equipment : Window
{
    // :)

    public Equipment_InventoryManagement InventoryManagement;

    public static Window_Equipment window;

    public Slot_Inventory HighlighSlot;

    public void Start()
    {
        window = this;

        ShowEquipment_Update();

        SetEquipedSlotListAndValue();
    }

    public override void Update()
    {
        base.Update();

        DragAndDropUpdate();
    }

    public static Dictionary<SingleEquipment.type, Slot_Equiped> Equiped_Slot = new Dictionary<SingleEquipment.type, Slot_Equiped>();

    public Transform Equiped_Slot_Parent;

    public void SetEquipedSlotListAndValue()
    {
        Equiped_Slot.Clear();

        foreach (Transform child in Equiped_Slot_Parent)
        {
            Slot_Equiped a = child.gameObject.GetComponent<Slot_Equiped>();

            Equiped_Slot.Add(a.type, a);
        }

        foreach (SingleEquipment.type a in new List<SingleEquipment.type>(Equipment_Management.Equiped.Keys))
        {
            Equipment_Management.EquipEquipment(Equipment_Management.Equiped[a], false, true, false, a);
        }
    }

    public void SendFightError()
    {
        if (V.IsFr())
            SendError("Vous ne pouvez pas faire ça en combat");
        else
            SendError("You can't do that while in fight");
    }

    public override void Close(bool ignoreAutorization = false)
    {
        EraseDragGraphic(false);

        Main_UI.Display_Title_Erase();

        base.Close(ignoreAutorization);
    }
}