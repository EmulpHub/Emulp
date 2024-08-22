using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoDamage : MonoBehaviour
{
    public float damage;
    public Entity caster;
    public bool animate;

    public InfoDamage(float damage, Entity caster, bool animate = true)
    {
        this.damage = damage;
        this.animate = animate;
        this.caster = caster;
    }

    public InfoKill CreateInfoKill()
    {
        return new InfoKill(caster);
    }
}
