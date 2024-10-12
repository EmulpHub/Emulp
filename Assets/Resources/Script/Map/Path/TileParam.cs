using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileParam : MonoBehaviour
{
    public List<string> listIgnore { get; set; } = new List<string>();
    public List<string> listNotWalkable { get; set; } = new List<string>();

    public TileParam(List<string> listIgnore, List<string> listNotWalkable = null)
    {
        this.listIgnore = listIgnore;

        if(listNotWalkable == null)
            listNotWalkable = 

        this.listNotWalkable = listNotWalkable;
    }
}
