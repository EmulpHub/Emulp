using PathFindingName;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using static UnityEngine.GraphicsBuffer;

public class MonsterAction_Movement : MonsterAction
{
    MonsterBehaviorMoveInfo moveInfo;

    public MonsterAction_Movement (MonsterBehaviorMoveInfo moveInfo, PriorityLayer layer, int priority) : base (layer, priority)
    {
        this.moveInfo = moveInfo;
    }

    public override bool Condition()
    {
        Entity target = monsterBehavior.DecideWhoToAttack();
        int distance = F.DistanceBetweenTwoPos(target,info.monster);

        string pos = info.monster.CurrentPosition_string;
        string targetPos = target.CurrentPosition_string;

        if (!info.CanMove()) return false;

        return !ValidPosToGo(pos, targetPos, distance);
    }

    public bool ValidPosToGo(string pos,string targetPos,int distance)
    {
        if (moveInfo.inLine && !F.IsInLine(pos, targetPos)) return false;

        if (moveInfo.lineOfView && !F.IsLineOfView(pos, targetPos)) return false;

        if (distance > moveInfo.distance_max) return false;

        if (distance < moveInfo.distance_min) return false;

        return true;
    }

    public override IEnumerator Execution()
    {
        string targetPos = moveInfo.target.CurrentPosition_string;

        bool selection(string posSelection)
        {
            if(posSelection == targetPos || posSelection == info.monster.CurrentPosition_string) return false;

            int distance = F.DistanceBetweenTwoPos(posSelection, targetPos);

            return ValidPosToGo(posSelection, targetPos, distance);
        }

        string targetSquare = PosToFirstConditionTile.Take(info.monster.CurrentPosition_string, selection);

        if (targetSquare == "")
            throw new System.Exception("Not good pos with pos = " + targetSquare);

        Action_movement.Add(new PathParam(info.monster.CurrentPosition_string, targetSquare), info.monster);

        yield return new WaitForSeconds(1);
    }
}

public class MonsterBehaviorMoveInfo
{
    public bool toward { get; private set; }
    public int distance_min { get; private set; } = 1;
    public int distance_max { get; private set; } = 999;
    public bool lineOfView { get; private set; } = false;
    public bool inLine { get; private set; } = false;
    public Entity target { get; private set; }

    public MonsterBehaviorMoveInfo(Entity target, bool toward)
    {
        this.target = target;
        this.toward = toward;
    }

    public MonsterBehaviorMoveInfo SetDistanceMin(int distance_min)
    {
        this.distance_min = distance_min;
        return this;
    }

    public MonsterBehaviorMoveInfo SetDistanceMax(int distance_max)
    {
        this.distance_max = distance_max;
        return this;
    }

    public MonsterBehaviorMoveInfo SetDistance(int distance_min, int distance_max)
    {
        this.distance_min = distance_min;
        this.distance_max = distance_max;
        return this;
    }

    public MonsterBehaviorMoveInfo SetLineOfView(bool value)
    {
        this.lineOfView = value;
        return this;
    }

    public MonsterBehaviorMoveInfo SetInLine(bool value)
    {
        this.inLine = value;
        return this;
    }
}
