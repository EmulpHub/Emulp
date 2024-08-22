using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Window : MonoBehaviour
{
    public float size_min_x, size_min_y, size_max_x, size_max_y;

    private void Stretch()
    {
        float divSizeX = thisRect.sizeDelta.x / 2 * transform.lossyScale.x, divSizeY = thisRect.sizeDelta.y / 2 * transform.lossyScale.y;

        float up = divSizeY + transform.position.y;
        float down = -divSizeY + transform.position.y;
        float right = divSizeX + transform.position.x;
        float left = -divSizeX + transform.position.x;

        var point = stretchDirection;
        var mousePos = CursorInfo.Instance.positionInWorld3;

        float mousePosX = mousePos.x;
        float mousePosY = mousePos.y;

        void strecth_right()
        {
            Strectch(mousePosX, right, false, true);
        }

        void strecth_left()
        {
            Strectch(left, mousePosX, false, false);
        }

        void strecth_up()
        {
            Strectch(mousePosY, up, true, true);
        }

        void strecth_down()
        {
            Strectch(down, mousePosY, true, false);
        }

        if (point == StretchDirection.up_right)
        {
            strecth_up();
            strecth_right();
        }
        else if (point == StretchDirection.up_left)
        {
            strecth_up();
            strecth_left();
        }
        else if (point == StretchDirection.down_left)
        {
            strecth_down();
            strecth_left();
        }
        else if (point == StretchDirection.down_right)
        {
            strecth_down();
            strecth_right();
        }

        if (point == StretchDirection.up)
            strecth_up();
        else if (point == StretchDirection.down)
            strecth_down();
        else if (point == StretchDirection.right)
            strecth_right();
        else if (point == StretchDirection.left)
            strecth_left();
    }

    private void Strectch(float axe, float axe_corner, bool IsY, bool negative)
    {
        float scale = IsY ? transform.parent.localScale.y : transform.parent.localScale.x;

        float size = (axe - axe_corner) / scale;

        float x = 0;
        float y = 0;
        if (IsY)
            y = size;
        else
            x = size;

        Vector3 finalSize = thisRect.sizeDelta + new Vector2(x, y);

        if (IsY)
        {
            if (finalSize.y < size_min_y || finalSize.y > size_max_y)
                return;
        }
        else
            if (finalSize.x < size_min_x || finalSize.x > size_max_x)
            return;

        thisRect.sizeDelta = finalSize;

        if (negative)
            thisRect.anchoredPosition = thisRect.anchoredPosition + new Vector2(x / 2, y / 2);
        else
            thisRect.anchoredPosition = thisRect.anchoredPosition - new Vector2(x / 2, y / 2);
    }

    public void SetSize(Vector2 newSize, bool moveWindow = true)
    {
        thisRect.sizeDelta = newSize;

        if (moveWindow)
            thisRect.anchoredPosition -= (newSize - thisRect.sizeDelta) / 2;
    }
}
