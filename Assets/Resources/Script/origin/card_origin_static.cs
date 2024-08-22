using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card_origin_static : MonoBehaviour
{
    public static bool StartChoice()
    {
        if (Origin.player_origin == Origin.Value.none)
        {
            CreateChoiceCard();

            return true;
        }

        return false;
    }

    public static bool CreateChoiceCard()
    {
        if (Origin.player_origin != Origin.Value.none) return false;

        int index = 0;
        int index_max = Origin.lsChoosableOrigin.Count - 1;

        foreach (Origin.Value l in Origin.lsChoosableOrigin)
        {
            CreateCard(l, index, index_max);

            index++;
        }

        return true;
    }

    public static void CreateCard(Origin.Value o, int index = 0, int index_max = 0, Transform parent = null)
    {
        card_origin cardScript = Instantiate(Resources.Load<GameObject>("Prefab/Card/Card_origin"), parent).GetComponent<card_origin>();

        cardScript.Init(index, index_max);

        cardScript.Init_Specific(o);
    }
}
