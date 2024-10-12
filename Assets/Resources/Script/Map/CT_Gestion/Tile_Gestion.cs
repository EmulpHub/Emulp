using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Tile_Gestion : MonoBehaviour
{
    private static Tile_Gestion _instance;

    public static Tile_Gestion Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.Find("TileGestion").GetComponent<Tile_Gestion>();

            return _instance;
        }
    }

    public Transform parent;

    public GameObject prefab_Tile_Graphic, prefab_Tile_Movement, prefab_Tile_Spell;

    public List<string> selectionnedPath = new List<string>();

    public void Update()
    {
        Mouse_Detection();
    }

    public void Add_IconSpell(Tile tile)
    {
        TileIconManager.Instance.Add(tile, iconSpell, colorSpell, 0.035f, true);
    }

    public void Add_IconMovement(Tile tile, string lastPrecedent)
    {
        TileIconManager.Instance.Add_Movement(tile, lastPrecedent, iconMovement, colorMovement, 0.02f, false);
    }

    public void UpdateAllTileColor ()
    {
        foreach (Tile tile in TileInfo.Instance.GetListTile())
        {
            tile.UpdateColor();
        }
    }

    public void UpdateAllTileSprite ()
    {
        foreach(Tile tile in TileInfo.Instance.GetListTile())
        {
            tile.SetSprite();
        }
    }
}
