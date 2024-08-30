using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class card_primaryStats : card
{
    public static bool CreateChoiceCardRandom ()
    {
        try
        {
            List<primaryStatsSetter.list> stats = new List<primaryStatsSetter.list>();

            List<primaryStatsSetter.list> availableKeys = new List<primaryStatsSetter.list>(primaryStatsSetter.dicInfo.Keys);

            if (availableKeys.Count < 3) return false;

            while (stats.Count < 3)
            {
                int randomRange = Random.Range(0, availableKeys.Count);

                primaryStatsSetter.list list = availableKeys[randomRange];

                var setterInfo = primaryStatsSetter.dicInfo[list];

                if (setterInfo.checkIfPossible())
                    stats.Add(list);


                availableKeys.RemoveAt(randomRange);
            }

            CreateChoiceCard(stats);
        } catch(System.Exception e)
        {
            Debug.Log("Y a ce bug bizarre à résoudre sur le primary stats setter : " +e.Message);
        }
        return false;
    }

    public static bool CreateChoiceCard(List<primaryStatsSetter.list> stats)
    {
        int i = 0;

        foreach (primaryStatsSetter.list s in stats)
        {
            CreateCard(primaryStatsSetter.dicInfo[s], i, stats.Count - 1);

            i++;
        }

        return stats.Count > 0;
    }

    public static void CreateCard(primaryStatsSetterInfo info, int index = 0, int index_max = 0)
    {
        card_primaryStats cardScript = Instantiate(Resources.Load<GameObject>("Prefab/Card/Card_primaryStats")).GetComponent<card_primaryStats>();

        cardScript.Init(index, index_max);

        cardScript.Init_Specific(info);
    }

    [HideInInspector]
    public primaryStatsSetterInfo stats;

    public void Init_Specific(primaryStatsSetterInfo info)
    {
        var desc_txt = DescriptionStatic.SeparateDesc(info.description());

        desc.text = descColor.convert(desc_txt.normal);

        info_txt.text = desc_txt.info;

        stats = info;
    }

    public override void OnClick()
    {
        base.OnClick();

        stats.AddToCurrentStat();

        RemoveAllCurrentCard();
    }
}
