using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellEffect : MonoBehaviour
{
    public static int nbPaUsedThisTurn, nbPaUsedThisCombat;

    public static int lastTurnId;

    internal bool applyPercentageEffect = true;
    public bool DoDamage;

    public bool IsACrit;

    public static EventHandlerNoArg event_paUsed = new EventHandlerNoArg(true);

    public float EffectMultiplicator;

    public bool dontUseCost { get => info.dontUseCost; }

    public Action_spell_info info;

    public Action_spell_info_player infoPlayer;

    public List<string> getAreaEffect;

    public Vector2 targetedSquareWorldVector2 { get => F.ConvertToWorldVector2(targetedSquare); }

    public string targetedSquare
    {
        get => info.targetedSquare;
    }

    public Sprite holderSprite { get => spellHolder.graphique.sprite; }

    public Entity caster { get => info.caster; }

    public Monster caster_monster
    {
        get
        {
            if (caster is Monster m)
                return m;

            return null;
        }

    }

    public static int GetNbPaUsed_ThisTurn()
    {
        if (lastTurnId != EntityOrder.id_turn)
        {
            lastTurnId = EntityOrder.id_turn;
            nbPaUsedThisTurn = 0;
        }

        return nbPaUsedThisTurn;
    }

    public static void AddNbPaUsed(int nb)
    {
        if (lastTurnId != EntityOrder.id_turn)
        {
            lastTurnId = EntityOrder.id_turn;
            nbPaUsedThisTurn = 0;
        }

        nbPaUsedThisTurn += nb;
        nbPaUsedThisCombat += nb;

        event_paUsed.Call();
    }

    public void SetBoolInfo(bool doDamage, bool applyPercentage)
    {
        DoDamage = doDamage;
        applyPercentageEffect = applyPercentage;
    }

    public virtual void InfoBool()
    {
        SetBoolInfo(true, true);
    }

    public int calcSTR(float value)
    {
        return calc(V.player_info.CalcStrStats(value, infoPlayer.str));
    }

    public int calcDEX(float value)
    {
        return calc(V.player_info.CalcDexStats(value, infoPlayer.dex));
    }

    public float calcFDEX(float value)
    {
        return calc(V.player_info.CalcDexStats(value, infoPlayer.dex));
    }

    public int calcRES(float value)
    {
        return calc(V.player_info.CalcResStats(value, infoPlayer.res));
    }

    public int calcEFF(float value)
    {
        return calc(V.player_info.CalcEffStats(value, infoPlayer.eff));
    }

    public virtual float calcDamage(float value)
    {
        return calc(value);
    }

    public int calc(float value)
    {
        return calc_stat(value, EffectMultiplicator, spellHolder);
    }

    public float calcF(float value)
    {
        return calcF_stat(value, EffectMultiplicator, spellHolder);
    }

    public static int calc_stat(float b, float multiplicator, Spell e)
    {
        float min = 0;
        if (b >= 1)
            min = 1;

        float calcStat = Mathf.RoundToInt(calcF_stat(b, multiplicator, e));

        return (int)Mathf.Clamp(calcStat, min, 99999);
    }

    public static float calcF_stat(float b, float multiplicator, Spell e)
    {

        //Additional effect
        if (e.typeSpell == Spell.typeOfSpell.weapon)
        {
            multiplicator *= statsForPlayer.GetMultiplicator(statsForPlayer.type.effect_weapon_multiplicator);
        }
        else if (e.typeSpell == Spell.typeOfSpell.objectEquipment)
        {
            multiplicator *= statsForPlayer.GetMultiplicator(statsForPlayer.type.effect_object_multiplicator);
        }

        if (SpellGestion.Get_RangeMax(e.spell) > 1)
        {
            multiplicator *= statsForPlayer.GetMultiplicator(statsForPlayer.type.effect_onDistance_multiplicator);
        }

        return b * multiplicator;
    }

    public Spell spellHolder
    {
        get
        {
            return info.spell;
        }
    }

    public virtual void Init(Action_spell_info info)
    {
        this.info = info;
        if (info is Action_spell_info_player infoPlayer)
        {
            this.infoPlayer = infoPlayer;
        }

        getAreaEffect = SpellInfo.GetStringEffectList(spellHolder.spell, targetedSquare, caster.CurrentPosition_string, caster.IsPlayer());

        InfoBool();

        EffectMultiplicator = CalcCC(spellHolder.Multiplicator);
        if (info is Action_spell_info_player infoPlayerR)
        {
            EffectMultiplicator *= infoPlayerR.multiplicator;
        }
    }

    public float CalcCC(float m)
    {
        if (!applyPercentageEffect)
        {
            IsACrit = false;
            return m;
        }

        int rd = Random.Range(1, 100 + 1);

        if (V.script_Scene_Main_Administrator.onlyCrit && V.administrator)
        {
            rd = 0;
        }
        if (V.script_Scene_Main_Administrator.noCrit && V.administrator)
            rd = 10000;

        if (rd <= caster.Info.cc)
        {
            IsACrit = true;
            return m * ((float)caster.Info.ec / 100 + 1);
        }

        IsACrit = false;

        float modifier = 1;

        if (caster.IsPlayer()) modifier = (float)V.player_info.effectForNonCc / 100;

        return m * modifier;
    }

    public virtual void Cost()
    {
        if (V.script_Scene_Main_Administrator.focus_spell.Contains(spellHolder.spell) || dontUseCost)
            return;

        int pa = spellHolder.Get_pa_cost();

        if (spellHolder.cd < 0)
        {
            spellHolder.SetUse();
        }
        else
        {
            spellHolder.SetCooldown(spellHolder.cd);
        }

        info.caster.Remove_pa(pa, false);

        if (info.caster == V.player_entity)
        {
            event_player_cost.Call(pa);
        }
    }

    public static EventHandlerNoArg event_all_crit = new EventHandlerNoArg(true);
    public static EventHandlerNoArg event_player_crit = new EventHandlerNoArg(true);
    public static EventHandlerNoArg event_player_beforeAction_playingSpell = new EventHandlerNoArg(true);
    public static EventHandlerNoArg event_player_beforeAction_playingSpell_damageable = new EventHandlerNoArg(true);

    public static EventHandlerNoArg event_player_afterAction_playingSpell = new EventHandlerNoArg(true);
    public static EventHandlerNoArg event_player_afterAction_playingSpell_damageable = new EventHandlerNoArg(true);
    public static EventHandlerNoArg event_monster_crit = new EventHandlerNoArg(true);

    public static EventHandlerInt event_player_cost = new EventHandlerInt(true);

    public IEnumerator Cast()
    {
        float timeStartCast = Time.time;

        if (IsACrit)
        {
            event_all_crit.Call();
            if (caster.IsPlayer())
                event_player_crit.Call();
            else if (caster.IsMonster())
                event_monster_crit.Call();

            spellHolder.StartCoroutine(spellHolder.Anim_PopUp(V.CC, V.CalcEntityDistanceToBody_Vector3(caster.transform.position) + new Vector3(0.3f, 0.6f, 0), 1, 1.5f));
        }

        if (caster.IsPlayer())
        {
            event_player_beforeAction_playingSpell.Call();
            if (DoDamage)
                event_player_beforeAction_playingSpell_damageable.Call();
        }

        Cast_Before_Void();

        ApplyPercentage();

        Cost();

        Task t = new Task(Effect_before());

        while (t != null && t.Running)
        {
            yield return new WaitForEndOfFrame();
        }

        List<Task> lsT = new List<Task>();

        foreach (string pos in getAreaEffect)
        {
            Entity e = EntityByPos.TryGet(pos);

            Effect_Target_Before(e, pos);

            lsT.Add(new Task(Effect_Target(e, pos)));

            Effect_Target_After(e, pos);
        }

        while (lsT.Count > 0)
        {
            Task s = lsT[0];

            if (!s.Running)
            {
                lsT.Remove(s);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }

        Cast_After_Void();

        t = new Task(Cast_After());

        while (t != null && t.Running)
        {
            yield return new WaitForEndOfFrame();
        }

        Cast_Finish_Void();

        if (caster.IsPlayer())
        {
            event_player_afterAction_playingSpell.Call();
            if (DoDamage)
                event_player_afterAction_playingSpell_damageable.Call();
        }

        if (Time.time - timeStartCast < 0.2f)
        {
            yield return new WaitForSeconds(Time.time - timeStartCast);
        }

        V.player_info.CalculateValue();

    }

    public virtual IEnumerator Effect_before()
    {
        yield break;
    }


    public virtual IEnumerator Cast_After()
    {
        yield break;
    }

    public virtual IEnumerator Effect_Target(Entity target, string pos)
    {
        yield break;
    }

    public virtual bool TargetForDamage(Entity target)
    {
        return target != null && target != caster && !target.Info.dead;
    }
    public virtual bool TargetForBonus(Entity target)
    {
        return target != null && !target.Info.dead;
    }
    public virtual bool TargetForAll(Entity target)
    {
        return target != null && !target.Info.dead;
    }

    public void Effect_Target_Before(Entity target, string pos)
    {
    }

    public void Effect_Target_After(Entity target, string pos)
    {

    }

    public virtual void Cast_Before_Void()
    {

    }

    public virtual void Cast_After_Void()
    {
        if (caster == V.player_entity)
        {
            if (DoDamage)
                V.player_entity.RemoveEffect(Effect.effectType.damage_oneUse, true);

            if (applyPercentageEffect)
                V.player_entity.RemoveEffect(Effect.effectType.criticalHitChance_oneUse, true);

            AddNbPaUsed(spellHolder.Get_pa_cost());
        }

    }

    public virtual void Cast_Finish_Void()
    {
        Destroy(this, 10);
    }

    public virtual void ApplyPercentage()
    {
        if (caster != V.player_entity)
            return;

        if (applyPercentageEffect)
        {
            V.player_entity.RemoveEffect(Effect.effectType.effectPercentage_OneUse);
        }

    }

    public virtual void actionToolbar_add(int toolbarId) { }

    public virtual void actionToolbar_remove(int toolbarId) { }
}