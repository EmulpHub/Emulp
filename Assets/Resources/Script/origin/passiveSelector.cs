using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class passiveSelector : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Origin_Passive.Value passive;

    public Image render;

    public card_origin card;

    private string title, description;

    public void Init(Origin_Passive.Value passive, card_origin theCard)
    {
        this.passive = passive;

        var info = Origin_Passive.Get(passive);

        render.sprite = info.image;
        title = info.title;
        description = info.description;

        card = theCard;
    }


    public void OnPointerClick(PointerEventData pointerEventData)
    {
        card.UseThis(passive);
    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Main_UI.Display_Description(title,description, transform.position, 1);
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Main_UI.Display_Description_Erase();
    }
}
