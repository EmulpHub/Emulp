using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SpellGestion : MonoBehaviour
{
    /// <summary>
    /// Initialize all spell that only monster can use
    /// </summary>
    public static void Initialize_monster()
    {
        //Normal
        AddSpell_info(List.monster_normal_attack, "1_1", 3, -4, Spell.Range_type.normal);

        //funky
        AddSpell_info(List.monster_funky_attack, "1_3", 3, -4, Spell.Range_type.normal);
        AddSpell_info(List.monster_funky_recoil, "1_1", 4, 2, Spell.Range_type.normal);

        //Shark
        AddSpell_info(List.monster_shark_attack, "1_1", 3, -4, Spell.Range_type.normal);
        AddSpell_info(List.monster_teleport_shark, "1_5", 4, 4, Spell.Range_type.noNeedOfDistance);
        AddSpell_info(List.monster_shark_heal, "0", 4, 6, Spell.Range_type.noNeedOfDistance);

        //Archer
        AddSpell_info(List.monster_archer_attack, "3_4", 3, -4, Spell.Range_type.normal);
        AddSpell_info(List.monster_archer_sprint, "0", 3, 4, Spell.Range_type.noNeedOfDistance);

        //Vala
        AddSpell_info(List.monster_vala_attack, "1_1", 3, -5, Spell.Range_type.normal);
        AddSpell_info(List.monster_vala_fireThrowing, "1_4", 4, 1, Spell.Range_type.line);
        AddSpell_info(List.monster_vala_invokeInflamed, "1_1", 3, 5, Spell.Range_type.normal);

        //Inflamed
        AddSpell_info(List.monster_inflamed_attack, "1_1", 3, -3, Spell.Range_type.normal);

        //grassy
        AddSpell_info(List.monster_grassy_heal, "1_2", 3, -2, Spell.Range_type.normal);
        AddSpell_info(List.monster_grassy_boost, "1_1", 4, 4, Spell.Range_type.normal);
        AddSpell_info(List.monster_grassy_attack, "1_1", 3, -4, Spell.Range_type.normal);

        //magnetic
        AddSpell_info(List.monster_magnetic_attack, "1_1", 3, -4, Spell.Range_type.normal);
        AddSpell_info(List.monster_magnetic_attract, "1_3", 4, 2, Spell.Range_type.normal);

        if (V.IsFr())
        {
            //Normal
            ModifySpell_string(List.monster_normal_attack, "Petite attaque", "Inflige *dmg 6 à 8 dégâts*end");

            //funky
            ModifySpell_string(List.monster_funky_attack, "Lancer de baton", "Inflige *dmg 7 à 9 dégâts*end");
            ModifySpell_string(List.monster_funky_recoil, "Repli", "Inflige *dmg 4 à 6 dégâts*end et *spes'eloigne de 1 case*end");

            //shark
            ModifySpell_string(List.monster_shark_attack, "Morsure", "Inflige *dmg 8 à 10 dégâts*end");
            ModifySpell_string(List.monster_teleport_shark, "Saut", "*speTéléporte*end à la case ciblée");
            ModifySpell_string(List.monster_shark_heal, "Soin des mers", "*bonSoigne 35%*end de ses points de vie maximum");

            //Archer
            ModifySpell_string(List.monster_archer_attack, "Tir", "Inflige *dmg 7 à 9 dégâts*end");
            ModifySpell_string(List.monster_archer_sprint, "Course", "*pdm+2 pm*end");

            //Vala
            ModifySpell_string(List.monster_vala_attack, "Embrasement", "Inflie *dmg 8 à 10 dégâts*end");
            ModifySpell_string(List.monster_vala_fireThrowing, "Boule de feu", "Lance une boule de feu infligeant *dmg13 dégâts*end");
            ModifySpell_string(List.monster_vala_invokeInflamed, "Esprits enflammés", "Invoque un *dmgEsprits enflammés*end");

            //Inflamed
            ModifySpell_string(List.monster_inflamed_attack, "Attaque de flamme", "Inflige *dmg 4 dégâts*end");

            //Grassy
            ModifySpell_string(List.monster_grassy_heal, "Soin", "*bonSoigne de 4 à 6*end");
            ModifySpell_string(List.monster_grassy_boost, "Boost", "*bon+4 pa*end et *pdm+2 pm*end");
            ModifySpell_string(List.monster_grassy_attack, "Branche", "Inflige *dmg4 à 6 dégâts*end");

            //magnetic
            ModifySpell_string(List.monster_magnetic_attack, "Clous", "Inflige *dmg 7 à 9 dégâts*end");
            ModifySpell_string(List.monster_magnetic_attract, "Magnetisme", "Inflige *dmg4 à 6 dégâts*end et attire de *spe2 cases*end");
        }
        else
        {
            ModifySpell_string(List.monster_normal_attack, "Little attack", "Deal *dmg 6 to 8 damage*end");

            //funky
            ModifySpell_string(List.monster_funky_attack, "Stick throw", "Deal *dmg 7 to 9 damage*end");
            ModifySpell_string(List.monster_funky_recoil, "Retreat", "Deal *dmg 4 to 6 damage*end and *speretreat 1 square*end");

            //shark
            ModifySpell_string(List.monster_shark_attack, "Bite", "Deal *dmg 8 to 10 damage*end");
            ModifySpell_string(List.monster_teleport_shark, "Jump", "*speTeleports*end to target cell");
            ModifySpell_string(List.monster_shark_heal, "Care of the seas", "*hpHeals 35%*end of his maximum health points");


            //Archer
            ModifySpell_string(List.monster_archer_attack, "Shot", "Deal *dmg 7 to 9 damage*end");
            ModifySpell_string(List.monster_archer_sprint, "Run", "*pdm+2 mp*end");

            //Vala
            ModifySpell_string(List.monster_vala_attack, "Ignite", "Deal *dmg 8 to 10 damage*end");
            ModifySpell_string(List.monster_vala_fireThrowing, "Fire ball", "Launch a fire ball dealing *dmg13 dégâts*end");
            ModifySpell_string(List.monster_vala_invokeInflamed, "Flaming spirits", "Summon a *dmgFlaming spirits*end");

            //Inflamed
            ModifySpell_string(List.monster_inflamed_attack, "Flame attack", "Deal *dmg 4 damage*end");

            //Grassy
            ModifySpell_string(List.monster_grassy_heal, "Heal", "*bonHeal 4 to 6*end");
            ModifySpell_string(List.monster_grassy_boost, "Boost", "*bon+4 ap*end et *pdm+2 mp*end");
            ModifySpell_string(List.monster_grassy_attack, "Branch", "Deal *dmg4 to 6 damage*end");

            //magnetic
            ModifySpell_string(List.monster_magnetic_attack, "Nails", "Deal *dmg 7 to 9 damage*end");
            ModifySpell_string(List.monster_magnetic_attract, "Magnetism", "Deal *dmg4 to 6 damage*end and attract *spetwo square*end");
        }
    }
}
