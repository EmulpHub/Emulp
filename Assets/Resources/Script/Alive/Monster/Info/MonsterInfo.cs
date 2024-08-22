using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MonsterInfo : EntityInfo
{
    public Monster holder_monster { get => (Monster)holder; }

    public override void InitInfo(Entity holder)
    {
        base.InitInfo(holder);

        type = Type.monster;

        if (monster_type == MonsterType.vala) holder.EndOfGame = true;
    }

    public bool IsAnInvocation = false;

    public enum MonsterType { random, normal, funky, shark, archer, vala, inflamed, grassy, magnetic, junior }

    public MonsterType monster_type;

    public float str = 0;

    public virtual void AddPassiveEffect() { }

    public override void CalculateValue()
    {
        base.CalculateValue();

        str = Set_str();
    }

    public float CalculateDamage(float baseDamage)
    {
        float dmg = baseDamage * (1 + str / 100);

        dmg *= (float)holder.CollectStr(Effect.effectType.power) / 100 + 1;
        dmg *= 1 - (float)holder.CollectStr(Effect.effectType.reductionPower) / 100;

        dmg *= Ascension.GetAscensionParameter(Ascension.ModifierType.monster_damage) / 100;

        return Mathf.Clamp(dmg, 0, float.MaxValue);
    }

    public void AddSpellStats(SpellGestion.List sp, bool CheckDistance = false)
    {
        holder_monster.allAvailableStats.Add(sp, new SpellStats(holder_monster, sp, CheckDistance));
    }

    public virtual void SetAvailableSpell() { }

    public virtual string Title()
    {
        return "Normal";
    }

    public int Set_str()
    {
        float nb = 20 * (level - 1);

        nb += holder.CollectStr(Effect.effectType.mastery);

        nb *= (float)holder.CollectStr(Effect.effectType.masteryPercentage) / 100 + 1;

        return Mathf.RoundToInt(nb);
    }
}

