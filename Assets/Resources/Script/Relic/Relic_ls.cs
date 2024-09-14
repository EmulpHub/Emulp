using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class relic : MonoBehaviour
{
    public string title, description;
    public Sprite sp;
    public List<(SingleEquipment.value_type t, int value)> equipmentValue;

    public void set(string title, string description, Sprite sp, List<(SingleEquipment.value_type t, int value)> ls)
    {
        this.title = title;
        this.description = description;
        this.sp = sp;
        this.equipmentValue = ls;
    }

    public static void ResetAllRelicStat() { }

    public virtual void Add() { }

    public virtual void Remove() { }
}

public class relic_doble : relic
{
    int nbTurn = 0;

    public void startCombat()
    {
        nbTurn = 0;
    }

    public void turn(Entity e)
    {
        nbTurn++;
        if (nbTurn == 5)
        {
            Apply();
            nbTurn = 0;
        }
    }

    public void Apply()
    {
        V.player_entity.AddEffect(
            Effect.CreateEffect(Effect.effectType.Spellx2_oneUse, 1, 1, null, Effect.Reduction_mode.endTurn)
            );
    }

    int id1, id2;

    public override void Add()
    {
        id2 = Player.event_startCombat.Add(startCombat);
        id1 = V.player_entity.event_turn_start.Add(turn);
    }

    public override void Remove()
    {
        Player.event_startCombat.Remove(id2);
        V.player_entity.event_turn_start.Remove(id1);
    }
}

public class relic_life : relic
{
    public void application(Entity t, float dmg)
    {
        if (t.Info.tackledEffect <= 0)
        {
            return;
        }

        float additionalPv = V.player_info.Life_max * 0.05f;

        t.Damage(new InfoDamage(additionalPv, V.player_entity));
    }

    int key, key_del;
    int id;

    public override void Add()
    {
        id = Monster.event_all_monster_dmg.Add(application);

        float a(Entity e = null)
        {
            return Mathf.CeilToInt(V.player_info.Life_max * 0.01f);
        }

        key = statsForPlayer.Add(statsForPlayer.type.stats_life, 40);
        key_del = statsForPlayer.Add_del(new List<(statsForPlayer.type t, statsForPlayer.calculateVal a, bool needTargetInfo)>
        {
            (statsForPlayer.type.stats_tackle,a,false),
            (statsForPlayer.type.stats_leak,a,false)
        });
    }

    public override void Remove()
    {
        Monster.event_all_monster_dmg.Remove(id);
        statsForPlayer.Remove(key);
        statsForPlayer.Remove(key_del);
    }
}


public class relic_energetic : relic
{
    int key;

    public override void Add()
    {
        key = statsForPlayer.Add(statsForPlayer.type.stats_paMax, 1);
    }

    public override void Remove()
    {
        statsForPlayer.Remove(key);
    }
}


public class relic_vampire : relic
{
    int key;

    public override void Add()
    {
        key = statsForPlayer.Add(statsForPlayer.type.stats_lifeSteal, 30);
    }


    public override void Remove()
    {
        statsForPlayer.Remove(key);
    }
}

public class relic_distance : relic
{
    public Display_SimpleText text;

    public int lastDamageShow = -1;

    public static int calcBonusDamage(Entity target)
    {
        int distance = F.DistanceBetweenTwoPos(V.player_entity, target) - 1;

        return Mathf.CeilToInt(Mathf.Pow(1.1f, distance) * 100);
    }

    public void Application_mouseOver(Entity e)
    {
        if (V.game_state != V.State.fight)
            return;

        int dmg = calcBonusDamage(e) - 100;

        if (text == null)
        {
            lastDamageShow = dmg;

            text = Display_SimpleText.Display("+" + lastDamageShow + "%", RelicInit.relic_sprite(RelicInit.relicLs.distance));

            Anim_MouseOver();
        }
        else if (dmg != lastDamageShow)
        {
            lastDamageShow = dmg;

            Anim_MouseOver();

            text.Init("+" + dmg + "%", RelicInit.relic_sprite(RelicInit.relicLs.distance));
        }

        text.transform.position = e.transform.position + new Vector3(-0.15f, -0.3f, 0);
    }

    public void Application_mouseExit(Entity e)
    {
        lastDamageShow = -1;
        Anim_MouseExit();
    }

    void Anim_MouseOver()
    {
        if (text == null)
            return;

        text.gameObject.SetActive(true);

        text.description.DOKill();
        text.description.DOFade(1, 0.3f);

        text.img.DOKill();
        text.img.DOFade(1, 0.3f);
    }

