using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LayerWorldGeneration
{
    partial class WorldGeneration : MonoBehaviour
    {
        static int nbConnector (string pos)
        {
            int nbConnector = 0;

            List<string> neighbour = new()
            {
                F.AdditionPos(pos, "0_1"),
                F.AdditionPos(pos, "0_-1"),
                F.AdditionPos(pos, "1_0"),
                F.AdditionPos(pos, "-1_0"),
            };

            foreach (string nearPos in neighbour)
            {
                if (preWorld.Contains(nearPos)) nbConnector++;
            }

            return nbConnector;
        }

        static bool validForHole(string pos)
        {
            int nbConnectorPos = nbConnector(pos);

            if (nbConnectorPos < 2) return false;

            List<string> neighbour = new()
            {
                F.AdditionPos(pos, "0_1"),
                F.AdditionPos(pos, "0_-1"),
                F.AdditionPos(pos, "1_0"),
                F.AdditionPos(pos, "-1_0"),
            };

            foreach(string neighbourPos in neighbour)
            {
                if (nbConnector(neighbourPos) < 2) return false;
            }

            return true;
        }

        static string GetHolePosition()
        {
            List<string> validHole = new ();

            foreach (string pos in preWorld)
            {
                if (validForHole(pos)) validHole.Add(pos);
            }

            if (validHole.Count == 0) return "";

            return validHole[Random.Range(0, validHole.Count)];
        }

        static void AddHole(int nb)
        {
            for (int i = 0; i < nb; i++)
            {
                string pos = GetHolePosition();

                if (pos == "") return;

                preWorld.Remove(pos);
            }
        }
    }
}