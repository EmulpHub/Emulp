using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class toolbar_stats : MonoBehaviour
{
    private void Start()
    {
        PlayerInfo.event_player_xpGain.Add(image_experienceUpdateCallCoroutine);
    }

    private void Update()
    {
        setVariable();
        setTxt();
    }

    #region commonVariable

    public float fillAmountModifierSpeed;

    float  pa, pm;

    void setVariable()
    {
        pa = V.player_info.PA;
        pm = V.player_info.PM;
    }

    #endregion

    #region exp

    public Image imageExperience;

    public void image_experienceUpdateCallCoroutine(float gainXp)
    {
        StopAllCoroutines();
        StartCoroutine(image_experienceUpdate(gainXp));
    }

    public IEnumerator image_experienceUpdate(float gainXp)
    {
        int level = V.player_info.level;

        float xpSave = V.player_info.xp, xpMaxSave = V.player_info.xp_max;

        float fill_exp = (xpSave + gainXp) / xpMaxSave;

        float speed = 1.2f;

        bool up = false;

        if (fill_exp > 1) { up = true; fill_exp = 1; }

        imageExperience.DOKill();
        imageExperience.DOFillAmount(fill_exp, speed).SetEase(Ease.OutQuart);

        if (up)
        {
            yield return new WaitForSeconds(speed + 0.4f);

            float fill_new = (xpSave + gainXp - xpMaxSave) / V.player_info.CalcXpMax(level + 1);

            imageExperience.fillAmount = 0;

            imageExperience.DOKill();
            imageExperience.DOFillAmount(fill_new, speed).SetEase(Ease.OutQuart);
        }
    }

    #endregion

    #region text

    public Text txt_pa, txt_pm;

    void setTxt()
    {
        txt_pa.text = "" + pa;
        txt_pm.text = "" + pm;
    }

    #endregion
}
