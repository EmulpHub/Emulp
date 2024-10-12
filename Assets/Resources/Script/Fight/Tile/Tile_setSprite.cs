using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Tile : MonoBehaviour
{
    public void SetSprite()
    {
        var posTuple = F.ReadString(data.pos);

        int x = posTuple.x;
        int y = posTuple.y;

        bool containRight = SetSprite_IsContain(x + 1, y, data.ignoreAllEntity, data.listTileDependancy),
            containleft = SetSprite_IsContain(x - 1, y, data.ignoreAllEntity, data.listTileDependancy),
            containup = SetSprite_IsContain(x, y + 1, data.ignoreAllEntity, data.listTileDependancy),
            containdown = SetSprite_IsContain(x, y - 1, data.ignoreAllEntity, data.listTileDependancy);

        bool Check(bool right, bool left, bool up, bool down)
        {
            return right == containRight && left == containleft && up == containup && down == containdown;
        }

        Sprite s = null;

        if (Check(right: false, left: true, up: true, down: false)) //Up and left contain
        {
            s = Tile_Gestion.Instance.smooth_downRight;
        }
        else if (Check(right: false, left: true, up: false, down: true))  //down and left contain
        {
            s = Tile_Gestion.Instance.smooth_upRight;
        }
        else if (Check(right: true, left: false, up: false, down: true))  //down and right contain
        {
            s = Tile_Gestion.Instance.smooth_upLeft;
        }
        else if (Check(right: true, left: false, up: true, down: false))  //up and right contain
        {
            s = Tile_Gestion.Instance.smooth_downLeft;
        }
        else if (Check(right: false, left: false, up: false, down: true)) //only down contain
        {
            s = Tile_Gestion.Instance.smooth_up;
        }
        else if (Check(right: false, left: false, up: true, down: false)) //only up contain
        {
            s = Tile_Gestion.Instance.smooth_down;
        }
        else if (Check(right: true, left: false, up: false, down: false)) //only right contain
        {
            s = Tile_Gestion.Instance.smooth_left;
        }
        else if (Check(right: false, left: true, up: false, down: false)) //only left contain
        {
            s = Tile_Gestion.Instance.smooth_right;
        }
        else
        {
            s = Tile_Gestion.Instance.normal;
        }

        render.sprite = s;
    }

    public bool SetSprite_IsContain(int x, int y, bool IgnoreAllEntity, List<string> CombatTileList)
    {
        bool containTile = CombatTileList.Contains(F.ConvertToString(x, y));

        if (IgnoreAllEntity)
        {
            return containTile;
        }
        else
        {
            bool containEntity = V.player_entity.CurrentPosition_string == F.ConvertToString(x, y);

            return containTile || containEntity;
        }
    }
}
