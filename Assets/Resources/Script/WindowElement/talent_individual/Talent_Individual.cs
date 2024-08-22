using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Talent_Individual : WindowSkillElement
{
    [HideInInspector]
    public string title, description;

    [HideInInspector]
    public Talent_Gestion.talent thisTalent;

    [HideInInspector]
    public Sprite graphique;

    public Image img, img_lock;

    public bool Locked = true;

    public Panel_skill_talent parentHolder;

    public Panel_Interactable_OnCursorShowHand Panel_Interactable_OnCursorShowHand;

    public void SetInfo(Talent_Gestion.talent t)
    {
        thisTalent = t;

        title = Talent_Gestion.GetTitle(t);
        description = Talent_Gestion.GetDescription(t);

        graphique = Talent_Gestion.GetSprite(t);

        SetLocked();

        SetSprite();
    }

    public void SetLocked()
    {
        if (V.administrator && V.script_Scene_Main_Administrator.unlockAllTalents)
        {
            Locked = false;
            return;
        }

        Locked = (thisTalent != Talent_Gestion.talent.empty && !Talent_Gestion.unlockedTalent.Contains(thisTalent));

        if (Locked)
        {
            title = V.IsFr() ? "Vérouillé" : "Locked";
            description = V.IsFr() ? "Talent vérouillé" : "Locked talent";
        }
        else
        {
            title = Talent_Gestion.GetTitle(thisTalent);
            description = Talent_Gestion.GetDescription(thisTalent);
        }
    }

    public void SetSprite()
    {
        img.gameObject.SetActive(!Locked);
        img_lock.gameObject.SetActive(Locked);

        img.sprite = graphique;
    }

    [HideInInspector]
    public bool NewTalent;

    public GameObject newTalentExclamation;

    public void ExclamationManagement()
    {
        NewTalent = Talent_Gestion.newTalentUnlocked.Contains(thisTalent);

        newTalentExclamation.gameObject.SetActive(NewTalent);
    }

    private new void Update()
    {
        base.Update();

        SetLocked();

        SetSprite();

        contour_Management();

        ExclamationManagement();
    }

    [HideInInspector]
    public int index;

    public Vector2 startVector;

    public float distanceDiff;

    public int maxTalentPerLine;

    public void SetPosition(int i)
    {
        index = i;

        Vector2 pos = startVector + new Vector2(distanceDiff * (i % maxTalentPerLine), -distanceDiff * (i / maxTalentPerLine));

        rectThis.anchoredPosition = pos;
    }

    public void SelectionnateThisTalent()
    {
        parentHolder.MakeAnimation_Shake(new Vector3(0.1f, 0.1f, 0));

        TreeElementBuyable.AnimationSparkle(5, TreeElementBuyable.SparkleAnimationColor.yellow, parentHolder);
    }

    public override void Window_Interaction_Click()
    {
        base.Window_Interaction_Click();

        if (!Locked)
        {
            Character.SelectEquipedTalent(thisTalent, parentHolder.currentSlot);

            SelectionnateThisTalent();
        }
    }

    public Talent_Individual currentTalent;

    public override void Window_Interaction_MouseOver_Start()
    {
        base.Window_Interaction_MouseOver_Start();

        DisplayInfo();

        currentTalent = this;

        if (NewTalent)
        {
            Talent_Gestion.newTalentUnlocked.Remove(thisTalent);
        }
    }

    public override void Window_Interaction_MouseOver_Update()
    {
        base.Window_Interaction_MouseOver_Update();

        DisplayInfo();

        currentTalent = this;
    }

    public override void Window_Interaction_MouseOver_End()
    {
        base.Window_Interaction_MouseOver_End();

        if (currentTalent == this && DisplayInfo_isShowed())
        {
            Main_UI.Display_Description_Erase();
            currentTalent = null;
        }

    }

    /// <summary>
    /// Display the info of this skill
    /// </summary>
    public override void DisplayInfo()
    {
        Main_UI.Display_Description(title, description, parentHolder.DisplayUiDescriptionPosition, 0);
    }

    public override bool DisplayInfo_isShowed()
    {
        return Main_UI.ui_displayDescription_Current_script != null && Main_UI.ui_displayDescription_Current_script.title.text == title;
    }
}
