using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Diagnostics;

public partial class Scene_Main : MonoBehaviour
{
    public ProgressionBarXp ProgessionBarXpScript;

    public Transform effect_parent;

    [HideInInspector]
    public float cam_YPosMin, cam_XPosMax, cam_YPosMax;

    public float cam_YPosMin_Passive, cam_XPosMax_Passive, cam_YPosMax_Passive;

    public float cam_YPosMin_Fight, cam_XPosMax_Fight, cam_YPosMax_Fight;

    public Color32 equipment_commonRef, equipment_inhabituelleRef, color_equipment_rareRef;

    public static Color32 color_equipment_common, color_equipment_uncommon, color_equipment_rare;

    public Color32 equipment_commonRefHigh, equipment_inhabituelleRefHigh, color_equipment_rareRefHigh;

    public static Color32 color_equipment_commonHigh, color_equipment_uncommonHigh, color_equipment_rareHigh;

    public MiniMap miniMap;

    void Awake()
    {
        color_equipment_common = equipment_commonRef;
        color_equipment_uncommon = equipment_inhabituelleRef;
        color_equipment_rare = color_equipment_rareRef;

        color_equipment_commonHigh = equipment_commonRefHigh;
        color_equipment_uncommonHigh = equipment_inhabituelleRefHigh;
        color_equipment_rareHigh = color_equipment_rareRefHigh;

        Main_UI.dontChangeCursor.Clear();

        //Initialize the scene with value to redefine etc...
        V.Initializing_startScene_Awake();

        //Instantiate the toolbar
        V.main_ui.ToolBarManagement_Instantiate();

        //Assignate static value here
        //The gameobject of the title
    }

    public static Color32 ChooseColorFromRarity(SingleEquipment.rarity rarity)
    {
        if (rarity == SingleEquipment.rarity.Common)
            return color_equipment_common;
        else if (rarity == SingleEquipment.rarity.Uncommon)
            return color_equipment_uncommon;
        else if (rarity == SingleEquipment.rarity.Rare)
            return color_equipment_rare;

        throw new System.Exception("mauvais rarity dans choose colorFromRarity sceneMain");
    }

    public static Color32 ChooseColorFromRarityHigh(SingleEquipment.rarity rarity)
    {
        if (rarity == SingleEquipment.rarity.Common)
            return color_equipment_commonHigh;
        else if (rarity == SingleEquipment.rarity.Uncommon)
            return color_equipment_uncommonHigh;
        else if (rarity == SingleEquipment.rarity.Rare)
            return color_equipment_rareHigh;

        throw new System.Exception("mauvais rarity dans choose colorFromRarityHigh sceneMain");
    }

    /// <summary>
    /// The unseen tile that cover all tile in the boundaries of the camera (spawnable tile for monster
    /// </summary>
    public static List<string> SpawnableMonsterTile = new List<string>();

    private void Start()
    {

        V.Initializing_startScene_Start();


        //Make the game be out combat for the start
        V.game_state = V.State.passive;

        //Set spawnableMonsterTile
        SpawnableMonsterTile = SetSpawnableMonsterTile();

        //Activate / Dectivate gameobject
        Main_Map.ground_positionning.gameObject.SetActive(false);



    }

    //The button in the bottom right of the toolbar
    //And the sprite that show aside the mouse when a spell is selectionned
    public GameObject button_EndOfturn;

    public static bool aWindowIsUsed;

    public static bool isMouseOverAWindow;

    public SpriteRenderer WinLooseTransition;

    private void Update()
    {
        V.Administrator_update();

        aWindowIsUsed = WindowInfo.Instance.IsMouseOverAreaOfAWindow();

        isMouseOverAWindow = WindowInfo.Instance.IsMouseOverAreaOfAWindow();

        bool SpellEqualEmpty = SpellGestion.selectionnedSpell_list == SpellGestion.List.empty;

        if (Input.GetMouseButtonDown(0) && WindowInfo.Instance.listActiveWindow.Count > 0 && SpellEqualEmpty && !isMouseOverAWindow && Panel_button.currentSelectionedButton != null)
        {
            WindowInfo.Instance.DeselectionnateAllWindow();
        }

        //Managing input and state
        if (V.game_state == V.State.positionning)
        {
            //Manage the positioning phase
            Positionning_management();
        }
        else if (V.game_state == V.State.fight)
        {
            cam_YPosMax = cam_YPosMax_Fight;
            cam_YPosMin = cam_YPosMin_Fight;
            cam_XPosMax = cam_XPosMax_Fight;

            //The combat Phase
            Combat_management();
        }
        else if (V.game_state == V.State.passive)
        {
            cam_YPosMax = cam_YPosMax_Passive;
            cam_YPosMin = cam_YPosMin_Passive;
            cam_XPosMax = cam_XPosMax_Passive;

            PassiveManagement();
        }

        V.main_ui.Update_Ui();

        Scene_Main_EchapControl.EchapManagement();

        SoundManager.MusicGestion();

        var findWindow = WindowInfo.Instance.GetWindow(WindowInfo.type.Ascension);

        if (findWindow.find)
        {
            var ascensionWindow = findWindow.w as Window_Ascension;

            if (!ascensionWindow.noInfo)
                WinLooseTransition.DOFade(1, 1);
        }
    }

    #region For "MustBeAssigned" on "MyBox" made by Andrew Rumak thank to him

    //From "MyBox" of Andrew Rumak
    [HideInInspector]
    public MonoBehaviour MyScript;

    [HideInInspector]
    public float MyFloat;

    #endregion
}