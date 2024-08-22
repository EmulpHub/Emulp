using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public partial class Equipment_Card : card
{
    public Animator card_back;

    public SpriteRenderer card_back_bg;

    public float ShakeScaleStrength;

    public Vector3 ShakeRotationVector;

    public int vibrato_Scale, vibrator_Rotation;

    [HideInInspector]
    public bool StartShowingValue;

    public GameObject  Holder_Canvas;

    public float colorChangeSpeedInSec;
    public float colorPauseMax;
    float timer = 0, pauseTimer = 0;
    bool Higher = false, first = true;

    public void ManageColorChange()
    {
        pauseTimer -= Time.deltaTime;

        if (pauseTimer <= 0)
            timer -= 1 * Time.deltaTime;

        if (timer <= 0)
        {
            Color32 col = new Color32(255, 255, 255, 255);

            if (Higher)
                col = currentEquipment.GetColor_High();
            else
                col = currentEquipment.GetColor();

            ModifyColor(col, colorChangeSpeedInSec);
            timer = colorChangeSpeedInSec;
            Higher = !Higher;

            if (!first)
            {
                pauseTimer = colorPauseMax;
            }
            else
            {
                first = false;
            }
        }
    }

    public List<Image> colorImg = new List<Image>();

    public void ModifyColor(Color32 newColor, float speedInSec)
    {
        foreach (Image i in colorImg)
        {
            i.transform.DOKill();

            i.DOColor(newColor, speedInSec);
        }
    }

}
