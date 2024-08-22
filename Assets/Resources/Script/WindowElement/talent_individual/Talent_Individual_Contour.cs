using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Talent_Individual : WindowSkillElement
{
    public Image contour;

    public Color32 Normal, Highlighted, Equiped;

    public void contour_Management()
    {
        Color32 curColor = Normal;

        if (Locked)
        {
            curColor = Normal;
        }
        else if (MouseOver)
        {
            curColor = Highlighted;
        }
        else if (thisTalent != Talent_Gestion.talent.empty && Character.IsTalentEquiped(thisTalent))
        {
            curColor = Equiped;
        }

        contour.color = curColor;
    }
}
