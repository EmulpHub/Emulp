using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EntityInfo : MonoBehaviour
{
    private void CalculateValueEntity(Entity e)
    {
        CalculateValue();
    }

    public virtual void CalculateValue()
    {
        Life_max = Set_LifeMax();

        PA_max = Set_PaMax();
        PM_max = Set_PmMax();

        tackle = Set_Tackle();
        leak = Set_Leak();

        CalcTackle();

        cc = Set_CC();
        ec = Set_EC();

        if (V.game_state != V.State.fight)
            ResetAllStats();
    }

    public int LifeMaxAddDebug;

    public float Set_LifeMax()
    {
        float nb = Set_LifeMax_add();

        Set_LifeMax_Multiply(ref nb);

        nb += LifeMaxAddDebug;

        return nb;
    }

    public virtual float Set_LifeMax_add()
    {
        float nb = 0;

        nb += holder.CollectStr(Effect.effectType.maximumLife);

        nb += holder.CollectStr(Effect.effectType.pv);

        return nb;
    }

    public virtual void Set_LifeMax_Multiply(ref float nb)
    {
        nb *= ((float)holder.CollectStr(Effect.effectType.maximumLifePercentage) / 100 + 1);
    }

    public int Set_PaMax()
    {
        float nb = Set_PaMax_add();

        Set_PaMax_Multiply(ref nb);

        return Mathf.CeilToInt(nb);
    }

    public virtual int Set_PaMax_add()
    {
        int nb = 6;

        nb += holder.CollectStr(Effect.effectType.boost_pa);

        return nb;
    }

    public virtual void Set_PaMax_Multiply(ref float nb)
    {
    }

    public int Set_PmMax()
    {
        int nb = Set_PmMax_add();

        Set_PmMax_Multiply(ref nb);

        return nb;
    }

    public virtual int Set_PmMax_add()
    {
        int nb = 3;

        nb += holder.CollectStr(Effect.effectType.boost_pm);

        return nb;
    }

    public virtual void Set_PmMax_Multiply(ref int nb) { }

    public int Set_Tackle()
    {
        float nb = Set_Tackle_add();

        Set_Tackle_Multiply(ref nb);

        return Mathf.CeilToInt(nb);
    }

    public virtual int Set_Tackle_add()
    {
        int nb = 0;

        return nb;
    }

    public virtual void Set_Tackle_Multiply(ref float nb)
    {
        nb *= (float)holder.CollectStr(Effect.effectType.tacklePercentage) / 100 + 1;
    }

    public int Set_Leak()
    {
        float nb = Set_Leak_add();

        Set_Leak_Multiply(ref nb);

        return Mathf.CeilToInt(nb);
    }

    public virtual float Set_Leak_add()
    {
        float nb = 0;

        return nb;
    }

    public virtual void Set_Leak_Multiply(ref float nb)
    {
        nb *= (float)holder.CollectStr(Effect.effectType.leakPercentage) / 100 + 1;
    }


    public int Set_CC()
    {
        float nb = Set_CC_add();

        Set_CC_Multiply(ref nb);

        return Mathf.CeilToInt(nb);
    }

    public virtual int Set_CC_add()
    {
        int nb = 5;

        nb += holder.CollectStr(Effect.effectType.criticalHitChance);

        nb += holder.CollectStr(Effect.effectType.criticalHitChance_oneUse);

        return nb;
    }

    public virtual void Set_CC_Multiply(ref float nb)
    {
    }

    public int Set_EC()
    {
        float nb = Set_EC_Add();

        Set_EC_Multiply(ref nb);

        return Mathf.CeilToInt(nb);
    }

    public virtual int Set_EC_Add()
    {
        float nb = 50;

        int add = Set_CC() - 100;

        if (add > 0)
            nb += add;

        nb += holder.CollectStr(Effect.effectType.ec);

        nb += holder.CollectStr(Effect.effectType.ec_oneUse);

        return Mathf.CeilToInt(nb);
    }

    public virtual void Set_EC_Multiply(ref float nb)
    {
    }
}
