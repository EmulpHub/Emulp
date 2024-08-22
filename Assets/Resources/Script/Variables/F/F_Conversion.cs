using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public partial class F : MonoBehaviour
{
    /// <summary>
    /// Convert x and y coordinate into pos_string
    /// </summary>
    public static string ConvertToString(int x, int y)
    {
        return x + "_" + y;
    }

    /// <summary>
    /// Convert Vector2Int coordinate into pos_string
    /// </summary>
    public static string ConvertToString(Vector2Int pos)
    {
        return pos.x + "_" + pos.y;
    }

    public static string ConvertToString(Vector3Int pos)
    {
        return pos.x + "_" + pos.y;
    }

    /// <summary>
    /// Convert Vector2 coordinate into pos_string
    /// </summary>
    public static string ConvertToStringDependingOfGrid(Vector2 pos)
    {
        return ConvertToString(ConvertToGridVector(pos));
    }

    /// <summary>
    /// Convert Vector3 coordinate into pos_string
    /// </summary>
    public static string ConvertToStringDependingOfGrid(Vector3 pos)
    {
        return ConvertToStringDependingOfGrid(new Vector2(pos.x, pos.y));
    }

    /// <summary>
    /// Convert string into Vector2
    /// </summary>
    /// <param name="pos">The wanted pos</param>
    /// <returns></returns>
    public static Vector2 ConvertToVector2(string pos)
    {
        (int x, int y) p1 = ReadString(pos);

        return new Vector2(p1.x, p1.y);
    }

    /// <summary>
    /// Convert string to world vector2
    /// </summary>
    /// <param name="pos">The wanted pos</param>
    /// <returns></returns>
    public static Vector2 ConvertToWorldVector2(string pos)
    {
        Vector2Int Position = ConvertToVector2Int(pos);

        return Main_Map.ground.CellToWorld(new Vector3Int(Position.x, Position.y, 0)) + new Vector3(0, Main_Map.size_y / 2, 0);
    }

    /// <summary>
    /// Convert Vector2Int into worldVector in vector2
    /// </summary>
    /// <param name="pos">The wanted pos</param>
    /// <returns></returns>
    public static Vector2 ConvertToWorldVector2(Vector2Int pos)
    {
        return ConvertToWorldVector2(ConvertToString(pos));
    }

    /// <summary>
    /// Convert string to Vector2Int
    /// </summary>
    /// <param name="pos">The wanted pos</param>
    /// <returns></returns>
    public static Vector2Int ConvertToVector2Int(string pos)
    {
        (int x, int y) p1 = ReadString(pos);

        return new Vector2Int(p1.x, p1.y);
    }

    /// <summary>
    /// Convert Vector2 into Vector2Int
    /// </summary>
    /// <param name="Position">The wanted pos</param>
    /// <returns></returns>
    public static Vector2Int ConvertToVector2Int(Vector2 Position)
    {
        //Set the int position (convert to Vector3Int)
        return new Vector2Int((int)Position.x, (int)Position.y);
    }

    /// <summary>
    /// Convert Vector2 into Grid position
    /// </summary>
    /// <param name="Position">The wanted pos</param>
    /// <returns></returns>
    public static Vector2Int ConvertToGridVector(Vector2 Position)
    {
        return (Vector2Int)Main_Map.ground.WorldToCell(Position);
    }

    /// <summary>
    /// Convert vector2 into gridposition depending of tilemap
    /// </summary>
    /// <param name="Position">The wanted pos</param>
    /// <param name="tilemap">The wanted tilemap where we want to check</param>
    /// <returns></returns>
    public static Vector2Int ConvertToGridVector_withTilemap(Vector2 Position, Tilemap tilemap)
    {
        return (Vector2Int)tilemap.WorldToCell(Position);
    }

    /// <summary>
    /// Convert Vector2Int into Vector3 world vector
    /// </summary>
    /// <param name="Position">The wanted pos</param>
    /// <param name="tilemap">The wanted tilemap where we want to check</param>
    /// <returns></returns>
    public static Vector3 ConvertToWorldVector_withTilemap(Vector2Int Position, Tilemap tilemap)
    {
        return tilemap.CellToWorld(new Vector3Int(Position.x, Position.y, 0)) + new Vector3(0, Main_Map.size_y / 2, 0);
    }
}
