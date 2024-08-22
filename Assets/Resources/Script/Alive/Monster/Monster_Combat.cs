using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster : Entity
{
    public void ResetUseOfAllAvailableSpell()
    {
        foreach (SpellStats spellStats in allAvailableStats.Values)
        {
            spellStats.ResetUse();
        }
    }
}
