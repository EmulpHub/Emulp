using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Entity : MonoBehaviour
{
    public float runSpeed, runSpeed_combat;
    public float walkSpeed, walkSpeed_combat;

    public float ChooseRunSpeed()
    {
        if (runningInfo.mode == RunningInfo.Mode.walk)
            return V.game_state == V.State.fight ? walkSpeed_combat : walkSpeed;
        else
            return V.game_state == V.State.fight ? runSpeed_combat : runSpeed;
    }

    public virtual float ChooseMoveSpeed(float baseValue, string pos1, string pos2)
    {
        return baseValue;
    }

}
