using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster : Entity
{
    [HideInInspector]
    public Vector3 MemoryPosition = Vector3.zero;

    public Dictionary<SpellGestion.List, SpellStats> allAvailableStats = new Dictionary<SpellGestion.List, SpellStats>();

    public SpellStats GetSpellStats(SpellGestion.List l)
    {
        return allAvailableStats[l];
    }
}
