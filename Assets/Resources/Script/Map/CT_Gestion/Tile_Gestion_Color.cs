using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Tile_Gestion : MonoBehaviour
{
    public enum Color { green, green_light, blue, blue_over, blue_noLine, blue_over_noLine, red }

    public Color32 green, green_light, blue, blue_over, blue_over_noLine, blue_noLine, red;

    public Color32 colorMovement, colorSpell;

    public Color32 ConvertEnumColorIntoColor32(Color color)
    {
        if (color == Color.green)
            return green;
        else if (color == Color.blue)
            return blue;
        else if (color == Color.blue_over)
            return blue_over;
        else if (color == Color.blue_noLine)
            return blue_noLine;
        else if (color == Color.blue_over_noLine)
            return blue_over_noLine;
        else if (color == Color.red)
            return red;
        else //green light
            return green_light;
    }
}
