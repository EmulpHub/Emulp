using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster : Entity
{
    public IEnumerator Behavior_shark()
    {
        if (ShouldStopAndSetCommonValue()) yield break;

        SpellStats meleeAttack = GetSpellStats(SpellGestion.List.monster_shark_attack);
        SpellStats tp = GetSpellStats(SpellGestion.List.monster_teleport_shark);

        Entity target = DecideWhoToAttack();
        float distanceWithTarget = F.DistanceBetweenTwoPos(target, this);

        bool launchableSpell = meleeAttack.isLaunchable || tp.isLaunchable;

        if (launchableSpell)
        {
            if (distanceWithTarget == 1 && meleeAttack.isLaunchable)
            {
                StartCoroutine(CastASpell(meleeAttack, target));

                yield break;
            }
            else if (canMoveAndHavePm && distanceWithTarget > 1)
            {
                StartCoroutine(MoveTowardEntity(target, false, 1));

                yield break;
            }
            else if (tp.isLaunchable)
            {
                StartCoroutine(CastASpell(tp, target));

                yield break;
            }
        }
        else if (canMoveAndHavePm && distanceWithTarget > 1)
        {

            StartCoroutine(MoveTowardEntity(target, false, 1));

            yield break;
        }

        Action_nextTurn.Add(this);

        yield return new WaitForSeconds(0);
    }
}
