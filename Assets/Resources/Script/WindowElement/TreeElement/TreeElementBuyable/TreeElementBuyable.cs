using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public partial class TreeElementBuyable : TreeElement
{
    public virtual bool CurrentlyLocked ()
    {
        return false;
    }

    public override void Func_update()
    {
        base.Func_update();

        GraphiqueUpdate();

        CostTxtUpdate();

        gray_update();
    }

    public override State SetState()
    {
        if (IsPurchased())
            return State.purchased;
        else if (IsBuyable())
            return State.buyable;
        else if (!DadIsPurchased())
            return State.dadNotAcquired;
        else
            return State.dadAcquired;

    }
}
