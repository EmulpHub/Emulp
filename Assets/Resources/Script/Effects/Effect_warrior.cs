using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Effect : MonoBehaviour
{
    public static Effect e_warrior_power, e_warrior_defense;

    public static void Warrior_Alternate()
    {
        int power = V.player_entity.CollectStr(effectType.Warrior_Power);
        int defense = V.player_entity.CollectStr(effectType.Warrior_Resistance);

        string text = V.IsFr() ? "Alterne" : "Alternate";

        /*if (!Talent_Gestion.EquipedTalent(SpellGestion.List.warrior_talent_continuity))
            Main_UI.Display_movingText_basicValue(text, V.Color.green, V.player_entity.transform.position);*/

        //Combat_Effect cf_resistance = null, cf_power = null;

        V.player_entity.RemoveEffect(effectType.Warrior_Power);
        V.player_entity.RemoveEffect(effectType.Warrior_Resistance);

        if (power > 0)
            WarriorParticleManagement.CreateDefense(V.CalcEntityDistanceToBody(V.player_entity), power);

        if (defense > 0)
            WarriorParticleManagement.CreatePower(V.CalcEntityDistanceToBody(V.player_entity), defense);
        //Warrior_AddResistance(power, false, "", false);
        //Warrior_AddPower(defense, false, "", false);

        return;

        if (power > 0)
        {
            if (defense == 0) //Il n'y a pas de defense
            {
                Warrior_AddResistance(power, false, "", false);
            }
            else
            {
                Effect e = V.player_entity.GetEffect(effectType.Warrior_Resistance);

                float diff = power - e.str;

                string txt = V.IsFr() ? "Defense" : "Resistance";

                //SI superieur = augmentation
                //if (diff > 0)
                //  Main_UI.Display_movingText("+" + diff + " " + txt, V.Color.green, e.CombatEffect_Holder.transform.position + V.movingText_StartDistance, V.movingText_TravelDistance / 2);

                e.SetStrenght(power);

                //cf_resistance = e.CombatEffect_Holder;
            }
        }
        else
        {
            //Destroy defense

            //V.player_entity.(Effect.effectType.Warrior_Resistance);
        }

        if (defense > 0)
        {
            if (power == 0) //Il n'y a pas de puissance
            {
                Warrior_AddPower(defense, false, "", false);
            }
            else
            {
                Effect e = V.player_entity.GetEffect(effectType.Warrior_Power);

                float diff = defense - e.str;

                string txt = V.IsFr() ? "Puissance" : "Power";

                //SI superieur = augmentation
                //if (diff > 0)
                //  Main_UI.Display_movingText("+" + diff + " " + txt, V.Color.green, e.CombatEffect_Holder.transform.position + V.movingText_StartDistance, V.movingText_TravelDistance / 2);

                e.SetStrenght(defense);

                //cf_power = e.CombatEffect_Holder;
            }
        }
        else
        {
            //Destroy power

            //  V.player_entity.ClearEffect_ofType(Effect.effectType.Warrior_Power);
        }

        //Additional effect
        //Talent_Gestion.TryTalent(SpellGestion.List.warrior_talent_continuity);
        //Talent_Gestion.Apply_Evolution();
    }

    public static void Move(ref List<Combat_Effect> list, int oldIndex, int newIndex)
    {
        // exit if positions are equal or outside array
        if ((oldIndex == newIndex) || (0 > oldIndex) || (oldIndex >= list.Count) || (0 > newIndex) ||
            (newIndex >= list.Count))
        {
            return;
        }
        // local variables
        var i = 0;
        Combat_Effect tmp = list[oldIndex];

        // move element down and shift other elements up
        if (oldIndex < newIndex)
        {
            for (i = oldIndex; i < newIndex; i++)
            {
                list[i] = list[i + 1];
            }
        }
        // move element up and shift other elements down
        else
        {
            for (i = oldIndex; i > newIndex; i--)
            {
                list[i] = list[i - 1];
            }
        }
        // put element from position 1 to destination
        list[newIndex] = tmp;
    }

    public static void Warrior_RemovePower(int toRemove, bool Display, string additionalText)
    {
        if (toRemove == 0)
            return;
        /*
        var result = V.player_entity.GetEffect_byType(effectType.Warrior_Power);

        if(result.find)
        {
            result.e.RemoveStrenght(toRemove);
        }*/

        string txt = "-" + toRemove;

        if (Display)
        {
            Main_UI.Display_movingText_basicValue(txt + additionalText, V.Color.red, V.player_entity.transform.position, V.pix_power);
        }
    }

    public static void Warrior_AddPower(int strenght, bool Display, string additionalText, bool withEffect = true)
    {
        if (strenght == 0)
            return;

        if (Character.IsTalentEquiped(Talent_Gestion.talent.SpeDefense))
        {
            Warrior_AddResistance(strenght, Display, additionalText);
            return;
        }


        string txt = "+" + strenght;

        if (Display)
        {
            Main_UI.Display_movingText_basicValue(txt + additionalText, V.Color.green, V.player_entity.transform.position, V.pix_power);
        }

        if (withEffect)
        {
            //event_player_addWarrior.CallAll();

        }
    }

    public static void Warrior_AddPower(int strenght, bool Display)
    {
        Warrior_AddPower(strenght, Display, "");
    }

    public static void Warrior_AddPower(int strenght)
    {
        Warrior_AddPower(strenght, true, "");
    }

    public static void Warrior_AddResistance(int strenght, bool Display, string additionalText, bool withEffect = true)
    {
        if (strenght == 0)
            return;

        if (Character.IsTalentEquiped(Talent_Gestion.talent.SpePower))
        {
            Warrior_AddPower(strenght, Display, additionalText);
            return;
        }

        //  V.player_entity.AddEffect(Effect.CreateEffect(effectType.Warrior_Resistance, strenght, 0, V.warrior_resistance, "", Reduction_mode.never));
        string txt = "+" + strenght;

        if (Display)
        {
            Main_UI.Display_movingText_basicValue(txt + additionalText, V.Color.green, V.player_entity.transform.position, V.pix_defense);
        }

        if (withEffect)
        {
        }
    }

    public static void Warrior_AddResistance(int strengh, bool Display)
    {
        Warrior_AddResistance(strengh, Display, "");
    }
    public static void Warrior_AddResistance(int strengh)
    {
        Warrior_AddResistance(strengh, true, "");
    }

    public static void Warrior_Focus_Defense(int additional = 0)
    {
        int DefenseToAdd = Get_power() + additional;

        Warrior_AddResistance(DefenseToAdd, true);

        // V.player_entity.ClearEffect_ofType(Effect.effectType.Warrior_Power);
    }

    public static void Warrior_Focus_Power(int additional = 0)
    {

        int DefenseToAdd = Get_defense() + additional;

        Warrior_AddPower(DefenseToAdd, true);

        // V.player_entity.ClearEffect_ofType(Effect.effectType.Warrior_Resistance);
    }

    /// <summary>
    /// Get warrior power
    /// </summary>
    /// <returns></returns>
    public static int Get_power()
    {
        return 0;
        // return V.player_entity.CollectStr(effectType.Warrior_Power,true);
    }

    /// <summary>
    /// Get warrior defense
    /// </summary>
    /// <returns></returns>
    public static int Get_defense()
    {
        return 0;
        // return V.player_entity.CollectStr(effectType.Warrior_Resistance,true);
    }

    public static bool IsDefenseMajoritary()
    {
        return Get_defense() > Get_power();
    }

    public static bool IsPowerMajoritary()
    {
        int power = Get_power();

        return power != 0 && power >= Get_defense();
    }

    public static int Warrior_ConvertPowerIntoReal(int value)
    {
        int v = value;

        return v;// V.player_entity.CollectAllStrenght(Type.Warrior_Power);
    }

    public static int Warrior_ConvertPowerIntoReal()
    {
        return Warrior_ConvertPowerIntoReal(V.player_entity.CollectStr(effectType.Warrior_Power));
    }

    public static int Warrior_ConvertResistanceIntoReal(int value)
    {
        int v = Mathf.FloorToInt(((float)value / (100 + (float)value)) * 100); //V.player_entity.CollectAllStrenght(Type.Warrior_Resistance);

        v = Mathf.Clamp(v, 1, 95);

        return v;
    }

    public static int Warrior_ConvertResistanceIntoReal()
    {
        return Warrior_ConvertResistanceIntoReal(V.player_entity.CollectStr(effectType.Warrior_Resistance));
    }

    public static int Warrior_PowerFromPassive(SpellGestion.List spell)
    {
        SpellInfo info = SpellGestion.info[spell];

        if (!info.IsWeapon)
            return 0;

        if (spell == SpellGestion.List.base_fist)
        {
            return 3;
        }

        int basePower = 0;

        if (info.pa_cost == 1 || info.pa_cost == 0)
            basePower = 3;
        if (info.pa_cost == 2)
            basePower = 5;
        else if (info.pa_cost == 3)
            basePower = 10;
        else if (info.pa_cost == 4)
            basePower = 15;
        else
            basePower = 15 + (info.pa_cost - 4) * 5;

        if (Equipment_Management.ObjectWeapon_List.ContainsKey(spell))
        {
            switch (Equipment_Management.ObjectWeapon_List[spell].Rarity)
            {
                case SingleEquipment.rarity.Uncommon:
                    basePower += 5;
                    break;
                case SingleEquipment.rarity.Rare:
                    basePower += 10;
                    break;
                default:
                    break;
            }
        }

        return basePower;
    }

    public static float calcReductionAmount()
    {
        float nb = 30;

        nb -= V.player_entity.CollectStr(effectType.warrior_reducePowerReduction);

        return nb / 100;
    }

    public static void StartTurn(Entity e)
    {
        float power = Get_power();

        if (power > 0)
        {
            Warrior_RemovePower(Mathf.CeilToInt((float)power * (float)calcReductionAmount()), true, "");
        }
    }

    static int id1;

    public static void AddEvent()
    {
        id1 = V.player_entity.event_turn_start.Add(StartTurn);
    }

    public static void RemoveEvent()
    {
        V.player_entity.event_turn_start.Remove(id1);
    }

    public static void AddConflagration(int count)
    {
        bool find = false;

        var warriorRageEffect = V.player_entity.GetEffect(Effect.effectType.Warrior_conflagration, ref find);

        if (find)
        {
            warriorRageEffect.SetDuration(count);
        }
        else
        {/*
            var e = V.player_entity.AddEffect(
                Effect.CreateEffect(Effect.effectType.Warrior_conflagration, 1, 2, Resources.Load<Sprite>("Image/Sort/warrior/conflagrationIcon"), "", Effect.Reduction_mode.startTurn)
                );*/

            var endless = AnimEndless_IdleAnimation.Create(V.player_entity, V.Entity_DistanceToBody, "EndlessConflagration", "", "", 2.5f);

            endless.ChangeSortingOrder(-1);
            endless.ChangeLayer("default");

            // endless.LinkToEffect(e);

            endless.SetEndlessSound(SoundManager.list.spell2_warrior_ConflagrationEndless);
        }
    }
}
