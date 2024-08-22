using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerInfo : EntityInfo
{
    public int damage, po, spike, effectForNonCc, armorEfficacity, HealEfficacity;
    public int armor = 0;
    public int str = 0, dex, res, eff;
    public float lifeSteal = 0;
    public int xp = 0;
    public int xp_max = 20;
    public int point_skill = 0;

    public float CalcStrStats(float value, float str = -1)
    {
        if (str == -1) str = this.str;

        return value * (1 + str / 100);
    }

    public float CalcResStats(float value, float res = -1)
    {
        if (res == -1) res = this.res;

        return value * (1 + res / 100);
    }

    public float CalcDexStats(float value, float dex = -1)
    {
        if (dex == -1) dex = this.dex;

        return value * (1 + dex / 100);
    }

    public float CalcEffStats(float value, int eff = -1)
    {
        if (eff == -1) eff = this.eff;

        return value + eff;
    }

    public override void CalculateValue()
    {
        base.CalculateValue();

        damage = SetDamage();

        lifeSteal = Set_lifeSteal();

        str = Set_str();
        dex = Set_dex();
        res = Set_Res();
        eff = Set_Eff();

        po = SetPo();

        effectForNonCc = SetNonCC_Efficacity();

        spike = Set_Spike();

        armorEfficacity = SetArmorEfficacity();
        HealEfficacity = SetHealEfficacity();

        xp_max = CalcXpMax(level);

        Spell.ResetAllSpellInfo();

        event_calculateValue.Call();
    }

    public void ResetExpStats()
    {
        level = 1;
        xp = 0;
        point_skill = 0;
        xp_max = CalcXpMax(1);
    }

    public override int Set_CC_add()
    {
        int nb = base.Set_CC_add();

        nb += Equipment_Management.GetEquipmentValue(SingleEquipment.value_type.cc);

        nb += statsForPlayer.GetInt(statsForPlayer.type.stats_cc_chance);

        return nb;
    }

    public override int Set_EC_Add()
    {
        int nb = base.Set_EC_Add();

        nb += Equipment_Management.GetEquipmentValue(SingleEquipment.value_type.ec); ;

        return nb;
    }

    public override void Set_EC_Multiply(ref float nb)
    {
        base.Set_EC_Multiply(ref nb);

        nb *= statsForPlayer.GetMultiplicator(statsForPlayer.type.effect_cc_multiplicator);
    }

    public int SetNonCC_Efficacity()
    {
        float nb = 100;

        nb *= statsForPlayer.GetMultiplicator(statsForPlayer.type.effect_nonCc_multiplicator);

        return Mathf.CeilToInt(nb);
    }

    public int Set_Spike()
    {
        float nb = 0;

        nb += Equipment_Management.GetEquipmentValue(SingleEquipment.value_type.special_spike);

        nb += holder.CollectStr(Effect.effectType.spike);

        nb *= (float)holder.CollectStr(Effect.effectType.spikePercentage) / 100 + 1;

        return Mathf.RoundToInt(nb);
    }

    public int SetPo()
    {
        int nb = 0;

        nb += Equipment_Management.GetEquipmentValue(SingleEquipment.value_type.po);

        nb += holder.CollectStr(Effect.effectType.po);

        nb += statsForPlayer.GetInt(statsForPlayer.type.stats_po);

        return nb;
    }

    public int SetDamage()
    {
        int nb = 0;

        nb += holder.CollectStr(Effect.effectType.damage);

        nb += holder.CollectStr(Effect.effectType.damage_oneUse);

        nb += holder.CollectStr(Effect.effectType.damage_multipleUse);

        nb += Equipment_Management.GetEquipmentValue(SingleEquipment.value_type.damage);

        return nb;
    }

    public int Set_lifeSteal()
    {
        float nb = 0;

        nb += Equipment_Management.GetEquipmentValue(SingleEquipment.value_type.lifeSteal);

        nb += statsForPlayer.Get(statsForPlayer.type.stats_lifeSteal);

        nb += holder.CollectStr(Effect.effectType.lifeSteal);

        return Mathf.RoundToInt(nb);
    }

    public int Set_str()
    {
        float nb = 0;

        nb += Equipment_Management.GetEquipmentValue(SingleEquipment.value_type.mastery);

        nb += primaryStatsSetter.GetStats(primaryStatsSetter.carac.str);

        nb += holder.CollectStr(Effect.effectType.strenght);

        nb += holder.CollectStr(Effect.effectType.mastery);

        nb *= (float)holder.CollectStr(Effect.effectType.masteryPercentage) / 100 + 1;

        return Mathf.RoundToInt(nb);
    }

    public int Set_dex()
    {
        float nb = 0;

        nb += primaryStatsSetter.GetStats(primaryStatsSetter.carac.dex);

        return Mathf.RoundToInt(nb);
    }

    public int Set_Res()
    {
        float nb = 0;

        nb += primaryStatsSetter.GetStats(primaryStatsSetter.carac.res);

        return Mathf.RoundToInt(nb);
    }

    public int Set_Eff()
    {
        float nb = 0;

        nb += primaryStatsSetter.GetStats(primaryStatsSetter.carac.eff);

        return Mathf.RoundToInt(nb);
    }

    public override float Set_LifeMax_add()
    {
        float nb = 100;

        nb += base.Set_LifeMax_add();

        nb += Equipment_Management.GetEquipmentValue(SingleEquipment.value_type.life);

        nb += statsForPlayer.Get(statsForPlayer.type.stats_life);

        nb += primaryStatsSetter.GetStats(primaryStatsSetter.carac.life);


        return nb;
    }

    public override int Set_PaMax_add()
    {
        int nb = base.Set_PaMax_add();

        nb += Equipment_Management.GetEquipmentValue(SingleEquipment.value_type.pa);

        nb += primaryStatsSetter.GetStats(primaryStatsSetter.carac.pa);

        nb += statsForPlayer.GetInt(statsForPlayer.type.stats_paMax);

        return nb;
    }

    public override void Set_PaMax_Multiply(ref float nb)
    {
        if (V.administrator && V.script_Scene_Main_Administrator is not null)
        {
            if (V.script_Scene_Main_Administrator.ForcePa != 0 && V.script_Scene_Main_Administrator.ForcePa != 6)
                nb = V.script_Scene_Main_Administrator.ForcePa;

            if (V.script_Scene_Main_Administrator.Infinite_PMPALEAK)
                nb = 999;
        }
    }

    public override int Set_PmMax_add()
    {
        int nb = base.Set_PmMax_add();

        nb += Equipment_Management.GetEquipmentValue(SingleEquipment.value_type.pm);
        nb += statsForPlayer.GetInt(statsForPlayer.type.stats_pmMax);
        nb += primaryStatsSetter.GetStats(primaryStatsSetter.carac.pm);

        return nb;
    }

    public override void Set_PmMax_Multiply(ref int nb)
    {
        if (V.administrator && V.script_Scene_Main_Administrator is not null)
        {
            if (V.script_Scene_Main_Administrator.ForcePm != 0 && V.script_Scene_Main_Administrator.ForcePm != 3)
                nb = V.script_Scene_Main_Administrator.ForcePm;

            if (V.script_Scene_Main_Administrator.Infinite_PMPALEAK)
                nb = 999;
        }
    }

    public override int Set_Tackle_add()
    {
        int nb = 12;

        nb += 6 * level;

        nb += Equipment_Management.GetEquipmentValue(SingleEquipment.value_type.tackle);

        nb += statsForPlayer.GetInt(statsForPlayer.type.stats_tackle);

        nb += base.Set_Tackle_add();

        return nb;
    }

    public override float Set_Leak_add()
    {
        float nb = 10;

        nb += 4 * level;

        nb += Equipment_Management.GetEquipmentValue(SingleEquipment.value_type.leak);

        nb += statsForPlayer.GetInt(statsForPlayer.type.stats_leak);

        nb += base.Set_Leak_add();

        return nb;
    }

    public override void Set_Leak_Multiply(ref float nb)
    {
        base.Set_Leak_Multiply(ref nb);


        if (V.administrator && V.script_Scene_Main_Administrator is not null)
        {
            if (V.script_Scene_Main_Administrator.Infinite_PMPALEAK)
                nb = 9999999;
        }
    }

    public int CalcXpMax(int lvl)
    {
        float value = (5 + 10 * (lvl - 1));

        return Mathf.Clamp(Mathf.RoundToInt(value), 5, 99999);
    }

    public int SetArmorEfficacity()
    {
        int nb = 0;

        nb += Equipment_Management.GetEquipmentValue(SingleEquipment.value_type.armor);

        nb += holder.CollectStr(Effect.effectType.armor);
        nb += holder.CollectStr(Effect.effectType.armor_oneUse);

        return nb;
    }

    public int SetHealEfficacity()
    {
        int nb = 0;

        nb += Equipment_Management.GetEquipmentValue(SingleEquipment.value_type.heal);

        nb += holder.CollectStr(Effect.effectType.heal);

        nb += holder.CollectStr(Effect.effectType.heal_OneUse);

        return nb;
    }
}
