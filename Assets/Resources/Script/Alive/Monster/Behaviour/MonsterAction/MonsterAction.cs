using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAction : MonoBehaviour
{
    public enum PriorityLayer { Turn, Movement, Spell }

    public PriorityLayer priorityLayer;

    public int Priority;

    public Monster_Behavior_Info info;
    public MonsterBehavior monsterBehavior;

    public MonsterAction(PriorityLayer layer, int priority)
    {
        priorityLayer = layer;
        Priority = priority;
    }

    public virtual bool Condition()
    {
        return false;
    }

    protected virtual IEnumerator Execution(MonsterBehaviorResult result)
    {
        throw new System.Exception("Not normal monster Action");
    }

    public virtual MonsterBehaviorResult CallExecution()
    {
        var result = new MonsterBehaviorResult();

        MonsterBehaviorManager.Instance.StartCoroutine(Execution(result));

        return result;
    }
}
