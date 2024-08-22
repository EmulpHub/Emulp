using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleStatic : MonoBehaviour
{
    public static Dictionary<string, Obstacle> list = new ();

    public static void Add(Obstacle obstacle)
    {
        if (obstacle == null) return;

        if (!list.ContainsKey(obstacle.pos))
            list.Add(obstacle.pos, obstacle);
    }

    public static void Remove(Obstacle obstacle)
    {
        if (obstacle == null) return;

        if (list.ContainsKey(obstacle.pos))
            list.Remove(obstacle.pos);
    }

    public static void Clear()
    {
        foreach (Obstacle a in new List<Obstacle>(list.Values))
        {
            a.destroyThis();
        }

        list.Clear();
    }

    public static bool Contain(string pos)
    {
        return list.ContainsKey(pos);
    }
}
