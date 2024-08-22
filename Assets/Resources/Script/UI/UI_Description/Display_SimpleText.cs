using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display_SimpleText : MonoBehaviour
{
    public Text description;

    public Image img;

    public static Display_SimpleText Display(string description, Sprite Img)
    {
        GameObject g = Instantiate(Resources.Load<GameObject>("Prefab/UI_SimpleText"));

        Display_SimpleText txt = g.GetComponent<Display_SimpleText>();

        txt.Init(description, Img);

        return txt;
    }

    public void Init(string description, Sprite img)
    {
        this.description.text = description;

        this.img.gameObject.SetActive(img != null);

        this.img.sprite = img;
    }
}
