using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipClass_init : MonoBehaviour
{
    public enum Value
    {
        none, heal5,armor5,lifeSteal10, strenght30, pv50,spikeShard, spellIncreaseRight,AccumulationWhenDamage,
        ArmEffect30,ObjectEffect30,Pa1,Pm1
    }

    public static Dictionary<Value, EquipInfo> dicoEquipInfo = new Dictionary<Value, EquipInfo>();

    public static EquipInfo GetInfo (Value type)
    {
        return dicoEquipInfo[type];
    }

    public static EquipClass CreateClass (Value type)
    {
        EquipClass equipClass = (EquipClass)new GameObject("EquipEffect").AddComponent(Type.GetType("EquipClass_"+ type.ToString()));

        equipClass.transform.parent = V.inGameCreatedGameobjectHolder;

        return equipClass;
    }

    public static void Init ()
    {
        dicoEquipInfo.Clear();

        Add(Value.none, "error");
        Add(Value.heal5, "Infligez des dégâts donne *spe1 stack*end\nA la fin du tour soignez *res10*end + *res2*end par stack si vous avez au moins *spe1 stack*end et perdez votre stack");
        Add(Value.armor5, "+5% de vos pv max en armure au debut du tour");
        Add(Value.lifeSteal10, "+10% vol de vie");
        Add(Value.strenght30,  "+30 force");
        Add(Value.pv50, "+50 pv");
        Add(Value.spikeShard, "Lorsque vous gagnez de l'armure gagnez 1 morceau d'epine à 3 morceau gagnez 1 epine");
        Add(Value.spellIncreaseRight, "Vos sorts de droite ont +20% d'effet");
        Add(Value.AccumulationWhenDamage, "Subir des dégats donne 1 accumulation");
        Add(Value.ArmEffect30, "Votre arme a +30% d'effet");
        Add(Value.ObjectEffect30, "Vos objet a +30% d'effet");
        Add(Value.Pa1, "+1 pa");
        Add(Value.Pm1, "+1 pm");

    }

    public static void Add(Value type,string description)
    {
        dicoEquipInfo.Add(type, new EquipInfo( description));
    }
}

public class EquipInfo
{
    public string description;

    public EquipInfo( string description)
    {
        this.description = description;
    }
}