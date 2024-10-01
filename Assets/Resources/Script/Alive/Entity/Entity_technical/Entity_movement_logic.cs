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

            GoHere(nextPos, speedThis);

            runningInfo.SetNextPos(nextPos);


            if (i % 2 == 0 && !V.IsFight())
                Spell.Reference.CreateParticle_Leaf(transform.position, 0.6f);

            i++;

            MoveOne();

            yield return new WaitForSeconds(speedThis);
        }

        if (endOfRun && runningInfo.CanStillRun(saveRunId))
            EndOfRun(mode);
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
