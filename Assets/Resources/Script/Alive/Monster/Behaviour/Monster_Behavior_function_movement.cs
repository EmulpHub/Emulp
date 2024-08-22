using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFindingName;
using System.Linq;

public partial class Monster : Entity
{
    public IEnumerator MoveTowardEntity(Entity target, bool withLineOfView, int minDistance, int maxDistance, bool InLine)
    {
        string targetPos = target.CurrentPosition_string;

        List<string> sorter(List<string> listPos)
        {
            return listPos.
                OrderBy(a => F.DistanceBetweenTwoPos(a, targetPos)).
                ThenBy(b => Path.Find(new PathParam(CurrentPosition_string, b)).path.Count).ToList();
        }

        bool research(string pos)
        {
            return F.IsTileWalkable(pos);
        }

        bool selection(string pos)
        {
            if (InLine && !F.IsInLine(pos, targetPos)) return false;

            if (withLineOfView && !F.IsLineOfView(pos, targetPos)) return false;

            int distance = F.DistanceBetweenTwoPos(pos, targetPos);

            if (distance < minDistance || distance > maxDistance) return false;

            return true;
        }

        string targetSquare = SorterPos.Take(targetPos, maxDistance, research, sorter, selection);

        Action_movement.Add(new PathParam(CurrentPosition_string, targetSquare), this);

        StartWhenNoAction(currentCoroutine_name, 0.1f, false);
        yield return null;
    }

    public IEnumerator MoveTowardEntity(Entity target, bool withLineOfView, int minDistance, int maxDistance)
    {
        StartCoroutine(MoveTowardEntity(target, withLineOfView, minDistance, maxDistance, false));
        yield break;
    }

    public IEnumerator MoveTowardEntity(Entity target, bool withLineOfView, int stopDistance, bool InLine)
    {
        StartCoroutine(MoveTowardEntity(target, withLineOfView, 1, stopDistance, InLine));
        yield break;
    }

    public IEnumerator MoveTowardEntity(Entity target, bool withLineOfView, int stopDistance)
    {
        StartCoroutine(MoveTowardEntity(target, withLineOfView, 1, stopDistance, false));
        yield break;
    }

    public IEnumerator MoveAwayFromEntity(Entity target, int maxDistance, bool withLineOfView = false)
    {
        string targetPos = target.CurrentPosition_string;

        List<string> sorter(List<string> listPos)
        {
            return listPos.
                OrderByDescending(a => F.DistanceBetweenTwoPos(a, targetPos)).
                ThenBy(b => Path.Find(new PathParam(CurrentPosition_string, b)).path.Count).ToList();
        }

        bool research(string pos)
        {
            return F.IsTileWalkable(pos);
        }

        bool selection(string pos)
        {
            if (withLineOfView && !F.IsLineOfView(pos, targetPos)) return false;

            int distance = F.DistanceBetweenTwoPos(pos, targetPos);

            if (distance > maxDistance) return false;

            return true;
        }

        string targetSquare = SorterPos.Take(targetPos, Info.GetRealPm(), research, sorter, selection);

        Action_movement.Add(new PathParam(CurrentPosition_string, targetSquare), this);

        StartWhenNoAction(currentCoroutine_name, 0.1f, false);
        yield return null;
    }

    public IEnumerator MoveAwayFromEntity(Entity Target)
    {
        StartCoroutine(MoveAwayFromEntity(Target, 999));

        yield return new WaitForSeconds(0);
    }
}
