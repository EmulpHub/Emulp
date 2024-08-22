using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class weapon_knife : spellEffect_Player
{
    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            V.player_entity.AddEffect(
                Effect.CreateCustomEffect(1, holderSprite, Effect.Reduction_mode.never, "", new List<Effect>{
                   Effect.CreateEffect(Effect.effectType.lifeSteal,calc(50),1,holderSprite,Effect.Reduction_mode.never)
                })
                , false);

            target.Damage(new InfoDamage(calcDamage(20), caster));

            V.player_entity.RemoveEffect_byTitle("vlv");
        }

        yield return null;
    }
}


public class weapon_crowBar : spellEffect_Player
{
    public override IEnumerator Effect_before()
    {
        caster.Animation_DealDamage(targetedSquare);

        yield return null;
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            Vector2 posToAttack = V.CalcEntityDistanceToBody(target);

            if (F.IsFaceRight_entity(pos, caster.CurrentPosition_string))
            {
                StartCoroutine(spellHolder.Anim_Kick(holderSprite, posToAttack + new Vector2(0.55f, 1.2f), true, 0.8f, 90));
            }
            else
            {
                StartCoroutine(spellHolder.Anim_Kick(holderSprite, posToAttack + new Vector2(-0.55f, 1.2f), false, 0.8f, 90));
            }

            yield return new WaitForSeconds(0.15f);

            SoundManager.PlaySound(SoundManager.list.weapon_crowBar);

            spellHolder.CreateParticle_Impact(posToAttack, 1.5f, Spell.Particle_Amount._20);

            //If it's a monster make animation like he receive damage
            caster.Animation_DealDamage(targetedSquareWorldVector2);

            target.Damage(new InfoDamage(calcDamage(18), caster));

            target.Push(calc(2), caster);
        }

        yield return null;
    }
}

public class weapon_ak47 : spellEffect_Player
{

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            float speed = F.Projectile_CalculateTravelTime(caster, target);

            Vector2 Launching()
            {
                caster.Animation_DealDamage(targetedSquareWorldVector2);

                Vector2 posToAttack = (Vector2)target.transform.position + new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f) + 0.5f);

                StartCoroutine(spellHolder.Anim_Projectile(caster, V.weapon_bullet, posToAttack, false, speed));

                SoundManager.PlaySound(SoundManager.list.weapon_ak47_Launch);

                return posToAttack;
            }

            void Impact(Vector2 posToAttack, string add = "")
            {
                SoundManager.PlaySound(SoundManager.list.weapon_ak47_Impact);

                spellHolder.CreateParticle_Impact(posToAttack);

                target.Damage(new InfoDamage(calcDamage(5), caster));
            }

            Vector2 pos_1 = Launching();

            yield return new WaitForSeconds(0.1f);

            Vector2 pos_2 = Launching();

            yield return new WaitForSeconds(0.1f);

            Vector2 pos_3 = Launching();

            yield return new WaitForSeconds(speed - 0.2f);

            Impact(pos_1, " ");

            yield return new WaitForSeconds(0.1f);

            Impact(pos_2, "  ");

            yield return new WaitForSeconds(0.1f);

            Impact(pos_3, "   ");
        }
    }
}

public class weapon_shieldAttack : spellEffect_Player
{
    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            target.Damage(new InfoDamage(calcDamage(13), caster));

            V.player_info.AddArmor(calc(15));
        }

        yield return null;
    }
}

