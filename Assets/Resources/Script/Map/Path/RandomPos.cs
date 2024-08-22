using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PathFindingName
{
    public class RandomPos : MonoBehaviour
    {
        public static string Take(string start, int radius, Condition researchCondition, Condition selectionCondition)
        {
            List<string> listPos = RadiusPos.List(start, radius, researchCondition);

            List<string> listPossiblePos = listPos.Where(a => selectionCondition(a)).ToList();

            if (listPossiblePos.Count == 0) return "";

            return listPossiblePos[Random.Range(0, listPossiblePos.Count)];
        }
    }
}