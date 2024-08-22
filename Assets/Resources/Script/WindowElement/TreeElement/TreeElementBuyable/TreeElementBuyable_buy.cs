using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class TreeElementBuyable : TreeElement
{
    public Color32 skillGoldEffectBuyedColor;

    public static List<int> allBuyedFragmentInstanceId = new List<int>();

    public virtual void Buy()
    {
        if (IsPurchased())
            return;

        SoundManager.PlaySound(SoundManager.list.ui_selectionSpell);

        SoundManager.PlaySound(SoundManager.list.ui_newSpell_2);

        AnimationSparkle(30, SparkleAnimationColor.yellow, this);
    }

    public virtual bool IsBuyable()
    {
        return false;

    }

    public virtual bool IsPurchased()
    {
        return true;
    }
}
