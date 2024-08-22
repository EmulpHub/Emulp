using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotriceGainShow : MonoBehaviour
{
    public void Init(Dictionary<Nortice_GainSystem.category, notriceGainInfo> dic)
    {
        int i = 0;

        while (i < line_parent.transform.childCount)
        {
            if (line_parent.GetChild(i).gameObject != total)
            {
                DestroyImmediate(line_parent.GetChild(i).gameObject);

                i--;
            }

            i++;
        }

        foreach (Nortice_GainSystem.category c in Nortice_GainSystem.orderToShowNorticeGain)
        {
            if (dic.ContainsKey(c))
            {
                CreateLine(dic[c]);
            }
        }

        total.transform.SetAsLastSibling();

        total_txt.text = "Total : " + GetTotal(dic);
    }

    public GameObject total;

    public Text total_txt;

    public GameObject Line;

    public Transform line_parent;

    public void CreateLine(notriceGainInfo inf)
    {
        GameObject g = Instantiate(Line, line_parent);

        Text t = g.transform.GetChild(0).GetComponent<Text>();

        t.text = inf.GetLeftString();
        t.transform.GetChild(0).GetComponent<Text>().text = inf.GetRightString();
    }

    public static int GetTotal(Dictionary<Nortice_GainSystem.category, notriceGainInfo> ls)
    {
        int t = 0;

        foreach (notriceGainInfo inf in ls.Values)
        {
            t += inf.gain;
        }

        return t;
    }
}
