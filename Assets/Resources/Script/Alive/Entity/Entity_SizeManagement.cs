using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract partial class Entity : MonoBehaviour
{
    [HideInInspector]
    public Vector3 baseScale;

    public void ManageSize_Start()
    {
        baseScale = transform.localScale;
        LastCalculatedSize = baseScale.x;
    }

    [HideInInspector]
    public float LastCalculatedSize;

    public void ManageSize_update()
    {
        float size = getCurrentSize();

        transform.DOScale(new Vector3(size, size, 1), 0.5f);

        if (size != LastCalculatedSize)
        {
            ParticleLeaf(2, 1.3f);

            LastCalculatedSize = size;
        }
    }

    public float getCurrentSize()
    {
        float size = baseScale.x;

        foreach ((whenToErase mustErase, float power) v in new List<(whenToErase c, float power)>(additionalSize))
        {
            if (v.mustErase())
            {
                additionalSize.Remove(v);
            }
            else
            {
                size += v.power;
            }
        }

        return size;
    }

    public delegate bool whenToErase();

    public List<(whenToErase mustErase, float power)> additionalSize = new List<(whenToErase mustErase, float power)>();

    public void additionalSize_add(float power, whenToErase mustErase)
    {
        additionalSize.Add((mustErase, power * 0.2f));
    }
}
