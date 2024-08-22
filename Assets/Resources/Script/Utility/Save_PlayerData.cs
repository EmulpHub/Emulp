using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Save_PlayerData
{
    public int player_level, player_xp, player_xp_max, player_skillPoint;

    public List<SpellGestion.List> toolbar_R = new List<SpellGestion.List>();

    public static List<SpellGestion.List> toolbar = new List<SpellGestion.List>();

    public List<SpellGestion.List> purchasedSkill = new List<SpellGestion.List>();

    public List<Talent_Gestion.talent> equiped_talent = new List<Talent_Gestion.talent>();

    public int Area, Area_count, Area_count_max, GameMonster_Level;

    public int monsterSave_level;

    public MonsterInfo.MonsterType monsterSave_type;

    public List<(MonsterInfo.MonsterType type, int level)> monster_list_save = new List<(MonsterInfo.MonsterType type, int level)>();

    //ARtifact

    public List<SpellGestion.List> activeWeaponCard = new List<SpellGestion.List>();
    public List<SpellGestion.List> NonSelectedWeaponCard = new List<SpellGestion.List>();

    public SpellGestion.List currentWeapon = SpellGestion.List.base_fist;

    //OPTION
    public float Volume;
    public bool Mute;
    public bool Fullscreen;

    public V.L Language;

    //SKILL
    public bool ShowTalent;
    public List<int> baseSpellDic_Locked = new List<int>();


    public int FPS_Higher_old, FPS_Lower_old, FPS_Average_old, FPS_Average_Recent_old;

    //Tutoriel
    public bool ActiveTutoriel;

    //Equipment
    public List<string> NotEquiped = new List<string>();

    public List<string> NewEquipment = new List<string>();

    public Dictionary<SingleEquipment.type, string>
        Equiped = new Dictionary<SingleEquipment.type, string>();

    //Talent
    public List<Talent_Gestion.talent> lockedTalent, unlockedTalent, newTalentUnlocked = new List<Talent_Gestion.talent>();

    public Dictionary<Character.talentSlot, Talent_Gestion.talent> equipedTalent = new Dictionary<Character.talentSlot, Talent_Gestion.talent>();

    //Map
    public string playerPosition;

    public Dictionary<string, Dictionary<string, bool>> collectableSave =
        new Dictionary<string, Dictionary<string, bool>>();

    public List<SpellGestion.List> choosenSkillSave = new List<SpellGestion.List>();
    public bool randomized;

    public int HigherAscension, HigherUnlockedAscension, nortice, currentAscension;

    public void Save()
    {
        bool SavePlayerData = true;

        if (SceneManager.GetActiveScene().name == "Main")
        {
            SavePlayerData = !V.script_Scene_Main_Administrator.SaveOnlyForFps;
        }

        if (V.administrator)
        {
            FPS_Higher_old = Scene_Main_Administrator.FPS_Higher;
            FPS_Lower_old = Scene_Main_Administrator.FPS_Lower;
            FPS_Average_old = Scene_Main_Administrator.FPS_Average;
            FPS_Average_Recent_old = Scene_Main_Administrator.FPS_Average_Recent;
        }

        if (!SavePlayerData)
            return;

        //PLAYER
        player_level = V.player_info.level;

        player_xp = V.player_info.xp;
        player_xp_max = V.player_info.xp_max;
        player_skillPoint = V.player_info.point_skill;

        //TOOLBAR
        toolbar_R = SpellInToolbar.activeSpell;

        //SKILL
        purchasedSkill = TreeSkill.PurchasedSkill;
        choosenSkillSave = Window_skill.choosenSkillSave;
        randomized = Window_skill.IsRandomized;

        //equiped_talent = Character.equipedTalent;
        /*
        talent_panel_skill_color[0] = Panel_skill_talent.Buy_Color.r;
        talent_panel_skill_color[1] = Panel_skill_talent.Buy_Color.g;
        talent_panel_skill_color[2] = Panel_skill_talent.Buy_Color.b;
        talent_panel_skill_color[3] = Panel_skill_talent.Buy_Color.a;*/

        ShowTalent = Window_skill.ShowTalent;

        //MAP
        GameMonster_Level = Scene_Main.game_monsterLevel;

        //MONSTER
        monsterSave_level = Scene_Main.monster_levelSave;
        monsterSave_type = Scene_Main.monster_typeSave;
        monster_list_save = Scene_Main.monster_list_save;

        //OPTION
        Volume = SoundManager.Volume;
        Mute = SoundManager.MuteSound;
        Fullscreen = Screen.fullScreen;

        Language = V.language;

        //Tutorial
        ActiveTutoriel = V.Tutorial_Get();

        //Equipment
        NotEquiped.Clear();
        Equiped.Clear();

        NotEquiped = ConvertEquipmentListIntoId(Equipment_Management.NotEquiped);
        NewEquipment = ConvertEquipmentListIntoId(Equipment_Management.NewEquipment);

        foreach (SingleEquipment.type type in Equipment_Management.Equiped.Keys)
        {
            Equiped.Add(type, Equipment_Management.Equiped[type].name);
        }

        //Talent
        lockedTalent = Talent_Gestion.lockedTalent;
        unlockedTalent = Talent_Gestion.unlockedTalent;
        newTalentUnlocked = Talent_Gestion.newTalentUnlocked;
        equipedTalent = Character.equipedTalent;

        //Map
        playerPosition = WorldData.PlayerPositionInWorld;
        baseSpellDic_Locked = Character.baseSpellDic_Locked;

        //Notrice
        HigherAscension = Ascension.HigherAscension;
        HigherUnlockedAscension = Ascension.HigherUnlockedAscension;
        nortice = Ascension.nortice;
        currentAscension = Ascension.currentAscension;

    }

    public List<string> ConvertEquipmentListIntoId(List<SingleEquipment> ls)
    {
        List<string> a = new List<string>();

        foreach (SingleEquipment b in ls)
        {
            if (b != null)
                a.Add(b.name);
        }

        return a;
    }

    public List<SingleEquipment> ConvertIdListIntoEquipment(List<string> ls)
    {
        List<SingleEquipment> a = new List<SingleEquipment>();

        foreach (string b in ls)
        {
            a.Add(SingleEquipment.GetEquipFromName(b));
        }

        return a;
    }

    public void Load_Awake()
    {

        bool IsMain = SceneManager.GetActiveScene().name == "Main";

        bool SavePlayerData = true;

        if (IsMain)
        {
            SavePlayerData = !(V.script_Scene_Main_Administrator.SaveOnlyForFps && V.administrator);
        }

        if (V.administrator)
        {
            Scene_Main_Administrator.FPS_Higher_old = FPS_Higher_old;
            Scene_Main_Administrator.FPS_Lower_old = FPS_Lower_old;
            Scene_Main_Administrator.FPS_Average_old = FPS_Average_old;
            Scene_Main_Administrator.FPS_Average_Recent_old = FPS_Average_Recent_old;
        }

        if (!SavePlayerData)
            return;

        //OPTION
        SoundManager.Volume = Volume;
        SoundManager.MuteSound = Mute;
        Screen.fullScreen = Fullscreen;

        //OTHER
        V.Tutorial_Set(!SavePlayerData);

        if (SavePlayerData)
        {
            StartMenu.ShowTutorial = false;
        }

        V.language = Language;

        if (!IsMain)
        {
            return;
        }

        toolbar = toolbar_R;

        V.player_info.level = player_level;

        V.player_info.xp = player_xp;
        V.player_info.xp_max = player_xp_max;

        //Skill
        V.player_info.point_skill = player_skillPoint;

        TreeSkill.PurchasedSkill = purchasedSkill;

        Window_skill.choosenSkillSave = choosenSkillSave;
        Window_skill.IsRandomized = randomized;

        Scene_Main.game_monsterLevel = GameMonster_Level;

        Window_skill.ShowTalent = ShowTalent;

        //MONSTER
        Scene_Main.monster_levelSave = monsterSave_level;
        Scene_Main.monster_typeSave = monsterSave_type;

        Scene_Main.monster_list_save = monster_list_save;

        //Talent
        Talent_Gestion.lockedTalent = lockedTalent;
        Talent_Gestion.unlockedTalent = unlockedTalent;
        Talent_Gestion.newTalentUnlocked = newTalentUnlocked;
        Character.equipedTalent = equipedTalent;

        Character.UpdateAllTalentEffect();

        //Map
        Debug.Log("Save player pos = " +playerPosition);
        WorldData.PlayerPositionInWorld = playerPosition;
        Character.baseSpellDic_Locked = baseSpellDic_Locked;


        //Notrice
        Ascension.HigherAscension = HigherAscension;
        Ascension.HigherUnlockedAscension = HigherUnlockedAscension;
        Ascension.nortice = nortice;
        Ascension.currentAscension = currentAscension;
    }

    public void Load_Start()
    {
        bool IsMain = SceneManager.GetActiveScene().name == "Main";

        bool SavePlayerData = true;

        if (IsMain)
        {
            SavePlayerData = !(V.script_Scene_Main_Administrator.SaveOnlyForFps && V.administrator);
        }

        if (!SavePlayerData)
            return;


        if (!IsMain)
        {
            return;
        }

        //Equipment

        Equipment_Management.NotEquiped = ConvertIdListIntoEquipment(NotEquiped);
        Equipment_Management.NewEquipment = ConvertIdListIntoEquipment(NewEquipment);

        Equipment_Management.Equiped.Clear();

        foreach (SingleEquipment.type type in Equiped.Keys)
        {
            SingleEquipment e = SingleEquipment.GetEquipFromName(Equiped[type]);

            Equipment_Management.Equiped.Add(type, e);

            if (type == SingleEquipment.type.object_equipment)
            {
                Equipment_Management.ChangeObject(e.Spell, true);
            }
            else if (type == SingleEquipment.type.object_equipment_2)
            {
                Equipment_Management.ChangeObject(e.Spell, false);
            }
        }
    }
}