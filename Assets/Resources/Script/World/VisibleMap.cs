using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LayerMap
{
    public class VisibleMap : MonoBehaviour
    {
        private static List<string> list = new List<string>();

        public static bool contain(string pos)
        {
            return list.Contains(pos);
        }

        public static void Init()
        {
            list.Clear();

            Add("0_0");

            WorldLoad.event_loadMap.Add(Add);
        }

        public static void Add(string pos)
        {
            if (!list.Contains(pos))
                list.Add(pos);

            void AddAtPos(string posToAdd)
            {
                if (!list.Contains(posToAdd) && WorldData.World.ContainsKey(posToAdd))
                    list.Add(posToAdd);
            }

            AddAtPos(F.AdditionPos(pos, "0_1"));
            AddAtPos(F.AdditionPos(pos, "0_-1"));
            AddAtPos(F.AdditionPos(pos, "1_0"));
            AddAtPos(F.AdditionPos(pos, "-1_0"));

            WorldData.GetMapInfo(pos).AddedToVisibleMap();
        }

        public static bool NearAVisibleMap(string pos)
        {
            bool checkAtPos(string pos)
            {
                return contain(pos);
            }

            if (checkAtPos(F.AdditionPos(pos, "0_1"))) return true;
            if (checkAtPos(F.AdditionPos(pos, "0_-1"))) return true;
            if (checkAtPos(F.AdditionPos(pos, "1_0"))) return true;
            if (checkAtPos(F.AdditionPos(pos, "-1_0"))) return true;

            return false;
        }
    }
}
