using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityOrder_Ennemy : MonoBehaviour
{

    public List<Monster> list = new List<Monster>();

    public List<Entity> listToEntity
    {
        get
        {
            var list = new List<Entity>();

            foreach(Monster m in this.list)
                list.Add(m);

            return list;
        }
    }

    public void Add(Monster ennemy)
    {
        list.Add(ennemy);
    }

    public void Remove(Monster ennemy)
    {
        if (!list.Contains(ennemy)) return;

        list.Remove(ennemy);
    }

    public void StartTurn()
    {
        foreach (Monster m in new List<Monster>(list))
        {
            m.Turn_start();
        }

        MonsterBehaviorManager.Instance.StartToBehave();
    }

    public void Clear()
    {
        list.Clear();
    }

    public bool Contain(Monster ennemy)
    {
        return list.Contains(ennemy);
    }

    public bool PassTurn ()
    {
        foreach (Monster m in new List<Monster>(list))
        {
            m.Turn_end();
        }

        return true;
    }

    public bool IsTurnOfMonster()
    {
        return !EntityOrder.Instance.AllyTurn;
    }
}
