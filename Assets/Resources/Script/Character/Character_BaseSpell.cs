using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Character : MonoBehaviour
{
    public static Dictionary<int, baseSpellInfo> baseSpellDic = new Dictionary<int, baseSpellInfo>();

    public static List<int> baseSpellDic_Locked = new List<int>();

    public static void baseSpellInit()
    {
        List<BaseSpellInfo> l = Origin.Get_Base();

        if (l.Count == 0)
            return;

        baseSpellDic.Clear();

        foreach(BaseSpellInfo info in l)
        {
            if (info.levelRequired == 0) continue;

            addBaseSpell(info.levelRequired, info.spell);
        }
    }

    public static void addBaseSpell(int lvl, SpellGestion.List sp)
    {
        baseSpellDic.Add(lvl, new baseSpellInfo(lvl, sp));

        baseSpellDic_Locked.Add(lvl);
    }

    public static SpellGestion.List getBaseSpellFromLvl(int lvl)
    {
        if (baseSpellDic.ContainsKey(lvl))
            return baseSpellDic[lvl].spell;

        return SpellGestion.List.none;
    }

    public static SpellGestion.List TakeAllPreviousSpell(int lvl)
    {
        SpellGestion.List lastSpell = SpellGestion.List.none;

        int i = 0;

        while (i < baseSpellDic_Locked.Count)
        {
            int val = baseSpellDic_Locked[i];

            if (val < lvl)
            {
                baseSpellDic_Locked.Remove(val);

                lastSpell = baseSpellDic[val].spell; Main_UI.Toolbar_AddSpell(baseSpellDic[val].spell);
            }
            else
            {
                i++;
            }
        }

        return lastSpell;
    }
}

public class baseSpellInfo
{
    public int levelRequired;

    public SpellGestion.List spell;

    public baseSpellInfo(int lvl, SpellGestion.List sp)
    {
        levelRequired = lvl;
        spell = sp;
    }
}