    void Anim_MouseExit()
    {
        if (text == null)
            return;

        text.description.DOFade(0, 0);
        text.img.DOFade(0, 0);
        text.gameObject.SetActive(false);
    }

    int key, key_del;

    int id1, id2;

    public override void Add()
    {
        id1 = Monster.event_all_monster_mouseOver.Add(Application_mouseOver);
        id2 = Monster.event_all_monster_mouseExit.Add(Application_mouseExit);

        float a(Entity e = null)
        {
            return calcBonusDamage(e) - 1 * 100;
        }

        key = statsForPlayer.Add(statsForPlayer.type.stats_po, 1);
        key_del = statsForPlayer.Add_del(statsForPlayer.type.allDamage_multiplicator, a, true);
    }

    public override void Remove()
    {
        Monster.event_all_monster_mouseOver.Remove(id1);
        Monster.event_all_monster_mouseExit.Remove(id2);

        statsForPlayer.Remove(key);
        statsForPlayer.Remove_del(key_del);
    }
}

public class relic_equipment : relic
{
    public int key;

    public override void Add()
    {
        key = statsForPlayer.Add(new List<(statsForPlayer.type t, float val)>
        {
            (statsForPlayer.type.stats_paMax,1),
            (statsForPlayer.type.effect_object_multiplicator,100),
            (statsForPlayer.type.effect_weapon_multiplicator,50)
        });
    }

    public override void Remove()
    {
        statsForPlayer.Remove(key);
    }
}

public class relic_allOrNothing : relic
{
    public int key;

    public override void Add()
    {
        key = statsForPlayer.Add(new List<(statsForPlayer.type t, float val)>
        {
            (statsForPlayer.type.effect_cc_multiplicator,100),
            (statsForPlayer.type.effect_nonCc_multiplicator,-50),
            (statsForPlayer.type.stats_cc_chance,20)
        });
    }

    public override void Remove()
    {
        statsForPlayer.Remove(key);
    }
}


public class relic_criticalExpert : relic
{
    public void Application_withoutEntity()
    {
        string source = RelicInit.relic_title(RelicInit.relicLs.criticalExpert);

        bool find = false;

        Effect e = V.player_entity.GetEffect_byTitle(source, ref find);

        if (find)
        {
            int max = 100;

            int toAdd = Mathf.Clamp(max - e.Str, 0, 5);

            if (toAdd > 0)
            {
                e.AddStr(toAdd);
            }
        }
        else
        {
            V.player_entity.AddEffect(
                Effect.CreateEffect(source, Effect.effectType.ec, 5, 0, RelicInit.relic_sprite(RelicInit.relicLs.criticalExpert), Effect.Reduction_mode.never)
                );
        }
    }
    int id1;
    public override void Add()
    {
        id1 = spellEffect.event_player_crit.Add(Application_withoutEntity);
    }

    public override void Remove()
    {
        spellEffect.event_player_crit.Remove(id1);
    }
}

public class relic_spikyArmor : relic
{
    public void Application_withEntity(Entity t)
    {
        int armor = Mathf.CeilToInt(5 * V.player_info.spike);

        V.player_info.AddArmor(armor);
    }

    int id1;

    public override void Add()
    {
        id1 = V.player_entity.event_turn_end.Add(Application_withEntity);
    }

    public override void Remove()
    {
        V.player_entity.event_turn_end.Remove(id1);
    }

}

public class relic_killer : relic
{
    public void Application_withEntity(Entity m)
    {
        V.player_entity.Add_pa(3);

        if (m is Monster && !((Monster)m).monsterInfo.IsAnInvocation)
        {
            V.player_entity.AddEffect(
                Effect.CreateEffect(Effect.effectType.power, 50, 0, RelicInit.relic_sprite(RelicInit.relicLs.killer), Effect.Reduction_mode.never)
                );
        }
    }

    int id;

    public override void Add()
    {
        id = Monster.event_all_monster_die.Add(Application_withEntity);
    }


    public override void Remove()
    {
        Monster.event_all_monster_die.Remove(id);
    }
}

public class relic_endurance : relic
{
    public int lastTurnApply = -1;

    public void Application_withoutEntity(float gainArmor)
    {
        V.player_entity.Add_pa(2);
    }

    public void Application_Heal(InfoHeal info)
    {
        Application_withoutEntity(0);
    }

    int id, id2;

    public override void Add()
    {
        id = V.player_info.event_armor_gain.Add(Application_withoutEntity);
        id2 = V.player_entity.event_life_heal.Add(Application_Heal);
    }

    public override void Remove()
    {
        V.player_info.event_armor_gain.Remove(id);
        V.player_entity.event_life_heal.Remove(id2);
    }
}