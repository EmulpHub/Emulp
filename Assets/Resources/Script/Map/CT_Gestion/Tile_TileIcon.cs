using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CT_TileIcon : MonoBehaviour
{
    private static readonly Lazy<CT_TileIcon> lazy =
        new(() => new CT_TileIcon());

    public static CT_TileIcon Instance { get { return lazy.Value; } }

    static GameObject _prefab;

    private static GameObject Prefab
    {
        get
        {
            if (_prefab == null)
                _prefab = Resources.Load<GameObject>("Prefab/TileIcon");

            return _prefab;
        }
    }

    public TileIcon Add(Tile tile, Sprite img, Color couleur, float scale, bool preserveAspect)
    {
        string pos = tile.pos;

        TileIcon v = CreateTile(pos, tile, img, couleur, scale, preserveAspect);

        dicPosTile.Add(pos, v);

        return v;
    }

    public void Add_Movement(Tile tile, string precedentPos, Sprite img, Color couleur, float scale, bool preserveAspect)
    {
        var tileIcon = Add(tile, img, couleur, scale, preserveAspect);

        tileIcon.transform.eulerAngles = new Vector3(0, 0, F.DegreeTowardThisDirection(tile.pos, precedentPos));
    }

    public bool Contain(string pos)
    {
        return dicPosTile.ContainsKey(pos);
    }

    public void Remove(string pos)
    {
        if (dicPosTile.ContainsKey(pos))
        {
            TileIcon a = dicPosTile[pos];

            if (a != null)
                Destroy(a.gameObject);

            dicPosTile.Remove(pos);
        }
    }

    public void RemoveAll()
    {
        foreach (TileIcon c in new List<TileIcon>(dicPosTile.Values))
        {
            Remove(c.pos);
        }
    }

    private Dictionary<string, TileIcon> dicPosTile = new Dictionary<string, TileIcon>();

    private TileIcon CreateTile(string pos, Tile tile, Sprite img, Color color, float scale = 1, bool preserveAspec = false)
    {
        TileIcon icon = Instantiate(Prefab, tile.transform).GetComponent<TileIcon>();

        icon.SetImg(img)
            .SetPos(pos)
            .SetColor(color)
            .SetScale(scale);

        icon.transform.localPosition = Vector3.zero;

        if (preserveAspec)
            icon.transform.GetChild(0).GetComponent<Image>().preserveAspect = true;

        return icon;
    }
}
