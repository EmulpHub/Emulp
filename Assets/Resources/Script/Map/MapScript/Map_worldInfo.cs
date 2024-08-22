using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LayerMap;

public partial class Map : MonoBehaviour
{
    public IMap info;

    [HideInInspector]
    public string posInWorld
    {
        get
        {
            return info.posInWorld;
        }
    }
}
