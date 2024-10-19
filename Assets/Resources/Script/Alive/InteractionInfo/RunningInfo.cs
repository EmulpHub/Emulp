using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFindingName;

public class RunningInfo
{
    private static int nextRunId = 0;

    public int runId { get; private set; }

    public PathResult pathResult { get; private set; }

    public List<string> walkablePath { get; private set; }

    public enum Mode { walk, run }

    public Mode mode { get; private set; }

    private Entity holder;

    public bool running { get; private set; }

    public bool towardRight { get; private set; }

    public DirectionData.Direction directionToArea { get; private set; }

    private string _nextPos = "999_999";

    public float speed { get; private set; }
    
    public string NextPos
    {
        get
        {
            return _nextPos;
        }
        private set
        {
            _nextPos = value;
        }
    }

    public void SetSpeed(float value)
    {
        speed = value;
    }
    public void SetPath (PathResult path)
    {
        this.pathResult = path;
    }

    public void SetWalkablePath(List<string> path)
    {
        this.walkablePath = path;
    }

    public void Run()
    {
        bool alreadyRunning = running;

        running = true;
        runId = nextRunId;
        nextRunId++;

        if (!alreadyRunning || mode == Mode.walk)
        {
            if (pathResult.LengthInSquare > 2)
                mode = RunningInfo.Mode.run;
            else
                mode = RunningInfo.Mode.walk;
        }
    }

    public void stop()
    {
        SetNextPos("999_999");
        running = false;
    }

    public bool CanStillRun(int id)
    {
        if (id != runId) return false;
        if (!running) return false;

        return true;
    }

    public void SetNextPos (string nextPos)
    {
        NextPos = nextPos;

        var distance = F.DistanceBetweenTwoPos_inVector2Int(holder.CurrentPosition_string, nextPos);

        towardRight = distance.x > 0 || distance.y < 0;
    }

    public void SetDirectionToArea (DirectionData.Direction dir)
    {
        directionToArea = dir;
    }

    public RunningInfo(Entity holder)
    {
        this.holder = holder;
    }

}