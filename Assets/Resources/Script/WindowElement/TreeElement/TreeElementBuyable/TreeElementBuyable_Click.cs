using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TreeElementBuyable : TreeElement
{
    public override void Window_Interaction_Click()
    {
        base.Window_Interaction_Click();

        if (CurrentlyLocked()) return;

        if (IsBuyable()) Buy();
        else if (IsPurchased()) ClickWhenPurchased();
    }

    public virtual void ClickWhenPurchased ()
    {

    }
}
