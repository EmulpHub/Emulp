using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using LayerMap;

public partial class Monster : Entity
{
    bool soundFirst = true;

    public override float Damage(InfoDamage infoDamage)
    {
        base.Damage(infoDamage);

        if (soundFirst)
            SoundManager.PlaySound(SoundManager.list.monster_hit);
        else
            SoundManager.PlaySound(SoundManager.list.monster_hit_2);

        soundFirst = !soundFirst;

        float degree = -45;

        if (transform.position.x - infoDamage.caster.transform.position.x > 0)
            degree = 45;

        Spell.Reference.CreateParticle_BloodLoss(V.CalcEntityDistanceToBody(this), 1 + Mathf.Clamp(infoDamage.damage / 100, 0, 1), Spell.Particle_Amount._16, degree);

        event_all_monster_dmg.Call(this, infoDamage.damage);

        ChangeEye_Hurt();

        return infoDamage.damage;
    }

    public override void Add_pa(int amount, bool Display = true)
    {
        base.Add_pa(amount, Display);

        ChangeEye_Happy();
    }

    public override void Add_pm(int amount, bool Display = true)
    {
        base.Add_pm(amount, Display);

        ChangeEye_Happy();
    }

    public override void Remove_pa(int amount, bool NegativeWay = false, bool display = true, string additionalText = "")
    {
        base.Remove_pa(amount, NegativeWay, display, additionalText);
    }

    public override void Remove_pm(int amount, bool NegativeWay = false, bool Display = true, string additionalText = "")
    {
        base.Remove_pm(amount, NegativeWay, Display, additionalText);
    }

    public override void EffectWhenRemovingPa(int amount)
    {
        base.EffectWhenRemovingPa(amount);

        event_all_monster_remove_pa.Call(this);
    }

    public override void EffectWhenRemovingPm(int amount)
    {
        base.EffectWhenRemovingPm(amount);

        event_all_monster_remove_pm.Call(this);
    }

    public override void Turn_start()
    {
        base.Turn_start();

        ResetAllAnimation();

        StartCoroutine(Monster_Turn());
    }

    [HideInInspector]
    public bool deadAnimationIsRunning;

    public override void Kill(InfoKill infoKill)
    {
        if (isKilled) return;

        if (!deadAnimationIsRunning)
        {
            deadAnimationIsRunning = true;

            ChangeEye_Dead();

            bool PlayerWin = true;

            foreach (Entity e in AliveEntity.list)
            {
                if (e == this || e.Info.dead || !e.IsMonster())
                    continue;

                PlayerWin = false;
                break;
            }

            if (PlayerWin)
                SoundManager.PlaySound(SoundManager.list.monster_dead_endCombat);
            else
                SoundManager.PlaySound(SoundManager.list.monster_dead);

            bool FacingRight = F.IsFaceRight_entity(this, infoKill.caster);

            Spell.Reference.CreateParticle_BloodLoss(transform.position + V.Entity_DistanceToBody_Vector3, 1, Spell.Particle_Amount._12);

            if (FacingRight)
                Renderer_movable.transform.DORotate(new Vector3(0, 0, 45), 0.8f);
            else
                Renderer_movable.transform.DORotate(new Vector3(0, 0, -45), 0.8f);

            event_all_monster_die.Call(this);
        }

        if (V.game_state == V.State.fight)
        {
            EntityOrder.Remove(this);

            Scene_Main.MonsterKilled.Add(new Scene_Main.KilledMonsterInfo(monsterInfo.level, monsterInfo.monster_type));

            Nortice_GainSystem.AddNotricePoint_monster_death(this);

            if (!EntityOrder.ContainMonster())
            {
                if (EndOfGame)
                {
                    Scene_Main.EndOfCombat(true, this, true);

                    BossKill.BossKill.Exe();
                }
                else
                {
                    MapInfo_fight_normal map = ((MapInfo_fight_normal)Main_Map.currentMap.info);

                    map.monsterkilled = true;

                    Main_Map.currentMap.Locked_clear();

                    Scene_Main.EndOfCombat(true, this);
                }
            }
        }

        foreach (Monster m in new List<Monster>(invoke_list))
        {
            if (m is null)
                continue;

            m.Kill(infoKill);
        }

        Main_Map.currentMap.monsterInArea.Remove(this);

        base.Kill(infoKill);
    }

    public override void ResetAllAnimation()
    {
        base.ResetAllAnimation();

        ResetSpriteAndMovement();
    }
}
