using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warrior_Punch : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(true, true);
    }
    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            Vector2 posToAttack = F.GetPosNearTarget(target, CursorInfo.Instance.positionInWorld);

            Sprite sp = F.GetSpellSpriteAtPath_warrior("punch_anim");

            SoundManager.PlaySound(SoundManager.list.spell_warrior_powerfulPunch);
            SoundManager.PlaySound(SoundManager.list.spell2_warrior_ConflagrationPunch);

            float dmg = 10;

            target.Damage(new InfoDamage(calcDamage(dmg), caster));

            if (F.IsFaceRight_entity(caster.CurrentPosition_string, pos))
            {
                spellHolder.StartCoroutine(spellHolder.Anim_Punch(sp, posToAttack + new Vector2(-0.55f, 0), true, 1, 1.4f));
            }
            else
            {
                spellHolder.StartCoroutine(spellHolder.Anim_Punch(sp, posToAttack + new Vector2(0.55f, 0), false, 1, 1.4f));
            }
        }

        yield return null;
    }
}

public class warrior_spent : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        float accumulation = V.player_entity.CollectStr(Effect.effectType.accumulation);

        float div = 10;

        int nbAttack = Mathf.FloorToInt(accumulation / div);

        for (int i = 0; i < nbAttack; i++)
        {
            List<Monster> aliveEntity = new List<Monster>(AliveEntity.listMonster);

            int o = 0;
            while (o < aliveEntity.Count)
            {
                if (aliveEntity[o].IsDead())
                    aliveEntity.RemoveAt(o);
                else
                    o++;
            }

            if (aliveEntity.Count == 0) break;

            Monster monster = aliveEntity[Random.Range(0, aliveEntity.Count)];

            float baseDamage = calcDamage(10 + i * calcEFF(4));

            monster.Damage(new InfoDamage(baseDamage, caster));

            SoundManager.PlaySound(SoundManager.list.spell2_warrior_Meteore);

            spellAnimation.anim_simple("Meteore", V.CalcEntityDistanceToBody(monster), 0.2f + i * 0.05f);

            yield return new WaitForSeconds(0.3f);
        }
    }

    int id1, id2, id3;

    public override void actionToolbar_add(int toolbarId)
    {
        id3 = Player.event_startCombat.Add(startCombat);
        id1 = PlayerInfo.event_player_doDmg.Add(DamagingEnnemy);
        id2 = Monster.event_all_monster_die.Add(KillingEnnemy);
    }

    public override void actionToolbar_remove(int toolbarId)
    {
        Player.event_startCombat.Remove(id3);
        PlayerInfo.event_player_doDmg.Remove(id1);
        Monster.event_all_monster_die.Remove(id2);
    }

    public void DamagingEnnemy(Entity entity, float dmg)
    {
        AddAcumulation(entity, nbAccumulationGain);
    }

    public void KillingEnnemy(Entity entity)
    {
        AddAcumulation(entity, 5);
    }

    public void startCombat()
    {
        nbAccumulationGain = 1;
        AddAcumulation(null, 0);
    }

    public static int nbAccumulationGain;

    public static void AddAcumulation(Entity target, int str)
    {
        if (V.player_entity.ContainEffect_byTitle(SpellGestion.Get_Title(SpellGestion.List.warrior_spent))) { return; }

        V.player_entity.AddEffect(Effect.CreateEffect(Effect.effectType.accumulation, str, 3, Resources.Load<Sprite>("Image/Sort/warrior/Accumulation"), Effect.Reduction_mode.never));
        if (target != null && str > 0)
            WarriorParticleManagement.CreatePower(V.CalcEntityDistanceToBody(target), str);
    }
}

