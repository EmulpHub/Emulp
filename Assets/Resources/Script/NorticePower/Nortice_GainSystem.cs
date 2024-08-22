using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nortice_GainSystem : MonoBehaviour
{
    public static List<category> orderToShowNorticeGain = new List<category>
    { category.levelUp, category.monster_death,category.AscensionSucces,category.AscensionModifier };
    public enum category { monster_death, levelUp, AscensionSucces, AscensionModifier }

    public static Dictionary<category, notriceGainInfo> gainedNortice = new Dictionary<category, notriceGainInfo>();


    public static void AddNotricePoint_monster_death(Monster m)
    {
        if (m.monsterInfo.IsAnInvocation)
            return;

        category c = category.monster_death;

        notriceGainInfo_monsterDeath i = null;

        if (gainedNortice.ContainsKey(c))
        {
            i = (notriceGainInfo_monsterDeath)gainedNortice[c];
        }
        else
        {
            i = new notriceGainInfo_monsterDeath();

            gainedNortice.Add(c, i);
        }

        i.Add_monster(m);

        Window_Ascension.UpdateWindow();
    }

    public static void AddNotricePoint_LevelUp()
    {
        category c = category.levelUp;

        notriceGainInfo_levelUp i = null;

        if (gainedNortice.ContainsKey(c))
        {
            i = (notriceGainInfo_levelUp)gainedNortice[c];
        }
        else
        {
            i = new notriceGainInfo_levelUp();

            gainedNortice.Add(c, i);
        }

        i.Add(1);

        Window_Ascension.UpdateWindow();
    }

    public static void AddNotricePoint_AscensionSucces()
    {
        category c = category.AscensionSucces;

        notriceGainInfo_ascensionSucces i = null;

        if (gainedNortice.ContainsKey(c))
        {
            i = (notriceGainInfo_ascensionSucces)gainedNortice[c];
        }
        else
        {
            i = new notriceGainInfo_ascensionSucces();

            gainedNortice.Add(c, i);
        }

        i.Add(Ascension.currentAscension);

        Window_Ascension.UpdateWindow();
    }

    public static void AddNotricePoint_AscensionModifier()
    {
        category c = category.AscensionModifier;

        notriceGainInfo_ascensionModifier i = null;

        if (gainedNortice.ContainsKey(c))
        {
            i = (notriceGainInfo_ascensionModifier)gainedNortice[c];
        }
        else
        {
            i = new notriceGainInfo_ascensionModifier();

            gainedNortice.Add(c, i);
        }

        i.Add(Ascension.currentAscension);

        Window_Ascension.UpdateWindow();
    }
}

public class notriceGainInfo
{
    public Nortice_GainSystem.category c;
    public int amount, gain;

    public notriceGainInfo(Nortice_GainSystem.category c)
    {
        this.c = c;
        this.amount = 0;
    }

    public virtual void Add(int v)
    {
        gain += v;
        Window_Ascension.AddNorticePowerDisplay(v);
    }

    public virtual string GetLeftString()
    {
        return "null";
    }


    public virtual string GetRightString()
    {
        return "+" + gain;
    }
}

public class notriceGainInfo_monsterDeath : notriceGainInfo
{

    public notriceGainInfo_monsterDeath() : base(Nortice_GainSystem.category.monster_death)
    {

    }

    public void Add_monster(Monster m)
    {
        Add(1);
        amount += 1;
    }

    public override string GetLeftString()
    {
        return V.IsFr() ? "Monstres tués (" + amount + ")" : "Monster killed (" + amount + ")";
    }
}

public class notriceGainInfo_levelUp : notriceGainInfo
{

    public notriceGainInfo_levelUp() : base(Nortice_GainSystem.category.levelUp)
    {

    }

    public override string GetLeftString()
    {
        return V.IsFr() ? "Niveau joueur (" + amount + ")" : "Player level (" + amount + ")";
    }

    public override void Add(int v)
    {
        amount++;
        base.Add(v);
    }
}

public class notriceGainInfo_ascensionSucces : notriceGainInfo
{

    public notriceGainInfo_ascensionSucces() : base(Nortice_GainSystem.category.AscensionSucces)
    {

    }

    public override string GetLeftString()
    {
        return V.IsFr() ? "Ascension réussis !" : "Succesful ascension !";
    }

    /// <summary>
    /// v = currentAscension
    /// </summary>
    /// <param name="v"></param>
    public override void Add(int v)
    {
        int result = 10 + v * 5;

        base.Add(result);
    }
}

public class notriceGainInfo_ascensionModifier : notriceGainInfo
{

    public notriceGainInfo_ascensionModifier() : base(Nortice_GainSystem.category.AscensionModifier)
    {

    }

    public override string GetLeftString()
    {
        return V.IsFr() ? "Ascension bonus(+" + calcPercentage(Ascension.currentAscension) + "%)" : "Bonus Ascension(+" + calcPercentage(Ascension.currentAscension) + "%)";
    }

    public int calcPercentage(int asc)
    {
        return 10 + (asc / 2) * 5;
    }

    /// <summary>
    /// v = currentAscension
    /// </summary>
    /// <param name="v"></param>
    public override void Add(int v)
    {
        int result = Mathf.CeilToInt((float)NotriceGainShow.GetTotal(Nortice_GainSystem.gainedNortice) * ((float)calcPercentage(v) / 100) + 1);

        result = Mathf.Clamp(0, 1, 999999);

        base.Add(result);
    }
}

/*
            case Nortice_GainSystem.category.levelUp:
                return V.IsFr() ? "Niveau joueur (" + amount + ")" : "Player level (" + amount + ")";
*/
