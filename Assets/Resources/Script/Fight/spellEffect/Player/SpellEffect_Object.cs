using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class object_healtPotion : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }

    public override IEnumerator Effect_before()
    {
        SoundManager.PlaySound(SoundManager.list.object_healthPotion);

        caster.Heal(new InfoHeal(calc(0.25f * V.player_info.Life_max)));

        Vector2 posAnim = caster.transform.position + new Vector3(0, 0.5f, 0); //+ new Vector2(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f) + 0.5f);

        StartCoroutine(
            spellHolder.Anim_PopDown(holderSprite, posAnim, 0.7f, 2)
            );

        spellHolder.CreateParticle_Impact_Entering(posAnim, 1.5f, Spell.Particle_Amount._24, Spell.Particle_Color.blood);

        yield return null;
    }
}

public class object_blood : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }

    public override IEnumerator Effect_before()
    {
        int amount = calc(50);

        V.player_entity.AddEffect(
            Effect.CreateEffect(Effect.effectType.lifeSteal, amount, 1, holderSprite, Effect.Reduction_mode.endTurn)
            );

        yield return null;
    }
}

public class object_spike : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }

    public override IEnumerator Effect_before()
    {
        int amount = calc(1);

        Effect_spike.AddSpike(amount);

        Vector2 position = V.CalcEntityDistanceToBody(caster);

        CustomEffect_Spike.create(position);

        CustomEffect_EndlessSpike.AddDisableTime(1);

        yield return null;
    }
}


public class object_energeticDrink : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }

    public int nb = 0;

    public bool ShouldWaitAttraction;

    public override IEnumerator Effect_before()
    {
        SoundManager.PlaySound(SoundManager.list.object_energeticDrink);

        ShouldWaitAttraction = false;

        foreach (Entity e in AliveEntity.list)
        {
            if (e == caster)
                continue;

            if (F.DistanceBetweenTwoPos(caster.CurrentPosition_string, e.CurrentPosition_string) <= 3 && F.IsInLine(caster.CurrentPosition_string, e.CurrentPosition_string))
            {
                string pos = e.Attract(3, V.player_entity);

                if (F.DistanceBetweenTwoPos(caster.CurrentPosition_string, pos) <= 1)
                {
                    nb++;
                }

                if (e.CurrentPosition_string != pos)
                {
                    ShouldWaitAttraction = true;

                    Vector3 posV3 = V.CalcEntityDistanceToBody(e.CurrentPosition_string);

                    spellHolder.StartCoroutine(spellHolder.Anim_PopUpBig(V.attraction_center, posV3, 1, 0.8f));

                    spellHolder.StartCoroutine(spellHolder.Anim_Projectile_DoMove(e, V.attraction_arrow, V.CalcEntityDistanceToBody(caster)));
                }
            }
        }

        if (ShouldWaitAttraction)
            yield return new WaitForSeconds(0.7f);
    }

    public override IEnumerator Cast_After()
    {

        int power = 0;

        int index = 0;

        Vector3 pos = V.CalcEntityDistanceToBody(caster);

        float pitch = 1;

        StartCoroutine(
    spellHolder.Anim_PopDown(holderSprite, pos, 0.7f, 2)
    );

        while (index < nb)
        {

            GameObject g = spellHolder.CreateParticle_Impact_Entering_Uncomplete_Static(pos, 1.2f, Spell.Particle_Amount._12);

            g.transform.eulerAngles = new Vector3(0, 0, (pitch - 1) * 100);

            //h.CreateParticle_Bonus(pos, 1, Combat_spell.Particle_Amount._24);
            SoundManager.PlaySound(SoundManager.list.spell_warrior_divineSword_gaininigPower, pitch);

            pitch += 0.2f;

            power += calc(20);

            index++;
            if (index < nb)
                yield return new WaitForSeconds(0.2f);
        }

        Main_UI.Display_movingText_basicValue("+" + power + "%", V.Color.green, V.player_entity.transform.position);

        V.player_entity.AddEffect(
            Effect.CreateEffect(Effect.effectType.power, power, calc(2), holderSprite, Effect.Reduction_mode.endTurn)
            );

        yield return null;
    }
}


