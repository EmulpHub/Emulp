using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_kill : Action
{
    public Entity target, caster;
    string saveName;

    public Action_kill(Entity target, Entity caster) : base()
    {
        type = Type.kill;
        CanBeErased = false;
        this.target = target;
        this.caster = caster;
        saveName = target.Info.EntityName;
    }

    internal override void Execute_main()
    {
        target.Kill(new InfoKill(caster));
    }

    public override bool Finished()
    {
        if (base.Finished()) return true;

        return Executed;
    }

    public static void Add(Entity target, Entity caster)
    {
        Action_kill actionToAdd = new Action_kill(target, caster);

        toDo.Add(actionToAdd);
    }

    public static void Add_Kill_prioritary(Entity target, Entity caster)
    {
        Action_kill actionToAdd = new Action_kill(target, caster);

        toDo.Insert(0, actionToAdd);
    }

    public override string debug()
    {
        return descColor.convert("*dmgkill*end " + saveName);
    }
}
