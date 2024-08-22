using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WindowSkillElement : MonoBehaviour
{
    [HideInInspector]
    public Window_skill window;

    [HideInInspector]
    public RectTransform rectThis;

    [HideInInspector]
    public Vector3 baseScale;

    public RectTransform box_detection;

    public virtual void SetWindow()
    {
        (bool find, Window w) v = WindowInfo.Instance.GetWindow(WindowInfo.type.skill);

        if (!v.find)
            throw new System.Exception("The window don't exist = InitToTheWindowPanelSkilk");

        window = (Window_skill)v.w;
    }

    public bool AutorizeToClick()
    {
        return !window.background_drag && !window.Manipulating;
    }

    public void Start()
    {
        func_start();
    }

    public virtual void func_start()
    {
        Initialization();
    }

    public virtual void Func_update() { }

    public virtual void Initialization()
    {
        rectThis = GetComponent<RectTransform>();

        baseScale = transform.localScale;

        SetWindow();
    }

    public void Update()
    {
        Gestion_Mouseover();

        Func_update();
    }

    public virtual bool IsMouseOverThis()
    {
        bool result = !window.Manipulating
            && window.background_isMouseOver
            && ClickAutorization.Autorized(this.gameObject)
            && (window.mouseIsOver || (window.highLightedSkill == this || ignoreHighlightedSkill));

        if (!result) return false;

        return DetectMouse.IsMouseOnUI(box_detection);
    }

    public bool ignoreHighlightedSkill = false;

    public virtual void Window_Interaction_MouseOver_Start()
    {
        if (ignoreHighlightedSkill) return;

        window.ElementStartMouseOver(this);
    }

    public virtual void Window_Interaction_MouseOver_End()
    {
        if (ignoreHighlightedSkill) return;

        window.ElementEndMouseOver(this);
    }

    public virtual void Window_Interaction_MouseOver_Update()
    {
        bool canMouseOver = window.mouseIsOver || (window.highLightedSkill == this) || ignoreHighlightedSkill;

        if (!canMouseOver) return;

        if (Input.GetMouseButtonDown(0) && AutorizeToClick() && !V.IsFight())
        {
            Window_Interaction_Click();
        }
    }

    public virtual void Window_Interaction_Click()
    {
        window.Selectionnate();
    }

    [HideInInspector]
    public bool MouseOver;

    public void Gestion_Mouseover()
    {
        bool newMouseOver = IsMouseOverThis();

        if (newMouseOver != MouseOver)
        {
            if (newMouseOver)
                Window_Interaction_MouseOver_Start();
            else
                Window_Interaction_MouseOver_End();
        }
        else if (newMouseOver)
            Window_Interaction_MouseOver_Update();

        MouseOver = newMouseOver;
    }

    public virtual void DisplayInfo()
    {

    }

    public virtual bool DisplayInfo_isShowed()
    {
        return false;
    }
}
