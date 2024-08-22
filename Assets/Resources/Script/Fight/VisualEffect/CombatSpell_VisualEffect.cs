using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSpell_VisualEffect : MonoBehaviour
{
    public delegate float Modifiy();

    public static Dictionary<string, (List<string> ls, Modifiy m)> toolbar_visualEffect = new Dictionary<string, (List<string> ls, Modifiy m)>();

    public static void Add(string id, List<string> ls, Modifiy m)
    {
        toolbar_visualEffect.Add(id, (ls, m));

        UpdateToolbarVisualEffect_Call();
    }

    public static void Remove(string id)
    {
        toolbar_visualEffect.Remove(id);

        UpdateToolbarVisualEffect_Call();
    }

    public Transform visualEffectParent;

    public static void UpdateToolbarVisualEffect_Call()
    {
        V.CombatSpell_VisualEffect.UpdateToolbarVisualEffect();
    }

    public void UpdateToolbarVisualEffect()
    {
        while (visualEffectParent.childCount > 0)
        {
            DestroyImmediate(visualEffectParent.GetChild(0).gameObject);
        }

        foreach ((List<string> ls, Modifiy m) v in toolbar_visualEffect.Values)
        {
            AddVisualEffect(v.ls);
        }
    }

    public GameObject VisualEffectPrefab;

    public void AddVisualEffect(List<string> ls)
    {
        GameObject g = Instantiate(VisualEffectPrefab, visualEffectParent);

        g.GetComponent<CombatSpell_VisualEffectPrefab>().Init(ls);
    }

    public static float GetVisualEffect(string pos)
    {
        float nb = 0;

        foreach ((List<string> ls, Modifiy m) v in toolbar_visualEffect.Values)
        {
            if (v.ls.Contains(pos))
            {
                nb += v.m();
            }
        }

        return nb;
    }
}