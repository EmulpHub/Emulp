using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeNorticeFragment : TreeElementBuyable
{

    private int _id = -1;

    public int Id
    {
        get
        {
            if (_id == -1)
            {
                _id = TreeNortice.NextID;
                TreeNortice.NextID++;
            }

            return _id;
        }
    }
    public override bool IsBuyable()
    {
        return (Ascension.nortice >= norticeCost) && (DadIsPurchased() || ChildIsPurchased());
    }

    public override bool IsPurchased()
    {

        return allBuyedFragmentInstanceId.Contains(Id);
    }

    public override void Buy()
    {
        base.Buy();

        if (nbFragment < 0)
        {
            //SpellGestion.AddFragment_Effect(spell, Mathf.Abs(nbFragment));
        }
        else
        {
            //SpellGestion.AddFragment(spell, nbFragment);
        }

        Ascension.nortice -= norticeCost;

        allBuyedFragmentInstanceId.Add(Id);
    }

    public int nbFragment;

    public SpellGestion.List spell;

    public override void SetInfo(Window_skill w = null)
    {
        base.SetInfo(w);
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();

        if (nbFragment < 0)
        {
            //Main_UI.Display_Description(title, Nortice_Ls.GetDescription_Effect(spell, Mathf.Abs(nbFragment)), transform.position, transform.lossyScale.x * distanceMultiplicator);
        }
        else
        {
            //Main_UI.Display_Description(title, Nortice_Ls.GetDescription_Fragment(spell, nbFragment), transform.position, transform.lossyScale.x * distanceMultiplicator);
        }
    }

    public GameObject norticeCostGameobject;
    public Text norticeCost_txt;
    public int norticeCost;

    public override void CostTxtUpdate()
    {
        norticeCostGameobject.gameObject.SetActive(curState != State.purchased);
        norticeCost_txt.text = "" + norticeCost;
    }
}