public class weapon_sniper : spellEffect_Player
{
    public override IEnumerator Effect_before()
    {
        caster.Animation_DealDamage(targetedSquareWorldVector2);

        yield return null;
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {

        if (TargetForDamage(target))
        {
            float dis = F.DistanceBetweenTwoPos(caster, target);

            float speed = F.Projectile_CalculateTravelTime(caster, target) * 0.8f;

            Vector2 posToAttack = V.CalcEntityDistanceToBody(target);

            StartCoroutine(spellHolder.Anim_Projectile(caster, V.weapon_bulletStrong, posToAttack, false, speed, dis * 0.08f + 1));

            SoundManager.PlaySound(SoundManager.list.weapon_Sniper_Launch);

            yield return new WaitForSeconds(speed);

            SoundManager.PlaySound(SoundManager.list.weapon_Sniper_Impact);

            if (dis >= 7)
            {
                spellHolder.CreateParticle_Impact(posToAttack, dis * 0.15f + 1, Spell.Particle_Amount._24);

            }
            else
            {
                spellHolder.CreateParticle_Impact(posToAttack, dis * 0.1f + 1, Spell.Particle_Amount._12);

            }

            target.Damage(new InfoDamage(calcDamage(15 + 5 * dis), caster));
        }
    }
}

public class weapon_bow : spellEffect_Player
{
    public override IEnumerator Effect_before()
    {
        caster.Animation_DealDamage(targetedSquareWorldVector2);

        yield return null;
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            float speed = F.Projectile_CalculateTravelTime(caster, target);

            Sprite arrow = Resources.Load<Sprite>("Image/Monster/Spell/arrow");

            Vector2 posToAttack = V.CalcEntityDistanceToBody(target);

            StartCoroutine(spellHolder.Anim_Projectile(caster, arrow, posToAttack, false, speed));

            SoundManager.PlaySound(SoundManager.list.weapon_bow_launch);

            yield return new WaitForSeconds(speed);

            SoundManager.PlaySound(SoundManager.list.weapon_bow_impact);

            spellHolder.CreateParticle_Impact(posToAttack, 1, Spell.Particle_Amount._8);

            target.Damage(new InfoDamage(calcDamage(15), caster));
        }
    }
}

public class weapon_taser : spellEffect_Player
{
    public override IEnumerator Effect_before()
    {
        caster.Animation_DealDamage(targetedSquareWorldVector2);

        yield return null;
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            StartCoroutine(spellHolder.Anim_Projectile_DoMove(caster, V.base_taser_illustration, V.CalcEntityDistanceToBody(target), false, 0.3f, 1.3f, 180));

            SoundManager.PlaySound(SoundManager.list.weapon_taser);

            yield return new WaitForSeconds(0.15f);

            spellHolder.CreateParticle_Leaf(targetedSquareWorldVector2, 1.2f);

            spellHolder.CreateParticle_Impact(V.CalcEntityDistanceToBody(target), 1.2f);

            target.Damage(new InfoDamage(calcDamage(16), caster));

            target.Remove_pa(calc(2), true, true);
        }
    }
}


public class weapon_mace : spellEffect_Player
{
    public override IEnumerator Effect_before()
    {
        caster.Animation_DealDamage(targetedSquareWorldVector2);

        yield return null;
    }

    bool soundPlayed = false;

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        StartCoroutine(spellHolder.Anim_FallingDown(V.mace_illustration, targetedSquareWorldVector2 + new Vector2(0, 1.3f), 2));

        yield return new WaitForSeconds(0.1f);

        if (TargetForDamage(target))
        {
            target.Damage(new InfoDamage(calcDamage(10) + V.player_info.Life_max * 0.05f, caster));

            spellHolder.CreateParticle_Impact(V.CalcEntityDistanceToBody(target), 1.2f, Spell.Particle_Amount._12);
        }

        if (!soundPlayed)
        {
            soundPlayed = true;
            SoundManager.PlaySound(SoundManager.list.weapon_mace);
        }

        spellHolder.CreateParticle_Leaf(targetedSquareWorldVector2);
    }
}

public class weapon_littleSword : spellEffect_Player
{
    public override IEnumerator Effect_before()
    {
        SoundManager.PlaySound(SoundManager.list.weapon_littleSword);

        caster.Animation_DealDamage(targetedSquareWorldVector2);

        LittleSword_Direction = F.IsFaceRight_entity(caster.CurrentPosition_string, targetedSquare);

        yield return null;
    }

    bool LittleSword_Direction = false;

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            StartCoroutine(spellHolder.Anim_LittleSword(holderSprite, V.CalcEntityDistanceToBody(target) + (LittleSword_Direction ? new Vector2(-0.3f, 0) : new Vector2(0.3f, 0)), LittleSword_Direction));

            yield return new WaitForSeconds(0.1f);

            spellHolder.CreateParticle_Impact(V.CalcEntityDistanceToBody(target));

            target.Damage(new InfoDamage(calcDamage(16), caster));
        }
    }

    public override IEnumerator Cast_After()
    {
        int damageGive = calc(7);

        V.player_entity.AddEffect(
            Effect.CreateEffect(Effect.effectType.damage_oneUse, damageGive, 0, SpellGestion.Get_sprite(spellHolder.spell), Effect.Reduction_mode.never)
            );

        yield return null;
    }
}

