using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Equipment_Management : MonoBehaviour
{
    public static Dictionary<SingleEquipment.value_type, int> Effects = new Dictionary<SingleEquipment.value_type, int>();

    public static void CalcAllEquipmentEffect()
    {
        Effects.Clear();

        foreach (SingleEquipment a in Equiped.Values)
        {
            foreach ((int value, SingleEquipment.value_type type) v in a.effects)
            {
                AddEffectToAllEquipment(v);
            }
        }

        V.player_info.CalculateValue();
        V.player_info.ResetAllStats();
    }

    public static void ClearAllEquipmentEffect()
    {
        Effects.Clear();

    }

    public static int GetEquipmentValue(SingleEquipment.value_type type)
    {
        if (Effects.ContainsKey(type))
            return Effects[type];

        return 0;
    }

    private static void AddEffectToAllEquipment((int value, SingleEquipment.value_type type) v)
    {
        if (Effects.ContainsKey(v.type))
        {
            Effects[v.type] += v.value;
        }
        else
        {
            Effects.Add(v.type, v.value);
        }
    }
}
