using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Tile : MonoBehaviour
{
    public void ChangeColor(Tile_Gestion.Color color)
    {
        ChangeColor(Tile_Gestion.Instance.ConvertEnumColorIntoColor32(color));
    }

    public void ChangeColor(Color32 color)
    {
        render.SetBgColor(color);
    }

    public virtual void UpdateColor() { }
}
