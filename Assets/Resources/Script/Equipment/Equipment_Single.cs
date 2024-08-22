using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Reflection.Emit;

public class SingleEquipment : MonoBehaviour
{

    public static Dictionary<(rarity rar, value_type type), (int primary, int secondary)> activeAndBonusEffect =
        new Dictionary<(rarity rar, value_type type), (int primary, int secondary)>();

    public static void activeAndBonusEffect_init()
    {
        if (activeAndBonusEffect.Count > 0)
            return;

        rarity curRar = rarity.Common;

        void add(value_type t, int active, int secondary = 0)
        {
            activeAndBonusEffect.Add((curRar, t), (active, secondary));
        }

        add(value_type.life, 10);
        add(value_type.mastery, 5);
        add(value_type.damage, 1);

        curRar = rarity.Uncommon;

        add(value_type.life, 25, 10);
        add(value_type.mastery, 10, 5);
        add(value_type.damage, 2, 1);
        add(value_type.cc, 20, 10);

        curRar = rarity.Rare;

        add(value_type.life, 40, 25);
        add(value_type.mastery, 30, 15);
        add(value_type.damage, 4, 2);
        add(value_type.cc, 30, 20);

        add(value_type.po, 1);
    }

    public static Dictionary<string, SingleEquipment> Ls = new Dictionary<string, SingleEquipment>();

    public static SingleEquipment GetEquipFromName(string sp)
    {
        return Ls[sp.ToLower()];
    }

    public static void CreateLs(Transform parent, bool first = true)
    {
        activeAndBonusEffect_init();

        if (first)
            Ls.Clear();

        foreach (Transform c in parent)
        {
            if (c.name[0] != '$')
            {
                SingleEquipment a = c.GetComponent<SingleEquipment>();

                a.Init();

                try
                {
                    Ls.Add(a.name.ToLower(), a);
                }
                catch (System.Exception ex)
                {
                    print("Ex avec a.name = " + a.Graphic.name + " et ex = " + ex.Message);
                }
            }

            if (c.transform.childCount > 0)
                CreateLs(c, false);
        }
    }

    public enum rarity
    {
        Common, Uncommon, Rare, None, random
    }

    public static string ConvertRarityIntoString(rarity rarity)
    {
        if (V.IsFr())
        {
            if (rarity == rarity.Common)
            {
                return "Commun";
            }
            else if (rarity == rarity.Uncommon)
            {
                return "Inhabituel";
            }
            else if (rarity == rarity.Rare)
            {
                return "Rare";
            }
        }
        else if (V.IsUk())
        {
            if (rarity == rarity.Common)
            {
                return "Common";
            }
            else if (rarity == rarity.Uncommon)
            {
                return "Uncommon";
            }
            else if (rarity == rarity.Rare)
            {
                return "Rare";
            }
        }

        throw new System.Exception("RARITY ARGUMENT IS NOT GOOD IN CONVERTRARITYINTOSTRING OF EQUIPMENT SINGLE");
    }

    public static V.Color getColorFromRarity_global(rarity rar)
    {
        switch (rar)
        {
            case rarity.Common:
                return V.Color.common;
            case rarity.Uncommon:
                return V.Color.uncommon;
            case rarity.Rare:
                return V.Color.rare;
        }

        throw new System.Exception("NO VALID RARITY OR NOT CREATED YET equipment single getColorFromRarity");
    }

    public V.Color getColorFromRarity()
    {
        return getColorFromRarity_global(Rarity);
    }

    public string ConvertRarityIntoString()
    {
        return ConvertRarityIntoString(Rarity);
    }

    public rarity Rarity;

    public enum value_type
    {
        life, mastery, damage, tackle, leak, pa, pm, po, special_spike, none, cc, ec, armor, heal, lifeSteal
    }

    public enum type
    {
        helmet,
        chest,
        belt,
        pant,
        boot,
        weapon,
        object_equipment,
        object_equipment_2,
        none
    }

    public type Type;

    public static string ConvertTypeIntoString(type type)
    {
        if (V.IsFr())
        {
            switch (type)
            {
                case type.helmet:
                    return "Casque";
                case type.chest:
                    return "Plastron";
                case type.belt:
                    return "Ceinture";
                case type.pant:
                    return "Pantalon";
                case type.boot:
                    return "Botte";
                case type.weapon:
                    return "Arme";
                case type.object_equipment:
                    return "Objet";
                case type.object_equipment_2:
                    return "Objet";
            }
        }
        else if (V.IsUk())
        {
            switch (type)
            {
                case type.helmet:
                    return "Helmet";
                case type.chest:
                    return "Chest";
                case type.belt:
                    return "Belt";
                case type.pant:
                    return "Pant";
                case type.boot:
                    return "Boot";
                case type.weapon:
                    return "Weapon";
                case type.object_equipment:
                    return "Object";
                case type.object_equipment_2:
                    return "Object";
            }
        }

        throw new System.Exception("RARITY ARGUMENT IS NOT GOOD IN CONVERTRARITYINTOSTRING OF EQUIPMENT SINGLE");
    }

    public string ConvertTypeIntoString()
    {
        return ConvertTypeIntoString(Type);
    }

    public Sprite Graphic;

    public string fr_title, fr_desc;
    public string uk_title, uk_desc;

