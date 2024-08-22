using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PathFindingName
{
    public class RadiusPos : MonoBehaviour
    {
        public static List<string> List(string start, int radius, Condition researchCondition, Condition selectionCondition = null)
        {
            List<string> listPossiblePos = new List<string>();

            List<string> open = new List<string>() { start };

            void Check(string pos)
            {
                if (pos == start) return;

                if (listPossiblePos.Contains(pos)) return;

                if (F.DistanceBetweenTwoPos(pos, start) <= radius && researchCondition(pos))
                {
                    listPossiblePos.Add(pos);
                    open.Add(pos);
                }
            }

            while (open.Count > 0)
            {
                string tile = open.First();

                open.RemoveAt(0);

                Check(F.AdditionPos(tile, "1_0"));
                Check(F.AdditionPos(tile, "-1_0"));
                Check(F.AdditionPos(tile, "0_1"));
                Check(F.AdditionPos(tile, "0_-1"));
            }

            if (selectionCondition == null)
                return listPossiblePos;
            else
                return listPossiblePos.Where(a => selectionCondition(a)).ToList();
        }
    }
}