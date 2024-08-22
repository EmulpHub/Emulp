using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card_Relic : card
{
    public RelicInit.relicLs relic;

    public equipmentHolder equipmentDesc;

    public void Init_Specific(RelicInit.relicLs e)
    {
        relic = e;
        title.text = descColor.convert(RelicInit.relic_title(relic));

        info_txt.text = "";

        img.sprite = RelicInit.relic_sprite(relic);

        equipmentDesc.init(RelicInit.relic_desc(relic), RelicInit.relic_equipmentValue(relic));
    }

    public override void OnClick()
    {
        base.OnClick();

        RelicInit.EquipRelic(relic);

        RemoveAllCurrentCard();

        if (CardStarter.CreateOriginChoiceCard)
        {
            card_origin_static.StartChoice();

            CardStarter.Background_switchSelection(CardBackground.Selection.origin);
        }
        else
        {
            CardStarter.Background_remove();
        }
    }

    public override float getYPosition()
    {
        return 0;
    }
}
