using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class AscensionInfo : MonoBehaviour, IPointerClickHandler
{
    public AscensionInfo_Holder holder;

    public static GameObject Create(int ascension, Transform parent)
    {
        GameObject g = Instantiate(Resources.Load<GameObject>("Prefab/UI/AscensionScene/AscensionInfo"), parent);

        AscensionInfo script = g.GetComponent<AscensionInfo>();

        script.Init(ascension, parent.GetComponent<AscensionInfo_Holder>());

        return g;
    }

    public void OnPointerClick(PointerEventData data)
    {/*
        if (locked)
            return;*/

        holder.MakeMovement(ascension);

        Ascension.SetNewAscension(ascension);
    }

    public int ascension;

    public void Init(int asc, AscensionInfo_Holder h)
    {
        holder = h;
        ascension = asc;

        GetComponent<RectTransform>().anchoredPosition = new Vector2(holder.calcX(asc), 0);

        SetText();
    }

    public GameObject button_start;

    public bool locked;

    public GameObject LockedGraphique;

    public void Update()
    {
        locked = ascension > Ascension.HigherUnlockedAscension;

        button_start.gameObject.SetActive(Ascension.currentAscension == ascension && !locked);

        LockedGraphique.gameObject.SetActive(locked);
    }

    public Text Title, effect;

    public void Button()
    {
        if (Ascension.currentAscension != ascension)
            return;

        V.EraseNonStaticVar();

        V.eventLoadingNewScene.Call();

        SceneManager.LoadScene("Main");
    }

    public void SetText()
    {
        if (ascension == 0)
        {
            Title.text = "Normal";
            effect.text = V.IsFr() ? "- Pas de modification -" : "- No Modifiers -";
        }
        else
        {
            Title.text = "Ascension " + ascension;
            effect.text = Ascension.ConvertAscensionModifierIntoString(Ascension.calcAscensionModifier(ascension, true));
        }
    }
}
