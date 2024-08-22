using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class Monster : Entity
{
    /// <summary>
    /// What the entity should do when it's his turn
    /// </summary>
    /// <returns></returns>
    public IEnumerator Monster_Turn()
    {
        IsStuck = false;

        while (Action.toDo.Count > 0)
        {
            if (ShouldStopTheBehavior())
                yield break;

            yield return new WaitForEndOfFrame();
        }

        ResetUseOfAllAvailableSpell();

        yield return new WaitForSeconds(0.1f);

        switch (monsterInfo.monster_type)
        {
            case MonsterInfo.MonsterType.normal:
                currentCoroutine_name = "Behavior_normal";
                StartCoroutine(Behavior_normal());
                break;
            case MonsterInfo.MonsterType.funky:
                currentCoroutine_name = "Behavior_funky";
                StartCoroutine(Behavior_funky());
                break;
            case MonsterInfo.MonsterType.shark:
                currentCoroutine_name = "Behavior_shark";
                StartCoroutine(Behavior_shark());
                break;
            case MonsterInfo.MonsterType.archer:
                currentCoroutine_name = "Behavior_Archer";
                StartCoroutine(Behavior_Archer());
                break;
            case MonsterInfo.MonsterType.vala:
                currentCoroutine_name = "Behavior_Vala";
                StartCoroutine(Behavior_Vala());
                break;
            case MonsterInfo.MonsterType.inflamed:
                currentCoroutine_name = "Behavior_Inflamed";
                StartCoroutine(Behavior_Inflamed());
                break;
            case MonsterInfo.MonsterType.grassy:
                currentCoroutine_name = "Behavior_grassy";
                StartCoroutine(Behavior_grassy());
                break;
            case MonsterInfo.MonsterType.magnetic:
                currentCoroutine_name = "Behavior_magnetic";
                StartCoroutine(Behavior_magnetic());
                break;
            case MonsterInfo.MonsterType.junior:
                currentCoroutine_name = "Behavior_junior";
                StartCoroutine(Behavior_junior());
                break;
        }
    }

    [HideInInspector]
    public string currentCoroutine_name;

    bool canMoveAndHavePm { get { return ableToMove && realPm > 0; } }

    bool ableToMove { get { return !IsStuck; } }
    int realPm { get { return Info.GetRealPm(); } }

    [HideInInspector]
    public int lastTurnPlayed = -1;

    public bool ShouldStopAndSetCommonValue()
    {
        if (ShouldStopTheBehavior())
            return true;

        ResetAllAnimation();

        lastTurnPlayed = EntityOrder.id_turn;

        return false;
    }

    public bool ShouldStopTheBehavior()
    {
        return !EntityOrder.Contain(this) || !EntityOrder.IsTurnOf(this) || V.game_state != V.State.fight || IsDead();
    }

    public Entity DecideWhoToAttack()
    {
        List<Entity> ennemie = new List<Entity>();
        List<float> value = new List<float>();

        foreach (Entity e in AliveEntity.list)
        {
            if (F.IsEnnemy(e, this))
            {
                ennemie.Add(e);

                value.Add(e.Info.Life * (F.DistanceBetweenTwoPos(e, this) * 0.3f + 1));
            }
        }

        Entity target = null;

        float minValue = float.MaxValue;
        for (int i = 0; i < value.Count; i++)
        {
            if (value[i] < minValue)
            {
                minValue = value[i];
                target = ennemie[i];
            }
        }

        if (target == null) throw new System.Exception("target can't be null");

        return target;
    }
}
