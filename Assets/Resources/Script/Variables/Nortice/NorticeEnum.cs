using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NorticeEnum : MonoBehaviour
{
    public enum Value { relic, passive}

    public static void CreateList ()
    {
        list.Clear();

        AddInfo(Value.relic,10);
        AddInfo(Value.passive,20);

        if (V.IsFr())
        {
            ModifyTitleDescription(Value.relic, "Relic", "Au debut de la partie choisisez une relique parmis 3 aléatoire");
            ModifyTitleDescription(Value.passive, "Passif", "Au debut de la partie choisisez un passif parmis 3 spécifique a votre classe choisi");
        }
        else
        {
            ModifyTitleDescription(Value.relic, "Relic", "At start of the game choose a relic from 3 random one");
            ModifyTitleDescription(Value.passive, "Passive", "At start of the game choose a passive from 3 specific to your class");
        }
    }

    private static Dictionary<Value, NorticeInfo> list = new Dictionary<Value, NorticeInfo>();

    public static void AddInfo (Value val,int fragmentCost)
    {
        NorticeInfo info = new NorticeInfo(val,fragmentCost);

        list.Add(val, info);
    }


    public static void ModifyTitleDescription(Value val, string title,string description)
    {
        NorticeInfo info = list[val];

        info.title = title;
        info.description = description;
    }

    public static bool Purchased (Value val)
    {
        return Get(val).purchased;
    }

    public static NorticeInfo Get (Value val)
    {
        return list[val];
    }
}

public class NorticeInfo
{
    public bool purchased = false;

    public NorticeEnum.Value value;

    public string title = "ERROR in setting title value", description = "ERROR in setting description value";

    public int fragmentCost;

    public Sprite img,img_grey;

    public NorticeInfo(NorticeEnum.Value value, int fragmentCost)
    {
        this.value = value;
        this.fragmentCost = fragmentCost;

        img = Resources.Load<Sprite>("Image/Notrice/" +value.ToString());
        img_grey = Resources.Load<Sprite>("Image/Notrice/" + value.ToString() +"_gray");
    }
}