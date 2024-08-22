using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStarter : MonoBehaviour
{
    public static bool CreateOriginChoiceCard = false;

    private static CardBackground background;

    public static void MainStart ()
    {
        bool createdCard = false;

        CardBackground.Selection selection = CardBackground.Selection.relic;

        if (relic_managmentMain.ShouldCreateChoiceCard())
        {
            createdCard = true;
            relic_managmentMain.StartChoiceCardRelic();
            CreateOriginChoiceCard = true;
        }
        else if (Origin.player_origin == Origin.Value.none)
        {
            createdCard = true;
            CreateOriginChoiceCard = false;
            card_origin_static.StartChoice();
            selection = CardBackground.Selection.origin;

        }

        if (!createdCard) return;

        background = CardBackground.Create();

        Background_switchSelection(selection);
    }

    public static void Background_remove ()
    {
        background.Remove();
    }

    public static void Background_switchSelection (CardBackground.Selection selection)
    {
        background.changeSelection(selection);

    }
}