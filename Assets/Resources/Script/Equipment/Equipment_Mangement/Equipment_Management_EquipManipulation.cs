using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Equipment_Management : MonoBehaviour
{
    static float timeBeforeNewDisplay;

    public static bool TitleInventoryCanBeShowed()
    {
        return timeBeforeNewDisplay < Time.time;
    }

    public static void timeBeforeNewDisplay_Set()
    {
        timeBeforeNewDisplay = Time.time + 0.2f;
    }

    public static bool EquipSlotIsEmpty(SingleEquipment.type t)
    {
        return !Equiped.ContainsKey(t);
    }

    public static void EquipEquipment(SingleEquipment newEquip, bool withEffect = true, bool Force = false, bool UnequipAllowed = true, SingleEquipment.type specificType = SingleEquipment.type.none)
    {
        if (V.game_state == V.State.fight && !Force)
            return;

        bool NotEquipedContain = NotEquiped.Contains(newEquip);

        if (!NotEquipedContain && !Force)
            throw new System.Exception("cette equipement n'a pas été obtenue par ce joueur (EquipEquipment dans Equipment_Management)");

        Description_text.EraseDispay();

        SingleEquipment.type type = newEquip.Type;

        if (specificType != SingleEquipment.type.none)
        {
            type = specificType;
        }

        List<SingleEquipment.type> typeToChange = new List<SingleEquipment.type> { type };

        if (type == SingleEquipment.type.object_equipment && specificType == SingleEquipment.type.none)
        {
            typeToChange.Add(SingleEquipment.type.object_equipment_2);
        }

        int i = 0;

        while (i < typeToChange.Count)
        {
            SingleEquipment.type t = typeToChange[i];

            if (Equiped.ContainsKey(t) && i == typeToChange.Count - 1)
            {
                newEquip.Equip();

                UnEquipEquipment(t);

                Equiped[t] = newEquip;

                type = t;
            }
            else if (!Equiped.ContainsKey(t))
            {
                newEquip.Equip();

                Equiped.Add(t, newEquip);

                type = t;
                break;
            }


            i++;
        }

        /*
        if (Equiped.ContainsKey(t))
        {
            UnEquipEquipment(t);

            Equiped[t] = newEquip;

            type = t;
        }
        else
        {
            Equiped.Add(t, newEquip);
            type = t;
        }*/


        if (NotEquipedContain)
        {
            NotEquiped.Remove(newEquip);
        }

        if (Window_Equipment.Equiped_Slot.ContainsKey(type))
        {
            Window_Equipment.Equiped_Slot[type].equipment = newEquip;

            if (withEffect)
            {
                Window_Equipment.Equiped_Slot[type].Anim_Click();
                SoundManager.PlaySound(SoundManager.list.equipment_equip);
            }
        }


        CalcAllEquipmentEffect();

        if (newEquip.Type == SingleEquipment.type.weapon)
        {
            ChangeWeapon(newEquip.Spell);
        }
        else if (newEquip.Type == SingleEquipment.type.object_equipment)
        {
            ChangeObject(newEquip.Spell, type != SingleEquipment.type.object_equipment_2);
        }

        timeBeforeNewDisplay_Set();
    }

    public static void ChangeWeapon(SpellGestion.List spell)
    {
        Spell.Weapon.ChangeSpell(spell);
    }

    public static void ChangeObject(SpellGestion.List spell, bool first)
    {
        if (first)
            Spell.Object.ChangeSpell(spell);
        else
            Spell.Object_2.ChangeSpell(spell);

    }

    public static void UnEquipEquipment(SingleEquipment.type type)
    {
        if (V.game_state == V.State.fight)
            return;

        if (!Equiped.ContainsKey(type))
            throw new System.Exception("cette equipement n'a pas été obtenue par ce joueur (EquipEquipment dans Equipment_Management)");

        Description_text.EraseDispay();

        SoundManager.PlaySound(SoundManager.list.equipment_desequip);

        Equipment_InventoryManagement inv = Window_Equipment.window.InventoryManagement;

        if (inv.inventory.Count < inv.getMaxSlotInventoryPerPage())
            inv.CreateInventorySlot(Equiped[type]);

        Slot_Equiped ancientEquip = Window_Equipment.Equiped_Slot[type];

        if (ancientEquip.equipment != null)
        {
            NotEquiped.Add(ancientEquip.equipment);


            ancientEquip.equipment.UnEquip();

            ancientEquip.equipment = null;
        }

        Equiped.Remove(type);

        CalcAllEquipmentEffect();

        if (type == SingleEquipment.type.weapon)
        {
            ChangeWeapon(SpellGestion.List.base_fist);
        }
        else if (type == SingleEquipment.type.object_equipment)
        {
            ChangeObject(SpellGestion.List.empty, true);
        }
        else if (type == SingleEquipment.type.object_equipment_2)
        {
            ChangeObject(SpellGestion.List.empty, false);
        }

        timeBeforeNewDisplay_Set();
    }

    public static void ObtainNewEquipment(SingleEquipment newEquipment)
    {
        if (NotEquiped.Contains(newEquipment) || Equiped.ContainsValue(newEquipment))
            throw new System.Exception("equipement deja contenus dans notEquiped ou equiped (getNewEquipement dans Equipement_Management)");

        NotEquiped.Add(newEquipment);

        Main_Object.Enable(Main_Object.objects.button_equipment);

        try
        {
            LockedLs.Remove(newEquipment);
            Locked[newEquipment.Rarity].Remove(newEquipment);
        }
        catch
        {
            throw new System.Exception("BUG OBTAIN NEW EQUIPMENT with new equip = " + newEquipment.fr_title);
        }
        /*
        if (EquipSlotIsEmpty(newEquipment.Type))
        {
            EquipEquipment(newEquipment);
        }
        else if (newEquipment.Type == SingleEquipment.type.object_equipment && EquipSlotIsEmpty(SingleEquipment.type.object_equipment_2))
        {
            EquipEquipment(newEquipment, true, false, true, SingleEquipment.type.object_equipment_2);
        }
        else
        {*/
            NewEquipment.Add(newEquipment);

            Equipment_InventoryManagement.AcquiredEquipment.Add(newEquipment);
        //}
    }
}
