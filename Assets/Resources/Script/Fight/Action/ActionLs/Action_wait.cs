using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_wait : Action
{
    public Action_wait(float Cooldown) :base()
    {
        type = Type.wait;
        this.Cooldown = Cooldown;
    }

    public float Cooldown;

    internal override void Execute_main()
    {
        Cooldown -= 1 * Time.deltaTime;
    }

    public override bool Finished()
    {
        if (base.Finished()) return true;

        return Cooldown <= 0;
    }

    public static void Add(float timeInSec)
    {
        Action_wait actionToAdd = new Action_wait(timeInSec);

        toDo.Add(actionToAdd);
    }

    public static void Add_prioritary(float timeInSec)
    {
        Action_wait actionToAdd = new Action_wait(timeInSec);

        ActionRunning.Insert(0, actionToAdd);
    }

    public override string debug()
    {
        return descColor.convert("Wait *spe" + F.ShowXdecimal(Cooldown,1)+"*end");
    }
}
