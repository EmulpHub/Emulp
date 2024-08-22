using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talent_empty : talentInfo { }

public class talent_warrior_powerReduction : talentInfo
{
    public override bool CanBeObtained()
    {
        return Origin.IsOrigin(Origin.Value.underground);
    }

    public override void Apply(float power)
    {
        base.Apply(power);

        V.player_entity.AddEffect(
            Effect.CreateEffect(title, Effect.effectType.warrior_reducePowerReduction, calcPower_int(10, power), 0, sp, Effect.Reduction_mode.permanent)
            , false);
    }

    public override void Remove()
    {
        base.Remove();

        V.player_entity.RemoveEffect_byTitle(title, true);
    }
}

public class talent_DeGauche : talentInfo
{
    public override void Apply(float power)
    {
        base.Apply(power);

        CombatSpell_VisualEffect.Add(title, new List<string> { "0_0", "0_1" }, delegate ()
         {
             return calcPower(0.2f, power);
         });
    }

    public override void Remove()
    {
        base.Remove();

        CombatSpell_VisualEffect.Remove(title);
    }
}

public class talent_Iccanobif : talentInfo
{
    int id;

    public override void Apply(float power)
    {
        base.Apply(power);

        effect = 5 * power;

        Monster.event_all_monster_remove_paOrPm.Add(Apply);
    }

    public override void Remove()
    {
        base.Remove();

        Monster.event_all_monster_remove_paOrPm.Remove(id);
    }

    public float effect;

    public void Apply(Entity e)
    {
        Main_UI.Display_movingText_basicValue("+" + Mathf.CeilToInt(effect) + "%", V.Color.green, V.player_entity.transform.position, Talent_Gestion.GetSprite(Talent_Gestion.talent.Iccanobif));

        V.player_entity.AddEffect(
            Effect.CreateEffect(Effect.effectType.effectPercentage, Mathf.CeilToInt(effect), 0, Talent_Gestion.GetSprite(Talent_Gestion.talent.Iccanobif), Effect.Reduction_mode.never)
            );

    }
}

public class talent_slower : talentInfo
{
    int id;

    public override void Apply(float power)
    {
        base.Apply(power);

        percentageLife = 10 * power / 100;

        id = Monster.event_all_monster_remove_paOrPm.Add(Apply);
    }

    public override void Remove()
    {
        base.Remove();

        Monster.event_all_monster_remove_paOrPm.Remove(id);
    }

    public float percentageLife;

    public void Apply(Entity e)
    {
        V.player_info.AddArmor(Mathf.CeilToInt(percentageLife * V.player_info.Life_max));
    }
}

public class talent_SpePower : talentInfo
{
    public override bool CanBeObtained()
    {
        return Origin.IsOrigin(Origin.Value.underground);
    }

    public override void Apply(float power)
    {
        base.Apply(power);

        V.player_entity.AddEffect(
            Effect.CreateEffect(title, Effect.effectType.power, calcPower_int(20, power), 0, sp, Effect.Reduction_mode.permanent)
            , false);
    }

    public override void Remove()
    {
        base.Remove();

        V.player_entity.RemoveEffect_byTitle(title, true);
    }
}

public class talent_SpeDefense : talentInfo
{

    public override bool CanBeObtained()
    {
        return Origin.IsOrigin(Origin.Value.underground);
    }
    public override bool Con()
    {
        return TreeSkill.PurchasedSkill.Contains(SpellGestion.List.warrior_spike) || TreeSkill.PurchasedSkill.Contains(SpellGestion.List.warrior_spikyPosture) || !Talent_Gestion.lockedTalent.Contains(Talent_Gestion.talent.InfiniteSpike);
    }

    public override void Apply(float power)
    {
        base.Apply(power);

        V.player_entity.AddEffect(
            Effect.CreateEffect(title, Effect.effectType.spikePercentage, calcPower_int(100, power), 0, sp, Effect.Reduction_mode.permanent)
            , false);
    }

