using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionHoverDisplay : MonoBehaviour
{
    public Image image;
    public Text paText;

    public GameObject spellHolder;

    public void SetToSpell (SpellGestion.List spell)
    {
        spellHolder.SetActive(true);

        paText.text = SpellGestion.Get_paCost(spell).ToString();
        image.sprite = SpellGestion.Get_sprite(spell);
    }

    public void SetToSimpleImage (Sprite sprite)
    {
        spellHolder.SetActive(false);

        image.sprite = sprite;
    }
}
