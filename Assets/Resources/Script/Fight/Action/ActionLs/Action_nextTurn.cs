using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_nextTurn : Action
{
    public Action_nextTurn() : base()
    {
        type = Type.nextTurn;
    }

    protected override IEnumerator Execute_main()
    {
        SpellGestion.ResetSelectionnedSpell();

        V.script_Scene_Main.EnregistredPath_clear();

        EntityOrder.Instance.PassTurn();

        yield return null;
    }
    public override bool IsFinished()
    {
        return base.IsFinished();
    }

    public static void Add()
    {
        ActionManager.Instance.AddToDo(Create());
    }

    public static Action_nextTurn Create ()
    {
        return new Action_nextTurn();
    }

    public override string debug()
    {
        return descColor.convert("pass turn");
    }
}


//public static List<Action_nextTurn> getToDoNextTurn()
//{
//    List<Action_nextTurn> ls = new List<Action_nextTurn>();

//    foreach (Action action in ActionManager.Instance.listOfToDoAction)
//    {
//        if (action.type == Type.nextTurn)
//            ls.Add((Action_nextTurn)action);
//    }

//    return ls;
//}

///// <summary>
///// Add a passTurn action
///// </summary>
///// <param name="entity">The entity that passTurn (for avoid double pass)</param>
//public static void Add(Entity entity)
//{
//    //Check if the same entity doesn't ask to pass turn if so don't add more new turn and stop this void
//    foreach (Action_nextTurn action in getToDoNextTurn())
//    {
//        if (action.entity == entity)
//            return;

//    }

//    Action_nextTurn actionToAdd = new Action_nextTurn(entity);

//    ActionManager.Instance.AddToDo(actionToAdd);
//}
