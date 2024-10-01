using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Tile : MonoBehaviour
{
    public byte color_a;

    private Color32 GetColor(Color32 co)
    {
        return new Color32(co.r, co.g, co.b, color_a);
    }

    public Tile_Gestion.Color CurrentColor;

    public void ChangeColor(Tile_Gestion.Color color)
    {
        CurrentColor = color;

        render.color = GetColor(Tile_Gestion.Instance.ConvertEnumColorIntoColor32(color));
    }

    public virtual void SetNormalColor() { }

    public void HighlightOn()
    {
        if (CurrentColor == Tile_Gestion.Color.green)
            ChangeColor(Tile_Gestion.Color.green_light);
        else if (CurrentColor == Tile_Gestion.Color.blue)
            ChangeColor(Tile_Gestion.Color.blue_over);
        else if (CurrentColor == Tile_Gestion.Color.blue_noLine)
            ChangeColor(Tile_Gestion.Color.blue_over_noLine);
    }
}
