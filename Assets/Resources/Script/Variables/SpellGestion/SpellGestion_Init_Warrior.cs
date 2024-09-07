using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SpellGestion : MonoBehaviour
{
    /// <summary>
    /// Initialize spell that the warrior class have
    /// </summary>
    public static void Initialize_player_origin_warrior()
    {
        Color32 col = spellInteriorCol_underground;

        AddSpell_info(List.warrior_rockThrow, range: "0", pa_cost: 4, cd: 5, Spell.Range_type.normal, CursorMode.physical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.warrior_Punch, range: "1", pa_cost: 3, cd: -2, Spell.Range_type.normal, CursorMode.physical, range_effect_size.Cone, TargetMode.all, col);
        AddSpell_info(List.warrior_divineSword, range: "0", pa_cost: 4, cd: -2, Spell.Range_type.normal, CursorMode.magical, range_effect_size.oneSquareAround, TargetMode.entity, col);
        AddSpell_info(List.warrior_Double, range: "0", pa_cost: 0, cd: 6, Spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.entity, col);
        AddSpell_info(List.warrior_spikeAttack, range: "1_3", pa_cost: 4, cd: -2, Spell.Range_type.line, CursorMode.magical, range_effect_size.singleTarget, TargetMode.entity, col);
        AddSpell_info(List.warrior_jump, range: "1_3", pa_cost: 3, cd: 3, Spell.Range_type.noNeedOfLineOfView, CursorMode.magical, range_effect_size.oneSquareAround, TargetMode.empty, col);
        AddSpell_info(List.warrior_endurance, range: "0", pa_cost: 0, cd: 5, Spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.warrior_strength, range: "0", pa_cost: 4, cd: 6, Spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.warrior_earthTotem, range: "3_3", pa_cost: 3, cd: 6, Spell.Range_type.noNeedOfLineOfView, CursorMode.magical, range_effect_size.twoSquareAround, TargetMode.empty, col);
        AddSpell_info(List.warrior_os, range: "0", pa_cost: 2, cd: 4, Spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.warrior_heal, range: "0", pa_cost: 3, cd: 5, Spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.warrior_attack, range: "1", pa_cost: 3, cd: 1, Spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.warrior_execution, range: "0", pa_cost: 3, cd: 4, Spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.warrior_spent, range: "0", pa_cost: 4, cd: 4, Spell.Range_type.normal, CursorMode.magical, range_effect_size.fiveSquareAround, TargetMode.all, col);
        AddSpell_info(List.warrior_spikeConversion, range: "0", pa_cost: 0, cd: 4, Spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);

        if (V.IsFr())
        {
            ModifySpell_string(List.warrior_rockThrow, "Défense d'épines", "*arm+20 armure*end\n*dex+1 épine*end *spe(3 tours)*end\nGagnez une épine *sperénitialise*end la durée de vie toutes les épines");
            ModifySpell_string(List.warrior_Punch, "Embrasement", "Frappez l'ennemie et infligez *dmg10 dégâts*end\nLa premiere fois que vous lancez ce sort ce tour vous gagnez *res4 armure*end et *eff1 accumuluation*end par ennemie touchés");
            ModifySpell_string(List.warrior_divineSword, "Epee divine", "Invoquez une épee qui inflige *dmg5 dégâts*end puis *dmg10 dégâts*end autour de vous\n\nPour chaque ennemie touchés gagnez *eff1 accumulation*end");
            ModifySpell_string(List.warrior_spikeAttack, "Attaque épineuse", "Frappez avec vos épines a hauteur de *spe100%*end de dégâts + *dex10%*end par point d'*speaccumulation*end");
            ModifySpell_string(List.warrior_jump, "Saut", "Sautez sur la case cible et infligez *dmg13 dégâts*end aux ennemie autour\nPour chaque ennemie touchés gagné *res10 armure*end");
            ModifySpell_string(List.warrior_endurance, "Endurance", "*dex+3 pa*end");
            ModifySpell_string(List.warrior_strength, "Force", "Votre prochain sort se lance une *spedeuxieme fois*end");
            ModifySpell_string(List.warrior_earthTotem, "Earth totem", "Invoquez un *spetotem*end sur la case ciblée qui infligera *dmg10 dégâts*end aux ennemies à moins de *spe2 case*end autour de lui.\nSi ce totem tue un ennemie vous gagnez *eff2 accumulation*end.\n\nLes *spetotems*end declenchent leur *speeffets*end a *spela fin de votre tour*end, *spea la mort du totem*end ou *spelors de son apparition*end");
            ModifySpell_string(List.warrior_os, "Os", "Subissez *spe25%*end de vos pv max\n*dex+2 épine*end");
            ModifySpell_string(List.warrior_heal, "Soin", "Soignez vous de *res30*end et vos totem de *res15*end\nLes *spesoins*end sont augmentés de *dex10%*end par *speaccumulation*end");
            ModifySpell_string(List.warrior_attack, "Attaque", "13 dégâts\n-33% dégâts subis ce tour");
            ModifySpell_string(List.warrior_execution, "Execution", "Détruisez votre *spearmure*end, l'*spearmure*end perdu est ensuite répartis en dégâts parmi tous les ennemis en vie à hauteur de *spe100%*end + *dex10%*end par point d'*speccumulation*end");
            ModifySpell_string(List.warrior_spent, "Dépense", "Lancez une *speméteorite*end aux ennemies proche de vous aléatoirement qui inflige *dmg10 dégâts*end à l'ennemie et a tous les autres a moins de *spe1 case*end\nChaque méteorite vous soigne de *res5 pv*end.\n\nLancez une nouvelle *speméteorite*end pour chaque point d'*speaccumulation*end");
            ModifySpell_string(List.warrior_spikeConversion, "Conversion d'épine", "Perdez vos *speépines*end et gagnez le montant perdu en *speaccumulation*end");
        }
        else
        {
            ModifySpell_string(List.warrior_Punch, "Punch", "*dmg10 damage*end\n+5 armor");
            ModifySpell_string(List.warrior_jump, "Jump", "Teleport\n+15 armor");
            ModifySpell_string(List.warrior_endurance, "Endurance", "+3 ap(1 turn)");
            ModifySpell_string(List.warrior_strength, "Strength", "Your next spell have *bon+50%*end effects");

        }

    }
}

