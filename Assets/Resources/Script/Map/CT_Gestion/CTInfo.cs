using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTInfo : MonoBehaviour
{
    private static readonly Lazy<CTInfo> lazy = new(() => new CTInfo());

    public static CTInfo Instance { get { return lazy.Value; } }

    private Dictionary<string, CT> dicPosCT = new Dictionary<string, CT>();

    private Dictionary<CT.Type, int> dicTypeCTNB = new Dictionary<CT.Type, int>();

    public List<CT> listCT = new List<CT>();
    public List<string> listPosCTKeys = new List<string>();

    public CTInfo()
    {
        dicTypeCTNB.Add(CT.Type.graphic, 0);
        dicTypeCTNB.Add(CT.Type.movement, 0);
        dicTypeCTNB.Add(CT.Type.spell, 0);
    }

    public void Add(CT ct)
    {
        dicPosCT.Add(ct.pos, ct);
        listPosCTKeys.Add(ct.pos);
        listCT.Add(ct);
        dicTypeCTNB[ct.type]++;
    }

    public void Remove(CT ct)
    {
        dicPosCT.Remove(ct.pos);
        listPosCTKeys.Remove(ct.pos);
        listCT.Remove(ct);
        dicTypeCTNB[ct.type]--;
    }

    public CT Get(string pos)
    {
        if (!dicPosCT.ContainsKey(pos)) return null;

        return dicPosCT[pos];
    }

    public bool Exist(string pos)
    {
        return dicPosCT.ContainsKey(pos);
    }

    public bool ExistType(CT.Type type)
    {
        return dicTypeCTNB[type] > 0;
    }

    public void ResetNearSprite(string pos, bool ignoreEntity = false, List<string> CombatTileList = null)
    {
        if (CombatTileList == null) CombatTileList = listPosCTKeys;

        void SetSprite(string pos, string toAdd = "0_0")
        {
            string posToCheck = F.AdditionPos(pos, toAdd);

            if (Exist(posToCheck)) dicPosCT[posToCheck].SetSprite(ignoreEntity, CombatTileList);
        }

        SetSprite(pos);

        SetSprite(pos, "1_0");
        SetSprite(pos, "-1_0");
        SetSprite(pos, "0_1");
        SetSprite(pos, "0_-1");
    }

    public void ResetSpriteOfAllActiveTile()
    {
        foreach (string pos in listPosCTKeys)
        {
            ResetNearSprite(pos);
        }
    }

    public void ListTile_Clear()
    {
        foreach (CT tile in new List<CT>(listCT))
        {
            Remove(tile);
            tile.Erase(CT.AnimationErase_type.none);
        }
    }

    public void ListTile_Clear_Except(List<string> PosToIgnore)
    {
        foreach (CT tile in new List<CT>(listCT))
        {
            if (PosToIgnore.Contains(tile.pos)) continue;

            Remove(tile);
            tile.Erase(CT.AnimationErase_type.none);
        }
    }
}