    public override void Remove()
    {
        base.Remove();

        V.player_entity.RemoveEffect_byTitle(title, true);
    }
}

public class talent_Optimisation : talentInfo
{
    public override bool CanBeObtained()
    {
        return Origin.IsOrigin(Origin.Value.underground);
    }

    int id1;

    public override void Apply(float power)
    {
        base.Apply(power);

        V.player_entity.AddEffect(
            Effect.CreateCustomTxtEffect(title, description, 0, sp, Effect.Reduction_mode.permanent)
            , false);

        id1 = V.player_entity.event_turn_end.Add(applyEffect);
    }

    public override void Remove()
    {
        base.Remove();

        V.player_entity.RemoveEffect_byTitle(title, true);

        V.player_entity.event_turn_end.Remove(id1);

    }

    public static void applyEffect(Entity e)
    {
        float power = Effect.Get_power();
        float defense = Effect.Get_defense();

        if (defense <= 0 && power <= 0)
            return;

        if (Effect.IsPowerMajoritary())
        {
            foreach (Entity a in AliveEntity.list)
            {
                if (a == e)
                    continue;

                if (F.DistanceBetweenTwoPos(a, e) <= 2)
                {
                    a.Damage(new InfoDamage(power * 0.2f, V.player_entity));
                }
            }
        }
        else if (Effect.IsDefenseMajoritary())
        {
            V.player_entity.Heal(new InfoHeal(defense * 0.2f));
        }
    }
}

public class talent_Cycle : talentInfo
{
    public static float power;

    int id1;

    public override void Apply(float power)
    {
        base.Apply(power);

        V.player_entity.AddEffect(
            Effect.CreateCustomTxtEffect(title, description, 0, sp, Effect.Reduction_mode.permanent)
            , false);

        talent_Cycle.power = power;

        id1 = V.player_entity.event_turn_start.Add(StartTurn);
    }

    public override void Remove()
    {
        base.Remove();

        V.player_entity.RemoveEffect_byTitle(title, true);

        V.player_entity.event_turn_start.Remove(id1);
    }

    public static void StartTurn(Entity e)
    {
        bool GoodTurn = EntityOrder.id_turn == EntityOrder.id_turn_startCombat || (EntityOrder.id_turn - EntityOrder.id_turn_startCombat) % 2 == 0;

        if (GoodTurn)
        {
            V.player_entity.AddEffect(
                Effect.CreateEffect(Talent_Gestion.GetTitle(Talent_Gestion.talent.Cycle), Effect.effectType.effectPercentage, talentInfo.calcPower_int(50, talent_Cycle.power), 1, Talent_Gestion.GetSprite(Talent_Gestion.talent.Cycle), Effect.Reduction_mode.startTurn)
                );
        }
        else
        {
            V.player_entity.AddEffect(
    Effect.CreateEffect(Talent_Gestion.GetTitle(Talent_Gestion.talent.Cycle), Effect.effectType.power, -50, 1, Talent_Gestion.GetSprite(Talent_Gestion.talent.Cycle), Effect.Reduction_mode.startTurn)
    );
        }
    }
}

public class talent_BleedingMaster : talentInfo
{
    public override bool CanBeObtained()
    {
        return Origin.IsOrigin(Origin.Value.underground);
    }

    public float power;

    public override bool Con()
    {
        return TreeSkill.PurchasedSkill.Contains(SpellGestion.List.warrior_bleeding);
    }

    int id;

    public override void Apply(float power)
    {
        base.Apply(power);

        this.power = power;

        V.player_entity.AddEffect(
            Effect.CreateCustomTxtEffect(title, description, 0, sp, Effect.Reduction_mode.permanent)
            , false);
    }

    public override void Remove()
    {
        base.Remove();

        V.player_entity.RemoveEffect_byTitle(title, true);
    }

    public void EffectWhenBleedingDamage()
    {
        Effect.Warrior_AddResistance(calcPower_int(10, power));
    }
}

