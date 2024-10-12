using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAction_PassTurn : MonsterAction
{
    public MonsterAction_PassTurn(PriorityLayer layer, int priority) : base(layer,priority)
    {

    }

    public override bool Condition()
    {
        return true;
    }

    protected override IEnumerator Execution(MonsterBehaviorResult result)
    {
        throw new System.Exception("This must be manager in monster Behavior Manager");

    }
}
