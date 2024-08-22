using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SpellGestion : MonoBehaviour
{
    public static void Initialize_player_weapon()
    {
        Color32 col = spellInteriorCol_weapon;

        AddSpell_info(List.weapon_crowBar, range: "1_2", pa_cost: 3, cd: 1, Spell.Range_type.normal, CursorMode.physical, range_effect_size.singleTarget, TargetMode.all, col, true);
        //AddSpell_info(List.weapon_ak47, range: "2_5", pa_cost: 3, cd: -3, Combat_spell.Range_type.normal, CursorMode.physical, range_effect_size.singleTarget, TargetMode.all, col, true);
        //AddSpell_info(List.weapon_sniper, range: "3_6", pa_cost: 4, cd: -3, Combat_spell.Range_type.normal, CursorMode.physical, range_effect_size.singleTarget, TargetMode.all, col, true);
        //AddSpell_info(List.weapon_bow, range: "2_5", pa_cost: 3, cd: -3, Combat_spell.Range_type.normal, CursorMode.physical, range_effect_size.singleTarget, TargetMode.all, col, true);
        AddSpell_info(List.weapon_taser, range: "1", pa_cost: 3, cd: 1, Spell.Range_type.normal, CursorMode.physical, range_effect_size.line_2, TargetMode.all, col, true);
        //AddSpell_info(List.weapon_mace, range: "1", pa_cost: 3, cd: -3, Combat_spell.Range_type.normal, CursorMode.physical, range_effect_size.Cone_Inverted, TargetMode.all, col, true);
        AddSpell_info(List.weapon_littleSword, range: "1", pa_cost: 3, cd: -3, Spell.Range_type.normal, CursorMode.physical, range_effect_size.Front3line, TargetMode.all, col, true);
        //AddSpell_info(List.weapon_magicWand, range: "2_3", pa_cost: 3, cd: -3, Combat_spell.Range_type.normal, CursorMode.magical, range_effect_size.oneSquareAround, TargetMode.all, col, true);
        //AddSpell_info(List.weapon_statue, range: "2_3", pa_cost: 3, cd: -3, Combat_spell.Range_type.noNeedOfLineOfView, CursorMode.physical, range_effect_size.oneSquareAround, TargetMode.all, col, true);
        //AddSpell_info(List.weapon_dagger, range: "2_4", pa_cost: 3, cd: -3, Combat_spell.Range_type.normal, CursorMode.physical, range_effect_size.singleTarget, TargetMode.all, col, true);
        //AddSpell_info(List.weapon_bowlingBall, range: "1_4", pa_cost: 2, cd: -3, Combat_spell.Range_type.line, CursorMode.physical, range_effect_size.Cone, TargetMode.all, col, true);
        AddSpell_info(List.weapon_spear, range: "1", pa_cost: 3, cd: 1, Spell.Range_type.normal, CursorMode.physical, range_effect_size.oneSquareAround, TargetMode.all, col, true);
        AddSpell_info(List.weapon_steal, range: "1_5", pa_cost: 3, cd: 1, Spell.Range_type.normal, CursorMode.physical, range_effect_size.singleTarget, TargetMode.all, col, true);
        AddSpell_info(List.weapon_shieldAttack, range: "1_3", pa_cost: 3, cd: -2, Spell.Range_type.normal, CursorMode.physical, range_effect_size.singleTarget, TargetMode.all, col, true);
        AddSpell_info(List.weapon_knife, range: "1_2", pa_cost: 3, cd: -2, Spell.Range_type.line, CursorMode.physical, range_effect_size.singleTarget, TargetMode.all, col, true);

        if (V.IsFr())
        {
            ModifySpell_string(List.weapon_crowBar, "Pied de biche", "Inflige *dmg 18 dégâts *end et pousse de *bon 2 case *end");
            //ModifySpell_string(List.weapon_ak47, "Ak47", "Inflige *dmg 5 dégâts*end 3 fois");
            //ModifySpell_string(List.weapon_sniper, "Snip er", "Inflige *dmg 15 dégâts*end + *dmg5 par case entre vous et la cible*end");
            //ModifySpell_string(List.weapon_bow, "Arc", "Inflige *dmg 15 dégâts *end");
            ModifySpell_string(List.weapon_taser, "Taser", "Inflige *dmg 16 dégâts *end, retire *bon 2 pa*end");
            //ModifySpell_string(List.weapon_mace, "Massue", "Inflige *dmg 10 dégâts*end + *spe5% de vos points de vie maximum*end");
            ModifySpell_string(List.weapon_littleSword, "Petite epee", "Inflige *dmg 16 dégâts *end\nVotre prochaine attaque a *bon+7 dommage*end");
            //ModifySpell_string(List.weapon_magicWand, "Baguette magique", "Inflige *dmg 5 dégâts *end. Effectuer une action vous apporte *bon 1 magie*end. Chaque *bon magie *end augmente les dégâts de cette attaque de *bon 2 (max 20)*end");
            //ModifySpell_string(List.weapon_statue, "Statue", "Inflige *dmg 10 dégâts *end. Si vos pm sont a *pdm 0 *end inflige *dmg 24 dégats*end a la place");
            //ModifySpell_string(List.weapon_dagger, "Dague", "Lance *bon 2 dague *end infligeant chacune *dmg 6 dégâts *end");
            //ModifySpell_string(List.weapon_bowlingBall, "Balle de bowling", "Inflige *dmg1 à 10 dégâts*end\nVotre prochain sort à *bon+20% chance de coup critique*end");
            ModifySpell_string(List.weapon_spear, "Lance", "Inflige *dmg15 dégâts*end*spe(1 case)*end\n*eff+2 pa*end par ennemie toucher");
            ModifySpell_string(List.weapon_steal, "Vole", "Inflige *dmg16 dégâts*end\nVole *bon1 pm*end");
            ModifySpell_string(List.weapon_shieldAttack, "Attaque au bouclier", "Inflige *dmg13 dégâts*end\n*arm+15 armure*end");
            ModifySpell_string(List.weapon_knife, "Attaque au couteau", "Inflige *dmg20 dégâts*end\n+50% vol de vie pour ce sort");

        }
        else
        {
            //ModifySpell_string(List.weapon_bow, "Bow", "Deal *dmg 15 damage *end");
            ModifySpell_string(List.weapon_crowBar, "Crow bar", "Deal *dmg 18 damage *end and push *man 2 square *end");
            //ModifySpell_string(List.weapon_ak47, "Ak47", "Deal *dmg 5 damage*end 3 times ");
            //ModifySpell_string(List.weapon_sniper, "Sniper", "Deal *dmg 15 damage*end + *dmg5 per square between you and the target*end");
            ModifySpell_string(List.weapon_taser, "Taser", "Deal *dmg 16  damage *end, remove *bon 2 ap *end");
            //ModifySpell_string(List.weapon_mace, "Mace", "Deal *dmg 10 damage*end + *spe5% of your maximum life*end");
            ModifySpell_string(List.weapon_littleSword, "Little sword", "Deal *dmg 16 damage *end\nYour next attack deal *bon+7 damage*end");
            //ModifySpell_string(List.weapon_magicWand, "Magic wand", "Deal *dmg 5 damage *end. Performing an action gives you *bon 1 magic*end. Each *bon magic *end increases the damage of this attack by *bon 2 (max 20)*end");
            //ModifySpell_string(List.weapon_statue, "Statue", "Deal *dmg 10 damage *end. If your pm are at *pdm 0 *end deal *dmg 24 damage *end instead");
            //ModifySpell_string(List.weapon_dagger, "Dagger", "Throw *bon 2 dagger *end each inflicting *dmg 6 damage *end");
            ModifySpell_string(List.weapon_spear, "Spear", "Deal *dmg15 damage*end*spe(1 square)*end\n*eff+2 ap*end per ennemy targeted");
            ModifySpell_string(List.weapon_steal, "Steal", "Deal *dmg16 damage*end\nSteal *bon1 mp*end");
            ModifySpell_string(List.weapon_shieldAttack, "Shield attack", "Deal *dmg13 damage*end\n*arm+15 armor*end");
            ModifySpell_string(List.weapon_knife, "Knife attack", "Deal *dmg20 damage*end\n+50% lifesteal this turn");

        }
    }
}
