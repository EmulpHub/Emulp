using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SpellGestion : MonoBehaviour
{

    /// <summary>
    /// Initialize all spell that both player and monster can have
    /// </summary>
    public static void Initialize_common()
    {
        AddSpell_info(List.empty, "0_0", 0, 0, Spell.Range_type.normal, CursorMode.physical, range_effect_size.singleTarget, TargetMode.entity, spellInteriorCol_normal);

        if (V.IsFr())
        {
            ModifySpell_string(List.empty, "Vide", "emplacement vide");
        }
        else
        {
            ModifySpell_string(List.empty, "Empty", "empty slot");
        }
    }

    /// <summary>
    /// Initalize spell that all classe can have
    /// </summary>
    public static void Initialize_player_base()
    {
        AddSpell_info(List.base_fist, range: "1_1", pa_cost: 2, cd: -2, Spell.Range_type.normal, CursorMode.physical, range_effect_size.singleTarget, TargetMode.all, spellInteriorCol_weapon, true);


        if (V.IsFr())
        {
            ModifySpell_string(List.base_fist, "Poing", "*dmg 5 dégâts *end");
        }
        else
        {
            ModifySpell_string(List.base_fist, "Fist", "*dmg 5 damage *end");
        }
    }
}
