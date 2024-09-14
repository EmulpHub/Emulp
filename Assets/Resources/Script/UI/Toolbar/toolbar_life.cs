using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toolbar_life : MonoBehaviour
{
    private void Start()
    {
        V.player_info.event_armor_gain.Add(event_gainArmor);
        V.player_info.event_armor_loose.Add(event_loosingArmor);
        V.player_entity.event_life_dmg.Add(event_damage);
        V.player_entity.event_life_heal.Add(event_heal);

        PlayerInfo.event_calculateValue.Add(event_calculateValue);

        shaderAnimation_Life = gameObject.AddComponent<toolbar_shaderAnimation>();
        shaderAnimation_Armor = gameObject.AddComponent<toolbar_shaderAnimation>();
        shaderAnimation_Life.material = fillAmount_life.material;
        shaderAnimation_Armor.material = fillAmount_armor.material;

        event_calculateValue();
        UpdateBrightLife();
        UpdateBrightArmor();
    }

    public void Update()
    {
        SetText();
    }

    #region updateLife

    public float fillAmountSpeed;
    public Image fillAmount_life, fillAmount_armor;

    void update_fillAmount()
    {
        float life_max = V.player_info.Life_max + V.player_info.armor;
        float life = V.player_info.Life;
        float armor = V.player_info.armor;

        float fill_life = life / life_max;
        float fill_armor = fill_life + armor / life_max;

        fillAmount_life.DOKill();
        fillAmount_life.DOFillAmount(fill_life, fillAmountSpeed).SetEase(Ease.OutQuint);

        fillAmount_armor.DOKill();
        fillAmount_armor.DOFillAmount(fill_armor, fillAmountSpeed).SetEase(Ease.OutQuint);

    }

    void event_calculateValue ()
    {
        update_fillAmount();
        UpdateNoiseScale();
    }

    void event_gainArmor(float armorGain)
    {
        ArmorOffsetGainAnimation(armorGain);
        UpdateBrightArmor();

        update_fillAmount();
        UpdateNoiseScale();

        modifier_armorGain(armorGain);
    }

    void event_loosingArmor(float looseArmor)
    {
        ArmorOffsetDamageAnimation(looseArmor);
        UpdateBrightArmor();

        update_fillAmount();
        UpdateNoiseScale();

        modifier_damage(looseArmor);
    }

    void event_damage(InfoDamage info)
    {
        OffsetDamageAnimation(info.damage);

        UpdateBrightLife();
        update_fillAmount();
        UpdateNoiseScale();

        modifier_damage(info.damage);
    }

    void event_heal(InfoHeal info)
    {
        update_fillAmount();
        UpdateNoiseScale();

        modifier_heal(info.heal);

        if (V.player_entity.InfoPlayer.Life == V.player_entity.InfoPlayer.Life_max)
            return;

        UpdateBrightLife();

        OffsetHealAnimation(info.heal);
    }

    #endregion

    #region Text

    public Text txt_life, txt_armor;

    public void SetText()
    {
        int life = Mathf.CeilToInt(V.player_info.Life);
        int armor = V.player_info.armor;

        txt_armor.gameObject.SetActive(armor > 0);

        txt_armor.text = "" + armor;
        txt_life.text = "" + life;
    }

    public Text txt_modifier;
    public Outline txt_modifierOutline;

    public Color color_damage, color_heal, color_armor;

    public void modifier_damage (float amount)
    {
        txt_modifier.text = "-" + amount;
        txt_modifier.color = color_damage;

        modifier_apparition();
    }

    public void modifier_heal (float amount)
    {
        txt_modifier.text = "+" + amount;
        txt_modifier.color = color_heal;

        modifier_apparition();
    }

    public void modifier_armorGain (float amount)
    {
        txt_modifier.text = "+" + amount;
        txt_modifier.color = color_armor;

        modifier_apparition();
    }

    public float modifierApparitionSpeed,modifierDestroySpeed,modifierScaleNormal,modifierScaleBig;

    public void modifier_apparition ()
    {
        txt_modifier.DOKill();
        
        txt_modifier.DOFade(1, 0);
        txt_modifierOutline.DOFade(1, 0);

        txt_modifier.DOFade(0, modifierDestroySpeed);
        txt_modifierOutline.DOFade(0, modifierDestroySpeed / 2);

        txt_modifier.transform.DOScale(modifierScaleBig, 0);
        txt_modifier.transform.DOScale(modifierScaleNormal, modifierApparitionSpeed);
    }

    #endregion

    #region LifeOffset

    public float offsetSpeed, offsetMultiplier;

    private toolbar_shaderAnimation shaderAnimation_Life,shaderAnimation_Armor;

    public void OffsetDamageAnimation (float damage)
    {
        float ratio = damage / V.player_entity.Info.Life_max;

        ratio = Mathf.Clamp(ratio,0.5f,1);

        shaderAnimation_Life.Offset_start(ratio * offsetMultiplier,offsetSpeed);
    }

    public void OffsetHealAnimation(float heal)
    {
        float ratio = heal / V.player_entity.Info.Life_max;

        ratio = Mathf.Clamp(ratio, 0.5f, 1);

        ratio *= -1;

        shaderAnimation_Life.Offset_start(ratio * offsetMultiplier, offsetSpeed);
    }


    #endregion

    #region Armor

    public void ArmorOffsetDamageAnimation(float damage)
    {
        float ratio = damage / V.player_entity.Info.Life_max;

        ratio = Mathf.Clamp(ratio, 0.5f, 1);

        shaderAnimation_Armor.Offset_start(ratio * offsetMultiplier, offsetSpeed);
    }

    public void ArmorOffsetGainAnimation(float gain)
    {
        float ratio = gain / V.player_entity.Info.Life_max;

        ratio = Mathf.Clamp(ratio, 0.5f, 1);

        ratio *= -1;

        shaderAnimation_Armor.Offset_start(ratio * offsetMultiplier, offsetSpeed);
    }

    #endregion

    #region NoiseScale 

    public float NoiseScale_toAdd_armor,NoiseScale_toAdd_life, NoiseScaleSpeed;

    public void UpdateNoiseScale ()
    {
        float noiseScaleLife = Mathf.Clamp(V.player_entity.InfoPlayer.Life / 10, 0, 100);

        noiseScaleLife += NoiseScale_toAdd_life;

        shaderAnimation_Life.NoiseScale_set(noiseScaleLife, NoiseScaleSpeed);

        float noiseScaleArmor = Mathf.Clamp(V.player_entity.InfoPlayer.armor / 2, 0, 100);

        noiseScaleArmor += NoiseScale_toAdd_armor;

        shaderAnimation_Armor.NoiseScale_set(noiseScaleArmor, NoiseScaleSpeed);
    }

    #endregion

    #region Bright 

    public float life_Bright_max, life_Bright_min,Bright_speed;
    public float armor_Bright_max, armor_Bright_min;

    public void UpdateBrightArmor()
    {
        shaderAnimation_Armor.Bright_set(armor_Bright_max, armor_Bright_min, Bright_speed);
    }

    public void UpdateBrightLife()
    {
        shaderAnimation_Life.Bright_set(life_Bright_max, life_Bright_min, Bright_speed);
    }

    #endregion
}
