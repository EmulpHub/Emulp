using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using LayerMap;

public class MiniMap : MonoBehaviour
{
    public Transform caseParent, caseOutlineParent;

    public Dictionary<string, Case> cases = new Dictionary<string, Case>();

    public Text distanceText;

    public void InitWorld ()
    {
        while (caseParent.transform.childCount > 0) DestroyImmediate(caseParent.GetChild(0).gameObject);
        while (caseOutlineParent.transform.childCount > 0) DestroyImmediate(caseOutlineParent.GetChild(0).gameObject);

        VisibleMap.Init();
        VisitedMap.Init();

        cases.Clear();

        foreach(string pos in WorldData.World.Keys)
        {
            cases.Add(pos,Case.create(pos,WorldData.World[pos]));
        }

        UpdateAllCases();
    }

    public void UpdateAllCases ()
    {
        foreach(Case oneCase in cases.Values)
        {
            oneCase.UpdateCase();
        }
    }

    public void Update()
    {
        distanceText.gameObject.SetActive(WorldData.distance > 0);

        distanceText.text = "" + WorldData.distance;

        UpdateAllCases();
    }
}