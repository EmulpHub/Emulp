using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CT_Gestion : MonoBehaviour
{
    private List<string> ListAOE = new List<string>();

    /// <summary>
    /// The tile that have been erased to show AOE
    /// </summary>
    private List<string> ListErasedTile = new List<string>();

    public void SetListAOE(string targetedSqaure, bool NoLineOfView = false)
    {
        AOE_EraseAll();

        ListAOE = SpellInfo.GetStringEffectList(SpellGestion.selectionnedSpell_list, targetedSqaure, V.player_entity.CurrentPosition_string);

        foreach (string pos in ListAOE)
        {
            if (CTInfo.Instance.Get(pos) != null)
            {
                ListErasedTile.Add(pos);

                Erase(pos, CT.AnimationErase_type.none);
            }

            CT_Graphic.Add(pos, NoLineOfView ? Color.blue_over_noLine : Color.blue_over, false, true, true, ListAOE, false, 1);
        }

        MouseOnTile = CTInfo.Instance.Get(targetedSqaure);

        Add_IconSpell(MouseOnTile);
    }

    public void AOE_EraseAll()
    {
        foreach (string p in ListAOE)
        {
            CT tile = CTInfo.Instance.Get(p);

            if (tile != null && tile.type == CT.Type.graphic)
            {
                Erase(p, CT.AnimationErase_type.none);

                if (ListErasedTile.Contains(p))
                    CT_Spell.Add(p, false);
            }
        }

        ListAOE.Clear();

        ListErasedTile.Clear();

        CTInfo.Instance.ResetSpriteOfAllActiveTile();
    }
}
