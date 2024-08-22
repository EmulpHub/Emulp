using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display_description_text : Display_description
{
    public Text infoTxt;

    public Spell_SetInfoGroup InfoGroup;

    public RectTransform right_group, left_group;

    public float descriptionText_yPos, descriptionText_yPos_withShiftInfo, descriptionText_yPos_withNoTitle, box_WidhtSize_noTitle;

    public ContentSizeFitter ctf_description;

    [HideInInspector]
    public bool InfoText_IsSet = false;

    [HideInInspector]
    public string txt_desc, txt_info;

    [HideInInspector]
    public bool ContainMoreInfo;

    [HideInInspector]
    public string baseText;

    public void InfoText()
    {
        if (InfoText_IsSet) return;

        InfoText_IsSet = true;

        (string normal, string info) v = SeparateDesc(baseText);

        if (v.info != "")
        {
            txt_info = v.info;
            txt_desc = v.normal;
            ContainMoreInfo = true;
        }
        else
        {
            ContainMoreInfo = false;
            txt_desc = baseText;
        }

        description.text = descColor.convert(txt_desc + txt_info);

        SetPositionAndScale(true);

        description.text = descColor.convert(txt_desc);

        SetPositionAndScale(true);
    }

    public override void SetPositionAndScale(bool SaveValue = false)
    {
        //UI stuff
        if (ContainMoreInfo && WithShiftInfo)
        {
            infoTxt.text = V.IsFr() ? descColor.convert("<i>*inf*inl--Shift--*end pour plus de détails*end</i>") : descColor.convert("<i>*inf*inl--Shift--*end for more details*end</i>");
        }
        else
        {
            infoTxt.text = "";
        }

        int txt_len = GetMaxNbLine(description.text);

        float Max = description_maxWidth;

        description.rectTransform.sizeDelta = new Vector2(Max, description.rectTransform.sizeDelta.y);

        ctf_description.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;

        ctf_description.enabled = false;
        ctf_description.enabled = true;


        float infoTxt_YHeigh = (ContainMoreInfo && WithShiftInfo) ? infoTxt.rectTransform.sizeDelta.y : 0;

        float box_yscale = 0;

        float additionalTitle_YPos = 3;

        if (title.text != "")
        {
            box_yscale += title.rectTransform.rect.height;
        }

        box_yscale += box_minHeightSize_noPa + description.rectTransform.rect.height - 16;

        bool titleIsOn = title.text.Length > 0;

        float descAdditional = descriptionText_yPos;

        if (WithShiftInfo)
        {
            descAdditional = descriptionText_yPos_withShiftInfo;
        }
        else if (!titleIsOn)
        {
            descAdditional = descriptionText_yPos_withNoTitle;
        }

        description.rectTransform.anchoredPosition = new Vector2(0, descAdditional + infoTxt_YHeigh);

        float width = titleIsOn ? box_WidhtSize : box_WidhtSize_noTitle;

        box.rectTransform.sizeDelta = new Vector2(width, box_yscale + additionalTitle_YPos + infoTxt_YHeigh);

        float disX = 5, disY = 5;


        left_group.anchoredPosition = new Vector2(-box.rectTransform.rect.width / 2 - disX, box.rectTransform.rect.height - disY);
        right_group.anchoredPosition = new Vector2(box.rectTransform.rect.width / 2 + disX, box.rectTransform.rect.height - disY);


        base.SetPositionAndScale(SaveValue);
    }

    bool WithShiftInfo = false;

    public override void Check_PositionAndScale()
    {
        InfoText();

        string desc = "";

        WithShiftInfo = ContainMoreInfo;

        bool shiftpressed = false;

        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            shiftpressed = true;
            WithShiftInfo = false;
            desc = txt_desc + txt_info;
        }
        else
        {
            desc = txt_desc;
        }

        if (description.text != desc)
            description.text = descColor.convert(desc);

        InfoGroup.gameObject.SetActive(shiftpressed);

        base.Check_PositionAndScale();
    }

    public static void Display_Description (string title,string description, Vector3 Position, float distance)
    {
        Display_Description(title, description, (0, false), (0, false), (0, false), (0, false), Position, distance,Spell.Range_type.normal, SpellGestion.range_effect_size.singleTarget, false);
    }

    public static void Display_Description(string title, string description, (int pa, bool m) pa, (int cd, bool m) cd, (int rangeMin, bool m) rgMin, (int rangeMax, bool m) rgMax,
Vector3 Position, float distance,
Spell.Range_type range_Type, SpellGestion.range_effect_size zone, bool WithTab)
    {
        Main_UI.descType descType = Main_UI.descType.text;

        //Set the parent
        GameObject parent = Main_UI.ui_display_descriptionG;

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

        Display_description DisplayDesc = parent.GetComponent<Display_description>();

        Image Box = DisplayDesc.box;

        Main_UI.ui_displayDescription_Current_script = DisplayDesc;

        Text text_title = DisplayDesc.title;
        Text text_description = DisplayDesc.description;
        //Text text_cd = DisplayDesc.cd;
        // Text text_range = DisplayDesc.range;

        if (description == "")
        {
            description = title;
            title = "";
        }

        //Set his text value
        text_title.text = descColor.convert(title);

        string desc_BaseText = "";

        if (WithTab)
        {
            desc_BaseText = "   " + descColor.convert(description);
        }
        else
        {
            desc_BaseText = descColor.convert(description);
        }

        text_description.text = desc_BaseText;
        //DisplayDesc.baseText = description;

        //DisplayDesc.InfoText_IsSet = false;

        //DisplayDesc.InfoGroup.Init(pa, range_Type, title, rgMin, rgMax, zone, cd);

        float TheoricalNewYPos = Position.y + distance;

        //Update the change
        Vector3 FinalPos = new Vector3(Position.x, TheoricalNewYPos, parent.transform.position.z);

        //DisplayDesc.InfoText();

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
