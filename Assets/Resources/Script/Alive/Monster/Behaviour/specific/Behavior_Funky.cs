using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster : Entity
{
    /// <summary>
    /// The behavior when on combat for a funky monster
    /// </summary>
    /// <returns></returns>
    public IEnumerator Behavior_funky()
    {
        if (ShouldStopAndSetCommonValue()) yield break;

        SpellStats meleeAttack_recol = GetSpellStats(SpellGestion.List.monster_funky_recoil);
        SpellStats distanceAttack = GetSpellStats(SpellGestion.List.monster_funky_attack);

        Entity target = DecideWhoToAttack();
        float distanceWithTarget = F.DistanceBetweenTwoPos(target, this);

        bool ASpellCanBelaunched = meleeAttack_recol.isLaunchable || distanceAttack.isLaunchable;

        if (ASpellCanBelaunched)
        {
            bool LineOfView = F.IsLineOfView(CurrentPosition_string, target.CurrentPosition_string);

            if (distanceWithTarget == 1 && meleeAttack_recol.isLaunchable)
            {
                StartCoroutine(CastASpell(meleeAttack_recol, target));

                yield break;
            }
            else if (canMoveAndHavePm && distanceWithTarget == 1)
            {
                StartCoroutine(MoveAwayFromEntity(target));

                yield break;
            }
            else if (distanceWithTarget <= 3 && LineOfView)
            {

                StartCoroutine(CastASpell(distanceAttack, target));

                yield break;
            }
            else if (canMoveAndHavePm && (distanceWithTarget > 3 || !LineOfView))
            {
                StartCoroutine(MoveTowardEntity(target, true, 3));

                yield break;
            }
        }
        else if (canMoveAndHavePm)
        {

            StartCoroutine(MoveAwayFromEntity(target));

            yield break;
        }

        Action_nextTurn.Add(this);

        yield return new WaitForSeconds(0);
    }
}
