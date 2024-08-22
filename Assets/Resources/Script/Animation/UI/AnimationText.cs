using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AnimationText : MonoBehaviour
{
    float speed = 30;

    public void launch(Text txt, string message, float pause = 0, bool resetTitle = true)
    {
        StopAllCoroutines();

        if(message == "")
            StartCoroutine(launchEnumerator_emptyTxt(txt, pause));
        else
            StartCoroutine(launchEnumerator(txt,message,pause,resetTitle));
    }


    IEnumerator launchEnumerator_emptyTxt(Text txt,float pause = 0)
    {
        if (pause != 0) yield return new WaitForSeconds(pause);

        float nb = 0;

        float localSpeed = speed;

        while(txt.text.Length > 0) { 
            txt.text = txt.text.Substring(0,txt.text.Length -1);
            nb++;
            localSpeed *= 1.3f;
            ; yield return new WaitForSeconds(1 / localSpeed);
        }
    }

    IEnumerator launchEnumerator (Text txt, string message,float pause = 0,bool resetTitle = true)
    {
        if (resetTitle) txt.text = "";

        if(pause != 0)
        yield return new WaitForSeconds(pause);

        float nb = 0;

        float localSpeed = speed;

        foreach(char c in message)
        {
            txt.text += c;
            nb++;
            localSpeed *= 1.1f;
            yield return new WaitForSeconds(1 / localSpeed);
        }
    }
}
