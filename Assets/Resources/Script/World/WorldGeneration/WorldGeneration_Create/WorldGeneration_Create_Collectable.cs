using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LayerMap;

namespace LayerWorldGeneration
{
    public partial class WorldGeneration : MonoBehaviour
    {
        public static IMap Create_collectable_equipment(string pos)
        {
            int nbChest = 2;

            if (WorldData.distance == 2) nbChest++;

            int levelChest = 4 + WorldData.distance * 2;

            MapInfo_collectable_equipment mapInfo = new MapInfo_collectable_equipment(pos, levelChest, nbChest, Collectable_Chest.GainType.All);

            WorldData.AddInWorld(mapInfo);

            return mapInfo;
        }

        public static IMap Create_collectable_talent(string pos)
        {
            int nbTalent = 1;

            if (WorldData.distance == 2) nbTalent++;

            MapInfo_collectable_talent mapInfo = new MapInfo_collectable_talent(pos,nbTalent);

            WorldData.AddInWorld(mapInfo);

            return mapInfo;
        }
    }
}