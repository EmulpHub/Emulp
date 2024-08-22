using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentBoxClose : WindowSkillElement
{
    public Panel_skill_talent talent;

    public override void Window_Interaction_Click()
    {
        base.Window_Interaction_Click();

        Window_Interaction_MouseOver_End();

        window.highLightedSkill = null;

        talent.DeactivateTalentList();
    }
}
