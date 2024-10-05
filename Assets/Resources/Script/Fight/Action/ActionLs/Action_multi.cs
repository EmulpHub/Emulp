using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_multi : Action
{
    public Action_multi () : base()
    {
        type = Type.multi;
    }

    private List<Action> listAction = new List<Action>();

    protected override IEnumerator Execute_main()
    {
        foreach(Action action in listAction)
        {
            action.Execute();
        }

        yield return null;
    }

    public override bool IsFinished ()
    {
        foreach (Action action in listAction)
        {
            if (!action.IsFinished())
                return false;
        }

        return base.IsFinished();
    }

    public static void Add (List<Action> listAction)
    {
        Action_multi action_Multi = new Action_multi();

        action_Multi.listAction = listAction;

        ActionManager.Instance.AddToDo(action_Multi);
    }
}
