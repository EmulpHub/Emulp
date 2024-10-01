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

    public override IEnumerator Execution()
    {
        Action_nextTurn.Add(info.monster);

        yield break;
    }
}
