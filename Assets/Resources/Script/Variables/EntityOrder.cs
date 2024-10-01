using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class EntityOrder : MonoBehaviour
{
    public static List<Entity> list = new List<Entity>();

    public static List<Monster> list_monster = new List<Monster>();

    public static int id_turn = 0, id_turn_startCombat;

    public static int nbTurnSinceStartCombat;

    public static int index;

    public static Entity entity;

    private static List<EntityInfo.Type> containType = new List<EntityInfo.Type>();

    public static void Add(Entity entity_toAdd)
    {
        list.Add(entity_toAdd);

        EntityInfo.Type t = entity_toAdd.Info.type;

        if (!containType.Contains(t))
            containType.Add(t);

        if (t == EntityInfo.Type.monster && !list_monster.Contains((Monster)entity_toAdd))
        {
            list_monster.Add((Monster)entity_toAdd);
        }
    }

    /// <summary>
    /// Clear entityOrder
    /// </summary>
    public static void Clear()
    {
        containType.Clear();

        list.Clear();

        list_monster.Clear();
    }

    /// <summary>
    /// Remove a specific entity from the entityOrder (in case of diyng for exemple)
    /// </summary>
    /// <param name="entity_toRemove">The entity we want to remove from entityOrder</param>
    public static void Remove(Entity entity_toRemove)
    {
        if (list.Contains(entity_toRemove))
        {
            if (IsTurnOf(entity_toRemove))
            {
                PassTurn();
            }

            list.Remove(entity_toRemove);

            EntityInfo.Type t = entity_toRemove.Info.type;

            if (t == EntityInfo.Type.monster && list_monster.Contains((Monster)entity_toRemove))
            {
                list_monster.Remove((Monster)entity_toRemove);
            }

            foreach (Entity a in list)
            {
                if (a.Info.type == t)
                    return;
            }

            if (containType.Contains(t))
                containType.Remove(t);
        }
        else
        {
            print("No entity found in entityOrder");
        }
    }

    /// <summary>
    /// Set the turn to be of the one of a specific entity
    /// </summary>
    /// <param name="entity_toSetTurn">Wich entity turn will be</param>
    public static void SetTurn(Entity entity_toSetTurn)
    {
        if (list.Contains(entity_toSetTurn))
        {
            entity = entity_toSetTurn;
            index = list.IndexOf(entity_toSetTurn);
        }
        else
        {
            print("No entity found in entityOrder");
        }
    }

    /// <summary>
    /// check if there is a specific type of entity in the entityOrder for exemple if there is monster
    /// </summary>
    /// <param name="type">The type of entity we want to check</param>
    /// <returns></returns>
    public static bool ContainType(EntityInfo.Type type)
    {
        return containType.Contains(type);
    }

    /// <summary>
    /// Is entityOrder contain at least one monster
    /// </summary>
    /// <returns></returns>
    public static bool ContainMonster()
    {
        return ContainType(EntityInfo.Type.monster);
    }

    /// <summary>
    /// Is entityOrder contain the player
    /// </summary>
    /// <returns></returns>
    public static bool ContainPlayer()
    {
        return ContainType(EntityInfo.Type.player);
    }

    /// <summary>
    /// Is entityOrder contain the entity given in parameter
    /// </summary>
    /// <param name="target">The entity we want to check if it's contain in the list</param>
    /// <returns></returns>
    public static bool Contain(Entity target)
    {
        return list.Contains(target);
    }

    /// <summary>
    /// Is entityOrder current turn is set on entity_isTurnOf
    /// </summary>
    /// <param name="entity_isTurnOf">The entity we want to check if it's his turn</param>
    /// <returns></returns>
    public static bool IsTurnOf(Entity entity_isTurnOf)
    {
        return entity == entity_isTurnOf;
    }

    /// <summary>
    /// Check if it's the turn of player
    /// </summary>
    /// <returns></returns>
    public static bool IsTurnOf_Player()
    {
        return entity == V.player_entity;
    }

    /// <summary>
    /// pass the turn
    /// </summary>
    public static void PassTurn()
    {
        //Clear all tile in scene
        //Combat_Tile_Gestion.Clear();

        //Combat_spell.SetGameAction_movement();

        //Set the void the entity must start at the end of his turn
        entity.Turn_end();

        TileInfo.Instance.ListTile_Clear();

        //Change index
        index++;
        if (index >= list.Count)
        {
            id_turn++;
            nbTurnSinceStartCombat++;
            index = 0;

            foreach (Spell spell in SpellInToolbar.activeSpell_script)
            {
                spell.Turn_reset();
            }
        }

        //set value of entity and set his start function
        entity = list[index];

        entity.Turn_start();
    }
}
