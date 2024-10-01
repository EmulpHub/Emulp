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

    public void Start()
    {
        StartCoroutine(Manage());
    }

    public IEnumerator Manage ()
    {
        while (true)
        {
            yield return new WaitUntil(() => V.IsFight() && !EntityOrder.IsTurnOf_Player());

            Monster monster = (Monster)EntityOrder.entity;

            monster.CombatBehavior.Behave();

            yield return new WaitUntil(() => !ActionManager.Instance.Running());
        }
    }
}
