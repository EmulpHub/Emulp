using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Map : MonoBehaviour
{
    [HideInInspector]
    public GameObject CurrentAlphaMonster;

    public string monsterStartPosition;

    public List<Monster> monsterInArea = new List<Monster>();

    public bool MonsterShouldRespawn;

    public int MonsterRespawnTime;

    public static Dictionary<string, float> LastMonsterDeathOnMap = new Dictionary<string, float>();

    public int monsterNumberMin, monsterNumberMax;

    public bool MonsterMovementNotAllowed;

    public List<Monster> InstantiateListMonster(MonsterInMap listMonster)
    {
        var lsMonster = new List<Monster>();

        foreach (MonsterListInfo monsterInfo in listMonster.list)
        {
            var entity = Invocating.CreateEntity(new CreaterInfoMonster(Main_Map.currentMap.monsterStartPosition, monsterInfo.level, monsterInArea.Count == 0, monsterInfo.type));

            var monster = entity as Monster;

            monsterInArea.Add(monster);
            lsMonster.Add(monster);
        }

        return lsMonster;
    }

    public static Dictionary<string, SavingEntityInformation> MonsterInfoPerMap = new Dictionary<string, SavingEntityInformation>();

    public static (bool find, SavingEntityInformation info) MonsterInfoPerMap_Get(string pos)
    {
        if (MonsterInfoPerMap.ContainsKey(pos))
        {
            return (true, MonsterInfoPerMap[pos]);
        }
        else
        {
            return (false, null);
        }
    }

    public static void MonsterPerInfo_Add(string pos, SavingEntityInformation save)
    {
        if (MonsterInfoPerMap.ContainsKey(pos))
        {
            MonsterInfoPerMap[pos] = save;
        }
        else
        {
            MonsterInfoPerMap.Add(pos, save);
        }
    }
}
