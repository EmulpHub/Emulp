using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Action_spell_info
{
    public Spell spell { get; private set; }
    public Entity caster { get; private set; }
    public Entity target { get; private set; }
    public string targetedSquare { get; private set; } = "ERROR";

    public Action_spell_info(Spell spell, Entity caster, Entity target, string targetedSquare)
    {
        this.spell = spell;
        this.caster = caster;
        this.target = target;
        this.targetedSquare = targetedSquare;
    }
    public bool main { get; private set; } = true;
    public bool forceLaunch { get; private set; } = false;
    public bool dontUseCost { get; private set; } = false;
    public float multiplicator { get; private set; } = 1;

    public Action_spell_info SetLaunchValue(bool main, bool forceLaunch = false, bool dontUseCost = false)
    {
        this.main = main;
        this.forceLaunch = forceLaunch;
        this.dontUseCost = dontUseCost;
        return this;
    }

    public Action_spell_info SetMultiplicator(float value)
    {
        multiplicator = value;
        return this;
    }

    public Action_spell_info AddMultiplicator(float value)
    {
        multiplicator += value;
        return this;
    }
}
