using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public partial class F : MonoBehaviour
{
    public static (int x, int y) ReadString(string pos)
    {
        for (int i = 0; i < pos.Length; i++)
        {
            if (pos[i] == '_')
            {
                return (int.Parse(pos.Substring(0, i)), int.Parse(pos.Substring(i + 1)));
            }
        }

        print("BUG READING with pos = " + pos);
        return (999, 999);
    }
    
    public static List<string> TileAtXDistance(int maxDistance, string origine)
    {
        (int x, int y) originxy = ReadString(origine);

        int origine_x = originxy.x;
        int origine_y = originxy.y;

        List<string> Tile_toReturn = new List<string>();
        List<string> Tile_interne = new List<string>();

        if (maxDistance == 0)
        {
            Tile_toReturn.Add(ConvertToString(origine_x, origine_y));

            return Tile_toReturn;
        }

        int x = maxDistance;
        int y = 0;

        Tile_interne.Add(ConvertToString(x, y));
        Tile_toReturn.Add(ConvertToString(x + origine_x, y + origine_y));

        while (x > 0)
        {
            x--;
            y++;

            Tile_interne.Add(ConvertToString(x, y));
            Tile_toReturn.Add(ConvertToString(x + origine_x, y + origine_y));
        }

        foreach (string pos in new List<string>(Tile_interne))
        {
            (int x, int y) p1 = ReadString(pos);

            int pos_x = p1.x;
            int pos_y = p1.y;

            Tile_interne.Add(ConvertToString(-pos_x, -pos_y));
            Tile_toReturn.Add(ConvertToString(-pos_x + origine_x, -pos_y + origine_y));

            if (pos_x != 0 && pos_y != 0)
            {
                Tile_interne.Add(ConvertToString(-pos_x, pos_y));
                Tile_toReturn.Add(ConvertToString(-pos_x + origine_x, pos_y + origine_y));
                Tile_interne.Add(ConvertToString(pos_x, -pos_y));
                Tile_toReturn.Add(ConvertToString(pos_x + origine_x, -pos_y + origine_y));
            }
        }

        return Tile_toReturn;
    }

    public static List<string> TileAtXDistance(int maxDistance)
    {
        return TileAtXDistance(maxDistance, "0_0");
    }

    public static List<string> TileBetweenStartandEndDistance(int startDistance, int endDistance, string startPos)
    {
        List<string> AllTile = new List<string>();

        for (int i = startDistance; i <= endDistance; i++)
        {
            AllTile.AddRange(TileAtXDistance(i, startPos));
        }

        return AllTile;
    }

    #region Check

    public static bool IsTileExistWithNoObstacle(Vector2Int pos, bool IgnoreCamera, List<string> CustomObstacle)
    {
        if (IgnoreCamera)
        {
            return IsTileExistWithNoObstacle_Tilemap(pos, CustomObstacle);
        }
        else
        {
            Vector3 cameraPos = Camera.main.WorldToViewportPoint(ConvertToWorldVector2(pos));

            return IsTileExistWithNoObstacle_Tilemap(pos, CustomObstacle) && IsTileInTheGameArea(F.ConvertToString(pos));
        }
    }

    public static bool IsTileInTheGameArea(string pos, bool ForPlayer = true)
    {
        Vector3 cameraPos = Camera.main.WorldToViewportPoint(ConvertToWorldVector2(pos));

        float minY = 0;//ForPlayer ? V.script_Scene_Main.cam_YPosMin : V.script_Scene_Main.cam_YPosMin_Fight;
        float maxY = 0;//ForPlayer ? V.s
        float maxX = 0;//ForPlayer ? V.script_Scene_Main.cam_XPosMax : V.script_Scene_Main.cam_XPosMax_Fight;

        if (ForPlayer)
        {
            maxY = V.script_Scene_Main.cam_YPosMax;
            minY = V.script_Scene_Main.cam_YPosMin;
            maxX = V.script_Scene_Main.cam_XPosMax;
        }
        else
        {
            maxY = V.script_Scene_Main.cam_YPosMax_Fight;
            minY = V.script_Scene_Main.cam_YPosMin_Fight;
            maxX = V.script_Scene_Main.cam_XPosMax_Fight;
        }

        return F.IsBetweenTwoValues(cameraPos.y, minY, maxY) &&
            F.IsBetweenTwoValues(cameraPos.x, (1 / maxX) - 1, maxX);

    }

    public static bool IsTileExistWithNoObstacle(Vector2Int pos, bool IgnoreCamera)
    {
        return IsTileExistWithNoObstacle(pos, IgnoreCamera, new List<string>(ObstacleStatic.list.Keys));
    }

    public static bool IsTileExistWithNoObstacle(Vector2Int pos)
    {
        Vector3 cameraPos = Camera.main.WorldToViewportPoint(ConvertToWorldVector2(pos));

        return IsTileExistWithNoObstacle_Tilemap(pos)
            && F.IsBetweenTwoValues(cameraPos.y, V.script_Scene_Main.cam_YPosMin, V.script_Scene_Main.cam_YPosMax) && F.IsBetweenTwoValues(cameraPos.x, (1 / V.script_Scene_Main.cam_XPosMax) - 1, V.script_Scene_Main.cam_XPosMax);
        /*cameraPos.y >= V.script_Scene_Main.cam_YPosMin &&
        cameraPos.y <= V.script_Scene_Main.cam_YPosMax && cameraPos.x <= V.script_Scene_Main.cam_XPosMax && cameraPos.x >= (1 / V.script_Scene_Main.cam_XPosMax) - 1;*/
    }

    public static bool IsTileExistWithNoObstacle_Tilemap(Vector2Int pos, List<string> CustomObstacle)
    {
        return Main_Map.ground.HasTile(new Vector3Int(pos.x, pos.y, 0)) && !Main_Map.ground_above.HasTile(new Vector3Int(pos.x, pos.y, 0)) && !CustomObstacle.Contains(F.ConvertToString(pos));
    }

    public static bool IsTileExistWithNoObstacle_Tilemap(Vector2Int pos)
    {
        return Main_Map.ground.HasTile(new Vector3Int(pos.x, pos.y, 0)) && !Main_Map.ground_above.HasTile(new Vector3Int(pos.x, pos.y, 0)) && !ObstacleStatic.Contain(ConvertToString(pos));
    }


    #region WithEntity 

    public static bool IsTileSeenable(string pos)
    {
        Vector3 cameraCoord = Camera.main.WorldToViewportPoint(ConvertToWorldVector2(pos));

        float min = 0, max = 1;

        return F.IsBetweenTwoValues(cameraCoord.x, min, max) && F.IsBetweenTwoValues(cameraCoord.y, min, max);
    }

    public static bool IsTileApproximativelySeenable(string pos, Tilemap target)
    {
        Vector3 pos_origin = ConvertToWorldVector_withTilemap(ConvertToVector2Int(pos), target);

        Vector3 pos_right = pos_origin + Main_Map.size_x_vector3 / 2;
        Vector3 pos_left = pos_origin - Main_Map.size_x_vector3 / 2;
        Vector3 pos_up = pos_origin + Main_Map.size_y_vector3 / 2;
        Vector3 pos_down = pos_origin - Main_Map.size_y_vector3 / 2;

        return IsPosSeenable(pos_right) || IsPosSeenable(pos_left) || IsPosSeenable(pos_up) || IsPosSeenable(pos_down);
    }

    public static bool IsTileConfortablySeenable(string pos)
    {
        Vector3 cameraCoord = Camera.main.WorldToViewportPoint(ConvertToWorldVector2(pos));

        float min = 0, max = 0.95f;

        return F.IsBetweenTwoValues(cameraCoord.x, min, max) && F.IsBetweenTwoValues(cameraCoord.y, min, max);
    }

    public static bool IsTileFree(string pos, bool ignoreAllEntity = false)
    {
        return EntityByPos.TryGet(pos) == null || ignoreAllEntity;
    }

    public static bool IsTileFree(string pos, Entity ignoreEntity)
    {
        return IsTileFree(new List<Entity> { ignoreEntity }, pos);
    }

    public static bool IsTileFree(List<Entity> ignore_entity, string pos)
    {
        var entity = EntityByPos.TryGet(pos);

        return entity == null || ignore_entity.Contains(entity);
    }

    public static bool IsTileFree(int x, int y, Entity ignoreEntity)
    {
        return IsTileFree(ConvertToString(x, y), ignoreEntity);
    }

    public static bool IsTileFree(Vector2Int pos, Entity ignoreEntity)
    {
        return IsTileFree(ConvertToString(pos), ignoreEntity);
    }

    public static bool IsTileMapContainTile(string pos, Tilemap tilemap)
    {
        return tilemap.HasTile((Vector3Int)ConvertToVector2Int(pos));
    }

    #endregion

    #endregion
}
