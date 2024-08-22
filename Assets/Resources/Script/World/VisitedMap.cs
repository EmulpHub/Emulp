using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LayerMap
{

    public class VisitedMap : MonoBehaviour
    {
        public static List<string> visitedMap = new List<string>();

        public static bool contain(string pos)
        {
            return visitedMap.Contains(pos);
        }

        public static void Add(string pos)
        {
            if (visitedMap.Contains(pos)) return;

            visitedMap.Add(pos);
            WorldData.GetMapInfo(pos).AddedToVisitedMap();
        }

        public static void Init()
        {
            visitedMap.Clear();

            Add("0_0");

            WorldLoad.event_loadMap.Add(Add);
        }
    }
}