public class warrior_execution : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(true, true);
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            SoundManager.PlaySound(SoundManager.list.spell2_warrior_Execution);

            spellAnimation.anim_simple("Execution", V.CalcEntityDistanceToBody(target), 1);

            yield return new WaitForSeconds(1.3f);

            float percentageMissHealt = ((float)target.Info.Life_max - (float)target.Info.Life) / (float)target.Info.Life_max;

            float amount = Mathf.Clamp((percentageMissHealt / 0.7f) * 25, 5, 25);

            float originalDamage = target.Damage(new InfoDamage(calcDamage(amount), caster));

            bool execute = target.IsDead();

            if (execute)
            {
                float nextDmg = originalDamage * ((float)calcDEX(50) / 100);

                float scale = nextDmg / originalDamage;

                scale = Mathf.Clamp(scale, 0.6f, 2);

                do
                {
                    List<Monster> monsterToDamage = new List<Monster>();

                    foreach (Monster m in AliveEntity.listMonster)
                    {
                        if (!m.IsDead())
                            monsterToDamage.Add(m);
                    }

                    bool contain = monsterToDamage.Count > 0;

                    foreach (Monster m in monsterToDamage)
                    {
                        spellAnimation.anim_simple("Execution", V.CalcEntityDistanceToBody(m), scale);
                    }

                    if (contain)
                    {
                        SoundManager.PlaySound(SoundManager.list.spell2_warrior_Execution);

                        yield return new WaitForSeconds(1.3f);
                    }

                    execute = false;

                    foreach (Monster m in monsterToDamage)
                    {
                        m.Damage(new InfoDamage(nextDmg, caster));

                        if (m.IsDead()) execute = true;
                    }
                } while (execute);
            }
        }

        yield return null;
    }
}

public class warrior_attack : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(true, true);
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            V.player_entity.AddEffect(
                Effect.CreateEffect(Effect.effectType.resistance, 33, 1, null, Effect.Reduction_mode.startTurn)
                );

            target.Damage(new InfoDamage(calcDamage(13), caster));
        }

        yield return null;
    }
}


public class warrior_heal : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        spellAnimation.anim_simple("Heal", V.CalcEntityDistanceToBody(V.player_entity));

        List<Invocation> ls = new List<Invocation>();

        foreach (Entity e in AliveEntity.list)
        {
            if (e is Invocation invoc)
            {
                if (invoc.InvocType == Invocation.invocType.totem)
                {
                    ls.Add(invoc);
                }
            }
        }

        yield return new WaitForSeconds(0.25f);

        V.player_entity.Heal(new InfoHeal(calcRES(30)));

        int i = 0;
        while (i < ls.Count)
        {
            ls[i].Heal(new InfoHeal(calcRES(15)));

            i++;
        }

        yield return null;
    }
}


public class warrior_os : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        spellAnimation.anim_simple("Os", V.CalcEntityDistanceToBody(target), 1.5f);

        SoundManager.PlaySound(SoundManager.list.spell2_warrior_os);

        yield return new WaitForSeconds(0.2f);

        V.player_entity.Damage(new InfoDamage(V.player_info.Life_max * 0.2f, caster));

        int amount = calcDEX(1);

        Effect_spike.AddSpike(amount);

        yield return null;
    }
}


public class warrior_earthTotem : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }

    public override IEnumerator Cast_After()
    {
        yield return null;

        Invocating.CreateEntity(new CreaterInfoEarthTotem(info.targetedSquare, calcRES(50), calcDamage(10)));
    }
}

public class warrior_rockThrow : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        V.player_info.AddArmor(calcRES(20));

        Effect_spike.AddSpike(calcDEX(1));

        SoundManager.PlaySound(SoundManager.list.spell2_warrior_RockThrow);

        CustomEffect_Spike.create(V.CalcEntityDistanceToBody(V.player_entity));

        yield return null;
    }
}

public class warrior_Double : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, false);
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        Effect.Warrior_AddPower(Effect.Get_power());

        V.player_entity.AddEffect(
            Effect.CreateEffect(Effect.effectType.Warrior_fatigue, 0, 2, holderSprite, Effect.Reduction_mode.startTurn)
            );

        yield return null;
    }
}


public class warrior_spikeAttack : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }

    float ratio = -1;

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        spellAnimation.anim_simple("SpikeAttack", V.CalcEntityDistanceToBody(target), 1);

        if (TargetForDamage(target) && V.player_info.spike > 0)
        {
            ratio = calcDEX(100) / 100;

            spike.dmgEffect *= ratio;

            Action_spell_info_player info = new Action_spell_info_player();

            info.spell = Spell.Create(SpellGestion.List.spike);
            info.caster = caster;
            info.listTarget = new List<Entity>() { target };
            info.targetedSquare = target.CurrentPosition_string;

            Action_spell.Add(info);
        }

        yield return null;
    }

    public override IEnumerator Cast_After()
    {
        spike.dmgEffect /= ratio;

        yield return null;
    }
}

