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

    public float Cooldown;

    public float StartTimeCode;

    internal override void Execute_main()
    {
        //On ne fait rien
    }

    public override bool Finished()
    {
        if (base.Finished()) return true;

        return Time.time - StartTimeCode >= Cooldown;
    }

    public static void Add(float timeInSec)
    {
        Action_waitFixed actionToAdd = new Action_waitFixed(timeInSec);

        toDo.Add(actionToAdd);
    }

    public static void Add_wait_prioritary(float timeInSec)
    {
        Action_waitFixed actionToAdd = new Action_waitFixed(timeInSec);

        ActionRunning.Insert(0, actionToAdd);
    }


    public override string debug()
    {
        return descColor.convert("Wait fixed *spe" + F.ShowXdecimal(Cooldown, 1) +"*end");
    }
}
