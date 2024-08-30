using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TalentHolderIndividual : WindowSkillElement
{
    public int LockedUntilLevel;

    public Panel_skill_talent talentHolder;

    public Character.talentSlot slot;

    bool Locked { get => LockedUntilLevel > V.player_info.level; }

    public Image render;

    public Sprite LockSp;

    public Talent_Gestion.talent thisTalent { get => Character.GetTalentAtSlot(slot); }

    public override void Func_update()
    {
        base.Func_update();

        if (Locked)
        {
            render.sprite = LockSp;
            render.gameObject.SetActive(true);
            changeColor(normal);
            return;
        }

        render.gameObject.SetActive(thisTalent != Talent_Gestion.talent.empty);

        if (thisTalent != Talent_Gestion.talent.empty)
        {
            render.sprite = Talent_Gestion.GetSprite(thisTalent);
        }

        if (mouseOver)
        {
            changeColor(highlight);
        }
        else if (talentHolder.currentSlot == slot)
        {
            changeColor(selected);
        }
        else
        {
            changeColor(normal);
        }
    }

    public override void Window_Interaction_MouseOver_Update()
    {
        base.Window_Interaction_MouseOver_Update();
        mouseOver = true;

        DisplayInfo();
    }

    public bool mouseOver;

    public override void Window_Interaction_MouseOver_Start()
    {
        base.Window_Interaction_MouseOver_Start();

        if (!talentHolder.talentHolderOnMouse.Contains(this.gameObject))
            talentHolder.talentHolderOnMouse.Add(this.gameObject);

        mouseOver = true;

        DisplayInfo();

        TreeElement.Animation_set(this.gameObject, setScaleStrengh, baseScale);

        transform.SetAsLastSibling();
    }

    public override void Window_Interaction_MouseOver_End()
    {
        base.Window_Interaction_MouseOver_End();

        if (talentHolder.talentHolderOnMouse.Contains(this.gameObject))
            talentHolder.talentHolderOnMouse.Remove(this.gameObject);

        mouseOver = false;

        TreeElement.Animation_scale(gameObject, baseScale.x, scaleDownSpeed);

        Description_text.EraseDispay();
    }

    public override void Window_Interaction_Click()
    {
        base.Window_Interaction_Click();

        if (!talentHolder.talentListHolderIsActive)
            talentHolder.talentListHolder_Active();

        if (Locked)
            return;

        talentHolder.currentSlot = slot;
    }

    /// <summary>
    /// Display the info of this skill
    /// </summary>
    public override void DisplayInfo()
    {
        string title = "", description = "";

        if (Locked)
        {
            if (V.IsFr())
            {
                title = "Vérouillé";
                description = "Vérouillé jusqu'au niveau " + LockedUntilLevel + " (niveau actuelle : " + V.player_info.level + ")";
            }
            else
            {
                title = "Locked";
                description = "Locked until level " + LockedUntilLevel + " (actual level : " + V.player_info.level + ")";
            }
        }
        else
        {
            if (thisTalent == Talent_Gestion.talent.empty)
            {
                if (V.IsFr())
                {
                    title = "Emplacement de talent";
                    description = "vide";
                }
                else
                {
                    title = "Talent holder";
                    description = "Empty";
                }
            }
            else
            {
                title = Talent_Gestion.GetTitle(thisTalent);
                description = Talent_Gestion.GetDescription(thisTalent);
            }
        }

        Description_text.Display(title, description, talentHolder.DisplayUiDescriptionPosition, 0);
    }

    public float setScaleStrengh, scaleDownSpeed;


    public Image contour;
    public Color32 normal, highlight, selected;

    public void changeColor(Color32 col)
    {
        contour.color = col;
    }
}
