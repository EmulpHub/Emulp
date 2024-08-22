using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class EndScreenObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image graphique;

    private void Start()
    {
        Init();
    }

    public virtual void Init()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DisplayInfo();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EraseDisplayInfo();
    }

    public float dis = 0.8f;

    public virtual void DisplayInfo()
    {

    }

    public virtual void EraseDisplayInfo()
    {

    }
}
