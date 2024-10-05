using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_waitFixed : Action
{
    public Action_waitFixed (float timeInSec) : base()
    {
        type = Type.wait_fixed;

        StartTimeCode = Time.time;
        Cooldown = timeInSec;
    }

    private float Cooldown;

    private float StartTimeCode;

    public float GetRemainingTime ()
    {
        return (Time.time - StartTimeCode) - Cooldown;
    }

    protected override IEnumerator Execute_main()
    {
        yield return null;
    }

    public override bool IsFinished()
    {
        return GetRemainingTime() <= 0 && base.IsFinished();
    }

    public override string debug()
    {
        return descColor.convert("Wait fixed *spe" + F.ShowXdecimal(Cooldown, 1) + "*end");
    }

    public static void Add(float timeInSec)
    {
        Action_waitFixed actionToAdd = new Action_waitFixed(timeInSec);

        ActionManager.Instance.AddToDo(actionToAdd);
    }

    public static void Add_prioritary(float timeInSec)
    {
        Action_waitFixed actionToAdd = new Action_waitFixed(timeInSec);

        ActionManager.Instance.AddToDo_prioritary(actionToAdd);
    }

}
