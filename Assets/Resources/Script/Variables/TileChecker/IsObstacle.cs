using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsObstacle : MonoBehaviour
{
    public static bool Check (string pos)
    {
        if (Main_Map.ground_above.HasTile(F.ConvertToVector3Int(pos)))
            return true;

        return false;
    }
}
