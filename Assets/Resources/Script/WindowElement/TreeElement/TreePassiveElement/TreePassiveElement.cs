using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class TreePassiveElement : TreeElement
{
    public override bool DisplayInfo_isShowed()
    {
        return DescriptionStatic.CurrentDescription_script != null && DescriptionStatic.CurrentDescription_script.title.text == title;
    }

    public override void DisplayInfo()
    {
        if (passive == Origin_Passive.Value.none) return;

        Description_text.Display(title,description,transform.position,transform.lossyScale.x);
    }

    public Image render;

    public void GraphicUpdate ()
    {
        if(passive == Origin_Passive.Value.none)
        {
            render.gameObject.SetActive(false);
            return;
        }
        else
        {
            render.gameObject.SetActive(true);

            render.sprite = graphiqueSprite;

            return;
        }
    }

    public override void Func_update()
    {
        base.Func_update();

        GraphicUpdate();
    }
}
