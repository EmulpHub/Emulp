using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Tutorial : MonoBehaviour
{
    public static Tutorial tuto;

    public void Start()
    {
        ClickAutorization.Add(this.gameObject);

        tuto = this;

        windowTxt = Window_Txt.CreateFloatingWindow_Empty();

        windowTxt.AlwaysFirstPlanSortingOrder = true;
        windowTxt.DontAllowClickOnCloseButton = true;
        //windowTxt.event_Close += CloseButton;

        StartCoroutine(StepManagement());
    }

    [HideInInspector]
    public Window_Txt windowTxt;

    public step currentStep;

    public List<step> stepOrder = new List<step>
    {
        new step_intro(),
        new step_movementOutOfCombat(),
        new step_startAfight(),
        new step_positionning(),
        new step_yourTurn(),
        new step_receiveSpell(),
        new step_useYourSpell(),
        new step_monsterTurn(),
        new step_newTurn(),
        new step_youWin(),
        new step_skillTree(),
        new step_talent(),
        new step_EquipTalent(),
        new step_congrat(),
        new step_equipment()
    };

    public IEnumerator StepManagement()
    {
        int i = 0;

        while (i < stepOrder.Count)
        {
            step s = stepOrder[i];

            currentStep = s;

            s.takeEffect();

            i++;

            yield return new WaitUntil(s.finished);

            s.finished_effect();
        }

        print("finish");
        FinishTutorial();
    }

    public Monster m;

    [HideInInspector]
    public bool ForceCloseButton;

    public bool WonderingIfClosing;

    public void CloseButton()
    {/*
        if (ForceCloseButton)
        {
            ForceCloseButton = false;
            windowTxt.Close_Force();
            return;
        }

        windowTxt.event_Close -= CloseButton;

        WonderingIfClosing = true;

        Window_Confirmation.Launch(V.IsFr() ? "Voulez-vous vraiment passer le tutoriel ?" : "Do you realy want to skip the tutorial", () =>
        {
            WonderingIfClosing = false;
            FinishTutorial();
        }
        , () => { windowTxt.event_Close += CloseButton; });
        */
    }

    public void FinishTutorial()
    {
        StopAllCoroutines();

        currentStep.finished_effect();

        //windowTxt.event_Close -= CloseButton;

        //windowTxt.Close_Force();
        SpellGestion.AddStartingSpellToTheToolbar();
        ClickAutorization.Exception_Clear();
        ClickAutorization.Remove(this.gameObject);
        Main_Object.Modify_all(true);
        if (V.game_state != V.State.fight)
            Main_Object.Disable(Main_Object.objects.button_endOfTurn);

        Destroy(this.gameObject);
    }
}

public class step
{
    public bool finishedEffectIsDone;

    public virtual void finished_effect()
    {
        if (finishedEffectIsDone)
            return;

        finishedEffectIsDone = true;

        restrictionManagement(false);
    }

    public bool finishedBool;

    public virtual bool finished()
    {
        return finishedBool;
    }

    public Window_Txt windowTxt { get { return tuto.windowTxt; } set { tuto.windowTxt = value; } }

    public Tutorial tuto { get { return Tutorial.tuto; } }

    public virtual void restrictionManagement(bool apply) { }

    public virtual void takeEffect() { }

    public virtual bool exitButton()
    {
        if (tuto.WonderingIfClosing)
            return false;

        finishedBool = true;
        return true;
    }

}