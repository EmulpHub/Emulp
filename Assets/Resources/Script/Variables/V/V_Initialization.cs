using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using UnityEngine.Tilemaps;

public partial class V : MonoBehaviour
{
    public static bool SaveHaveBeenLoaded = false;

    public static Editor_TileSetHolder InfoTile;

    public static bool ActiveSpellHaveBeenLoaded;

    public static relic_managmentMain relic_ManagmentMain;

    public static void Initializing_startScene_Awake()
    {
        RelicInit.InitRelic();

        Main_UI.InitUIDisplay();

        descColor.initColor();
        Ascension.InitAllAscensionModifier();

        SoundManager.Initialize();

        Canvas.ForceUpdateCanvases();

        Main_UI.InitalizeCursorTexture();

        Initializing_sprite();

        SpellGestion.Initialize();

        Origin.InitOrigin();

        primaryStatsSetter.Init();

        if (SceneManager.GetActiveScene().name == "Main")
        {
            InfoTile = GameObject.Find("INFOHOLDER").GetComponent<Editor_TileSetHolder>();

            Talent_Gestion.Init();

            Animation_AcquiredStuff.Init();

            Equipment_Management.Init();

            Spell.Initialize_Particle();

            AnimEndlessStatic.createSprite();

            V.player = GameObject.FindGameObjectWithTag("Player");
            V.player_entity = V.player.GetComponent<Player>();

            EquipClass_init.Init();

            SingleEquipment.CreateLs(Resources.Load<Transform>("Prefab/Equipment/list_equipment"));

            V.map_possibleToMove = GameObject.Find("PossibleToMove").GetComponent<Map_PossibleToMove>();
            V.CombatSpell_VisualEffect = GameObject.Find("Toolbar").GetComponent<CombatSpell_VisualEffect>();
            V.script_Scene_Main = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Scene_Main>();
            V.script_Scene_Main_Administrator = GameObject.Find("Administrator").GetComponent<Scene_Main_Administrator>();
            V.main_ui = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Main_UI>();
            V.combat_spell_GameObject = Resources.Load<GameObject>("Prefab/Spell");
            V.worldNavigationGestion = GameObject.Find("Maps_parents").GetComponent<WorldNavigation>();
            V.errorTxt = GameObject.Find("ErrorText").GetComponent<ErrorTxt>();
            mapTransitionScreen = GameObject.Find("TransitionScreen").GetComponent<SpriteRenderer>();

            Main_Object.Initialize_object();

            Initializing_TileBase();

            Animation_Custom_TalentEnergy.Initialize_GameObject();

            Main_Map.Initialize();

            if (V.administrator && V.script_Scene_Main_Administrator.dontGenerateMap)
            {
                var map = V.script_Scene_Main_Administrator.dontGenerateMapGameobject;

                map.Init();

                Main_Map.SetMap(map);

                DirectionData.LoadAllDirectionData("0_0");
            }
            else
            {
                WorldLoad.GenerateWorldAndMiniMap(0);
            }

            //SaveHaveBeenLoaded = Save_SaveSystem.LoadSave_WithoutWarning();

            ActiveSpellHaveBeenLoaded = SpellInToolbar.activeSpell.Count > 0;

            SpellInToolbar.activeSpell_script.Clear();

            SpellGestion.get_description_calcValue_init();

            Character.InitForbideenTalent();

            CardStarter.MainStart();

            SquareToNextAreaManager.Instance.Init();
        }
    }

    public static void Initializing_startScene_Start()
    {
        string scene_name = SceneManager.GetActiveScene().name;

        if (scene_name == "Main")
        {
            Effect.AddEvent();

            //SaveHaveBeenLoaded = Save_SaveSystem.LoadSave_Start_WithoutWarning();

            if (V.Tutorial_Get())
            {
                Instantiate(Resources.Load<GameObject>("Prefab/tutorial/TutorialHolder"));
            }
            else if (!ActiveSpellHaveBeenLoaded)
            {
                SpellGestion.AddStartingSpellToTheToolbar();
            }

            V.player_info.CalculateValue();

        }
    }

    public static List<EventHandler> listStaticEvent = new List<EventHandler>();

