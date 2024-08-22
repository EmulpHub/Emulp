using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMoveAutorization : MonoBehaviour
{
    private static readonly Lazy<PlayerMoveAutorization> lazy =
           new(() => new PlayerMoveAutorization());

    public static PlayerMoveAutorization Instance { get { return lazy.Value; } }

    private List<GameObject> listDontMove = new List<GameObject>();

    public void Add(GameObject toAdd)
    {
        if (listDontMove.Contains(toAdd)) return;

        listDontMove.Add(toAdd);
    }

    public void Remove(GameObject toAdd)
    {
        if (!listDontMove.Contains(toAdd)) return;

        listDontMove.Remove(toAdd);
    }

    public void AddOrRemove(GameObject toAdd, bool add)
    {
        if (add) Add(toAdd);
        else Remove(toAdd);
    }

    public void Clear()
    {
        listDontMove.Clear();
    }

    public bool Can()
    {
        return listDontMove.Count == 0;
    }
}
