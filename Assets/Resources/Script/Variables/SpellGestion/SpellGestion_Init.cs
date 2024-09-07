using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SpellGestion : MonoBehaviour
{
    public enum List
    {
        //Common spell (both for player and monster)
        empty, none,
        //Monster
        monster_normal_attack, monster_funky_attack, monster_funky_recoil, monster_shark_attack, monster_teleport_shark, monster_shark_heal,
        monster_archer_attack, monster_archer_sprint, monster_vala_attack, monster_vala_fireThrowing, monster_inflamed_attack, monster_vala_invokeInflamed,
        monster_grassy_heal, monster_grassy_boost, monster_grassy_attack, monster_magnetic_attack, monster_magnetic_attract,
        //Special
        spike,
        //Base
        base_fist,
        //Weapon
        weapon_mace, weapon_taser, weapon_bow, weapon_littleSword, weapon_magicWand, weapon_steal, weapon_shieldAttack, weapon_knife,
        weapon_statue, weapon_dagger, weapon_crowBar, weapon_ak47, weapon_sniper,
        //Object
        object_empty, object_healtPotion, object_energeticDrink, object_bloodShield, object_fieldGlass, object_sacrifice, object_watch, object_liberation, object_rush, object_floor, object_totem,
        object_spike, object_blood,
        //Warrior
        warrior_rockThrow, warrior_Double,warrior_armor, warrior_spikeAttack,warrior_earthTotem,warrior_os,warrior_heal, warrior_attack,warrior_execution,
        warrior_divineSword, warrior_bleeding, warrior_Punch, warrior_flash, warrior_whip, warrior_focus, warrior_offensivePosture, warrior_cudgel, warrior_exercice,
        warrior_Growth, warrior_Trap, warrior_Trap_effect, warrior_slowingSpike, warrior_strength, warrior_defensivePosture, warrior_spike, warrior_defense, warrior_jump, warrior_aspiration, warrior_spikyPosture, warrior_attraction,

        //Object
        object_dice, weapon_bowlingBall, weapon_spear,

        //Surface
        surface_arrow, surface_fieryShot, surface_fallback, surface_longBow, surface_shuriken, surface_sharpenedArrow, surface_bow,
        surface_bandage, surface_turret, surface_laceration, surface_jump, surface_woodenShield, surface_vision, surface_energy,

        //NorticeSurface
        norticeSurface_wand, norticeSurface_energeticWand, norticeSurface_rapidity, norticeSurface_amplifiedHeal, norticeSurface_amplifiedArmor, norticeSurface_whirlwind, norticeSurface_crystalGift, norticeSurface_energy,
        norticeSurface_BigWand, norticeSurface_book, norticeSurface_poisonedVial, norticeSurface_Optimisation, norticeSurface_meteorite, warrior_shield, warrior_endurance, warrior_spent,warrior_spikeConversion
    };

    public static void AddStartingSpellToTheToolbar()
    {
        VerifyAndAddSpell(SpellGestion.List.base_fist);
    }

    public static void VerifyAndAddSpell(SpellGestion.List l)
    {
        if (!SpellInToolbar.activeSpell.Contains(l))
        {
            if (l != SpellGestion.List.base_fist)
                TreeSkill.PurchasedSkill.Add(l);

            Main_UI.Toolbar_AddSpell(l);
        }
    }

}