public class talent_InfiniteEffect : talentInfo
{
    public override bool CanBeObtained()
    {
        return Origin.IsOrigin(Origin.Value.underground);
    }
    public override void Apply(float power)
    {
        base.Apply(power);

        this.power = power;

        V.player_entity.AddEffect(
            Effect.CreateCustomTxtEffect(title, description, 0, sp, Effect.Reduction_mode.permanent)
            , false);
    }

    int id;

    public override void Remove()
    {
        base.Remove();

        V.player_entity.RemoveEffect_byTitle(title, true);
    }

    public float power;

    public void DoEffect()
    {
        string title = V.IsFr() ? "Effets infinie" : "Infinite effect";

        bool find = false;

        Effect e = V.player_entity.GetEffect_byTitle(title, ref find);

        bool ok = !find;

        float reste = 3;

        if (!ok)
        {
            float effect = e.Str;

            float max = 9999;
            reste = max - effect;

            ok = reste > 0;
        }

        if (ok && reste > 0)
            V.player_entity.AddEffect(
            Effect.CreateEffect(title, Effect.effectType.effectPercentage, (int)Mathf.Clamp(reste, 0, calcPower_int(3, power)), 0, Talent_Gestion.GetSprite(Talent_Gestion.talent.InfiniteEffect), Effect.Reduction_mode.never)
            );
    }
}

public class talent_InfiniteSpike : talentInfo
{
    public override bool CanBeObtained()
    {
        return Origin.IsOrigin(Origin.Value.underground);
    }
    public override void Apply(float power)
    {
        base.Apply(power);

        this.power = power;

        V.player_entity.AddEffect(
            Effect.CreateCustomTxtEffect(title, description, 0, sp, Effect.Reduction_mode.permanent)
            , false);
    }

    int id;

    public override void Remove()
    {
        base.Remove();

        V.player_entity.RemoveEffect_byTitle(title, true);
    }

    public float power;

    public void DoEffect()
    {
        Effect_spike.AddSpike(calcPower_int(3, power));

    }
}

public class talent_power : talentInfo
{
    public override void Apply(float power)
    {
        base.Apply(power);

        V.player_entity.AddEffect(
            Effect.CreateEffect(Talent_Gestion.GetTitle(Talent_Gestion.talent.power), Effect.effectType.power, calcPower_int(10, power), 0, sp, Effect.Reduction_mode.permanent)
            , false);
    }

    public override void Remove()
    {
        base.Remove();

        V.player_entity.RemoveEffect_byTitle(Talent_Gestion.GetTitle(Talent_Gestion.talent.power), true);
    }
}

public class talent_goodView : talentInfo
{
    int key;

    public override void Apply(float power)
    {
        base.Apply(power);

        key = statsForPlayer.Add(new List<(statsForPlayer.type, float val)>
        {
            (statsForPlayer.type.effect_onDistance_multiplicator, 10),
            (statsForPlayer.type.stats_po, 1),
        });

    }

    public override void Remove()
    {
        base.Remove();

        statsForPlayer.Remove(key);

        V.player_entity.RemoveEffect_byTitle(title, true);
    }
}

public class talent_cc : talentInfo
{
    int key;

    public override void Apply(float power)
    {
        base.Apply(power);

        key = statsForPlayer.Add(new List<(statsForPlayer.type, float val)>
        {
            (statsForPlayer.type.stats_cc_chance, calcPower_int(15, power)),
            (statsForPlayer.type.stats_ec, calcPower_int(25, power)),
        });

    }

    public override void Remove()
    {
        base.Remove();

        statsForPlayer.Remove(key);
    }
}

public class talent_heal : talentInfo
{
    int id;

    public override void Apply(float power)
    {
        base.Apply(power);

        healPower = power;

        id = Monster.event_all_monster_dmg.Add(healing);
    }

    public override void Remove()
    {
        base.Remove();

        Monster.event_all_monster_dmg.Remove(id);
    }

