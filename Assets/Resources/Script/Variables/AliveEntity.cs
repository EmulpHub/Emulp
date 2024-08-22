using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveEntity : MonoBehaviour
{
    /// <summary>
    /// A list of all Entity that are alive (not neccessary in fight)
    /// </summary>
    public static List<Entity> list = new List<Entity>();

    public static List<Monster> listMonster
    {
        get
        {
            List<Monster> l = new List<Monster>();

            foreach(Entity m in list)
            {
                if(m is Monster mons)
                    l.Add(mons);
            }

            return l;
        }
    }

    public static List<string> ListPosition()
    {
        List<string> positionListe = new();

        foreach (Entity entity in list)
            positionListe.Add(entity.CurrentPosition_string);

        return positionListe;
    }

    public static void Clear()
    {
        list.Clear();
    }

    /// <summary>
    /// Remove a specific alive entity
    /// </summary>
    /// <param name="remove">The entity we want to remove</param>
    public static void Remove(Entity remove)
    {
        list.Remove(remove);
    }

    public static void Add(Entity entity)
    {
        if (list.Contains(entity)) return;

        list.Add(entity);
    }

    /// <summary>
    /// return null if didn't find
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static Entity GetEntityByPos(string pos)
    {
        foreach (Entity e in list)
        {
            if (e.CurrentPosition_string == pos) return e;
        }

        return null;
    }
}
