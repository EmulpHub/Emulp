using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFindingName;

public partial class MonsterBehavior : MonoBehaviour
{
    public MonsterBehaviorResult Behave()
    {
        MonsterAction action = ChooseAction();

        return action.CallExecution();
    }
}

public class MonsterBehaviorResult
{
    public Action action { get; private set; } = null;
    public bool allowMultiAction { get; private set; } = true;
    public bool passTurn { get; private set; } = false;

    public void SetPassTurn (bool passTurn)
    {
        this.passTurn = passTurn;
    }

    public void SetMultiAction (bool AllowMultiAction)
    {
        this.allowMultiAction = AllowMultiAction;
    }

    public void SetAction (Action action)
    {
        this.action = action;
    }
}
