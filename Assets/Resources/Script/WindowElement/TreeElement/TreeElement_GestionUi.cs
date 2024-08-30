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
        return DescriptionStatic.DescriptionPrefab != null && DescriptionStatic.CurrentDescription_script.title.text == title;
    }

    public override void DisplayInfo()
    {
        Description_text.Display(title, description, transform.position, transform.lossyScale.x * distanceMultiplicator);
    }
}
