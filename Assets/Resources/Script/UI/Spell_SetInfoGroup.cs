using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spell_SetInfoGroup : MonoBehaviour
{
    public Sprite zone_line_2, zone_oneSquareAround, zone_twoSquareAround, zone_threeSquareAround, zone_cone, zone_cone_inverted, special_noNeedOfLineOfView, special_InLine, zone_oneSquareAround_line, zone_twoSquareAround_line, zone_threeSquareAround_line;

    public Sprite ChooseSprite(SpellGestion.range_effect_size zone)
    {
        switch (zone)
        {
            case SpellGestion.range_effect_size.Cone:
                return zone_cone;
            case SpellGestion.range_effect_size.Cone_Inverted:
                return zone_cone_inverted;
            case SpellGestion.range_effect_size.line_2:
                return zone_line_2;
            case SpellGestion.range_effect_size.oneSquareAround:
                return zone_oneSquareAround;
            case SpellGestion.range_effect_size.twoSquareAround:
                return zone_twoSquareAround;
            case SpellGestion.range_effect_size.threeSquareAround:
                return zone_threeSquareAround;
            case SpellGestion.range_effect_size.oneSquareAround_line:
                return zone_oneSquareAround_line;
            case SpellGestion.range_effect_size.twoSquareAround_line:
                return zone_twoSquareAround_line;
            case SpellGestion.range_effect_size.threeSquareAround_line:
                return zone_threeSquareAround_line;
            case SpellGestion.range_effect_size.Front3line:
                return special_InLine;
        }

        throw new System.Exception("Range effect size not correct");
    }

    public RectTransform cd_group, distance_group, distance_zone_group, special_unique, special_double;

    public Image cd_logo, cd_distance, distance_zone, special_unique_img;

    public Text txt_cd, txt_range;

    public Color32 normal, modified;

    public void Init(int pa, Spell.Range_type rangeType, string title, int rangeMin, int rangeMax, SpellGestion.range_effect_size zone, int cd)
    {
        Init((pa, false), rangeType, title, (rangeMin, false), (rangeMax, false), zone, (cd, false));
    }

    public void Init((int pa, bool modified) pa, Spell.Range_type rangeType, string title, (int rangeMin, bool modified) rangeMin, (int rangeMax, bool modified) rangeMax, SpellGestion.range_effect_size zone, (int cd, bool modified) cd)
    {
        bool Empty = title == "Vide" || title == "Empty" || title == "Emplacement d'objet vide" || title == "Empty object slot";

        //Special
        if (pa.pa == -1 || Empty)
        {
            special_unique.gameObject.SetActive(false);
            special_double.gameObject.SetActive(false);
        }
        else
        {
            if (rangeType == Spell.Range_type.noNeedOfLineOfView)
            {
                special_unique.gameObject.SetActive(true);
                special_double.gameObject.SetActive(false);

                special_unique_img.sprite = special_noNeedOfLineOfView;
            }
            else if (rangeType == Spell.Range_type.line)
            {

                special_unique.gameObject.SetActive(true);
                special_double.gameObject.SetActive(false);

                special_unique_img.sprite = special_InLine;

            }
            else
            {
                special_unique.gameObject.SetActive(false);
                special_double.gameObject.SetActive(false);
            }
        }

        //Range
        if (pa.pa == -1 || Empty)
        {
            distance_group.gameObject.SetActive(false);
        }
        else if (rangeMin.rangeMin == 0 && rangeMax.rangeMax == 0)
        {
            distance_group.gameObject.SetActive(true);

            txt_range.text = "0";

            if (rangeMin.modified || rangeMax.modified)
                txt_range.color = modified;
            else
                txt_range.color = normal;

            cd_distance.sprite = V.logo_player;
        }
        else
        {
            distance_group.gameObject.SetActive(true);

            if (rangeMin.rangeMin != 1)
            {
                txt_range.text = "" + rangeMin.rangeMin + "-" + rangeMax.rangeMax + "";
            }
            else
            {
                txt_range.text = "" + rangeMax.rangeMax;
            }

            if (rangeMin.modified || rangeMax.modified)
                txt_range.color = modified;
            else
                txt_range.color = normal;

            if (rangeMax.rangeMax == 1)
                cd_distance.sprite = V.logo_melee;
            else
                cd_distance.sprite = V.logo_distance;
        }

        //ZONE
        if (zone == SpellGestion.range_effect_size.singleTarget || Empty)
        {
            distance_zone_group.gameObject.SetActive(false);
        }
        else
        {
            distance_zone_group.gameObject.SetActive(true);

            distance_zone.sprite = ChooseSprite(zone);
        }

        //CD
        if (cd.cd == 0 || Empty)
        {
            cd_group.gameObject.SetActive(false);
            txt_cd.text = "";

        }
        else
        {
            cd_group.gameObject.SetActive(true);
            txt_cd.text = "" + Mathf.Abs(cd.cd);

            if (cd.modified)
            {
                txt_cd.color = modified;
            }
            else
            {
                txt_cd.color = normal;
            }

            if (cd.cd < 0)
                cd_logo.sprite = V.logo_usePerTurn;
            else
                cd_logo.sprite = V.logo_cooldown;
        }
    }

    public void Init(SpellGestion.List spell)
    {
        (int pa, bool modified) pa = SpellGestion.Get_paCost_full(spell);
        Spell.Range_type rangeType = SpellGestion.Get_rangeType(spell);
        string title = SpellGestion.Get_Title(spell);
        (int rangeMax, bool modified) rangeMax = SpellGestion.Get_RangeMax_full(spell);
        (int rangeMin, bool modified) rangeMin = SpellGestion.Get_RangeMin_full(spell);
        SpellGestion.range_effect_size zone = SpellGestion.Get_RangeEffect(spell);
        (int cd, bool modified) cd = SpellGestion.Get_Cd_full(spell);

        Init(pa, rangeType, title, rangeMin, rangeMax, zone, cd);
    }
}