    public static void EraseNonStaticVar()
    {
        foreach (EventHandler e in listStaticEvent)
        {
            e.Clear();
        }

        PlayerMoveAutorization.Instance.Clear();

        ClickAutorization.Clear();

        ClickAutorization.Exception_Clear();

        AliveEntity.Instance.Clear();

        EntityOrder.Clear();

        primaryStatsSetter.ClearCurrentStats();

        SpellInToolbar.activeSpell_script.Clear();

        game_state = State.passive;

        Scene_Main.isMouseOverAWindow = false;
        Map_PossibleToMove.MouseIsOnToolbar = false;

        WindowInfo.Instance.ClearListWindow();

        Main_UI.dontChangeCursor.Clear();
    }

    public static void ErasePlayerStats()
    {
        player_info.ResetExpStats();

        SpellInToolbar.activeSpell.Clear();

        TreeSkill.PurchasedSkill.Clear();

        Window_skill.ShowTalent = false;

        Character.baseSpellDic_Locked.Clear();

        WindowInfo.Instance.ClearListWindow();

        RelicInit.ClearEquipedRelic();

        relic.ResetAllRelicStat();
        Effect.RemoveEvent();

        Combat_Effect.listCombatEffect.Clear();

        Equipment_Management.All.Clear();
        Equipment_Management.LockedLs.Clear();
        Equipment_Management.Locked.Clear();
        Equipment_Management.ObjectWeapon_List.Clear();

        Equipment_InventoryManagement.AcquiredEquipment.Clear();
        Equipment_Management.Equiped.Clear();
        Equipment_Management.NotEquiped.Clear();
        Equipment_Management.NewEquipment.Clear();

        Equipment_Management.ClearAllEquipmentEffect();

        Talent_Gestion.lockedTalent.Clear();
        Character.equipedTalent.Clear();

        Talent_Gestion.unlockedTalent.Clear();
        Talent_Gestion.newTalentUnlocked.Clear();

        Origin.ChangeOrigin(Origin.Value.none);

        Origin_Passive.ClearPlayer();

        Window_skill.IsRandomized = false;
    }

    public static Sprite tackle_effect_sprite,
        shark_effect_sprite,
        fleeing_effect_sprite,
        accuracy_effect_sprite,
        volcanicPower_effect_sprite,
        invoked_effect_sprite,
        inflamed_attack_sprite,
        vala_Attack_sprite,
        mace_illustration,
        base_taser_illustration,
        base_aspiration_illustration,
        warrior_power,
        warrior_resistance,
        logo_cooldown,
        logo_usePerTurn,
        logo_distance,
        logo_melee,
        logo_player,
        massProtection_Individual,
        whip_animation,
        warrior_heal,
        warrior_suffering_animation,
        icon_equipment_common,
        icon_equipment_uncommon,
        icon_equipment_rare,
        weapon_bullet,
        weapon_bulletStrong,
        weapon_magic,
        object_liberation_graphic,
        object_rush_pm,
        object_totem_Active,
        warrior_weakness,
        warrior_modularHeart_Electricity,
        warrior_modularHeart_Spikes,
        grassy_effect_sprite,
        revenge,
        grassy_boost, grassy_attack, grassy_heal,
        magnetic_metal, magnetic_nail, magnetic_magnet,
        ui_box, ui_box_opaque,
        eye_angry_default,
        movemementPoint_Arrow, movementPoint_Marker,
        monster_normalPassive,
        monster_juniorPassive,
        exercise_pa, exercise_pm,
        damageExplosion,
        strength_green, strength_red,
        divineSword_Anim,
        attraction_center, attraction_arrow,
        relic_life_endlessRender,
        icon_po,
        norticeIcon,
        CC, icon_cc, icon_ec,
        norticeSurface_power,
        effect_bleeding;

    public static Texture2D cursor_up, cursor_right, cursor_upRight, cursor_upLeft;

