using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class toolbar_stats : MonoBehaviour
{
    public static toolbar_stats instance;

    private void Start()
    {
        instance = this;

        V.player_info.event_armor_gain.Add(manage_fa);
        V.player_info.event_armor_loose.Add(manage_fa);
        V.player_entity.event_life_dmg.Add(manage_fa);
        V.player_entity.event_life_heal.Add(manage_fa);

        PlayerInfo.event_calculateValue.Add(manage_fa);
    }

    private void Update()
    {
        setVariable();
        setTxt();
    }

    #region commonVariable

    public float fa_speed;

    float life, armor, pa, pm;

    Entity player;

    void setVariable()
    {
        life = Mathf.CeilToInt(V.player_info.Life);
        armor = V.player_info.armor;
        pa = V.player_info.PA;
        pm = V.player_info.PM;
        player = V.player_entity;
    }

    #endregion

    #region fillAmount

    public Image fa_life, fa_armor, txt_exp;

    void manage_fa()
    {
        float life_max = V.player_info.Life_max + V.player_info.armor;
        float life = V.player_info.Life;
        float armor = V.player_info.armor;

        float fill_life = life / life_max;
        float fill_armor = fill_life + armor / life_max;

        fa_life.DOKill();
        fa_life.DOFillAmount(fill_life, fa_speed).SetEase(Ease.OutQuart);

        fa_armor.DOKill();
        fa_armor.DOFillAmount(fill_armor, fa_speed).SetEase(Ease.OutQuart);

    }

    #endregion

    #region exp

    public void txt_expUpdate(float gainXp)
    {
        StopAllCoroutines();
        StartCoroutine(text_expUpdate(gainXp));
    }

    public IEnumerator text_expUpdate(float gainXp)
    {
        int level = V.player_info.level;

        float xpSave = V.player_info.xp, xpMaxSave = V.player_info.xp_max;

        float fill_exp = (xpSave + gainXp) / xpMaxSave;

        float speed = 1.2f;

        bool up = false;

        if (fill_exp > 1) { up = true; fill_exp = 1; }

        txt_exp.DOKill();
        txt_exp.DOFillAmount(fill_exp, speed).SetEase(Ease.OutQuart);

        if (up)
        {
            yield return new WaitForSeconds(speed + 0.4f);

            float fill_new = (xpSave + gainXp - xpMaxSave) / V.player_info.CalcXpMax(level + 1);

            txt_exp.fillAmount = 0;

            txt_exp.DOKill();
            txt_exp.DOFillAmount(fill_new, speed).SetEase(Ease.OutQuart);
        }
    }

    #endregion

    #region text

    public Text txt_life, txt_pa, txt_pm;

    void setTxt()
    {
        if (armor > 0)
            txt_life.text = life + "+" + armor;
        else
            txt_life.text = "" + life;

        txt_pa.text = "" + pa;
        txt_pm.text = "" + pm;
    }

    #endregion
}
