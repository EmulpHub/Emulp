using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_spell : Action
{
    public Action_spell_info info;

    public Action_spell() : base()
    {
        type = Type.spell;
    }

    protected override IEnumerator Execute_main()
    {
        V.script_Scene_Main.EnregistredPath_clear();

        info.spell.CastSpell(info);

        yield return null;
    }

    public override bool IsFinished()
    {
        return (info.spell == null || info.spell.IsFinish()) && base.IsFinished();
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
                    Action_spell_info_player newSpellInfo = new Action_spell_info_player(Spell.Create(info.spell.spell), info.target, info.targetedSquare);
                    newSpellInfo.SetLaunchValue(false, true, true)
                        .AddMultiplicator((float)e.Str / 100);

                    Add_spell_one(newSpellInfo);

                }
                else
                {
                    Action_spell_info newSpellInfo = new Action_spell_info(Spell.Create(info.spell.spell),info.caster, info.target, info.targetedSquare);
                    newSpellInfo.SetLaunchValue(false, true, true)
                        .AddMultiplicator((float)e.Str / 100);

                    Add_spell_one(newSpellInfo);
                }

                e.ReduceDuration(1);
            }
        }
    }

    private static void Add_spell_one(Action_spell_info info)
    {
        ActionManager.Instance.AddToDo(Create(info));
    }

    public static Action_spell Create (Action_spell_info info)
    {
        Action_spell actionToAdd = new Action_spell();

        actionToAdd.info = info;

        return actionToAdd;
    }

    public override string debug()
    {
        return descColor.convert("casting *dex" + info.spell.spell.ToString() + "*end caster is *dex" + info.caster.Info.EntityName + "*end");
    }
}

