using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class AscensionInfo_Holder : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public void Update()
    {
        ascensionIno_Parent_management();
    }

    public float movementSpeed;

    public RectTransform ascensionInfo_Parent;

    public void ascensionIno_Parent_management()
    {
        if (!beingDrag && timeBeforeNewSet <= Time.time)
        {
            //SetNewPosAndApply(ascensionInfo_Parent.anchoredPosition.x);
        }
        else if (beingDrag)
        {
            ascensionInfo_Parent.DOKill();
        }
    }

    [HideInInspector]
    public float timeBeforeNewSet;

    public void SetNewPosAndApply(float x)
    {
        Ascension.SetNewAscension(getAscensionDependingOfX(x));

        MakeMovement(Ascension.currentAscension);
    }

    public void MakeMovement(int ascension, bool instant = false)
    {
        if (instant)
        {
            ascensionInfo_Parent.anchoredPosition = new Vector3(-calcX(ascension), 0, 0);
        }
        else
        {
            ascensionInfo_Parent.DOKill();
            ascensionInfo_Parent.DOAnchorPos(new Vector3(-calcX(ascension), 0, 0), instant ? 0 : movementSpeed);

            timeBeforeNewSet = Time.time + movementSpeed + 0.1f;
        }
    }

    public float calcX(int ascension)
    {
        float w = 500;

        return ascension * w + ascension * 10;
    }

    public int getAscensionDependingOfX(float x)
    {
        float distance = 0;

        int closest = 0;
        float distanceMin = 10000;

        int cur = 0;

        while (cur <= Ascension.HigherAscension)
        {
            distance = Mathf.Abs(calcX(cur) - x);

            if (distance < distanceMin)
            {
                distanceMin = distance;
                closest = cur;
            }

            cur++;
        }

        return closest;
    }

    public bool beingDrag = false;

    public void OnBeginDrag(PointerEventData data)
    {
        beingDrag = true;
    }

    public void OnEndDrag(PointerEventData data)
    {
        beingDrag = false;
    }
}
