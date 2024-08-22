using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MapTransitionAnimation : MonoBehaviour
{
    public static void Next ()
    {
        Sequence theSequence = DOTween.Sequence();

        theSequence.Append(V.mapTransitionScreen.DOFade(1, 0.05f));

        theSequence.Append(V.mapTransitionScreen.DOFade(0, 1.5f));

        theSequence.Play();
    }
}
