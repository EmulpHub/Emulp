using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class Entity : MonoBehaviour
{
    [HideInInspector]
    public Vector2Int CurrentPosition
    {
        get
        {
            return F.ConvertToVector2Int(CurrentPosition_string);
        }
    }

    public string _currentPosition_string;

    [HideInInspector]
    public string CurrentPosition_string
    {
        get
        {
            return _currentPosition_string;
        }
    }

    public void SetPosition(string pos)
    {
        _currentPosition_string = pos;

        transform.DOKill();
        transform.position = F.ConvertToWorldVector2(pos);
    }

    public void CheckAChangeOfPosition()
    {
        string newPos = F.ConvertToStringDependingOfGrid(transform.position);

        if (newPos == CurrentPosition_string) return;

        _currentPosition_string = newPos;

        Glyphe.CheckGlypheForAnEntity(this);

        Info.CalcTackle();
    }
}
