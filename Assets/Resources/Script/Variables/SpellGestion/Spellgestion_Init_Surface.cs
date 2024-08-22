using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SpellGestion : MonoBehaviour
{

    /// <summary>
    /// Initialize spell that the warrior class have
    /// </summary>
    public static void Initialize_player_origin_surface()
    {
        Color32 col = spellInteriorCol_surface;

        AddSpell_info(List.surface_bow, range: "1_3", pa_cost: 2, cd: -2, Spell.Range_type.normal, CursorMode.physical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.surface_fieryShot, range: "3_4", pa_cost: 3, cd: 2, Spell.Range_type.noNeedOfLineOfView, CursorMode.physical, range_effect_size.twoSquareAround, TargetMode.all, col);
        AddSpell_info(List.surface_fallback, range: "1_3", pa_cost: 2, cd: 4, Spell.Range_type.line, CursorMode.physical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.surface_longBow, range: "1_3", pa_cost: 3, cd: -2, Spell.Range_type.normal, CursorMode.physical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.surface_shuriken, range: "1_3", pa_cost: 4, cd: -2, Spell.Range_type.normal, CursorMode.physical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.surface_sharpenedArrow, range: "2_3", pa_cost: 3, cd: -2, Spell.Range_type.normal, CursorMode.physical, range_effect_size.oneSquareAround, TargetMode.all, col);
        AddSpell_info(List.surface_arrow, range: "1_4", pa_cost: 3, cd: -2, Spell.Range_type.normal, CursorMode.physical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.surface_bandage, range: "0", pa_cost: 2, cd: 3, Spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.surface_turret, range: "0", pa_cost: 3, cd: 5, Spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.surface_laceration, range: "0", pa_cost: 4, cd: 5, Spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.surface_jump, range: "1_2", pa_cost: 3, cd: 1, Spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.empty, col);
        AddSpell_info(List.surface_woodenShield, range: "0", pa_cost: 2, cd: 6, Spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.surface_vision, range: "0", pa_cost: 2, cd: 7, Spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.surface_energy, range: "0", pa_cost: 0, cd: 5, Spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);

        if (V.IsFr())
        {
            ModifySpell_string(List.surface_arrow, "Fleche", "*dmg 10 dégâts*end\n*bon+1 po*end");
            ModifySpell_string(List.surface_fieryShot, "Tir enflammé", "*dmg 15 dégâts*end\nLes ennemis touchés sont attirés de *bon1 cases*end vers le centre");
            ModifySpell_string(List.surface_fallback, "Repli", "*dmg 7 dégâts*end\nReculez de *bon2 cases*end et gagnés *arm10 armure*end");
            ModifySpell_string(List.surface_longBow, "Arc long", "*dmg 10 dégâts + 5*end par po possédez");
            ModifySpell_string(List.surface_shuriken, "Shuriken", "Lance 2 couteau infligeant *dmg 5 dégâts*end chacun\nPour chaque 2 po possédez lancez un shuriken supplementaire");
            ModifySpell_string(List.surface_sharpenedArrow, "Fléche aiguisée", "*dmg 10 dégâts*end\n20% des dégâts infligés sont convertis en saignement pour la cible");
            ModifySpell_string(List.surface_bow, "Arc", "*dmg 8 dégâts*end\n*arm+5 armure*end");
            ModifySpell_string(List.surface_bandage, "Bandage", "Soigne de *bon20*end");
            ModifySpell_string(List.surface_turret, "Mode tourelle", "Convertissez vos pm en po. Et gagnez l'effet tourelle pendant ce tour : *bon+25% d'effet*end sur vos sorts, la premiere attaque contre un monstre le pousse de *bon2 cases*end");
            ModifySpell_string(List.surface_laceration, "Laceration", "Les ennemies avec saignement subisent *bon50%*end leur dégats de saignement\n Gagnez *arm10 armure*end par ennemie affectés");
            ModifySpell_string(List.surface_jump, "Saut", "Téléporte a la case ciblé\n*arm+5 armure*end");
            ModifySpell_string(List.surface_woodenShield, "Bouclier en bois", "*arm+15 armure*end et *pdm1 pm*end\nVous ne pouvez pas être tacklé ce tour");
            ModifySpell_string(List.surface_vision, "Vision", "*bon+1 po (3 tours)*end");
            ModifySpell_string(List.surface_energy, "Energy", "*bon+3 pa*end");

        }
        else
        {

        }
    }
}
