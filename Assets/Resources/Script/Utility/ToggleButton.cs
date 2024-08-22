using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    #region Technical

    public string title_fr, title_uk;

    public GameObject checkBox;

    public Text txt;

    public delegate bool condition();

    public condition Condition;

    public delegate void click();

    public click Click;

    private void Start()
    {
        Initialize();

        checkBox.gameObject.SetActive(Condition());
    }

    void Update()
    {
        if (V.IsFr())
            txt.text = title_fr;
        else
        {
            txt.text = title_uk;
        }

        checkBox.gameObject.SetActive(Condition());

        ManageMouse();
    }

    public RectTransform detectionRect;
    bool MouseOver_save;

    public bool IgnoreClickAutorization;

    void ManageMouse()
    {
        bool MouseIsOver = DetectMouse.IsMouseOnUI(detectionRect);

        if (MouseIsOver && !MouseOver_save)
        {
            MouseEnter();
        }
        else if (MouseIsOver && MouseOver_save)
        {
            MouseOver();
        }
        else if (!MouseIsOver && MouseOver_save)
        {
            MouseExit();
        }

        MouseOver_save = MouseIsOver;
    }

    void MouseEnter()
    {

    }

    void MouseExit()
    {

    }

    void MouseOver()
    {

        if (Input.GetMouseButtonDown(0) && (ClickAutorization.Autorized(this.gameObject) || IgnoreClickAutorization))
        {
            Click();
        }
    }

    #endregion

    #region Uses

    public enum type { activateTutorial, mute, fullscreen }

    public type Type;

    public void Initialize()
    {
        if (Type == type.activateTutorial)
        {
            Condition = delegate
            {
                return V.Tutorial_Get();

            };

            Click = delegate
            {
                V.Tutorial_Set(!V.Tutorial_Get());
            };
        }
        else if (Type == type.mute)
        {
            Condition = delegate
            {
                return SoundManager.MuteSound;

            };

            Click = delegate
            {

                SoundManager.MuteSound = !SoundManager.MuteSound;
                if (!SoundManager.MuteSound)
                {
                    SoundManager.StopAllSound();
                }
            };
        }
        else if (Type == type.fullscreen)
        {
            Condition = delegate
            {
                return Screen.fullScreen;

            };

            Click = delegate
            {

                Screen.fullScreen = !Screen.fullScreen;
            };
        }

    }

    #endregion
}
