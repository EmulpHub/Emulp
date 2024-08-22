using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public abstract partial class Spell : MonoBehaviour
{
    public Image contour;

    public Color32 contour_normal, contour_highlight, contour_use;

    bool ContourIsNotNormalColor;

    public float contour_transitionSpeed;

    public void ContourManagement()
    {
        Color32 colorToSet = contour_normal;

        if (mouseIsOver)
        {
            ContourIsNotNormalColor = true;
            colorToSet = contour_highlight;
        }
        else if (SpellGestion.selectionnedSpell_list == spell && spell != SpellGestion.List.empty)
        {
            ContourIsNotNormalColor = true;
            colorToSet = contour_use;
        }
        else
        {
            ContourIsNotNormalColor = false;
        }

        contour.DOColor(colorToSet, contour_transitionSpeed);
    }
}
