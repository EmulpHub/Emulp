using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SpellGestion : MonoBehaviour
{
    public static void Initialize_player_object()
    {
        Color32 col = spellInteriorCol_weapon;

        AddSpell_info(List.object_healtPotion, range: "0", pa_cost: 0, cd: 6, Spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.object_empty, range: "0", pa_cost: 0, cd: 10, Spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        //AddSpell_info(List.object_energeticDrink, range: "0", pa_cost: 0, cd: 4, Combat_spell.Range_type.normal, CursorMode.magical, range_effect_size.threeSquareAround_line, TargetMode.all, col);
        AddSpell_info(List.object_bloodShield, range: "0", pa_cost: 0, cd: 4, Spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        //AddSpell_info(List.object_fieldGlass, range: "0", pa_cost: 0, cd: 4, Combat_spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        //AddSpell_info(List.object_sacrifice, range: "0", pa_cost: 0, cd: 5, Combat_spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.object_watch, range: "0", pa_cost: 0, cd: 6, Spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.object_liberation, range: "0", pa_cost: 0, cd: 5, Spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.object_rush, range: "0", pa_cost: 0, cd: 4, Spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.object_floor, range: "0", pa_cost: 0, cd: 12, Spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.object_totem, range: "0", pa_cost: 0, cd: 15, Spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        //AddSpell_info(List.object_dice, range: "0", pa_cost: 0, cd: 5, Combat_spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.object_spike, range: "0", pa_cost: 0, cd: 6, Spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.object_blood, range: "0", pa_cost: 0, cd: 5, Spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);

        if (V.IsFr())
        {
            ModifySpell_string(List.object_healtPotion, "Potion de vie", "Soigne *hea25%*end pv de vos pv max");
            ModifySpell_string(List.object_empty, "Emplacement d'objet vide", "Obtenez des objets a utiliser en combat");
            //ModifySpell_string(List.object_energeticDrink, "Boissons énergisante", "Attire les ennemis en ligne autour de vous de 3 cases\n*bon+20% de dégâts(2 tours)*end pour chaque ennemie a votre contact");
            ModifySpell_string(List.object_bloodShield, "Bouclier de sang", "*arm+35 armure*end");
            //ModifySpell_string(List.object_fieldGlass, "Longue vue", "*bon+2 po(2 tour)*end");
            //ModifySpell_string(List.object_sacrifice, "Sacrifice", "Perdez 30% de vos pv actuelle et gagnez *bon+30% de dégâts(2 tours)*end");
            ModifySpell_string(List.object_watch, "Montre", "Le delai de recuperation du dernier sort, arme ou objet utilisé est annulé");
            ModifySpell_string(List.object_liberation, "Liberation", "Repousse de *spe2 cases *end les ennemis a votre contact");
            ModifySpell_string(List.object_rush, "Rush", "*bon+2 pa*end et *bon+1 pm*end");
            ModifySpell_string(List.object_floor, "Farine", "*bon +4 pa*end");
            ModifySpell_string(List.object_totem, "Totem", "Vous ne pouvez pas mourrir pendant ce tour");
            //ModifySpell_string(List.object_dice, "Dés", "*bon+33% chance de coup critique*end pour ce tour");
            ModifySpell_string(List.object_spike, "Epines", "+1 épine");
            ModifySpell_string(List.object_blood, "Sang", "*bon+50%*end vol de vie ce tour");
        }
        else
        {
            ModifySpell_string(List.object_healtPotion, "Life potion", "Heal *hea25%*end of your maximum pv");
            ModifySpell_string(List.object_empty, "Empty object slot", "Obtain object to use in combat");
            //ModifySpell_string(List.object_energeticDrink, "Energy drink", "Attract in line ennemy around you that are 3 square away\n*war+20% damage(2 turns)*end for each ennemy at your contact");
            ModifySpell_string(List.object_bloodShield, "Blood shield", "*arm+35 armor*end");
            //ModifySpell_string(List.object_fieldGlass, "Field-glass", "*bon+2 po(2 turns)*end");
            //ModifySpell_string(List.object_sacrifice, "Sacrifice", "Loose 30% of your current healt and win *bon30% damage(2 turns)*end");
            ModifySpell_string(List.object_watch, "Watch", "The cooldown of your last spell, weapon or object used is canceled");
            ModifySpell_string(List.object_liberation, "Liberation", "Push ennemies at your contact *spe2 square away *end");
            ModifySpell_string(List.object_rush, "Rush", "*bon+2 ap*end and *bon+1 mp*end");
            ModifySpell_string(List.object_floor, "Floor", "*bon +4 ap*end");
            ModifySpell_string(List.object_totem, "Totem", "You can't die this turn");
            //ModifySpell_string(List.object_dice, "Dice", "*bon+33% critical hit chance*end for this turn");
            ModifySpell_string(List.object_spike, "Spike", "+1 spike");
            ModifySpell_string(List.object_blood, "Blood", "*bon+50%*end life steal this turn");

        }
    }
}
