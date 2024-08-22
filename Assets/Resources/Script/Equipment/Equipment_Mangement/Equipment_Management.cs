using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Equipment_Management : MonoBehaviour
{
    public static List<SingleEquipment> All = new List<SingleEquipment>();

    public static Dictionary<SpellGestion.List, SingleEquipment> ObjectWeapon_List = new Dictionary<SpellGestion.List, SingleEquipment>();

    public static Dictionary<SingleEquipment.rarity, List<SingleEquipment>>
        Locked = new Dictionary<SingleEquipment.rarity, List<SingleEquipment>>();

    public static List<SingleEquipment> LockedLs = new List<SingleEquipment>();

    public static List<SingleEquipment> NotEquiped = new List<SingleEquipment>();

    public static Dictionary<SingleEquipment.type, SingleEquipment>
        Equiped = new Dictionary<SingleEquipment.type, SingleEquipment>();

    public static List<SingleEquipment> NewEquipment = new List<SingleEquipment>();

    public static bool ContainEquipement(string title)
    {
        foreach (SingleEquipment e in Equiped.Values)
        {
            if (e.fr_title == title || e.uk_title == title)
                return true;
        }

        return false;
    }

    public static void Init()
    {

        GameObject list_equipment = Resources.Load<GameObject>("Prefab/Equipment/list_equipment");

        Init(list_equipment.transform);
    }

    public static void Init(Transform target)
    {
        foreach (Transform equipment in target.transform)
        {
            if (equipment.childCount > 0)
            {
                Init(equipment);
            }
            else
            {
                SingleEquipment a = equipment.GetComponent<SingleEquipment>();

                if (a == null)
                    continue;

                a.CreateEffects();

                All.Add(a);

                if (Locked.ContainsKey(a.Rarity))
                {
                    LockedLs.Add(a);
                    Locked[a.Rarity].Add(a);
                }
                else
                {
                    LockedLs.Add(a);
                    Locked.Add(a.Rarity, new List<SingleEquipment> { a });
                }

                if (a.Type == SingleEquipment.type.object_equipment || a.Type == SingleEquipment.type.weapon)
                    ObjectWeapon_List.Add(a.Spell, a);
            }
        }
    }
}
