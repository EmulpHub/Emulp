using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class RelicInit : MonoBehaviour
{
    public static bool CreateChoiceCard(Transform parent)
    {
        if (relicInfo.Count == 0)
            return false;

        List<relicLs> ls = new List<relicLs>(relicInfo.Keys);
        List<relicLs> relicSelection = new List<relicLs>();

        int nb = 3, totalCount = 0;

        while (nb > 0 && ls.Count > 0)
        {
            relicLs r = ls[Random.Range(0, ls.Count)];

            relicSelection.Add(r);
            ls.Remove(r);


            nb--;
            totalCount++;
        }

        int i = 0;

        foreach (relicLs t in relicSelection)
        {
            CreateCard(t, i, totalCount - 1, parent);

            i++;
        }

        return relicSelection.Count > 0;
    }

    public static void CreateCard(relicLs relic, int index = 0, int index_max = 0,Transform parent = null)
    {
        card_Relic cardScript = Instantiate(Resources.Load<GameObject>("Prefab/Card/Card_relic"), parent).GetComponent<card_Relic>();

        cardScript.Init(index, index_max);

        cardScript.Init_Specific(relic);
    }
}
