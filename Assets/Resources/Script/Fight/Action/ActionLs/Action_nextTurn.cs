using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_nextTurn : Action
{
    public Action_nextTurn(Entity entity) : base()
    {
        type = Type.nextTurn;
        this.entity = entity;
    }

    public Entity entity;

    internal override void Execute_main()
    {

        SpellGestion.ResetSelectionnedSpell();

        V.script_Scene_Main.EnregistredPath_clear();

        //Get to the next turn
        EntityOrder.PassTurn();
    }
    public override bool Finished()
    {
        if (base.Finished()) return true;

        return Executed;
    }

    public static List<Action_nextTurn> getToDoNextTurn()
    {
        List<Action_nextTurn> ls = new List<Action_nextTurn>();

        foreach (Action action in toDo)
        {
            if (action.type == Type.nextTurn)
                ls.Add((Action_nextTurn)action);
        }

        return ls;
    }

    /// <summary>
    /// Add a passTurn action
    /// </summary>
    /// <param name="entity">The entity that passTurn (for avoid double pass)</param>
    public static void Add(Entity entity)
    {
        //Check if the same entity doesn't ask to pass turn if so don't add more new turn and stop this void
        foreach (Action_nextTurn action in getToDoNextTurn())
        {
            if (action.entity == entity)
                return;

        }

        //The next action To Add
        Action_nextTurn actionToAdd = new Action_nextTurn(entity);

        //Add the action to the list
        toDo.Add(actionToAdd);
    }

    public override string debug()
    {
        return descColor.convert("pass turn for *RES" + entity.Info.EntityName + "*end");
    }
}
