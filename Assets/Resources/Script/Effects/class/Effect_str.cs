using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Effect : MonoBehaviour
{
    private int str;

    public int Str
    {
        get
        {
            return str;
        }
    }

    public enum AddMode { addition, multiplication };

    internal AddMode addMode;

    public virtual void setAddMode()
    {
        addMode = AddMode.addition;
    }

    public void AddStr(int toAdd)
    {
        if (addMode == AddMode.addition)
        {
            SetStrenght(str + toAdd);
        }
        else if (addMode == AddMode.multiplication)
        {
            SetStrenght(Mathf.RoundToInt((float)str * ((float)toAdd / 100) + toAdd + str));
        }
    }

    public void RemoveStr(int toRemove)
    {
        if (addMode == AddMode.addition)
        {
            SetStrenght(str - toRemove);
        }
        else
        {
            SetStrenght(Mathf.RoundToInt((float)str * (-(float)toRemove / 100) + str - toRemove));
        }
    }


    public void SetStrenght(int newStrenght)
    {
        event_strChanging.Call(str,newStrenght);

        str = newStrenght;

        UpdateStr();

        if (str <= 0)
        {
            Kill(false);
        }
    }

    public virtual void UpdateStr()
    {

    }
}
