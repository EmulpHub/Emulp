using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public abstract partial class Entity : MonoBehaviour
{
    public virtual void UI_Management() { }

    public virtual void ResetUI() { }

    public float display_Damage_distance;

    public void DisplayPaChange(int amount, string additionalText)
    {
        string paTxt = V.IsFr() ? "pa" : "ap";

        string start = amount < 0 ? "" : "+";

        Main_UI.Display_movingText_basicValue(start + amount + paTxt + additionalText, V.Color.red, transform.position, V.pix_pa);
    }

    public void DisplayPmChange(int amount, string additionalText)
    {
        string paTxt = V.IsFr() ? "pm" : "mp";

        string start = amount < 0 ? "" : "+";

        Main_UI.Display_movingText_basicValue(start + amount + paTxt + additionalText, V.Color.red, transform.position, V.pix_pm);
    }

}
