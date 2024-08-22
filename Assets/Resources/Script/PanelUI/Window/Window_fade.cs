using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Window : MonoBehaviour
{
    const float fadeSize = 125;

    public RectTransform fade_object;

    public void UpdateFade()
    {
        fade_object.transform.position = transform.position;
        fade_object.gameObject.SetActive(inFront);

        fade_object.sizeDelta = new Vector3(thisRect.rect.width + fadeSize, thisRect.rect.height + fadeSize);
    }
}
