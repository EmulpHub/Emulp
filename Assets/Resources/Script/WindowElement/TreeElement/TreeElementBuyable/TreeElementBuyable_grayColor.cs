using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TreeElementBuyable : TreeElement
{
    public void gray_update()
    {
        isGray = !IsPurchased();
    }

    bool isGray = false;

    public void SetGraphiqueTexture()
    {
        graphique.sprite = isGray ? graphiqueSprite_gray : graphiqueSprite;
    }
}
