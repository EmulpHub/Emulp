using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class Entity : MonoBehaviour
{
    public bool IsDead()
    {
        return Info.IsDead();
    }

    [HideInInspector]
    public bool EndOfGame;

    public int nbKill = 0;

    protected bool isKilled = false;

    public virtual void Kill(InfoKill infoKill)
    {
        if (isKilled) return;

        isKilled = true;

        if (EntityOnMouseOver == this) EntityOnMouseOver = null;

        if (V.game_state == V.State.fight) Scene_Main.SetGameAction_movement();

        Info.Kill();

        event_die.Call(this);

        Destroy(this.gameObject);
    }

    public string Push(int strenght, Entity caster)
    {
        return Push(strenght, caster.CurrentPosition_string);
    }

    public string Push(int strenght, string posOrigin)
    {
        DirectionData.Direction dir = DirectionData.Direction.empty;

        if (strenght < 0)
        {
            dir = DirectionData.GetDirection(posOrigin, CurrentPosition_string);
            strenght *= -1;
        }
        else
        {
            dir = DirectionData.GetDirection(CurrentPosition_string, posOrigin);
        }

        Vector2Int PosToPush = CurrentPosition;
        while (strenght > 0)
        {
            Vector2Int nextPos = PosToPush + DirectionData.ConvertToVector2Int(dir);

            if (F.IsTileWalkable(nextPos))
            {
                PosToPush = nextPos;
                strenght--;
            }
            else
            {
                break;
            }
        }

        GoHere(PosToPush, 0.4f);

        if (V.game_state == V.State.fight) event_allEntity_push.Call();

        return F.ConvertToString(PosToPush);
    }

    public string Attract(int strenght, Entity caster)
    {
        return Push(-strenght, caster);
    }

    public string Attract(int strenght, string posOrigin)
    {
        return Push(-strenght, posOrigin);
    }

    private float TimeBeforeNotInMovement = -1;

    public bool IsInMovementInFight()
    {
        return TimeBeforeNotInMovement >= Time.time;
    }

    public void GoHere(string pos, float time, Ease ease = Ease.Flash)
    {
        TimeBeforeNotInMovement = Time.time + time;

        //SetPosition(CurrentPosition_string, true);

        transform.DOKill();
        transform.DOMove(F.ConvertToWorldVector2(pos), time).SetEase(ease);
    }

    public void GoHere(Vector2Int pos, float time)
    {
        GoHere(F.ConvertToString(pos), time);
    }

    public float startOfTurn_Arrow_Distance;

    public virtual void Turn_start()
    {

        V.main_ui.Display_StartOfTurnArrow(this, startOfTurn_Arrow_Distance);

        Glyphe.CheckGlypheForAnEntity(this, true);

        TurnEffect(true);

        event_turn_start.Call(this);

        event_allEntity_turnStart.Call(this);
    }

    public virtual void Turn_end()
    {

        TurnEffect(false);

        event_turn_end.Call(this);

        Glyphe.CheckGlypheForAnEntity(this, true);

        event_allEntity_turnEnd.Call(this);
    }

    public virtual void Add_pa(int amount, bool display, string additionalText)
    {
        Info.PA += amount;

        Info.CalculateValue();

        if (display && amount != 0)
            DisplayPaChange(amount, additionalText);

    }

    public virtual void Add_pa(int amount, string additionalText)
    {
        Add_pa(amount, true, additionalText);
    }

    public virtual void Add_pa(int amount, bool Display = true)
    {
        Add_pa(amount, Display, "");
    }

    public virtual void Add_pm(int amount, bool display, string additionalText)
    {
        Info.PM += amount;

        Info.CalculateValue();

        if (display && amount != 0)
            DisplayPmChange(amount, additionalText);
    }

    public virtual void Add_pm(int amount, string additionalText)
    {
        Add_pm(amount, true, additionalText);
    }

    public virtual void Add_pm(int amount, bool Display = true)
    {
        Add_pm(amount, Display, "");
    }

    public virtual void Remove_pa(int amount, bool NegativeWay = false, bool display = false, string additionalText = "")
    {
        Info.PA -= amount;

        if (IsPlayer())
            V.player_info.CalculateValue();

        if (display && amount != 0)
        {
            DisplayPaChange(-amount, additionalText);
        }

        if (NegativeWay)
        {
            EffectWhenRemovingPa(amount);
        }
    }

    public virtual void Remove_pm(int amount, bool NegativeWay = false, bool Display = false, string additionalText = "")
    {
        Info.PM -= amount;

        if (IsPlayer())
            V.player_info.CalculateValue();

        if (Display && amount != 0)
        {
            DisplayPmChange(-amount, additionalText);
        }

        if (NegativeWay)
        {
            EffectWhenRemovingPm(amount);
        }
    }

    public virtual void EffectWhenRemovingPa(int amount) { }

    public virtual void EffectWhenRemovingPm(int amount) { }


    public bool IsPlayer()
    {
        return Info.IsPlayer();
    }

    public bool IsMonster()
    {
        return Info.IsMonster();
    }

    public float CalculateStrenght(int dmg, int life_max)
    {
        float dmg_percentLifeRemoved = 0;
        if (life_max - dmg != 0 && dmg >= 20)
        {
            dmg_percentLifeRemoved = (float)Info.Life_max / ((float)Info.Life_max - dmg) - 1;
        }

        float strenght = 0;
        float Percent = dmg_percentLifeRemoved;

        if (Percent >= 0.6f)
        {
            strenght = lifeBarShakeAnimation_Strenght_Max;
        }
        else if (Percent >= 0.3f)
        {
            Percent *= 3.333f;
            strenght = Percent * lifeBarShakeAnimation_Strenght_Max;
            if (strenght < lifeBarShakeAnimation_Strenght_Min)
            {
                strenght = lifeBarShakeAnimation_Strenght_Min;
            }
        }

        return strenght;
    }


    public virtual void Heal(InfoHeal infoHeal)
    {
        infoHeal.heal = Info.CalcHeal(infoHeal.heal);

        Main_UI.Display_movingText_basicValue("+ " + infoHeal.heal, V.Color.green, transform.position, V.pix_heal);

        animation_Boost();

        Info.Heal(infoHeal);

        event_life_heal.Call();
    }

    public virtual float Damage(InfoDamage infoDamage)
    {
        if (infoDamage.caster == V.player_entity)
            PlayerInfo.event_player_doDmg.Call(this, infoDamage.damage);

        infoDamage.damage = Info.CalculateResistance(infoDamage.damage);

        Info.Damage(infoDamage);

        if (infoDamage.damage > 0)
        {
            Main_UI.MovingStruct n = Main_UI.Display_movingText_basicValue("- " + infoDamage.damage, V.Color.red, transform.position, null);

            n.size = Mathf.Clamp(infoDamage.damage - 20, 0, 300) / 100 + 1;
        }

        Vector3 Direction = transform.position - infoDamage.caster.transform.position;
        Vector3 punchPos = Direction * damage_RecalStrenght;

        if (infoDamage.animate)
        {
            ResetAllAnimation();

            float finalSpeed = 0.6f;

            if (infoDamage.caster != this)
            {
                Renderer_movable.transform.DOPunchPosition(punchPos, finalSpeed, 1);
            }
            Renderer_movable.transform.DOPunchRotation(new Vector3(0, 0, damageRotation * (Direction.x < 0.1f ? 1 : -1)), finalSpeed, 1);
        }
        else
        {
            Renderer_movable.transform.DOPunchScale(new Vector3(0, -0.3f, 1), 0.5f);
        }

        event_life_dmg.Call();

        event_allEntity_dmg.Call(this, infoDamage);

        return infoDamage.damage;
    }

}
