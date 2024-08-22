using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LayerMap;

public partial class Case : MonoBehaviour
{
    public Color32 outlineColor_nonVisited,outlineColor_visited, outlineColor_CurrentMap;

    [HideInInspector]
    public RectTransform outline_rectTransform;

    [HideInInspector]
    public Image outline;

    public void SetOutlineColor()
    {
        if (pos == WorldData.PlayerPositionInWorld)
        {
            outline_rectTransform.SetAsLastSibling();
            outline.color = outlineColor_CurrentMap;
        }
        else if (!VisitedMap.contain(pos))
        {
                outline.color = outlineColor_nonVisited;
        }else
        {
            outline.color = outlineColor_visited;

        }
    }
}
