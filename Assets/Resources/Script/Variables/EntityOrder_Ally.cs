using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityOrder_Ally : MonoBehaviour
{
    public List<Entity> list = new List<Entity>();

    public Entity entity;
    public int index;

    public void Add(Entity ally)
    {
        list.Add(ally);
    }

    public void Remove(Entity ally)
    {
        if (!list.Contains(ally)) return;

        list.Remove(ally);
    }

    public bool Contain(Entity ally)
    {
        return list.Contains(ally);
    }

    public void StartTurn()
    {
        entity = V.player_entity;
        index = list.IndexOf(V.player_entity);
        if (index != 0)
        {
            list.RemoveAt(index);
            list.Insert(0, V.player_entity);
            index = 0;
        }

        entity.Turn_start();
    }

    public void Clear()
    {
        list.Clear();
    }

    /// <summary>
    /// Return true if it the turn of monster
    /// </summary>
    /// <returns></returns>
    public bool PassTurn()
    {
        entity.Turn_end();

        //Change index
        index++;
        if (index >= list.Count)
        {
            index = 0;

            foreach (Spell spell in SpellInToolbar.activeSpell_script)
            {
                spell.Turn_reset();
            }

            return true;
        }
        else
        {
            //set value of entity and set his start function
            entity = list[index];

            entity.Turn_start();

            return false;
        }
    }
}
