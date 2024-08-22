using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using LayerMap;

public partial class WorldData : MonoBehaviour
{
    /// <summary>
    /// Like act in slay the spire
    /// </summary>
    public static int distance = 0; //0, 1, 2

    public static string PlayerPositionInWorld = "0_0";

    private static Dictionary<string, IMap> _world = new();

    public static Dictionary<string, IMap> World { get { return _world; } }

    public static bool Contain(string pos)
    {
        return _world.ContainsKey(pos);
    }

    public static IMap GetMapInfo(string pos)
    {
        return _world[pos];
    }

    public static void AddInWorld(IMap info)
    {
        _world.Add(info.posInWorld, info);
    }

    public static void ClearWorld()
    {
        _world.Clear();
    }
}