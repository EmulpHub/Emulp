using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_spell : Action
{
    public Action_spell_info info;

    public Spell info_spell { get { return info.spell; } }

    public Action_spell() : base()
    {
        type = Type.spell;
    }

    internal override void Execute_main()
    {
        V.script_Scene_Main.EnregistredPath_clear();

        info_spell.CastSpell(info); //Must be same param than mainIsLaunchable
    }

    public override bool Finished()
    {
        if (base.Finished()) return true;

        return (info_spell == null || info_spell.IsFinish()) && Executed;
    }

    public static void Add(Action_spell_info info)
    {
        Add_spell_one(info);

        if (info.caster.ContainEffect(Effect.effectType.additionalSpellCast))
        {
            Effect e = info.caster.GetEffect(Effect.effectType.additionalSpellCast);

            while (e.DurationInTurn > 0)
            {
                if (info.caster == V.player_entity)
                {
                    Action_spell_info_player newSpellInfo = new Action_spell_info_player();

                    newSpellInfo.spell = Spell.Create(info.spell.spell);
                    newSpellInfo.caster = info.caster;
                    newSpellInfo.listTarget = new List<Entity>(info.listTarget);
                    newSpellInfo.targetedSquare = info.targetedSquare;
                    newSpellInfo.forceLaunch = true;
                    newSpellInfo.dontUseCost = true;
                    newSpellInfo.main = false;
                    newSpellInfo.multiplicator += ((float)e.Str / 100);

                    Add_spell_one(newSpellInfo);

                }
                else
                {
                    Action_spell_info newSpellInfo = new Action_spell_info();

                    newSpellInfo.spell = Spell.Create(info.spell.spell);
                    newSpellInfo.caster = info.caster;
                    newSpellInfo.listTarget = new List<Entity>(info.listTarget);
                    newSpellInfo.targetedSquare = info.targetedSquare;
                    newSpellInfo.forceLaunch = true;
                    newSpellInfo.dontUseCost = true;
                    newSpellInfo.main = false;

                    Add_spell_one(newSpellInfo);
                }

                e.ReduceDuration(1);
            }
        }
    }

    private static void Add_spell_one(Action_spell_info info)
    {
        Action_spell actionToAdd = new Action_spell();

        actionToAdd.info = info;

        toDo.Add(actionToAdd);
    }

    public override string debug()
    {
        return descColor.convert("casting *dex" + info.spell.spell.ToString() + "*end caster is *dex" + info.caster.Info.EntityName + "*end");
    }
}

public class Action_spell_info
{
    public bool main = true;
    public bool forceLaunch = false;
    public bool dontUseCost = false;

    public string targetedSquare = "erreur";

    public Spell spell;
    public Entity caster;
    private Entity _target;

    private List<Entity> _listTarget = new List<Entity>();


    public List<Entity> listTarget
    {
        get
        {
            return _listTarget;
        }
        set
        {
            _listTarget = value;
            _target = value[0];
        }
    }

    public Entity target
    {
        get
        {
            return _target;
        }
    }

}

public class Action_spell_info_player : Action_spell_info
{
    public float str, dex, res;
    public int eff;

    public float multiplicator = 1;

    public Action_spell_info_player()
    {
        str = V.player_info.str;
        dex = V.player_info.dex;
        res = V.player_info.res;
        eff = V.player_info.eff;
    }
}