using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFindingName;

public partial class MonsterBehavior : MonoBehaviour
{
    public void Behave ()
    {
        MonsterAction action = ChooseAction();

        MonsterBehaviorManager.Instance.StartCoroutine(action.Execution());
    }
}
