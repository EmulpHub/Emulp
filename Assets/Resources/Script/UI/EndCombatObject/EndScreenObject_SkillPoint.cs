using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenObject_SkillPoint : EndScreenObject
{
    public override void Init()
    {
        base.Init();
    }

    string getTitle()
    {
        return V.IsFr() ? "+1 competence" : "+1 skill";
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();

        Main_UI.Display_Title(getTitle(), transform.position, dis);
    }

    public override void EraseDisplayInfo()
    {
        base.EraseDisplayInfo();

        Main_UI.Display_Title_Erase(getTitle());
    }

    public static void Add(Transform parent)
    {
        GameObject g = Instantiate(Resources.Load<GameObject>("Prefab/UI/EndScreenObjectHolder_skill"), parent);

        V.player_info.point_skill++;
    }
}
