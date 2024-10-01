using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_kill : Action
{
    public Entity target, caster;
    private string target_name;

    public Action_kill(Entity target, Entity caster) : base()
    {
        type = Type.kill;
        CanBeErased = false;
        this.target = target;
        this.caster = caster;
        target_name = target.Info.EntityName;
    }

    internal override IEnumerator Execute_main()
    {
        target.Kill(new InfoKill(caster));

        yield return null;
    }

    public static void Add(Entity target, Entity caster)
    {
        Action_kill actionToAdd = new Action_kill(target, caster);
        ActionManager.Instance.AddToDo(actionToAdd);

    }

    public static void Add_prioritary(Entity target, Entity caster)
    {
        Action_kill actionToAdd = new Action_kill(target, caster);
        ActionManager.Instance.AddToDo_prioritary(actionToAdd);

    }

    public override string debug()
    {
        return descColor.convert("*dmgkill*end " + target_name);
    }
}
