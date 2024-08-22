using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public partial class Character : MonoBehaviour
{
    public static PlayerInfo playerInfo;

    public static void CalcValueOfEveryone()
    {
        foreach (Entity e in new List<Entity>(AliveEntity.list))
        {
            e.Info.CalculateValue();
        }
    }
}
