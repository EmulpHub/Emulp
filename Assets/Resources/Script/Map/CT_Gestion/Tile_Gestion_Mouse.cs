using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class Tile_Gestion : MonoBehaviour
{
    public Tile MouseOnTile = null;

    public void Mouse_Detection()
    {
        bool Condition (Entity entity)
        {
            return entity.IsMouseOnEntity;
        }

        string mousePos = AliveEntity.Instance.GetFirstEntity(Condition)?.CurrentPosition_string;

        if (mousePos == null) mousePos = CursorInfo.Instance.position;

        if (!Scene_Main.tilePos.Contains(mousePos) || Scene_Main.isMouseOverAWindow) //In case this pos doesn't exist, the mouse is not on a tile
        {
            Mouse_Reset();
            return;
        }

        Tile mouseTile = TileInfo.Instance.Get(mousePos);

        if (mouseTile != null && !ClickAutorization.Autorized(mouseTile.gameObject)) mouseTile = null;

        if (MouseOnTile != mouseTile || MouseOnTile == null || mouseTile == null)
        {
            if (MouseOnTile != null)
                Mouse_exit(MouseOnTile);

            if (mouseTile != null)
            {
                MouseOnTile = mouseTile;

                Mouse_enter(MouseOnTile);
            }
        }
        else if (MouseOnTile == mouseTile)
        {
            Mouse_over(MouseOnTile);
        }
    }

    public void Mouse_enter(Tile theTile)
    {
        theTile.WhenTheMouseEnter();

        if (V.game_state_action == V.State_action.spell)
            SetListAOE(theTile.pos, !Scene_Main.tilePos_withLineOfView.Contains(theTile.pos));
    }

    public void Mouse_over(Tile theTile)
    {
        theTile.WhenTheMouseIsOver();
    }

    public void Mouse_exit(Tile theTile)
    {
        theTile.WhenTheMouseExit();

        CT_TileIcon.Instance.Remove(theTile.pos);


    }

    public void Mouse_Reset()
    {
        if (ListAOE.Count > 0 && V.game_state_action == V.State_action.spell)
            AOE_EraseAll();

        CT_TileIcon.Instance.RemoveAll();

        if (MouseOnTile != null)
        {
            MouseOnTile.WhenTheMouseExit();
            MouseOnTile = null;
        }
    }

}
