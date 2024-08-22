using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Audio;
using System.Diagnostics;

public partial class SoundManager : MonoBehaviour
{
    public enum list
    {
        //PLAYER SKILL
        none,

        //BASIC
        spell_basic_punch,

        //WARRIOR
        spell_warrior_powerfulPunch, spell_warrior_heal, spell_warrior_defense, spell_warrior_alternate, spell_warrior_whip_push, spell_warrior_whip_attract,
        spell_warrior_pact, spell_warrior_planification, spell_warrior_suffering, spell_warrior_murderer, spell_warrior_continuite, spell_warrior_rampart,
        spell_warrior_flash, spell_warrior_massProtection, spell_warrior_lastBreath, spell_warrior_divineSword_gaininigPower,

        //COMMON
        common_tackle, common_walkSound, common_invocation,

        //PLAYER RELATED
        player_winning, player_losing, player_levelUp, player_levelUp_2, player_levelUp_2_silent,

        //UI
        ui_buttonPressed, ui_selectionSpell, ui_choiceLanguage, ui_window_open, ui_window_close, ui_newSpell, ui_newSpell_2, ui_error,

        //MONSTER
        monster_walkSoundM, monster_dead, monster_dead_endCombat, monster_grass_attack, monster_grass_boost, monster_grass_heal,

        //SPELL monster
        spell_monster_normal_punch, //normal
        spell_monster_funky_wood, spell_monster_funky_escape, //funky
        spell_monster_shark_bite, spell_monster_shark_tp, //shark
        spell_monster_archer_arrow_launch, spell_monster_archer_arrow_impact, spell_monster_archer_pm, //archer
        spell_monster_vala_attack, spell_monster_vala_fireThrowing_travelling, spell_monster_vala_fireThrowing_impact, //vala
        spell_monster_inflamed_attack,

        //Object
        object_healthPotion, object_energeticDrink, object_Liberation, object_Rush_First, object_Rush_Second, object_BloodShield_First, object_BloodShield_Second,
        object_Sacrifice, object_FieldGlass, object_Floor, object_Floor_Losing, object_Totem_Activating, object_Totem_First, object_watch,

        weapon_littleSword, weapon_dagger_launching, weapon_dagger_impact, weapon_taser, weapon_statue, weapon_magicWand, weapon_magicWand_magic, weapon_ak47_Impact, weapon_ak47_Launch,
        weapon_Sniper_Launch, weapon_Sniper_Impact, weapon_crowBar, weapon_bow_impact, weapon_bow_launch, weapon_mace,

        //EQUIPMENT
        equipment_open, equipment_equip, equipment_desequip, equipment_card_open_common, equipment_card_open_unCommon, equipment_card_open_rare,

        //ENVIRONNEMENT
        environment_endOfCombat, environment_positioning_1, environment_positioning_2, environment_enter_positioning, environment_enter_combat, monster_magnetic_attack, monster_magnetic_attract, monster_hit, monster_hit_2, monster_hit_dead,
        spell_monster_angry, environment_entering_map, environment_movingToNewArea,environment_movingToSameArea, environment_entering_fightBoss,environment_entering_fightNormal, environment_enteringTalent,environment_enteringEquipment,

        spell_warrior_particle_impact, spell_warrior_particle_impact_defense, spell_warrior_jump_jump, spell_warrior_jump_landing,

        //Spell 2
        spell2_basic_fist, spell2_warrior_ConflagrationEndless, spell2_warrior_ConflagrationPunch, spell2_warrior_RockThrow, spell2_warrior_spent, spell2_warrior_endurance,
        spell2_warrior_os, spell2_warrior_Strenght, spell2_warrior_DivineSword, spell2_warrior_Execution, spell2_warrior_Meteore, spell2_warrior_EarthTotemDamage

        , player_movement_move, player_movement_click_grass, player_movement_stopWalk
    };

    public static List<int> DIV = new List<int>();

    public static List<string> domain = new List<string>
    { "spell","spell2", "basic", "warrior", "common", "environment", "monster", "ui", "player","object","weapon","equipment","movement" };

    public static Dictionary<list, AudioSource> sounds = new Dictionary<list, AudioSource>();

    public static void Initialize()
    {
        if (sounds.Count > 0)
            return;

        sounds.Clear();

        for (int i = 0; i < System.Enum.GetValues(typeof(list)).Length; i++)
        {
            list soundName = (list)i;

            if (soundName == list.none) continue;

            sounds.Add(soundName, Initalize_One(soundName));


        }

        init_Music();
    }