public class warrior_armor : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, false);
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        int amount = Mathf.CeilToInt(Effect.Get_power() / 2);

        Effect.Warrior_RemovePower(amount, true, "");

        V.player_info.AddArmor(amount);

        yield return null;
    }
}


public class warrior_endurance : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }

    public static int healAmount;

    public override IEnumerator Cast_After()
    {
        spellAnimation.anim_simple("Endurance", V.CalcEntityDistanceToBody(caster));

        SoundManager.PlaySound(SoundManager.list.spell2_warrior_endurance);

        yield return new WaitForSeconds(0.45f);

        int amount = calcDEX(3);

        V.player_entity.AddEffect(Effect.CreateEffect(Effect.effectType.boost_pa, amount, 1, holderSprite, Effect.Reduction_mode.endTurn)
            );

        V.player_entity.Add_pa(amount, true);

        yield return base.Cast_After();
    }

    public static void Application()
    {
        if (!V.player_entity.ContainEffect_byTitle(SpellGestion.Get_Title(SpellGestion.List.warrior_endurance)))
            return;

        V.player_entity.Heal(new InfoHeal(healAmount));
    }
}

public class warrior_shield : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }

    public override IEnumerator Cast_After()
    {
        V.player_info.AddArmor(calc(20));

        return base.Cast_After();
    }
}

public class warrior_whip : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(true, true);
    }
    public override IEnumerator Effect_before()
    {
        caster.Animation_DealDamage(targetedSquare);

        yield return null;
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            Vector2 tilePos = F.ConvertToWorldVector2(pos);

            target.Damage(new InfoDamage(calcDamage(Random.Range(10, 14 + 1)), caster));

            if (Effect.IsDefenseMajoritary())
            {
                Vector2 additional = new Vector2(-0.55f, 1.2f);

                if (F.IsFaceRight_entity(caster.CurrentPosition_string, pos))
                {
                    spellHolder.StartCoroutine(spellHolder.Anim_Whip(V.whip_animation, tilePos + additional, true, 1, 1.2f));
                }
                else
                {
                    additional = new Vector2(0.55f, 1.2f);

                    spellHolder.StartCoroutine(spellHolder.Anim_Whip(V.whip_animation, tilePos + additional, false, 1, 1.2f));
                }

                SoundManager.PlaySound(SoundManager.list.spell_warrior_flash);

                yield return new WaitForSeconds(0.3f);

                SoundManager.PlaySound(SoundManager.list.spell_warrior_whip_push);

                spellHolder.CreateParticle_Impact(tilePos + additional * 0.5f, 1, Spell.Particle_Amount._8);

                target.Push(calc(2), caster);
            }
            else if (Effect.IsPowerMajoritary())
            {

                spellHolder.CreateParticle_Impact(target.transform.position + new Vector3(0, 0.5f, 0), 1, Spell.Particle_Amount._12);

                spellHolder.StartCoroutine(spellHolder.Anim_Lasso(target));

                SoundManager.PlaySound(SoundManager.list.spell_warrior_whip_attract);

                yield return new WaitForSeconds(0.3f);

                SoundManager.PlaySound(SoundManager.list.spell_warrior_flash);

                target.Attract(calc(2), caster);
            }
            else
            {

                spellHolder.CreateParticle_Impact(tilePos, 1, Spell.Particle_Amount._8);
            }
        }

        yield return null;
    }
}

public class warrior_divineSword : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(true, true);
    }

    public List<Entity> listTarget = new List<Entity>();

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
            listTarget.Add(target);

        yield return null;
    }

    public override IEnumerator Cast_After()
    {
        SoundManager.PlaySound(SoundManager.list.spell2_warrior_DivineSword);

        foreach (var monster in listTarget)
        {
            spellAnimation.anim_simple("DivineSword", V.CalcEntityDistanceToBody(monster), 0.8f);

        }

        yield return new WaitForSeconds(0.2f);

        float dmg = calcDamage(8);

        V.player_info.AddArmor(5 * listTarget.Count);

        foreach (var monster in listTarget)
        {
            monster.Damage(new InfoDamage(dmg, caster));
        }

        yield return new WaitForSeconds(0.2f);

        foreach (var monster in listTarget)
        {
            spellAnimation.anim_simple("DivineSword", V.CalcEntityDistanceToBody(monster), 1.2f);
        }

        yield return new WaitForSeconds(0.2f);

        dmg = calcDamage(15);

        foreach (var monster in listTarget)
        {
            monster.Damage(new InfoDamage(dmg, caster));

        }
    }
}

