using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster : Entity
{
    public IEnumerator Behavior_magnetic()
    {
        if (ShouldStopAndSetCommonValue()) yield break;

        SpellStats attack = GetSpellStats(SpellGestion.List.monster_magnetic_attack);
        SpellStats attract = GetSpellStats(SpellGestion.List.monster_magnetic_attract);

        Entity target = DecideWhoToAttack();

        float distanceWithTarget = F.DistanceBetweenTwoPos(target, this);

        if (attack.isLaunchable || attract.isLaunchable)
        {
            bool WithLineOfView = F.IsLineOfView(target.CurrentPosition_string, CurrentPosition_string);

            int attractRange = 4;

            bool InLine = F.IsInLine(target.CurrentPosition_string, CurrentPosition_string);

            if (distanceWithTarget == 1 && attack.isLaunchable)
            {
                StartCoroutine(CastASpell(attack, target));

                yield break;
            }
            else if (attract.isLaunchable && distanceWithTarget <= attractRange && WithLineOfView && InLine && !canMoveAndHavePm)
            {
                StartCoroutine(CastASpell(attract, target));

                yield break;
            }
            else if (canMoveAndHavePm && distanceWithTarget > 1)
            {
                if (attract.isLaunchable)
                {
                    StartCoroutine(MoveTowardEntity(target, true, 1, true));

                    yield break;

                }
                else
                {
                    StartCoroutine(MoveTowardEntity(target, false, 1, false));

                    yield break;
                }

            }
        }
        else if (canMoveAndHavePm && distanceWithTarget > 1)
        {
            StartCoroutine(MoveTowardEntity(target, true, 1, true));

            yield break;
        }

        Action_nextTurn.Add(this);

        yield return new WaitForSeconds(0);
    }
}
