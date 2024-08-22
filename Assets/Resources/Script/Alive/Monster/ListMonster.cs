using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterRandom;

public class MonsterInMap : MonoBehaviour
{
    public List<MonsterListInfo> list;

    public void add(MonsterListInfo monsterInfo)
    {
        list.Add(monsterInfo);
    }

    public MonsterInMap(MonsterListInfo monsterInfo)
    {
        this.list = new List<MonsterListInfo> { monsterInfo };
    }

    public MonsterInMap(List<MonsterListInfo> list)
    {
        this.list = list;
    }

    public static MonsterInMap CreateRandomListMonster(int lvl_min, int lvl_max, int nbMonster, int availablePoint)
    {
        MonsterInMap listMonster = new(new List<MonsterListInfo>());

        for (int i = 0; i < nbMonster; i++)
            listMonster.add(new MonsterListInfo(Random.Range(lvl_min, lvl_max)));

        MonsterStatic_Random.Randomize(listMonster, availablePoint);

        return listMonster;
    }
}

public class MonsterListInfo
{
    public int level;
    public MonsterInfo.MonsterType type;

    public MonsterListInfo(int level, MonsterInfo.MonsterType type = MonsterInfo.MonsterType.random)
    {
        this.level = level;
        this.type = type;
    }
}
