using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster : Entity
{
    /// <summary>
    /// The behavior when on combat for a normal monster
    /// </summary>
    /// <returns></returns>
    public IEnumerator Behavior_Inflamed()
    {
        if (ShouldStopAndSetCommonValue()) yield break;

        SpellStats meleeAttack = GetSpellStats(SpellGestion.List.monster_inflamed_attack);

        Entity target = DecideWhoToAttack();
        float distanceWithTarget = F.DistanceBetweenTwoPos(target, this);

        if (meleeAttack.isLaunchable)
        {
            if (distanceWithTarget == 1)
            {
                StartCoroutine(CastASpell(meleeAttack, target));

                yield break;
            }
            else if (canMoveAndHavePm && distanceWithTarget > 1)
            {
                StartCoroutine(MoveTowardEntity(target, false, 1));

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
