using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using LayerMap;
using MonsterRandom;
using System.Linq;

namespace LayerWorldGeneration
{
    partial class WorldGeneration : MonoBehaviour
    {
        static List<string> preWorld;

        public static void GenerateRandomWorld()
        {
            WorldData.ClearWorld();

            Create_empty("0_0");

            if (WorldData.distance == 0) distance_0();
            else if (WorldData.distance == 1) distance_1();
            else if (WorldData.distance == 2) distance_2();

            if (preWorld.Count > 0) throw new System.Exception("Setting for random map is not ");
        }

        static void distance_0()
        {
            SetMapPosition(0, 1, 2);
        }

        static void distance_1()
        {
            SetMapPosition(1, 2, 3);
        }

        static void distance_2()
        {
            SetMapPosition(2, 2, 4);
        }

        static void SetMapPosition(int nbTalent, int nbEquipment, int nbMonster)
        {
            preWorld = CreatePreWorld(nbTalent + nbEquipment + nbMonster + 1); //0_0 do not count

            AddBoss();

            AddMonster_Around0_0(ref nbMonster);

            AddTalent(nbTalent);

            AddEquipment(nbEquipment);

            AddMonster(nbMonster);
        }

        static void AddMonster_Around0_0(ref int nbMonster)
        {
            List<string> neighbour = new List<string>()
            {
                "1_0","-1_0","0_1","0_-1"
            };

            foreach (string pos in neighbour)
            {
                if (nbMonster <= 0)
                    return;

                if (preWorld.Contains(pos))
                {
                    Create_fight_normal(pos);

                    nbMonster--;

                    preWorld.Remove(pos);
                }
            }
        }
    }
}
