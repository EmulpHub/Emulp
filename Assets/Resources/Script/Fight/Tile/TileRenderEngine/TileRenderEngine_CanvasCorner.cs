using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class TileRenderEngine_CanvasCorner : MonoBehaviour
{
    public Canvas canvas;
    public Image TopRight, TopLeft, BotRight, BotLeft;

    public void SetImage (Sprite sprite)
    {
        SetSprite(sprite,TopRight);
        SetSprite(sprite,TopLeft);
        SetSprite(sprite,BotRight);
        SetSprite(sprite,BotLeft);
    }

    private void SetSprite (Sprite sprite,Image img)
    {
        img.sprite = sprite;
    }

    public void SetLayer(int layer)
    {
        canvas.sortingOrder = layer;
    }

    public void SetColor(Color32 color)
    {
        SetColorTo(color, TopRight);
        SetColorTo(color, TopLeft);
        SetColorTo(color, BotRight);
        SetColorTo(color, BotLeft);
    }

    public void SetAllowRounded(bool topRight, bool topLeft, bool botRight, bool botLeft)
    {
        rounded_topRight = topRight;
        rounded_topLeft = topLeft;
        rounded_botRight = botRight;
        rounded_botLeft = botLeft;

        SetRoundedCorner(roundedCorner);
    }

    private float roundedCorner = 2;

    public void SetRoundedCorner(float nb)
    {
        roundedCorner = nb;

        SetRoundedTo(rounded_topRight ? nb : 100, TopRight);
        SetRoundedTo(rounded_topLeft ? nb : 100, TopLeft);
        SetRoundedTo(rounded_botRight ? nb : 100, BotRight);
        SetRoundedTo(rounded_botLeft ? nb : 100, BotLeft);
    }

    public void SetSize (float size)
    {
        transform.localScale = new Vector3(size, size, 1);
    }

    private void SetRoundedTo(float nb, Image img)
    {
        img.pixelsPerUnitMultiplier = nb;
    }

    private bool rounded_topRight, rounded_topLeft, rounded_botRight, rounded_botLeft;

    private void SetColorTo(Color32 color, Image img)
    {
        img.color = color;
    }
}
