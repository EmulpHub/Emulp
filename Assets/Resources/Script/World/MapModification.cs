using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapModification : MonoBehaviour
{
    public enum groundAboveTilemap
    {
        close_all, close_right, close_left, close_top, close_bottom,
        open_all, open_right, open_top, open_left, open_bottom,
        open_topAndRight, open_topAndLeft, open_topAndBottom,
        open_bottomAndRight, open_bottomAndLeft,
        open_rightAndLeft
    }

    public static groundAboveTilemap getWichGroundAboveIsBest(string pos)
    {
        bool containRight = WorldData.Contain(F.AdditionPos(pos, "1_0"));
        bool containLeft = WorldData.Contain(F.AdditionPos(pos, "-1_0"));
        bool containTop = WorldData.Contain(F.AdditionPos(pos, "0_1"));
        bool containBottom = WorldData.Contain(F.AdditionPos(pos, "0_-1"));

        if (containRight && containLeft && containTop && containBottom) //OPEN ALL
            return groundAboveTilemap.open_all;

        //CLOSE ONE UNIT
        if (!containRight && containLeft && containTop && containBottom) //CLOSE RIGHT
            return groundAboveTilemap.close_right;
        else if (containRight && !containLeft && containTop && containBottom) //CLOSE LEFT
            return groundAboveTilemap.close_left;
        else if (containRight && containLeft && !containTop && containBottom) //CLOSE TOP
            return groundAboveTilemap.close_top;
        else if (containRight && containLeft && containTop && !containBottom) //CLOSE BOTTOM
            return groundAboveTilemap.close_bottom;

        //OPEN ONE UNIT
        if (containRight && !containLeft && !containTop && !containBottom) //CLOSE RIGHT
            return groundAboveTilemap.open_right;
        else if (!containRight && containLeft && !containTop && !containBottom) //CLOSE LEFT
            return groundAboveTilemap.open_left;
        else if (!containRight && !containLeft && containTop && !containBottom) //CLOSE TOP
            return groundAboveTilemap.open_top;
        else if (!containRight && !containLeft && !containTop && containBottom) //CLOSE BOTTOM
            return groundAboveTilemap.open_bottom;

        if (containRight && containLeft && !containTop && !containBottom) //OPEN right and left
            return groundAboveTilemap.open_rightAndLeft;
        else if (!containRight && !containLeft && containTop && containBottom) //OPEN TOP AND BOTTOM
            return groundAboveTilemap.open_topAndBottom;
        else if (containRight && !containLeft && containTop && !containBottom) //OPEN TOP AND BOTTOM
            return groundAboveTilemap.open_topAndRight;
        else if (!containRight && containLeft && containTop && !containBottom) //OPEN TOP AND BOTTOM 
            return groundAboveTilemap.open_topAndLeft;

        if (containRight && !containLeft && !containTop && containBottom) //OPEN bot and right
            return groundAboveTilemap.open_bottomAndRight;
        else if (!containRight && containLeft && !containTop && containBottom) //OPEN bot and left
            return groundAboveTilemap.open_bottomAndLeft;

        return groundAboveTilemap.close_all;
    }

    public static List<TileBase> getWichTileBaseToRemove(string pos)
    {
        bool containRight = WorldData.Contain(F.AdditionPos(pos, "1_0"));
        bool containLeft = WorldData.Contain(F.AdditionPos(pos, "-1_0"));
        bool containTop = WorldData.Contain(F.AdditionPos(pos, "0_1"));
        bool containBottom = WorldData.Contain(F.AdditionPos(pos, "0_-1"));

        List<TileBase> tiles = new List<TileBase>();

        if (!containRight)
        {
            tiles.Add(V.Go_Right);
        }

        if (!containLeft)
        {
            tiles.Add(V.Go_Left);
        }

        if (!containTop)
        {
            tiles.Add(V.Go_Up);
        }

        if (!containBottom)
        {
            tiles.Add(V.Go_Down);
        }

        return tiles;
    }

    public static Tilemap getWallTilemap(groundAboveTilemap t)
    {
        return Resources.Load<Tilemap>("Prefab/Map/groundAboveTemplate/border/template_" + t.ToString());
    }

    public static void addBorderWall(Map target, string pos)
    {
        groundAboveTilemap t = getWichGroundAboveIsBest(pos);

        Tilemap walls = getWallTilemap(t);

        AddTileToAMap(target, walls);

        removeTileFromMap(getWichTileBaseToRemove(pos), target.Player_ReachPoint);
    }

    public static void AddTileToAMap(Map target, Tilemap toAdd)
    {

        Tilemap groundAbove = target.ground_above;

        foreach (Vector3Int Position in toAdd.cellBounds.allPositionsWithin)
        {
            if (toAdd.HasTile(Position))
            {
                TileBase b = toAdd.GetTile(Position);

                groundAbove.SetTile(Position, b);
            }
        }
    }

    public static void removeTileFromMap(List<TileBase> ToRemove, Tilemap map)
    {
        foreach (Vector3Int Position in map.cellBounds.allPositionsWithin)
        {
            if (map.HasTile(Position))
            {
                TileBase b = map.GetTile(Position);

                if (ToRemove.Contains(b))
                    map.SetTile(Position, null);
            }
        }
    }
}
