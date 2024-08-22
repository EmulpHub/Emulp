using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Effect : MonoBehaviour
{
    public enum effectType
    {
        power, boost_pa, resistance, mastery, maximumLife, tackle, masteryPercentage, maximumLifePercentage, tacklePercentage, leakPercentage, boost_pm,
        custom, Warrior_Power, Warrior_Resistance, reducePaOnSpecifiedSpell, damage, po, custom_magic, heal, bleeding, customTxt, focus, effectPercentage, additionalSpellArea,
        effectPercentage_OneUse, heal_OneUse, armor, armor_oneUse, spikePercentage, spike, reductionPower, damage_oneUse, criticalHitChance, criticalHitChance_oneUse, ec, ec_oneUse,
        untacklable, Warrior_conflagration, Warrior_fatigue, spikeAdditionalDamage, damage_multipleUse, lifeSteal, Spellx2_oneUse, warrior_reducePowerReduction,
        strenght, pv, spikeShard, accumulation,additionalSpellCast
    }

    public bool unique;

    public effectType type;

    public Sprite img;

    public Entity holder;

    public int id;

    public string CodeName;
}
