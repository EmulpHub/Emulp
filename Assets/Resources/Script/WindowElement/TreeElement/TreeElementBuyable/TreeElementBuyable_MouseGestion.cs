using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TreeElementBuyable : TreeElement
{

    public override void Cursor()
    {
        base.Cursor();

        bool result = MouseOver && (IsBuyable() || IsPurchased());

        Main_UI.ManageDontMoveCursor(gameObject, result);

        if (result) Window.SetCursorAndOffsetHand();
    }
}
