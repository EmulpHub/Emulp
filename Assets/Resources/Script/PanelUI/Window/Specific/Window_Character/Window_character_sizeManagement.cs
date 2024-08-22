using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Window_character : Window
{
    public void UpdateSizeDependingOfEffect()
    {
        Size_InitValue();

        Vector2 newSize = CalcSizeWindow();

        if (newSize != thisRect.sizeDelta)
        {
            //SetSize(newSize);
        }
    }

    public void Size_InitValue()
    {
        if (BaseRectScale == Vector2.zero)
            BaseRectScale = thisRect.sizeDelta;
    }

    [HideInInspector]
    public Vector2 BaseRectScale;

    public float XDistanceDependingOfEffect;

    public Vector2 CalcSizeWindow()
    {
        return BaseRectScale + new Vector2(Effect_XDistanceMax * XDistanceDependingOfEffect, 0);
    }
}
