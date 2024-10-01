using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

public class Scene_Main_Administrator : MonoBehaviour
{
    #region TemporaryTest

    public void TemporaryTest_Start() { }

    public void TemporaryTest_Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            float damage = 10;
            if (Input.GetKey(KeyCode.LeftShift))
                damage = 50;

            V.player_entity.Damage(new InfoDamage(damage, V.player_entity, true));
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            V.player_entity.Info.LifeMaxAddDebug += 10;

            V.player_entity.Info.CalculateValue();
            V.player_entity.Info.ResetAllStats();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            V.player_entity.Info.LifeMaxAddDebug -= 10;

            V.player_entity.Info.CalculateValue();
            V.player_entity.Info.ResetAllStats();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            V.player_entity.Heal(new InfoHeal(10));
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            V.player_entity.InfoPlayer.AddArmor(10);
        }
    }

    public IEnumerator feur ()
    {
        V.player_entity.ResetAllStats();

        yield return new WaitForSeconds(1);

        V.player_entity.Damage(new InfoDamage(90, V.player_entity, true));
    }

    public static int easeId;

    #endregion

    #region Option Management

    public Origin.Value predefinedOrigin;

    public bool predefinedRelic_bool;

    public RelicInit.relicLs predefinedRelic;

    public List<MonsterInfo.MonsterType> focus_monster;

    public List<SpellGestion.List> focus_spell;

    public List<string> focus_Equipment = new List<string>();

    public bool AddSpell;

    public int levelMonster = 0;

    public int startMonsterNb = 0;

    public bool Infinite_PMPALEAK;

    public bool ResetStartMap = true, activeTutorial = false;

    public bool SaveOnlyForFps;

    public bool ShowAutorization;

    public bool ShowAWindowIsMouseOver;

    public bool InstaFight;

    public int ForcePC = 0, ForcePa, ForcePm, ForcePa_Monster, ForcePm_Monster;

    public bool Invulnerable;

    public bool activeFps;

    public bool unlockAllEquipments, unlockAllTalents;

    public V.L Language;

    public bool permaShowAscension = true;

    public bool ShowRelicCard;

    public bool activateMusic;

    public bool onlyCrit, noCrit;

    public bool dontGenerateMap;

    public Map dontGenerateMapGameobject;

    public void Awake()
    {
        if (!V.administrator)
        {
            ResetValue();

            Administrator_holder.gameObject.SetActive(false);
        }
        else
            Option_Awake();
    }

    public void ResetValue()
    {
        Invulnerable = false;
        ForcePa = 0;
        ForcePm = 0;
        ForcePa_Monster = 0;
        ForcePm_Monster = 0;
        InstaFight = false;
        levelMonster = 0;
        startMonsterNb = 0;
        Infinite_PMPALEAK = false;
        focus_monster = new List<MonsterInfo.MonsterType>();
        focus_spell = new List<SpellGestion.List>();
        ResetStartMap = false;
        activeTutorial = false;
        SaveOnlyForFps = false;
        ShowAWindowIsMouseOver = false;
    }

    public void Option_Awake()
    {
        if (activeTutorial)
        {
            V.Tutorial_Set(true);
        }

        V.language = Language;

    }

    public void Option_Start()
    {
        if (ForcePC != 0)
            V.player_info.point_skill = ForcePC;

        StartCoroutine(Option_Start_Ienumerator());
    }

    public IEnumerator Option_Start_Ienumerator()
    {
        StartCoroutine(Option_AddSpell());

        if (predefinedOrigin != Origin.Value.none)
        {
            Origin.ChangeOrigin(predefinedOrigin);
        }

        if (predefinedRelic_bool)
        {
            RelicInit.EquipRelic(predefinedRelic);
        }

        yield return new WaitForSeconds(0);
    }

    public IEnumerator Option_AddSpell()
    {
        yield return new WaitForSeconds(0.1f);

        foreach (SpellGestion.List l in focus_spell)
        {
            if (l != SpellGestion.List.empty)
            {
                Main_UI.Toolbar_AddSpell(l);
            }
        }

        /*
        foreach (string s in focus_Equipment)
        {
            Collectable_Chest.Add(-1, null, false, Equipment_Single.GetEquipFromName(s));
        }*/

    }

    public void Option_Update()
    {
        if (ShowAWindowIsMouseOver)
            print("WindowIsMouseOver = " + Scene_Main.isMouseOverAWindow);

        if (ShowAutorization)
        {
            if (ClickAutorization.list.Count == 0)
                print("No current click autorization");

            foreach (int a in ClickAutorization.list)
            {
                print("autorization = " + a);
            }
        }

        if (AddSpell)
        {
            AddSpell = false;
            StartCoroutine(Option_AddSpell());
        }

    }

    #endregion

    public GameObject Administrator_holder;

    public Text Administrator_fps;

    [HideInInspector]
    public float Delay;

    public bool Showfps;

    // Start is called before the first frame update
    void Start()
    {
        Reset();

        Delay = 1;

        Option_Start();

        TemporaryTest_Start();
    }

    private void Update()
    {
        Option_Update();
    }

    private void LateUpdate()
    {

        Administrator_fps.gameObject.SetActive(V.administrator);

        if (!V.administrator)
            return;
        if (Showfps)
        {

            if (Delay <= 0)
                Administrator_fps.text = FpsManager();
            else
                Delay -= 1 * Time.deltaTime;
        }
        else
        {
            Administrator_fps.text = "";
        }

        if (showHelpTxt)
            Administrator_fps.text += "\n\n L = Save \n Y = Delete save \n I = Load save \n M = Kill Monster";

        TemporaryTest_Update();
    }

    public static void Reset()
    {
        //RESET
        FPS_Count = 0;
        FPS_AllRecorded = 0;
        FPS_Average = 0;
        FPS_Current_Cooldown = 0;
        FPS_Current = 0;
        FPS_Higher = 0;
        FPS_Lower = 999;
        FPS_Average_Recent = 0;
        RecentFPSObserved.Clear();
    }

    public static int FPS_Higher, FPS_Lower = 999;

    public static int FPS_Current;

    public static float FPS_Current_Cooldown, FPS_Current_CooldownMax = 0.3f;

    public static int FPS_AllRecorded, FPS_Count, FPS_Average, FPS_Average_Recent;

    public static Dictionary<float, int> RecentFPSObserved = new Dictionary<float, int>();

    public static int FPS_Higher_old, FPS_Lower_old, FPS_Average_old, FPS_Average_Recent_old;

    public static string FpsManager()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Reset();
        }

        int FPS = Mathf.FloorToInt(1 / Time.unscaledDeltaTime);

        if (FPS_Current_Cooldown <= 0)
        {
            FPS_Current_Cooldown = FPS_Current_CooldownMax;
            FPS_Current = FPS;
        }

        if (FPS > FPS_Higher)
        {
            FPS_Higher = FPS;
        }

        if (FPS < FPS_Lower)
        {
            FPS_Lower = FPS;
        }

        FPS_Count++;
        FPS_AllRecorded += FPS;

        FPS_Average = FPS_AllRecorded / FPS_Count;

        FPS_Current_Cooldown -= 1 * Time.deltaTime;

        //RecentFPS

        float AfterThatTime = Time.time - 5;

        int total = 0, count = 0;

        foreach (float t in new List<float>(RecentFPSObserved.Keys))
        {
            if (t < AfterThatTime)
            {
                RecentFPSObserved.Remove(t);
            }
            else
            {
                total += RecentFPSObserved[t];
                count++;
            }
        }

        RecentFPSObserved.Add(Time.time, FPS);

        if (count == 0)
            count = 1;

        FPS_Average_Recent = total / count;

        string normal = string.Format("FPS : {0}\n Higher : {1}\n Lower : {2}\n Avg : {3}\n Recent Avg : {4}", FPS_Current, FPS_Higher, FPS_Lower, FPS_Average, FPS_Average_Recent);

        if (FPS_Higher_old != 0)
        {
            return normal + string.Format("\n \n OLD Higher : {0}\n OLD Lower : {1}\n OLD Avg : {2}\n Recent Avg : {3}", FPS_Higher_old, FPS_Lower_old, FPS_Average_old, FPS_Average_Recent_old);
        }

        return normal;
    }

    public bool showHelpTxt;
}
