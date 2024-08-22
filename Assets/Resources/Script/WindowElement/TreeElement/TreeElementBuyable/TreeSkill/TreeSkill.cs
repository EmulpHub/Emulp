using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TreeSkill : TreeElementBuyable
{
    public static List<SpellGestion.List> PurchasedSkill = new List<SpellGestion.List>();

    [HideInInspector]
    public bool LockedByLevel { get => LockedByLevel_val > 0; }

    public int LockedByLevel_val;

    public override bool CurrentlyLocked()
    {
        return LockedByLevel && V.player_info.level < LockedByLevel_val;
    }

    public override bool IsBuyable()
    {
        return V.player_info.point_skill >= 1 && DadIsPurchased() && !IsPurchased();
    }

    public override bool IsPurchased()
    {
        return LockedByLevel || PurchasedSkill.Contains(spell);
    }

    public override void Buy()
    {
        base.Buy();

        V.player_info.point_skill--;

        AnimationToSpell();

        if (SpellGestion.allSkill_locked.Contains(spell))
            SpellGestion.allSkill_locked.Remove(spell);

        PurchasedSkill.Add(spell);

        clickAnimation();
    }


    public void AnimationToSpell()
    {
        Vector2 toolbar_skillPos = Main_UI.Toolbar_AddSpell(spell);

        if (toolbar_skillPos != Vector2.zero)
        {
            Animation_skillAssignation.Instantiate(transform.position, toolbar_skillPos, spell);
        }
    }

    public void AnimationToTalent()
    {
        Animation_skillAssignation.Instantiate(transform.position, window.TalentG.transform.position, spell, 1, 1, true);
    }

    public override void ClickWhenPurchased()
    {
        clickAnimation();
        SpellGestion.SetSelectionnedSpell(spell, null, transform.position);
    }

    void clickAnimation()
    {
        ShakeAnimation(punchScaleStrengh_MouseClick, 0.5f, baseScale.x, this.gameObject);
    }
}
