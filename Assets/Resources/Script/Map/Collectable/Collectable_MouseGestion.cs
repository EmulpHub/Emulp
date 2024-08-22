using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract partial class Collectable : MonoBehaviour
{
    [HideInInspector]
    public bool RememberMouseOver;

    public void Mouse_Management()
    {
        bool MouseIsOver = DetectMouse.IsMouseOnUI(img.rectTransform) && !Scene_Main.aWindowIsUsed && V.game_state == V.State.passive;

        if (MouseIsOver && !RememberMouseOver)
        {
            MouseEnter();
        }
        else if (MouseIsOver && RememberMouseOver)
        {
            MouseOver();
        }
        else if (!MouseIsOver && RememberMouseOver)
        {
            MouseExit();
        }

        RememberMouseOver = MouseIsOver;
    }

    [HideInInspector]
    public float outline_thicness;

    public float outline_speed, outline_max;

    public void MouseEnter()
    {
        PlayerMoveAutorization.Instance.Add(gameObject);

        ShowTitle();
    }

    public void MouseOver()
    {
        if (Main_UI.ui_displayTitle_Current == null)
            ShowTitle();

        if (V.game_state != V.State.passive)
            outline_thicness = 0;
        else
        {
            outline_thicness += outline_speed;

            if (outline_thicness >= outline_max)
                outline_thicness = outline_max;
        }

        if (Input.GetMouseButton(0) && ClickAutorization.Autorized(this.gameObject) && !Clicked && V.game_state == V.State.passive)
            OnClick();
    }

    public void MouseExit()
    {
        PlayerMoveAutorization.Instance.Remove(gameObject);

        Clicked = false;
        outline_thicness = 0;

        Main_UI.Display_Title_Erase();
    }
}
