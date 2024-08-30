using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class card_Talent : card
{
    public static bool CreateChoiceCard()
    {
        List<Talent_Gestion.talent> talentLocked = new List<Talent_Gestion.talent>(Talent_Gestion.lockedTalent);

        if (talentLocked.Count == 0) return false;

        List<Talent_Gestion.talent> ls = new List<Talent_Gestion.talent>();

        int nb = 3, totalCount = 0;

        while (nb > 0 && talentLocked.Count > 0)
        {
            Talent_Gestion.talent t = talentLocked[UnityEngine.Random.Range(0, talentLocked.Count)];

            var talentInfo = Talent_Gestion.Get(t);

            if(!talentInfo.Con())
            {
                talentLocked.Remove(t);
                continue;
            }

            ls.Add(t);

            talentLocked.Remove(t);

            nb--;
            totalCount++;
        }

        int i = 0;

        foreach (Talent_Gestion.talent t in ls)
        {
            CreateCard(t, i, totalCount - 1);

            i++;
        }

        return ls.Count > 0;
    }

    public static (bool find, Talent_Gestion.talent t) SearchTalent(bool WithCon = false)
    {
        Talent_Gestion.talent t = Talent_Gestion.takeRandomLockedTalent(WithCon);

        if (t == Talent_Gestion.talent.empty)
            return (false, Talent_Gestion.talent.empty);

        return (true, t);
    }

    public static void CreateCard(Talent_Gestion.talent talent, int index = 0, int index_max = 0)
    {
        card_Talent cardScript = Instantiate(Resources.Load<GameObject>("Prefab/Card/Card_talent")).GetComponent<card_Talent>();

        cardScript.Init(index, index_max);

        cardScript.Init_Specific(talent);
    }

    [HideInInspector]
    public Talent_Gestion.talent currentTalent;

    public void Init_Specific(Talent_Gestion.talent e)
    {
        currentTalent = e;
        title.text = descColor.convert(Talent_Gestion.GetTitle(e));

        string desc_txt = DescriptionStatic.SeparateDesc(Talent_Gestion.GetDescription(e)).normal;

        desc.text = descColor.convert(desc_txt);

        info_txt.text = V.IsFr() ? "nouveau talent" : "new talent";

        img.sprite = Talent_Gestion.GetSprite(e);
    }

    public override void OnClick()
    {
        base.OnClick();

        Talent_Gestion.UnlockTalent(currentTalent);

        RemoveAllCurrentCard();
    }

}
