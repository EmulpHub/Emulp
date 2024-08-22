using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class RelicInit : MonoBehaviour
{
    #region relic ls

    public enum relicLs
    {
        equipment, life, distance, criticalExpert, spikyArmor, endurance,
        killer, allOrNothing, vampire, energetic, doble
    }

    public static List<relicLs> equipedRelic = new List<relicLs>();

    public static void EquipRelic(relicLs r)
    {
        equipedRelic.Add(r);

        relic_get(r).Add();

        V.player_info.CalculateValue();
    }

    public static void RemoveRelic(relicLs r)
    {
        if (!equipedRelic.Contains(r))
            return;

        equipedRelic.Remove(r);

        relic_get(r).Remove();

        V.player_info.CalculateValue();
    }

    public static void ClearEquipedRelic()
    {
        foreach (relicLs l in new List<relicLs>(equipedRelic))
        {
            RemoveRelic(l);
        }
    }

    public static bool IsEquipedRelic(relicLs r)
    {
        return equipedRelic.Contains(r);
    }

    #endregion

    #region info

    public static Dictionary<relicLs, relic> relicInfo = new Dictionary<relicLs, relic>();

    public static string relic_title(relicLs r)
    {
        return relicInfo[r].title;
    }

    public static string relic_desc(relicLs r)
    {
        return descColor.convert(relicInfo[r].description);
    }

    public static Sprite relic_sprite(relicLs r)
    {
        return relicInfo[r].sp;
    }

    public static List<(SingleEquipment.value_type t, int value)> relic_equipmentValue(relicLs r)
    {
        return relicInfo[r].equipmentValue;
    }


    public static relic relic_get(relicLs r)
    {
        return relicInfo[r];
    }

    #endregion
}
