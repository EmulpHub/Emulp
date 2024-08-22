using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// On mouse over an ui element or a collider change the cursor texture to be the one set in parameter (cursorType)
/// </summary>
public class OnMouseOver_ShowHand : MonoBehaviour
{
    public enum DetectionType { ui, collider }

    public DetectionType detectionType;

    RectTransform thisRect;

    private void Start()
    {

        if (detectionType == DetectionType.ui)
            thisRect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (detectionType == DetectionType.ui)
            mouseOver = DetectMouse.IsMouseOnUI(thisRect) && ClickAutorization.Autorized(this.gameObject);

        Main_UI.ManageDontMoveCursor(this.gameObject, mouseOver);

        if (mouseOver)
        {
            Window.SetCursorAndOffsetHand();
        }
    }

    bool mouseOver = false;

    private void OnMouseOver()
    {
        if (ClickAutorization.Autorized(this.gameObject))
        {
            mouseOver = false;
            return;
        }

        if (detectionType == DetectionType.collider)
        {
            mouseOver = true;
        }
    }

    private void OnMouseExit()
    {
        if (detectionType == DetectionType.collider)
        {
            mouseOver = false;
        }
    }
}
