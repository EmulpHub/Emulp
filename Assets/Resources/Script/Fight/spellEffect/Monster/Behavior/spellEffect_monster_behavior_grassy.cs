using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_grassy_boost : spellEffect_Monster
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForBonus(target))
        {
            caster.Animation_DealDamage(targetedSquareWorldVector2);

            StartCoroutine(spellHolder.Anim_PopDown_Shaking(V.grassy_boost, V.CalcEntityDistanceToBody(target), 1.3f, 1.7f));

            SoundManager.PlaySound(SoundManager.list.monster_grass_boost);

            yield return new WaitForSeconds(0.3f);

            Monster m = (Monster)target;

            int nbPa = calc(2), nbPm = calc(1);

            target.AddEffect(Effect.CreateCustomTxtEffect("Boost", V.IsFr() ? "*bon+" + nbPa + " pa*end et *bon+" + nbPm + " pm*end" : "*bon+" + nbPa + " ap*end and *bon+" + nbPm + " mp*end", calc(1), V.grassy_boost, Effect.Reduction_mode.endTurn));

            int saveTurn = m.lastTurnPlayed;

            target.additionalSize_add(1,
                delegate
                {
                    return saveTurn != m.lastTurnPlayed && EntityOrder.entity != target;
                }
            );

            spellHolder.CreateParticle_Impact_Entering(V.CalcEntityDistanceToBody(target), 1.3f, Spell.Particle_Amount._36, Spell.Particle_Color.yellow);

            target.Add_pa(nbPa);

            yield return new WaitForSeconds(0.4f);

            spellHolder.CreateParticle_Impact_Entering(V.CalcEntityDistanceToBody(target), 1.3f, Spell.Particle_Amount._20, Spell.Particle_Color.green);

            target.Add_pm(nbPm);
        }

        yield return null;
    }
}

public class monster_grassy_heal : spellEffect_Monster
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForBonus(target))
        {
            caster.Animation_DealDamage(targetedSquareWorldVector2);

            StartCoroutine(spellHolder.Anim_PopDown(V.grassy_heal, V.CalcEntityDistanceToBody(target), 1.3f, 1.7f));

            SoundManager.PlaySound(SoundManager.list.monster_grass_heal);

            yield return new WaitForSeconds(0.3f);

            spellHolder.CreateParticle_Impact_Entering(V.CalcEntityDistanceToBody(target), 1.4f, Spell.Particle_Amount._20, Spell.Particle_Color.green);

            int heal = calc(Random.Range(4, 6));

            target.Heal(new InfoHeal(heal));
        }

        yield return null;
    }
}

public class monster_grassy_attack : spellEffect_Monster
{
    public override void InfoBool()
    {
        SetBoolInfo(true, true);
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            caster.Animation_DealDamage(targetedSquareWorldVector2);

            StartCoroutine(spellHolder.Anim_PopDown_Shaking(V.grassy_attack, V.CalcEntityDistanceToBody(target), 1.3f, 1.7f));

            spellHolder.CreateParticle_Impact(V.CalcEntityDistanceToBody(target), 1.2f, Spell.Particle_Amount._20, Spell.Particle_Color.impact);

            SoundManager.PlaySound(SoundManager.list.monster_grass_attack);

            yield return new WaitForSeconds(0.15f);

            int nb = calc(Random.Range(4, 6));

            target.Damage(new InfoDamage(nb, caster));
        }

        yield return null;
    }
}