/*
 *         AddSpell_info(List.warrior_spep, range: "1_4", pa_cost: 3, cd: 3, Combat_spell.Range_type.line, CursorMode.physical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.warrior_flash, range: "0", pa_cost: 0, cd: -2, Combat_spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        //AddSpell_info(List.warrior_exercice, range: "0", pa_cost: 0, cd: 6, Combat_spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, cilbedClass);
        AddSpell_info(List.warrior_cudgel, range: "1", pa_cost: 3, cd: -2, Combat_spell.Range_type.normal, CursorMode.physical, range_effect_size.Cone_Inverted, TargetMode.all, col);
        //AddSpell_info(List.warrior_Growth, range: "0", pa_cost: 4, cd: 6, Combat_spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, cilbedClass);
        //AddSpell_info(List.warrior_Trap, range: "0_1", pa_cost: 4, cd: 7, Combat_spell.Range_type.noNeedOfLineOfView, CursorMode.magical, range_effect_size.twoSquareAround, TargetMode.all, cilbedClass);
        //AddSpell_info(List.warrior_Trap_effect, range: "0", pa_cost: 0, cd: 0, Combat_spell.Range_type.normal, CursorMode.physical, range_effect_size.singleTarget, TargetMode.all, cilbedClass);
        //AddSpell_info(List.warrior_slowingSpike, range: "1_4", pa_cost: 3, cd: 4, Combat_spell.Range_type.line, CursorMode.physical, range_effect_size.Cone, TargetMode.all, cilbedClass);
        //AddSpell_info(List.warrior_bleeding, range: "0_3", pa_cost: 3, cd: 1, Combat_spell.Range_type.noNeedOfLineOfView, CursorMode.physical, range_effect_size.oneSquareAround, TargetMode.all);
        //AddSpell_info(List.warrior_focus, range: "0_3", pa_cost: 3, cd: 3, Combat_spell.Range_type.line, CursorMode.magical, range_effect_size.oneSquareAround, TargetMode.all);
        AddSpell_info(List.warrior_offensivePosture, range: "0", pa_cost: 0, cd: 6, Combat_spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.warrior_defensivePosture, range: "0", pa_cost: 3, cd: 4, Combat_spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.warrior_spike, range: "0", pa_cost: 4, cd: 3, Combat_spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.warrior_defense, range: "0", pa_cost: 3, cd: 3, Combat_spell.Range_type.normal, CursorMode.magical, range_effect_size.singleTarget, TargetMode.all, col);
        AddSpell_info(List.warrior_aspiration, range: "0", pa_cost: 3, cd: 2, Combat_spell.Range_type.normal, CursorMode.magical, range_effect_size.twoSquareAround, TargetMode.all, col);
        AddSpell_info(List.warrior_spikyPosture, range: "0", pa_cost: 3, cd: 2, Combat_spell.Range_type.normal, CursorMode.magical, range_effect_size.twoSquareAround, TargetMode.all, col);
        AddSpell_info(List.warrior_attraction, range: "0", pa_cost: 2, cd: 4, Combat_spell.Range_type.normal, CursorMode.magical, range_effect_size.threeSquareAround_line, TargetMode.all, col);

        if (V.IsFr())
        {
            ModifySpell_string(List.warrior_flash, "Eclair", "Alterne\n<i>*inf *inl Alterne *end transforme votre *inl Puissance en Defense *end et vice versa*end</i>");
            //ModifySpell_string(List.warrior_exercice, "Exercice", "*pda+2 pa(2 tours)*end");
            ModifySpell_string(List.warrior_cudgel, "Gourdin", "*dmg14 à 16 dégâts*end\nSoigne *pdv5 pv*end par ennemie touchés");
            //ModifySpell_string(List.warrior_Growth, "Croissance", "*war+10 puissance*end et *manCroissance (2 tours)*end\n*warCroissance :*end Au début du tour *war+10 puissance*end");
            //ModifySpell_string(List.warrior_Trap, "Piège", "Pose un *manPiège (3 tours)*end\n*manPiège:*end Les ennemies dans le *manPiège*end subissent *dmg3 dégâts*end et vous gagnez *war5 puissance*end");
            //ModifySpell_string(List.warrior_slowingSpike, "Piques ralentissantes", "Inflige *dmg10 dégâts*end et retire *pdm1 pm*end *inf(2 tours)*end");
            ModifySpell_string(List.warrior_spep, "Fouet", "*dmg10 à 14 dégâts *end\n*infPuissance majoritaire:*end Attire de *man2 cases*end \n*infDefense majoritaire:*end Repousse de *man2 cases*end");
            //ModifySpell_string(List.warrior_bleeding, "Saignement", "*dmg5 à 6 dégâts*end et applique *bon5 saignement*end");
            //ModifySpell_string(List.warrior_focus, "Focus", "Applique l'étât Focus et *war+10 defense*end\n*inf*inlFocus*end augmente de *bon100%*end la prochaine application de *inlsaignement*end sur la cible*end");
            ModifySpell_string(List.warrior_offensivePosture, "Posture offensive", "Votre défense devient de la puissance\n*pda+1 pa*end\nVos sorts ont une zone d'effet augmenté de *man1 cases*end et ont *bon+30% d'effets*end*bon(1 tours)*end");
            ModifySpell_string(List.warrior_defensivePosture, "Posture défensive", "*speVotre puissance devient de la défense*end\n*arm+25 armure*end, gagnez ce montant en *bondéfense*end");
            ModifySpell_string(List.warrior_spike, "Épines", "*arm+15 armure*end et *bon+2 épines*end\n*infChaque épine possedez inflige 3 dégâts a l'attaquant\nLa défense augmente les dégats de vos épines*end");
            ModifySpell_string(List.warrior_defense, "Défense", "*war+10 défense*end\nsi votre vie est pleine *war+10 défense*end sinon *pdv+20 pv*end");
            ModifySpell_string(List.warrior_aspiration, "Aspiration", "Les ennemis a moins de *bon2 case*end de distance ont *bon-20% de dégâts*end et vous donne *bon+10 pv*end chacun");
            ModifySpell_string(List.warrior_spikyPosture, "Posture piquante", "*war+15 épines*end pour le reste du combat\nLorsque vous infligez des dégâts d'épines infligez *bon50%*end des dégâts infligés aux ennemis a moins de 2 case autour de vous");
            ModifySpell_string(List.warrior_attraction, "Attraction", "*dmg5 dégâts*end, attire les ennemies autour de vous de 3 cases\n*pda+1 pa*end et *arm+5 armure *end par ennemi attiré");

        }
        else
        {
            ModifySpell_string(List.warrior_flash, "Flash", "Alternate\n<i>*inf *inl Alternating *end transform your *inl power into defense *end and vice versa*end</i>");
            ModifySpell_string(List.warrior_cudgel, "Cudgel", "*dmg14 to 16 damage*end\nHeal *pdv5 pv*end par ennemy hit");
            ModifySpell_string(List.warrior_spep, "spep", "*dmg10 to 14 dégâts *end\n*infPower majoritary:*end Attract de *man2 square*end \n*infDefense majoritary:*end Push *man2 square*end");

            //ModifySpell_string(List.warrior_bleeding, "Bleeding", "*dmg5 to 6 damage*end and apply *bon5 bleeding*end");
            //ModifySpell_string(List.warrior_focus, "Focus", "Apply Focus and *war+10 defense*end\n*inf*inlFocus*end increase by *bon100%*end the next application of *inlbleeding*end on the target*end");
            ModifySpell_string(List.warrior_offensivePosture, "Offensive posture", "Your defense become power\n*pda+1 ap*end\nYour spell have an range effect increased by *man1 square*end and have *bon+30% effects*end*bon(1 tours)*end");
            ModifySpell_string(List.warrior_defensivePosture, "Defensive posture", "*speYour power become defense*end\n*arm+25 armor*end, gain same amount as *bondefense*end");
            ModifySpell_string(List.warrior_spike, "Spikes", "*arm+15 armor*end and *bon+2 spike*end\n*infEach owned spike deal 3 damage to your attacker\nDefense increase the damage of your spike*end");
            ModifySpell_string(List.warrior_defense, "Defense", "*war+10 defense*end\nif your life is full *war+10 defense*end else *pdv+20 pv*end");
            ModifySpell_string(List.warrior_aspiration, "Aspiration", "Ennemy at least *bon2 squares*end around you have *bon-20% damage*end and give *bon+10 pv*end each");
            ModifySpell_string(List.warrior_spikyPosture, "Spiky posture", "*war+15 spikes*end for the rest of the fight\nWhen you deal spike damage deal *bon50%*end of damage dealt to nearby ennemy 2 square away");
            ModifySpell_string(List.warrior_attraction, "Attraction", "*dmg5 damage*end, attract ennemy in line with you that are 3 squares away\n*pda+1 ap*end and *arm+5 armor *end for each attracted ennemy");
            ModifySpell_string(List.warrior_shield, "Shield", "*arm+5 armore*end");

            
                        ModifySpell_string(List.warrior_Punch, "Punch", "Deal *dmg8 damage*end. If *warPower is majoritary*end deal *dmg13 damage instead*end\nIf Defense is majoritary");
                        ModifySpell_string(List.warrior_Alternate, "Alternate", "Deal *dmg 4 spike damage around you *end *inl ( 1 per turn). *end *alt Alternate *end \n<i>*inf *inl Alternate *end transform your *inl Power into Defense *end and vice versa*end</i>\n<i>*inf *inl Defense reduce damage you take *end and increase your *inl spike damage *end *end</i>");
                        ModifySpell_string(List.warrior_divineSword, "Divine sword", "Deal *dmg15 damage around you*end, if you hit a target gain *war15 power*end\n\n<i>*inf La *inl Puissance *end augmente vos *inl dégâts infligés *end *end</i>");
                        ModifySpell_string(List.warrior_poisonDagger, "Poisoned dagger", "Deal *dmg10 damage*end and apply *man Weakness (2 turn)*end\n<i>*inf *inl Weakness *end increase the receive damage of the target by *inl 33% *end*end</i>");
                        ModifySpell_string(List.warrior_rush, "Rush", "Deal *dmg13 damage*end and *man move to the contact of the target *end, *war+10 power*end");
                        ModifySpell_string(List.warrior_spiral, "Spiral", "Deal *dmg 10 damage*end. If the target have *man Weakness *end *dmg double the damage*end and *man remove 2 ap *end to the target");
                        ModifySpell_string(List.warrior_modularHeart, "Modular heart", "If your *war power is majoritary *end gain *man Electricity (2 turn)*end and *dmg+3 damage*end else gain *man Spikes (2 turns)*end and heal *pdv10% of your max health *end \n\n<i>*inf *inl Electricity *end give you *inl 1 ap and 5 power *end the *inl first time you hit an ennemy*end during your turn \n *inl Spikes *end give you *inl 5 spike damage and +5 defense *end when you *inl receive damage *end *end</i> ");
                        ModifySpell_string(List.warrior_retreat, "Retreat", "Deal *dmg 6 damage*end, apply *man Weakness (2 turns) *end and *man move 2 square away *end");
                        ModifySpell_string(List.warrior_suction, "Suction", "Deal *dmg 12 spike damage *end around you, target are attracted by *man 3 square *end. For each ennemy targeted you gain *war 15 defense *end and *bon heal 15% of your max healt*end then you make a *war Focus defense*end \n\n<i>*inf *inl Focus defense *end convert your *inl power into defense*end *end</i>");
                        ModifySpell_string(List.warrior_flash, "Flash", "*warAlterne*end\n<i>*inf *inl Alternate *end transform your *inl Power into Defense *end and vice versa*end</i>");

                        ModifySpell_string(List.warrior_talent_WeakPoint, "Weak point", "The efficacity of de *man Weakness become 50%*end. *dmg+5 damage*end against target affected by *man Weakness*end");
                        ModifySpell_string(List.warrior_talent_evolution, "Evolution", "When your *war power *end reach a new treshold gain effect for the rest of the fight : \n *war 50 power *end : *bon +5 damage *end \n *war 100 power *end : *bon +1 ap and +5 damage *end \n *war 150 power *end : *bon +1 mp and +5 damage *end \n *war 200 power *end : *dmg +100% more damage dealt *end");
                
        }
 */