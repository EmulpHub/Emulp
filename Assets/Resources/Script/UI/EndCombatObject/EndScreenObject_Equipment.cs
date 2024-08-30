using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenObject_Equipment : EndScreenObject
{
    [HideInInspector]
    public SingleEquipment e;

    public Image rarIndicator;

    public override void Init()
    {
        base.Init();

        rarIndicator.color = V.GetColor(e.getColorFromRarity());
        graphique.sprite = e.Graphic;
    }

    string t = "";
    bool isDesc = true;

    public override void DisplayInfo()
    {
        base.DisplayInfo();

        if (e.effects_type.Count == 0)
        {
            //V.Color RarColor = equipment.getColorFromRarity();

            Main_UI.Display_Title(e.GetTitle(), transform.position, dis);
            Description_text.EraseDispay();

            isDesc = false;

            t = e.GetTitle();
        }
        else
        {
            isDesc = true;

            Main_UI.Display_Title_Erase();

            t = Slot_Equiped.DisplayEquipmentInfo(e, transform.position, dis);
        }
    }

    public override void EraseDisplayInfo()
    {
        base.EraseDisplayInfo();

        if (isDesc)
            Description_text.EraseDispay(t);
        else
            Main_UI.Display_Title_Erase(t);
    }

    public static void Add(Transform parent, SingleEquipment e)
    {
        GameObject g = Instantiate(Resources.Load<GameObject>("Prefab/UI/EndScreenObjectHolder_Equipment"), parent);

        EndScreenObject_Equipment s = g.GetComponent<EndScreenObject_Equipment>();

        s.e = e;

        Equipment_Management.ObtainNewEquipment(e);
    }
}
