using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toolbar_shaderAnimation : MonoBehaviour
{
    public Material material;

    public void Update()
    {
        material.SetFloat("_offset", offset);
        material.SetFloat("_noiseScale", noiseScale);
        material.SetFloat("_bright", bright);
    }

    #region offset

    private float offset ;

    private Tween tweenOffset;

    public void Offset_start(float amount,float speed)
    {
        Offset_stop();

        tweenOffset = DOVirtual.Float(offset,offset + amount, speed, (x) =>
        {
            offset = x;
        });
    }

    public void Offset_stop()
    {
        if(tweenOffset != null) tweenOffset.Kill();
    }

    #endregion

    #region noiseScale

    private float noiseScale;

    private Tween tweenNoiseScale;

    public void NoiseScale_set(float scale, float speed)
    {
        NoiseScale_stop();

        tweenNoiseScale = DOVirtual.Float(noiseScale, scale, speed, (x) =>
        {
            noiseScale = x;
        });
    }

    public void NoiseScale_stop()
    {
        if (tweenNoiseScale != null) 
            tweenNoiseScale.Kill();
    }

    #endregion

    #region bright

    private float bright;

    private Tween tweenBright;

    public void Bright_set(float from,float to, float speed)
    {
        Bright_stop();

        tweenBright = DOVirtual.Float(from, to, speed, (x) =>
        {
            bright = x;
        });
    }

    public void Bright_stop()
    {
        if (tweenBright != null) tweenBright.Kill();
    }

    #endregion
}
