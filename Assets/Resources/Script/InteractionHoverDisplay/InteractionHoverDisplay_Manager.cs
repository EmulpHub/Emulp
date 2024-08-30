using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionHoverDisplay_Manager : MonoBehaviour
{
    #region lazy

    private static readonly Lazy<InteractionHoverDisplay> lazy = new(() => new InteractionHoverDisplay());

    public static InteractionHoverDisplay Instance { get { return lazy.Value; } }

    #endregion

    public enum Mode { none,movement,spell }

    public InteractionHoverDisplay display;

    public Sprite icon_movement;

    public Vector2 selectionnedSpell_gap;
    
    public Mode GetMode()
    {
        bool ItsFight = V.game_state == V.State.fight;

        var mouseOnTile = CT_Gestion.Instance.MouseOnTile;

        if (ItsFight && mouseOnTile != null && mouseOnTile.type == CT.Type.movement)
            return Mode.movement;

        else if ((ItsFight && V.game_state_action == V.State_action.spell) ||
            (SpellGestion.selectionnedSpell_list != SpellGestion.List.none && SpellGestion.selectionnedSpell_list != SpellGestion.List.empty))
            return Mode.spell;

        return Mode.none;
    }

    public void Update ()
    {
        Mode mode = GetMode();

        display.gameObject.SetActive(mode != Mode.none);

        if (mode != Mode.none)
        {
            if (mode == Mode.spell)
            {
                display.SetToSpell(SpellGestion.selectionnedSpell_list);
            }
            else 
            {
                display.SetToSimpleImage(icon_movement);
            }

            Vector3 MousePos = CursorInfo.Instance.positionInWorld;

            display.transform.position = new Vector2(MousePos.x, MousePos.y) + selectionnedSpell_gap;
        }
    }
}