using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Main_UI : MonoBehaviour
{
    public static GameObject ui_displayTitle_Current, ui_displayMonsterList_Current;

    public static DescriptionStatic.Type ui_displayDesc_descType = DescriptionStatic.Type.none;

    public static Display_title ui_displayTitle_Current_script;

    public static void Display_Title(string text, Vector3 Position, float distance, V.Color text_color = V.Color.white)
    {
        //Set the parent
        GameObject parent = Main_UI.ui_display_titleG;

        //if a title is already display
        if (ui_displayTitle_Current != null)
        {
            //Make the parent is the current title in the scene (we want to update the current tile)
            parent = ui_displayTitle_Current;
        }
        else //If there is no current title
        {
            //Set the parent to be a new title 
            parent = Instantiate(parent);
            ui_displayTitle_Current = parent;
        }

        Display_title script = parent.GetComponent<Display_title>();
        /*
        if (opaque)
            script.box_img.sprite = V.ui_box_opaque;
        else
            script.box_img.sprite = V.ui_box;*/

        ui_displayTitle_Current_script = script;

        //Set ui to null
        Text ui_displayTitle_text = script.txt;
        //Set box to null
        RectTransform ui_displayTitle_box = script.box/*, ui_displayTitle_box_contour = script.box_contour*/;

        //Set his text value
        ui_displayTitle_text.text = descColor.convert(text);

        //LayoutRebuilder.ForceRebuildLayoutImmediate(ui_displayTitle_text.rectTransform);

        /*ContentSizeFitter ctf = ui_displayTitle_text.GetComponent<ContentSizeFitter>();

        ctf.enabled = false;
        ctf.enabled = true;

        Canvas.ForceUpdateCanvases();*/

        //then get acces to the x scale (that been modifiy by the component content size filter)
        /*float scale_x_text = ui_displayTitle_text.rectTransform.sizeDelta.x;

        //If this scale is lower than the minX size the box must have
        if (scale_x_text <= V.main_ui.boxTitle_MinXSize)
        {
            //Then set the box x size to min size
            ui_displayTitle_box.sizeDelta = new Vector2(V.main_ui.boxTitle_MinXSize, V.main_ui.boxTitle_AdditionalY + ui_displayTitle_text.rectTransform.sizeDelta.y);
        }
        else
        {
            //Then set the box x size to the equal scale of the text + additionalX 
            ui_displayTitle_box.sizeDelta = new Vector2(scale_x_text + V.main_ui.boxTitle_AdditionalX, V.main_ui.boxTitle_AdditionalY + ui_displayTitle_text.rectTransform.sizeDelta.y);
        }*/

        /*ui_displayTitle_box_contour.sizeDelta = ui_displayTitle_box.sizeDelta;*/

        //Arrange the title to be in a confortably seeable position

        //The distanceToAdd to the Position parameter
        Vector3 distanceToAdd = new Vector3(0, distance, 0);

        //The finalPos will set to the parent
        Vector3 FinalPos = new Vector3(Position.x, Position.y, parent.transform.position.z) + distanceToAdd;

        //Check if it's in a conforatble position (if yes change the pos)
        FinalPos = FindConfortablePos(V.camera_maxYPos, FinalPos, ui_displayTitle_box, parent);

        //Update the change
        parent.transform.position = new Vector3(FinalPos.x, FinalPos.y, parent.transform.position.z);

        /*Image box_contour_img = script.box_contour_img;*/

        //Change the color of the box
        //box_img.color = V.GetColor(box_color);
        //change the color of the box_contour
        //box_contour_img.color = V.GetColor(box_contour_color);
        //change the color of the text
        script.txt.color = V.GetColor(text_color);

        Canvas.ForceUpdateCanvases();
    }

    public static void Display_MonsterList(List<Monster> monster_list, Vector3 Position, float distance, V.Color text_color = V.Color.white)
    {
        //Set the parent
        GameObject parent = Main_UI.ui_display_monsterListG;

        //if a title is already display
        if (ui_displayMonsterList_Current != null)
        {
            //Make the parent is the current title in the scene (we want to update the current tile)
            parent = ui_displayMonsterList_Current;
        }
        else //If there is no current title
        {
            //Set the parent to be a new title 
            parent = Instantiate(parent);
            ui_displayMonsterList_Current = parent;
        }

        Display_title dd = parent.GetComponent<Display_title>();

        //Set ui to null
        Text ui_displayTitle_text = dd.txt;
        //Set box to null
        RectTransform ui_displayTitle_box = dd.box/*, ui_displayTitle_box_contour = dd.box_contour*/;

        //ui_displayTitle_text.text = Main_UI.ConvertDescWithColor(txt);

        LayoutRebuilder.ForceRebuildLayoutImmediate(ui_displayTitle_text.rectTransform);

        //then get acces to the x scale (that been modifiy by the component content size filter)
        float scale_x_text = ui_displayTitle_text.rectTransform.sizeDelta.x;

        //If this scale is lower than the minX size the box must have
        if (scale_x_text <= V.main_ui.boxTitle_MinXSize)
        {
            //Then set the box x size to min size
            ui_displayTitle_box.sizeDelta = new Vector2(V.main_ui.boxTitle_MinXSize, V.main_ui.boxTitle_AdditionalY + ui_displayTitle_text.rectTransform.sizeDelta.y + 7);
        }
        else
        {
            //Then set the box x size to the equal scale of the text + additionalX 
            ui_displayTitle_box.sizeDelta = new Vector2(scale_x_text + V.main_ui.boxTitle_AdditionalX, V.main_ui.boxTitle_AdditionalY + ui_displayTitle_text.rectTransform.sizeDelta.y + 7);
        }

        //ui_displayTitle_box_contour.sizeDelta = ui_displayTitle_box.sizeDelta;

        //Arrange the title to be in a confortably seeable position

        //The distanceToAdd to the Position parameter
        Vector3 distanceToAdd = new Vector3(0, distance, 0);

        //The finalPos will set to the parent
        Vector3 FinalPos = new Vector3(Position.x, Position.y, parent.transform.position.z) + distanceToAdd;

        //Check if it's in a conforatble position (if yes change the pos)
        FinalPos = FindConfortablePos(V.camera_maxYPos, FinalPos, ui_displayTitle_box, parent);

        //Update the change
        parent.transform.position = new Vector3(FinalPos.x, FinalPos.y, parent.transform.position.z);

        //Change the color of the box
        //ui_displayTitle_box.GetComponent<Image>().color = V.GetColor(box_color);
        //change the color of the box_contour
        //ui_displayTitle_box_contour.GetComponent<Image>().color = V.GetColor(box_contour_color);
        //change the color of the text
        ui_displayTitle_text.color = V.GetColor(text_color);

        Canvas.ForceUpdateCanvases();
    }

    public static void Display_EraseAllType()
    {
        Display_Title_Erase();
        Display_MonsterList_Erase();
    }

    /// <summary>
    /// Erase the current title
    /// </summary>
    public static void Display_Title_Erase()
    {
        //Erase current title
        Destroy(ui_displayTitle_Current);
    }

    /// <summary>
    /// Erase the current title if his title is "title"
    /// </summary>
    public static void Display_Title_Erase(string title)
    {
        if (ui_displayTitle_Current != null && ui_displayTitle_Current.transform.GetChild(2).GetComponent<Text>().text == title)
        {
            Display_Title_Erase();
        }
    }

    public static void Display_MonsterList_Erase()
    {
        Destroy(ui_displayMonsterList_Current);
    }
}
