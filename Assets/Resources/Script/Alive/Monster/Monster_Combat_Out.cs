using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFindingName;

public partial class Monster : Entity
{
    public int zRotation_max, zRotation_max_combat;

    public float animationOutCombat_moveSpeed, animationCombat_moveSpeed;

    [HideInInspector]
    public float cooldownPeriodicMove = 0;

    public float cooldownPeriodicMove_reset_min, cooldownPeriodicMove_reset_max;

    public int periodicMove_MaxDistance_min = 3, periodicMove_MaxDistance_max = 4;

    void OutCombat_Movement_Monster()
    {
        if (outside) return;

        if (cooldownPeriodicMove <= 0 && V.game_state == V.State.passive)
        {
            bool selectionCondition(string p)
            {
                return F.DistanceBetweenTwoPos(p, CurrentPosition_string) <= 4;
            }

            bool researcheCondition(string p)
            {
                return F.IsTileWalkable(p);
            }

            string pos = RandomPos.Take(CurrentPosition_string, 6, researcheCondition, selectionCondition);

            MoveTo(new PathParam(CurrentPosition_string, pos));

            SetRandomCooldownPeriodicMove();
        }

        if (!runningInfo.isRunning && !Main_Map.currentMap.MonsterMovementNotAllowed)
        {
            cooldownPeriodicMove -= 1 * Time.deltaTime;
        }
    }

    public void SetRandomCooldownPeriodicMove()
    {
        cooldownPeriodicMove = Random.Range(cooldownPeriodicMove_reset_min, cooldownPeriodicMove_reset_max + 1);
    }

    public override void Update_passive()
    {
        base.Update_passive();

        float distance = F.DistanceBetweenTwoPos_float(F.ConvertToWorldVector2(V.player_entity.CurrentPosition), F.ConvertToWorldVector2(CurrentPosition));

        if ((distance <= 0.5f || V.script_Scene_Main_Administrator.InstaFight) && !outside)
        {
            foreach (Monster m in AliveEntity.listMonster)
            {
                m.ResetSpriteAndMovement();

                m.cooldownPeriodicMove = 1;
            }

            V.player_entity.ResetSpriteAndMovement();

            Scene_Main.LaunchCombat(Main_Map.currentMap.monsterInArea);
        }

        OutCombat_Movement_Monster();

        GestionHighlight();
    }

    public void GestionHighlight()
    {
        if (!IsMouseOnEntity && HighlighMonster.Contains(this))
            Thicness_Over();
    }
}