using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAction : MonoBehaviour
{
    public enum PriorityLayer { Turn,Other,Movement,Boost,Attack }

    public PriorityLayer priorityLayer;

    public int Priority;

    public Monster_Behavior_Info info;
    public MonsterBehavior monsterBehavior;

    public MonsterAction (PriorityLayer layer,int priority)
    {
        priorityLayer = layer;
        Priority = priority;
    }

    public virtual bool Condition()
    {
        return false;
    }

    public virtual IEnumerator Execution ()
    {
        yield break;
    }
}
