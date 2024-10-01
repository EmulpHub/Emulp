using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;


public class AliveEntity : MonoBehaviour
{
    private static readonly Lazy<AliveEntity> lazy = new(() => new AliveEntity());

    public static AliveEntity Instance { get { return lazy.Value; } }

    private List<Entity> listEntity { get; set; } = new List<Entity>();
    private List<Monster> listMonster { get; set; } = new List<Monster>();

    public void Add(Entity entity)
    {
        if (listEntity.Contains(entity)) return;
        
        listEntity.Add(entity);

        if(entity is Monster monster)
            listMonster.Add(monster);
    }

    public void Remove(Entity entity)
    {
        if (!listEntity.Contains(entity)) return;

        listEntity.Remove(entity);

        if (entity is Monster monster)
            listMonster.Remove(monster);
    }

    public void Clear()
    {
        listEntity.Clear();
        listMonster.Clear();
    }

    #region listeEntityManipulation

    public List<string> GetListPosition()
    {
        List<string> listPosition = new List<string>();

        foreach (Entity entity in listEntity)
            listPosition.Add(entity.CurrentPosition_string);

        return listPosition;
    }

    public bool InValidEntity(Entity entity)
    {
        if (entity == null || entity.Info.IsDead())
            return true;

        return false;
    }

    public delegate void EntityTraveler(Entity entity);

    public void TravelCustomListEntity(EntityTraveler traveler,List<Entity> customListe)
    {
        var listSaveEntity = new List<Entity>(customListe);

        foreach (var entity in listSaveEntity)
        {
            if (InValidEntity(entity))
                continue;

            traveler(entity);
        }
    }

    public void TravelEntity (EntityTraveler traveler)
    {
        TravelCustomListEntity(traveler, listEntity);
    }

    public delegate bool EntityCondition(Entity entity);

    public Entity GetFirstEntity (EntityCondition condition)
    {
        var listSaveEntity = new List<Entity>(listEntity);

        foreach (var entity in listSaveEntity)
        {
            if (InValidEntity(entity))
                continue;

            if (condition(entity))
                return entity;
        }

        return null;
    }

    public bool AnyEntity(EntityCondition condition)
    {
        var listSaveEntity = new List<Entity>(listEntity);

        foreach (var entity in listSaveEntity)
        {
            if (InValidEntity(entity))
                continue;

            if (condition(entity))
                return true;
        }

        return false;
    }

    public List<Entity> WhereEntity(EntityCondition condition)
    {
        var listSaveEntity = new List<Entity>(listEntity);

        var listWhere = new List<Entity>();

        foreach (var entity in listSaveEntity)
        {
            if (InValidEntity(entity))
                continue;

            if (condition(entity))
                listWhere.Add(entity);
        }

        return listWhere;
    }

    #endregion

    #region listMonsterManipulation

    public bool ContainAliveMonster (out int nbMonster)
    {
        nbMonster = 0;

        foreach (Monster monster in listMonster)
        {
            if (InValidMonster(monster))
                continue;

            nbMonster++;
        }

        return nbMonster > 0;
    }

    public bool InValidMonster (Monster monster)
    {
        if (monster == null || monster.Info.IsDead())
            return true;

        return false;
    }

    public delegate void MonsterTraveler(Monster monster);

    public void TravelMonster(MonsterTraveler traveler)
    {
        var listSaveMonster = new List<Monster>(listMonster);

        foreach (var monster in listSaveMonster)
        {
            if (InValidMonster(monster))
                continue;

            traveler(monster);
        }
    }


    public delegate bool MonsterCondition(Monster monster);

    public List<Monster> WhereMonster(MonsterCondition condition)
    {
        var listSaveMonster = new List<Monster>(listMonster);

        var listWhere = new List<Monster>();

        foreach (var monster in listSaveMonster)
        {
            if (InValidMonster(monster))
                continue;

            if (condition(monster))
                listWhere.Add(monster);
        }

        return listWhere;
    }

    #endregion
}
