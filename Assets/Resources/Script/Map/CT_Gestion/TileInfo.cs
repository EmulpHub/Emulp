using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    private static readonly Lazy<TileInfo> lazy = new(() => new TileInfo());

    public static TileInfo Instance { get { return lazy.Value; } }

    private Dictionary<string, Tile> dictionaryPosTile = new Dictionary<string, Tile>();

    private Dictionary<Tile.Type, int> dictionaryNbTilePerType = new Dictionary<Tile.Type, int>();

    public List<Tile> listTile = new List<Tile>();
    public List<string> listTilePos = new List<string>();

    public TileInfo()
    {
        dictionaryNbTilePerType.Add(Tile.Type.graphic, 0);
        dictionaryNbTilePerType.Add(Tile.Type.movement, 0);
        dictionaryNbTilePerType.Add(Tile.Type.spell, 0);
    }

    public void Add(Tile tile)
    {
        dictionaryPosTile.Add(tile.pos, tile);
        listTilePos.Add(tile.pos);
        listTile.Add(tile);
        dictionaryNbTilePerType[tile.type]++;
    }

    public void Remove(Tile tile)
    {
        dictionaryPosTile.Remove(tile.pos);
        listTilePos.Remove(tile.pos);
        listTile.Remove(tile);
        dictionaryNbTilePerType[tile.type]--;
    }

    public Tile Get(string pos)
    {
        if (!dictionaryPosTile.ContainsKey(pos)) return null;

        return dictionaryPosTile[pos];
    }

    public bool Exist(string pos)
    {
        return dictionaryPosTile.ContainsKey(pos);
    }

    public bool ExistType(Tile.Type type)
    {
        return dictionaryNbTilePerType[type] > 0;
    }

    public void ResetNearSprite(string pos, bool ignoreEntity = false, List<string> CombatTileList = null)
    {
        if (CombatTileList == null) CombatTileList = listTilePos;

        void SetSprite(string pos, string toAdd = "0_0")
        {
            string posToCheck = F.AdditionPos(pos, toAdd);

            if (Exist(posToCheck)) dictionaryPosTile[posToCheck].SetSprite(ignoreEntity, CombatTileList);
        }

        SetSprite(pos);

        SetSprite(pos, "1_0");
        SetSprite(pos, "-1_0");
        SetSprite(pos, "0_1");
        SetSprite(pos, "0_-1");
    }

    public void ResetSpriteOfAllActiveTile()
    {
        foreach (string pos in listTilePos)
        {
            ResetNearSprite(pos);
        }
    }

    public void ListTile_Clear()
    {
        foreach (Tile tile in new List<Tile>(listTile))
        {
            Remove(tile);
            tile.Erase(Tile.AnimationErase_type.none);
        }
    }

    public void ListTile_Clear_Except(List<string> PosToIgnore)
    {
        foreach (Tile tile in new List<Tile>(listTile))
        {
            if (PosToIgnore.Contains(tile.pos)) continue;

            Remove(tile);
            tile.Erase(Tile.AnimationErase_type.none);
        }
    }
}