using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ProgressionBarXp : MonoBehaviour
{
    public void Start()
    {
        Xp = V.player_info.xp;
        Xp_max = V.player_info.xp_max;
        level = V.player_info.level;
    }

    public float Scale_Y_normal, Scale_Y_Big;
    public float Level_Normal, Level_Big;

    public float speed_blue, speed_blue_light;

    public float exeTimer;

    public Color Normal, RecompenseAvailable;

    public float changeColorSpeed;

    public GameObject VisualEffectWhenGainReward;

    public Vector3 VisualEffectReward_scale;

    public float VisualEffect_scaleSpd;

    void Update()
    {
        Progression_Fill.fillAmount = Xp / Xp_max;
        //Level.text = "Niveau " + level;

        if (levelGained > 0)
        {

            VisualEffectWhenGainReward.transform.DOScale(VisualEffectReward_scale, VisualEffect_scaleSpd);
        }
        else
        {
            VisualEffectWhenGainReward.transform.DOScale(Vector3.zero, VisualEffect_scaleSpd);
        }

        string txt = V.IsFr() ? "RECOMPENSE !" : "REWARD !";

        if (levelGained > 1)
            txt += " (" + levelGained + ")";

        exeTimer -= 1 * Time.deltaTime;

        if (XpToGain > 0 || exeTimer > 0)
        {
            if (TimeBeforeSTart < Time.time && exeTimer <= 0)
            {
                float toGain = 0;
                if (Xp + XpToGain > Xp_max)
                {
                    toGain = XpToGain - (Xp + XpToGain - Xp_max);
                }
                else
                    toGain = XpToGain;

                exeTimer = speed_blue + 0.1f;

                Xp += toGain;

                XpToGain -= toGain;
            }
        }
        else
        {
            CurrentlyRising = false;
        }

        if (Xp >= Xp_max)
        {
            Progression_Fill.DOKill();

            SoundManager.PlaySound(SoundManager.list.player_levelUp_2_silent);

            Progression_Fill.fillAmount = 0;
            XpToGainSave = XpToGain;
            Xp_beforeGain = 0;
            level++;
            Xp = 0;
            Xp_max = V.player_info.CalcXpMax(level);
            CanHaveReward = true;
            levelGained++;

            Spell.Reference.CreateParticle_Bonus(new Vector3(0, -1.8f, 0), 3, Spell.Particle_Amount._48);
            Main_UI.Display_movingText(V.IsFr() ? "+1 niveau" : "+1 level", V.Color.blue, new Vector3(0, -1.8f, 0), 1, 1.5f);
        }

        if (CanHaveReward)
        {
            ShowReward();
            CanHaveReward = false;
        }
    }

    [HideInInspector]
    public bool CanHaveReward;
    [HideInInspector]
    public int levelGained = 0;

    public void ShowReward()
    {
        if (V.game_state == V.State.fight)
            return;

        UI_LevelUp.ShowLevelUp(levelGained);

        levelGained = 0;
    }

    public Image Progression_Fill;

    [HideInInspector]
    public int level = 1;
    [HideInInspector]
    public float Xp, Xp_max, Xp_beforeGain, XpToGainSave;

    [HideInInspector]
    public float XpToGain;

    public float TimeBeforeSTart;

    bool CurrentlyRising = false;

    public void GainXp(float nb)
    {
        Main_UI.Display_movingText("+" + nb + " xp", V.Color.blue, new Vector3(-5.93f, -1.92f, 0), 1, 1.5f + Mathf.Clamp((float)nb / 20, 0, 5));

        if (XpToGain <= 0 && !CurrentlyRising)
            TimeBeforeSTart = Time.time + 0.1f;

        XpToGain += nb;

        XpToGainSave = XpToGain;

        Xp_beforeGain = Xp;

        CurrentlyRising = true;
    }
}