using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipClass : MonoBehaviour
{
    public virtual void Add()
    {

    }

    public virtual void Remove()
    {

    }

    public float power = 1;
}

public class EquipClass_none : EquipClass
{

}

public class EquipClass_heal5 : EquipClass
{
    int id, id2;

    public override void Add()
    {
        id = Monster.event_all_monster_dmg.Add(MonsterDamaged);
        id2 = V.player_entity.event_turn_end.Add(Application);
    }

    public override void Remove()
    {
        Monster.event_all_monster_dmg.Remove(id);
        V.player_entity.event_turn_end.Remove(id2);
    }

    public void MonsterDamaged(Entity e, float dmg)
    {
        bool find = false;

        var effect = V.player_entity.GetEffect_byTitle("Heal per hit", ref find);

        if (find)
        {
            effect.AddStr(1);
        }
        else
        {
            addEffect();
        }
    }

    public void addEffect()
    {
        Effect custom = Effect.CreateCustomTxtEffect("Heal per hit", "", 0, null, Effect.Reduction_mode.never);

        V.player_entity.AddEffect(custom);

        custom.SetInfoString(() =>
        {
            return "*bon" + custom.Str + "*end";
        });
    }

    public void Application(Entity e)
    {
        bool find = false;

        var effect = V.player_entity.GetEffect_byTitle("Heal per hit", ref find);

        int stack = 0;

        if (find)
        {
            stack = effect.Str;
            effect.SetStrenght(0);
        }
        else
            return;

        float heal = V.player_info.CalcResStats(10) + V.player_info.CalcResStats(2) * stack;

        V.player_entity.Heal(new InfoHeal(heal));
    }
}

public class EquipClass_armor5 : EquipClass
{
    int id;

    public override void Add()
    {
        id = V.player_entity.event_turn_start.Add(Application);
    }

    public override void Remove()
    {
        V.player_entity.event_turn_start.Remove(id);
    }

    public void Application(Entity e)
    {
        V.player_info.AddArmor(Mathf.CeilToInt(V.player_info.Life_max * 0.05f * power));
    }
}

public class EquipClass_ArmEffect30 : EquipClass
{
    int key;

    public override void Add()
    {
        key = statsForPlayer.Add(statsForPlayer.type.effect_weapon_multiplicator, 30 * power);
    }

    public override void Remove()
    {
        statsForPlayer.Remove(key);
    }
}


public class EquipClass_ObjectEffect30 : EquipClass
{
    int key;

    public override void Add()
    {
        key = statsForPlayer.Add(statsForPlayer.type.effect_object_multiplicator, 30 * power);
    }

    public override void Remove()
    {
        statsForPlayer.Remove(key);
    }
}

public class EquipClass_Pa1 : EquipClass
{
    int key;

    public override void Add()
    {
        key = statsForPlayer.Add(statsForPlayer.type.stats_paMax, 1 * power);
    }

    public override void Remove()
    {
        statsForPlayer.Remove(key);
    }
}

public class EquipClass_Pm1 : EquipClass
{
    int key;

    public override void Add()
    {
        key = statsForPlayer.Add(statsForPlayer.type.stats_pmMax, 1 * power);
    }

    public override void Remove()
    {
        statsForPlayer.Remove(key);
    }
}

public class EquipClass_lifeSteal10 : EquipClass
{
    public override void Add()
    {
        V.player_entity.AddEffect(
            Effect.CreateEffect("lifeSteal10", Effect.effectType.lifeSteal, Mathf.CeilToInt(10 * power), 0, null, Effect.Reduction_mode.permanent)
            , false);
    }

    public override void Remove()
    {
        V.player_entity.RemoveEffect_byTitle("lifeSteal10", true);
    }
}

public class EquipClass_pv50 : EquipClass
{
    public override void Add()
    {
        V.player_entity.AddEffect(
            Effect.CreateEffect("pv50", Effect.effectType.pv, Mathf.CeilToInt(50 * power), 0, null, Effect.Reduction_mode.permanent)
            , true);
    }

    public override void Remove()
    {
        V.player_entity.RemoveEffect_byTitle("pv50", true);
    }
}

public class EquipClass_spellIncreaseRight : EquipClass
{
    public override void Add()
    {
        CombatSpell_VisualEffect.Add("spellIncreaseRight20", new List<string> { "5_0", "5_1" }, delegate ()
        {
            return 0.2f * power;
        });
    }

    public override void Remove()
    {
        CombatSpell_VisualEffect.Remove("spellIncreaseRight20");
    }
}


public class EquipClass_AccumulationWhenDamage : EquipClass
{
    int id;
    public override void Add()
    {
        id = V.player_entity.event_dmg.Add(Application);
    }

    public override void Remove()
    {
        V.player_entity.event_dmg.Remove(id);
    }

    public void Application(InfoDamage info)
    {
        warrior_spent.AddAcumulation(V.player_entity, 1);
    }
}


public class EquipClass_strenght30 : EquipClass
{
    public override void Add()
    {
        V.player_entity.AddEffect(
            Effect.CreateEffect("Strenght", Effect.effectType.strenght, Mathf.CeilToInt(30 * power), 0, null, Effect.Reduction_mode.permanent)
            , false);
    }

    public override void Remove()
    {
        V.player_entity.RemoveEffect_byTitle("Strenght", true);
    }
}

public class EquipClass_spikeShard : EquipClass
{
    int id;

    public override void Add()
    {
        id = V.player_info.event_armor_gain.Add(application);
    }

    public override void Remove()
    {
        V.player_info.event_armor_gain.Remove(id);
    }

    public void application(float armorGain)
    {
        V.player_entity.AddEffect(
            Effect.CreateEffect("", Effect.effectType.spikeShard, 1, 0, null, Effect.Reduction_mode.never)
            );
    }
}