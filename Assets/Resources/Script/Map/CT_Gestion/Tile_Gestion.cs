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
                _instance = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Tile_Gestion>();

            return _instance;
        }
    }

    public void Update()
    {
        Mouse_Detection();
    }

    public void Add_IconSpell(Tile tile)
    {
        CT_TileIcon.Instance.Add(tile, iconSpell, colorSpell, 0.035f, true);
    }

    public void Add_IconMovement(Tile tile, string lastPrecedent)
    {
        CT_TileIcon.Instance.Add_Movement(tile, lastPrecedent, iconMovement, colorMovement, 0.02f, false);
    }
}
