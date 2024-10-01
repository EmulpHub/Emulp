using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Combat_EndOfTurn : MonoBehaviour
{
    public Image EndOfTurn;

    public Color32 normal, green;

    RectTransform rectThis;

    public void Pressed()
    {
        if (Scene_Main.isMouseOverAWindow || !ClickAutorization.Autorized(this.gameObject))
        {
            return;
        }

        SoundManager.PlaySound(SoundManager.list.ui_buttonPressed);

        if (EntityOrder.IsTurnOf_Player() && V.game_state == V.State.fight)
        {
            MouseClick();

            Action_nextTurn.Add(V.player_entity);
        }
        else if (V.game_state == V.State.positionning)
        {
            MouseClick();
            Scene_Main.EndOfPositioning_StartCombat();
        }
    }

    private void Start()
    {
        Renderer = this.gameObject.GetComponent<Image>();

        rectThis = GetComponent<RectTransform>();
    }

    [HideInInspector]
    public Image Renderer;

    public float DelayMax;

    [HideInInspector]
    public float Delay;

    public bool MouseIsOver;

    public GameObject subHolder;

    public void Update()
    {
        Update_ui();

        if (V.game_state == V.State.fight && EntityOrder.IsTurnOf_Player())
        {
            if ((!SpellGestion.IsASpellLaunchable() && V.player_info.PM <= 0) && !ActionManager.Instance.Contain(Action.Type.nextTurn))
            {
                if (Delay <= 0)
                {
                    ChangeColor(false);
                }
                else
                {
                    Delay -= 1 * Time.deltaTime;
                    ChangeColor(true);
                }
            }
            else
            {
                Delay = 0;
                ChangeColor(true);
            }
        }
        else if (V.game_state == V.State.positionning)
        {
            Delay = 0;
            ChangeColor(false);
        }
        else
        {
            Delay = 0;
            ChangeColor(true);
        }

        bool NextMouseIsOver = DetectMouse.IsMouseOnUI(rectThis) && !Scene_Main.isMouseOverAWindow;

        if (NextMouseIsOver != MouseIsOver)
        {
            if (NextMouseIsOver)
            {
                MouseEnter();
            }
            else
            {
                MouseExit();
            }
        }

        MouseIsOver = NextMouseIsOver;

        bool ShowHand = MouseIsOver && (EntityOrder.IsTurnOf_Player() || V.game_state == V.State.positionning);

        Main_UI.ManageDontMoveCursor(gameObject, ShowHand);

        if (ShowHand)
            Window.SetCursorAndOffsetHand();

        if (MouseIsOver)
        {
            if (!mouseOver)
            {
                MouseEnter();
            }
        }
    }

    public Text EndOfTurn_txt;

    public void Update_ui()
    {
        if (V.game_state == V.State.positionning)
        {
            if (V.IsFr())
            {
                EndOfTurn_txt.text = "COMMENCER";
            }
            else
            {
                EndOfTurn_txt.text = "START";
            }
        }
        else
        {
            if (V.IsFr())
            {
                EndOfTurn_txt.text = "FIN DU TOUR";
            }
            else
            {
                EndOfTurn_txt.text = "END TURN";
            }
        }
    }

    public void MouseClick()
    {
    }

    [HideInInspector]
    public bool mouseOver = false;

    public void MouseEnter()
    {
        mouseOver = true;
    }

    public void MouseExit()
    {
        mouseOver = false;
    }


    /// <summary>
    /// Change the sprite of the button ot be obligated or not
    /// </summary>
    /// <param name="Normal"></param>
    public void ChangeColor(bool Normal)
    {
        if (Normal)
        {
            Renderer.color = normal;
        }
        else
        {
            Renderer.color = green;

        }
    }
}
