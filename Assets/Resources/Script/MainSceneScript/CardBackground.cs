using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardBackground : MonoBehaviour
{
    public static CardBackground Create ()
    {
        CardBackground bg = Instantiate(Resources.Load<GameObject>("Script/MainSceneScript/CardBackground")).GetComponent<CardBackground>();

        bg.setValue();

        return bg;
    }

    public void setValue()
    {
        anim = this.gameObject.AddComponent(typeof(AnimationText)) as AnimationText;

        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    public Text title;
    public Image bg;

    public enum Selection { relic,origin}

    [HideInInspector]
    Selection selection;

    [HideInInspector]
    AnimationText anim;

    public void changeSelection (Selection selection)
    {
        this.selection = selection;

        string txt = "ERROR";

        if (selection == Selection.relic)
        {
            txt = V.IsFr() ? "Choisisez une relique" : "Choose a relic";
        }
        else if (selection == Selection.origin)
        {
            txt = V.IsFr() ? "Choisisez votre origine" : "Choose your origin";
        }

        anim.launch(title,txt,0.3f);
    }

    public void Remove ()
    {
        title.DOFade(0, 0.5f).SetEase(Ease.InExpo);
        bg.DOFade(0, 0.5f).SetEase(Ease.InExpo);
        anim.launch(title,"",0,false);
        Destroy(this.gameObject,3);
    }
}
