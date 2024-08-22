using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Effect : MonoBehaviour
{
    /*
    public static bool ContainWeaknessEffect(Entity target)
    {
        bool find = false;

        target.GetEffect(GetWeaknessName(), ref find);

        return find;
    }

    public static string GetWeaknessName()
    {
        return V.IsFr() ? "Fragile" : "Weakness";
    }

    public static void ApplyWeakness(Entity target, int duration = 2, string source = "")
    {
        string title = GetWeaknessName();

        bool find = false;

        Effect e = target.GetEffect(title, ref find);

        if (find)
        {
            e.SetDuration(duration);
        }
        else
        {
            target.AddEffect(
                Effect.CreateEffect(effectType.resistance, -33, duration, V.warrior_weakness, source, Reduction_mode.endTurn)
                );

            target.AddCustomEffect(title, duration, V.warrior_weakness, source, Reduction_mode.endTurn,
                new List<(effectType type, int strenght, string title, string description)>
                {
                (Effect.effectType.resistance,/*Talent_Gestion.EquipedTalent(SpellGestion.List.warrior_talent_WeakPoint) ? -50 :*/// -33,"",""),

            //}, false, false);
//        }
/*
        string add = " " + duration + " tours";
        if (!V.IsFr())
            add = " " + duration + " turns";

        Main_UI.Display_movingText_basicValue(GetWeaknessName() + add, V.Color.red, target.transform.position, V.pix_weakness);
    }
*/
}
