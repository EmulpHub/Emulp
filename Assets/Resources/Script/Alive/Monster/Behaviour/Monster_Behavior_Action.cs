using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class MonsterBehavior : MonoBehaviour
{
    internal List<MonsterAction> listMonsterAction = new List<MonsterAction>();

    private MonsterAction chooseAction_private()
    {
        List<MonsterAction> ListPossibleAction = new List<MonsterAction>();

        foreach (MonsterAction monsterAction in listMonsterAction)
        {
            if (monsterAction.Condition())
                ListPossibleAction.Add(monsterAction);
        }

        MonsterAction action = ListPossibleAction.Where(a => a.priorityLayer == ListPossibleAction.Max(b => b.priorityLayer)).OrderByDescending(c => c.Priority).First();

        return action;
    }

    public void AddAction (MonsterAction action)
    {
        action.info = Info;
        action.monsterBehavior = this;

        listMonsterAction.Add(action);
    }

    public virtual void SetMonsterAction()
    {
        listMonsterAction = new List<MonsterAction>();

        AddAction(new MonsterAction_PassTurn(MonsterAction.PriorityLayer.Turn,0));
    }

}
