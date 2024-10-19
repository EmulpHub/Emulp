using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFindingName;

public partial class Entity : MonoBehaviour
{
    protected virtual void Treat_PathParam(PathParam pathParam) { }

    public virtual bool MoveTo(PathParam pathParam)
    {
        Treat_PathParam(pathParam);

        var pathResult = Path.Find(pathParam);

        return Run(pathResult);
    }

    private bool Run(PathResult pathResult)
    {
        runningInfo.SetPath(pathResult);

        var pathWalkable = new List<string>();

        var path = pathResult.path;

        for (int i = 0; i < Info.GetRealPm() && i < path.Count; i++)
        {
            pathWalkable.Add(path[i]);
        }

        runningInfo.SetWalkablePath(pathWalkable);

        if (pathResult.path.Count == 0) return false;

        string start = CurrentPosition_string;
        string end = pathResult.endOfPath;

        if (start == end) return false;

        event_ChangeOfDirectionRun.Call(this);

        StartCoroutine(RunEnumerator(pathResult));

        return true;
    }
}
