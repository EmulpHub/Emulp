using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Main_UI : MonoBehaviour
{
    public static void Display_Description(Talent_Gestion.talent t, Vector3 Position, float distance)
    {
        Display_Description(Talent_Gestion.GetTitle(t), Talent_Gestion.GetDescription(t), Position, distance);
    }

    public static void Display_Description(string title, string description, int pa, int cd, int rangeMin, int rangeMax,
    Vector3 Position, float distance,
    Spell.Range_type range_Type, SpellGestion.range_effect_size zone, bool WithTab)
    {
        Display_Description(title, description, (pa, false), (cd, false), (rangeMin, false), (rangeMax, false), Position, distance, range_Type, zone, WithTab);
    }

    /// <summary>
    /// Display description for spell
    /// </summary>
    /// <param name="title">The title of the description</param>
    /// <param name="description">The description of the display</param>
    /// <param name="pa">The pa of the display</param>
    /// <param name="cd">The current cd of the display (if negative it means that's it max use per turn)</param>
    /// <param name="rangeMin">The min range of the concerned spell</param>
    /// <param name="rangeMax">The max range of the concerned spell</param>
    /// <param name="Position">The position where this should be displayed</param>
    /// <param name="distance">The +y distance this display will be show</param>
    /// <param name="description_BoxType">The specific contour this display should have</param>
    /// <param name="range_Type">The range_Type (if it's in line or no)</param>
    /// <param name="WithTab">Is the description should start with a tabulation</param>
    public static void Display_Description(string title, string description, (int pa, bool m) pa, (int cd, bool m) cd, (int rangeMin, bool m) rgMin, (int rangeMax, bool m) rgMax,
    Vector3 Position, float distance,
    Spell.Range_type range_Type, SpellGestion.range_effect_size zone, bool WithTab)
    {
        Display_description_text.Display_Description(title, description, pa, cd, rgMin, rgMax, Position, distance, range_Type, zone, WithTab);
    }

    public static void Display_Description(SpellGestion.List sp, Vector3 Position, float distance, bool baseValue = true, Spell e = null)
    {
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
            baseValue = true;

        string additonalTitleTxt = baseValue ? " *inf(base)*end" : "";

        Display_Description(SpellGestion.Get_Title(sp) + additonalTitleTxt, SpellGestion.Get_Description(sp, baseValue, e), SpellGestion.Get_paCost_full(sp), SpellGestion.Get_Cd_full(sp), SpellGestion.Get_RangeMin_full(sp),
            SpellGestion.Get_RangeMax_full(sp), Position, distance, SpellGestion.Get_rangeType(sp), SpellGestion.Get_RangeEffect(sp), false);
    }

    /// <summary>
    /// Display description for spell Without rangeType
    /// </summary> 
    /// <param name="title">The title of the description</param>
    /// <param name="description">The description of the display</param>
    /// <param name="pa">The pa of the display</param>
    /// <param name="cd">The current cd of the display (if negative it means that's it max use per turn)</param>
    /// <param name="rangeMin">The min range of the concerned spell</param>
    /// <param name="rangeMax">The max range of the concerned spell</param>
    /// <param name="Position">The position where this should be displayed</param>
    /// <param name="distance">The +y distance this display will be show</param>
    public static void Display_Description(string title, string description, int pa, int cd, int rangeMin, int rangeMax, Vector3 Position, float distance, Spell.Range_type range_Type)
    {
        Display_Description(title, description, pa, cd, rangeMin, rangeMax, Position, distance, range_Type, SpellGestion.range_effect_size.singleTarget, true);
    }

    /// <summary>
    /// Display description for spell Without rangeType
    /// </summary> 
    /// <param name="title">The title of the description</param>
    /// <param name="description">The description of the display</param>
    /// <param name="pa">The pa of the display</param>
    /// <param name="cd">The current cd of the display (if negative it means that's it max use per turn)</param>
    /// <param name="rangeMin">The min range of the concerned spell</param>
    /// <param name="rangeMax">The max range of the concerned spell</param>
    /// <param name="Position">The position where this should be displayed</param>
    /// <param name="distance">The +y distance this display will be show</param>
    public static void Display_Description(string title, string description, int pa, int cd, int rangeMin, int rangeMax, Vector3 Position, float distance)
    {
        Display_Description(title, description, pa, cd, rangeMin, rangeMax, Position, distance, Spell.Range_type.normal, SpellGestion.range_effect_size.singleTarget, true);
    }

    /// <summary>
    /// Display description for spell without rangeType and pa
    /// </summary>
    /// <param name="title">The title of the description</param>
    /// <param name="description">The description of the display</param>
    /// <param name="Position">The position where this should be displayed</param>
    /// <param name="distance">The +y distance this display will be show</param>
    public static void Display_Description(string title, string description, Vector3 Position, float distance)
    {
        Display_Description(title, description, -1, 0, 0, 0, Position, distance, Spell.Range_type.normal, SpellGestion.range_effect_size.singleTarget, true);
    }

    /// <summary>
    /// Display description for spell (without pa and option "withTab")
    /// </summary>
    /// <param name="title">The title of the description</param>
    /// <param name="description">The description of the display</param>
    /// <param name="Position">The position where this should be displayed</param>
    /// <param name="distance">The +y distance this display will be show</param>
    public static void Display_Description(string title, string description, Vector3 Position, float distance, bool WithTab)
    {
        Display_Description(title, description, -1, 0, 0, 0, Position, distance, Spell.Range_type.normal, SpellGestion.range_effect_size.singleTarget, WithTab);
    }

    /// <summary>
    /// Erase the current description
    /// </summary>
    public static void Display_Description_Erase()
    {
        //Erase current description
        DestroyImmediate(ui_displayDescription_Current);
    }

    /// <summary>
    /// Erase the current description with his title = title
    /// </summary>
    public static void Display_Description_Erase(string title)
    {
        if (ui_displayDescription_Current == null)
            return;

        Display_description dd = ui_displayDescription_Current.GetComponent<Display_description>();

        bool result = dd.title.text == title;

        if (!result && dd is Display_description_text)
        {
            result = ((Display_description_text)dd).description.text == title;
        }

        if (result)
        {
            Display_Description_Erase();
        }
    }
}
