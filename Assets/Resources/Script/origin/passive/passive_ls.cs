using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passiveInfo : MonoBehaviour
{
    public string title, description;

    public Sprite image;

    public virtual void Add() { }

    public virtual void Remove() { }
}

public class passive_rage : passiveInfo
{
    int id;
    public override void Add()
    {
        id = Player.event_startCombat.Add(ApplyEffectWithoutEntity);
    }

    public override void Remove()
    {
        Player.event_startCombat.Remove(id);
    }

    static void ApplyEffectWithoutEntity()
    {
        ApplyEffect(null);
    }

    static void ApplyEffect(Entity entity = null)
    {
        Effect.Warrior_AddPower(50);
    }
}

public class passive_intensity : passiveInfo
{

    int id1;

    public override void Add()
    {
        id1 = V.player_entity.event_turn_end.Add(application);
    }

    public override void Remove()
    {
        V.player_entity.event_turn_end.Remove(id1);
    }

    public static void application(Entity e)
    {
        V.player_entity.Heal(new InfoHeal(V.player_info.Life_max * 0.1f));
    }
}

public class passive_traveler : passiveInfo
{
    int id;

    public override void Add()
    {
        id = PlayerInfo.event_calculateValue.Add(application);
    }

    public override void Remove()
    {
        PlayerInfo.event_calculateValue.Remove(id);
    }

    public static void application()
    {
        int nb = V.player_info.PM * 5;

        bool find = false;

        Effect e = V.player_entity.GetEffect_byTitle(Origin_Passive.Get_Title(Origin_Passive.Value.traveler), ref find);

        if (find)
        {
            if (e.Str < nb)
            {
                e.SetStrenght(nb);
            }
        }
        else if (nb > 0)
        {
            V.player_entity.AddEffect(
                Effect.CreateEffect(Origin_Passive.Get_Title(Origin_Passive.Value.traveler), Effect.effectType.effectPercentage, nb, 1, Origin_Passive.Get_Image(Origin_Passive.Value.traveler), Effect.Reduction_mode.never)
                );
        }
    }
}

public class passive_melee : passiveInfo
{
    int id1;

    public override void Add()
    {
        V.player_entity.event_turn_start.Add(a);
    }


    public override void Remove()
    {
        V.player_entity.event_turn_start.Remove(id1);
    }

    public void a(Entity e)
    {
        int nb = 0;

        foreach (Monster m in EntityOrder.list_monster)
        {
            if (F.DistanceBetweenTwoPos(m, e) <= 3)
            {
                nb++;
            }
        }

        if (nb > 0)
            V.player_entity.AddEffect(
                Effect.CreateEffect(Effect.effectType.effectPercentage, nb * 20, 1, Origin_Passive.Get_Image(Origin_Passive.Value.melee), Effect.Reduction_mode.startTurn)
                );
    }
}


public class passive_rising : passiveInfo
{
    int id1, id2, id3;

    public override void Add()
    {
        id2 = Player.event_startCombat.Add(application_startCombat);
        id1 = V.player_entity.event_turn_end.Add(application_endTurn);
        id3 = spellEffect.event_player_cost.Add(application_pa);
    }


    public override void Remove()
    {
        Player.event_startCombat.Remove(id2);
        V.player_entity.event_turn_end.Remove(id1);
        spellEffect.event_player_cost.Remove(id3);
    }

    public static void AddEffect(int nb)
    {
        V.player_entity.AddEffect(
            Effect.CreateEffect(Effect.effectType.effectPercentage, nb, 1, Origin_Passive.Get_Image(Origin_Passive.Value.rising), Effect.Reduction_mode.never)
            );

    }

    public static void application_startCombat()
    {
        AddEffect(-50);
    }

    public static void application_endTurn(Entity e)
    {
        AddEffect(10);
    }

    public static int paUsed;

    public static void application_pa(int pa)
    {
        paUsed += pa;

        while (paUsed >= 12)
        {
            AddEffect(10);
            paUsed -= 12;
        }
    }
}

public class passive_overflow : passiveInfo
{
    int id1;

    public override void Add()
    {
        id1 = spellEffect.event_player_cost.Add(application);
    }

    public override void Remove()
    {
        spellEffect.event_player_cost.Remove(id1);
    }

    public static void application(int paCost)
    {
        if (paCost > 0)
            V.player_entity.AddEffect(
                Effect.CreateEffect(Effect.effectType.effectPercentage, paCost * 5, 1, Origin_Passive.Get_Image(Origin_Passive.Value.overflow), Effect.Reduction_mode.startTurn)
                );
    }
}

public class passive_control : passiveInfo
{
    int id1;
    public override void Add()
    {
        V.player_entity.event_turn_end.Add(a);
    }

    public override void Remove()
    {
        V.player_entity.event_turn_end.Remove(id1);
    }

    public void a(Entity e)
    {
        int nb = 0;

        foreach (Monster m in EntityOrder.list_monster)
        {
            if (F.IsInLine(m.CurrentPosition_string, V.player_entity.CurrentPosition_string) || F.IsInDiagonal(m.CurrentPosition_string, V.player_entity.CurrentPosition_string))
                nb++;
        }

        if (nb > 0)
            V.player_entity.AddEffect(
                Effect.CreateEffect(Effect.effectType.po, nb, 1, Origin_Passive.Get_Image(Origin_Passive.Value.control), Effect.Reduction_mode.startTurn)
                );

        if (nb >= 3)
            V.player_entity.AddEffect(
                Effect.CreateEffect(Effect.effectType.power, 30, 1, Origin_Passive.Get_Image(Origin_Passive.Value.control), Effect.Reduction_mode.startTurn)
                );
    }
}

public class passive_isolation : passiveInfo
{
    int id1;
    public override void Add()
    {
        base.Add();

        id1 = V.player_entity.event_turn_start.Add(a);
    }

    public override void Remove()
    {
        base.Add();

        V.player_entity.event_turn_start.Remove(id1);
    }

    public void a(Entity e)
    {
        if (!Origin_Passive.ContainPlayer(Origin_Passive.Value.isolation))
            return;

        int nb = 0;

        foreach (Monster m in EntityOrder.list_monster)
        {
            if (F.DistanceBetweenTwoPos(m, e) <= 3)
            {
                nb++;
            }
        }

        if (nb == 0)
            V.player_entity.AddEffect(
                Effect.CreateEffect(Effect.effectType.effectPercentage, 30, 1, Origin_Passive.Get_Image(Origin_Passive.Value.isolation), Effect.Reduction_mode.startTurn)
                );
    }
}

public class passive_pawn : passiveInfo
{
    int id1, id2;

    public override void Add()
    {
        id1 = F.event_entity_tp.Add(apply);
        id2 = Entity.event_allEntity_push.Add(apply);
    }

    public override void Remove()
    {
        F.event_entity_tp.Remove(id1);
        Entity.event_allEntity_push.Remove(id2);
    }

    static void apply()
    {

        V.player_entity.AddEffect(
            Effect.CreateEffect(Effect.effectType.power, 5, 1, Origin_Passive.Get_Image(Origin_Passive.Value.pawn), Effect.Reduction_mode.never)
            );
    }
}
