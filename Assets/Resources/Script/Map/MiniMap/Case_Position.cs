using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Case : MonoBehaviour
{
    public float margin;

    public void CalcPosition()
    {
        Vector2Int xy = F.ConvertToVector2Int(pos);
        Vector2Int xyPlayer = F.ConvertToVector2Int(WorldData.PlayerPositionInWorld);

        int x = xy.x - xyPlayer.x;
        int y = xy.y - xyPlayer.y;

        Vector2 position = new Vector2(x * (thisRect.rect.width + margin), y * (thisRect.rect.height + margin * 1.77f));

        thisRect.anchoredPosition = position;
        outline_rectTransform.anchoredPosition = position;
    }

}
