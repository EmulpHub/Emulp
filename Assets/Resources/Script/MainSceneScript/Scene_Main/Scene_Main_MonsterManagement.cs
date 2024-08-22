using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Scene_Main : MonoBehaviour
{
    public MonsterInfo.MonsterType Focus;

    public GameObject monster_prefab;

    public static int game_monsterLevel = 1;

    public enum MonsterApparitionPreference { none, left, right }

    public static List<string> allMonsterPos = new List<string>();


    public static int currentIdForMonster;

    public static int monster_levelSave = -1;

    public static MonsterInfo.MonsterType monster_typeSave = MonsterInfo.MonsterType.normal;

    public static List<(MonsterInfo.MonsterType type, int level)> monster_list_save = new List<(MonsterInfo.MonsterType type, int level)>();
}
