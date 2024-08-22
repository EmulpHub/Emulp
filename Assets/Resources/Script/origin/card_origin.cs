using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card_origin : card
{
    [HideInInspector]
    public Origin.Value o;

    public void Init_Specific(Origin.Value o)
    {
        this.o = o;
        title.text = Origin.Get_Title(o);

        desc.text = Origin.Get_Description(o);

        info_txt.text = "";

        img.sprite = Origin.Get_Sprite(o);

        passive = Origin.Get_Passive(o);

        if (!NorticeEnum.Purchased(NorticeEnum.Value.passive)) allPassiveScript.Clear();

        InitPassiveHolder();
    }

    public void InitPassiveHolder()
    {
        int i = 0;

        foreach (passiveSelector p in allPassiveScript)
        {
            p.Init(passive[i], this);

            i++;
        }
    }

    public void UseThis(Origin_Passive.Value p)
    {
        Origin_Passive.AddPlayerPassive(p);

        Origin.ChangeOrigin(o);

        CardStarter.Background_remove();

        RemoveAllCurrentCard();

        base.OnClick();
    }

    [HideInInspector]
    public List<Origin_Passive.Value> passive = new List<Origin_Passive.Value>();

    public List<passiveSelector> allPassiveScript = new List<passiveSelector>();

    public static Origin.Value currentSelectedOrigin;

    public void PassiveHolder_Management()
    {
        if (currentSelectedOrigin == o)
            currentSelectedOrigin = Origin.Value.none;
        else
            currentSelectedOrigin = o;
    }

    public override void OnClick()
    {
        if (allPassiveScript.Count == 0)
        {
            UseThis(Origin_Passive.Value.none);
        }
        else
        {
            PassiveHolder_Management();
        }
    }

    public Transform passiveHolder;

    public override void Update()
    {
        base.Update();

        passiveHolder.gameObject.SetActive(currentSelectedOrigin == o);
    }

    public override float getYPosition()
    {
        return 0;
    }
}
