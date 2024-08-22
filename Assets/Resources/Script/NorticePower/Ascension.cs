using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ascension : MonoBehaviour
{
    public enum ModifierType { monster_damage, monster_life }

    public static Dictionary<ModifierType, float> ascension_modifier = new Dictionary<ModifierType, float>();

    public static Dictionary<int, List<(ModifierType, float)>> allAscensionModifier = new Dictionary<int, List<(ModifierType, float)>>();

    public static int HigherAscension = 0;

    public static int HigherUnlockedAscension = 0;

    public static int nortice = 0;

    public static void AscensionManagement()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            RaiseAscension();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            LowAscension();
        }
    }

    public static void RaiseAscension()
    {
        if (HigherUnlockedAscension >= currentAscension + 1)
        {
            SetNewAscension(currentAscension + 1);
        }
    }

    public static void LowAscension()
    {
        if (currentAscension != 0)
        {
            SetNewAscension(currentAscension - 1);
        }
    }

    public static void InitAllAscensionModifier()
    {
        allAscensionModifier.Clear();
        HigherAscension = 0;

        //1
        AddAscensionModifier(1, ModifierType.monster_damage, 30);
        AddAscensionModifier(1, ModifierType.monster_life, 30);

        //2
        AddAscensionModifier(2, ModifierType.monster_damage, 50);
        AddAscensionModifier(2, ModifierType.monster_life, 50);

        //3
        AddAscensionModifier(3, ModifierType.monster_damage, 120);
        AddAscensionModifier(3, ModifierType.monster_life, 120);
    }

    public static void AddAscensionModifier(int ascension, ModifierType type, float str)
    {
        if (allAscensionModifier.ContainsKey(ascension))
        {
            allAscensionModifier[ascension].Add((type, str));
        }
        else
        {
            allAscensionModifier.Add(ascension, new List<(ModifierType, float)> { (type, str) });
        }

        if (ascension > HigherAscension)
            HigherAscension = ascension;
    }

    public static int currentAscension = 0;

    public static Dictionary<ModifierType, float> currentAscensionModifier = new Dictionary<ModifierType, float>();

    public static string ConvertAscensionModifierIntoString(Dictionary<ModifierType, float> ls)
    {
        List<ModifierType> order = new List<ModifierType> { ModifierType.monster_damage, ModifierType.monster_life };

        string txt = "";

        foreach (ModifierType t in order)
        {
            if (ls.ContainsKey(t))
                txt += ConvertModifierIntoString(t, ls[t]) + "\n";
        }

        if (txt.Length == 0)
            return V.IsFr() ? "normal monster" : "monstre normaux";

        return txt;
    }

    public static string ConvertModifierIntoString(ModifierType t, float v)
    {
        string txt = "";

        switch (t)
        {
            case ModifierType.monster_damage:
                txt += (V.IsFr() ? "Dégâts des monstres: " : "monster damage: ") + v + " %";
                break;

            case ModifierType.monster_life:
                txt += (V.IsFr() ? "Vie des monstres: " : "monster life: ") + v + " %";
                break;

        }

        return txt;
    }

    public static void SetNewAscension(int ascension)
    {
        currentAscension = ascension;
        currentAscensionModifier = calcAscensionModifier(ascension);
    }

    public static float GetAscensionParameter(ModifierType type)
    {
        if (currentAscensionModifier.ContainsKey(type))
        {
            return currentAscensionModifier[type] + 100;
        }

        return 100;
    }

    public static Dictionary<ModifierType, float> calcAscensionModifier(int ascension, bool individual = false)
    {
        Dictionary<ModifierType, float> ls = new Dictionary<ModifierType, float>();

        void add(ModifierType t, float f)
        {
            if (ls.ContainsKey(t))
            {
                ls[t] += f;
            }
            else
            {
                ls.Add(t, f);
            }
        }

        if (!individual)
        {

            for (int i = 1; i <= ascension; i++)
            {
                List<(ModifierType t, float v)> l = allAscensionModifier[i];

                for (int o = 0; o < l.Count; o++)
                {
                    add(l[o].t, l[0].v);
                }
            }

            return ls;

        }
        else
        {
            List<(ModifierType t, float v)> l = allAscensionModifier[ascension];

            for (int o = 0; o < l.Count; o++)
            {
                add(l[o].t, l[0].v);
            }

            return ls;
        }
    }

    public static void Win()
    {
        if (HigherAscension > HigherUnlockedAscension)
            HigherUnlockedAscension++;

        RaiseAscension();

        LoadAscensionScene();

        GainNorticPower();

        Save_SaveSystem.SaveGame_WithoutWarning();

    }

    public static void Loose()
    {
        GainNorticPower();

        LoadAscensionScene();

        Save_SaveSystem.SaveGame_WithoutWarning();

    }

    public static void GainNorticPower()
    {
        int total = NotriceGainShow.GetTotal(Nortice_GainSystem.gainedNortice);

        nortice += total;

        Nortice_GainSystem.gainedNortice.Clear();
    }

    public static void LoadAscensionScene()
    {
        V.ErasePlayerStats();

        V.EraseNonStaticVar();

        V.eventLoadingNewScene.Call();

        SceneManager.LoadScene("NorticePower");
    }
}
