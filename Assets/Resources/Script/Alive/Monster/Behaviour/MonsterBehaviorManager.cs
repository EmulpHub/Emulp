using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviorManager : MonoBehaviour
{
    private static MonsterBehaviorManager _instance;
    public static MonsterBehaviorManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.Find("MonsterBehaviorManager").GetComponent<MonsterBehaviorManager>();
            return _instance;
        }
    }

    public void StartToBehave()
    {
        StartCoroutine(Manage());
    }

    private IEnumerator Manage()
    {
        while (V.IsFight() && !EntityOrder.Instance.IsTurnOf_Player())
        {
            var listMonster = new List<Monster>(EntityOrder.InstanceEnnemy.list);

            List<Action> listAction = new List<Action>();

            Action firstActionToDo = null;

            bool PassTurn = true;

            foreach (Monster m in listMonster)
            {
                var behaviorResult = m.CombatBehavior.Behave();

                if (!behaviorResult.passTurn)
                {
                    if (behaviorResult.action is null)
                        throw new System.Exception("mais bref");

                    if (behaviorResult.allowMultiAction)
                    {
                        listAction.Add(behaviorResult.action);
                    }
                    else if (firstActionToDo is null)
                    {
                        firstActionToDo = behaviorResult.action;
                    }

                    PassTurn = false;
                }
            }

            if (PassTurn)
            {
                Action_nextTurn.Add();
            }
            else if (listAction.Count > 0)
            {
                Action_multi.Add(listAction);
            }
            else if (firstActionToDo is not null)
            {
                ActionManager.Instance.AddToDo(firstActionToDo);
            }

            yield return new WaitUntil(() => !ActionManager.Instance.Running());
        }
    }
}