public class warrior_flash : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, false);
    }

    public override IEnumerator Effect_before()
    {
        SoundManager.PlaySound(SoundManager.list.spell_warrior_flash);

        spellHolder.StartCoroutine(spellHolder.Anim_Flash(holderSprite, V.CalcEntityDistanceToBody(caster), 1.5f, !Effect.IsPowerMajoritary()));

        Effect.Warrior_Alternate();

        yield return null;
    }
}

public class warrior_bleeding : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(true, true);
    }
    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            target.Damage(new InfoDamage(calcDamage(Random.Range(5, 6 + 1)), caster));
            /*
                        Effect e = Effect.AddBleeding(target, calc(5));

                        h.StartCoroutine(h.Anim_PopUpBig(holderSprite, V.CalcEntityDistanceToBody(target)));

                        AnimEndless_Particle a = AnimEndless_Particle.Create(target, V.Entity_DistanceToBody_Vector3 + new Vector3(0, 0.3f, 0), Combat_spell.Reference.CreateParticle_Endless_Bleeding(Vector2.zero, 1.2f), AnimEndless_Particle.AnimationExit.none, 1.5f);

                        a.LinkToEffect(e);

                        a.SetFollowMode(AnimEndlessStatic.FollowMode.instant);

                        a.AddCustomUpdate(() =>
                        {
                            Combat_spell.ModifyBurst(a.Particle_system, Mathf.Clamp(Mathf.CeilToInt(e.Str / 25), 1, 20));
                        });*/
        }

        yield return null;
    }
}

public class warrior_cudgel : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(true, true);
    }

    public override IEnumerator Effect_before()
    {
        SoundManager.PlaySound(SoundManager.list.spell_warrior_powerfulPunch);

        caster.Animation_DealDamage(targetedSquareWorldVector2);

        StartCoroutine(spellHolder.Anim_Cudgel(holderSprite, targetedSquareWorldVector2 + new Vector2(-0.55f, 1.4f), true, 1.5f));

        yield return new WaitForSeconds(0.2f);
    }

    int nb = 0;

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            Vector2 posAttack = V.CalcEntityDistanceToBody(target);

            target.Damage(new InfoDamage(calcDamage(Random.Range(14, 16 + 1)), caster));

            spellHolder.CreateParticle_Impact(posAttack);

            nb++;
        }

        yield return null;
    }

    public override IEnumerator Cast_After()
    {
        float heal = 0;

        int index = 0;

        Vector3 pos = V.CalcEntityDistanceToBody(caster);

        float pitch = 1;

        while (index < nb)
        {
            GameObject g = spellHolder.CreateParticle_Impact_Entering_Uncomplete_Static(pos, 1.2f, Spell.Particle_Amount._12);

            g.transform.eulerAngles = new Vector3(0, 0, (pitch - 1) * 100);

            //h.CreateParticle_Bonus(pos, 1, Combat_spell.Particle_Amount._24);
            SoundManager.PlaySound(SoundManager.list.spell_warrior_divineSword_gaininigPower, pitch);

            pitch += 0.2f;

            heal += calc(5);

            index++;
            if (index < nb)
                yield return new WaitForSeconds(0.2f);
        }

        V.player_entity.Heal(new InfoHeal(heal));

        yield return null;
    }
}

public class warrior_focus : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }
    public override IEnumerator Effect_before()
    {
        caster.Animation_DealDamage(targetedSquareWorldVector2);

        Effect.Warrior_AddResistance(calc(10));

        yield return null;
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            Effect e = Effect.CreateEffect(Effect.effectType.focus, calc(100), 0, holderSprite, Effect.Reduction_mode.never);

            e = target.AddEffect(e);

            spellHolder.StartCoroutine(spellHolder.Anim_PopUpBig(holderSprite, V.CalcEntityDistanceToBody(target)));

            AnimEndless_Render a = AnimEndless_Render.Create(target, V.Entity_DistanceToBody_Vector3, holderSprite, AnimEndless_Render.AnimationExit.normal, 1.5f);

            a.LinkToEffect(e);

            a.setIdleAnimation(AnimEndless_Render.IdleAnim.size);

            a.SetFollowMode(AnimEndlessStatic.FollowMode.instant);
        }

        yield return null;
    }
}

