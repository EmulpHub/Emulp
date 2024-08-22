using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectMouse : MonoBehaviour
{
    public static bool IsMouseOnUI_AvoidUIElement(RectTransform target, List<RectTransform> uiElementToAvoid, bool show)
    {
        bool MouseOnThis = IsMouseOnUI(target);

        if (show)
        {
            Debug.Log("MouseOnThis = " + MouseOnThis);
        }

        if (MouseOnThis)
        {
            foreach (RectTransform rect in uiElementToAvoid)
            {
                if (rect.gameObject.activeSelf == true && IsMouseOnUI(rect))
                {
                    if (show)
                    {
                        Debug.Log("return false because of a child");
                    }

                    return false;
                }
            }

            if (show)
            {
                Debug.Log("return true mouse is on it");
            }

            return true;
        }

        if (show)
        {
            Debug.Log("return false because mouse isn't on it");
        }

        return false;
    }

    public static bool IsMouseOnUI_AvoidUIElement(RectTransform target, List<RectTransform> uiElementToAvoid)
    {
        return IsMouseOnUI_AvoidUIElement(target, uiElementToAvoid, false);
    }

    public static bool IsMouseOnUI(RectTransform target, bool Show)
    {
        if (Show)
        {
            print("cursor = " + CursorInfo.Instance.positionInWorld);
        }

        return IsPosOnUi(target, CursorInfo.Instance.positionInWorld);
    }

    public static bool IsPosOnUi(RectTransform target,/*, bool show,*/ Vector3 pos)
    {
        (Vector3 down_left, Vector3 up_right) v = Main_UI.GetBoundaries_V2(target);

        return F.IsBetweenTwoValues(pos.x, v.down_left.x, v.up_right.x) && F.IsBetweenTwoValues(pos.y, v.down_left.y, v.up_right.y);
    }

    public static bool IsMouseOnUI(RectTransform target)
    {
        return IsMouseOnUI(target, false);
    }

}
