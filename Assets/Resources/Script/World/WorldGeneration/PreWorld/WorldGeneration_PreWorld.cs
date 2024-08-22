using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LayerWorldGeneration
{
    partial class WorldGeneration : MonoBehaviour
    {
        static void AddPreWorldAtPos(string posToAdd, List<string> createdCase, List<string> possibleCase)
        {
            if (posToAdd == "0_0") return;

            if (!possibleCase.Contains(posToAdd) && !createdCase.Contains(posToAdd))
                possibleCase.Add(posToAdd);
        }

        static void AddPreWorld(string pos, List<string> createdCase, List<string> possibleCase)
        {
            AddPreWorldAtPos(F.AdditionPos(pos, "0_1"), createdCase, possibleCase);
            AddPreWorldAtPos(F.AdditionPos(pos, "0_-1"), createdCase, possibleCase);
            AddPreWorldAtPos(F.AdditionPos(pos, "1_0"), createdCase, possibleCase);
            AddPreWorldAtPos(F.AdditionPos(pos, "-1_0"), createdCase, possibleCase);
        }

        static List<string> CreatePreWorld(int nbMap)
        {
            List<string> possibleCase = new() { "1_0", "-1_0", "0_1", "0_-1" };

            List<string> createdCase = new();

            for (int i = 0; i < nbMap; i++)
            {
                string pos = possibleCase[Random.Range(0, possibleCase.Count)];

                AddPreWorld(pos, createdCase, possibleCase);

                createdCase.Add(pos);

                possibleCase.Remove(pos);
            }

            return createdCase;
        }
    }
}