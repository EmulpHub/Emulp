using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapSetterInfo : MonoBehaviour
{
    public Tilemap ground_above;
    public Transform positionningParent;

    public List<string> collectablePos;
    public string monsterPos;
}
