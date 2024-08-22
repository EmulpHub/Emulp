using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LayerMap;

namespace LayerWorldGeneration
{
    public partial class WorldGeneration : MonoBehaviour
    {
        public static MapInfo Create_empty(string pos)
        {
            MapInfo mapInfo = new MapInfo(pos);

            WorldData.AddInWorld(mapInfo);

            return mapInfo;
        }
    }
}