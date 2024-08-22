using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTalentElement : TreeElement
{
    public override void Window_Interaction_Click()
    {
        base.Window_Interaction_Click();

        AnimationSparkle(25, SparkleAnimationColor.green, this);
    }

    public void SelectionateThisTalent()
    {
        if (Window_skill.ShowTalent)
        {
            Animation_punchScale(gameObject,0.2f,punchScaleSpeed_Up);
        }

        Window_skill.ShowTalent = true;
    }
}
