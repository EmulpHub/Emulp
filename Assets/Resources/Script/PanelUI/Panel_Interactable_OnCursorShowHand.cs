using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Panel_Interactable_OnCursorShowHand : MonoBehaviour
{

    /// <summary>
    /// Is the mouse over this ui
    /// </summary>
    [HideInInspector]
    public bool RememberMouseOver;

    public bool IgnoreWindow;

    public bool IgnoreAutorization;

    public bool Debug_show;

    public V.State OnlyWithThisGameState, NotInThisGameState;

    public Vector3 CustomScale;

    public float WaitSec;

    [HideInInspector]
    public float EffectiveTime;

    [HideInInspector]
    public float baseScale_x, baseScale_y;

    public void Start()
    {
        EffectiveTime = Time.time + WaitSec;

        if (CustomScale != Vector3.zero)
        {
            baseScale_x = CustomScale.x;
            baseScale_y = CustomScale.y;
        }
        else
        {
            baseScale_x = transform.localScale.x;
            baseScale_y = transform.localScale.y;
        }

        if (Debug_show)
            Debug.Log("base scale x = " + transform.localScale.x + " base scale y = " + transform.localScale.y);

        if (referenceRect == null)
            referenceRect = GetComponent<RectTransform>();
    }

    public RectTransform referenceRect;

    public bool ModifyCursor = true;

    public void Update()
    {
        if (Debug_show)
            Debug.Log("Mouse on ui = " + DetectMouse.IsMouseOnUI(referenceRect) + " Window = " + (!Scene_Main.isMouseOverAWindow || IgnoreWindow) + " autorization = " + (ClickAutorization.Autorized(this.gameObject) || IgnoreAutorization));

        bool MouseOver = DetectMouse.IsMouseOnUI(referenceRect) && (IgnoreWindow || !Scene_Main.isMouseOverAWindow) &&
            (IgnoreAutorization || ClickAutorization.Autorized(this.gameObject)) &&
            (OnlyWithThisGameState == V.game_state || OnlyWithThisGameState == V.State.None) && (NotInThisGameState != V.game_state || NotInThisGameState == V.State.None);

        if (MouseOver && !RememberMouseOver)
        {
            WhenMouseStart();
        }
        else if (MouseOver && RememberMouseOver)
        {
            WhenMouseIsOver();
        }
        else if (!MouseOver && RememberMouseOver)
        {
            WhenMouseExit();
        }

        RememberMouseOver = MouseOver;
    }

    float animation_speed = 0.3f;

    public void WhenMouseStart()
    {
        Animation_start();

        if (ModifyCursor)
        {
            Window.SetCursorAndOffsetHand();

            Main_UI.ManageDontMoveCursor(this.gameObject, true);
        }
    }

    [HideInInspector]
    public bool Clicked = false;

    public AudioSource sound;

    public void WhenMouseIsOver()
    {
        bool mouse = Input.GetMouseButtonDown(0);

        if (mouse && !Clicked)
        {
            if (sound != null)
                PlaySound();
            Clicked = true;

            Animation_Click();
        }
        else if (!mouse)
        {
            Clicked = false;
        }

    }

    public void PlaySound()
    {
        AudioSource a = Instantiate(sound.gameObject).GetComponent<AudioSource>();

        SoundManager.PlaySound(a, a);
    }

    public void WhenMouseExit()
    {
        if (ModifyCursor)
        {
            Main_UI.ManageDontMoveCursor(this.gameObject, false);

        }

        Clicked = false;

        Animation_exit();
    }

    #region Anim

    public float Animation_StartMultiplier = 1.01f;

    public float PlusSizeAnimation_MouseOver = 0;

    public void Animation_start()
    {
        if (!IsItTimeToShow() || !WithAnimation)
            return;

        //anim
        transform.DOComplete();
        transform.localScale = new Vector3(baseScale_x * Animation_StartMultiplier, baseScale_y * Animation_StartMultiplier, 1) * (1 + PlusSizeAnimation_MouseOver);
    }

    public void Animation_exit()
    {
        if (!IsItTimeToShow() || !WithAnimation)
            return;

        transform.DOScale(new Vector3(baseScale_x, baseScale_y, 1), animation_speed);
    }

    public bool AllowClick;

    public float Animation_ClickSizeMultiplier = 0.05f;

    public void Animation_Click()
    {
        if (!AllowClick || !IsItTimeToShow() || !WithAnimation)
            return;

        Animation_start();

        transform.DOPunchScale(new Vector3(baseScale_x * Animation_ClickSizeMultiplier, baseScale_y * Animation_ClickSizeMultiplier, 0), animation_speed, 1);
    }

    public bool WithAnimation = true;

    public bool IsItTimeToShow()
    {
        return Time.time >= EffectiveTime;
    }

    #endregion
}
