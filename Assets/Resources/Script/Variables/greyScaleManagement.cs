using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class greyScaleManagement : MonoBehaviour

{

    public Sprite s;

    public Image img;

    public void Start()
    {
        img.sprite = ConvertToGrayscale(s);
    }

    public static Texture2D textureFromSprite(Sprite sprite)
    {
        if (sprite.rect.width != sprite.texture.width)
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                         (int)sprite.textureRect.y,
                                                         (int)sprite.textureRect.width,
                                                         (int)sprite.textureRect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        else
            return sprite.texture;
    }

    public static Sprite ConvertToGrayscale(Sprite s)
    {

        Texture2D tex = new Texture2D(s.texture.width, s.texture.height, TextureFormat.RGBA32, false);

        Graphics.CopyTexture(s.texture, tex);

        tex.Apply();

        Color32[] pixelsLS = tex.GetPixels32();

        int w = tex.width;
        int h = tex.height;

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                Color32 pixel = pixelsLS[x + y * tex.width];

                if (pixel.a < 50)
                {
                    continue;
                }

                int p = ((256 * 256 + pixel.r) * 256 + pixel.b) * 256 + pixel.g;
                int b = p % 256;
                p = Mathf.FloorToInt(p / 256);
                int g = p % 256;
                p = Mathf.FloorToInt(p / 256);
                int r = p % 256;
                float l = (0.2126f * r / 255f) + 0.7152f * (g / 255f) + 0.0722f * (b / 255f);
                Color c = new Color(l, l, l, 1);
                tex.SetPixel(x, y, c);
            }
        }

        tex.Apply(false);

        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

    }
}