public class object_bloodShield : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }
    public override IEnumerator Effect_before()
    {

        SoundManager.PlaySound(SoundManager.list.object_BloodShield_First);

        V.player_info.AddArmor(calc(35));

        Vector2 posAnim = caster.transform.position + new Vector3(0, 0.5f, 0); //+ new Vector2(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f) + 0.5f);

        StartCoroutine(
            spellHolder.Anim_PopUpBig(holderSprite, posAnim)
            );

        spellHolder.CreateParticle_Bonus(posAnim);

        yield return null;
    }
}

public class object_fieldGlass : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }
    public override IEnumerator Effect_before()
    {
        int dur = calc(1);

        SoundManager.PlaySound(SoundManager.list.object_FieldGlass);

        Vector2 posAnim = caster.transform.position + new Vector3(0, 0.5f, 0); //+ new Vector2(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f) + 0.5f);

        StartCoroutine(
            spellHolder.Anim_PopDown(holderSprite, posAnim, 1, 2)
            );

        spellHolder.CreateParticle_Leaf(posAnim);

        spellHolder.CreateParticle_Impact_Entering(posAnim, 2, Spell.Particle_Amount._24, Spell.Particle_Color.yellow);

        Effect e = caster.AddEffect(
            Effect.CreateCustomEffect(dur, SpellGestion.Get_sprite(SpellGestion.List.object_fieldGlass), Effect.Reduction_mode.startTurn, SpellGestion.Get_Title(SpellGestion.List.object_fieldGlass), new List<Effect>
            {
                Effect.CreateEffectForCustom(Effect.effectType.po,calc(2))
            })
            );

        AnimEndless_Particle an = AnimEndless_Particle.Create(V.player_entity, new Vector3(0, 0.5f, 0),
            spellHolder.CreateParticle_Circle_Endless(Vector2.zero, 1, Spell.Particle_Amount._6, Spell.Particle_Color.yellow)
            , AnimEndless_Particle.AnimationExit.FadeOut_particle, 1, 1);

        an.LinkToEffect(e);

        yield return null;
    }
}

public class object_sacrifice : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }
    public override IEnumerator Effect_before()
    {
        Vector2 posAnim = caster.transform.position + new Vector3(0, 0.5f, 0); //+ new Vector2(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f) + 0.5f);

        StartCoroutine(
            spellHolder.Anim_PopDown(holderSprite, posAnim, 1, 2)
            );

        spellHolder.CreateParticle_Leaf(posAnim);

        spellHolder.CreateParticle_BloodLoss(posAnim, 1, Spell.Particle_Amount._24);

        spellHolder.CreateParticle_Impact_Entering(posAnim, 2, Spell.Particle_Amount._24, Spell.Particle_Color.blood);

        SoundManager.PlaySound(SoundManager.list.object_Sacrifice);

        int nb = calc(30);

        Main_UI.Display_movingText_basicValue("+" + nb + "%", V.Color.green, V.player_entity.transform.position);

        V.player_entity.AddEffect(
            Effect.CreateEffect(Effect.effectType.power, nb, calc(2), holderSprite, Effect.Reduction_mode.endTurn)
            );
        /*
        Combat_Effect e = caster.AddEffect(
            Effect.CreateEffect(Effect.effectType.power, calc(30), calc(2), holderSprite, "", Effect.Reduction_mode.startTurn)).CombatEffect_Holder;

        Animation_Endless_Particle.Create(V.player_entity, new Vector3(0, 0.5f, 0),
            h.CreateParticle_Circle_Endless(Vector2.zero, 1.5f, Combat_spell.Particle_Amount._48, Combat_spell.Particle_Color.blood)
            , Animation_Endless.Erase_Type.FadeOut_particle, 1, 1).LinkToEffect(e);
        */
        int damage = F.CalculateDamageThatCannotKill(caster, caster.Info.Life * 0.3f);

        caster.Damage(new InfoDamage(damage, caster));

        yield return null;
    }
}

