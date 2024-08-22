using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LayerMap;

namespace LayerWorldGeneration
{
    public partial class WorldGeneration : MonoBehaviour
    {
        public static IMap Create_fight_Boss(string pos)
        {
            MonsterInMap listMonster = new(new MonsterListInfo(3 + WorldData.distance * 5, MonsterInfo.MonsterType.vala));

            MapInfo_fight_boss mapInfo = new(pos, listMonster);

            WorldData.AddInWorld(mapInfo);

            return mapInfo;
        }

        public static IMap Create_fight_normal(string pos)
        {
            int nbMonster = Random.Range(1, 2 + 1);

            if (WorldData.distance == 2)
            {
                nbMonster++;
            }

            int availablePoint = Random.Range(0, 2 + 1) + WorldData.distance;

            int lvl = 2 + WorldData.distance * 3;

            if (WorldData.distance == 2)
            {
                Mathf.Clamp(nbMonster, 2, 99);
                Mathf.Clamp(availablePoint, 3, 99);
            }

            MonsterInMap listMonster = MonsterInMap.CreateRandomListMonster(lvl, lvl + 1, nbMonster, availablePoint);

            MapInfo_fight_normal mapInfo = new MapInfo_fight_normal(pos, listMonster);

            WorldData.AddInWorld(mapInfo);

            return mapInfo;
        }
    }
}