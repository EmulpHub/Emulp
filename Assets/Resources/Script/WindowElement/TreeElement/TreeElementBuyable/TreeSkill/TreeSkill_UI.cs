using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public partial class TreeSkill : TreeElementBuyable
{
    public Image interiorContour;
    public Color32 interiorColor_normal, interiorColor_purchased;

    /// <summary>
    /// Display the info of this skill
    /// </summary>
    public override void DisplayInfo()
    {
        if (spell == SpellGestion.List.none) return;

        if (CurrentlyLocked())
        {
            Description_text.Display(V.IsFr() ? "Vérouillé" : "Locked", V.IsFr() ? "Vérouillé jusqu'au niveau " + LockedByLevel_val : "Locked until level " + LockedByLevel_val, transform.position, transform.lossyScale.x * distanceMultiplicator);

            return;
        }

        Description_text.Display(spell, transform.position, transform.lossyScale.x * distanceMultiplicator);
    }

    public Sprite Lock;

    public override void GraphiqueUpdate()
    {
        if (CurrentlyLocked())
        {
            graphique.sprite = Lock;
        }
        else
        {
            SetGraphiqueTexture();
        }

        if(curState == State.purchased)
        {
            interiorContour.color = interiorColor_purchased;
        } else
        {
            interiorContour.color = interiorColor_normal;
        }
    }

    public override void CostTxtUpdate()
    {
        bool locked = CurrentlyLocked();

        pa_parent.gameObject.SetActive(!locked && ( curState == State.buyable || curState == State.purchased));

        if (locked)
        {
            pa_cost.text = "" + LockedByLevel_val;
        }
        else
        {
            pa_cost.text = "" + pa;
        }
    }
}