    public static void Initializing_sprite()
    {
        tackle_effect_sprite = Resources.Load<Sprite>("Image/miscellaneous/tackle_effect");
        shark_effect_sprite = Resources.Load<Sprite>("Image/Monster/Spell/shark_effect");
        fleeing_effect_sprite = Resources.Load<Sprite>("Image/Monster/Spell/fleeing_effect");
        accuracy_effect_sprite = Resources.Load<Sprite>("Image/Monster/Spell/accuracy_effect");
        volcanicPower_effect_sprite = Resources.Load<Sprite>("Image/Monster/Spell/Vala_VolcanicPower");
        invoked_effect_sprite = Resources.Load<Sprite>("Image/Monster/Spell/Invoked");
        inflamed_attack_sprite = Resources.Load<Sprite>("Image/Monster/Spell/Monster_Inflamed_Attack");
        vala_Attack_sprite = Resources.Load<Sprite>("Image/Monster/Spell/Monster_Vela_Attack");
        mace_illustration = Resources.Load<Sprite>("Image/Sort/Base_Mace_illustration");
        base_taser_illustration = Resources.Load<Sprite>("Image/Sort/Base_Taser_Illustration");
        base_aspiration_illustration = Resources.Load<Sprite>("Image/Sort/base_aspiration_illustration");
        warrior_power = Resources.Load<Sprite>("Image/Sort/Warrior/Warrior_Power");
        warrior_resistance = Resources.Load<Sprite>("Image/Sort/warrior/Defense");

        logo_cooldown = Resources.Load<Sprite>("UI/SwedishPalette/logo_Cd");
        logo_usePerTurn = Resources.Load<Sprite>("UI/SwedishPalette/logo_UsePerTurn");
        logo_distance = Resources.Load<Sprite>("UI/SwedishPalette/logo_distance");
        logo_melee = Resources.Load<Sprite>("UI/SwedishPalette/logo_melee");
        logo_player = Resources.Load<Sprite>("UI/SwedishPalette/Logo_Distance_Player");

        massProtection_Individual = Resources.Load<Sprite>("Image/Sort/warrior/MassProtection_individual");
        whip_animation = Resources.Load<Sprite>("Image/Sort/warrior/Whip_Animation");
        warrior_heal = Resources.Load<Sprite>("Image/Sort/Warrior_Heal");
        warrior_suffering_animation = Resources.Load<Sprite>("Image/Sort/warrior/Suffering_Animation");
        icon_equipment_common = Resources.Load<Sprite>("Image/Equipment/other/icon_equipment_common");
        icon_equipment_uncommon = Resources.Load<Sprite>("Image/Equipment/other/icon_equipment_uncommon");
        icon_equipment_rare = Resources.Load<Sprite>("Image/Equipment/other/icon_equipment_rare");
        weapon_bullet = Resources.Load<Sprite>("Image/miscellaneous/bullet");
        weapon_bulletStrong = Resources.Load<Sprite>("Image/miscellaneous/bullet_strong");
        weapon_magic = Resources.Load<Sprite>("Image/miscellaneous/Magic");
        object_liberation_graphic = Resources.Load<Sprite>("Image/miscellaneous/Liberation_Graphic");
        object_rush_pm = Resources.Load<Sprite>("Image/miscellaneous/Object_Rush_pm");
        object_totem_Active = Resources.Load<Sprite>("Image/miscellaneous/Totem_Active");
        warrior_weakness = Resources.Load<Sprite>("Image/Sort/warrior/weakness");
        warrior_modularHeart_Electricity = Resources.Load<Sprite>("Image/miscellaneous/modularHeart_Electrical");
        warrior_modularHeart_Spikes = Resources.Load<Sprite>("Image/miscellaneous/modularHeart_Spike");
        grassy_effect_sprite = Resources.Load<Sprite>("Image/Monster/Spell/grassy_passive");
        revenge = Resources.Load<Sprite>("Image/Monster/Spell/Revenge");
        grassy_boost = Resources.Load<Sprite>("Image/Monster/Spell/grassy_boost");
        grassy_attack = Resources.Load<Sprite>("Image/Monster/Spell/grassy_attack");
        grassy_heal = Resources.Load<Sprite>("Image/Monster/Spell/grassy_heal");
        magnetic_metal = Resources.Load<Sprite>("Image/Monster/Spell/magnetic_metal");
        magnetic_nail = Resources.Load<Sprite>("Image/Monster/Spell/magnetic_nail");
        magnetic_magnet = Resources.Load<Sprite>("Image/Monster/Spell/magnetic_magnet");
        ui_box = Resources.Load<Sprite>("UI/DisplayDescription/UI_box");
        ui_box_opaque = Resources.Load<Sprite>("UI/DisplayDescription/UI_box_noOpacity");
        eye_angry_default = Resources.Load<Sprite>("Image/Monster/eye/eye_angry");
        movemementPoint_Arrow = Resources.Load<Sprite>("Image/miscellaneous/movementPoint_Arrow");
        movementPoint_Marker = Resources.Load<Sprite>("Image/miscellaneous/movementPoint_Markeur");
        monster_normalPassive = Resources.Load<Sprite>("Image/Monster/Spell/normal");
        exercise_pa = Resources.Load<Sprite>("Image/miscellaneous/Exercice_Pa");
        exercise_pm = Resources.Load<Sprite>("Image/miscellaneous/Exercise_pm");
        damageExplosion = Resources.Load<Sprite>("Image/Monster/Spell/entity_damageExplosion_2");
        monster_juniorPassive = Resources.Load<Sprite>("Image/Monster/Spell/JuniorPassive");

        divineSword_Anim = Resources.Load<Sprite>("Image/Sort/warrior/DivineSword_Anim");

        strength_green = Resources.Load<Sprite>("Image/Sort/warrior/Force_heal");
        strength_red = Resources.Load<Sprite>("Image/Sort/warrior/Force_power");

        attraction_arrow = Resources.Load<Sprite>("Image/Sort/warrior/Attraction_arrow");
        attraction_center = Resources.Load<Sprite>("Image/Sort/warrior/attraction_center");

        relic_life_endlessRender = Resources.Load<Sprite>("Image/relic/relic_life_endlessRender");

        icon_po = Resources.Load<Sprite>("Image/UI/icon_equipment_po");

        norticeIcon = Resources.Load<Sprite>("Image/relic/notrice");

        CC = Resources.Load<Sprite>("Image/miscellaneous/CC");
        icon_cc = Resources.Load<Sprite>("Image/UI/icon_equipment_cc");
        icon_ec = Resources.Load<Sprite>("Image/UI/icon_equipment_ec");

        norticeSurface_power = Resources.Load<Sprite>("Image/Sort/norticeSurface/power");

        effect_bleeding = Resources.Load<Sprite>("Image/Sort/warrior/bleeding");

        cursor_up = Resources.Load<Texture2D>("UI/Cursor/cursor_up_2");
        cursor_right = Resources.Load<Texture2D>("UI/Cursor/cursor_right_2");
        cursor_upRight = Resources.Load<Texture2D>("UI/Cursor/cursor_upRight_2");
        cursor_upLeft = Resources.Load<Texture2D>("UI/Cursor/cursor_upLeft_2");

        Initializing_sprite_pixel();
    }

