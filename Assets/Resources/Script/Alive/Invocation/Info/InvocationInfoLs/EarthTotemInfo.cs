using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthTotemInfo : InvocationInfo
{
    public int basePvMax;
    public float baseDamage;

    public override float Set_LifeMax_add()
    {
        int nb = basePvMax;

        return base.Set_LifeMax_add() + nb;
    }
}
