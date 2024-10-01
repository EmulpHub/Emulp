using PathFindingName;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static PathFindingName.SorterPos;

namespace PathFindingName
{
    public class PosToFirstConditionTile : MonoBehaviour
    {
        public static string Take(string start, Condition condition)
        {
            var open = new List<string>() { start };

            var nextOpen = new List<string>();

            var close = new List<string>();

            void Check(string posToCheck)
            {
                if (posToCheck == start)
                    return;

                if (!F.IsTileWalkable(posToCheck))
                    return;

                if (open.Contains(posToCheck) || close.Contains(posToCheck) || nextOpen.Contains(posToCheck))
                    return;

                nextOpen.Add(posToCheck);
                close.Add(posToCheck);
            }

            while (open.Count > 0)
            {
                for (int i = 0; i < open.Count; i++)
                {
                    var pos = open[i];

                    Check(F.AdditionPos(pos, "0_1"));
                    Check(F.AdditionPos(pos, "0_-1"));
                    Check(F.AdditionPos(pos, "1_0"));
                    Check(F.AdditionPos(pos, "-1_0"));

                    if (condition(pos))
                        return pos;
                }

                open = nextOpen;
                nextOpen = new List<string>();
            }

            return "";
        }
    }
}