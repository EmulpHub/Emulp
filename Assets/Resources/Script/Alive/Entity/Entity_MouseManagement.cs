using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Entity : MonoBehaviour
{

    [HideInInspector]
    public bool Highlight = false;

    public float title_ApparitionSpeed;

    [HideInInspector]
    public float title_currentDistance;

    public float title_minDistance, title_maxDistance;

    [HideInInspector]
    public bool IsMouseOnEntity;

    public float lifeBar_maxDistance;

    public virtual void OnMouseIsOver()
    {
        if (Scene_Main.isMouseOverAWindow || !ClickAutorization.Autorized(this.gameObject))
            return;

        IsMouseOnEntity = true;

        title_currentDistance += title_ApparitionSpeed * Time.deltaTime;
        if (title_currentDistance >= title_maxDistance && V.game_state != V.State.fight)
        {
            title_currentDistance = title_maxDistance;
        }
        else if (title_currentDistance >= lifeBar_maxDistance && V.game_state == V.State.fight)
        {
            title_currentDistance = lifeBar_maxDistance;
        }

        EntityOnMouseOver = this;

        Thicness_Over();
    }

    public virtual void OnMouseIsDown_Left()
    {
        if (Map_PossibleToMove.CanFightClick())
        {
            Map_PossibleToMove.FightClick(CurrentPosition_string);

            SpellGestion.ResetSelectionnedSpell();
        }
    }

    public virtual void OnMouseIsDown_Right()
    {
        if (Input.GetMouseButtonDown(1) && !IsDead() && !Scene_Main.isMouseOverAWindow)
            WindowInfo.Instance.OpenOrCloseWindow(WindowInfo.type.character, this);
    }

    public virtual void OnMouseIsExit()
    {
        if (V.game_state == V.State.fight)
        {
            var CTTile = TileInfo.Instance.Get(CurrentPosition_string);

            if (EntityOrder.IsTurnOf_Player() && V.game_state_action == V.State_action.spell && CTTile != null)
                CTTile.WhenTheMouseExit();

            EntityOnMouseOver = null;
        }

        IsMouseOnEntity = false;

        Thicness_Exit();
    }


    public int outlineThicness_Max;
    public float outlineThicness_Speed;

    public Color outlineColor;

    float highlight_UpdateThicness_min = 3, highlight_UpdateThicness_max = 5, highlight_UpdateThicness_speed = 3;

    bool max = true;

    public void Thicness_Update()
    {
        if (Highlight && !IsMouseOnEntity)
        {
            if (max)
            {
                CurrentThicness = Mathf.Lerp(CurrentThicness, highlight_UpdateThicness_max, highlight_UpdateThicness_speed * Time.deltaTime);
                if (CurrentThicness >= highlight_UpdateThicness_max - 0.4f)
                {
                    CurrentThicness = highlight_UpdateThicness_max;
                    max = false;
                }
            }
            else
            {
                CurrentThicness = Mathf.Lerp(CurrentThicness, highlight_UpdateThicness_min, highlight_UpdateThicness_speed * Time.deltaTime);
                if (CurrentThicness <= highlight_UpdateThicness_min + 0.4f)
                {
                    CurrentThicness = highlight_UpdateThicness_min;
                    max = true;
                }
            }
        }
    }

    public void Thicness_Over()
    {
        CurrentThicness += outlineThicness_Speed * Time.deltaTime;

        float max = outlineThicness_Max;
        if (Highlight)
            max *= 2;

        if (CurrentThicness > max)
            CurrentThicness = max;
    }

    public void Thicness_Exit()
    {
        CurrentThicness = 0;
        if (Highlight)
        {
            CurrentThicness = highlight_UpdateThicness_min;
            max = true;
        }
    }

    public void ActiveHighlight()
    {
        Highlight = true;
    }

    public void DeactivateHighlight()
    {
        Highlight = false;
        CurrentThicness = 0;
    }
}
