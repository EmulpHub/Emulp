using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TreePassiveElement : TreeElement
{

    Sprite graphiqueSprite;

    Origin_Passive.Value passive;

    public void SetInfo(Origin_Passive.Value passive)
    {
        this.passive = passive;

        if (passive != Origin_Passive.Value.none)
        {
            title = Origin_Passive.Get_Title(passive);
            description = Origin_Passive.Get_Description(passive);

            graphiqueSprite = Origin_Passive.Get_Image(passive);
        }
    }
}