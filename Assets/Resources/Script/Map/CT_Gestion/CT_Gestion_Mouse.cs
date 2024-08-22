using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class CT_Gestion : MonoBehaviour
{
    public CT MouseOnTile = null;

    public void Mouse_Detection()
    {
        string mousePos = AliveEntity.list.FirstOrDefault(a => a.IsMouseOnEntity)?.CurrentPosition_string;

        if (mousePos == null) mousePos = CursorInfo.Instance.position;

        if (!Scene_Main.tilePos.Contains(mousePos) || Scene_Main.aWindowIsUsed) //In case this pos doesn't exist, the mouse is not on a tile
        {
            Mouse_Reset();
            return;
        }

        CT mouseTile = CTInfo.Instance.Get(mousePos);

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

    public void Mouse_enter(CT theTile)
    {
        theTile.WhenTheMouseEnter();

        if (V.game_state_action == V.State_action.spell)
            SetListAOE(theTile.pos, !Scene_Main.tilePos_withLineOfView.Contains(theTile.pos));
    }

    public void Mouse_over(CT theTile)
    {
        theTile.WhenTheMouseIsOver();
    }

    public void Mouse_exit(CT theTile)
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
