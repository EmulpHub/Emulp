using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Main_Object : MonoBehaviour
{
    public enum objects
    {
        pa, pm, life, ToolbarSpell, button_character, button_skill, button_equipment, button_endOfTurn, spell_punch, Map_possibleToMove
    }

    public static Dictionary<objects, GameObject> list = new Dictionary<objects, GameObject>();

    public static void Initialize_object()
    {
        list.Clear();

        list.Add(objects.pa, V.main_ui.pa);
        list.Add(objects.pm, V.main_ui.pm);
        list.Add(objects.life, V.main_ui.life);
        list.Add(objects.ToolbarSpell, V.main_ui.ToolbarSpell);
        list.Add(objects.button_endOfTurn, V.script_Scene_Main.button_EndOfturn);
        list.Add(objects.button_character, V.main_ui.button_character);
        list.Add(objects.button_skill, V.main_ui.button_skill);
        list.Add(objects.button_equipment, V.main_ui.button_equipment);
        list.Add(objects.spell_punch, null);
        list.Add(objects.Map_possibleToMove, V.main_ui.Map_possibleToMove);
    }

    public static GameObject Get(objects o)
    {
        return list[o];
    }

    public static void Modify(objects o, GameObject obj)
    {

        list[o] = obj;
    }

    public static void SetActive(objects o, bool active)
    {
        if (o == objects.Map_possibleToMove)
        {
            list[o].GetComponent<BoxCollider2D>().enabled = active;
            return;
        }

        list[o].gameObject.SetActive(active);
    }

    public static void Enable(objects o)
    {
        SetActive(o, true);
    }

    public static void Disable(objects o)
    {
        SetActive(o, false);
    }

    public static void Modify_list(List<objects> l, bool enable)
    {
        foreach (objects o in l)
            SetActive(o, enable);
    }

    public static void Modify_all(bool enable)
    {
        foreach (objects o in Enum.GetValues(typeof(objects)))
        {
            SetActive(o, enable);
        }
    }

    public static void Enable_list(List<objects> l)
    {
        foreach (objects o in l)
            Enable(o);
    }

    public static void Disable_list(List<objects> l)
    {
        foreach (objects o in l)
            Disable(o);
    }
}
