using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public partial class Equipment_InventoryManagement : MonoBehaviour
{
    public void Start()
    {
        Init();
    }

    public void Update()
    {
        if (AcquiredEquipment.Count > 0)
            UpdateInventory();

        PageManagement();
    }

    public Window_Equipment window;

    public GameObject inventory_slot;

    public Transform inventory_slot_transform;

    public void Init()
    {
        if (V.script_Scene_Main_Administrator.unlockAllEquipments)
        {
            Equipment_Management.NotEquiped = Equipment_Management.All;
        }

        AcquiredEquipment.Clear();

        CreateAllSlotInventory(CurrentPage, currentSortingMode);
    }

    public enum sort_mode { rarity_UpToLower, rarity_LowertoUp, Equipment_helmet, Equipment_chest, Equipment_Belt, Equipment_Pant, Equipment_Boot, Equipment_Weapon, Equipment_Object, Equipment_Object_2 }

    public List<SingleEquipment> AllEquipment_inInventory = new List<SingleEquipment>();

    public void CreateAllSlotInventory(int page, sort_mode sort = sort_mode.rarity_UpToLower)
    {
        //CreateAllSlot
        inventory_clear();

        foreach (Transform child in inventory_slot_transform)
        {
            Destroy(child.gameObject);
        }

        //Determine ls
        List<SingleEquipment> Ls = new List<SingleEquipment>();

        if (sort == sort_mode.rarity_UpToLower)
            Ls = sort_rarity(true);
        else if (sort == sort_mode.rarity_LowertoUp)
            Ls = sort_rarity(false);
        else if (sort == sort_mode.Equipment_helmet)
            Ls = sort_rarity(true, SingleEquipment.type.helmet);
        else if (sort == sort_mode.Equipment_chest)
            Ls = sort_rarity(true, SingleEquipment.type.chest);
        else if (sort == sort_mode.Equipment_Belt)
            Ls = sort_rarity(true, SingleEquipment.type.belt);
        else if (sort == sort_mode.Equipment_Boot)
            Ls = sort_rarity(true, SingleEquipment.type.boot);
        else if (sort == sort_mode.Equipment_Pant)
            Ls = sort_rarity(true, SingleEquipment.type.pant);
        else if (sort == sort_mode.Equipment_Weapon)
            Ls = sort_rarity(true, SingleEquipment.type.weapon);
        else if (sort == sort_mode.Equipment_Object)
            Ls = sort_rarity(true, SingleEquipment.type.object_equipment);

        AllEquipment_inInventory = Ls;

        //Call
        CreateAllSlotInventory_instantiate(page, Ls);
    }

    public List<SingleEquipment> sort_none()
    {
        return Equipment_Management.NotEquiped;
    }

    public List<SingleEquipment> sort_rarity(bool UpToLower, SingleEquipment.type OnlyEquip = SingleEquipment.type.none)
    {
        Dictionary<int, List<SingleEquipment>> dc =
            new Dictionary<int, List<SingleEquipment>>();

        List<int> allRarityLevel = new List<int>();

        bool EquipRestriction = OnlyEquip != SingleEquipment.type.none;

        foreach (SingleEquipment s in Equipment_Management.NotEquiped)
        {
            if ((EquipRestriction && s.Type != OnlyEquip) || s == null)
            {
                continue;
            }

            int rarity_Int = (int)s.Rarity * (UpToLower ? -1 : 1);

            if (dc.ContainsKey(rarity_Int))
            {
                dc[rarity_Int].Add(s);
            }
            else
            {
                allRarityLevel.Add(rarity_Int);
                dc.Add(rarity_Int, new List<SingleEquipment> { s });
            }
        }

        List<SingleEquipment> ls = new List<SingleEquipment>();
        allRarityLevel.Sort();

        foreach (int r in allRarityLevel)
        {
            ls.AddRange(dc[r]);
        }

        return ls;
    }

    public int getMaxSlotInventoryPerPage()
    {
        return slotPerLine * MaxLine;
    }

    public int getStartIndexFromPage(int page)
    {
        return (page - 1) * getMaxSlotInventoryPerPage();
    }

    public void CreateAllSlotInventory_instantiate(int page, List<SingleEquipment> ls)
    {
        int count = 0;

        int i = getStartIndexFromPage(page);

        while (i < ls.Count)
        {
            CreateInventorySlot(ls[i]);

            i++;
            count++;
            if (count / slotPerLine >= MaxLine)
            {
                NextPageExist = true;
                break;
            }
            else
            {
                NextPageExist = false;
            }
        }
    }

    public void CreateInventorySlot(SingleEquipment theEquipment)
    {
        CreateInventorySlot(inventory.Count, theEquipment);
    }

    public void CreateInventorySlot(int index, SingleEquipment theEquipment)
    {
        GameObject g = Instantiate(inventory_slot, inventory_slot_transform);

        Slot_Inventory a = g.GetComponent<Slot_Inventory>();

        a.NewItem = Equipment_Management.NewEquipment.Contains(theEquipment);

        CalcPos_InventorySlot(a, index, true);

        a.equipment = theEquipment;

        a.window = window;

        a.parent_script = this;

        a.Init();

        inventory_add(a);
    }

    public void RemoveSlot(Slot_Inventory slot)
    {
        inventory_remove(slot);

        ResetAllInventorySlotPos();
    }

    public static List<SingleEquipment> AcquiredEquipment = new List<SingleEquipment>();

    public void UpdateInventory()
    {
        foreach (SingleEquipment a in new List<SingleEquipment>(AcquiredEquipment))
        {
            CreateInventorySlot(a);

            AcquiredEquipment.Remove(a);
        }

        ResetAllInventorySlotPos();
    }

    public void ResetAllInventorySlotPos()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            CalcPos_InventorySlot(inventory[i], i, false);
        }

        AddSlotInventoryFromTheNextPage();

    }

    public void AddSlotInventoryFromTheNextPage()
    {
        int m = getMaxSlotInventoryPerPage() * CurrentPage;

        if (inventory.Count <= m && m <= AllEquipment_inInventory.Count)
        {
            int index = getStartIndexFromPage(CurrentPage + 1);

            SingleEquipment toAdd = AllEquipment_inInventory[index];

            while (inventory_equipment.Contains(toAdd) && AllEquipment_inInventory.Count > index + 1)
            {
                index++;

                toAdd = AllEquipment_inInventory[index];
            }

            CreateInventorySlot(toAdd);
        }
    }

    public void CalcPos_InventorySlot(Slot_Inventory slot, int index, bool init)
    {
        Vector3 newPos = CalcPos(index);

        RectTransform slot_rect = slot.GetComponent<RectTransform>();

        Vector3 oldPos = slot_rect.anchoredPosition;

        float AlphaStart = 0.8f;

        if (init)
        {
            slot_rect.anchoredPosition = new Vector2(CalcPos_x(index % slotPerLine), newPos.y);
        }
        else if (newPos.y != oldPos.y)
        {
            AlphaStart = 0;

            slot_rect.anchoredPosition = new Vector2(CalcPos_x(slotPerLine + 1), newPos.y);
        }

        if (oldPos != newPos)
        {
            CanvasGroup sp = slot_rect.gameObject.transform.GetChild(0).GetComponent<CanvasGroup>();

            sp.DOFade(AlphaStart, 0);

            sp.DOFade(1, 0.35f);
        }

        slot_rect.DOAnchorPos(newPos, 0.35f);
    }

    public Vector2 Calc_start;

    public float Calc_diffPosition;

    public int slotPerLine, MaxLine;

    public Vector2 CalcPos(int index)
    {
        return Calc_start + new Vector2(CalcPos_x(index % slotPerLine), CalcPos_y(index / slotPerLine));
    }

    public float CalcPos_x(int index)
    {
        return Calc_diffPosition * index;
    }

    public float CalcPos_y(int index)
    {
        return -Calc_diffPosition * index;
    }

    #region inventoryManagement

    public List<Slot_Inventory> inventory = new List<Slot_Inventory>();
    public List<SingleEquipment> inventory_equipment = new List<SingleEquipment>();

    public void inventory_clear()
    {
        inventory.Clear();
        inventory_equipment.Clear();
    }

    public void inventory_add(Slot_Inventory e)
    {
        inventory.Add(e);
        inventory_equipment.Add(e.equipment);
    }

    public void inventory_remove(Slot_Inventory e)
    {
        inventory.Remove(e);
        inventory_equipment.Remove(e.equipment);
    }

    #endregion

}
