using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class base_fist : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(true, true);
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {

            target.Damage(new InfoDamage(calcDamage(5), caster));

            Vector2 posToAttack = F.GetPosNearTarget(target, CursorInfo.Instance.positionInWorld);

            spellAnimation.anim_simple("punch", posToAttack);

            SoundManager.PlaySound(SoundManager.list.spell2_basic_fist);
            SoundManager.PlaySound(SoundManager.list.spell_basic_punch);

        }

        yield return null;
    }
}