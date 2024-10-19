using PathFindingName;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Entity : MonoBehaviour
{
    private IEnumerator RunEnumerator(PathResult pathResult)
    {
        runningInfo.Run();

        int saveRunId = runningInfo.runId;

        RunningInfo.Mode mode = runningInfo.mode;

        float speed = ChooseRunSpeed();

        runningInfo.SetSpeed(speed);

        int i = 0;

        StartOfRun();

        var path = pathResult.path;

        bool endOfRun = true;

        while (i < path.Count && CanMoveOne())
        {
            if (!runningInfo.CanStillRun(saveRunId))
            {
                endOfRun = false;
                break;
            }

            string nextPos = path[i];

            float speedThis = ChooseMoveSpeed(speed, CurrentPosition_string, nextPos);

            if (CanGoToNextPos(pathResult,nextPos))
            {
                runningInfo.SetNextPos(nextPos);

                GoHere(nextPos, speedThis);

                if (i % 2 == 0 && !V.IsFight())
                    Spell.Reference.CreateParticle_Leaf(transform.position, 0.6f);

                i++;

                MoveOne();
            }

            yield return new WaitForSeconds(speedThis);
        }

        if (endOfRun && runningInfo.CanStillRun(saveRunId))
            EndOfRun(mode);
    }

    private bool CanGoToNextPos(PathResult pathResult, string nextPos)
    {
        if (!Walkable.Check(nextPos, pathResult.walkableParam))
            return false;

        bool Condition (Entity e)
        {
            if (e != this && e.runningInfo.NextPos == nextPos)
                return true;

            return false;
        }

        if (AliveEntity.Instance.AnyEntity(Condition))
            return false;

        return true;
    }

    public virtual bool CanMoveOne()
    {
        if (V.game_state == V.State.passive) return true;

        return Info.GetRealPm() > 0;
    }

    public virtual void MoveOne()
    {
        if (V.game_state == V.State.fight)
            Info.PM--;
    }
}
