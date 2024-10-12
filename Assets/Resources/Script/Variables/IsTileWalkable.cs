using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public partial class F : MonoBehaviour
{
    public static bool IsTileWalkable(string pos, bool IgnoreCamera = false, List<string> listObstacle = null, List<Entity> listEntityToIgnore = null)
    {
        Vector2Int pos_grid = ConvertToVector2Int(pos);

        if (listObstacle == null)
            listObstacle = new List<string>();

        if (listEntityToIgnore == null)
            listEntityToIgnore = new List<Entity>();

        return IsTileExistWithNoObstacle(pos_grid, IgnoreCamera, listObstacle) && IsTileFree(listEntityToIgnore, pos);
    }

    public static bool IsTileWalkable_IgnoreAllEntity(string pos)
    {
        Vector2Int pos_grid = ConvertToVector2Int(pos);

        return IsTileExistWithNoObstacle(pos_grid, false) && IsTileFree(pos, true);
    }

    public static bool IsTileWalkable(int x, int y, bool IgnoreCamera = false)
    {
        return IsTileWalkable(ConvertToString(x, y), IgnoreCamera);
    }

    public static bool IsTileWalkable(Vector2 pos, bool IgnoreCamera = false)
    {
        return IsTileWalkable(ConvertToStringDependingOfGrid(pos), false);
    }

    public static bool IsTileWalkable(Vector2Int pos, bool IgnoreCamera = false)
    {
        return IsTileWalkable(ConvertToString(pos), IgnoreCamera);
    }

    public static bool IsTileWalkable(string pos, List<Entity> Ignorate_entity)
    {
        return IsTileWalkable(pos, Ignorate_entity, false);
    }
    public static bool IsTileWalkable(int x, int y, Tilemap ground, Tilemap ground_Above)
    {
        Vector2Int pos_grid = ConvertToVector2Int(ConvertToString(x, y));

        return ground.HasTile(new Vector3Int(pos_grid.x, pos_grid.y, 0)) && !ground_Above.HasTile(new Vector3Int(pos_grid.x, pos_grid.y, 0));
    }
    public static bool IsTileWalkable(string pos, Entity Ignorate_entity, bool IgnoreCamera, List<string> CustomObstacle)
    {
        Vector2Int pos_grid = ConvertToVector2Int(pos);

        return IsTileExistWithNoObstacle(pos_grid, IgnoreCamera, CustomObstacle) && IsTileFree(pos, Ignorate_entity);
    }

    public static bool IsTileWalkable(string pos, Entity Ignorate_entity, bool IgnoreCamera = false)
    {
        return IsTileWalkable(pos, Ignorate_entity, IgnoreCamera, new List<string>(ObstacleStatic.list.Keys));
    }

    public static bool IsTileWalkable(string pos, List<Entity> Ignorate_entity, bool IgnoreCamera)
    {
        Vector2Int pos_grid = ConvertToVector2Int(pos);

        var entity = EntityByPos.TryGet(pos);

        if (entity != null && !Ignorate_entity.Contains(entity))
            return false;

        return IsTileExistWithNoObstacle(pos_grid, IgnoreCamera);
    }
}
