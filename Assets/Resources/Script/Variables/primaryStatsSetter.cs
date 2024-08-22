using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class primaryStatsSetter : MonoBehaviour
{

    public enum carac { life, str, dex, res, eff, pa, pm }

    public static string caracToString(carac car, int value)
    {
        if (V.IsFr())
        {

            switch (car)
            {
                case carac.life:
                    return value + " pdv";

                case carac.str:
                    return value + " force (Stats rouge)";

                case carac.res:
                    return value + " résistance (Stats bleu)";

                case carac.dex:
                    return value + " dextérité (Stats verte)";

                case carac.eff:
                    return value + " efficacité (Stats jaune)";

                case carac.pa:
                    return value + " pa";

                case carac.pm:
                    return value + " pm";
            }
        }
        else
        {
            switch (car)
            {
                case carac.life:
                    return value + " life";

                case carac.str:
                    return value + " strenght (Stats rouge)";

                case carac.res:
                    return value + " resistance (Stats bleu)";

                case carac.dex:
                    return value + " dexterity (Stats verte)";


                case carac.eff:
                    return value + " efficacité (Stats jaune)";

                case carac.pa:
                    return value + " ap";

                case carac.pm:
                    return value + " mp";
            }
        }

        throw new System.Exception("carac to string not implemented");
    }

    public static Dictionary<list, primaryStatsSetterInfo> dicInfo = new Dictionary<list, primaryStatsSetterInfo>();

    public enum list { str, dex, res, life, eff, ap, mp }

    public static void Init()
    {
        dicInfo.Clear();

        Add(new primaryStatsSetterInfo(list.life, new List<(carac stat, int value)>
        {
            (carac.life,20),
        }, () =>
        {
            return V.player_info.level % 5 != 0;
        }));

        Add(new primaryStatsSetterInfo(list.str, new List<(carac stat, int value)>
        {
            (carac.str,20),
        }, () =>
        {
            return V.player_info.level % 5 != 0;
        }));

        Add(new primaryStatsSetterInfo(list.dex, new List<(carac stat, int value)>
        {
            (carac.dex,20),
        }, () =>
        {
            return V.player_info.level % 5 != 0;
        }));

        Add(new primaryStatsSetterInfo(list.res, new List<(carac stat, int value)>
        {
            (carac.res,20),
        }, () =>
        {
            return V.player_info.level % 5 != 0;
        }));


        var eff = new primaryStatsSetterInfo(list.eff, new List<(carac stat, int value)>
        {
            (carac.eff,1)
        }, () =>
        {
            return V.player_info.level % 3 == 0;
        });

        Add(eff);

        var pa = new primaryStatsSetterInfo(list.ap, new List<(carac stat, int value)>
        {
            (carac.pa,1)
        }, () =>
        {
            return V.player_info.level % 5 == 0;
        });

        Add(pa);

        var pm = new primaryStatsSetterInfo(list.mp, new List<(carac stat, int value)>
        {
            (carac.pm,1)
        }, () =>
        {
            return V.player_info.level % 5 == 0;
        });


        Add(pm);
    }

    private static void Add(primaryStatsSetterInfo info)
    {
        dicInfo.Add(info.id, info);
    }

    private static Dictionary<carac, int> currentStats = new Dictionary<carac, int>();

    public static void AddToCurrentStats(carac car, int value)
    {
        if (currentStats.ContainsKey(car))
        {
            currentStats[car] += value;
        }
        else
        {
            currentStats.Add(car, value);
        }
    }

    public static void ClearCurrentStats()
    {
        currentStats.Clear();
    }

    public static int GetStats(carac car)
    {
        if (currentStats.ContainsKey(car)) return currentStats[car];

        return 0;
    }
}

public class primaryStatsSetterInfo
{
    public primaryStatsSetter.list id;

    public List<(primaryStatsSetter.carac stat, int value)> caracs;

    public primaryStatsSetterInfo(primaryStatsSetter.list stats, List<(primaryStatsSetter.carac stat, int value)> caracs
        , check condition = null)
    {
        this.id = stats;
        this.caracs = caracs;

        if (condition != null) checkIfPossible = condition;
    }

    public string description()
    {
        string desc = "";

        foreach (var element in caracs)
            desc += primaryStatsSetter.caracToString(element.stat, element.value) + "\n";

        return desc;
    }

    public void AddToCurrentStat()
    {
        foreach (var element in caracs)
            primaryStatsSetter.AddToCurrentStats(element.stat, element.value);

        V.player_info.CalculateValue();
    }

    public delegate bool check();

    public check checkIfPossible = () => true;
}
