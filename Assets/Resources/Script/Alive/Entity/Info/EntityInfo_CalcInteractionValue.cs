using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EntityInfo : MonoBehaviour
{
    public virtual float CalcDamage(float dmg)
    {
        dmg *= 1 + (float)holder.CollectStr(Effect.effectType.power) / 100;
        dmg *= 1 - (float)holder.CollectStr(Effect.effectType.reductionPower) / 100;

        return Mathf.Clamp(dmg, 0, 999999);
    }

    public virtual float CalculateResistance(float dmg)
    {
        dmg *= 1 - (float)holder.CollectStr(Effect.effectType.resistance) / 100;

        return dmg;
    }

    public virtual float CalcHeal(float heal)
    {
        return heal;
    }
}
