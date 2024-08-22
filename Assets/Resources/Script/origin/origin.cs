using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Origin : MonoBehaviour
{
    public enum Value { none, underground, surface, norticeLand }

    public static Value player_origin { get
        {
            if (player_originInfo == null) return Value.none;

            return player_originInfo.origin;
        }
    }

    public static originInfo player_originInfo;

    public static Dictionary<Value, originInfo> dicValueOriginInfo = new Dictionary<Value, originInfo>();
    public static List<Value> lsChoosableOrigin = new List<Value>();

    public static bool IsOrigin(Value newOrigin)
    {
        return player_origin == newOrigin;
    }

    public static void ChangeOrigin(Value newOrigin)
    {
        if (newOrigin == Value.none) {
            player_originInfo = null;
            return;
        }

        player_originInfo = dicValueOriginInfo[newOrigin];

        Character.baseSpellInit();

        ApplyFirstClassSpell();
    }

    public static void ApplyFirstClassSpell(bool force = true)
    {
        if (V.Tutorial_Get() && !force)
            return;

        foreach (BaseSpellInfo baseSpell in Get_Base())
        {
            if(baseSpell.levelRequired == 0)
            {
                SpellGestion.VerifyAndAddSpell(baseSpell.spell);
            }
        }
    }

    public static string Get_Title(Value or = Value.none)
    {
        if(or == Value.none) return player_originInfo.title;

        return dicValueOriginInfo[or].title;
    }

    public static string Get_Description(Value or = Value.none)
    {
        if (or == Value.none) return player_originInfo.title;

        return dicValueOriginInfo[or].description;
    }

    public static Sprite Get_Sprite(Value or = Value.none)
    {
        if (or == Value.none) return player_originInfo.image;

        return dicValueOriginInfo[or].image;
    }

    public static List<Origin_Passive.Value> Get_Passive(Value or = Value.none)
    {
        if (or == Value.none) return player_originInfo.passive;

        return dicValueOriginInfo[or].passive;
    }

    public static List<SpellGestion.List> Get_Actif(Value or = Value.none)
    {
        if (or == Value.none) return player_originInfo.spell_actif;

        return dicValueOriginInfo[or].spell_actif;
    }

    public static List<SpellGestion.List> Get_Inactive(Value or = Value.none)
    {
        if (or == Value.none) return player_originInfo.spell_inactif;

        return dicValueOriginInfo[or].spell_inactif;
    }

    public static List<BaseSpellInfo> Get_Base(Value or = Value.none)
    {
        if (or == Value.none) return player_originInfo.spell_base;

        return dicValueOriginInfo[or].spell_base;
    }
}
