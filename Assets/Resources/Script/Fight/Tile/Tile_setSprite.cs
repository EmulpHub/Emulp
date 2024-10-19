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

        bool TopRight = !containRight && !containdown;
        bool TopLeft = !containRight && !containup;
        bool BotRight = !containleft && !containdown;
        bool BotLeft = !containleft && !containup;

        render.SetRoundedCorner(TopRight, TopLeft, BotRight, BotLeft);
        UpdateSortingOrder();
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
