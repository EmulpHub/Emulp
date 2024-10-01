using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PathFindingName
{
    public class PossibleWalkingTile : MonoBehaviour
    {
        /// <summary>
        /// Return a dictionary with his key the pos of the tile and the value the distance to this tile for the entity
        /// </summary>
        /// <param name="start"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static Dictionary<string, int> Make(string start, int radius)
        {
            List<string> open = new List<string>() { start };

            List<string> nextOpen = new List<string>();

            List<string> close = new List<string>();

            var result = new Dictionary<string, int>();

            void Check(string pos)
            {
                if (pos == start)
                    return;

                if (!F.IsTileWalkable(pos) || F.DistanceBetweenTwoPos(pos, start) > radius)
                    return;

                if (nextOpen.Contains(pos) || open.Contains(pos) || close.Contains(pos))
                    return;

                nextOpen.Add(pos);
                close.Add(pos);
            }

            int distance = 0;

            while (open.Count > 0)
            {
                for (int i = 0; i < open.Count; i++)
                {
                    var pos = open[i];

                    Check(F.AdditionPos(pos, "0_1"));
                    Check(F.AdditionPos(pos, "0_-1"));
                    Check(F.AdditionPos(pos, "1_0"));
                    Check(F.AdditionPos(pos, "-1_0"));

                    result.Add(pos, distance);
                }

                open = nextOpen;
                nextOpen = new List<string>();
            }

            return result;
        }
    }
}
