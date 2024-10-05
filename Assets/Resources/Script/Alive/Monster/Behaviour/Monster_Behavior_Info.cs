using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Monster_Behavior_Info : MonoBehaviour
{
    public Monster monster { get; private set; }

    public string coroutineName { get; private set; }

    public Monster_Behavior_Info(Monster monster)
    {
        this.monster = monster;
        coroutineName = "Behavior_" + monster.monsterInfo.monster_type.ToString();
    }

    public int RealPm
    {
        get
        {
            if(monster is null || monster.Info is null)
                return 0;
            return monster.Info.GetRealPm();
        }
    }

    public bool CanMove()
    {
        return RealPm > 0;
    }

    public bool ShouldStopToBehave()
    {
        return !EntityOrder.InstanceEnnemy.IsTurnOfMonster() || V.game_state != V.State.fight || monster.IsDead();
    }
}
