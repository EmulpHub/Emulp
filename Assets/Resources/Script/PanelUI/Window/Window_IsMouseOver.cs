using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Window : MonoBehaviour
{
    [HideInInspector]
    public bool mouseIsOver
    {
        get => SetMouseIsOver();
    }
    public bool mouseIsOnWindowArea
    {
        get => SetMouseIsOnWindowArea();
    }

    public void UpdateMouse()
    {
        if (mouseIsOver)
            MouseOver();
    }

    protected virtual bool SetMouseIsOver()
    {
        if (!active) return false;

        if (Manipulating)
            return true;

        if (WindowInfo.IsMouseOverAnotherWindow(this))
            return false;

        if (!ClickAutorization.Autorized(this.gameObject))
            return false;

        if (!DetectMouse.IsMouseOnUI_AvoidUIElement(thisRect, listNotInWindow))
            return false;

        return block.Count == 0;
    }

    protected virtual bool SetMouseIsOnWindowArea()
    {
        if (!active) return false;

        if (Manipulating)
            return true;

        return DetectMouse.IsMouseOnUI(thisRect);
    }

    public void MouseOver()
    {
        if (Input.GetMouseButton(0))
            MouseDown();
        else
            SetState(state.idle);
    }

    private void MouseDown()
    {
        if (!inFront)
        {
            Selectionnate();
            return;
        }

        if (currentState == state.idle)
            SetState(CalcState());

        if (currentState == state.stretching)
            Stretch();
        else if (currentState == state.moving)
            Move();
    }
}
