using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenObject_Talent : EndScreenObject
{
    public Talent_Gestion.talent t;

    public override void Init()
    {
        base.Init();

        graphique.sprite = Talent_Gestion.GetSprite(t);
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();

        Description_text.Display(t,transform.position,dis);
    }

    public override void EraseDisplayInfo()
    {
        base.EraseDisplayInfo();

        Description_text.EraseDispay();
    }

    public static void Add(Transform parent, Talent_Gestion.talent t)
    {
        GameObject g = Instantiate(Resources.Load<GameObject>("Prefab/UI/EndScreenObjectHolder_Talent"), parent);

        EndScreenObject_Talent s = g.GetComponent<EndScreenObject_Talent>();

        s.t = t;

        Talent_Gestion.UnlockTalent(t);
    }
}
