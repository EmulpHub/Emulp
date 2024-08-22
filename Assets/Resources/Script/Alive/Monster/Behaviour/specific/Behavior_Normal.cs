using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster : Entity
{
    public IEnumerator Behavior_normal()
    {
        if (ShouldStopAndSetCommonValue()) yield break;

        SpellStats meleeAttack = GetSpellStats(SpellGestion.List.monster_normal_attack);

        Entity target = DecideWhoToAttack();
        float distanceWithTarget = F.DistanceBetweenTwoPos(target, this);

        if (meleeAttack.Launchable(target))
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
    }
}
