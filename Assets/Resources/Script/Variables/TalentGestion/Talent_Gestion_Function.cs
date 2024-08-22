
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Talent_Gestion : MonoBehaviour
{
    /*
    /// <summary>
    /// Check if a talent is equip, if yes apply it
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="amount"></param>
    public static void TryTalent(talent sp, int amount, Entity target, Entity caster)
    {
        if (EquipedTalent(sp))
        {
            ApplyTalent(sp, amount, target, caster);
        }
    }

    /// <summary>
    /// Check if a talent is equip, if yes apply it
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="amount"></param>
    public static void TryTalent(talent sp, Entity target, Entity caster)
    {
        TryTalent(sp, 0, target, caster);
    }

    /// <summary>
    /// Check if a talent is equip, if yes apply it
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="amount"></param>
    public static void TryTalent(talent sp)
    {
        TryTalent(sp, 0, null, null);
    }

    /// <summary>
    /// Check if a talent exist
    /// </summary>
    /// <param name="sp"></param>
    /// <returns></returns>
    public static bool EquipedTalent(talent sp)
    {
        if (!Character.ContainTalent(sp))
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Apply talent effect
    /// </summary>
    /// <param name="sp"></param>
    public static void ApplyTalent(talent sp, Entity target, Entity caster)
    {
        ApplyTalent(sp, 0, target, caster);
    }

    /// <summary>
    /// Apply talent effect
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="amount"></param>
    public static void ApplyTalent(talent sp, int amount, Entity target, Entity caster)
    {
        /*if (sp == talent.warrior_talent_continuity)
        {
            Apply_Continuity();
        }
        else if (sp == talent.warrior_talent_murderer)
        {
            Apply_Murderer();
        }
        else if (sp == talent.warrior_talent_evolution)
        {
            Apply_Evolution();
        }*/

    //SpellGestion.Class classe = SpellGestion.Get_classe(sp);
    //}*/
}
