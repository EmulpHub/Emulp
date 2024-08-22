using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingEntityInformation : MonoBehaviour
{
    public string pos;

    public MonsterInfo.MonsterType type;

    public int level;

    public bool Alpha;

    public List<(MonsterInfo.MonsterType type, int level)> MonsterToInstantiate;

    public SavingEntityInformation(string pos, MonsterInfo.MonsterType type, int level,
        List<(MonsterInfo.MonsterType type, int level)> MonsterToInstantiate, bool Alpha)
    {
        this.pos = pos;
        this.type = type;
        this.level = level;
        this.MonsterToInstantiate = MonsterToInstantiate;
        this.Alpha = Alpha;
    }
}
