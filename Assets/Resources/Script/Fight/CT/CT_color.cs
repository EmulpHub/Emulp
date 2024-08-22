using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CT : MonoBehaviour
{
    public byte color_a;

    private Color32 GetColor(Color32 co)
    {
        return new Color32(co.r, co.g, co.b, color_a);
    }

    public CT_Gestion.Color CurrentColor;

    public void ChangeColor(CT_Gestion.Color color)
    {
        CurrentColor = color;

        render.color = GetColor(CT_Gestion.Instance.ConvertEnumColorIntoColor32(color));
    }

    public virtual void SetNormalColor() { }

    public void HighlightOn()
    {
        if (CurrentColor == CT_Gestion.Color.green)
            ChangeColor(CT_Gestion.Color.green_light);
        else if (CurrentColor == CT_Gestion.Color.blue)
            ChangeColor(CT_Gestion.Color.blue_over);
        else if (CurrentColor == CT_Gestion.Color.blue_noLine)
            ChangeColor(CT_Gestion.Color.blue_over_noLine);
    }
}
