using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellEffect_Monster : spellEffect
{
    public MonsterInfo monsterInfo { get => caster_monster.monsterInfo; }

    public override bool TargetForBonus(Entity target)
    {
        return base.TargetForBonus(target) && target.IsMonster();
    }

    public override bool TargetForDamage(Entity target)
    {
        return base.TargetForDamage(target) && !target.IsMonster();
    }

    public override float calcDamage(float value)
    {
        return base.calcDamage(monsterInfo.CalcDamage(value));
    }
}
