using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_normal_attack : spellEffect_Monster
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

            StartCoroutine(spellHolder.Anim_PopDown(V.damageExplosion, V.CalcEntityDistanceToBody(target)));

            SoundManager.PlaySound(SoundManager.list.spell_monster_normal_punch);

            target.Damage(new InfoDamage(calcDamage(Random.Range(6, 8)), caster));

            yield return new WaitForSeconds(0.3f);
        }

        yield return null;
    }
}