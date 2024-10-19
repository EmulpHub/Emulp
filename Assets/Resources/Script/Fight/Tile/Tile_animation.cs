using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class Tile : MonoBehaviour
{
    public void Erase()
    {
        Destroy(this.gameObject);
    }

    public void Animate(AnimTileData animationData)
    {
        StartCoroutine(animationData.Make(this));
    }
}
