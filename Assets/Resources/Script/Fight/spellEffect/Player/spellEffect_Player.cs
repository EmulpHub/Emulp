using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellEffect_Player : spellEffect
{
    public override float calcDamage(float value)
    {
        return base.calcDamage(V.player_info.CalcDamage(value));
    }
}
