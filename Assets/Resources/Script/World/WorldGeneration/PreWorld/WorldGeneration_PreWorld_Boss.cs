using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LayerWorldGeneration
{
    partial class WorldGeneration : MonoBehaviour
    {
        static void AddBoss()
        {
            string lastGeneratedPos = preWorld.Last();

            Create_fight_Boss(lastGeneratedPos);

            preWorld.Remove(lastGeneratedPos);
        }
    }
}