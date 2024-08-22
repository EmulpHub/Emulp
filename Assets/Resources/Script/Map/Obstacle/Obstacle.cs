using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [HideInInspector]
    private string _pos;

    public string pos {

        get => _pos;

        set
        {
            _pos = value;

            if (!ObstacleStatic.Contain(_pos))
            {
                ObstacleStatic.Add(this);
            }
        }
    }

    public void SetPos(string pos)
    {
        this.pos = pos;
    }

    public void RemoveFromList ()
    {
        ObstacleStatic.Remove(this);
    }

    public void destroyThis()
    {
        RemoveFromList();

        Destroy(this.gameObject);
    }
}