    static float healPower;

    public void healing(Entity e, float dmg)
    {
        float amount = calcPower(V.player_info.Life_max * 0.05f, healPower);

        V.player_entity.Heal(new InfoHeal(amount));
    }
}

public class talent_spikeDamage : talentInfo
{
    public override void Apply(float power)
    {
        base.Apply(power);

        V.player_entity.AddEffect(

            Effect.CreateEffect(title, Effect.effectType.spikeAdditionalDamage, calcPower_int(3, power), 0, sp, Effect.Reduction_mode.permanent)
            , false);
    }

    public override void Remove()
    {
        base.Remove();

        V.player_entity.RemoveEffect_byTitle(title, true);
    }
}

public class talent_armor : talentInfo
{
    int id1;

    public override void Apply(float power)
    {
        base.Apply(power);

        p = power;

        id1 = V.player_entity.event_turn_start.Add(apply);
    }

    public override void Remove()
    {
        base.Remove();

        V.player_entity.event_turn_start.Remove(id1);
    }

    static float p;

    public void apply(Entity e)
    {
        V.player_info.AddArmor(Mathf.CeilToInt(calcPower(V.player_info.Life_max * 0.1f, p)));
    }
}


public class talent_efficacity : talentInfo
{
    public override bool Con()
    {
        return Talent_Gestion.unlockedTalent.Count > 0;
    }

    public static float power;

    public override void Apply(float power)
    {
        base.Apply(power);

        talent_efficacity.power = power;
    }

    public static float DoEffect(int b)
    {
        return b * calcPower(2, talent_efficacity.power);
    }

}

public class talent_pa : talentInfo
{

    public override void Apply(float power)
    {
        base.Apply(power);

        V.player_entity.AddEffect(

            Effect.CreateEffect(title, Effect.effectType.boost_pa, calcPower_int(1, power), 0, sp, Effect.Reduction_mode.permanent)
            , false);
    }

    public override void Remove()
    {
        base.Remove();

        V.player_entity.RemoveEffect_byTitle(title, true);

    }
}


public class talent_pm : talentInfo
{

    public override void Apply(float power)
    {
        base.Apply(power);

        V.player_entity.AddEffect(

            Effect.CreateEffect(title, Effect.effectType.boost_pm, calcPower_int(1, power), 0, sp, Effect.Reduction_mode.permanent)
            , false);
    }

    public override void Remove()
    {
        base.Remove();

        V.player_entity.RemoveEffect_byTitle(title, true);

    }
}


public class talent_adaptation : talentInfo
{
    public int efficacity, id1;

    public override void Apply(float power)
    {
        base.Apply(power);

        efficacity = calcPower_int(1, power);

        id1 = V.player_entity.event_turn_start.Add(application);
    }

    public override void Remove()
    {
        base.Remove();

        V.player_entity.event_turn_start.Remove(id1);
    }

    public void application(Entity e)
    {
        bool pm = F.ClosestEnnemy(V.player_entity).distance <= 3;

        if (pm)
        {
            V.player_entity.Add_pm(efficacity);
        }
        else
        {
            V.player_entity.AddEffect(
                Effect.CreateEffect(Effect.effectType.po, efficacity, 1, Talent_Gestion.GetSprite(Talent_Gestion.talent.adaptation), Effect.Reduction_mode.endTurn)
                );
        }
    }
}

public class talent_protection : talentInfo
{
    public int armorParPo, id1;

    public override void Apply(float power)
    {
        base.Apply(power);

        armorParPo = calcPower_int(5, power);

        id1 = V.player_entity.event_turn_end.Add(application);
    }

    public override void Remove()
    {
        base.Remove();


        V.player_entity.event_turn_end.Remove(id1);
    }

    public void application(Entity e)
    {
        if (V.player_info.po == 0)
            return;

        V.player_info.AddArmor(armorParPo * V.player_info.po);
    }
}
public class talent_TirAuCoeur : talentInfo
{
    public int bleedingPower_perHit;

