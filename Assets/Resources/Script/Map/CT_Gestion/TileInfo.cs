using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    private static readonly Lazy<TileInfo> lazy = new(() => new TileInfo());

    public static TileInfo Instance { get { return lazy.Value; } }

    private Dictionary<string, List<Tile>> dictionaryPosTile = new Dictionary<string, List<Tile>>();

    private List<string> listPos = new List<string>();

    private List<Tile> listTile = new List<Tile>();

    public List<string> GetListPos ()
    {
        return listPos;
    }

    public List<Tile> GetListTile()
    {
        return listTile;
    }

    public void Add(Tile tile)
    {
        if (dictionaryPosTile.ContainsKey(tile.data.pos)) {
            dictionaryPosTile[tile.data.pos].Add(tile);
        }
        else
        {
            dictionaryPosTile.Add(tile.data.pos, new List<Tile>() { tile});
        }

        listPos.Add(tile.data.pos);
        listTile.Add(tile);
    }

    public void Remove(Tile tile)
    {
        if (dictionaryPosTile.ContainsKey(tile.data.pos))
        {
            dictionaryPosTile[tile.data.pos].Remove(tile);
            if (dictionaryPosTile[tile.data.pos].Count == 0) 
                dictionaryPosTile.Remove(tile.data.pos);
        }

        listPos.Remove(tile.data.pos);
        listTile.Remove(tile);
    }

    public Tile Get(string pos,TileData.Layer layer = TileData.Layer.normal)
    {
        if (!dictionaryPosTile.ContainsKey(pos)) return null;

        return dictionaryPosTile[pos].FirstOrDefault(a => a.data.layer == layer);
    }

    public Tile GetHigherLayer(string pos)
    {
        if (!dictionaryPosTile.ContainsKey(pos)) return null;

        return dictionaryPosTile[pos].OrderByDescending(a => (int)(a.data.layer)).First();
    }

    public List<Tile> GetAll(string pos)
    {
        if (!dictionaryPosTile.ContainsKey(pos)) return null;

        return dictionaryPosTile[pos];
    }

    public bool Exist(string pos)
    {
        return dictionaryPosTile.ContainsKey(pos);
    }

    public void ListTile_Clear()
    {
        foreach (Tile tile in new List<Tile>(listTile))
        {
            Remove(tile);
            tile.Erase();
        }
    }

    public void ListTile_Clear_Except(List<string> PosToIgnore)
    {
        foreach (Tile tile in new List<Tile>(listTile))
        {
            if (PosToIgnore.Contains(tile.data.pos)) continue;

            Remove(tile);
            tile.Erase();
        }
    }
}