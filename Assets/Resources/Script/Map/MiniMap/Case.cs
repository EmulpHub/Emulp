using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LayerMap;

public partial class Case : MonoBehaviour
{
    RectTransform thisRect;

    public string pos;

    public IMap mapInfo;


    public bool showCase ()
    {
        bool showThisCase = VisibleMap.contain(pos);

        graphic.gameObject.SetActive(showThisCase);

        outline.gameObject.SetActive(showThisCase);

        return showThisCase;
    }

    public void UpdateCase ()
    {
        CalcPosition();

        if (!showCase()) return;

        SetOutlineColor();

        SetImage();

        SetImageColor();
    }
}
