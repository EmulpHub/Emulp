using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Combat_spell_application : Spell
{
    public static EventHandlerNoArg event_spell_afterCasting_turnOfPlayer = new EventHandlerNoArg(true);

    public static spellEffect Get(SpellGestion.List l)
    {
        spellEffect spellE = (spellEffect)new GameObject("SpellEffect").AddComponent(Type.GetType(l.ToString()));

        spellE.transform.parent = V.inGameCreatedGameobjectHolder;

        return spellE;
    }

    public override IEnumerator CastSpell_effect(Action_spell_info info)
    {
        
        if (EntityOrder.IsTurnOf_Player())
        {
            Scene_Main.SetNoAction();

            if (info.main)
                SpellGestion.SetSelectionnedSpell(SpellGestion.List.empty, null);
        }

        spellEffect spellE = Get(spell);

        spellE.Init(info);

        Task t = new Task(spellE.Cast());

        yield return new WaitUntil(() => { return !t.Running; });

        if (info.main)
        {
            if (EntityOrder.IsTurnOf_Player())
            {
                event_spell_afterCasting_turnOfPlayer.Call();
            }
        }

        EndOfCast = true;
        lastSpellLaunch = this;

        if (EntityOrder.IsTurnOf_Player() && V.game_state_action == V.State_action.Nothing)
            Scene_Main.SetGameAction_movement();
    }
}