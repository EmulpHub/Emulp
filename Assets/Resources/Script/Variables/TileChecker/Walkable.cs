using PathFindingName;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkable : MonoBehaviour
{
    public static bool Check(string pos, WalkableParam param)
    {
        if (param.listForbideenPos.Contains(pos))
            return false;

        if (param.CheckSeenable && !Seenable.Check(pos))
            return false;

        if (!Exist.Check(pos))
            return false;

        if (IsObstacle.Check(pos))
            return false;

        return true;
    }

    public static List<string> GetCommonForbideenPos ()
    {
        var result = new List<string>(AliveEntity.Instance.GetListPosition());

        result.AddRange(ObstacleStatic.list.Keys);

        return result;
    }
}

public class WalkableParam
{
    public static WalkableParam GetCommonParam()
    {
        return new WalkableParam(Walkable.GetCommonForbideenPos(), true);
    }

    public List<string> listForbideenPos { get; private set; }

    public WalkableParam SetListForbideenPos(List<string> listForbideenPos)
    {
        this.listForbideenPos = listForbideenPos;
        return this;
    }

    public WalkableParam AddToForbideenPos(string forbideenPos)
    {
        this.listForbideenPos.Add(forbideenPos);
        return this;
    }

    public WalkableParam AddToForbideenPos(List<string> listForbideenPos)
    {
        this.listForbideenPos.AddRange(listForbideenPos);
        return this;
    }

    public WalkableParam RemoveToForbideenPos(string forbideenPos)
    {
        this.listForbideenPos.Remove(forbideenPos);
        return this;
    }
    public bool CheckSeenable {  get; private set; }

    public WalkableParam(List<string> listForbideenPos, bool seenable)
    {
        this.listForbideenPos = listForbideenPos;
        CheckSeenable = seenable;
    }
}