public class object_watch : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }

    public override IEnumerator Effect_before()
    {
        if (Spell.lastSpellLaunch == null)
            Main_UI.Display_movingText_basicValue(V.IsFr() ? "Aucun sort lancé" : "No spell launch", V.Color.red, caster.transform.position);
        else if (Spell.lastSpellLaunch.IsOffCooldown())
            Main_UI.Display_movingText_basicValue(V.IsFr() ? "Le dernier sort lancé n'est pas en cooldown" : "Last spell launched is not cd", V.Color.red, caster.transform.position);
        else
        {

            Vector2 posAnim = caster.transform.position + new Vector3(0, 0.5f, 0); //+ new Vector2(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f) + 0.5f);

            StartCoroutine(
                spellHolder.Anim_PopUpBig(holderSprite, posAnim)
                );

            spellHolder.CreateParticle_Bonus(posAnim);

            spellHolder.CreateParticle_Leaf(caster.transform.position, UnityEngine.Random.Range(1.6f, 2));

            Vector3 posParticle = Spell.lastSpellLaunch.transform.position;

            Spell.lastSpellLaunch.SetCooldown(0);

            SoundManager.PlaySound(SoundManager.list.object_watch);

            spellHolder.CreateParticle_Impact_Entering_Uncomplete_Static(posParticle, 1.2f, Spell.Particle_Amount._36, Spell.Particle_Color.green);

            for (int i = 0; i < 2; i++)
            {
                yield return new WaitForSeconds(0.15f);

                spellHolder.CreateParticle_Impact_Entering_Uncomplete_Static(posParticle, 1.2f, Spell.Particle_Amount._36, Spell.Particle_Color.green);
            }
        }
        yield return null;
    }
}

public class object_liberation : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }
    public override IEnumerator Effect_before()
    {
        Vector2 posAnim = caster.transform.position + new Vector3(0, 0.5f, 0); //+ new Vector2(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f) + 0.5f);

        StartCoroutine(
            spellHolder.Anim_PopUpBig(holderSprite, posAnim)
            );

        spellHolder.CreateParticle_Impact(caster.transform.position + new Vector3(0, 0.5f, 0), 1.5f, Spell.Particle_Amount._24);
        spellHolder.CreateParticle_Leaf(caster.transform.position, UnityEngine.Random.Range(1.6f, 2));
        spellHolder.CreateParticle_Leaf(caster.transform.position, UnityEngine.Random.Range(1.6f, 2));

        SoundManager.PlaySound(SoundManager.list.object_Liberation);

        yield return null;
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (!TargetForAll(target))
            yield return null;

        (int x, int y) diff = F.DistanceBetweenTwoPos_xy(caster.CurrentPosition_string, target.CurrentPosition_string);

        diff = (diff.x * 3, diff.y * 3);

        string newPos = F.AdditionPos(caster.CurrentPosition_string, F.ConvertToString(diff.x, diff.y));

        StartCoroutine(spellHolder.Anim_Projectile(caster, V.object_liberation_graphic, F.ConvertToWorldVector2(newPos), false, 1, 2));

        spellHolder.CreateParticle_Impact(V.CalcEntityDistanceToBody(target));

        target.Push(calc(2), caster);

        yield return null;
    }
}

