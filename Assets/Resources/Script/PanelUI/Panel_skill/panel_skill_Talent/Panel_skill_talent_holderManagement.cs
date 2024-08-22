using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Panel_skill_talent : TreeElement
{
    public GameObject talentIndividual, talentIndividual_parent;

    public void talentHolderCreateIndividual()
    {
        int i = 0;

        foreach (Talent_Gestion.talent t in new List<Talent_Gestion.talent>(Talent_Gestion.ls.Keys))
        {
            InstantiateTalentIndividual(t, i);

            i++;
        }
    }

    public Vector3 DisplayUiDescriptionPosition_AdditionalPos;

    [HideInInspector]
    public Vector3 DisplayUiDescriptionPosition;

    public void DisplayUiDescriptionPosition_Update()
    {
        DisplayUiDescriptionPosition = transform.position + DisplayUiDescriptionPosition_AdditionalPos * transform.lossyScale.x;
    }

    public void InstantiateTalentIndividual(Talent_Gestion.talent t, int i)
    {
        GameObject g = Instantiate(talentIndividual, talentIndividual_parent.transform);

        Talent_Individual talentIndividual_script = g.GetComponent<Talent_Individual>();

        talentIndividual_script.Initialization();

        talentIndividual_script.SetInfo(t);

        talentIndividual_script.SetPosition(i);

        talentIndividual_script.parentHolder = this;
    }
}
