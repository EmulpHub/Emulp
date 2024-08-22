using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Window_skill : Window
{
    public void BuyRecursively(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (!child.gameObject.activeSelf)
                continue;

            if (child.gameObject.name == "Canceled")
                break;

            TreeElementBuyable ps = child.GetComponent<TreeElementBuyable>();

            if (ps != null)
            {
                V.player_info.point_skill++;

                ps.Buy();
            }
            else if (child.transform.childCount > 0)
            {
                BuyRecursively(child);
            }
        }
    }


    /// <summary>
    /// Get a list of all skill that the player can get
    /// </summary>
    /// <param name="parent">The parent of groupSpell</param>
    /// <returns></returns>
    public static List<SpellGestion.List> GetAllSkill(Transform parent)
    {
        List<SpellGestion.List> AllSkill = new List<SpellGestion.List>();

        foreach (Transform ch in parent)
        {
            if (ch.name == "Canceled")
                break;

            TreeSkill ps = ch.GetComponent<TreeSkill>();

            if (ps && ps.spell != SpellGestion.List.warrior_Punch)
            {
                AllSkill.Add(ps.spell);
            }

            if (ch.transform.childCount > 0)
            {
                AllSkill.AddRange(GetAllSkill(ch.transform));
            }
        }

        return AllSkill;
    }

    public static List<TreeElementBuyable> GetAllSkill_panelSkill(Transform parent)
    {
        List<TreeElementBuyable> AllSkill = new List<TreeElementBuyable>();

        foreach (Transform ch in parent)
        {
            if (ch.name == "Canceled")
                break;

            TreeElementBuyable ps = ch.GetComponent<TreeElementBuyable>();

            if (ps != null)
            {
                AllSkill.Add(ps);
            }

            if (ch.transform.childCount > 0)
            {
                AllSkill.AddRange(GetAllSkill_panelSkill(ch.transform));
            }
        }

        return AllSkill;
    }
}
