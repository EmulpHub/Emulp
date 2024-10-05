using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class EntityOrder : MonoBehaviour
{
    private static readonly Lazy<EntityOrder> lazy = new(() => new EntityOrder());
    public static EntityOrder Instance { get { return lazy.Value; } }
    public static EntityOrder_Ally InstanceAlly { get { return Instance.ally; } }
    public static EntityOrder_Ennemy InstanceEnnemy { get { return Instance.ennemy; } }

    private EntityOrder_Ally ally { get; set; }
    private EntityOrder_Ennemy ennemy { get; set; }

    public EntityOrder()
    {
        ally = new EntityOrder_Ally();
        ennemy = new EntityOrder_Ennemy();
    }

    public bool AllyTurn;

    public int id_turn = 0, id_turn_startCombat;

    public int nbTurnSinceStartCombat;

    public bool ContainMonster()
    {
        return ennemy.list.Count > 0;
    }

    public bool ContainPlayer()
    {
        return ally.list.Contains(V.player_entity);
    }

    public bool Contain(Entity target)
    {
        if (target is Monster m && ennemy.Contain(m))
            return true;

        return ally.Contain(target);
    }

    public bool IsTurnOf(Entity target)
    {
        if (!AllyTurn)
            return target is Monster m && ennemy.Contain(m);
        else
            return ally.entity == target;
    }

    public bool IsTurnOf_Player()
    {
        return IsTurnOf(V.player_entity);
    }

    public void StartCombat ()
    {
        AllyTurn = true;
        ally.StartTurn();
    }

    public void PassTurn()
    {
        if (AllyTurn && ally.PassTurn())
        {
            AllyTurn = false;
            ennemy.StartTurn();
        }
        else if (!AllyTurn && ennemy.PassTurn())
        {
            AllyTurn = true;
            ally.StartTurn();
        }

        id_turn++;
        nbTurnSinceStartCombat++;

        TileInfo.Instance.ListTile_Clear();
    }

    public void Clear()
    {
        ally.Clear();
        ennemy.Clear();
    }
}
