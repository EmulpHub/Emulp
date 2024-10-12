using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFindingName;

public partial class MonsterBehavior : MonoBehaviour
{
    private MonsterAction action;

    public MonsterAction ChooseAction ()
    {
        action = chooseAction_private();
        return action;
    }

    public MonsterBehaviorResult Behave()
    {
        return action.CallExecution();
    }
}

public class MonsterBehaviorResult
{
    public Action action { get; private set; } = null;

    public void SetAction (Action action)
    {
        this.action = action;
    }
}

public class MonsterBehaviorResult_movement : MonsterBehaviorResult
{
    public string EndPos { get; private set; }

    public void SetEndPos(string EndPos)
    {
        this.EndPos = EndPos;
    }
}
