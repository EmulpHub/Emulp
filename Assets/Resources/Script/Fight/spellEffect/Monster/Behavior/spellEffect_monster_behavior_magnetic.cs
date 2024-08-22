using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_magnetic_attack : spellEffect_Monster
{
    public override void InfoBool()
    {
        SetBoolInfo(true, true);
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            caster.Animation_DealDamage(targetedSquareWorldVector2);

            spellHolder.CreateParticle_Impact_Entering_Static_Img(V.CalcEntityDistanceToBody(target), V.magnetic_nail.texture, 0.8f, Spell.Particle_Amount._5, -60);

            SoundManager.PlaySound(SoundManager.list.monster_magnetic_attack);

            yield return new WaitForSeconds(0.3f);

            target.Damage(new InfoDamage(calcDamage(Random.Range(7, 9)), caster));
        }

        yield return null;
    }
}

public class monster_magnetic_attract : spellEffect_Monster
{
    public override void InfoBool()
    {
        SetBoolInfo(true, true);
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            caster.Animation_DealDamage(targetedSquareWorldVector2);

            StartCoroutine(spellHolder.Anim_Menacing(caster, V.magnetic_magnet, V.CalcEntityDistanceToBody(target), false, 0.3f, 0.8f));

            spellHolder.CreateParticle_Impact(V.CalcEntityDistanceToBody(target), 1.2f, Spell.Particle_Amount._20, Spell.Particle_Color.impact);

            SoundManager.PlaySound(SoundManager.list.monster_magnetic_attract);

            yield return new WaitForSeconds(0.4f);

            target.Damage(new InfoDamage(calcDamage(Random.Range(4, 6)), caster));

            target.Attract(calc(1), caster);
        }

        yield return null;
    }
}