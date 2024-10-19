using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRenderEngine : MonoBehaviour
{
    //first = bg
    //middle = outlineInterior
    //last = outlineExterior
    public TileRenderEngine_CanvasCorner first, middle, last;

    public float normalSize;

    public void SetOutline(Color32 outlineColor, float size)
    {
        first.SetSize(normalSize - size * 0.6666f);

        middle.gameObject.SetActive(true);
        middle.SetSize(normalSize);
        middle.SetColor(outlineColor);

        last.gameObject.SetActive(true);
        last.SetSize(normalSize + size * 0.333f);
        last.SetColor(outlineColor);
    }

    public void SetBgColor(Color32 color)
    {
        first.SetColor(color);
    }

    public void SetOrderLayer(int layer)
    {
        first.SetLayer(layer + 1);
        middle.SetLayer(layer);
        last.SetLayer(layer - 1);
    }

    public void SetRoundedCorner(bool topRight, bool topLeft, bool botRight, bool botLeft)
    {
        first.SetAllowRounded(topRight, topLeft, botRight, botLeft);
        middle.SetAllowRounded(topRight, topLeft, botRight, botLeft);
        last.SetAllowRounded(topRight, topLeft, botRight, botLeft);
    }

    public void SetRoundedCornerPixelPerUnit(float nb)
    {
        first.SetRoundedCorner(nb);
        middle.SetRoundedCorner(nb);
        last.SetRoundedCorner(nb);
    }

    public enum CornerSprite { spike_0, spike_1, spike_2 }


    public Sprite spike_0;
    public Sprite spike_1;
    public Sprite spike_2;

    public void SetCornerSprite(CornerSprite cornerSprite)
    {
        Sprite sp = null;

        switch (cornerSprite)
        {
            case CornerSprite.spike_0:
                sp = spike_0;
                break;

            case CornerSprite.spike_1:
                sp = spike_1;
                break;

            case CornerSprite.spike_2:
                sp = spike_2;
                break;
        }

        first.SetImage(sp);
        middle.SetImage(sp);
        last.SetImage(sp);
    }
}
