using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display_description_equipment : Display_description
{
    public SingleEquipment equip;

    public equipmentHolder equipmentHolderLine;

    public static void Display_Description(string title, Vector3 Position, float distance, SingleEquipment e)
    {
        Main_UI.descType descType = Main_UI.descType.equipment;

        //Set the parent
        GameObject parent = Main_UI.ui_display_description_equipmentG;

        //if a title is already display
        if (Main_UI.ui_displayDescription_Current != null && (descType == Main_UI.ui_displayDesc_descType || Main_UI.ui_displayDesc_descType == Main_UI.descType.none))
        {
            //Make the parent is the current title in the scene (we want to update the current tile)
            parent = Main_UI.ui_displayDescription_Current;
        }
        else //If there is no current title
        {
            if (Main_UI.ui_displayDescription_Current != null)
                Destroy(Main_UI.ui_displayDescription_Current.gameObject);

            //Set the parent to be a new title 
            parent = Instantiate(parent);
            Main_UI.ui_displayDescription_Current = parent;
            Main_UI.ui_displayDesc_descType = descType;
        }

        Display_description_equipment DisplayDesc = parent.GetComponent<Display_description_equipment>();

        Image Box = DisplayDesc.box;

        Main_UI.ui_displayDescription_Current_script = DisplayDesc;

        Text text_title = DisplayDesc.title;

        //Set his text value
        text_title.text = descColor.convert(title);

        DisplayDesc.equipmentHolderLine.init(e);

        Canvas.ForceUpdateCanvases();

        float TheoricalNewYPos = Position.y + distance;

        Vector3 FinalPos = new Vector3(Position.x, TheoricalNewYPos, parent.transform.position.z);

        FinalPos = Main_UI.FindConfortablePos(V.camera_maxYPos, FinalPos, Box.rectTransform, parent, DisplayDesc.saveYHeightBox);

        if (FinalPos.y < TheoricalNewYPos)
        {
            (Vector2 top_right, Vector2 bottom_left) v = Main_UI.GetBoundaries_V2(DisplayDesc.box.rectTransform, DisplayDesc.saveYHeightBox);

            TheoricalNewYPos = Position.y - distance - (v.top_right.y - v.bottom_left.y);

            FinalPos = new Vector3(Position.x, TheoricalNewYPos, parent.transform.position.z);
        }

        parent.transform.position = new Vector3(FinalPos.x, FinalPos.y, parent.transform.position.z);

        DisplayDesc.Check_PositionAndScale();
    }

    public static void Display_Description(string title, Vector3 Position, float distance, string spellEffect, List<(SingleEquipment.value_type t, int value)> ls)
    {
        Main_UI.descType descType = Main_UI.descType.equipment;

        //Set the parent
        GameObject parent = Main_UI.ui_display_description_equipmentG;

        //if a title is already display
        if (Main_UI.ui_displayDescription_Current != null && (descType == Main_UI.ui_displayDesc_descType || Main_UI.ui_displayDesc_descType == Main_UI.descType.none))
        {
            //Make the parent is the current title in the scene (we want to update the current tile)
            parent = Main_UI.ui_displayDescription_Current;
        }
        else //If there is no current title
        {
            if (Main_UI.ui_displayDescription_Current != null)
                Destroy(Main_UI.ui_displayDescription_Current.gameObject);

            //Set the parent to be a new title 
            parent = Instantiate(parent);
            Main_UI.ui_displayDescription_Current = parent;
            Main_UI.ui_displayDesc_descType = descType;
        }

        Display_description_equipment DisplayDesc = parent.GetComponent<Display_description_equipment>();

        Image Box = DisplayDesc.box;

        Main_UI.ui_displayDescription_Current_script = DisplayDesc;

        Text text_title = DisplayDesc.title;

        //Set his text value
        text_title.text = descColor.convert(title);

        DisplayDesc.equipmentHolderLine.init(spellEffect, ls);

        Canvas.ForceUpdateCanvases();

        float TheoricalNewYPos = Position.y + distance;

        Vector3 FinalPos = new Vector3(Position.x, TheoricalNewYPos, parent.transform.position.z);

        FinalPos = Main_UI.FindConfortablePos(V.camera_maxYPos, FinalPos, Box.rectTransform, parent, DisplayDesc.saveYHeightBox);

        if (FinalPos.y < TheoricalNewYPos)
        {
            (Vector2 top_right, Vector2 bottom_left) v = Main_UI.GetBoundaries_V2(DisplayDesc.box.rectTransform, DisplayDesc.saveYHeightBox);

            TheoricalNewYPos = Position.y - distance - (v.top_right.y - v.bottom_left.y);

            FinalPos = new Vector3(Position.x, TheoricalNewYPos, parent.transform.position.z);
        }

        parent.transform.position = new Vector3(FinalPos.x, FinalPos.y, parent.transform.position.z);

        DisplayDesc.Check_PositionAndScale();
    }

}
