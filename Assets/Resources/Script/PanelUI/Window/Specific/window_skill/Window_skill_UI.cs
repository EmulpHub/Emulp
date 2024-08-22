using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Window_skill : Window
{
    public Image resourceImg;
    public Text Skill_point;

    public Sprite resourceImg_skill, resourceImg_nortice;

    public override void Update_Ui()
    {
        if (V.IsFr())
        {
            title.text = "Competence";
        }

        else if (V.IsUk())
        {
            title.text = "Skill";
        }

        if (V.IsInMain)
        {
            resourceImg.sprite = resourceImg_skill;
            Skill_point.text = "" + V.player_info.point_skill + "";
        }
        else
        {
            resourceImg.sprite = resourceImg_nortice;
            Skill_point.text = "" + Ascension.nortice;
        }

        base.Update_Ui();
    }
}