public class warrior_offensivePosture : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }

    public override IEnumerator Effect_before()
    {
        Effect.Warrior_Focus_Power();

        Main_UI.Display_movingText_basicValue(V.IsFr() ? "Posture offensive" : "Offensive posture", V.Color.green, caster.transform.position);

        V.player_entity.Add_pa(calc(1));

        caster.AddEffect(
            Effect.CreateCustomEffect(calc(1), holderSprite, Effect.Reduction_mode.startTurn, SpellGestion.Get_Title(SpellGestion.List.warrior_offensivePosture), new List<Effect>
            {
                Effect.CreateEffectForCustom(Effect.effectType.additionalSpellArea,calc(1)),
                Effect.CreateEffectForCustom(Effect.effectType.effectPercentage,calc(30))
            }
            ));


        Vector2 pos = V.CalcEntityDistanceToBody(caster);

        spellHolder.StartCoroutine(spellHolder.Anim_PopUpBig(holderSprite, pos));

        spellHolder.CreateParticle_Bonus(pos, 1.1f, Spell.Particle_Amount._12);

        yield return null;
    }
}

public class warrior_defensivePosture : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }
    public override IEnumerator Effect_before()
    {
        Effect.Warrior_Focus_Defense();

        int armoreGained = V.player_info.AddArmor(calc(25));

        Vector2 pos = V.CalcEntityDistanceToBody(caster);

        spellHolder.StartCoroutine(spellHolder.Anim_PopUpBig(holderSprite, pos));

        spellHolder.CreateParticle_Bonus(pos, 1.1f, Spell.Particle_Amount._12);

        Effect.Warrior_AddResistance(armoreGained);

        yield return null;
    }
}

public class warrior_strength : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }
    public override IEnumerator Effect_before()
    {
        SoundManager.PlaySound(SoundManager.list.spell2_warrior_Strenght);

        spellAnimation.anim_simple("Strenght", V.CalcEntityDistanceToBody(caster), 1);

        yield return new WaitForSeconds(0.4f);

        Main_UI.Display_movingText_basicValue(SpellGestion.Get_Title(SpellGestion.List.warrior_strength), V.Color.green, caster.transform.position);

        caster.AddEffect(
            Effect.CreateEffect(Effect.effectType.additionalSpellCast, calcDEX(30), 1, V.strength_red, Effect.Reduction_mode.startTurn)
            );

        Vector2 pos = V.CalcEntityDistanceToBody(caster);

        spellHolder.CreateParticle_Bonus(pos, 1.1f, Spell.Particle_Amount._12);

        yield return null;
    }
}

public class warrior_spike : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }

    public override IEnumerator Effect_before()
    {
        V.player_info.AddArmor(calc(15));

        Effect_spike.AddSpike(calc(2));

        yield return null;
    }
}

public class warrior_defense : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }
    public override IEnumerator Effect_before()
    {
        bool maxLife = V.player_info.IsLifeMax();

        Effect.Warrior_AddResistance(calc(10));


        Vector2 pos = V.CalcEntityDistanceToBody(caster);

        spellHolder.StartCoroutine(spellHolder.Anim_PopUpBig(holderSprite, pos));

        spellHolder.CreateParticle_Bonus(V.CalcEntityDistanceToBody(caster), 1.1f, maxLife ? Spell.Particle_Amount._36 : Spell.Particle_Amount._12);


        if (!maxLife)
        {
            caster.Heal(new InfoHeal(calc(20)));
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
            Effect.Warrior_AddResistance(calc(10));
        }

        yield return null;
    }
}

