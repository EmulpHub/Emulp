using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Window_Equipment : Window
{
    [HideInInspector]
    public SingleEquipment showedEquipment;

    public GameObject showedEquipment_parent, ShowEquipment_effect_prefab;

    public Text ShowEquipment_title;

    public equipmentHolder equipHolder;

    public Image ShowEquipment_Graphic;

    public void ShowEquipment(SingleEquipment newOne)
    {
        SoundManager.PlaySound(SoundManager.list.ui_buttonPressed);
        showedEquipment = newOne;

        ShowEquipment_Update();
    }

    public void ShowEquipment_Update()
    {
        if (showedEquipment == null)
        {
            showedEquipment_parent.transform.gameObject.SetActive(false);
            return;
        }

        showedEquipment_parent.transform.gameObject.SetActive(true);

        ShowEquipment_Graphic.sprite = showedEquipment.Graphic;

        if (V.IsFr())
        {
            ShowEquipment_title.text = showedEquipment.fr_title;
        }
        else if (V.IsUk())
        {
            ShowEquipment_title.text = showedEquipment.uk_title;
        }

        equipHolder.init(showedEquipment);

        ManageDisplayDescription();
    }

    Slot_Equiped Slot_MouseOver_Old;

    public void ManageDisplayDescription()
    {
        if (Slot_MouseOver != Slot_MouseOver_Old)
        {
            if (Slot_MouseOver == null)
            {
                Description_text.EraseDispay();
            }
            else
            {
                Slot_Inventory.ShowEquipmentInfoDescOrTitle(showedEquipment, transform.position);
            }
        }

        Slot_MouseOver_Old = Slot_MouseOver;
    }

    public Vector2 startPos;

    public float DiffY;

    public Vector2 calcPos(int index)
    {
        return startPos + new Vector2(0, DiffY * -index);
    }

    public static (string title, string desc) ShowEquipment_Description(SingleEquipment e)
    {
        return (e.GetTitle(), e.GetDescriptionEffectIntoString());
    }

    public static string ShowEquipment_StatToString(int strenght, SingleEquipment.value_type type, bool withColor = true)
    {
        string start = "";

        string result = "+" + strenght + " ";

        if (V.IsFr())
        {
            switch (type)
            {
                case SingleEquipment.value_type.life:
                    start = "*res";
                    result += "vie";
                    break;
                case SingleEquipment.value_type.mastery:
                    start = "*str";
                    result += "force";
                    break;
                case SingleEquipment.value_type.tackle:
                    start = "*inl";
                    result += "tacle";
                    break;
                case SingleEquipment.value_type.leak:
                    start = "*inl";
                    result += "fuite";
                    break;
                case SingleEquipment.value_type.pa:
                    start = "*bon";
                    result += "pa";
                    break;
                case SingleEquipment.value_type.pm:
                    start = "*bon";
                    result += "pm";
                    break;
                case SingleEquipment.value_type.damage:
                    start = "*str";
                    result += "dommage";
                    break;
                case SingleEquipment.value_type.po:
                    start = "*bon";
                    result += "po";
                    break;
                case SingleEquipment.value_type.special_spike:
                    start = "*str";
                    result += "épines";
                    break;

                case SingleEquipment.value_type.cc:
                    start = "*bon";
                    result += "% chances critique(cc)";
                    break;
                case SingleEquipment.value_type.ec:
                    start = "*bon";
                    result += "% effets critique(ec)";
                    break;
                case SingleEquipment.value_type.armor:
                    start = "*arm";
                    result += "% armure";
                    break;
                case SingleEquipment.value_type.heal:
                    start = "*hea";
                    result += "% soins";
                    break;
                case SingleEquipment.value_type.lifeSteal:
                    start = "*hea";
                    result += "% life steal";
                    break;
            }
        }
        else
        {
            switch (type)
            {
                case SingleEquipment.value_type.life:
                    start = "*res";
                    result += "life";
                    break;
                case SingleEquipment.value_type.mastery:
                    start = "*str";
                    result += "strenght";
                    break;
                case SingleEquipment.value_type.tackle:
                    start = "*inl";
                    result += "tackle";
                    break;
                case SingleEquipment.value_type.leak:
                    start = "*inl";
                    result += "leak";
                    break;
                case SingleEquipment.value_type.pa:
                    start = "*bon";
                    result += "ap";
                    break;
                case SingleEquipment.value_type.pm:
                    start = "*bon";
                    result += "mp";
                    break;
                case SingleEquipment.value_type.damage:
                    start = "*str";
                    result += "damage";
                    break;
                case SingleEquipment.value_type.po:
                    start = "*bon";
                    result += "po";
                    break;
                case SingleEquipment.value_type.special_spike:
                    start = "*str";
                    result += "spikes";
                    break;
                case SingleEquipment.value_type.cc:
                    start = "*bon";
                    result += "% critical chance(cc)";
                    break;
                case SingleEquipment.value_type.ec:
                    start = "*bon";
                    result += "% critical effect(ce)";
                    break;
                case SingleEquipment.value_type.armor:
                    start = "*arm";
                    result += "% armor";
                    break;
                case SingleEquipment.value_type.heal:
                    start = "*hea";
                    result += "% heal";
                    break;
                case SingleEquipment.value_type.lifeSteal:
                    start = "*hea";
                    result += "% life steal";
                    break;
            }
        }

        if (result == "")
            throw new System.Exception("MAUVAIS VALUE TYPE OU JE SAIS PAS QUOI DANS SHOWEQUIPMENT_StatToString");

        if (withColor)
            return descColor.convert(start + result + "*end");
        else
            return result;
    }
}
