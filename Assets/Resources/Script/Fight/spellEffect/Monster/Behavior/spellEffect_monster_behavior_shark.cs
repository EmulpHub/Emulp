using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_shark_attack : spellEffect_Monster
{
    public override void InfoBool()
    {
        SetBoolInfo(true, true);
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            StartCoroutine(spellHolder.Anim_Bite(holderSprite, V.CalcEntityDistanceToBody(target), 1, 2));

            caster_monster.Animation_DealDamage(targetedSquareWorldVector2);

            SoundManager.PlaySound(SoundManager.list.spell_monster_shark_bite);

            yield return new WaitForSeconds(0.35f);

            StartCoroutine(spellHolder.Anim_PopDown(V.damageExplosion, V.CalcEntityDistanceToBody(target)));

            target.Damage(new InfoDamage(calcDamage(Random.Range(8, 10)), caster));
        }

        yield return null;
    }
}


public class monster_shark_heal : spellEffect_Monster
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }

    public override IEnumerator Effect_before()
    {
        caster.Heal(new InfoHeal(calc(caster.Info.Life_max * 0.35f)));

        yield return null;
    }
}


public class monster_teleport_shark : spellEffect_Monster
{

    public override void InfoBool()
    {
        SetBoolInfo(false, false);
    }

    public override IEnumerator Effect_before()
    {
        string nextMovePos = PathFinding.ClosestTileToTp(caster.CurrentPosition_string, info.target.CurrentPosition_string, spellHolder.range_max);

        if (nextMovePos != "999_999")
        {

            SoundManager.PlaySound(SoundManager.list.spell_monster_shark_tp);

            caster.Animation_StartTeleport(0.5f);
            yield return new WaitForSeconds(0.6f);
            caster.Animation_EndTeleport(0.3f);

            SoundManager.PlaySound(SoundManager.list.spell_monster_shark_tp);

            F.TeleportEntity(nextMovePos, caster);

            info.targetedSquare = nextMovePos;

            yield return null;
        }
    }
}
