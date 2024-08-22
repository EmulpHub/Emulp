using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public partial class Map_PossibleToMove : MonoBehaviour
{
    private void Update()
    {
        var pos = CursorInfo.Instance.positionInWorld;

        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }
}
