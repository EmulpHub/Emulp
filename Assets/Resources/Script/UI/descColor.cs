using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class descColor : MonoBehaviour
{
    public static void initColor()
    {
        dmg = "FF3F34";
        dex = "0BE881";
        inf = "808E9B";
        inf_light = "D2DAE2";
        eff = "FFE845";
        spe = "FFFFFF";
        res = "00d8d6";
    }

    public static List<string> AllPossibleColor = new List<string> {
        "dmg", //DAMAGE
        "str", //stats red
        "res", //stats blue
        "dex", //stats green
        "eff", //stats yellow
        "inf", //INFO
        "inl", //INFO Light
        "spe", //Blanc
        "hea", //heal
        "arm", //arm
        "bon",
        "mal"
    };

    public static string convert(string desc)
    {
        if (desc is null)
            return "";

        int i = 0;
        while (i < desc.Length)
        {
            if (desc[i] == '*')
            {
                string a = desc.Substring(i + 1, 3).ToLower();

                desc = desc.Remove(i, 4);

                desc = desc.Insert(i, convertCodeToColorHtml(a));
            }

            i++;
        }

        return RemoveDoubleSpace(desc);
    }

    public static string convert_noColorChange(string desc)
    {
        if (desc is null)
            return "";

        int i = 0;
        while (i < desc.Length)
        {
            if (desc[i] == '*')
            {
                desc = desc.Remove(i, 4);
            }

            i++;
        }

        return RemoveDoubleSpace(desc);
    }


    static string dmg, eff, dex, res;

    static string inf, inf_light, spe;

    public static string convertCodeToColorHtml(string a)
    {
        switch (a)
        {
            case "dmg":
                return "<color=#" + dmg + ">";

            case "bon":
                return "<color=#" + dex + ">";

            case "mal":
                return "<color=#" + dmg + ">";
            case "str":
                return "<color=#" + dmg + ">";
            case "eff":
                return "<color=#" + eff + ">";
            case "dex":
                return "<color=#" + dex + ">";
            case "res":
                return "<color=#" + res + ">";
            case "hea":
                return "<color=#" + res + ">";
            case "arm":
                return "<color=#" + res + ">";
            case "inf":
                return "<color=#" + inf + ">";
            case "inl":
                return "<color=#" + inf_light + ">";
            case "spe":
                return "<color=#" + spe + ">";
            case "end":
                return "</color>"; 
            default:
                throw new System.Exception("The code showed is not valid (ConversionDescWithColor) with txt = " + a);
        }
    }


    public static string RemoveDoubleSpace(string desc)
    {
        desc.Trim();

        bool containSpaceBetweenHtmlCode = false;

        int i = 0;
        while (i < desc.Length - 1)
        {
            if (desc[i] == ' ')
            {
                if (desc[i + 1] == ' ')
                {
                    desc = desc.Remove(i, 1);
                    i--;
                }
            }
            else if (desc[i] == '<')
            {
                containSpaceBetweenHtmlCode = i > 0 && desc[i - 1] == ' ';
            }
            else if (desc[i] == '>')
            {
                if (containSpaceBetweenHtmlCode && desc[i + 1] == ' ')
                {
                    desc = desc.Remove(i + 1, 1);
                    containSpaceBetweenHtmlCode = false;
                }
            }

            i++;
        }

        return desc.Trim();
    }
}
