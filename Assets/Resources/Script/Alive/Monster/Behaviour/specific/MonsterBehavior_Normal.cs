using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterBehavior_normal : MonsterBehavior
{
    public override void SetMonsterAction()
    {
        base.SetMonsterAction();

        //Movement
        var movementInfo = new MonsterBehaviorMoveInfo(V.player_entity,true).SetDistanceMax(1);
        
        MonsterAction_Movement actionMovement = new MonsterAction_Movement(movementInfo,MonsterAction.PriorityLayer.Movement,0);
        
        //Spell
        SpellStats spellStats = Info.monster.GetSpellStats(SpellGestion.List.monster_normal_attack);

        MonsterAction_Spell actionSpell = new MonsterAction_Spell(spellStats, MonsterAction.PriorityLayer.Attack, 0);
        
        AddAction(actionMovement);
        AddAction(actionSpell);
    }
}
