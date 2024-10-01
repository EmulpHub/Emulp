using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellStats
{
    public Monster holder;

    public SpellStats(Monster holder, SpellGestion.List sp, bool withDistance)
    {
        this.holder = holder;

        spell_type = sp;

        int cd = SpellGestion.Get_Cd(sp);

        if (cd < 0)
        {
            nbUseMax = Mathf.Abs(cd);
            cooldown_max = 0;
        }
        else
        {
            nbUseMax = 0;
            cooldown_max = cd;
        }

        cooldown = 0;

        nbUse = 0;

        range_max = SpellGestion.Get_RangeMax(sp);
        range_min = SpellGestion.Get_RangeMin(sp);
        pa_cost = SpellGestion.Get_paCost(sp);

        checkDistance = withDistance;
    }

    public int range_min, range_max, pa_cost;

    /// <summary>
    /// The spell_list of this spell
    /// </summary>
    public SpellGestion.List spell_type;

    /// <summary>
    /// The cooldown before use it again
    /// </summary>
    public int cooldown, cooldown_max;

    public int nbUse, nbUseMax;

    public bool checkDistance;

    private Entity permanentTarget;

    public Entity PermanentTarget
    {
        get
        {
            if (permanentTarget == null)
                return V.player_entity;
            return V.player_entity;
        }
        set
        {
            permanentTarget = value;
        }
    }

    public bool isLaunchable { get { return Launchable(permanentTarget); } }

    public bool isLaunchable_onlyPAAndCooldown { get { return Launchable_cooldownAndPa(); } }

    public bool isLaunchable_onlyCooldown { get { return Launchable_cooldownAndPa(); } }

    public bool isLaunchable_onlyDistance { get { return Launchable_distance(permanentTarget); } }

    public bool Launchable(Entity target = null)
    {
        bool general = Launchable_cooldownAndPa();

        if (checkDistance && general)
        {

            return Launchable_distance(target);
        }
        return general;
    }

    public bool Launchable_distance(Entity target)
    {
        int distance = F.DistanceBetweenTwoPos(holder.CurrentPosition_string, target.CurrentPosition_string);

        return range_min <= distance && distance <= range_max && F.IsLineOfView(holder.CurrentPosition_string, target.CurrentPosition_string);
    }

    public bool Launchable_cooldownAndPa()
    {
        return IsLaunchable_Cooldown() && (holder.Info.PA >= pa_cost);
    }

    public bool IsLaunchable_Cooldown()
    {
        return cooldown <= EntityOrder.id_turn || (nbUse < nbUseMax && nbUseMax != 0);
    }

    public void SetCooldown()
    {
        cooldown = cooldown_max + EntityOrder.id_turn;
    }

    public void SetUse()
    {
        if (cooldown_max > 0)
        {
            SetCooldown();
        }
        else
        {
            nbUse++;
        }
    }

    public void ResetUse()
    {
        nbUse = 0;
    }
}
