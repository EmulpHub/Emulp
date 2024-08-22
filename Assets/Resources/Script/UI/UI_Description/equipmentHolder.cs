using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class equipmentHolder : MonoBehaviour
{
    public Text line;

    public SingleEquipment e;

    public int indexToStart;

    public RectTransform thisRect;

    public int policeSize;

    public Color32 txtColor;

    public void init(SingleEquipment a)
    {
        if (e == a)
            return;

        e = a;

        List<SingleEquipment.value_type> ls = new List<SingleEquipment.value_type>(SingleEquipment.OrderToShowElement);

        List<(SingleEquipment.value_type t, int value)> ls_toReturn = new List<(SingleEquipment.value_type t, int value)>();

        while (ls.Count > 0)
        {
            SingleEquipment.value_type type = ls[0];

            int i = 0;

            while (i < e.effects_type.Count)
            {
                if (e.effects_type[i] == type)
                {
                    ls_toReturn.Add((type, e.effects_strenght[i]));
                    // initLine(Window_Equipment.ShowEquipment_StatToString(e.effects_strenght[i], type), type);
                }

                i++;
            }

            ls.RemoveAt(0);
        }

        init(a.getSpellEffect(), ls_toReturn,e);
    }

    public void init(string spellEffect, List<(SingleEquipment.value_type t, int value)> ls, SingleEquipment equipment = null)
    {

        ls = new List<(SingleEquipment.value_type t, int value)>(ls);

        while (transform.childCount > indexToStart)
        {

            DestroyImmediate(transform.GetChild(indexToStart).gameObject);
        }

        if (spellEffect.Length > 0)
        {
            initLine(spellEffect, SingleEquipment.value_type.none);
        }

        if(equipment != null && equipment.Effect != EquipClass_init.Value.none)
        {
            initLine(EquipClass_init.GetInfo(equipment.Effect).description,SingleEquipment.value_type.none);
        }

        while (ls.Count > 0)
        {
            (SingleEquipment.value_type t, int value) v = ls[0];

            initLine(Window_Equipment.ShowEquipment_StatToString(v.value, v.t), v.t);

            ls.RemoveAt(0);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(thisRect);
        Invoke("Force", 0.01f);
    }

    public void Force()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(thisRect);

    }

    public Sprite icon_damage, icon_str, icon_pa, icon_pm, icon_leak, icon_tackle, icon_po, icon_pv, icon_spike, icon_bug, icon_cc, icon_ec, icon_heal, icon_armor;

    public Sprite convertValueTypeIntoSprite(SingleEquipment.value_type i)
    {
        switch (i)
        {
            case SingleEquipment.value_type.damage:
                return icon_damage;

            case SingleEquipment.value_type.mastery:
                return icon_str;

            case SingleEquipment.value_type.life:
                return icon_pv;

            case SingleEquipment.value_type.leak:
                return icon_leak;

            case SingleEquipment.value_type.tackle:
                return icon_tackle;

            case SingleEquipment.value_type.special_spike:
                return icon_spike;

            case SingleEquipment.value_type.pa:
                return icon_pa;

            case SingleEquipment.value_type.pm:
                return icon_pm;

            case SingleEquipment.value_type.po:
                return icon_po;

            case SingleEquipment.value_type.cc:
                return icon_cc;
            case SingleEquipment.value_type.ec:
                return icon_ec;

            case SingleEquipment.value_type.armor:
                return icon_armor;
            case SingleEquipment.value_type.heal:
                return icon_heal;
            case SingleEquipment.value_type.lifeSteal:
                return icon_heal;
            case SingleEquipment.value_type.none:
                return null;
            default:
                return icon_bug;
        }
    }

    public bool equipmentOutline_activate;
    public Color32 equipmentOutline_color;


    public bool spellOutline_activate;
    public Color32 spellOutline_color;

    public void initLine(string str, SingleEquipment.value_type i)
    {
        line.text = str;

        line.fontSize = policeSize;

        line.color = txtColor;

        GameObject g = Instantiate(line, transform).gameObject;

        Image img = g.transform.GetChild(0).GetComponent<Image>();

        Outline o = g.GetComponent<Outline>();

        if (i != SingleEquipment.value_type.none)
        {
            img.sprite = convertValueTypeIntoSprite(i);

            o.enabled = equipmentOutline_activate;
            o.effectColor = equipmentOutline_color;
        }
        else
        {
            img.gameObject.SetActive(false);


            o.enabled = spellOutline_activate;
            o.effectColor = spellOutline_color;
        }
    }
}
