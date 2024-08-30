using System.Collections.Generic;
using UnityEngine;

public partial class SpellGestion : MonoBehaviour
{
    public static List<char> number = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    public static string Get_Description(List spell, bool baseValue = true, Spell e = null)
    {
        string desc = info[spell].description;

        if (baseValue)
            return desc;

        string finalDesc = "";

        int i = 0;

        string current = "";
        string colorTag = "";

        while (i < desc.Length)
        {
            char c = desc[i];

            if (c == '*')
            {
                string v = desc.Substring(i + 1, 3);

                if (!Calc.ContainsKey(v) && current == "")
                {
                    finalDesc += c;
                    i++;

                    continue;
                }

                if (v == "end")
                {
                    if (current != "")
                    {
                        string total = "";
                        string totalWithoutnb = "";

                        string nb_string = "";

                        bool itsNumber = false;

                        foreach (char ca in current)
                        {
                            if (number.Contains(ca))
                            {
                                itsNumber = true;

                                nb_string += ca;


                            }
                            else
                            {
                                if (itsNumber)
                                {
                                    int.TryParse(nb_string, out int nb);

                                    if (e is null)
                                        throw new System.Exception("spell can't be null ? je crois ?");

                                    total += "" + calcValueDesc(colorTag, nb, baseValue, e, current);

                                    itsNumber = false;

                                    nb_string = "";
                                }

                                total += ca;
                                totalWithoutnb += ca;
                            }
                        }

                        if (itsNumber)
                        {
                            int.TryParse(nb_string, out int nb);

                            if (e is null)
                                throw new System.Exception("spell can't be null ? je crois ?");

                            total += "" + calcValueDesc(colorTag, nb, baseValue, e, totalWithoutnb);
                        }

                        finalDesc += "*" + colorTag + total + "*end";

                        current = "";
                        colorTag = "";
                    }
                }
                else
                {
                    colorTag = v;
                }

                i += 3;
            }
            else if (colorTag != "")
            {
                current += c;
            }
            else
            {
                finalDesc += c;
            }

            i++;
        }


        return finalDesc;
    }

    public static int calcValueDesc(string tag, int value, bool baseValue, Spell sp, string current)
    {
        if (!baseValue && Calc.ContainsKey(tag))
        {
            return Calc[tag](value, sp, current);
        }
        else
        {
            return value;
        }
    }

    public delegate int tagCalc(int value, Spell e, string current);

    public static Dictionary<string, tagCalc> Calc = new Dictionary<string, tagCalc>();

    public static void get_description_calcValue_init()
    {
        Calc.Clear();

        Calc.Add("dmg", delegate (int v, Spell e, string current)
       {
           return spellEffect.calc_stat(V.player_info.CalcDamage(v), e.Multiplicator, e);
       });

        Calc.Add("str", delegate (int v, Spell e, string current)
        {
            return spellEffect.calc_stat(V.player_info.CalcStrStats(v), e.Multiplicator, e);
        });

        Calc.Add("dex", delegate (int v, Spell e, string current)
        {
            return spellEffect.calc_stat(V.player_info.CalcDexStats(v), e.Multiplicator, e);
        });

        Calc.Add("res", delegate (int v, Spell e, string current)
        {
            return spellEffect.calc_stat(V.player_info.CalcResStats(v), e.Multiplicator, e);
        });

        Calc.Add("eff", delegate (int v, Spell e, string current)
        {
            return spellEffect.calc_stat(V.player_info.CalcEffStats(v), e.Multiplicator, e);
        });

        Calc.Add("spe", delegate (int v, Spell e, string current)
        {
            return v;
        });

        Calc.Add("hea", delegate (int v, Spell e, string current)
        {
            return spellEffect.calc_stat(V.player_info.CalcHeal(v), e.Multiplicator, e);
        });

        Calc.Add("arm", delegate (int v, Spell e, string current)
        {
            return spellEffect.calc_stat(V.player_info.calcArmor(v), e.Multiplicator, e);
        });
    }
}