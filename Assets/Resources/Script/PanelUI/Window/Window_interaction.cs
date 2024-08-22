using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public abstract partial class Window : MonoBehaviour
{
    public virtual void Open()
    {
        SoundManager.Sound_OpenWindow();

        Selectionnate();

        Parent_canvas.gameObject.SetActive(true);
    }

    public virtual void Selectionnate()
    {
        if (WindowInfo.Instance.listActiveWindow.Contains(this))
            WindowInfo.Instance.listActiveWindow.Remove(this);

        WindowInfo.Instance.listActiveWindow.Add(this);

        WindowInfo.Instance.currentSelectionateWindow = this;

    }

    [HideInInspector]
    public bool DontAllowClickOnCloseButton = false;

    public EventHandlerNoArg event_Close = new EventHandlerNoArg(false);

    public virtual void Close(bool force = false)
    {
        if ((!ClickAutorization.Autorized(this.gameObject) || DontAllowClickOnCloseButton) && !force)
            return;

        event_Close.Call();

        SoundManager.Sound_CloseWindow();

        Deselectionnate();

        Parent_canvas.gameObject.SetActive(false);

        Main_UI.Display_EraseAllType();

        SetCursorAndOffset(null, Window.CursorMode.click_cursor);
    }

    public virtual void Destroy()
    {
        WindowInfo.Instance.RemoveWindow(this);

        WindowInfo.Instance.listActiveWindow.Remove(this);

        Destroy(Parent_canvas.gameObject);
    }

    public virtual void Deselectionnate()
    {
        PlayerMoveAutorization.Instance.Remove(gameObject);

        Main_UI.ManageDontMoveCursor(gameObject, false);

        block.Clear();
        block_cursor.Clear();

        SetState(state.idle);

        if (WindowInfo.Instance.currentSelectionateWindow == this)
            WindowInfo.Instance.currentSelectionateWindow = null;

    }

}