public class weapon_magicWand : spellEffect_Player
{
    public static int MagicWand_MaxStat = 20;

    float size = 1, damage = 5;

    public override IEnumerator Effect_before()
    {
        /*

        if (caster.ContainPower(Effect.effectType.custom_magic))
        {
            float effect = caster.GetPower(Effect.effectType.custom_magic);

            if (effect >= MagicWand_MaxStat)
            {
                size = 2;
            }
            else
            {
                size += effect / MagicWand_MaxStat * 0.7f;
            }

            damage += effect;
        }

        SoundManager.PlaySound(SoundManager.list.weapon_magicWand);
        */
        StartCoroutine(spellHolder.Anim_MagicWand(holderSprite, targetedSquareWorldVector2 + new Vector2(0.55f, 1.2f), true, size + 0.7f));

        yield return null;
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            target.Damage(new InfoDamage(calcDamage(damage), caster));

            if (size > 1)
                spellHolder.CreateParticle_Impact(V.CalcEntityDistanceToBody(target), 1, Spell.Particle_Amount._24);
            else
                spellHolder.CreateParticle_Impact(V.CalcEntityDistanceToBody(target), 1, Spell.Particle_Amount._8);
        }

        spellHolder.CreateParticle_Leaf(targetedSquareWorldVector2);

        yield return null;
    }

    public override IEnumerator Cast_After()
    {
        yield return null;
    }

    public void EffectAfterCast()
    {
        bool ContainPower = caster.ContainEffect(Effect.effectType.custom_magic);

        int nbPower = 0;

        if (ContainPower)
            nbPower = caster.GetEffect(Effect.effectType.custom_magic).Str;

        if (nbPower < weapon_magicWand.MagicWand_MaxStat)
        {
            string title = V.IsFr() ? "Baguette magique" : "Magic wand";

            caster.AddEffect(
                Effect.CreateEffect(title, Effect.effectType.custom_magic, 2, 1, V.weapon_magic, Effect.Reduction_mode.never)
                );

            spellHolder.CreateParticle_Bonus(caster.transform.position + new Vector3(0, 0.5f, 0), 1, Spell.Particle_Amount._8);

            StartCoroutine(spellHolder.Anim_PopDown(V.weapon_magic, caster.transform.position + new Vector3(0, 0.5f, 0), 1, 1.4f));

            SoundManager.PlaySound(SoundManager.list.weapon_magicWand_magic);
        }
    }


    int id = 0;

    public override void actionToolbar_add(int toolbarId)
    {
        base.actionToolbar_add(toolbarId);

        id = Combat_spell_application.event_spell_afterCasting_turnOfPlayer.Add(EffectAfterCast);
    }

    public override void actionToolbar_remove(int toolbarId)
    {
        base.actionToolbar_remove(toolbarId);

        Combat_spell_application.event_spell_afterCasting_turnOfPlayer.Remove(id);
    }
}

public class weapon_statue : spellEffect_Player
{
    bool soundPlayed = false;

    public override IEnumerator Effect_before()
    {
        caster.Animation_DealDamage(targetedSquareWorldVector2);

        yield return null;
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        float size = 1;

        if (TargetForDamage(target))
        {
            if (caster.Info.PM == 0)
            {
                size = 1.3f;
                target.Damage(new InfoDamage(calcDamage(24), caster));
            }
            else
            {
                target.Damage(new InfoDamage(calcDamage(10), caster));
            }

            spellHolder.CreateParticle_Impact(V.CalcEntityDistanceToBody(target), 1, size > 1 ? Spell.Particle_Amount._24 : Spell.Particle_Amount._8);
        }

        if (!soundPlayed)
        {
            soundPlayed = true;
            SoundManager.PlaySound(SoundManager.list.weapon_statue);
        }

        StartCoroutine(spellHolder.Anim_FallingDown(holderSprite, targetedSquareWorldVector2 + new Vector2(0, 1.3f), 1, size));

        yield return new WaitForSeconds(0.2f);

        spellHolder.CreateParticle_Leaf(targetedSquareWorldVector2 - new Vector2(0, 0.5f), 1.2f);
    }

    public override IEnumerator Cast_After()
    {
        yield return null;
    }
}

