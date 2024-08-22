using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_funky_attack : spellEffect_Monster
{

    public override void InfoBool()
    {
        SetBoolInfo(true, true);
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        Sprite stick = Resources.Load<Sprite>("Image/Monster/Spell/stick");

        if (TargetForDamage(target))
        {
            float speed = F.Projectile_CalculateTravelTime(caster, target);

            caster.Animation_DealDamage(targetedSquareWorldVector2);

            StartCoroutine(spellHolder.Anim_Projectile(caster, stick, V.CalcEntityDistanceToBody(target), true, speed));

            yield return new WaitForSeconds(speed);

            SoundManager.PlaySound(SoundManager.list.spell_monster_funky_wood);

            int nb = calc(Random.Range(7, 9));

            target.Damage(new InfoDamage(calcDamage(nb), caster));
        }

        yield return null;
    }
}

public class monster_funky_recoil : spellEffect_Monster
{

    public override void InfoBool()
    {
        SetBoolInfo(true, true);
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            StartCoroutine(spellHolder.Anim_PopDown(V.damageExplosion, V.CalcEntityDistanceToBody(target)));

            caster.Animation_DealDamage(targetedSquareWorldVector2);

            int nb = calc(Random.Range(4, 6));

            target.Damage(new InfoDamage(calcDamage(nb), caster));

            SoundManager.PlaySound(SoundManager.list.spell_monster_normal_punch);

            yield return new WaitForSeconds(0.35f);

            SoundManager.PlaySound(SoundManager.list.spell_monster_funky_escape);

            info.targetedSquare = caster.Push(calc(1), target);

            if (caster.CurrentPosition_string != targetedSquare)
            {
                //StartCoroutine(spellHolder.CreateAnimation_AttackRecoil(caster.CurrentPosition_string, targetedSquare));
            }
        }

        yield return null;
    }
}