using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_interactable_blockOnMouseOverIfWindowIsSelectionned : MonoBehaviour
{
    public Window window;

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

    public void WhenMouseEnter()
    {
        if (WindowInfo.Instance.IsSelectionned(window))
            window.Block_Add(gameObject);
    }

    public void WhenMouseOver()
    {
        if (WindowInfo.Instance.IsSelectionned(window))
            window.Block_Add(gameObject);
    }

    public void WhenMouseExit()
    {
        window.Block_Remove(gameObject);
    }
}
