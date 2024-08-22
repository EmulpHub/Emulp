using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using PathFindingName;


public class MovementInfo : MonoBehaviour
{
    static int IdMax = 1;

    private int _id = -1;

    public int Id
    {
        get
        {
            if (_id == -1)
                _id = IdMax++;

            return _id;
        }
    }


    public PathResult pathResult;

    public MovementInfo(PathParam param)
    {
        pathResult = Path.Find(param);

    }
}
