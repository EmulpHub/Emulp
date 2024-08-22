using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public partial class Monster : Entity
{
    [HideInInspector]
    public MonsterInfo monsterInfo
    {
        get
        {
            return (MonsterInfo)Info;
        }
    }

    [HideInInspector]
    public bool ContainPassive;

    [HideInInspector]
    public Effect Passive_Effect;

    public static MonsterInfo GetNewInfoFromType(MonsterInfo.MonsterType monsterType)
    {
        return (MonsterInfo)new GameObject("info " + monsterType.ToString()).AddComponent(Type.GetType(monsterType.ToString()));
    }

    public void SetType(MonsterInfo.MonsterType monsterType, int level)
    {
        MonsterInfo info = GetNewInfoFromType(monsterType);

        info.transform.parent = V.inGameCreatedGameobjectHolder;

        info.monster_type = monsterType;

        Info = info;

        Info.level = level;

        Info.InitInfo(this);

        monsterInfo.EntityName = monsterInfo.Title();

        monsterSprite = Resources.Load<Sprite>("Image/Monster/" + monsterInfo.monster_type.ToString());
        Renderer_nonMovable.sprite = monsterSprite;

        ContainPassive = true;

        monsterInfo.AddPassiveEffect();

        monsterInfo.SetAvailableSpell();

        monsterInfo.CalculateValue();
        monsterInfo.ResetAllStats();

        monsterInfo.CreateLifeBar(this);
    }

    public override bool ShouldShowLifeBar()
    {
        return base.ShouldShowLifeBar() && !outside;
    }

    public override void OnStart()
    {
        base.OnStart();

        cooldownPeriodicMove = Entity.Animation_Apparition_Speed + 0.2f + UnityEngine.Random.Range(0, 2);

        Eye_Init();
    }

    bool outside = false;

    public override void OnUpdate()
    {
        base.OnUpdate();

        bool inside = Main_Map.currentMap.monsterInArea.Contains(this) || monsterInfo.IsAnInvocation;

        outside = !inside;

        if (outside)
        {

            if (MemoryPosition == Vector3.zero)
                MemoryPosition = transform.position;

            transform.position = new Vector3(100, 100, transform.position.z);

            ResetSpriteAndMovement();

            AliveEntity.Remove(this);
        }
        else
        {
            if (MemoryPosition != Vector3.zero)
            {
                transform.position = MemoryPosition;
                MemoryPosition = Vector3.zero;
            }

            AliveEntity.Add(this);
        }

        Eye_management();
    }
}

