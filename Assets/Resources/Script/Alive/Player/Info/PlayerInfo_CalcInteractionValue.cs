using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerInfo : EntityInfo
{
    public override float CalcDamage(float dmg)
    {
        float nb = CalcStrStats(dmg);

        nb += damage;

        nb = base.CalcDamage(nb);

        nb *= statsForPlayer.GetMultiplicator(statsForPlayer.type.allDamage_multiplicator);

        return nb;
    }

    public float CalculateSpikeDamage()
    {
        float val = 10;

        val += holder.CollectStr(Effect.effectType.spikeAdditionalDamage);

        val += (float)damage / 5;

        val *= 1 + (float)holder.CollectStr(Effect.effectType.power) / 100;
        val *= 1 - (float)holder.CollectStr(Effect.effectType.reductionPower) / 100;

        return val;
    }

    public override float CalcHeal(float heal)
    {
        float nb = base.CalcHeal(CalcResStats(heal));

        nb *= HealEfficacity / 100 + 1;

        return nb;

    }
}
