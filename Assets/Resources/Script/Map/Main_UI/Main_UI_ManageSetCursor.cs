using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Main_UI : MonoBehaviour
{
    public static void ManageSetCursor()
    {
        RemoveInactiveCursorChange();

        if (dontChangeCursor.Count > 0)
            return;

        var mouseOnTile = Tile_Gestion.Instance.MouseOnTile;

        if (V.game_state == V.State.fight)
        {
            if (V.game_state_action == V.State_action.spell && EntityOrder.Instance.IsTurnOf_Player())
            {
                SpellGestion.CursorMode save = SpellGestion.Get_cursorMode(SpellGestion.selectionnedSpell_list);

                if (save == SpellGestion.CursorMode.physical)
                    Window.SetCursorAndOffset(cursor_Physical, Window.CursorMode.click_cursor);
                else if (save == SpellGestion.CursorMode.magical)
                    Window.SetCursorAndOffset(cursor_magical, Window.CursorMode.click_cursor);
            }
            else if (V.game_state_action == V.State_action.movement && mouseOnTile != null && TileInfo.Instance.ExistType(Tile.Type.movement) && EntityOrder.Instance.IsTurnOf_Player())
            {
                if (mouseOnTile.pos != V.player_entity.CurrentPosition_string)
                    Window.SetCursorAndOffsetHand();
                else
                    Window.SetCursorAndOffset(cursor_normal, Window.CursorMode.click_cursor);
            }
            else
            {
                Window.SetCursorAndOffset(cursor_normal, Window.CursorMode.click_cursor);
            }
        }
        else if (V.game_state == V.State.positionning && Positionning_tile.PositioningTile_OnMouseOver.Count > 0)
        {
            bool Hand = false;

            foreach (GameObject position_tile in Positionning_tile.PositioningTile_OnMouseOver)
            {
                if (position_tile.GetComponent<Positionning_tile>().forPlayer)
                {
                    Hand = true;
                    break;
                }
            }

            if (Hand)
            {
                Window.SetCursorAndOffsetHand();
            }
            else
            {
                Window.SetCursorAndOffset(cursor_normal, Window.CursorMode.click_cursor);
            }
        }
        else
        {
            Window.SetCursorAndOffset(cursor_normal, Window.CursorMode.click_cursor);
        }
    }
}
