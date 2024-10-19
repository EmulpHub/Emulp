using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Tile : MonoBehaviour
{
    public void SetCornerValue (float value)
    {
        render.SetRoundedCornerPixelPerUnit(value);
    }

    public void SetCornerSprite (TileRenderEngine.CornerSprite cornerSprite)
    {
        render.SetCornerSprite(cornerSprite);
    }
}
