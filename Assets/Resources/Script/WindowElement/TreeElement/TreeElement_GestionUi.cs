using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class TreeElement : WindowSkillElement
{
    public Color32 buy_color;

    public Image contour_white;

    public virtual void UpdateUI() { }

    public static float distanceMultiplicator = 30;

    public override bool DisplayInfo_isShowed()
    {
        return Main_UI.ui_displayDescription_Current_script != null && Main_UI.ui_displayDescription_Current_script.title.text == title;
    }

    public override void DisplayInfo()
    {
        Main_UI.Display_Description(title,description, transform.position, transform.lossyScale.x * distanceMultiplicator);
    }
}
