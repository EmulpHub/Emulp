using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Panel_Rod : MonoBehaviour
{
    public Vector2 baseScale;

    public RectTransform rectThis;

    public GameObject knob;

    public bool normalRod;

    public float timer_max;

    float timer;

    public TreeElement child, parent;

    private void Start()
    {
        ResetTimer();

        ResetTimer();
    }

    public void ResetTimer()
    {
        timer = timer_max;
    }

    public void Update()
    {
        BuyableAnimation_Management();

        ManageColor();
    }

    public void BuyableAnimation_Management()
    {
        if (!normalRod || child.curState != TreeElement.State.buyable) return;
        
        timer -= 1 * Time.deltaTime;

        if (timer <= 0)
        {
            ResetTimer();

            CreateNewKnob();
        }
    }

    public float AnchorPosYSpeed;

    public Color32 animationKnobColor;

    public void CreateNewKnob()
    {
        GameObject g = Instantiate(knob, transform);

        RectTransform r = g.GetComponent<RectTransform>();

        float h = r.rect.height;

        r.GetComponent<Image>().color = animationKnobColor;

        r.anchoredPosition = Vector3.zero - new Vector3(0, h, 0);

        r.DOAnchorPosY(rectThis.rect.height + h, AnchorPosYSpeed);

        Destroy(g, 8);
    }

    public Image WhitePart,contour;

    public Color32 buyable,NotVisible, Visible;

    public void ManageColor()
    {
        if (child.curState == TreeElement.State.buyable)
        {
            contour.color = buyable;
            setColorWhite(buyable);
        }
        else if (child.curState == TreeElement.State.purchased )
        {
            contour.color = NotVisible;
            setColorWhite(child.buy_color);
        }
        else if (child.curState == TreeElement.State.dadNotAcquired)
        {
            contour.color = NotVisible;
            setColorWhite(NotVisible);
        }
        else
        {
            contour.color = NotVisible;
            setColorWhite(Visible);
        }
    }

    public void setColorWhite (Color32 color)
    {
        WhitePart.color = color;

        foreach(Transform child in WhitePart.transform)
        {
            child.GetComponent<Image>().color = color;
        }
    }
}
