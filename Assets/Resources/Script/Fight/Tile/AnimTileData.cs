using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class AnimTileData
{
    public AnimTileData NextAnimation { get; private set; } 

    public float speed { get; private set; }
    public float delay { get; private set; }
    protected float delayBeforeDestroy { get; set; }

    public bool DestroyWhenFinish { get; private set; }

    public AnimTileData(float speed)
    {
        this.speed = speed;
    }

    public AnimTileData SetDestroyWhenFinish(float delayBeforeDestroy)
    {
        DestroyWhenFinish = true;
        this.delayBeforeDestroy = delayBeforeDestroy;
        return this;
    }

    public AnimTileData SetDelay(float value)
    {
        delay = value;
        return this;
    }

    public AnimTileData SetNextAnimation(AnimTileData animTileData) 
    {
        NextAnimation = animTileData;  
        return this;
    }

    public IEnumerator Make (Tile tile)
    {
        if(delay > 0)
            yield return new WaitForSeconds(delay);

        tile.StartCoroutine(MakeEnumerator(tile));

        yield return new WaitUntil(() => AnimFinished);

        if (DestroyWhenFinish)
        {
            yield return new WaitForSeconds(delayBeforeDestroy);

            tile.Erase();
        }
        else if(NextAnimation is not null)
        {
            tile.StartCoroutine(NextAnimation.Make(tile));
        }
    }

    protected bool AnimFinished = false;

    protected virtual IEnumerator MakeEnumerator(Tile tile)
    {
        AnimFinished = true;
        yield return null;
    }
}

public class AnimTileData_Scale : AnimTileData
{
    public float scaleStart { get; private set; }
    public float scaleEnd { get; private set; }

    public AnimTileData_Scale(float scaleStart, float scaleEnd, float speed) : base(speed)
    {
        this.scaleStart = scaleStart;
        this.scaleEnd = scaleEnd;
    }

    protected override IEnumerator MakeEnumerator(Tile tile)
    {
        tile.transform.DOKill();
        tile.transform.localScale = new Vector3(scaleStart, scaleStart, 1);
        tile.transform.DOScale(scaleEnd, speed);

        yield return new WaitForSeconds(speed);

        AnimFinished = true;
    }
}

