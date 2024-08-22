using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statsForPlayer : MonoBehaviour
{
    public delegate float calculateVal(Entity e = null);

    private static Dictionary<type, List<(int index, float val)>> value_opti =
        new Dictionary<type, List<(int index, float val)>>();

    private static Dictionary<type, List<(int index, calculateVal val, bool NeedTarget)>> value_del_opti =
        new Dictionary<type, List<(int index, calculateVal val, bool NeedTarget)>>();

    private static Dictionary<int, List<(type t, float val)>> value = new Dictionary<int, List<(type t, float val)>>();

    private static Dictionary<int, List<(type t, calculateVal val, bool NeedTarget)>> value_del = new Dictionary<int, List<(type t, calculateVal val, bool NeedTarget)>>();

    public enum type
    {
        stats_paMax, stats_pmMax, stats_cc_chance, lifeSteal, stats_life, stats_tackle, stats_leak, stats_po, stats_ec,
        effect_weapon_multiplicator, effect_object_multiplicator, effect_nonCc_multiplicator, effect_cc_multiplicator,
        effect_onDistance_multiplicator,
        allDamage_multiplicator, stats_lifeSteal

    }

    private static int index = int.MinValue;

    public static int Add(type t, float val)
    {
        return Add(new List<(type, float)> { (t, val) });
    }

    public static int Add_del(type t, calculateVal val, bool needTargetInfo)
    {
        return Add_del(new List<(type, calculateVal, bool needTarget)> { (t, val, needTargetInfo) });
    }

    public static int Add_del(List<(type t, calculateVal val, bool needTargetInfo)> ls)
    {
        foreach ((type t, calculateVal val, bool NeedTarget) v in ls)
        {
            if (value_del_opti.ContainsKey(v.t))
            {
                value_del_opti[v.t].Add((index, v.val, v.NeedTarget));
            }
            else
            {
                value_del_opti.Add(v.t,
                    new List<(int index, calculateVal val, bool NeedTarget)> { (index, v.val, v.NeedTarget) }
                    );
            }
        }

        value_del.Add(index, ls);

        V.player_info.CalculateValue();

        return ++index;
    }

    public static int Add(List<(type t, float val)> ls)
    {
        foreach ((type t, float val) v in ls)
        {

            if (value_opti.ContainsKey(v.t))
            {
                value_opti[v.t].Add((index, v.val));
            }
            else
            {
                value_opti.Add(v.t,
                    new List<(int index, float val)> { (index, v.val) }
                    );
            }
        }

        value.Add(index, ls);

        index++;

        V.player_info.CalculateValue();

        return index - 1;
    }

    public static void Remove_del(int index, bool firstActivation = true)
    {
        if (!value_del.ContainsKey(index))
            return;

        if (firstActivation)
            Remove(index, false);

        List<(type t, calculateVal v, bool b)> l = value_del[index];

        foreach ((type t, calculateVal v, bool b) val in l)
        {
            Remove_del_opti(val.t, index);
        }

        value_del.Remove(index);

        V.player_info.CalculateValue();

    }

    public static void Remove_del_opti(type t, int index)
    {
        foreach ((int index, calculateVal val, bool NeedTarget) v in new List<(int index, calculateVal val, bool NeedTarget)>(value_del_opti[t]))
        {
            if (v.index == index)
            {
                value_del_opti[t].Remove(v);
            }
        }
        V.player_info.CalculateValue();

    }

    public static void Remove(int index, bool firstActivation = true)
    {
        if (!value.ContainsKey(index))
        {
            return;
        }

        if (firstActivation)
            Remove_del(index, false);

        List<(type t, float v)> l = value[index];

        foreach ((type t, float v) val in l)
        {
            Remove_opti(val.t, index);
        }

        value.Remove(index);

        V.player_info.CalculateValue();

    }

    public static void Remove_opti(type t, int index)
    {
        foreach ((int index, float val) v in new List<(int index, float val)>(value_opti[t]))
        {
            if (v.index == index)
            {
                value_opti[t].Remove(v);
            }
        }

        V.player_info.CalculateValue();

    }

    public static float GetMultiplicator(type t)
    {
        return Get(t) / 100 + 1;
    }

    public static int GetInt(type t)
    {
        return (int)Get(t);
    }

    public static float Get(type t, Entity target = null)
    {
        float nb = 0;

        if (value_opti.ContainsKey(t))
        {
            foreach ((int index, float val) s in value_opti[t])
            {
                nb += s.val;
            }
        }

        if (value_del_opti.ContainsKey(t))
        {
            foreach ((int index, calculateVal val, bool needTargetInfo) s in value_del_opti[t])
            {
                if ((target != null || !s.needTargetInfo))
                {
                    nb += s.val(target);
                }
            }
        }

        return nb;
    }
}
