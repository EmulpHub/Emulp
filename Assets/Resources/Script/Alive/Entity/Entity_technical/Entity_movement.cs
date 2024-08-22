using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFindingName;

public partial class Entity : MonoBehaviour
{
    public float runSpeed, runSpeed_combat;
    public float walkSpeed, walkSpeed_combat;

    public virtual void StopRun()
    {
        currentMovement = null;
        runningInfo.isRunning = false;
    }

    public EventHandlerEntity event_ChangeOfDirectionRun = new EventHandlerEntity(false);

    public EventHandlerEntity event_StartOfRun = new EventHandlerEntity(false);
    public EventHandlerEntity event_EndOfRun = new EventHandlerEntity(false);

    [HideInInspector]
    public MovementInfo currentMovement;

    [HideInInspector]
    public RunningInfo runningInfo;

    public virtual float ChooseSpeed()
    {
        if (runningInfo.mode == RunningInfo.Mode.walk)
            return V.game_state == V.State.fight ? walkSpeed_combat : walkSpeed;
        else
            return V.game_state == V.State.fight ? runSpeed_combat : runSpeed;
    }

    public IEnumerator Run(MovementInfo infoMovement)
    {
        runningInfo.isRunning = true;

        RunningInfo.Mode mode = runningInfo.mode;

        float speed = ChooseSpeed();

        int i = 0;

        StartOfRun();

        var path = infoMovement.pathResult.path;

        while (i < path.Count && (currentMovement is not null && currentMovement.Id == infoMovement.Id) && CanMoveOne())
        {
            string nextPos = path[i];

            float speedThis = calcSpeed(speed, CurrentPosition_string, nextPos);

            GoHere(nextPos, speedThis);

            runningInfo.NextPos = nextPos;

            var distance = F.DistanceBetweenTwoPos_inVector2Int(CurrentPosition_string, nextPos);

            runningInfo.TowardRight = distance.x > 0 || distance.y < 0;

            if (i % 2 == 0 && !V.IsFight())
                Spell.Reference.CreateParticle_Leaf(transform.position, 0.6f);

            i++;

            MoveOne();

            yield return new WaitForSeconds(speedThis);
        }

        if (currentMovement is not null && currentMovement.Id == infoMovement.Id)
            StopRun();

        if (currentMovement is null)
            EndOfRun(mode);
    }

    public static int nbParticleEndOfRun = 2;

    public virtual void EndOfRun(RunningInfo.Mode mode = RunningInfo.Mode.run)
    {
        if (mode == RunningInfo.Mode.run)
            ParticleLeaf(nbParticleEndOfRun, 1);
        else
            ParticleLeaf(Mathf.CeilToInt((float)nbParticleEndOfRun / 2), 0.9f);

        runningInfo.directionTowardNewArea = DirectionData.Direction.empty;

        event_EndOfRun.Call(this);
    }

    public virtual void StartOfRun()
    {
        ParticleLeaf(Mathf.CeilToInt((float)nbParticleEndOfRun / 2), 0.6f);

        event_StartOfRun.Call(this);
    }

    public virtual float calcSpeed(float baseValue, string pos1, string pos2)
    {
        return baseValue;
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

    protected virtual void Treat_PathParam(PathParam pathParam)
    {

    }

    public virtual bool MoveTo(PathParam pathParam)
    {
        Treat_PathParam(pathParam);

        var movement = new MovementInfo(pathParam);

        if (movement.pathResult.LengthInSquare > RunningInfo.walkMaxLenght)
            runningInfo.mode = RunningInfo.Mode.run;
        else
            runningInfo.mode = RunningInfo.Mode.run;

        if (V.IsFight())
        {
            V.script_Scene_Main.EnregistredPath_clear();

            CTInfo.Instance.ListTile_Clear();

            var path = movement.pathResult.path;

            for (int i = 0; i < Info.GetRealPm() && i < path.Count; i++)
            {
                CT_Graphic.Add(path[i], CT_Gestion.Color.green_light, true, true, true);
            }
        }

        return Path_assignate(movement);
    }

    public bool Path_assignate(MovementInfo infoMovement)//List<V.LegalMove> newPath, Vector2Int endPosition, List<Entity> ignore_entity, string forcedFirstMove)
    {
        if (infoMovement.pathResult.path.Count == 0) return false;

        string start = CurrentPosition_string;
        string end = infoMovement.pathResult.endOfPath;

        if (start != end)
        {
            currentMovement = infoMovement;

            event_ChangeOfDirectionRun.Call(this);

            StartCoroutine(Run(infoMovement));

            return true;
        }

        return false;
    }
}

public class RunningInfo
{
    public enum Mode { walk, run }

    public Mode mode;

    private Entity holder;

    public bool isRunning;

    public bool TowardRight;

    private string _nextPos = "";

    public string NextPos
    {
        get
        {
            if (_nextPos == "")
                _nextPos = holder.CurrentPosition_string;

            return _nextPos;
        }
        set
        {
            _nextPos = value;
        }
    }

    public static int walkMaxLenght = 3;

    public DirectionData.Direction directionTowardNewArea = DirectionData.Direction.empty;

    public RunningInfo(Entity holder)
    {
        this.holder = holder;
    }
}