public class object_rush : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }
    public override IEnumerator Effect_before()
    {
        Vector2 posAnim = V.CalcEntityDistanceToBody(caster); //+ new Vector2(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f) + 0.5f);

        StartCoroutine(
            spellHolder.Anim_PopUpBig(holderSprite, posAnim)
            );

        SoundManager.PlaySound(SoundManager.list.object_Rush_First);

        spellHolder.CreateParticle_Impact_Entering(posAnim, 2, Spell.Particle_Amount._16, Spell.Particle_Color.green);
        spellHolder.CreateParticle_Impact_Entering(posAnim, 2, Spell.Particle_Amount._16, Spell.Particle_Color.green);


        int nbPa = calc(2), nbPm = calc(1);

        caster.Add_pa(nbPa, false);
        caster.Add_pm(nbPm, false);


        yield return null;
    }
}

public class object_floor : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }
    public override IEnumerator Effect_before()
    {
        Vector2 posAnim = V.CalcEntityDistanceToBody(caster);

        StartCoroutine(
            spellHolder.Anim_PopUpBig(holderSprite, posAnim)
            );

        int nb = calc(4);

        caster.Add_pa(nb, true, nb >= 10 ? " 0_0" : "");

        SoundManager.PlaySound(SoundManager.list.object_Floor);

        Spell.Reference.CreateParticle_Leaf(V.player_entity.transform.position, 1);

        spellHolder.CreateParticle_Raising(caster.transform.position, 1.5f, Spell.Particle_Amount._48, Spell.Particle_Color.yellow);

        yield return null;
    }
}

public class object_totem : spellEffect_Player
{
    public struct infoTotem
    {
        public bool activated;
        public int power, duration;

        public infoTotem(bool activated, int power, int duration)
        {
            this.activated = activated;
            this.power = power;
            this.duration = duration;
        }
    }

    public static infoTotem supInfo;

    public override IEnumerator Effect_before()
    {
        Vector2 posAnim = caster.transform.position + new Vector3(0, 0.5f, 0);

        StartCoroutine(
            spellHolder.Anim_PopUpBig(holderSprite, posAnim)
            );

        spellHolder.CreateParticle_Leaf(caster.transform.position, 1);

        SoundManager.PlaySound(SoundManager.list.object_Totem_Activating);

        spellHolder.CreateParticle_Bonus(posAnim, 1.3f, Spell.Particle_Amount._12);

        string title_effect = "totem";

        string description_effect = "Totem vous empeche de *bon mourrir *end";

        if (V.IsUk())
            description_effect = "Totem prevent you from *bon diyng *end";

        Main_UI.Display_movingText_basicValue("Totem", V.Color.green, caster.transform.position);

        AnimEndless_Render endlessAnimation = AnimEndless_Render.Create(caster, new Vector3(0, 0.5f, 0), holderSprite, AnimEndless_Render.AnimationExit.silent, 2, 1);

        endlessAnimation.SetSpecialId("totem");

        int nb = 1, dur = 1;

        Effect effect = caster.AddEffect(
            Effect.CreateCustomEffect(calc(dur), holderSprite, Effect.Reduction_mode.startTurn, title_effect, new List<Effect>
            {
                Effect.CreateEffectTxtForCustom(description_effect),
            }
        ));

        endlessAnimation.LinkToEffect(effect);

        supInfo = new infoTotem(false, nb, dur);

        yield return null;
    }
}

public class object_dice : spellEffect_Player
{
    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }

    public override IEnumerator Effect_before()
    {
        int cc = calc(33);

        Main_UI.Display_movingText_basicValue("+" + cc + "%", V.Color.green, V.player_entity.transform.position, V.icon_cc);

        caster.AddEffect(
            Effect.CreateEffect(Effect.effectType.criticalHitChance, cc, 1, holderSprite, Effect.Reduction_mode.startTurn)
            );

        Vector2 posAnim = caster.transform.position + new Vector3(0, 0.5f, 0); //+ new Vector2(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f) + 0.5f);

        StartCoroutine(
            spellHolder.Anim_PopDown(holderSprite, posAnim, 0.7f, 2)
            );

        spellHolder.CreateParticle_Impact_Entering(posAnim, 1.5f, Spell.Particle_Amount._24, Spell.Particle_Color.blood);

        yield return null;
    }
}