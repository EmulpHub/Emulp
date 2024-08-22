using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PathFindingName
{
    public class SorterPos : MonoBehaviour
    {
        public delegate List<string> Sorter(List<string> listPos);

        public static string Take(string start, int radius, Condition researchCondition, Sorter sorter, Condition selectionCondition = null)
        {
            List<string> listPos = RadiusPos.List(start, radius, researchCondition, selectionCondition);

            if (listPos.Count == 0) return "";

            listPos = sorter(listPos);

            return listPos.First();
        }
    }
}