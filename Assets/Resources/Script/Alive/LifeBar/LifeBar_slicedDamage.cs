using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LifeBar_slicedDamage : MonoBehaviour
{
    public RectTransform thisRect;
    public Image image, rightEdge;
    public float dmgAnimation_fadeSpeed, dmgAnimation_moveSpeed;

    [HideInInspector]
    public float lifeBeforeDmg,dmg;

    public void Appear ()
    {
        Sequence animationSequence = DOTween.Sequence();

        animationSequence.Append(thisRect.DOAnchorPosY(0, dmgAnimation_moveSpeed / 3));
        animationSequence.Join(image.DOFade(1, dmgAnimation_fadeSpeed / 3));
        animationSequence.Join(rightEdge.DOFade(1, dmgAnimation_fadeSpeed / 3));

        animationSequence.Play();
    }

    public void Destroy ()
    {
        Sequence animationSequence = DOTween.Sequence();

        animationSequence.Append(thisRect.DOAnchorPosY(-20, dmgAnimation_moveSpeed));
        animationSequence.Join(image.DOFade(0, dmgAnimation_fadeSpeed));
        animationSequence.Join(rightEdge.DOFade(0, dmgAnimation_fadeSpeed / 3));

        animationSequence.Play();

        Destroy(this.gameObject, 3);
    }
}
