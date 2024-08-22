using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public partial class Window_skill : Window
{
    public Image blackScreen;

    public GameObject spell_main, spell_nortice;

    public override void Awake()
    {
        base.Awake();

        base.type = WindowInfo.type.skill;

        blackScreen.gameObject.SetActive(true);

        if (blackScreen.color.a > 0)
        {
            Invoke("FadeBlackScreen", 0.05f);
        }

        bool inMain = SceneManager.GetActiveScene().name == "Main";

        spell_main.gameObject.SetActive(inMain);
        spell_nortice.gameObject.SetActive(!inMain);
    }

    public void FadeBlackScreen()
    {
        blackScreen.DOFade(0, 1);
    }

    public GameObject rod_prefab, rod_turning_prefab;

    public Panel_skill_talent panel_skill_talent;

    public GameObject sparkle;

    public GameObject sparkle_parent;

    public Transform groupOfSpell;

    public Transform rodParent;

    public RectTransform backGroundParent_rectTransform;

    private void Start()
    {
        backGroundParent_rectTransform = backGroundParent.GetComponent<RectTransform>();

        BaseAnchoredPosition = backGroundParent_rectTransform.anchoredPosition;

        TalentG_pst = TalentG.GetComponent<Panel_skill_talent>();

        if (V.IsInMain)
            RandomizeSpell();
    }

    public void SetBackgroundPosition(Vector2 newPos)
    {
        backGroundParent_rectTransform.anchoredPosition = newPos;
    }

    [HideInInspector]
    public Window window { get { return this; } }

    public float scrollStrenght;

    public WindowSkillElement highLightedSkill;

    public RectTransform background_space;

    public Transform skillAssignationParent;


    public GameObject TalentG;

    [HideInInspector]
    public Panel_skill_talent TalentG_pst;

    public static bool ShowTalent = false;

    [HideInInspector]
    public bool ShowTalent_Remember = false;

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.U) && V.administrator)
        {
            BuyRecursively(groupOfSpell);
        }

        background_isMouseOver = DetectMouse.IsMouseOnUI_AvoidUIElement(background_space, new List<RectTransform>());

        ScaleGestion();
        MovementAndScaleGestion_Update();

        if (highLightedSkill != null && !highLightedSkill.MouseOver)
        {
            highLightedSkill = null;
        }

        bool AWindowIsAlreadyUsed = WindowInfo.IsMouseOverAnotherWindow(window);

        if (((background_isMouseOver || background_drag)
            && SpellGestion.selectionnedSpell_list == SpellGestion.List.empty
            && !window.Manipulating && highLightedSkill == null && !AWindowIsAlreadyUsed)
            || IsDraging)
        {
            WhenMouseIsOver();
        }
        else
        {
            background_drag = false;
        }
    }

    public override void Close(bool ignoreAutorization = false)
    {
        base.Close(ignoreAutorization);

        TalentG_pst.talentHolderOnMouse.Clear();

        TalentG_pst.talentListHolder_Disactive();
    }
}

