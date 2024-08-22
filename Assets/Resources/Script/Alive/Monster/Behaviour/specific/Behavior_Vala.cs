using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFindingName;

public partial class Monster : Entity
{
    public IEnumerator Behavior_Vala()
    {
        if (ShouldStopAndSetCommonValue()) yield break;

        SpellStats meleeAttack = GetSpellStats(SpellGestion.List.monster_vala_attack);
        SpellStats fireThrowing = GetSpellStats(SpellGestion.List.monster_vala_fireThrowing);
        SpellStats invokeInflamed = GetSpellStats(SpellGestion.List.monster_vala_invokeInflamed);

        bool launchable = meleeAttack.isLaunchable || fireThrowing.isLaunchable || invokeInflamed.isLaunchable;

        Entity target = DecideWhoToAttack();
        float distanceWithTarget = F.DistanceBetweenTwoPos(target, this);

        if (launchable)
        {
            if (invokeInflamed.isLaunchable && (realPm == 0 || distanceWithTarget == 1))
            {
                List<string> pos_list = F.TileAtXDistance(1, CurrentPosition_string);

                string pos = "999";

                int distance = int.MaxValue;

                string targetPosition = target.CurrentPosition_string;

                foreach (string p in pos_list)
                {
                    if (!F.IsTileWalkable(p))
                        continue;

                    int d = Path.Find(new PathParam(p, targetPosition)).path.Count;


                    if (d < distance)
                    {
                        pos = p;
                        distance = d;
                    }
                }

                StartCoroutine(CastASpell(invokeInflamed, null, pos));

                yield break;
            }
            else if (fireThrowing.isLaunchable)
            {
                bool IsInLine = F.IsInLine(target.CurrentPosition_string, CurrentPosition_string);

                bool ContainLineOfView = F.IsLineOfView(target.CurrentPosition_string, CurrentPosition_string);

                bool GoodDistance = distanceWithTarget <= fireThrowing.range_max;

                if (GoodDistance && IsInLine && ContainLineOfView)
                {
                    StartCoroutine(CastASpell(fireThrowing, target));

                    yield break;
                }
                else if (canMoveAndHavePm)
                {
                    StartCoroutine(MoveTowardEntity(target, true, 3, true));

                    yield break;
                }
            }
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