public class weapon_dagger : spellEffect_Player
{
    public override IEnumerator Effect_before()
    {
        yield return null;
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            float speed = F.Projectile_CalculateTravelTime(caster, target) * 1.4f;

            Sprite arrow = Resources.Load<Sprite>("Image/miscellaneous/dagger");

            caster.Animation_DealDamage(targetedSquareWorldVector2);

            float dispersion = 0.5f;

            Vector2 posToAttack_1 = (Vector2)target.transform.position + new Vector2(UnityEngine.Random.Range(-dispersion, dispersion), 0.5f + UnityEngine.Random.Range(-dispersion, dispersion));

            SoundManager.PlaySound(SoundManager.list.weapon_dagger_launching);

            StartCoroutine(spellHolder.Anim_Projectile(caster, arrow, posToAttack_1, false, speed));

            yield return new WaitForSeconds(0.2f);

            SoundManager.PlaySound(SoundManager.list.weapon_dagger_launching);

            caster.Animation_DealDamage(targetedSquareWorldVector2);

            Vector2 posToAttack_2 = (Vector2)target.transform.position + new Vector2(UnityEngine.Random.Range(-dispersion, dispersion), 0.5f + UnityEngine.Random.Range(-dispersion, dispersion));

            StartCoroutine(spellHolder.Anim_Projectile(caster, arrow, posToAttack_2, false, speed));

            yield return new WaitForSeconds(Mathf.Clamp(speed - 0.2f, 0, 10));

            SoundManager.PlaySound(SoundManager.list.weapon_dagger_impact);

            spellHolder.CreateParticle_Impact(posToAttack_1, 1, Spell.Particle_Amount._8);

            target.Damage(new InfoDamage(calcDamage(6), caster));

            yield return new WaitForSeconds(0.2f);

            SoundManager.PlaySound(SoundManager.list.weapon_dagger_impact);

            spellHolder.CreateParticle_Impact(posToAttack_2, 1, Spell.Particle_Amount._8);

            target.Damage(new InfoDamage(calcDamage(6), caster));
        }
    }

    public override IEnumerator Cast_After()
    {
        yield return null;
    }
}

public class weapon_bowlingBall : spellEffect_Player
{
    public override IEnumerator Effect_before()
    {
        caster.Animation_DealDamage(targetedSquareWorldVector2);

        yield return null;
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {

        if (TargetForDamage(target))
        {
            /* float dis = F.DistanceBetweenTwoPos(caster, target);

             float speed = F.Projectile_CalculateTravelTime(caster, target) * 0.8f;

             Vector2 posToAttack = V.CalcEntityDistanceToBody(target);

             StartCoroutine(h.Anim_Projectile(caster, V.weapon_bulletStrong, posToAttack, false, speed, dis * 0.08f + 1));

             SoundManager.PlaySound(SoundManager.list.weapon_Sniper_Launch);

             yield return new WaitForSeconds(speed);

             SoundManager.PlaySound(SoundManager.list.weapon_Sniper_Impact);

             if (dis >= 7)
             {
                 h.CreateParticle_Impact(posToAttack, dis * 0.15f + 1, Combat_spell.Particle_Amount._24);

             }
             else
             {
                 h.CreateParticle_Impact(posToAttack, dis * 0.1f + 1, Combat_spell.Particle_Amount._12);

             }*/

            target.Damage(new InfoDamage(calcDamage(Random.Range(1, 10 + 1)), caster));


            yield return null;
        }
    }

    public override void Cast_After_Void()
    {
        base.Cast_After();

        int cc = calc(20);

        Main_UI.Display_movingText_basicValue("+" + cc + "%", V.Color.green, V.player_entity.transform.position, V.icon_cc);

        caster.AddEffect(
            Effect.CreateEffect(SpellGestion.Get_Title(spellHolder.spell), Effect.effectType.criticalHitChance_oneUse, cc, 1, SpellGestion.Get_sprite(spellHolder.spell), Effect.Reduction_mode.never)
            );
    }
}


public class weapon_spear : spellEffect_Player
{
    public override IEnumerator Effect_before()
    {
        caster.Animation_DealDamage(targetedSquareWorldVector2);

        yield return null;
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            target.Damage(new InfoDamage(calcDamage(15), caster));

            caster.Add_pa(calcEFF(2));

            yield return null;
        }
    }
}

public class weapon_steal : spellEffect_Player
{
    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            target.Damage(new InfoDamage(calcDamage(15), caster));

            int amount = calc(1);

            target.Remove_pm(amount, true, true);

            caster.Add_pm(amount);

            yield return null;
        }
    }
}