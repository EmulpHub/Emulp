using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_vala_attack : spellEffect_Monster
{

    public override void InfoBool()
    {
        SetBoolInfo(true, true);
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            StartCoroutine(spellHolder.Anim_Flame(V.vala_Attack_sprite, V.CalcEntityDistanceToBody(target), 4, 2.3f));

            caster.Animation_DealDamage(targetedSquareWorldVector2);

            SoundManager.PlaySound(SoundManager.list.spell_monster_vala_attack);

            int nb = calc(Random.Range(8, 10));

            target.Damage(new InfoDamage(calcDamage(nb), caster));
        }

        yield return null;
    }
}


public class monster_vala_fireThrowing : spellEffect_Monster
{
    public override void InfoBool()
    {
        SetBoolInfo(true, true);
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            float speed = F.Projectile_CalculateTravelTime(caster, target);

            //If it's a monster make animation like he receive damage
            caster.Animation_DealDamage(targetedSquareWorldVector2);

            Sprite FireBall = Resources.Load<Sprite>("Image/Monster/Spell/Vala_fireball");

            StartCoroutine(spellHolder.Anim_Projectile(caster, FireBall, V.CalcEntityDistanceToBody(target), false, speed, 1.3f));

            SoundManager.PlaySound(SoundManager.list.spell_monster_vala_fireThrowing_travelling);

            yield return new WaitForSeconds(speed);

            SoundManager.PlaySound(SoundManager.list.spell_monster_archer_arrow_impact);

            target.Damage(new InfoDamage(calcDamage(13), caster));
        }

        yield return null;
    }
}


public class monster_vala_invokeInflamed : spellEffect_Monster
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        caster.Animation_DealDamage(targetedSquareWorldVector2);

        int nb = calc(1);

        while (nb > 0)
        {
            nb--;
            Invocating.InvokeMonster(new CreaterInfoMonster(pos, caster.Info.level, false, MonsterInfo.MonsterType.inflamed), caster_monster);
        }

        yield return null;
    }
}