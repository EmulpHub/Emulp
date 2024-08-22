using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Effect : MonoBehaviour
{
    /*
    #region Electricity

    public static bool ContainElectricityEffect(Entity target)
    {
        bool find = false;

        target.GetEffect(GetElectricityName(), ref find);

        return find;
    }

    public static string GetElectricityName()
    {
        return V.IsFr() ? "Electricité" : "Electricity"; ;
    }

    public static void ApplyElectricity(Entity target, int duration = 2, string source = "")
    {
        string title = GetElectricityName();

        string desc = V.IsFr() ? "Gagnez *man +1 pa *end et *war +5 puissance *end la premiere fois que vous frappez un ennemi pendant votre tour" : " Gain *man +1 ap *end and *war +5 power *end the first time you hit an ennemy during your turn";
        /*
        target.AddCustomEffect(title, duration, V.warrior_modularHeart_Electricity, source, Reduction_mode.startTurn,
            new List<(effectType type, int strenght, string title, string description)>
            {
                (Effect.effectType.custom,0,title,desc),
                (Effect.effectType.damage,3,"","")

            }, false, false);*/
  //  }
/*
    public static int TimeTurn;

    public static List<int> monsterIdBeingHit = new List<int>();

    public static void CheckElectricityEffect(Entity target)
    {
        if (!ContainElectricityEffect(V.player_entity))
            return;

        if (TimeTurn != EntityOrder.id_turn)
        {
            TimeTurn = EntityOrder.id_turn;
            monsterIdBeingHit.Clear();
        }

        if (!monsterIdBeingHit.Contains(target.ID))
        {
            ElectricityEffect(target);

            monsterIdBeingHit.Add(target.ID);
        }
    }

    public static void ElectricityEffect(Entity target)
    {
        V.player_entity.Add_pa(1);
        Effect.Warrior_AddPower(5);
    }*/
/*
    #endregion

    #region Spike

    public static void CheckSpikeEffect()
    {
        if (!ContainSpikeEffect(V.player_entity))
            return;

        SpikeEffect();
    }

    public static bool ContainSpikeEffect(Entity target)
    {
        bool find = false;

        target.GetEffect(GetSpikeName(), ref find);

        return find;
    }*/
/*
    public static string GetSpikeName()
    {
        return V.IsFr() ? "Epine" : "Spike"; ;
    }
*//*
    public static void ApplySpike(Entity target, int duration = 2, string source = "")
    {
        string title = GetSpikeName();

        string desc = V.IsFr() ? "*dmg +5 dégâts d'epine *end. *war +5 defense lorsque vous subissez des dégats*end" : "*dmg +5 spike damage *end. *war +5 defense when you receive damage *end";

        /*        target.AddCustomEffect(title, duration, V.warrior_modularHeart_Spikes, source, Reduction_mode.startTurn,
                    new List<(effectType type, int strenght, string title, string description)>
                    {
                        (Effect.effectType.custom,0,title,desc),

                    }, false, false);*/
    //}

  /*  public static void SpikeEffect()
    {
        Effect.Warrior_AddResistance(5);
    }
*/
   // #endregion
}
