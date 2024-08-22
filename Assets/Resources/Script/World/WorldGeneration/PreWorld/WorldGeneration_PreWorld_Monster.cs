using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LayerWorldGeneration
{
    partial class WorldGeneration : MonoBehaviour
    {
        static void AddMonster(int nb)
        {
            for (int i = 0; i < nb; i++)
            {
                if (preWorld.Count == 0)
                    throw new System.Exception("BUG addMonsterPreworld");

                string pos = preWorld[0];

                preWorld.Remove(pos);

                Create_fight_normal(pos);
            }
        }
    }
}