    public static Sprite pix_weakness,
        pix_power,
        pix_defense,
        pix_spike,
        pix_heal,
        pix_pa,
        pix_pm,
        pix_angry,
        pix_bleeding,
        pix_heartDamage,
        pix_armor;

    public static void Initializing_sprite_pixel()
    {
        pix_weakness = Resources.Load<Sprite>("Image/UI/PixelArt/pix_weakness");
        pix_power = Resources.Load<Sprite>("Image/UI/PixelArt/pix_power");
        pix_defense = Resources.Load<Sprite>("Image/UI/PixelArt/pix_defense");
        pix_spike = Resources.Load<Sprite>("Image/UI/PixelArt/pix_spike");
        pix_heal = Resources.Load<Sprite>("Image/UI/PixelArt/pix_heal");
        pix_pa = Resources.Load<Sprite>("Image/UI/PixelArt/pix_pa");
        pix_pm = Resources.Load<Sprite>("Image/UI/PixelArt/pix_pm");
        pix_angry = Resources.Load<Sprite>("Image/UI/PixelArt/pix_angry");
        pix_bleeding = Resources.Load<Sprite>("Image/UI/PixelArt/pix_bleeding");
        pix_heartDamage = Resources.Load<Sprite>("Image/UI/PixelArt/pix_heartDamage");
        pix_armor = Resources.Load<Sprite>("Image/UI/PixelArt/pix_armor");
    }

    public static TileBase Go_Up, Go_Down, Go_Left, Go_Right;

    public static void Initializing_TileBase()
    {
        Go_Right = Resources.Load<TileBase>("Image/Tile/TileBase/Tile_NextArea_Right");
        Go_Left = Resources.Load<TileBase>("Image/Tile/TileBase/Tile_NextArea_Left");
        Go_Up = Resources.Load<TileBase>("Image/Tile/TileBase/Tile_NextArea_Up");
        Go_Down = Resources.Load<TileBase>("Image/Tile/TileBase/Tile_NextArea_Down");
    }
}
