using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeNortice : TreeElementBuyable
{
    public static int NextID;

    private int _id = -1;

    public int Id
    {
        get
        {
            if (_id == -1)
            {
                _id = NextID;
                NextID++;
            }

            return _id;
        }
    }

    public override bool IsBuyable()
    {
        return Ascension.nortice >= norticeCost && DadIsPurchased();
    }

    public override bool IsPurchased()
    {
        return allBuyedFragmentInstanceId.Contains(Id);
    }

    public override void Buy()
    {
        base.Buy();

        Ascension.nortice -= norticeCost;

        allBuyedFragmentInstanceId.Add(Id);

        NorticeEnum.Get(norticeName).purchased = true;
    }

    public NorticeEnum.Value norticeName;

    private NorticeInfo _info;

    private NorticeInfo info
    {
        get
        {
            if (_info == null) _info = NorticeEnum.Get(norticeName);

            return _info;
        }
    }

    private int norticeCost;

    public override void SetInfo(Window_skill w = null)
    {
        title = info.title;
        description = info.description;

        graphiqueSprite = info.img;
        graphiqueSprite_gray = info.img_grey;

        norticeCost = info.fragmentCost;
    }

    public GameObject norticeCostGameobject;
    public Text norticeCost_txt;

    public override void CostTxtUpdate()
    {
        norticeCostGameobject.gameObject.SetActive(curState != State.purchased);
        norticeCost_txt.text = "" + norticeCost;
    }
}
