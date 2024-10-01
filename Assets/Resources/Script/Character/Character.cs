using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public partial class Character : MonoBehaviour
{
    public static PlayerInfo playerInfo;

    public static void CalcValueOfEveryone()
    {
        void Traveler(Entity entity)
        {
            entity.Info.CalculateValue();
        }

        AliveEntity.Instance.TravelEntity(Traveler);
    }
}
