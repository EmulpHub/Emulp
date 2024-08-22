using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster : Entity
{
    /// <summary>
    /// The behavior when on combat for a normal monster
    /// </summary>
    /// <returns></returns>
    public IEnumerator Behavior_grassy()
    {
        if (ShouldStopAndSetCommonValue()) yield break;

        if (CheckRevengeEffect(this, 50, 2))
        {
            StartCoroutine(Wait(0.5f));

            yield break;
        }

        Entity target = DecideWhoToAttack();
        float distanceWithTarget = F.DistanceBetweenTwoPos(target, this);

        SpellStats boost = GetSpellStats(SpellGestion.List.monster_grassy_boost);
        SpellStats attack = GetSpellStats(SpellGestion.List.monster_grassy_attack);

        bool launchableboost = boost.Launchable(this);

        (bool find, Entity closestAllies) v = ChooseClosestAllies(this, delegate (Entity target, Entity origin)
        {
            return launchableboost || target.Info.Life < target.Info.Life_max;
        }
        );

        if (v.find && launchableboost)
        {
            int distanceBetweenClosestAllies = F.DistanceBetweenTwoPos(v.closestAllies, this);

            if (distanceBetweenClosestAllies > 1 && canMoveAndHavePm)
            {
                StartCoroutine(MoveTowardEntity(v.closestAllies, true, 1));

                yield break;
            }
            else if (distanceBetweenClosestAllies <= 1)
            {
                StartCoroutine(CastASpell(boost, v.closestAllies));

                yield break;
            }
        }
        else if (attack.isLaunchable)
        {
            if (distanceWithTarget == 1)
            {
                StartCoroutine(CastASpell(attack, target));

                yield break;
            }
            else if (canMoveAndHavePm && distanceWithTarget > 1)
            {
                StartCoroutine(MoveTowardEntity(target, false, 1));

                yield break;
            }
        }
        else if (canMoveAndHavePm)
        {
            if (ContainEffect_byTitle(Effect.GetRevengeName()))
            {
                if (distanceWithTarget > 1)
                {
                    StartCoroutine(MoveTowardEntity(target, false, 1));

                    yield break;
                }

            }
            else
            {
                StartCoroutine(MoveAwayFromEntity(target));

                yield break;
            }
        }

        Action_nextTurn.Add(this);

        yield return new WaitForSeconds(0);
    }
}
