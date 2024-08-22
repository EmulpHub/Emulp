using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Origin_Passive : MonoBehaviour
{
    public enum Value
    {
        none, rage, intensity, melee, pawn, isolation, control, overflow, rising, traveler
    }

    public static List<Value> player_passive = new List<Value>();

    public static Dictionary<Value, passiveInfo> dicPassivePassiveInfo = new Dictionary<Value, passiveInfo>();

    public static Value GetPlayerPassive ()
    {
        if (player_passive.Count == 0) return Value.none;

        return player_passive[0];
    }

    public static passiveInfo Get(Value passive)
    {
        return dicPassivePassiveInfo[passive];
    }

    public static string Get_Title (Value passive)
    {
        return Get(passive).title;
    }

    public static string Get_Description(Value passive)
    {
        return Get(passive).description;
    }

    public static Sprite Get_Image(Value passive)
    {
        return Get(passive).image;
    }

    public static void AddPlayerPassive(Value l)
    {
        if (l == Value.none) return;

        player_passive.Add(l);

        Get(l).Add();
    }

    public static bool ContainPlayer(Value l)
    {
        return player_passive.Contains(l);
    }

    public static void ClearPlayer()
    {
        foreach (Value l in player_passive)
        {
            Get(l).Remove();
        }

        player_passive.Clear();
    }

    public static void Initialize()
    {
        if (dicPassivePassiveInfo.Count > 0) return;

        if (V.IsFr())
        {
            AddOriginPassive(Value.rage, "Rage", "Début du combat : +50 puissance");
            AddOriginPassive(Value.intensity, "Intensité", "10% de soins par tour");
            AddOriginPassive(Value.melee, "Mêlée", "Début du tour : *bon+20%*end d'effet pour chaque ennemie a moins de 3 cases");
            //AddOriginPassive(Value.pawn, "Pion", "Lorsque une entité est poussé, attiré ou téléportez gagnez *dmg+5% dégâts*end jusqu'a la fin du combat (cumulable)");
            //AddOriginPassive(Value.isolation, "Isolation", "Début du tour : Si aucun ennemie est a moins de 3 cases gagnez *bon+30% d'effet*end pour le tour");
            //AddOriginPassive(Value.control, "Controle", "Fin du tour : +1 po par ennemie en ligne et en diagnoal\nSi vous gagnez 3 po ou plus gagnez également *bon+30% de dégâts*end pour ce tour");
            //AddOriginPassive(Value.overflow, "Overflow", "Utiliser un sort vous donne de l'effet supplementaire pour ce tour dépendant des pa utilisé *bon(5% par pa)*end");
            //AddOriginPassive(Value.rising, "Evolution", "Début du combat : *mal-50% d'effet*end\nFin du tour: *bon+10% d'effet*end\nDépensez 12 pa donne *bon+10% d'effet*end");
            //AddOriginPassive(Value.traveler, "Voyageur", "*bon+5% d'effet*end pour chaque pm a votre disposition\nCette effect ne peux jamais être réduit en combat");
        }
        else
        {
            AddOriginPassive(Value.rage, "Rage", "Start of the fight : *bon+50 power*end");
            AddOriginPassive(Value.intensity, "Intensity", "*bon+10%*end heal each turn");
            AddOriginPassive(Value.melee, "Melee", "Start of turn : *bon+20%*end effect for each ennemy at least 3 square away");
            //AddOriginPassive(Value.pawn, "Pawn", "When an entity is push, attracted or teleported gain *dmg+5% damage*end for the end of the combat (cumulable)");
            //AddOriginPassive(Value.isolation, "Isolation", "Start of turn : If any ennemy is around 3 square away gain *bon+30% effect*end for this turn");
            //AddOriginPassive(Value.control, "Control", "End of turn : +1 po for each ennemy in line and in diagonal\nIf you gain 3 or more po also gain *bon+30% more damage*end this turn");
            //AddOriginPassive(Value.overflow, "Overflow", "Using spell give you additional effect depending of the ap cost of the spell used *bon(5% per ap)*end");
            //AddOriginPassive(Value.traveler, "Traveler", "*bon+5% effect*end for each pm you have\nThis effect can never lower during fight");
            //AddOriginPassive(Value.rising, "Evolution", "Start of fight : *mal-50% effect*end\nEnd turn: *bon+10% effect*end\nUsing 12 pa give you *bon+10% effect*end");
        }
    }

    public static void AddOriginPassive(Value t, string title, string description)
    {
        passiveInfo inf = (passiveInfo)Activator.CreateInstance(Type.GetType("passive_"+t.ToString()));

        dicPassivePassiveInfo.Add(t, inf);

        inf.title = title;
        inf.description = description;

        inf.image = Resources.Load<Sprite>("Image/origin/passive" + t.ToString());
    }
}
