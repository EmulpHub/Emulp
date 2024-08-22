using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Diagnostics;

public partial class PathFinding : MonoBehaviour
{
    public static string ClosestTileToTp(string casterPos, string targetPos, int maxDistance = 999)
    {
        List<string> neighboreRelative = new List<string> { "1_0", "-1_0", "0_1", "0_-1" };

        List<string> posDone = new List<string> { };

        List<string> posToDo = new List<string> { targetPos };

        List<string> posToDoNext = new List<string>();

        while (posToDo.Count > 0)
        {
            string current = posToDo[0];

            foreach (string s in neighboreRelative)
            {
                string newPos = F.AdditionPos(current, s);

                if (posDone.Contains(newPos) || newPos == casterPos || newPos == targetPos || !F.IsTileWalkable(newPos))
                    continue;

                int disToCaster = F.DistanceBetweenTwoPos(newPos, casterPos);

                if (disToCaster <= maxDistance)
                    return newPos;

                posToDoNext.Add(newPos);
            }

            posDone.Add(current);

            posToDo.RemoveAt(0);

            if (posToDo.Count == 0)
            {
                posToDo = new List<string>(posToDoNext);

                posToDoNext.Clear();
            }
        }

        return "999_999";
    }
}
