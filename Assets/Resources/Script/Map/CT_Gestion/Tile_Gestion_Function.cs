using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Tile_Gestion : MonoBehaviour
{
    public void Erase(string pos, Tile.AnimationErase_type animation)
    {
        Tile ct = TileInfo.Instance.Get(pos);

        ct.Erase(animation);

        TileInfo.Instance.Remove(ct);
    }
}
