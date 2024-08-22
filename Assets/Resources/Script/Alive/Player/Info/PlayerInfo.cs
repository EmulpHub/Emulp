using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerInfo : EntityInfo
{
    public static EventHandlerEntityFloat event_player_doDmg = new EventHandlerEntityFloat(true);

    public static EventHandlerNoArg event_calculateValue = new EventHandlerNoArg(true);

    public EventHandlerNoArg event_armor_gain = new EventHandlerNoArg(false);
    public EventHandlerNoArg event_armor_loose = new EventHandlerNoArg(false);

    public override void InitInfo(Entity holder)
    {
        base.InitInfo(holder);

        type = Type.player;

        Entity.event_allEntity_dmg.Add(LifeStealEffect);
    }

    private void LifeStealEffect(Entity damagedEntity, InfoDamage infoDamage)
    {
        if (lifeSteal <= 0 || damagedEntity == holder || infoDamage.caster != holder) return;

        int heal = Mathf.CeilToInt(infoDamage.damage * lifeSteal / 100);

        holder.Heal(new InfoHeal(heal));
    }

    public override void ResetAllStats()
    {
        base.ResetAllStats();

        armor = 0;
    }

    public bool HaveSkillPoint()
    {
        return point_skill > 0;
    }

    public int AddArmor(int amount)
    {
        int val = calcArmor(amount);

        armor += val;

        holder.RemoveEffect(Effect.effectType.heal_OneUse);

        event_armor_gain.Call();

        return val;
    }

    public override void Damage(InfoDamage infoDamage)
    {
        infoDamage.damage = RemoveArmor(infoDamage.damage, out float armorTankedAmount);

        if (armorTankedAmount > 0)
        {
            Main_UI.MovingStruct n = Main_UI.Display_movingText_basicValue("- " + armorTankedAmount, V.Color.grey, holder.transform.position, V.pix_armor);

            n.size = Mathf.Clamp(infoDamage.damage - 20, 0, 300) / 100 + 1;
        }

        base.Damage(infoDamage);
    }

    public int calcArmor(float arm)
    {
        arm = CalcResStats(arm);

        return Mathf.CeilToInt(arm * ((float)armorEfficacity / 100 + 1));
    }

    public float RemoveArmor(float dmg, out float armorTankedAmount)
    {
        float dmgToReturn = dmg;

        if (armor > dmgToReturn)
        {
            armorTankedAmount = dmg;
            dmgToReturn = 0;
        }
        else
        {
            armorTankedAmount = armor;
            dmgToReturn -= armor;
        }

        armor = Mathf.FloorToInt(Mathf.Clamp(armor - dmg, 0, 99999));

        event_armor_loose.Call();

        return dmgToReturn;
    }

    public static void RemoveArmor_Display(int dmg, bool red)
    {
        Main_UI.Display_movingText_basicValue("-" + dmg, red ? V.Color.red : V.Color.grey, V.player_entity.transform.position, V.pix_armor);
    }


    public bool GainXp(int xp_gain)
    {
        V.script_Scene_Main.ProgessionBarXpScript.GainXp(xp_gain);

        bool levelUp = false;

        xp += xp_gain;

        while (xp >= xp_max)
        {
            levelUp = true;
            xp -= xp_max;
            level++;

            xp_max = CalcXpMax(level);
        }

        if (levelUp)
        {
            Nortice_GainSystem.AddNotricePoint_LevelUp();

            card_primaryStats.CreateChoiceCardRandom();

            point_skill++;

            CalculateValue();
        }

        return levelUp;
    }

    public int NbSkillBuyableAndRemaining()
    {
        if (point_skill > SpellGestion.allSkill_locked.Count)
            return SpellGestion.allSkill_locked.Count;

        return point_skill;
    }
}
