using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Entity : MonoBehaviour
{
    public EventHandlerEntity event_ChangeOfDirectionRun = new EventHandlerEntity(false);
    public EventHandlerEntity event_StartOfRun = new EventHandlerEntity(false);
    public EventHandlerEntity event_EndOfRun = new EventHandlerEntity(false);

    public virtual void StartOfRun()
    {
        ParticleLeaf(nbParticleEndOfRun / 2, 0.6f);

        event_StartOfRun.Call(this);
    }

    public virtual void StopRun() {
        runningInfo.stop();
    }

    public virtual void ChangeRun() { }

    public virtual void EndOfRun(RunningInfo.Mode mode = RunningInfo.Mode.run)
    {
        StopRun();

        if (mode == RunningInfo.Mode.run)
            ParticleLeaf(nbParticleEndOfRun, 1);
        else
            ParticleLeaf(nbParticleEndOfRun / 2, 0.9f);

        runningInfo.SetDirectionToArea(DirectionData.Direction.empty);

        event_EndOfRun.Call(this);

        runningInfo.SetNextPos("999_999");
    }

}
