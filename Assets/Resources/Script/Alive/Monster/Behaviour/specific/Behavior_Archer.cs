using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster : Entity
{
    /// <summary>
    /// The behavior when on combat for a funky monster
    /// </summary>
    /// <returns></returns>
    public IEnumerator Behavior_Archer()
    {
        if (ShouldStopAndSetCommonValue()) yield break;

        SpellStats archer_attack = GetSpellStats(SpellGestion.List.monster_archer_attack);
        SpellStats sprint_action = GetSpellStats(SpellGestion.List.monster_archer_sprint);

        Entity target = DecideWhoToAttack();
        float distanceWithTarget = F.DistanceBetweenTwoPos(target, this);

        if (archer_attack.Launchable_cooldownAndPa())
        {
            bool WithLineOfView = F.IsLineOfView(target.CurrentPosition_string, CurrentPosition_string);

            if (ableToMove)
            {
                if (distanceWithTarget < archer_attack.range_min && WithLineOfView)
                {
                    CastSprintSpellIfPossible(sprint_action, target);

                    StartCoroutine(MoveAwayFromEntity(target, archer_attack.range_max, true));

                    yield break;
                }
                else if (distanceWithTarget > archer_attack.range_max || !WithLineOfView)
                {
                    CastSprintSpellIfPossible(sprint_action, target);

                    StartCoroutine(MoveTowardEntity(target, true, archer_attack.range_min, archer_attack.range_max));

                    yield break;
                }
            }
            if ((distanceWithTarget >= archer_attack.range_min && distanceWithTarget <= archer_attack.range_max) && WithLineOfView)
            {
                StartCoroutine(CastASpell(archer_attack, target));

                yield break;
            }
        }
        else if (ableToMove)
        {
            CastSprintSpellIfPossible(sprint_action, target);

            StartCoroutine(MoveAwayFromEntity(target));

            yield break;
        }

        Action_nextTurn.Add(this);

        yield return new WaitForSeconds(0);
    }

    void CastSprintSpellIfPossible(SpellStats sprint_action, Entity target)
    {
        if (canMoveAndHavePm && sprint_action.isLaunchable_onlyPAAndCooldown)
        {
            StartCoroutine(CastASpell(sprint_action, target));

            StopCoroutine(currentCoroutine_name);
        }
    }

}
