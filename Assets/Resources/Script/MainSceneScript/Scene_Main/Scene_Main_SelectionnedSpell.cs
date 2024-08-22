using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public partial class Scene_Main : MonoBehaviour
{
    public enum DifferentShowSelectionned { movement, spell, none }

    public DifferentShowSelectionned GetCurrentDf()
    {
        bool ItsFight = V.game_state == V.State.fight;

        var mouseOnTile = CT_Gestion.Instance.MouseOnTile;

        if (ItsFight && mouseOnTile != null && mouseOnTile.type == CT.Type.movement)
            return DifferentShowSelectionned.movement;

        else if ((ItsFight && V.game_state_action == V.State_action.spell) ||
            (SpellGestion.selectionnedSpell_list != SpellGestion.List.none && SpellGestion.selectionnedSpell_list != SpellGestion.List.empty))
            return DifferentShowSelectionned.spell;

        return DifferentShowSelectionned.none;
    }

    public void SelectionnateSpell_Management()
    {
        DifferentShowSelectionned df = GetCurrentDf();

        selectionnedSpell.gameObject.SetActive(df != DifferentShowSelectionned.none);

        if (df != DifferentShowSelectionned.none)
        {
            Vector3 MousePos = CursorInfo.Instance.positionInWorld;

            SelectionnedSpell_Holder script = selectionnedSpell.GetComponent<SelectionnedSpell_Holder>();

            //Set the sprite of the selectionnedSpell to be the one of the spell we selectionned
            Image selectionnedSpell_img = script.img;

            // set MIN and MAX Anchor values(positions) to the same position (ViewportPoint)
            selectionnedSpell_img.rectTransform.anchorMin = MousePos;
            selectionnedSpell_img.rectTransform.anchorMax = MousePos;

            if (df == DifferentShowSelectionned.spell)
            {
                script.ActiveSpellGraphics();

                //Get the sprite of the spell and give it to the selectionnedSpell sprite
                selectionnedSpell_img.sprite = SpellGestion.Get_sprite(SpellGestion.selectionnedSpell_list);
            }
            else //For when we want to move over a square
            {
                script.DeactiveSpellGraphics();

                //Get the sprite of the spell and give it to the selectionnedSpell sprite
                selectionnedSpell_img.sprite = icon_Movement;
            }

            selectionnedSpell.transform.position = new Vector3(MousePos.x, MousePos.y, 0) + selectionnedSpell_gap;
        }

    }

}
