using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EntityInfo : MonoBehaviour
{
    public Entity holder;

    public string EntityName;

    static int IDMax;
    public int ID;

    public virtual void InitInfo(Entity holder)
    {
        this.holder = holder;

        ID = IDMax;
        IDMax++;

        AliveEntity.Add(holder);

        holder.event_turn_start.Add_last(CalculateValueEntity);
        holder.event_turn_end.Add_last(TurnEndEffect);
        holder.event_turn_end.Add_last(CalculateValueEntity);

        CalculateValue();
        ResetAllStats();
    }

    public virtual void CreateLifeBar(Entity holder)
    {
        LifeBar lifeBar = Instantiate(Resources.Load<GameObject>("Script/Alive/LifeBar/LifeBar")).GetComponent<LifeBar>();

        lifeBar.InitInfo(holder);
    }

    public enum Type { player, monster, playerFriendly }
    public Type type;
    public int level = 1;

    private float _life = 100;
    public float Life
    {
        get
        {
            return _life;
        }
        set
        {
            _life = value;
            if (value > Life_max) _life = Life_max;
            if (IsDead() && !dead)
                Action_kill.Add(holder, lastDamageSource?.caster);
        }
    }

    public float Life_max = 100;
    public int PM = 3, PM_max = 3;
    public int PA = 6, PA_max = 6;
    public int tackle, leak;
    public int cc, ec;
    public int tackledEffect = 0;

    public bool dead;

    public int GetRealPm()
    {
        return Mathf.Clamp(PM - tackledEffect, 0, PM);
    }

    public bool CanMove()
    {
        return GetRealPm() > 0;
    }

    public virtual void ResetAllStats()
    {
        Life = Life_max;
        PM = PM_max;
        PA = PA_max;
    }

    public bool IsPlayer()
    {
        return type == Type.player;
    }

    public bool IsMonster()
    {
        return type == Type.monster;
    }

    public bool IsLifeMax()
    {
        return Mathf.FloorToInt(Life) == Mathf.FloorToInt(Life_max);
    }

    public InfoDamage lastDamageSource;

    public virtual void Damage(InfoDamage infoDamage)
    {
        lastDamageSource = infoDamage;

        Life -= infoDamage.damage;
    }

    public void Heal(InfoHeal infoHeal)
    {
        Life += infoHeal.heal;
    }

    public bool IsDead()
    {
        return Life <= 0;
    }

    public void Kill()
    {
        if (dead) return;

        dead = true;
        AliveEntity.Remove(holder);
    }

    public void TurnEndEffect(Entity e = null)
    {
        PA = PA_max;
        PM = PM_max;
    }
}
