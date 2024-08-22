using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_inflamed_attack : spellEffect_Monster
{
    public override void InfoBool()
    {
        SetBoolInfo(true, true);
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            SoundManager.PlaySound(SoundManager.list.spell_monster_inflamed_attack);

            StartCoroutine(spellHolder.Anim_Flame(V.inflamed_attack_sprite, targetedSquareWorldVector2 + new Vector2(0, 0.5f), 4, 1.5f));

            caster.Animation_DealDamage(targetedSquareWorldVector2);

            target.Damage(new InfoDamage(calcDamage(4), caster));
        }

        yield return null;
    }
}