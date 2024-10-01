using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public partial class F : MonoBehaviour
{
    /// <summary>
    /// Read the x and y value of a string
    /// </summary>
    /// <param name="pos">The string we want to read</param>
    /// <returns></returns>
    public static (int x, int y) ReadString_NotOptimized(string pos)
    {
        string x = "", y = "";

        bool calculatingX = true;

        for (int i = 0; i < pos.Length; i++)
        {
            char p = pos[i];

            if (p != '_')
            {
                if (calculatingX)
                {
                    x += p;
                }
                else
                {
                    y += p;
                }
            }
            else
            {
                calculatingX = false;
            }
        }

        if (pos != "")
        {
            //convert string into int
            return (int.Parse(x), int.Parse(y));
        }
        else
        {
            print("BUG READING");
            return (999, 999);
        }
    }

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

    /// <summary>
    /// Get the x value of a string pos (Recomended only if you want x)
    /// </summary>
    /// <param name="pos">The string we want to read</param>
    /// <returns></returns>
    public static int ReadString_x(string pos)
    {
        return ReadString(pos).x;
    }

    /// <summary>
    /// Get the y value of a string pos (Recomended only if you want x)
    /// </summary>
    /// <param name="pos">The string we want to read</param>
    /// <returns></returns>
    public static int ReadString_y(string pos)
    {
        return ReadString(pos).y;
    }

    /// <summary>
    /// Get a list of all tile in a defined distance adding origine pos
    /// </summary>
    /// <param name="maxDistance">The distance where the tile must be from origine</param>
    /// <param name="origine">The origine pos where we calculate the Tile</param>
    /// <returns></returns>
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

    /// <summary>
    /// Get a list of all tile in a defined distance (relative)
    /// </summary>
    /// <param name="maxDistance">The distance where the tile must be from origine</param>
    /// <returns></returns>
    public static List<string> TileAtXDistance(int maxDistance)
    {
        return TileAtXDistance(maxDistance, "0_0");
    }

    /// <summary>
    /// Get a list of all tile in a specific distance btweeen start and end adding startPos (not relative)
    /// </summary>
    /// <param name="startDistance">The min distance allowed</param>
    /// <param name="endDistance">The maximum distance allowed</param>
    /// <param name="startPos">The starting pos</param>
    /// <returns></returns>
    public static List<string> TileBetweenStartandEndDistance(int startDistance, int endDistance, string startPos)
    {
        List<string> AllTile = new List<string>();

        for (int i = startDistance; i <= endDistance; i++)
        {
            AllTile.AddRange(TileAtXDistance(i, startPos));
        }

        return AllTile;
    }

    /// <summary>
    /// Get a list of all tile in a specific distance start and end (relative)
    /// </summary>
    /// <param name="startDistance">The min distance allowed</param>
    /// <param name="endDistance">The maximum distance allowed</param>
    /// <returns></returns>
    public static List<string> TileBetweenStartandEndDistance_relative(int startDistance, int endDistance)
    {
        return TileBetweenStartandEndDistance(startDistance, endDistance, "0_0");
    }


    #region Check

    /// <summary>
    /// Check if a tile exist and there is no natural obstacle and ignoreCamera depending of "IgnoreCamera"
    /// </summary>
    /// <param name="pos">The targeted position</param>
    /// <param name="IgnoreCamera">If we need to ignoreCamera or not</param>
    /// <returns></returns>
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

    /// <summary>
    /// Check if a tile exist and there is no natural obstacle and if it's in a good range from camera
    /// </summary>
    /// <param name="pos">The targeted position</param>
    /// <returns></returns>
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

    /// <summary>
    /// Check if a tile is walkable with no obastacle and no entity and in camera range (depending of IgnoreCamera parameter)
    /// </summary>
    /// <param name="pos">The targeted position</param>
    /// <param name="IgnoreCamera">If we need to ignoreCamera or not</param>
    /// <returns></returns>
    public static bool IsTileWalkable(string pos, bool IgnoreCamera = false)
    {
        Vector2Int pos_grid = ConvertToVector2Int(pos);

        return IsTileExistWithNoObstacle(pos_grid, IgnoreCamera) && IsTileFree(ignoreEntity, pos);
    }

    public static bool IsTileWalkable(string pos, bool IgnoreCamera, List<string> CustomObstacle)
    {
        Vector2Int pos_grid = ConvertToVector2Int(pos);

        return IsTileExistWithNoObstacle(pos_grid, IgnoreCamera, CustomObstacle) && IsTileFree(pos, null);
    }

    public static bool IsTileWalkable(string pos, bool IgnoreCamera, List<string> CustomObstacle, List<Entity> IgnoreEntity)
    {
        Vector2Int pos_grid = ConvertToVector2Int(pos);

        return IsTileExistWithNoObstacle(pos_grid, IgnoreCamera, CustomObstacle) && IsTileFree(IgnoreEntity, pos);
    }

    public static bool IsTileWalkable_IgnoreAllEntity(string pos)
    {
        Vector2Int pos_grid = ConvertToVector2Int(pos);

        return IsTileExistWithNoObstacle(pos_grid, false) && IsTileFree(pos, true);
    }

    /// <summary>
    /// Check if a tile is walkable with no obastacle and no entity and in camera range (depending of IgnoreCamera parameter)
    /// </summary>
    /// <param name="x">The x pos of the targeted position</param>
    /// <param name="y">The y pos of the targeted position</param>
    /// <param name="IgnoreCamera">If we need to ignoreCamera or not</param>
    /// <returns></returns>
    public static bool IsTileWalkable(int x, int y, bool IgnoreCamera)
    {
        return IsTileWalkable(ConvertToString(x, y), IgnoreCamera);
    }

    /// <summary>
    /// Check if a tile is walkable with no obstacle and no entity and in the camera range
    /// </summary>
    /// <param name="x">The x pos of the targeted position</param>
    /// <param name="y">The y pos of the targeted position</param>
    /// <returns></returns>
    public static bool IsTileWalkable(int x, int y)
    {
        return IsTileWalkable(ConvertToString(x, y), false);
    }

    /// <summary>
    /// Check if a tile is walkable with no obastacle and no entity and in camera range (depending of IgnoreCamera parameter)
    /// </summary>
    /// <param name="pos">The targeted pos</param>
    /// <param name="IgnoreCamera">If we ignoreCamera or not</param>
    /// <returns></returns>
    public static bool IsTileWalkable(Vector2 pos, bool IgnoreCamera)
    {
        return IsTileWalkable(ConvertToStringDependingOfGrid(pos), IgnoreCamera);
    }

    /// <summary>
    /// Check if a tile is walkable with no obstacle and no entity and in camera range
    /// </summary>
    /// <param name="pos">The targeted pos</param>
    /// <returns></returns>
    public static bool IsTileWalkable(Vector2 pos)
    {
        return IsTileWalkable(ConvertToStringDependingOfGrid(pos), false);
    }

    /// <summary>
    /// Check if a tile is walkable with no obstacle and no entity and in camera range (depending of IgnoreCamera parameter)
    /// </summary>
    /// <param name="pos">The targeted pos</param>
    /// <param name="IgnoreCamera"></param>
    /// <returns></returns>
    public static bool IsTileWalkable(Vector2Int pos, bool IgnoreCamera)
    {
        return IsTileWalkable(ConvertToString(pos), IgnoreCamera);
    }

    /// <summary>
    /// Check if a tile is walkable with no obstacle and no entity and in camera range
    /// </summary>
    /// <param name="pos">The targeted pos</param>
    /// <returns></returns>
    public static bool IsTileWalkable(Vector2Int pos)
    {
        return IsTileWalkable(ConvertToString(pos), false);
    }

    /// <summary>
    /// Check if a tile is walkable with no obstacle and no entity with specific tilemap ground and ground_Above
    /// </summary>
    /// <param name="x">The x value of the targeted pos</param>
    /// <param name="y">The y value of the targeted pos</param>
    /// <param name="ground">The ground tilemap</param>
    ///  <param name="ground_Above">The ground_above tilemap</param>
    /// <returns></returns>
    public static bool IsTileWalkable(int x, int y, Tilemap ground, Tilemap ground_Above)
    {
        Vector2Int pos_grid = ConvertToVector2Int(ConvertToString(x, y));

        return ground.HasTile(new Vector3Int(pos_grid.x, pos_grid.y, 0)) && !ground_Above.HasTile(new Vector3Int(pos_grid.x, pos_grid.y, 0));
    }

    #region WithEntity 

    /// <summary>
    /// Check if a tile is walkable with no obstacle and not a specific entity with ignoreCamera parameter
    /// </summary>
    /// <param name="pos">The targeted pos</param>
    /// <param name="IgnoreCamera">If we need to ignore Camera check or not</param>
    /// <returns></returns>
    public static bool IsTileWalkable(string pos, Entity Ignorate_entity, bool IgnoreCamera, List<string> CustomObstacle)
    {
        Vector2Int pos_grid = ConvertToVector2Int(pos);

        return IsTileExistWithNoObstacle(pos_grid, IgnoreCamera, CustomObstacle) && IsTileFree(pos, Ignorate_entity);
    }

    public static bool IsTileWalkable(string pos, Entity Ignorate_entity, bool IgnoreCamera)
    {
        return IsTileWalkable(pos, Ignorate_entity, IgnoreCamera, new List<string>(ObstacleStatic.list.Keys));
    }

    /// <summary>
    /// Check if a tile is walkable with no obstacle and not a specific entity with ignoreCamera parameter
    /// </summary>
    /// <param name="pos">The targeted pos</param>
    /// <param name="Ignorate_entity">Ignored this entity</param>
    /// <returns></returns>
    public static bool IsTileWalkable(string pos, Entity Ignorate_entity)
    {
        return IsTileWalkable(pos, Ignorate_entity, false);
    }

    /// <summary>
    /// Check if a tile is walkable with no obstacle and while ignoring a list of entity with ignoreCamera parameter
    /// </summary>
    /// <param name="pos">The targeted pos</param>
    /// <param name="Ignorate_entity">A list of all entity we must ignore</param>
    /// <param name="IgnoreCamera">If we need to ignore Camera check or not</param>
    /// <returns></returns>
    public static bool IsTileWalkable(string pos, List<Entity> Ignorate_entity, bool IgnoreCamera)
    {
        Vector2Int pos_grid = ConvertToVector2Int(pos);

        var entity = EntityByPos.TryGet(pos);

        if (entity != null && !Ignorate_entity.Contains(entity))
            return false;

        return IsTileExistWithNoObstacle(pos_grid, IgnoreCamera);
    }


    /// <summary>
    /// Check if a tile is walkable with no obstacle and while ignoring a list of entity
    /// </summary>
    /// <param name="pos">The targeted pos</param>
    /// <param name="Ignorate_entity">A list of all entity we must ignore</param>
    /// <returns></returns>
    public static bool IsTileWalkable(string pos, List<Entity> Ignorate_entity)
    {
        return IsTileWalkable(pos, Ignorate_entity, false);
    }

    /// <summary>
    /// Is this tile in pos is seenable
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static bool IsTileSeenable(string pos)
    {
        Vector3 cameraCoord = Camera.main.WorldToViewportPoint(ConvertToWorldVector2(pos));

        float min = 0, max = 1;

        return F.IsBetweenTwoValues(cameraCoord.x, min, max) && F.IsBetweenTwoValues(cameraCoord.y, min, max);
    }

    /// <summary>
    /// If all the corner of an tile element fit in the camera view
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static bool IsTileApproximativelySeenable(string pos)
    {
        Vector3 pos_origin = ConvertToWorldVector2(pos);

        Vector3 pos_right = pos_origin + Main_Map.size_x_vector3 / 2;
        Vector3 pos_left = pos_origin - Main_Map.size_x_vector3 / 2;
        Vector3 pos_up = pos_origin + Main_Map.size_y_vector3 / 2;
        Vector3 pos_down = pos_origin - Main_Map.size_y_vector3 / 2;

        return IsPosSeenable(pos_right) || IsPosSeenable(pos_left) || IsPosSeenable(pos_up) || IsPosSeenable(pos_down);
    }

    /// <summary>
    /// If all the corner of an tile element fit in the camera view depending of the tilemap
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static bool IsTileApproximativelySeenable(string pos, Tilemap target)
    {
        Vector3 pos_origin = ConvertToWorldVector_withTilemap(ConvertToVector2Int(pos), target);

        Vector3 pos_right = pos_origin + Main_Map.size_x_vector3 / 2;
        Vector3 pos_left = pos_origin - Main_Map.size_x_vector3 / 2;
        Vector3 pos_up = pos_origin + Main_Map.size_y_vector3 / 2;
        Vector3 pos_down = pos_origin - Main_Map.size_y_vector3 / 2;

        return IsPosSeenable(pos_right) || IsPosSeenable(pos_left) || IsPosSeenable(pos_up) || IsPosSeenable(pos_down);
    }

    /// <summary>
    /// If the tile is in a position that is no too close of the edges of the camera
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static bool IsTileConfortablySeenable(string pos)
    {
        Vector3 cameraCoord = Camera.main.WorldToViewportPoint(ConvertToWorldVector2(pos));

        float min = 0, max = 0.95f;

        return F.IsBetweenTwoValues(cameraCoord.x, min, max) && F.IsBetweenTwoValues(cameraCoord.y, min, max);
    }

    /// <summary>
    /// If the tile is seenable and walkable
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static bool IsTileSeenableAndWalkable(string pos)
    {
        return IsTileSeenable(pos) && IsTileWalkable(pos, false);
    }

    /// <summary>
    /// If the tile is confortably seenable and walkable
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static bool IsTileConfortablySeenableAndWalkable(string pos)
    {
        return IsTileConfortablySeenable(pos) && IsTileWalkable(pos, false);
    }

    public static bool IsTileFree(string pos, bool ignoreAllEntity = false)
    {
        return EntityByPos.TryGet(pos) == null || ignoreAllEntity;
    }

    /// <summary>
    /// Check if a tile is empty (ignoring one entity)
    /// </summary>
    /// <param name="pos">The targeted pos</param>
    /// <param name="ignoreEntity">Ignore this entity when checking</param>
    /// <returns></returns>
    public static bool IsTileFree(string pos, Entity ignoreEntity)
    {
        return IsTileFree(new List<Entity> { ignoreEntity }, pos);
    }

    /// <summary>
    /// Check if a tile is empty (ignoring multiple entity)
    /// </summary>
    /// <param name="pos">The targeted pos</param>
    /// <param name="ignore_entity">Ignore this list of entity</param>
    /// <returns></returns>
    public static bool IsTileFree(List<Entity> ignore_entity, string pos)
    {
        var entity = EntityByPos.TryGet(pos);

        return entity == null || ignore_entity.Contains(entity);
    }

    /// <summary>
    /// Check if a tile is empty (ignoring one entity)
    /// </summary>
    /// <param name="x">The x value of targeted pos</param>
    /// <param name="y">The y value of targeted pos</param>
    /// <param name="ignoreEntity">The ignore entity</param>
    /// <returns></returns>
    public static bool IsTileFree(int x, int y, Entity ignoreEntity)
    {
        return IsTileFree(ConvertToString(x, y), ignoreEntity);
    }

    /// <summary>
    /// Check if a tile is empty (ignoring one entity)
    /// </summary>
    /// <param name="pos">The targeted pos</param>
    /// <param name="ignoreEntity">The entity we want to ignore</param>
    /// <returns></returns>
    public static bool IsTileFree(Vector2Int pos, Entity ignoreEntity)
    {
        return IsTileFree(ConvertToString(pos), ignoreEntity);
    }

    /// <summary>
    /// Is a tilemap Contain a tile at pos
    /// </summary>
    /// <param name="pos">The targeted pos</param>
    /// <param name="tilemap">The targeted tilemap</param>
    /// <returns></returns>
    public static bool IsTileMapContainTile(string pos, Tilemap tilemap)
    {
        return tilemap.HasTile((Vector3Int)ConvertToVector2Int(pos));
    }

    public static (Dictionary<int, List<string>> allTile_TriedByDistance, List<string> allTile, List<int> allDistanceSorted) GetAllTileReachableByEntity(int distance, Entity e)
    {
        string entityStartPos = e.CurrentPosition_string;

        Dictionary<int, List<string>> allTile_TriedByDistance = new Dictionary<int, List<string>>();

        for (int i = 1; i <= distance; i++)
        {
            allTile_TriedByDistance.Add(i, new List<string>());
        }

        List<int> distanceSorted = new List<int>();

        List<string> allTile = new List<string>();

        List<string> CheckedTile = new List<string>();

        List<string> ToCalculateNearPos = new List<string>() { e.CurrentPosition_string };

        List<string> neighbor = new List<string> { "0_1", "0_-1", "1_0", "-1_0" };

        void VerifyAndAdd(string pos)
        {
            if (F.DistanceBetweenTwoPos(pos, entityStartPos) <= distance && F.IsTileWalkable(pos) && !CheckedTile.Contains(pos) && IsTileInTheGameArea(pos, e.IsPlayer()))
            {
                ToCalculateNearPos.Add(pos);
            }
        }

        void AddTileInResult(string pos, int dis)
        {
            if (allTile.Contains(pos) || pos == entityStartPos)
                return;

            allTile.Add(pos);

            allTile_TriedByDistance[dis].Add(pos);

            if (!distanceSorted.Contains(dis))
            {
                distanceSorted.Add(dis);
            }
        }

        int max = 0;

        while (ToCalculateNearPos.Count > 0 && max < 1000)
        {
            string origine = ToCalculateNearPos[0];

            foreach (string s in neighbor)
            {
                string newPos = F.AdditionPos(s, origine);

                VerifyAndAdd(newPos);
            }

            AddTileInResult(origine, F.DistanceBetweenTwoPos(origine, entityStartPos));

            CheckedTile.Add(origine);

            ToCalculateNearPos.RemoveAt(0);

            max++;
            if (max == 1000)
            {
                print("MAX RETURNED");
            }
        }

        distanceSorted.Sort();

        return (allTile_TriedByDistance, allTile, distanceSorted);
    }

    public static List<Entity> GetAllEntityNearTile (string tile,int distance)
    {
        List<Entity> listEntity = new List<Entity>();

        void Traveler(Entity entity)
        {
            if (F.DistanceBetweenTwoPos(entity.CurrentPosition_string, tile) <= distance)
                listEntity.Add(entity);
        }

        AliveEntity.Instance.TravelEntity(Traveler);

        return listEntity;
    }


    public static List<Monster> GetAllMonsterNearTile(string tile, int distance)
    {
        List<Monster> listMonster = new List<Monster>();

        bool Condition(Monster e)
        {
            if (F.DistanceBetweenTwoPos(e.CurrentPosition_string, tile) <= distance)
                return true;

            return false;
        }

        return listMonster;
    }

    #endregion

    #endregion
}
