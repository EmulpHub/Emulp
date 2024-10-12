using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Tile : MonoBehaviour
{
    public void ChangeColor(Tile_Gestion.Color color)
    {
        render.color = Tile_Gestion.Instance.ConvertEnumColorIntoColor32(color);
    }

    public virtual void UpdateColor() { }
}
