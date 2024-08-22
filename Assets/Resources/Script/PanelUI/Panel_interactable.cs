using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Panel_interactable : MonoBehaviour
{
    /// <summary>
    /// The type of behaviour panel_interactable can do
    /// </summary>
    public enum Type
    {
        dontMove, block, cursor_handOnMouseOver, block_onMouseOver, detect_onMouseOver,
        block_onMouseOver_cursor, setOnTopWhenClicked, detect_onMouseOver_AndBlock, cursor_handOnMouseOver_ignoreWindow
    }

    /// <summary>
    /// The behavior this script should have
    /// </summary>
    public Type type;

    /// <summary>
    /// The window attached to this script
    /// </summary>
    public Window window;

    /// <summary>
    /// Is the mouse over this ui
    /// </summary>
    public bool MouseOver;

    /// <summary>
    /// Is it currently being drag
    /// </summary>
    public bool drag = false;

    RectTransform thisRect;

    private void Start()
    {
        thisRect = GetComponent<RectTransform>();
    }

    public void Update()
    {
        if (type == Type.block || type == Type.block_onMouseOver || type == Type.block_onMouseOver_cursor || type == Type.setOnTopWhenClicked)
        {
            bool AWindowIsAlreadyUsed = WindowInfo.IsMouseOverAnotherWindow(window);

            if (type != Type.setOnTopWhenClicked && !AWindowIsAlreadyUsed)
            {
                AWindowIsAlreadyUsed = !window.inFront;
            }

            if (DetectMouse.IsMouseOnUI(thisRect) && !AWindowIsAlreadyUsed)
            {
                if (type == Type.block || type == Type.setOnTopWhenClicked)
                {
                    if (!Input.GetMouseButton(0))
                    {
                        drag = false;
                        return;
                    }

                    drag = true;

                    if (type == Type.block)
                    {
                        window.Block_Add(gameObject);
                    }

                    if (!WindowInfo.Instance.IsSelectionned(window))
                    {
                        window.Selectionnate();
                        return;
                    }
                }
                else if (type == Type.block_onMouseOver || type == Type.block_onMouseOver_cursor)
                {
                    window.Block_Add(gameObject);
                }
                else if (type == Type.block_onMouseOver_cursor)
                {
                    window.Block_Cursor_Add(gameObject);
                }
            }
            else
            {
                if (type == Type.block_onMouseOver || type == Type.block)
                {
                    window.Block_Remove(gameObject);
                }
                else
                {
                    window.Block_Cursor_Remove(gameObject);
                }
            }
        }
        else if (type == Type.cursor_handOnMouseOver || type == Type.cursor_handOnMouseOver_ignoreWindow)
        {
            bool AWindowIsAlreadyUsed = WindowInfo.IsMouseOverAnotherWindow(window);

            if (DetectMouse.IsMouseOnUI(thisRect) && (!AWindowIsAlreadyUsed || type == Type.cursor_handOnMouseOver_ignoreWindow))
            {

                Window.SetCursorAndOffsetHand();
            }
        }
        else if (type == Type.dontMove)
        {
            bool MouseIsOver = false;

            Window_skill ws = gameObject.GetComponent<Window_skill>();

            if (ws != null)
            {
                MouseIsOver = ws.background_isMouseOver;

                PlayerMoveAutorization.Instance.AddOrRemove(this.gameObject, MouseIsOver);

                return;
            }

            Window ws_2 = gameObject.GetComponent<Window>();

            if (ws_2 != null)
            {
                MouseIsOver = ws_2.mouseIsOver;

                PlayerMoveAutorization.Instance.AddOrRemove(this.gameObject, MouseIsOver);

                return;
            }

            TreeElementBuyable ws_3 = gameObject.GetComponent<TreeElementBuyable>();

            if (ws_3 != null)
            {
                MouseIsOver = ws_3.MouseOver;

                PlayerMoveAutorization.Instance.AddOrRemove(this.gameObject, MouseIsOver);

                return;
            }
        }
        else if (type == Type.detect_onMouseOver || type == Type.detect_onMouseOver_AndBlock)
        {
            MouseOver = DetectMouse.IsMouseOnUI(thisRect) && !WindowInfo.IsMouseOverAnotherWindow(window);

            if (type == Type.detect_onMouseOver_AndBlock)
                PlayerMoveAutorization.Instance.AddOrRemove(this.gameObject, MouseOver);
        }
    }
}
