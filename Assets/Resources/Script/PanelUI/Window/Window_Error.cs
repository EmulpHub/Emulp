using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public abstract partial class Window : MonoBehaviour
{
    public Text error;

    bool error_IsOn;
    float cooldownError;

    public void UpdateError()
    {
        cooldownError -= Time.deltaTime;
        if (cooldownError <= 0 && error_IsOn)
        {
            error_IsOn = false;
            error.DOFade(0, 2);
        }
    }

    public void SendError(string txt)
    {
        error.transform.DOKill();
        error.DOFade(0, 0);
        error.DOFade(1, 1);

        var errorOutline = error.GetComponent<Outline>();

        errorOutline.DOFade(0, 0);
        errorOutline.DOFade(1, 1);

        error.transform.localScale = new Vector3(0.4f, 0.4f, 1);
        error.transform.DOScale(1, 0.5f);
        error.text = txt;

        error_IsOn = true;
        cooldownError = 3;

        SoundManager.PlaySound(SoundManager.list.ui_error);
    }

}
