using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public partial class Panel_skill_talent : TreeElement
{
    [HideInInspector]
    public List<GameObject> talentHolderOnMouse = new List<GameObject>();

    public Character.talentSlot currentSlot;

    public override void func_start()
    {
        base.func_start();

        talentHolderCreateIndividual();
    }

    public void Update_UI()
    {
        talentListHolder_Parent.gameObject.SetActive(talentListHolderIsActive);
    }

    public GameObject listButtonClose;

    public new void Update()
    {
        base.Update();

        DisplayUiDescriptionPosition_Update();

        talentListHolder_Management();

        Update_UI();

        newTalent_Management();
    }


    public void MakeAnimation_Shake(Vector3 power)
    {
        transform.DOKill();
        transform.DOPunchScale(power, punchScaleSpeed_Up, 1);
    }

    public GameObject talentListHolder, talentListHolder_Parent;

    public RectTransform talentListHolder_Rect;

    public override void Window_Interaction_Click()
    {
        if (talentHolderOnMouse.Count > 0)
            return;

        talentListHolder_ClickManagement();

        base.Window_Interaction_Click();
    }

    [HideInInspector]
    public bool talentListHolderIsActive = false;

    public void talentListHolder_ClickManagement()
    {
        if (!talentListHolderIsActive)
        {
            talentListHolder_Active();
        }
    }

    public void DeactivateTalentList()
    {
        if (talentListHolderIsActive)
        {
            talentListHolder_Disactive();
        }
    }

    public float talentListHolder_SizeDeltalimitY;

    public static bool deactivateTalentShow;

    public void talentListHolder_Management()
    {
        if(deactivateTalentShow && talentListHolderIsActive)
        {
            talentListHolder_Disactive();
        }

        if (!talentListHolderIsActive)
        {
            if (talentListHolder_Rect.sizeDelta.y <= talentListHolder_SizeDeltalimitY)
            {
                talentListHolder_Rect.DOKill();

                talentListHolder_Rect.sizeDelta = new Vector2(talentListHolder_Rect.sizeDelta.x, 0);
            }
        }

        listButtonClose.gameObject.SetActive(talentListHolderIsActive);
    }

    public float talentListHolder_SizeDeltaY, talentListHolder_speedAnimation, talentListHolder_speedAnimation_remove;

    public void talentListHolder_Active()
    {
        if (deactivateTalentShow)
            return;

        talentListHolderIsActive = true;
        talentListHolder.gameObject.SetActive(true);

        talentListHolder_Rect.DOKill();
        talentListHolder_Rect.sizeDelta = new Vector2(talentListHolder_Rect.sizeDelta.x, 0);

        talentListHolder_Rect.DOSizeDelta(new Vector2(talentListHolder_Rect.sizeDelta.x, talentListHolder_SizeDeltaY), talentListHolder_speedAnimation);

        CenterCamera(new Vector3(0, 0, 0));
    }

    public void talentListHolder_Disactive()
    {
        talentListHolderIsActive = false;

        talentListHolder_Rect.DOKill();

        talentListHolder_Rect.DOSizeDelta(new Vector2(talentListHolder_Rect.sizeDelta.x, 0), talentListHolder_speedAnimation);
    }

    public static bool newTalent { get => Talent_Gestion.newTalentUnlocked.Count > 0; }

    public GameObject newTalentrender, newTalentExclamation;

    public float newTalentRender_size, newTalentRender_speed;

    public void newTalent_Management()
    {
        newTalentrender.transform.DOScale(newTalent ? newTalentRender_size : 0, newTalentRender_speed);

        newTalentExclamation.transform.gameObject.SetActive(newTalent);
    }

    public override void Cursor()
    {
        base.Cursor();

        bool result = MouseOver;

        Main_UI.ManageDontMoveCursor(gameObject, result);

        if (result) Window.SetCursorAndOffsetHand();
    }
}
