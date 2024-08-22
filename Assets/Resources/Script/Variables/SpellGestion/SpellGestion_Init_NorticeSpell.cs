using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SpellGestion : MonoBehaviour
{

    public static void Initialize_player_origin_Nortice()
    {
        Color32 col = spellInteriorCol_notrice;

        AddSpell_info(List.norticeSurface_wand, range: "1_3", pa_cost: 3, cd: -2, Spell.Range_type.normal, CursorMode.physical, range_effect_size.singleTarget, TargetMode.all, col, true);
        AddSpell_info(List.norticeSurface_energeticWand, range: "1_4", pa_cost: 3, cd: -2, Spell.Range_type.normal, CursorMode.physical, range_effect_size.singleTarget, TargetMode.all, col, true);
        AddSpell_info(List.norticeSurface_rapidity, range: "0", pa_cost: 2, cd: 5, Spell.Range_type.normal, CursorMode.physical, range_effect_size.singleTarget, TargetMode.all, col, true);
        AddSpell_info(List.norticeSurface_amplifiedHeal, range: "0", pa_cost: 4, cd: 6, Spell.Range_type.normal, CursorMode.physical, range_effect_size.singleTarget, TargetMode.all, col, true);
        AddSpell_info(List.norticeSurface_amplifiedArmor, range: "0", pa_cost: 4, cd: 6, Spell.Range_type.normal, CursorMode.physical, range_effect_size.singleTarget, TargetMode.all, col, true);
        AddSpell_info(List.norticeSurface_whirlwind, range: "1_4", pa_cost: 5, cd: 4, Spell.Range_type.noNeedOfLineOfView, CursorMode.physical, range_effect_size.twoSquareAround, TargetMode.all, col, true);
        AddSpell_info(List.norticeSurface_crystalGift, range: "0", pa_cost: 0, cd: 2, Spell.Range_type.normal, CursorMode.physical, range_effect_size.singleTarget, TargetMode.all, col, true);
        AddSpell_info(List.norticeSurface_energy, range: "0", pa_cost: 0, cd: 5, Spell.Range_type.normal, CursorMode.physical, range_effect_size.singleTarget, TargetMode.all, col, true);
        AddSpell_info(List.norticeSurface_BigWand, range: "1_4", pa_cost: 4, cd: -2, Spell.Range_type.normal, CursorMode.physical, range_effect_size.oneSquareAround, TargetMode.all, col, true);
        AddSpell_info(List.norticeSurface_poisonedVial, range: "1_6", pa_cost: 2, cd: 1, Spell.Range_type.normal, CursorMode.physical, range_effect_size.oneSquareAround, TargetMode.all, col, true);
        AddSpell_info(List.norticeSurface_book, range: "0", pa_cost: 4, cd: 1, Spell.Range_type.normal, CursorMode.physical, range_effect_size.threeSquareAround_line, TargetMode.all, col, true);
        AddSpell_info(List.norticeSurface_Optimisation, range: "0", pa_cost: 99, cd: 99, Spell.Range_type.normal, CursorMode.physical, range_effect_size.singleTarget, TargetMode.all, col, true);
        AddSpell_info(List.norticeSurface_meteorite, range: "0", pa_cost: 6, cd: 7, Spell.Range_type.normal, CursorMode.physical, range_effect_size.singleTarget, TargetMode.all, col, true);

        if (V.IsFr())
        {
            ModifySpell_string(List.norticeSurface_wand, "Baguette", "*dmg10 à 12 dégâts*end\n*arm+5 armure*end");
            ModifySpell_string(List.norticeSurface_energeticWand, "Baguette energetique", "*dmg12 à 14 dégâts*end\n*bon+1 pa*end");
            ModifySpell_string(List.norticeSurface_rapidity, "Rapidité", "*bon+2 pm*end\nVous ne pouvez pas être taclé ce tour");
            ModifySpell_string(List.norticeSurface_amplifiedArmor, "Armure amplifié", "*arm+10 armure*end\nLes effets sur ce sort font effet 2 fois");
            ModifySpell_string(List.norticeSurface_amplifiedHeal, "Soins amplifié", "*bon+15 pv*end\nLes effets sur ce sort font effet 2 fois");
            ModifySpell_string(List.norticeSurface_whirlwind, "Tourbillon", "*dmg10 + 5*end par pm actuelle dégâts\nPour chaque pm gagnez *bon1 pa*end et *spe+3% d'effet*end pour le reste du combat");
            ModifySpell_string(List.norticeSurface_crystalGift, "Don de cristaux", "Perdez *spe20%*end de vos pv actuelle pour gagnez *bon20%*end d'effet pendant ce tour");
            ModifySpell_string(List.norticeSurface_energy, "Energie", "*bon+2 pa*end*bon(2 tours)*end");
            ModifySpell_string(List.norticeSurface_BigWand, "Grosse baguette", "*dmg15 dégâts*end\nPour chaque *spe2 pa*end dépensé pendant ce tour gagné *bon1 pa*end et augmenté les dégâts de ce sort de *spe10%*end");
            ModifySpell_string(List.norticeSurface_poisonedVial, "Fiole empoissonée", "*dmg6 dégâts*end\nRetire *bon2 pa*end");
            ModifySpell_string(List.norticeSurface_book, "Livre", "Inflige *dmg15 dégâts*end aux ennemis en ligne autour de vous et les repousses de *bon2 cases*end\n*arm+10 armure*end pour chaque ennemie repoussé");
            ModifySpell_string(List.norticeSurface_Optimisation, "Optimisation", "Le sort à droite et à guauche ont *spe+25% d'effet*end");
            ModifySpell_string(List.norticeSurface_meteorite, "Meteorite", "Cible tous les ennemies\nInflige *dmg20 dégâts*end et retire *bon3 pa*end\n*arm+15 armure*end par ennemie touchés");

        }
        else
        {

        }
    }
}