    int id;

    public override void Apply(float power)
    {
        base.Apply(power);

        bleedingPower_perHit = calcPower_int(5, power);

        id = Monster.event_all_monster_dmg.Add(application);
    }

    public override void Remove()
    {
        base.Remove();

        Monster.event_all_monster_dmg.Remove(id);
    }

    public int lastTurnId;

    public List<Entity> ls = new List<Entity>();

    public void application(Entity e, float dmg)
    {
        if (lastTurnId != EntityOrder.id_turn)
        {
            lastTurnId = EntityOrder.id_turn;
            ls.Clear();
        }

        if (!ls.Contains(e))
        {
            ls.Add(e);
            //Effect.AddBleeding(e, bleedingPower_perHit);
        }
    }
}

public class talent_distance : talentInfo
{
    public int pushStr, id1;

    public override void Apply(float power)
    {
        base.Apply(power);

        pushStr = calcPower_int(2, power);

        id1 = V.player_entity.event_turn_end.Add(application);
    }

    public override void Remove()
    {
        base.Remove();

        V.player_entity.event_turn_end.Remove(id1);
    }

    public void application(Entity e)
    {
        foreach (Monster m in EntityOrder.list_monster)
        {
            if (F.IsInLine(m.CurrentPosition_string, V.player_entity.CurrentPosition_string))
            {
                m.Push(pushStr, V.player_entity);
            }
        }
    }
}

public class talent_InfiniteEnergy : talentInfo
{
    public int power;

    int id1, id2;

    public override void Apply(float power)
    {
        base.Apply(power);

        this.power = calcPower_int(5, power);

        id1 = Player.event_startCombat.Add(Reset);
        id2 = spellEffect.event_paUsed.Add(application);
    }

    public override void Remove()
    {
        base.Remove();


        Player.event_startCombat.Remove(id1);
        spellEffect.event_paUsed.Remove(id2);
    }

    public int paNeeded;

    public void Reset()
    {
        paNeeded = 0;
    }

    int paThreshold = 10;

    public void application()
    {

        int nbPa = spellEffect.nbPaUsedThisCombat;

        int nbPaToRemove = paNeeded * paThreshold;

        if (nbPa - nbPaToRemove >= paThreshold)
        {
            paNeeded++;

            Effect.AddNorticeSurfacePower(power);
        }
    }
}

public class talent_Evolution_mage : talentInfo
{
    public int p, id1;

    public override void Apply(float power)
    {
        base.Apply(power);

        p = calcPower_int(1, power);

        id1 = V.player_entity.event_turn_start.Add(application);
    }

    public override void Remove()
    {
        base.Remove();

        V.player_entity.event_turn_start.Remove(id1);
    }

    public void application(Entity e)
    {
        string title = Talent_Gestion.GetTitle(Talent_Gestion.talent.InfiniteEnergy);
        Sprite sp = Talent_Gestion.GetSprite(Talent_Gestion.talent.InfiniteEnergy);

        bool find = false;

        if (EntityOrder.nbTurnSinceStartCombat >= 5)
        {
            V.player_entity.GetEffect_byTitle(title + " pm", ref find);

            if (!find)
            {
                V.player_entity.Add_pm(1);

                V.player_entity.AddEffect(
                    Effect.CreateEffect(title + " pm", Effect.effectType.boost_pm, 1, 1, sp, Effect.Reduction_mode.never)
                    );
            }
        }

        if (EntityOrder.nbTurnSinceStartCombat >= 10)
        {
            V.player_entity.GetEffect_byTitle(title + " pa", ref find);

            if (!find)
            {

                V.player_entity.Add_pa(1);

                V.player_entity.AddEffect(
                    Effect.CreateEffect(title + " pa", Effect.effectType.boost_pa, 1, 1, sp, Effect.Reduction_mode.never)
                    );
            }
        }
    }
}