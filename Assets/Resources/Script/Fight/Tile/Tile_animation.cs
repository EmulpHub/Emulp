using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class Tile : MonoBehaviour
{
    private float mouseOver_scale_start = 0.65f, mouseOver_scale_speed = 0.2f;

    void Scale_raise(float delayBetweenStartAndEndInSec, float scaleSpeedInSec, float startScale, float endScale)
    {
        StopAllCoroutines();
        StartCoroutine(Scale_raise_Enumerator(delayBetweenStartAndEndInSec, scaleSpeedInSec, startScale, endScale));
    }

    IEnumerator Scale_raise_Enumerator(float delayBetweenStartAndEndInSec, float scaleSpeedInSec, float startScale, float endScale)
    {
        transform.DOScale(startScale, 0);
        yield return new WaitForSeconds(delayBetweenStartAndEndInSec);
        transform.DOScale(endScale, scaleSpeedInSec);
    }

    public void AnimationApparition(bool instant = false)
    {
        if (instant)
            transform.localScale = new Vector3(1, 1, 1);
        else
            Scale_raise(0, mouseOver_scale_speed, mouseOver_scale_start, 1);
    }

    public enum AnimationErase_type { normal, instant, fade }

    public void Erase(AnimationErase_type animation)
    {
        if (animation == AnimationErase_type.normal)
        {
            Scale_raise(0, data.DeathSpeed, 1, 0);

            Destroy(this.gameObject, data.DeathSpeed + 0.2f);
        }
        else if (animation == AnimationErase_type.fade)
        {
            render.DOFade(0, data.DeathSpeed);

            Destroy(this.gameObject, data.DeathSpeed + 0.2f);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