    public static AudioSource Initalize_One(list sound_list)
    {
        //MakePath
        string path = "sound/";

        string currentDomain = "";

        int i = 0;

        string sound_list_string = sound_list.ToString();

        while (i < sound_list_string.Length)
        {
            char currentChar = sound_list_string[i];

            if (currentChar == '_' && domain.Contains(currentDomain))
            {
                path += currentDomain + "/";
                currentDomain = "";
            }
            else
            {
                currentDomain += sound_list_string[i];
            }

            i++;
        }

        path += currentDomain;

        AudioSource a = Resources.Load<AudioSource>(path);

        if (a == null)
            throw new System.Exception("No valid name at " + sound_list.ToString() + " and path = " + path);

        return a;
    }

    public static AudioSource GetSound(list soundToGet)
    {
        return sounds[soundToGet];
    }
}

/*
 * using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Audio;
using System.Diagnostics;

public partial class SoundManager : MonoBehaviour
{
    //UI
    public static AudioSource UI_ButtonPressed_2, UI_ChoiceLanguage, UI_Window_Open, UI_Window_Close, UI_SelectionSpell, UI_Error,
        UI_ButtonPressed, skill_newSpell;

    //MAP ENVIRONNEMENT
    public static AudioSource Environnement_EndOfCombat, Environnement_ChoosingPositioning_1, Environnement_ChoosingPositioning_2,
        Artifact_Collected;

    //COMMON SOUND (monster and player)
    public static AudioSource common_walkSound, common_tackle, common_invocation;

    //MONSTER
    public static AudioSource monster_dead, monster_dead_endCombat, monster_vala_attack, monster_normal_punch, monster_archer_pm,
        monster_funky_escape, monster_shark_tp, Monster_Funky_Wood, Monster_Archer_Arrow_Launch, Monster_Archer_Arrow_Impact,
        monster_inflamed_attack, vala_FireThrowing_Travelling, vala_FireThrowing_Impact, Monster_Shark_Bite, Monster_walk;

    //PLAYER RELATED
    public static AudioSource player_winning, player_losing, player_Enter_Combat, player_Enter_Positionning, player_levelUp;

    //SPELL
    public static AudioSource spell_punch, spell_alternate, spell_defense, spell_suffering, spell_flash, spell_kick, spell_Drive,
        spell_lasso, spell_PowerfulPunch, spell_DivineSword, spell_jump, spell_energy_zap, spell_energy_thunder, spell_taser, spell_Spike,
        spell_trap, spell_trap_set, spell_aspiration, spell_divineSword_first, spell_race, spell_mace, spell_enraged, spell_planification,
        spell_heal;

    public static void Initialize()
    {
        if (spell_punch != null)
            return;

        #region PLAYER SPECIFIC

        player_winning = Resources.Load<AudioSource>("Sound/Environment/Winnig");
        player_losing = Resources.Load<AudioSource>("Sound/Environment/Losing");
        player_Enter_Combat = Resources.Load<AudioSource>("Sound/Environment/Entrer_combat");
        player_Enter_Positionning = Resources.Load<AudioSource>("Sound/Environment/Entrer_positionning");

        #endregion

        #region PLAYER SPELL

        spell_punch = Resources.Load<AudioSource>("Sound/Spell/AudioSource_Punch");
        spell_kick = Resources.Load<AudioSource>("Sound/Spell/AudioSource_Kick");
        spell_Drive = Resources.Load<AudioSource>("Sound/Spell/AudioSource_Drive");
        spell_lasso = Resources.Load<AudioSource>("Sound/Spell/Spell_lasso");
        spell_PowerfulPunch = Resources.Load<AudioSource>("Sound/Spell/Sort_PowerfulPunch");
        spell_DivineSword = Resources.Load<AudioSource>("Sound/Spell/Spell_DivineSword");
        spell_jump = Resources.Load<AudioSource>("Sound/Spell/Spell_Jump");
        spell_energy_zap = Resources.Load<AudioSource>("Sound/Spell/Spell_Energy_Zap");
        spell_energy_thunder = Resources.Load<AudioSource>("Sound/Spell/Sort_Energy_Thunder");
        spell_taser = Resources.Load<AudioSource>("Sound/Spell/spell_taser");
        spell_Spike = Resources.Load<AudioSource>("Sound/Spell/Spell_Spike");
        spell_heal = Resources.Load<AudioSource>("Sound/Spell/Spell_Heal");
        spell_planification = Resources.Load<AudioSource>("Sound/Spell/Spell_planification");
        spell_enraged = Resources.Load<AudioSource>("Sound/Spell/Spell_Enraged");
        spell_race = Resources.Load<AudioSource>("Sound/Spell/Spell_race");
        spell_mace = Resources.Load<AudioSource>("Sound/Spell/Spell_Mace");
        spell_aspiration = Resources.Load<AudioSource>("Sound/Spell/Spell_aspiration");
        spell_divineSword_first = Resources.Load<AudioSource>("Sound/Spell/Spell_DivineSword_First");
        spell_trap = Resources.Load<AudioSource>("Sound/Spell/Spell_Trap");
        spell_trap_set = Resources.Load<AudioSource>("Sound/Spell/Spell_Trap_set");
        spell_alternate = Resources.Load<AudioSource>("Sound/Spell/Alternate");
        spell_defense = Resources.Load<AudioSource>("Sound/Spell/Defense");
        spell_flash = Resources.Load<AudioSource>("Sound/Spell/Flash");
        spell_suffering = Resources.Load<AudioSource>("Sound/Spell/Suffering");

        #endregion

        #region MONSTER SPECIFIC

        monster_dead = Resources.Load<AudioSource>("Sound/Monster/Monster_death");
        monster_dead_endCombat = Resources.Load<AudioSource>("Sound/Monster/Monster_death_endCombat");
        Monster_walk = Resources.Load<AudioSource>("Sound/Monster/Monster_walk");

        #endregion

        #region SPELL MONSTER

        monster_funky_escape = Resources.Load<AudioSource>("Sound/Monster/Monster_funky_escape");
        monster_shark_tp = Resources.Load<AudioSource>("Sound/Monster/Monster_shark_tp");
        Monster_Funky_Wood = Resources.Load<AudioSource>("Sound/Spell/Monster_Funky_Wood");
        Monster_Archer_Arrow_Launch = Resources.Load<AudioSource>("Sound/Monster/Monster_Archer_Arrow_Launch");
        Monster_Archer_Arrow_Impact = Resources.Load<AudioSource>("Sound/Monster/Monster_Archer_Arrow_Impact");
        monster_inflamed_attack = Resources.Load<AudioSource>("Sound/Monster/Monster_Inflamed_attack");
        vala_FireThrowing_Travelling = Resources.Load<AudioSource>("Sound/Monster/Vala_fireBall_travelling");
        vala_FireThrowing_Impact = Resources.Load<AudioSource>("Sound/Monster/Vala_FireBall_Impact");
        monster_normal_punch = Resources.Load<AudioSource>("Sound/Monster/Monster_Normal_punch");
        Monster_Shark_Bite = Resources.Load<AudioSource>("Sound/Monster/Monster_Shark_Bite");
        monster_archer_pm = Resources.Load<AudioSource>("Sound/Monster/Monster_archer_Pm");
        monster_vala_attack = Resources.Load<AudioSource>("Sound/Monster/Monster_Vala_attack");

        #endregion

        #region ENVIRONNEMENT

        Artifact_Collected = Resources.Load<AudioSource>("Sound/Environment/Artifact_Collected");
        Environnement_EndOfCombat = Resources.Load<AudioSource>("Sound/UI/UI_EndOfCombat");
        Environnement_ChoosingPositioning_1 = Resources.Load<AudioSource>("Sound/Environment/Environnement_ChoosingPositionning_1");
        Environnement_ChoosingPositioning_2 = Resources.Load<AudioSource>("Sound/Environment/Environnement_ChoosingPositionning_2");

        #endregion

        #region COMMON

        common_walkSound = Resources.Load<AudioSource>("Sound/Environment/WalkSound_loop");
        common_invocation = Resources.Load<AudioSource>("Sound/Spell/Invocation");
        common_tackle = Resources.Load<AudioSource>("Sound/Environment/JoueurTackle");

        #endregion

        #region UI

        UI_ButtonPressed = Resources.Load<AudioSource>("Sound/UI/UI_buttonPressed");
        UI_ButtonPressed_2 = Resources.Load<AudioSource>("Sound/UI/UI_buttonPressed_2");
        UI_ChoiceLanguage = Resources.Load<AudioSource>("Sound/UI/UI_LanguageChoice");
        skill_newSpell = Resources.Load<AudioSource>("Sound/UI/Skill_NewSpell");
        player_levelUp = Resources.Load<AudioSource>("Sound/Environment/Player_Up");
        UI_Window_Open = Resources.Load<AudioSource>("Sound/UI/UI_Window_Open");
        UI_Window_Close = Resources.Load<AudioSource>("Sound/UI/UI_Window_Close");
        UI_SelectionSpell = Resources.Load<AudioSource>("Sound/UI/UI_SelectionSpell");
        UI_Error = Resources.Load<AudioSource>("Sound/UI/UI_Error");

        #endregion
        //        spell_divineSword_first = Resources.Load<AudioSource>("Sound/Spell/Spell_DivineSword_First");
        //        spell_divineSword_first = Resources.Load<AudioSource>("Sound/Spell/Spell_DivineSword_First");
        //        spell_divineSword_first = Resources.Load<AudioSource>("Sound/Spell/Spell_DivineSword_First");
        //        spell_divineSword_first = Resources.Load<AudioSource>("Sound/Spell/Spell_DivineSword_First");

    }
}

 */
