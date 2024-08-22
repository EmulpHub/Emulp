using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PathFindingName
{
    public class PathAllAround : MonoBehaviour
    {
        public static Dictionary<string, List<string>> Make(string start, int radius)
        {
            var result = new Dictionary<string, List<string>>();

            List<string> open = new List<string>() { start };

            List<string> close = new List<string>();

            void Check(string pos)
            {
                if (pos == start)
                    return;

                if (!F.IsTileWalkable(pos) || F.DistanceBetweenTwoPos(pos, start) > radius)
                    return;

                if (close.Contains(pos) || open.Contains(pos))
                    return;

                open.Add(pos);
            }

            while (open.Count > 0)
            {
                var pos = open.First();

                Check(F.AdditionPos(pos, "0_1"));
                Check(F.AdditionPos(pos, "0_-1"));
                Check(F.AdditionPos(pos, "1_0"));
                Check(F.AdditionPos(pos, "-1_0"));

                result.Add(pos, Path.Find(new PathParam(start, pos)).path);

                open.RemoveAt(0);
                close.Add(pos);
            }

            return result;
        }
    }
}
