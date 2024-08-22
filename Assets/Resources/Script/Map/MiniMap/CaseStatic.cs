using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LayerMap;
using UnityEngine.UI;

public partial class Case : MonoBehaviour
{
    public static Case create(string pos, IMap mapInfo)
    {
        GameObject caseGameobject = Resources.Load<GameObject>("Prefab/MiniMap/Case");

        GameObject caseOutlineGameobject = Resources.Load<GameObject>("Prefab/MiniMap/CaseOutline");

        Case newCase = Instantiate(caseGameobject, V.script_Scene_Main.miniMap.caseParent).GetComponent<Case>();

        GameObject caseOutline = Instantiate(caseOutlineGameobject, V.script_Scene_Main.miniMap.caseOutlineParent.transform);

        newCase.pos = pos;

        newCase.thisRect = newCase.gameObject.GetComponent<RectTransform>();

        newCase.mapInfo = mapInfo;

        newCase.outline_rectTransform = caseOutline.GetComponent<RectTransform>();
        newCase.outline = caseOutline.GetComponent<Image>();

        return newCase;
    }
}
