using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Description_text : MonoBehaviour
{
    #region Display

    public static void Display(string title, string description, Vector3 Position, float distance)
    {
        Description desc = GetDisplay();

        desc.baseTitle = title;
        desc.baseDescription = description;

        desc.title.text = TreatTitle(title);
        desc.description.text = TreatDescription(description);

        Vector3 treatedPosition = TreatPosition(Position, desc, distance);

        desc.transform.position = treatedPosition;
    }

    public static void Display(Talent_Gestion.talent t, Vector3 Position, float distance)
    {
        Display(Talent_Gestion.GetTitle(t), Talent_Gestion.GetDescription(t), Position, distance);
    }

    public static void Display(SpellGestion.List spell, Vector3 Position, float distance, Spell spellObject = null)
    {
        string description = "";
        if (spellObject is null)
            description = SpellGestion.Get_Description(spell, true);
        else
            description = SpellGestion.Get_Description(spell, false, spellObject);

        Display(SpellGestion.Get_Title(spell), description, Position, distance);
    }

    #endregion

    #region Erase 

    public static void EraseDispay(string title = "")
    {
        if (DescriptionStatic.CurrentDescription != null)
        {
            if (title != "" && DescriptionStatic.CurrentDescription_script.baseTitle != title)
                return;

            Destroy(DescriptionStatic.CurrentDescription);
        }

        DescriptionStatic.CurrentDescription = null;
    }

    #endregion

    private static Vector3 TreatPosition(Vector3 position, Description desc, float distance)
    {
        Text description = desc.description;

        float PosY = position.y + distance;

        Vector3 treatedPosition = new Vector3(position.x, PosY, 0);

        treatedPosition = Main_UI.FindConfortablePos(V.camera_maxYPos, treatedPosition, desc.box.rectTransform, desc.gameObject);

        if (treatedPosition.y < PosY)
        {
            var boundaries = Main_UI.GetBoundaries_V2(desc.box.rectTransform);

            PosY = position.y - distance - (boundaries.top_right.y - boundaries.bottom_left.y);

            treatedPosition = new Vector3(position.x, PosY, 0);
        }

        return new Vector3(treatedPosition.x, treatedPosition.y, 0);
    }

    private static Description GetDisplay()
    {
        GameObject display = DescriptionStatic.CurrentDescription;

        if (display == null)
            display = Instantiate(DescriptionStatic.DescriptionPrefab).gameObject;

        DescriptionStatic.CurrentDescription = display;

        return display.GetComponent<Description>();
    }

    private static string TreatTitle(string title)
    {
        return descColor.convert(title);
    }

    private static string TreatDescription(string description)
    {
        return descColor.convert(description);
    }
}

