using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class Tile : MonoBehaviour
{
    public static float mouseOver_scale_start = 0.65f, mouseOver_scale_speed = 0.2f;

    void Scale_raise(float delayInSec, float speedInSec, float start, float endValue)
    {
        StopAllCoroutines();
        StartCoroutine(Scale_raise_Enumerator(delayInSec, speedInSec, start, endValue));
    }

    IEnumerator Scale_raise_Enumerator(float delayInSec, float speedInSec, float start, float endValue)
    {
        transform.DOScale(start, 0);
        yield return new WaitForSeconds(delayInSec);
        transform.DOScale(endValue, speedInSec);
    }

    public void AnimationApparition(bool instant = false)
    {
        if (instant)
            transform.localScale = new Vector3(1, 1, 1);
        else
            Scale_raise(0, mouseOver_scale_speed, mouseOver_scale_start, 1);
    }

    public void AnimationApparition_fast()
    {
        Scale_raise(0, mouseOver_scale_speed * 0.6f, mouseOver_scale_start - 0.2f, 1);
    }

    public void AnimationErase()
    {
        Scale_raise(0, DeathSpeed, 1, 0);
    }
}
