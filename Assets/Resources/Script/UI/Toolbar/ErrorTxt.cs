using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ErrorTxt : MonoBehaviour
{
    public static void DisplayError(Spell.StateBeforeLaunch state)
    {
        switch (state)
        {
            case Spell.StateBeforeLaunch.cooldown:
                DisplayError(V.IsFr() ? "Ce sort est en cooldown" : "This spell is on cooldown");
                return;


            case Spell.StateBeforeLaunch.lackOfPa:
                DisplayError(V.IsFr() ? "Pas assez de pa" : "Not enough ap");
                return;



        }

        return;
    }

    public static void DisplayError(string txt)
    {
        V.errorTxt.StopAllCoroutines();

        V.errorTxt.StartCoroutine(V.errorTxt.DisplayMsgError(txt));
    }

    public Text errorTxt;

    public float apparitionSpeed, apparitionTime, destructionSpeed;

    public IEnumerator DisplayMsgError(string txt)
    {
        errorTxt.DOKill();

        errorTxt.text = txt;

        errorTxt.DOFade(1, apparitionSpeed);

        yield return new WaitForSeconds(apparitionTime);

        errorTxt.DOFade(0, destructionSpeed);
    }
}
