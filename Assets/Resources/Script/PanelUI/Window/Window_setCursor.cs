using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Window : MonoBehaviour
{
    public void UpdateCursorTexture()
    {
        if ((mouseIsOver || Manipulating) && inFront && block.Count == 0 && block_cursor.Count == 0)
        {
            Main_UI.ManageDontMoveCursor(this.gameObject, true);

            SetCursor();
        }
        else
        {
            Main_UI.ManageDontMoveCursor(this.gameObject, false);
        }
    }

    public void SetCursor()
    {
        var state = CalcState();

        if (state == state.stretching)
        {
            var stretchDir = CalcStretchDirection();

            if (stretchDir == StretchDirection.right ||
                stretchDir == StretchDirection.left)

                SetCursorAndOffset(V.cursor_right);
            else if (stretchDir == StretchDirection.up ||
                stretchDir == StretchDirection.down)

                SetCursorAndOffset(V.cursor_up);
            else if (stretchDir == StretchDirection.up_left ||
                stretchDir == StretchDirection.down_right)

                SetCursorAndOffset(V.cursor_upLeft);
            else if (stretchDir == StretchDirection.down_left ||
                stretchDir == StretchDirection.up_right)

                SetCursorAndOffset(V.cursor_upRight);
        }
        else if (state == state.moving)
        {
            SetCursorAndOffsetHand();
        }
    }

    public static void SetCursorAndOffsetHand()
    {
        SetCursorAndOffset(Main_UI.cursor_hand_yellow, new Vector2(Main_UI.cursor_hand_yellow.width / 2, 0));
    }

    public static void SetCursorAndOffset(Texture2D tx, Vector2 specificPosition)
    {
        if (tx == null)
            tx = Main_UI.cursor_normal;

        Cursor.SetCursor(tx, specificPosition, UnityEngine.CursorMode.Auto);
    }

    public static void SetCursorAndOffset(Texture2D tx)
    {
        if (tx == null)
        {
            tx = Main_UI.cursor_normal;
        }

        SetCursorAndOffset(tx, new Vector2(tx.width / 2, tx.height / 2));
    }

    public enum CursorMode { click_cursor }

    public static void SetCursorAndOffset(Texture2D tx, CursorMode cursorMode)
    {
        if (tx == null)
        {
            tx = Main_UI.cursor_normal;
        }

        Vector3 pos = new Vector2(tx.width / 2, tx.height / 2);

        if (cursorMode == CursorMode.click_cursor)
        {
            pos = Vector2.zero;
        }

        SetCursorAndOffset(tx, pos);
    }


}
