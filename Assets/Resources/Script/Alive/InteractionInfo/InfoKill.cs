using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoKill : MonoBehaviour
{
    public Entity caster;

    public InfoKill(Entity caster)
    {
        this.caster = caster;
    }
}