    public string GetTitle()
    {
        return V.IsFr() ? fr_title : uk_title;
    }

    public string GetDescription()
    {
        return V.IsFr() ? fr_desc : uk_desc;
    }

    public static List<SingleEquipment.value_type> OrderToShowElement = new List<SingleEquipment.value_type>
    {SingleEquipment.value_type.life,SingleEquipment.value_type.mastery,SingleEquipment.value_type.damage,SingleEquipment.value_type.special_spike,
        value_type.cc,value_type.ec,value_type.armor,value_type.heal,value_type.lifeSteal,SingleEquipment.value_type.pa,SingleEquipment.value_type.pm,SingleEquipment.value_type.po,SingleEquipment.value_type.tackle,SingleEquipment.value_type.leak };

    public string getSpellEffect()
    {
        if (Type == type.object_equipment || Type == type.weapon)
        {
            return descColor.convert(SpellGestion.Get_Description(Spell));
        }

        return "";
    }

    public List<string> GetDescriptionEffect()
    {
        if (Type == type.object_equipment || Type == type.weapon)
        {
            return new List<string> { SpellGestion.Get_Description(Spell) };
        }

        List<string> ls = new List<string>();

        Dictionary<value_type, int> dic = getEffectToDic();

        foreach (value_type t in OrderToShowElement)
        {
            if (dic.ContainsKey(t))
            {
                ls.Add(Window_Equipment.ShowEquipment_StatToString(dic[t], t));

            }
        }

        return ls;
    }

    public string GetDescriptionEffectIntoString()
    {

        string desc = "";

        if (Effect != EquipClass_init.Value.none) desc += EquipClass_init.GetInfo(Effect).description + "\n";

        List<string> ls = GetDescriptionEffect();

        foreach (string a in ls)
        {
            desc += a + "\n";
        }

        if (desc.Length > 2)
            desc = desc.Substring(0, desc.Length - 1);

        return descColor.convert(desc);
    }

    public Color32 GetColor_High()
    {
        return Scene_Main.ChooseColorFromRarityHigh(Rarity);
    }

    public Color32 GetColor()
    {
        return Scene_Main.ChooseColorFromRarity(Rarity);
    }

    public Sprite GetIcon()
    {
        if (Rarity == rarity.Common)
        {
            return V.icon_equipment_common;
        }
        if (Rarity == rarity.Rare)
        {
            return V.icon_equipment_rare;
        }
        else if (Rarity == rarity.Uncommon)
        {
            return V.icon_equipment_uncommon;
        }

        throw new System.Exception("mauvaise icon GetIcon equipment_single");
    }

    public void Init()
    {
        InitEffect();

        InitEffectObject();
    }

    public value_type primaryEffect;

    public List<value_type> secondaryEffect = new List<value_type>();

    public void InitEffect()
    {
        effects_type.Clear();
        effects_strenght.Clear();

        void add(value_type e, bool primary)
        {
            if (e == value_type.none) return;

            int val = 0;

            if (primary)
            {
                val = activeAndBonusEffect[(Rarity, e)].primary;
            }
            else
            {
                val = activeAndBonusEffect[(Rarity, e)].secondary;
            }


            if (val == 0)
                throw new System.Exception("primary ou secondary effect pas valide pour equipment = " + fr_title);

            if (effects_type.Contains(e))
            {
                int i = effects_type.IndexOf(e);

                effects_strenght[i] += val;

                return;
            }

            effects_type.Add(e);
            effects_strenght.Add(val);
        }


        add(primaryEffect, true);

        foreach (value_type t in secondaryEffect)
        {
            add(t, false);
        }
    }

    [HideInInspector]
    public List<value_type> effects_type = new List<value_type>();
    [HideInInspector]
    public List<int> effects_strenght = new List<int>();

    public void CreateEffects()
    {
        if (effects_type.Count != effects_strenght.Count)
            throw new System.Exception("UNVALID EFFECTS IN EQUIPMENT_SINGLE for name = " + fr_title);

        for (int i = 0; i < effects_type.Count; i++)
        {
            effects.Add((effects_strenght[i], effects_type[i]));
        }
    }

    public List<(int value, value_type type)> effects = new List<(int value, value_type type)>();

    private Dictionary<value_type, int> convertEffectToDic = new Dictionary<value_type, int>();

    public Dictionary<value_type, int> getEffectToDic()
    {
        if (convertEffectToDic.Count == 0)
        {
            int i = 0;

            foreach (value_type t in effects_type)
            {
                convertEffectToDic.Add(t, effects_strenght[i]);

                i++;
            }
        }

        return convertEffectToDic;
    }

    public SpellGestion.List Spell;

    public void ModifyValue(value_type t, int newValue)
    {
        int i = effects_type.IndexOf(t);

        effects_strenght[i] = newValue;
    }

    public EquipClass_init.Value Effect;

    EquipClass EffectObject;

    public void InitEffectObject()
    {
        if (Effect == EquipClass_init.Value.none) return;

        EffectObject = EquipClass_init.CreateClass(Effect);
    }

    public void Equip()
    {
        if (Effect == EquipClass_init.Value.none) return;

        EffectObject.Add();
    }

    public void UnEquip()
    {

        if (Effect == EquipClass_init.Value.none) return;

        EffectObject.Remove();
    }
}