public class warrior_jump : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }

    public float speed = 0.3f;

    public override IEnumerator Effect_before()
    {

        Vector3 rotation = Vector3.zero;

        if (!F.IsFaceRight_entity(caster.CurrentPosition_string, targetedSquare))
        {
            rotation = new Vector3(0, 180, 0);
        }

        int distance = F.DistanceBetweenTwoPos(V.player_entity.CurrentPosition_string, targetedSquare);

        V.player_entity.PlayAnimation("jump");

        V.player_entity.LockRender(1, new SpriteRotaInfo(V.player_entity.player_jumping, rotation, Vector3.zero));

        SoundManager.PlaySound(SoundManager.list.spell_warrior_jump_jump);

        yield return new WaitForSeconds(0.2f);

        CustomEffect_ImpactRock.create(V.player_entity.transform.position, 3, 0.5f);

        yield return new WaitForSeconds(speed - 0.1f);

        V.player_entity.PlayAnimation("landing");

        F.TeleportEntity(targetedSquare, caster);

        yield return new WaitForSeconds(0.2f);

        SoundManager.PlaySound(SoundManager.list.spell_warrior_jump_landing);

        int nb = 7 + distance * 3;

        CustomEffect_ImpactRock.create(V.player_entity.transform.position, nb);

        spellHolder.CreateParticle_Leaf(F.ConvertToWorldVector2(targetedSquare), 1);
    }

    int nb = 0;

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            nb++;
            target.Damage(new InfoDamage(calcDamage(13), caster));
        }

        yield return null;
    }

    public override IEnumerator Cast_After()
    {
        V.player_info.AddArmor(calcRES(10) * nb);

        yield return null;
    }
}

public class warrior_aspiration : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }
    public int nb = 0;

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            nb++;

            int effectCalc = calc(20);

            target.AddEffect(
                Effect.CreateEffect(Effect.effectType.reductionPower, effectCalc, calc(2), holderSprite, Effect.Reduction_mode.endTurn)
                );

            Main_UI.Display_movingText_basicValue("-" + effectCalc + "% dmg", V.Color.red, V.CalcEntityDistanceToBody(target));

            spellHolder.StartCoroutine(spellHolder.Anim_PopUpBigSpinning(holderSprite, V.CalcEntityDistanceToBody(target), 1.2f));
        }

        yield return null;
    }

    public override IEnumerator Cast_After()
    {
        yield return new WaitForSeconds(0.5f);

        spellHolder.CreateParticle_Bonus(V.CalcEntityDistanceToBody(caster));

        caster.Heal(new InfoHeal(calc(10 * nb)));

        yield return null;
    }
}

public class warrior_spikyPosture : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }
    public override IEnumerator Effect_before()
    {
        int str = calc(15);
        int dur = calc(2);

        Effect_spike.AddSpike(str);

        caster.AddEffect(
            Effect.CreateCustomTxtEffect(SpellGestion.Get_Title(SpellGestion.List.warrior_spikyPosture), SpellGestion.Get_Description(SpellGestion.List.warrior_spikyPosture), dur, holderSprite, Effect.Reduction_mode.startTurn)
            );

        damageEpineReduction = calcF(0.25f);

        range = 2;

        Vector2 pos = V.CalcEntityDistanceToBody(caster);

        spellHolder.StartCoroutine(spellHolder.Anim_PopUpBig(holderSprite, pos));

        spellHolder.CreateParticle_Bonus(pos, 1.1f, Spell.Particle_Amount._12);

        yield return null;
    }

    public static float damageEpineReduction = 0.25f;

    public static int range = 2;

    public static void ApplySpikyPosture(int dmg)
    {
        foreach (Entity e in AliveEntity.list)
        {
            if (F.DistanceBetweenTwoPos(V.player_entity, e) <= range && e != V.player_entity)
            {
                Spell.Reference.StartCoroutine(Spell.Reference.Anim_SpikePosture(Resources.Load<Sprite>("Image/UI/icon_spike"), e.transform.position, 1));

                e.Damage(new InfoDamage(dmg * damageEpineReduction, V.player_entity));
            }
        }
    }
}

public class warrior_attraction : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(true, true);
    }
    public int nbAttracted;

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (TargetForDamage(target))
        {
            target.Damage(new InfoDamage(calcDamage(5), caster));

            target.Attract(3, caster);

            Vector3 posV3 = V.CalcEntityDistanceToBody(target);

            spellHolder.StartCoroutine(spellHolder.Anim_PopUpBig(V.attraction_center, posV3, 1, 0.8f));

            spellHolder.StartCoroutine(spellHolder.Anim_Projectile_DoMove(target, V.attraction_arrow, V.CalcEntityDistanceToBody(caster)));

            nbAttracted++;
        }

        yield return null;
    }

    public override IEnumerator Cast_After()
    {
        yield return new WaitForSeconds(0.5f);

        caster.Add_pa(calc(1) * nbAttracted);

        V.player_info.AddArmor(calc(5) * nbAttracted);
    }
}
