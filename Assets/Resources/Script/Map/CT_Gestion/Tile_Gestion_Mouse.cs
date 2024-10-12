using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class Tile_Gestion : MonoBehaviour
{
    public Tile MouseOnTile = null;

    public string GetMousePos ()
    {
        bool Condition(Entity entity)
        {
            return entity.IsMouseOnEntity;
        }

        string mousePos = AliveEntity.Instance.GetFirstEntity(Condition)?.CurrentPosition_string;

        if (mousePos == null) mousePos = CursorInfo.Instance.position;

        return mousePos;

    }

    public void Mouse_Detection()
    {
        string mousePos = GetMousePos();

        if (TileInfo.Instance.Get(mousePos) == null || Scene_Main.isMouseOverAWindow) //In case this pos doesn't exist, the mouse is not on a tile
        {
            Mouse_Reset();
            return;
        }

        Tile newMouseTile = TileInfo.Instance.GetHigherLayer(mousePos);

        if (newMouseTile != null && !ClickAutorization.Autorized(newMouseTile.gameObject)) 
            newMouseTile = null;

        if (newMouseTile == null)
        {
            if(MouseOnTile != null)
            {
                Mouse_exit(MouseOnTile);
                MouseOnTile = null;
            }

            return;
        }

        if (MouseOnTile != newMouseTile || MouseOnTile == null)
        {
            if (MouseOnTile != null)
                Mouse_exit(MouseOnTile);

            if (newMouseTile != null)
            {
                MouseOnTile = newMouseTile;

                Mouse_enter(MouseOnTile);
            }
        }
        else if (MouseOnTile == newMouseTile)
        {
            Mouse_over(MouseOnTile);
        }
    }

    public void Mouse_enter(Tile tile)
    {
        tile.WhenTheMouseEnter();

        if (V.game_state_action == V.State_action.spell)
        {
            bool lineOfView = false;

            if (tile.data.type == TileData.Type.spell)
            {
                lineOfView = ((TileData_spell)tile.data).lineOfView;
            }

            var color = lineOfView ? Color.blue_over : Color.blue_over_noLine;

            AOE_Create(tile.data.pos, color);
        }
        else
            MouseOnTile = tile;
    }

    public void Mouse_over(Tile theTile)
    {
        theTile.WhenTheMouseIsOver();
    }

    public void Mouse_exit(Tile theTile)
    {
        theTile.WhenTheMouseExit();

        TileIconManager.Instance.Remove(theTile.data.pos);
    }

    public void Mouse_Reset()
    {
        if (ListAOE.Count > 0 && V.game_state_action == V.State_action.spell)
            AOE_EraseAll();

        TileIconManager.Instance.RemoveAll();

        if (MouseOnTile != null)
        {
            MouseOnTile.WhenTheMouseExit();
            MouseOnTile = null;
        }
    }
}
