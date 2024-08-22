using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SingleEquipment))]
public class Equipment_Single_Editor : Editor
{
    public string Text_Label;

    public bool verifiyAllEquipment;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SingleEquipment e = (SingleEquipment)target;

        Text_Label = "In dev";

        EditorGUILayout.LabelField("-- " + state(e) + " --", EditorStyles.boldLabel);

        EditorGUILayout.LabelField("------------------------", EditorStyles.boldLabel);

        EditorGUILayout.LabelField(AutorizedValue(e), EditorStyles.boldLabel);
        EditorGUILayout.LabelField(AutorizedValue(e, false), EditorStyles.boldLabel);

    }

    public static string state(SingleEquipment e)
    {
        SingleEquipment.activeAndBonusEffect_init();

        //Secondariy
        SingleEquipment.rarity rar = e.Rarity;

        int nbOfSecondaryEffectAllowed = 0;

        if (e.Rarity == SingleEquipment.rarity.Uncommon)
            nbOfSecondaryEffectAllowed = 1;
        else if (e.Rarity == SingleEquipment.rarity.Rare)
            nbOfSecondaryEffectAllowed = 2;

        if (e.Type == SingleEquipment.type.object_equipment || e.Type == SingleEquipment.type.object_equipment_2 || e.Type == SingleEquipment.type.weapon)
            nbOfSecondaryEffectAllowed = 0;

        if (e.secondaryEffect.Count != nbOfSecondaryEffectAllowed)
        {
            return "Effet secondaire incorrect, nombre voulu = " + nbOfSecondaryEffectAllowed + " ";
        }

        (SingleEquipment.rarity rar, SingleEquipment.value_type t) v = (rar, e.primaryEffect);

        //Primary
        if (!SingleEquipment.activeAndBonusEffect.ContainsKey(v) || SingleEquipment.activeAndBonusEffect[v].primary <= 0)
        {
            return "Primary not good";
        }

        //Secondary
        List<SingleEquipment.value_type> secondaryEffect = e.secondaryEffect;

        foreach (SingleEquipment.value_type t in secondaryEffect)
        {
            v = (rar, t);

            if (!SingleEquipment.activeAndBonusEffect.ContainsKey(v) || SingleEquipment.activeAndBonusEffect[v].secondary <= 0)
            {
                return "Secondary not good = " + t.ToString();
            }
        }

        return "OK";
    }

    public string AutorizedValue(SingleEquipment e, bool showPrimary = true)
    {
        string primary = "Possible primary : ", secondary = "Possible secondary : ";

        SingleEquipment.activeAndBonusEffect_init();

        SingleEquipment.rarity rar = e.Rarity;

        foreach (SingleEquipment.value_type t in System.Enum.GetValues(typeof(SingleEquipment.value_type)))
        {
            (SingleEquipment.rarity r, SingleEquipment.value_type t) v = (e.Rarity, t);

            if (SingleEquipment.activeAndBonusEffect.ContainsKey(v))
            {
                int p = SingleEquipment.activeAndBonusEffect[v].primary;

                int s = SingleEquipment.activeAndBonusEffect[v].secondary;

                if (p > 0)
                {
                    primary += " " + v.t.ToString();
                }
                if (s > 0)
                {
                    secondary += " " + v.t.ToString();
                }
            }
        }

        if (showPrimary)
            return primary;
        else
            return secondary;
    }
}
