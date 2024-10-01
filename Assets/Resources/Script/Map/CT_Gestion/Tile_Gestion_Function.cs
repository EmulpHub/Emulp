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

    public void Erase_WithDelay_All(Tile.AnimationErase_type animation, float time)
    {
        foreach (string pos in TileInfo.Instance.listTilePos)
        {
            StartCoroutine(CoErase_WithDelay(pos, animation, time));
        }
    }

    public IEnumerator CoErase_WithDelay(string pos, Tile.AnimationErase_type animation, float time)
    {
        Tile tile = TileInfo.Instance.Get(pos);

        if (tile == null)
            yield break;

        tile.transform.position -= new Vector3(0, 0, -1);

        TileInfo.Instance.Remove(tile);

        yield return new WaitForSeconds(time);

        tile.Erase(animation);
    }
}
