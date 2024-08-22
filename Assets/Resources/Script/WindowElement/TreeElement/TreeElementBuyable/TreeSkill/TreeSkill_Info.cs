using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TreeSkill : TreeElementBuyable
{
    [HideInInspector]
    public int cd, pa, rangeMin, rangeMax;

    public SpellGestion.List spell;

    [HideInInspector]
    public Spell.Range_type range_Type;

    [HideInInspector]
    public SpellGestion.range_effect_size range_Zone;

    public override void SetInfo(Window_skill w = null)
    {
        base.SetInfo(w);

        if (spell != SpellGestion.List.none)
        {
            title = SpellGestion.Get_Title(spell);
            description = SpellGestion.Get_Description(spell);
            cd = SpellGestion.Get_Cd(spell);
            pa = SpellGestion.Get_paCost(spell);
            rangeMin = SpellGestion.Get_RangeMin(spell);
            rangeMax = SpellGestion.Get_RangeMax(spell);
            range_Type = SpellGestion.Get_rangeType(spell);
            range_Zone = SpellGestion.Get_RangeEffect(spell);

            graphiqueSprite = SpellGestion.Get_sprite(spell);
            graphiqueSprite_gray = SpellGestion.Get_sprite_Gray(spell);

            buy_color = SpellGestion.Get_col(spell);
        }

        pa_cost.text = "" + pa;
    }
}
