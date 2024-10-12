using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Entity
{
    public override void Turn_start()
    {

        string floor = SpellGestion.Get_Title(SpellGestion.List.object_floor);

        if (ContainEffect_byTitle(floor))
        {
            RemoveEffect_byTitle(floor);

            Remove_pa(3, true, true, " (" + floor + ")");

            SoundManager.PlaySound(SoundManager.list.object_Floor_Losing);

            StartCoroutine(
    Spell.Reference.Anim_PopUpBig(SpellGestion.Get_sprite(SpellGestion.List.object_floor), V.player_entity.transform.position + new Vector3(0, 0.5f, 0))
    );
            Spell.Reference.CreateParticle_Leaf(V.player_entity.transform.position, 1);

            Spell.Reference.CreateParticle_Lowering(V.player_entity.transform.position + new Vector3(0, 1.5f, 0), 2, Spell.Particle_Amount._12, Spell.Particle_Color.blood);
        }

        base.Turn_start();

        V.main_ui.Display_startOfTurn();
    }

    public override void Turn_end()
    {
        base.Turn_end();
    }

    public static EventHandlerNoArg event_startCombat = new EventHandlerNoArg(true);

    public void StartCombat()
    {
        EntityOrder.Instance.nbTurnSinceStartCombat = 0;
        spellEffect.nbPaUsedThisCombat = 0;

        EntityOrder.Instance.id_turn_startCombat = EntityOrder.Instance.id_turn;

        V.player_entity.ResetAllStats();

        event_startCombat.Call();

        V.player_info.CalculateValue();

        EntityOrder.Instance.StartCombat();
    }

}
