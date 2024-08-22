using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public static bool ShowTutorial = true;

    public void Start_button()
    {
        /*if (!EnterAName)
        {
            EnterAName = true;

            ClickAutorization.Add(1);
        }
        else
        {
            if (Character.name.Length == 0)
            {
                ChangeNamePlease.gameObject.SetActive(true);
                return;
            }*/

        //ClickAutorization.Remove(1);
        V.eventLoadingNewScene.Call();

        SceneManager.LoadScene(ContainSave ? "Main" : "Introduction");
        //}
    }

    public void Change_language(string l)
    {
        if (!ClickAutorization.Autorized(0))
            return;

        if (l == "uk")
            V.language = V.L.UK;
        else
            V.language = V.L.FR;
    }

    public Text ChangeNamePlease;

    public Text tx_welcome, tx_start, tx_start_enterYourName;

    public Image holder_uk, holder_fr;

    public Color32 activated, deactivated;

    public GameObject ActiveTutorialG;

    private bool ContainSave;

    public void Start()
    {
        Main_UI.InitalizeCursorTexture();

        MapTravelling_Start();

        //ContainSave = Save_SaveSystem.LoadSave_WithoutWarning();
    }

    public void Update()
    {

        ActiveTutorialG.gameObject.SetActive(ShowTutorial);

        Main_UI.RemoveInactiveCursorChange();

        if (Main_UI.dontChangeCursor.Count == 0)
        {
            Window.SetCursorAndOffset(Main_UI.cursor_normal, Window.CursorMode.click_cursor);
        }

        if (V.IsFr())
        {
            ChangeNamePlease.text = "Veuillez entrer votre nom s'il vous plait";

            holder_fr.color = activated;
            holder_uk.color = deactivated;

            tx_welcome.text = "Bienvenue dans";
            tx_start.text = "Commencer";
            tx_start_enterYourName.text = "C'est partis !";
        }
        else
        {
            ChangeNamePlease.text = "Please enter your name";

            holder_fr.color = deactivated;
            holder_uk.color = activated;

            tx_welcome.text = "Welcome to";
            tx_start.text = "Start";
            tx_start_enterYourName.text = "Let's go !";
        }

        MapTravelling_Update();

        Scene_Main_EchapControl.EchapManagement();
    }

    #region MapTravelling

    public float mapTraveling_Speed, mapTraveling_Ysize;

    public float acceleratingSpeed;

    public float travelDistance = 0, coveredDistance = -5;

    public GameObject mapTraveling_Parent;

    public GameObject[] maps;

    public int index = 0;

    public void MapTravelling_Start()
    {
        StartCoroutine(Accelerating_Speed());

        maps = Resources.LoadAll<GameObject>("Prefab/Map/StartMenuBackground/MapPrefab");

        InstantiateNewMap();
    }

    public IEnumerator Accelerating_Speed()
    {
        float Speed = mapTraveling_Speed;

        mapTraveling_Speed = 0;

        while (mapTraveling_Speed < Speed)
        {
            mapTraveling_Speed += acceleratingSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public void MapTravelling_Update()
    {
        float realSpeed = mapTraveling_Speed;

        if (mapTraveling_Speed > 0)
            realSpeed = mapTraveling_Speed * -1;

        mapTraveling_Parent.transform.position += new Vector3(0, realSpeed * Time.deltaTime, 0);

        travelDistance += Mathf.Abs(realSpeed) * Time.deltaTime;

        if (travelDistance - coveredDistance >= -10)
        {
            InstantiateNewMap();
        }
    }

    public void InstantiateNewMap()
    {
        if (index >= maps.Length)
            index = 0;

        GameObject g = Instantiate(maps[index], mapTraveling_Parent.transform);

        g.transform.localPosition = new Vector3(0, coveredDistance, 0);

        coveredDistance += 10;

        index++;

        Destroy(g, 120);
    }

    public void ActiveTutorial(bool enable)
    {
        V.Tutorial_Set(enable);
    }

    #endregion
}
