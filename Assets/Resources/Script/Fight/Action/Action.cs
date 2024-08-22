using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All the value for one specific Action
public partial class Action : MonoBehaviour
{
    public int id = 0;

    static int idMax = 0;

    public Action()
    {
        id = idMax;
        idMax++;
    }

    public bool CanBeErased = true, Executed = false;

    public enum Type { movement, nextTurn, wait, wait_fixed, spell, kill }

    public Type type;

    public void Execute()
    {
        V.player_info.CalculateValue();

        Executed = true;

        Execute_main();

        List<Type> EachFrameTypeCalculate = new List<Type>
        { Type.spell, Type.movement,  Type.kill };

        if (EachFrameTypeCalculate.Contains(type))
        {
            Character.CalcValueOfEveryone();
        }
    }

    internal virtual void Execute_main() { }

    public virtual bool Finished()
    {
        return Force_Stop;
    }

    [HideInInspector]
    public bool Force_Stop = false;

    public void Stop()
    {
        Force_Stop = true;
    }

    public virtual string debug()
    {
        return "ERROR";
    }
}
