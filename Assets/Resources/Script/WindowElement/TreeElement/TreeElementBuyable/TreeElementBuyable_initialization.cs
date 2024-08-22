using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TreeElementBuyable : TreeElement
{
    public override void Initialization()
    {
        base.Initialization();

        SetInfo(window);

        sparkle = window.sparkle;

    }

    public override void SetPosition()
    {
        if (!V.IsInMain)
            return;

        base.SetPosition();
    }
}
