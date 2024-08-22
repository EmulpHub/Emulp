using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_interactable_clickAutorizationOnMouseOver : MonoBehaviour
{
    [HideInInspector]
    public RectTransform rectThis;

    private void Start()
    {
        rectThis = GetComponent<RectTransform>();
    }

    [HideInInspector]
    bool RememberMouseOver;

    void Update()
    {
        bool MouseOnUi = DetectMouse.IsMouseOnUI(rectThis);

        if (MouseOnUi && MouseOnUi != RememberMouseOver)
        {
            WhenMouseEnter();
        }
        else if (MouseOnUi && MouseOnUi == RememberMouseOver)
        {
            WhenMouseOver();
        }
        else if (!MouseOnUi && RememberMouseOver)
        {
            WhenMouseExit();
        }

        RememberMouseOver = MouseOnUi;
    }

    public bool ItsToolbar;

    public void WhenMouseEnter()
    {
        if (ItsToolbar)
        {
            Map_PossibleToMove.MouseIsOnToolbar = true;
        }
        else
            ClickAutorization.Add(this.gameObject);
    }

    public void WhenMouseOver()
    {

    }

    public void WhenMouseExit()
    {
        if (ItsToolbar)
        {
            Map_PossibleToMove.MouseIsOnToolbar = false;
        }
        else
            ClickAutorization.Remove(this.gameObject);
    }
}
