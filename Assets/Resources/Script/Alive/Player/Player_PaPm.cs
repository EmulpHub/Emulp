using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Entity
{
    public override void Add_pa(int amount, bool Display = true)
    {
        base.Add_pa(amount, Display);
    }

    public override void EffectWhenRemovingPm(int amount)
    {
        base.EffectWhenRemovingPm(amount);

        if (ContainEffect_byTitle("Rush") && Info.PM <= 0)
        {
            bool first = true;

            float TotalPower = 15;

            int count = 1;

            List<AnimEndless> visualEffectLs = new List<AnimEndless>();

            while (ContainEffect_byTitle("Rush"))
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    TotalPower += 15;
                    TotalPower *= 1.15f;
                    count++;
                }

                GameObject part = Spell.Reference.CreateParticle_Circle_Endless(V.player_entity.transform.position + new Vector3(0, 0.5f, 0), 1 + count * 0.2f, Spell.Particle_Amount._24, Spell.Particle_Color.blood);

                visualEffectLs.Add(AnimEndless_Particle.Create(V.player_entity, new Vector3(0, 0.5f, 0), part, AnimEndless_Particle.AnimationExit.FadeOut_particle, 1, 1));

                if (V.IsFr())
                    Main_UI.Display_movingText_basicValue("+15 % dégâts", V.Color.green, V.player_entity.transform.position);
                else
                    Main_UI.Display_movingText_basicValue("+15 % damage", V.Color.green, V.player_entity.transform.position);

                RemoveEffect_byTitle("Rush");
            }

            SoundManager.PlaySound(SoundManager.list.object_Rush_Second);

            Effect.Reduction_mode reduc = EntityOrder.Instance.IsTurnOf_Player() ? Effect.Reduction_mode.startTurn : Effect.Reduction_mode.endTurn;

            Effect g = AddEffect(
                Effect.CreateEffect("Rush", Effect.effectType.power, Mathf.CeilToInt(TotalPower), 1, SpellGestion.Get_sprite(SpellGestion.List.object_rush), reduc)
                );

            foreach (var endless in visualEffectLs)
            {
                g.EndlessAnim_add(endless);
            }
        }
    }
}
