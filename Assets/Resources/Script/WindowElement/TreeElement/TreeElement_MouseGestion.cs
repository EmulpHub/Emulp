using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TreeElement : WindowSkillElement
{
    public override void Window_Interaction_MouseOver_Start()
    {
        base.Window_Interaction_MouseOver_Start();

        Animation_set(gameObject,punchScaleStrengh,baseScale);

        DisplayInfo();
    }

    /// <summary>
    /// When the mouse is over this skill
    /// </summary>
    public override void Window_Interaction_MouseOver_Update()
    {
        base.Window_Interaction_MouseOver_Update();

        DisplayInfo();

        if (Input.GetMouseButtonDown(1))
        {
            CenterCamera();
        }
    }

    /// <summary>
    /// When we exit this skill
    /// </summary>
    public override void Window_Interaction_MouseOver_End()
    {
        base.Window_Interaction_MouseOver_End();

        Animation_scale(gameObject,baseScale.x, punchScaleSpeed_Down);

        Main_UI.Display_Title_Erase();
        Main_UI.Display_Description_Erase();
    }

    public virtual void Cursor() { }
}
