using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Effect : MonoBehaviour
{
    public enum Reduction_mode { endTurn, startTurn, never, permanent }

    public Reduction_mode reduction_Mode;

    private int durationInTurn;

    public int DurationInTurn
    {
        get
        {
            return durationInTurn;
        }
    }

    public bool ShouldNeverExhaust
    {
        get
        {
            return reduction_Mode == Reduction_mode.never || reduction_Mode == Reduction_mode.permanent;
        }
    }

    public void SetDuration(int newDuration)
    {
        int old = durationInTurn;

        durationInTurn = newDuration;

        WhenDurationChange(old);

        CheckIfDeadAndKill();
    }

    public void ReduceDuration(int toReduce)
    {
        SetDuration(durationInTurn - toReduce);
    }

    public virtual void EndTurn()
    {
        ReduceDuration(1);
    }

    public virtual void WhenDurationChange (int oldDuration) {

    }
}
