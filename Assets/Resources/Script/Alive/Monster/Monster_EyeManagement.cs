using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public partial class Monster : Entity
{
    [HideInInspector]
    public Sprite eye_angry, eye_dead, eye_hurt, eye_happy, eye_attack;

    public Image eye_img;

    public enum eye_state { angry, dead, hurt };

    public void ChangeEye_angry()
    {
        if (DontChangeEyeAgain)
            return;

        eye_img.sprite = eye_angry;
    }

    public void ChangeEye_Dead()
    {
        eye_img.sprite = eye_dead;

        DontChangeEyeAgain = true;
    }

    public void ChangeEye_Hurt()
    {
        if (DontChangeEyeAgain)
            return;

        eye_img.sprite = eye_hurt;

        SetTimerBeforeChangingEye();
    }

    public void ChangeEye_Attack()
    {
        if (DontChangeEyeAgain)
            return;

        eye_img.sprite = eye_attack;

        SetTimerBeforeChangingEye();
    }

    public void ChangeEye_Happy()
    {
        if (DontChangeEyeAgain)
            return;

        eye_img.sprite = eye_happy;

        SetTimerBeforeChangingEye();
    }

    [HideInInspector]
    public float TimerBeforeChangingToAngryEye;

    [HideInInspector]
    public bool ShouldChangeAngryEyeAfterTiming, DontChangeEyeAgain;

    public void SetTimerBeforeChangingEye()
    {
        ShouldChangeAngryEyeAfterTiming = true;

        TimerBeforeChangingToAngryEye = 0.5f;
    }

    public void Eye_management()
    {
        TimerBeforeChangingToAngryEye -= Time.deltaTime;

        if (TimerBeforeChangingToAngryEye <= 0 && ShouldChangeAngryEyeAfterTiming)
        {
            ChangeEye_angry();

            ShouldChangeAngryEyeAfterTiming = false;
        }
    }

    public void Eye_Init()
    {
        Vector3 eye_scale = new Vector3(0.95f, 0.95f, 1);
        Vector2 eye_pos = new Vector2(0, 0.78f);

        if (monsterInfo.monster_type == MonsterInfo.MonsterType.junior)
        {
            eye_scale = new Vector3(0.6636615f, 0.6636615f, 1);
            eye_pos = new Vector2(0, 0.617f);
        }
        if (monsterInfo.monster_type == MonsterInfo.MonsterType.shark)
        {
            eye_scale = new Vector3(1.13f, 1.13f, 1);
            eye_pos = new Vector2(0, 0.833f);
        }
        else if (monsterInfo.monster_type == MonsterInfo.MonsterType.funky)
        {
            eye_scale = new Vector3(1.04f, 1.04f, 1);
            eye_pos = new Vector2(0, 0.833f);

            LoadSpecificEye("Image/Monster/eye/specificEye/funky");
        }
        else if (monsterInfo.monster_type == MonsterInfo.MonsterType.vala)
        {
            LoadSpecificEye("Image/Monster/eye/specificEye/vala");
        }
        else if (monsterInfo.monster_type == MonsterInfo.MonsterType.grassy)
        {
            LoadSpecificEye("Image/Monster/eye/specificEye/grassy");
        }

        eye_img.rectTransform.anchoredPosition = eye_pos;
        eye_img.transform.localScale = eye_scale;

        ChangeEye_angry();
    }

    public void LoadSpecificEye(string path)
    {
        Sprite[] a = Resources.LoadAll<Sprite>(path);

        foreach (Sprite eye in a)
        {
            string part = eye.name.Substring(0, 4);

            if (part == "dead")
            {
                eye_dead = eye;
            }
            else if (part == "angr")
            {
                eye_angry = eye;
            }
            else if (part == "happ")
            {
                eye_happy = eye;
            }
            else if (part == "hurt")
            {
                eye_hurt = eye;
            }
            else if (part == "atta")
            {
                eye_attack = eye;
            }
        }
    }

    public void LoadEyeAngry(Sprite newEye, bool reset = false)
    {
        eye_angry = newEye;

        if (reset)
            ChangeEye_Attack();
    }

    public override void ModifyRendererFade(float fadeValue, float time)
    {
        base.ModifyRendererFade(fadeValue, time);

        eye_img.DOFade(fadeValue, time);
    }

    public override void animation_Boost(float multiplicator = 1)
    {
        base.animation_Boost(multiplicator);

        ChangeEye_Happy();
    }
}
