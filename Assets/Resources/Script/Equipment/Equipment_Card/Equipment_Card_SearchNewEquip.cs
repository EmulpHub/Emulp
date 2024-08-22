using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Equipment_Card : card
{

    public static SingleEquipment.rarity ChooseRarity()
    {
        Dictionary<SingleEquipment.rarity,int> chanceRarity = new Dictionary<SingleEquipment.rarity, int>();

        SingleEquipment.rarity baseRarity = SingleEquipment.rarity.Common; ;

        if(WorldData.distance == 0)
        {
            chanceRarity = new Dictionary<SingleEquipment.rarity, int>
            {
                {SingleEquipment.rarity.Uncommon, 10}
            };
        }
        else if (WorldData.distance == 1)
        {
            chanceRarity = new Dictionary<SingleEquipment.rarity, int>
            {
                {SingleEquipment.rarity.Uncommon,50 },
                {SingleEquipment.rarity.Rare,10 } 
            };
        } else if( WorldData.distance == 2)
        {
            baseRarity = SingleEquipment.rarity.Uncommon;

            chanceRarity = new Dictionary<SingleEquipment.rarity, int>
            {
                {SingleEquipment.rarity.Rare,30 }
            };
        }

        int randomNumber = Random.Range(1, 100 + 1);

        foreach (SingleEquipment.rarity r in SearchEquip_Helper.OrderToCheckRarity)
        {
            if (chanceRarity.ContainsKey(r) && randomNumber <= chanceRarity[r])
            {
                return r;
            }
        }

        return baseRarity;
    }

    public static (bool findOne, List<SingleEquipment> equip) SearchChoiceEquipement(int nbEquip, int level, List<SingleEquipment.type> AutorizedType, SingleEquipment.rarity rar = SingleEquipment.rarity.None)
    {
        List<SingleEquipment> ls = new List<SingleEquipment>();

        bool find = false;

        int max = 0;

        while (nbEquip > 0 && max < 100)
        {
            (bool findOne, SingleEquipment equip) v = SearchNewEquipment(level, AutorizedType, rar, ls);

            if (v.findOne)
            {
                find = true;
                ls.Add(v.equip);
            }

            nbEquip--;
            max++;
            if (max == 100)
                print("while boucle equip");
        }

        return (find, ls);
    }

    public delegate bool Con(SingleEquipment e);

    public static (bool findOne, SingleEquipment equip) SearchNewEquipment(int level, List<SingleEquipment.type> AutorizedType, SingleEquipment.rarity rar = SingleEquipment.rarity.None, List<SingleEquipment> ToIgnore = null)
    {
        if (ToIgnore == null)
            ToIgnore = new List<SingleEquipment>();

        List<SingleEquipment> getLs(Con c)
        {
            List<SingleEquipment> ls = new List<SingleEquipment>();

            foreach (SingleEquipment a in Equipment_Management.LockedLs)
            {
                if (c(a))
                    ls.Add(a);
            }

            return ls;
        }

        (bool find, SingleEquipment e) TrySearch(Con c)
        {
            List<SingleEquipment> ls = getLs(c);

            if (ls.Count > 0)
            {
                return (true, ls[Random.Range(0, ls.Count)]);
            }
            else
            {
                return (false, null);
            }
        }

        if (rar == SingleEquipment.rarity.None)
        {
            rar = ChooseRarity();
        }

        List<int> acceptedLevel = new List<int> { level, level + 1, level - 1 };

        List<Con> lsC = new List<Con>
        {
            (SingleEquipment e) => { return Equipment_Management.LockedLs.Contains(e); },
            (SingleEquipment e) => { return e.Rarity == rar; },
            (SingleEquipment e) => { return AutorizedType.Contains(e.Type); },
            (SingleEquipment e) => { return !ToIgnore.Contains(e); }
        };

        (bool find, SingleEquipment e) v = (false, null);

        while (!v.find && lsC.Count > 0)
        {
            Con cn = (SingleEquipment e) =>
            {
                foreach (Con c in lsC)
                {
                    if (!c(e))
                        return false;
                }

                return true;
            };

            v = TrySearch(cn);

            lsC.RemoveAt(0);
        }

        return v;
    }

    public static SingleEquipment Search_EquipmentByRarity(List<SingleEquipment> ls, SingleEquipment.rarity wantedRarity = SingleEquipment.rarity.None, List<SingleEquipment> ToIgnore = null)
    {
        if (ToIgnore == null)
            ToIgnore = new List<SingleEquipment>();

        Dictionary<SingleEquipment.rarity, List<SingleEquipment>> dic =
            new Dictionary<SingleEquipment.rarity, List<SingleEquipment>>();

        void AddToDic(SingleEquipment b)
        {
            if (dic.ContainsKey(b.Rarity))
            {
                dic[b.Rarity].Add(b);
            }
            else
            {
                dic.Add(b.Rarity, new List<SingleEquipment> { b });
            }
        }

        (bool empty, SingleEquipment e) pickRandomly(List<SingleEquipment> ls_a)
        {
            if (ls_a.Count == 0)
                return (true, null);

            SingleEquipment pick(List<SingleEquipment> ls_b)
            {
                return ls_b[Random.Range(0, ls_b.Count)];
            }

            List<SingleEquipment> new_ls_a = new List<SingleEquipment>(ls_a);

            SingleEquipment e = null;

            bool find = false;

            int maxa = 0;

            while (new_ls_a.Count > 0 && !find && maxa < 100)
            {
                SingleEquipment e_new = pick(new_ls_a);

                if (ToIgnore.Contains(e_new))
                {
                    new_ls_a.Remove(e_new);
                }
                else
                {
                    find = true;
                    e = e_new;
                }

                maxa++;
            }

            if (maxa == 100)
                print("WHILE BOUCLE");

            return (false, e);
        }

        foreach (SingleEquipment a in ls)
        {
            AddToDic(a);
        }

        List<SingleEquipment.rarity> sortedRar = SearchEquip_Helper.sortLs(new List<SingleEquipment.rarity>(dic.Keys));

        SingleEquipment final = null;

        if (wantedRarity != SingleEquipment.rarity.None)
        {
            if (sortedRar.Contains(wantedRarity))
            {
                (bool empty, SingleEquipment e) v = pickRandomly(dic[wantedRarity]);

                final = v.e;
            }
        }

        int IncreaseChance = 0, max = 0;

        while (final == null && max < 100 && sortedRar.Count > 0)
        {
            foreach (SingleEquipment.rarity a in sortedRar)
            {
                float chance = 0;

                switch (a)
                {
                    case SingleEquipment.rarity.Rare:
                        chance = 20;
                        break;
                    case SingleEquipment.rarity.Uncommon:
                        chance = 40;
                        break;
                    case SingleEquipment.rarity.Common:
                        chance = 100;
                        break;
                }

                float rng = Random.Range(0, 100 + 1 - IncreaseChance);

                if (rng <= chance)
                {
                    (bool empty, SingleEquipment e) v = pickRandomly(dic[a]);

                    if (v.empty)
                    {
                        sortedRar.Remove(a);
                    }
                    else
                    {
                        final = v.e;
                    }

                    break;
                }
            }

            max++;
            if (max == 100)
                print("MAX FOR PICK RANDOM");

            IncreaseChance += 5;
        }

        return final;
    }

    public static List<SingleEquipment> Search_EquipmentByType(List<SingleEquipment> ls, List<SingleEquipment.type> AutorizedType)
    {
        Dictionary<SingleEquipment.type, List<SingleEquipment>> dic =
            new Dictionary<SingleEquipment.type, List<SingleEquipment>>();

        void AddToDic(SingleEquipment b)
        {
            if (dic.ContainsKey(b.Type))
            {
                dic[b.Type].Add(b);
            }
            else
            {
                dic.Add(b.Type, new List<SingleEquipment> { b });
            }
        }

        foreach (SingleEquipment a in ls)
        {
            if (AutorizedType.Contains(a.Type))
            {
                AddToDic(a);
            }
        }

        if (dic.Count == 0)
            return ls;

        List<SingleEquipment.type> typeLs = new List<SingleEquipment.type>(dic.Keys);

        return dic[
            typeLs[Random.Range(0, dic.Count)]
            ];
    }
}

public class SearchEquip_Helper
{
    public static List<SingleEquipment.rarity> sortLs(List<SingleEquipment.rarity> a)
    {
        List<SingleEquipment.rarity> n = new List<SingleEquipment.rarity>();

        SingleEquipment.rarity rar = SingleEquipment.rarity.Common;

        while (a.Count > 0)
        {
            int i = 0;
            int Higher = -1;

            while (i < a.Count)
            {
                int nb = ((int)a[i]);

                if (nb > Higher)
                {
                    Higher = nb;
                    rar = a[i];
                }

                i++;
            }

            n.Add(rar);
            a.Remove(rar);
        }

        return n;
    }

    public static List<SingleEquipment.type> All = new List<SingleEquipment.type>
    { SingleEquipment.type.weapon,SingleEquipment.type.boot,SingleEquipment.type.chest,
        SingleEquipment.type.belt,SingleEquipment.type.object_equipment,
        SingleEquipment.type.pant, SingleEquipment.type.helmet};


    public static List<SingleEquipment.rarity> OrderToCheckRarity = new List<SingleEquipment.rarity>
    {SingleEquipment.rarity.Rare,SingleEquipment.rarity.Uncommon,SingleEquipment.rarity.Common };
}