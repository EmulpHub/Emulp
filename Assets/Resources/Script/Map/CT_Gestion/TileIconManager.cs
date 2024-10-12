using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileIconManager : MonoBehaviour
{
    private static readonly Lazy<TileIconManager> lazy = new(() => new TileIconManager());

    public static TileIconManager Instance { get { return lazy.Value; } }

    private static GameObject _prefab;

    private static GameObject Prefab
    {
        get
        {
            if (_prefab == null)
                _prefab = Resources.Load<GameObject>("Prefab/TileIcon");

            return _prefab;
        }
    }

    private Dictionary<string, TileIcon> dicPosTileIcon = new Dictionary<string, TileIcon>();

    public TileIcon Add(Tile tile, Sprite img, Color couleur, float scale, bool preserveAspect)
    {
        string pos = tile.data.pos;

        TileIcon v = CreateTileIcon(pos, tile, img, couleur, scale, preserveAspect);

        dicPosTileIcon.Add(pos, v);

        return v;
    }

    public void Add_Movement(Tile tile, string precedentPos, Sprite img, Color couleur, float scale, bool preserveAspect)
    {
        var tileIcon = Add(tile, img, couleur, scale, preserveAspect);

        tileIcon.transform.eulerAngles = new Vector3(0, 0, F.DegreeTowardThisDirection(tile.data.pos, precedentPos));
    }

    public void Remove(string pos)
    {
        if (!dicPosTileIcon.ContainsKey(pos)) return;

        TileIcon tileIcon = dicPosTileIcon[pos];

        if (tileIcon != null)
            Destroy(tileIcon.gameObject);

        dicPosTileIcon.Remove(pos);
    }

    public void RemoveAll()
    {
        foreach (TileIcon c in new List<TileIcon>(dicPosTileIcon.Values))
        {
            Remove(c.pos);
        }
    }

    private TileIcon CreateTileIcon(string pos, Tile tile, Sprite img, Color color, float scale = 1, bool preserveAspec = false)
    {
        TileIcon script = Instantiate(Prefab, tile.transform).GetComponent<TileIcon>();

        script.SetImg(img)
            .SetPos(pos)
            .SetColor(color)
            .SetScale(scale);

        script.img.preserveAspect = preserveAspec;

        return script;
    }
}
