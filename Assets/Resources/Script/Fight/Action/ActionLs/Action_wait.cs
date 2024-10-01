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

    internal override IEnumerator Execute_main()
    {
        while (Cooldown > 0)
        {
            Cooldown -= Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
    }

    public override bool IsFinished()
    {
        return Cooldown <= 0 && base.IsFinished();
    }

    public static void Add(float timeInSec)
    {
        Action_wait actionToAdd = new Action_wait(timeInSec);

        ActionManager.Instance.AddToDo(actionToAdd);
    }

    public static void Add_prioritary(float timeInSec)
    {
        Action_wait actionToAdd = new Action_wait(timeInSec);

        ActionManager.Instance.AddToDo_prioritary(actionToAdd);
    }

    public override string debug()
    {
        return descColor.convert("Wait *spe" + F.ShowXdecimal(Cooldown,1)+"*end");
    }
}
