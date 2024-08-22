using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Panel_button : MonoBehaviour
{
    public WindowInfo.type link;

    public Sprite graphique;

    public Image render, bg, contour;

    public Color32 color_bg, color_bg_yellow, color_contour, color_contour_yellow;

    [HideInInspector]
    public Vector3 baseScale;

    KeyCode shortcut = KeyCode.None;

    public void Start()
    {
        baseScale = transform.localScale;

        render.sprite = graphique;

        if (link == WindowInfo.type.skill)
            shortcut = KeyCode.Z;
        else if (link == WindowInfo.type.character)
            shortcut = KeyCode.X;
        else if (link == WindowInfo.type.equipment)
            shortcut = KeyCode.C;

        thisRect = GetComponent<RectTransform>();

        if (link == WindowInfo.type.Ascension && Ascension.currentAscension == 0 &&
            !(V.administrator && V.script_Scene_Main_Administrator.permaShowAscension))
            this.gameObject.SetActive(false);
    }

    public float animationDuration = 0.5f;

    public void UpdateUI()
    {
        (bool find, Window window) WindowTuple = WindowInfo.Instance.GetWindow(link, V.player_entity);

        if (link == WindowInfo.type.skill && V.IsInMain)
        {
            float valeurY = (WindowTuple.find && WindowTuple.window.active) ? 1 : -1;

            render.transform.DOScaleY(valeurY, animationDuration);
        }
    }

    [HideInInspector]
    public bool isMouseOver_Save;

    RectTransform thisRect;

    private void Update()
    {
        UpdateUI();

        bool IsMouseOver = DetectMouse.IsMouseOnUI(thisRect) && ClickAutorization.Autorized(this.gameObject);

        if (IsMouseOver != isMouseOver_Save)
        {
            if (IsMouseOver == true)
                MouseEnterAnimation();
            else
                MouseExitAnimation();
        }

        if (ShowExclamation())
        {
            bg.color = color_bg_yellow;
            contour.color = color_contour_yellow;
            render.color = color_contour_yellow;
        }
        else
        {
            bg.color = color_bg;
            contour.color = color_contour;
            render.color = color_contour;
        }

        isMouseOver_Save = IsMouseOver;

        if (Input.GetKeyDown(shortcut))
            Open();
    }

    public bool ShowExclamation()
    {
        switch (link)
        {
            case WindowInfo.type.skill:
                if (V.IsInMain)
                    return V.player_info.NbSkillBuyableAndRemaining() > 0;
                else
                    return false;
            case WindowInfo.type.equipment:
                return Equipment_Management.NewEquipment.Count > 0;
            default:
                return false;
        }
    }

    public static GameObject currentSelectionedButton;

    public void MouseEnterAnimation()
    {
        currentSelectionedButton = gameObject;

        transform.DOKill();
        transform.localScale = new Vector3(punchScaleStrengh, punchScaleStrengh) + baseScale;
    }

    public void MouseExitAnimation()
    {
        currentSelectionedButton = null;

        transform.DOKill();
        transform.DOScale(baseScale, 0.2f);
    }

    public void Open()
    {
        if (!ClickAutorization.Autorized(this.gameObject))
            return;

        Animation_click();

        WindowInfo.Instance.OpenOrCloseWindow(link, V.player_entity);
    }


    public float punchScaleStrengh, punchScaleSpeed;

    public float clickStrenght = 0.02f;

    public void Animation_click()
    {
        transform.DOKill();
        transform.localScale = baseScale + new Vector3(punchScaleStrengh, punchScaleStrengh);
        transform.DOPunchScale(new Vector3(-clickStrenght, -clickStrenght, 0), punchScaleSpeed, 1);
    }
}
