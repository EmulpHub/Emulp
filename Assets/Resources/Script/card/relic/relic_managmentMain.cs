using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class relic_managmentMain : MonoBehaviour
{
    private static Transform relicCardParent;

    public static Transform RelicCardParent
    {
        get
        {

            if (relicCardParent == null)
            {
                relicCardParent = new GameObject("relicCardParent").transform;
            }

            return relicCardParent;
        }

        set { relicCardParent = value; }
    }
    public static void StartChoiceCardRelic()
    {
        RelicInit.CreateChoiceCard(RelicCardParent);
    }

    public static bool ShouldCreateChoiceCard()
    {
        return (NorticeEnum.Purchased(NorticeEnum.Value.relic) && RelicInit.equipedRelic.Count == 0) || (V.script_Scene_Main_Administrator.ShowRelicCard && V.administrator);
    }
}
