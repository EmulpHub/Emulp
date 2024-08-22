using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class TreeElementBuyable : TreeElement
{
    public GameObject pa_parent;
    public Image graphique;

    public Text pa_cost;

    [HideInInspector]
    public Sprite graphiqueSprite, graphiqueSprite_gray;

    public virtual void SetInfo(Window_skill w = null)
    {
        SetGraphiqueTexture();

        if (window != null) window = w;

        rectThis = GetComponent<RectTransform>();
    }
}
