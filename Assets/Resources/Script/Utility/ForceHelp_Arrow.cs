using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ForceHelp_Arrow : MonoBehaviour
{
    public static GameObject Arrow_gameobject;

    public delegate bool CheckWhenDestroyed();

    public CheckWhenDestroyed check;

    /// <summary>
    /// Origin is optional
    /// </summary>
    /// <param name="Position"></param>
    /// <param name="check"></param>
    /// <param name="origin"></param>
    public static ForceHelp_Arrow Instantiate_Arrow(Vector3 Position, CheckWhenDestroyed check)
    {
        if (Arrow_gameobject is null)
            Arrow_gameobject = Resources.Load<GameObject>("Prefab/Animation/ForceHelper_arrow");

        GameObject g = Instantiate(Arrow_gameobject);

        ForceHelp_Arrow script = g.GetComponent<ForceHelp_Arrow>();

        script.check = check;

        float YDistance = 1.5f;

        script.Position_Start = Position + new Vector3(0, YDistance, 0);
        script.Position_End = Position + new Vector3(0, YDistance + 1, 0);

        g.transform.position = script.Position_End;

        script.GoToStart = true;

        return script;
    }


    /// <summary>
    /// Origin is optional
    /// </summary>
    /// <param name="check"></param>
    /// <param name="origin"></param>
    public static ForceHelp_Arrow Instantiate_Arrow_EndOfTurn(CheckWhenDestroyed check)
    {
        return ForceHelp_Arrow.Instantiate_Arrow(Main_Object.list[Main_Object.objects.button_endOfTurn].transform.position - new Vector3(1, -1, 0), check);
    }


    [HideInInspector]
    public Vector3 Position_Start, Position_End;

    [HideInInspector]
    public bool GoToStart;

    public void Update()
    {
        float Speed = 2;

        if (GoToStart)
        {
            transform.DOMoveY(Position_Start.y, Speed);
            if (transform.position.y <= Position_Start.y + 0.2f)
            {
                GoToStart = false;
            }
        }
        else
        {
            transform.DOMoveY(Position_End.y, Speed);
            if (transform.position.y >= Position_End.y - 0.2f)
            {
                GoToStart = true;
            }
        }

        if (check())
            Destroy(this.gameObject);
    }
}
