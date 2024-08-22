using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LayerWorldGeneration
{
    partial class WorldGeneration : MonoBehaviour
    {
        static void AddEquipment(int nb)
        {
            for (int i = 0; i < nb; i++)
            {
                string pos = preWorld[Random.Range(0, preWorld.Count)];

                preWorld.Remove(pos);

                Create_collectable_equipment(pos);
            }
        }
    }
}