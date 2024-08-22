using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFindingName;

public partial class Monster : Entity
{
    #region ActionInFight

    public bool CanItMove()
    {
        return !IsStuck;
    }

    [HideInInspector]
    public float WaitingTime = 0.25f;

    [HideInInspector]
    public bool IsStuck = false;

    public void StartWhenNoAction(string coroutine_name, float WaitingTime, bool IsItStuck)
    {
        StartCoroutine(StartWhenNoAction_enum(coroutine_name, WaitingTime));

        IsStuck = IsItStuck;
    }

    public void StartWhenNoAction(string coroutine_name, float WaitingTime)
    {
        StartCoroutine(StartWhenNoAction_enum(coroutine_name, WaitingTime));
    }

    public IEnumerator StartWhenNoAction_enum(string coroutine_name, float WaitingTime)
    {
        while (Action.toDo.Count > 0)
            yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(WaitingTime);

        StartCoroutine(coroutine_name);
    }

    #endregion

    public delegate bool condition(Entity target, Entity origin);

    public static (bool find, Entity closest) ChooseClosestAllies(Entity origin, condition Condition)
    {
        (bool find, Entity closest) v = (false, null);

        int closestPath = int.MaxValue;

        foreach (Entity target in EntityOrder.list)
        {
            if (target == origin || target.IsMonster() != origin.IsMonster() || !Condition(target, origin))
                continue;

            var pathParam = new PathParam(origin.CurrentPosition_string, target.CurrentPosition_string).AddListIgnoreEntity(target);

            List<string> path = Path.Find(pathParam).path;

            if (path.Count < closestPath)
            {
                v.find = true;
                v.closest = target;
                closestPath = path.Count;
            }
        }

        return v;
    }

    public static bool CheckRevengeEffect(Entity target, int power_amount, int pa_amount)
    {
        if (target.ContainEffect_byTitle(Effect.GetRevengeName()))
            return false;

        if (EntityOrder.list_monster.Count != 1)
            return false;

        Effect.ApplyRevenge(target, power_amount, pa_amount);

        target.additionalSize_add(1,
    delegate
    {
        return false;
    }
);

        Spell.Reference.CreateParticle_Impact_Entering((Vector2)target.transform.position + V.Entity_DistanceToBody, 1.3f, Spell.Particle_Amount._36, Spell.Particle_Color.blood);

        return true;
    }

    public IEnumerator Wait(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);

        StartCoroutine(currentCoroutine_name);

    }
}
