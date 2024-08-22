using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public partial class TreeElementBuyable : TreeElement
{

    public virtual void CostTxtUpdate() { }

    public virtual void GraphiqueUpdate()
    {
        SetGraphiqueTexture();
    }

    public override void UpdateUI()
    {
        if (IsPurchased())
        {
            if (contour_white != null) contour_white.color = buy_color;
        }
    }
}
