using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEditor.PlayerSettings;

public partial class Tile : MonoBehaviour
{
    public SpriteRenderer render;

    public TileData data { get; private set; }

    public void SetData(TileData data)
    {
        this.data = data;

        render.sortingOrder = (int)data.layer;

        var pos_vector = data.posVector2;

        transform.position = new Vector3(pos_vector.x, pos_vector.y, transform.position.z);

        AnimationApparition(!data.DoScaleApparition);

        SetSprite();
    }

    public void Update()
    {
        WhenUpdate();
    }

    public virtual void WhenTheMouseEnter() { }

    public virtual void WhenTheMouseIsOver() { }

    public virtual void WhenTheMouseExit() { }

    public virtual void WhenUpdate()
    {
    }
}
