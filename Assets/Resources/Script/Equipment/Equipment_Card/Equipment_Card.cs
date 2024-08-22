using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public partial class Equipment_Card : card
{
    public static void CreateChoiceCard(List<SingleEquipment> ls_equip)
    {
        if (ls_equip.Count == 0)
            return;

        int i = 0, i_max = ls_equip.Count - 1;

        foreach (SingleEquipment e in ls_equip)
        {
            CreateCard(e, i, i_max);

            i++;
        }
    }

    public static SingleEquipment CurrentShowedCard;

    public static void CreateCard(SingleEquipment equipment, int index = 0, int index_max = 0)
    {
        Equipment_Card cardScript = Instantiate(Resources.Load<GameObject>("Prefab/Card/Card_equipment")).GetComponent<Equipment_Card>();

        CurrentShowedCard = equipment;

        cardScript.currentEquipment = equipment;

        cardScript.Init( index, index_max);
    }

    [HideInInspector]
    public SingleEquipment currentEquipment;

    public SpriteRenderer card_Back;

    public Sprite card_Back_common, Card_Back_uncommon, Card_Back_Rare;

    public override void Init(int index = 0, int index_max = 0)
    {
        base.Init(index,index_max);

        card_Back.enabled = true;

        if (currentEquipment.Rarity == SingleEquipment.rarity.Rare)
            card_Back.sprite = Card_Back_Rare;
        else if (currentEquipment.Rarity == SingleEquipment.rarity.Common)
            card_Back.sprite = card_Back_common;
        else
            card_Back.sprite = Card_Back_uncommon;

        ClickAutorization.Add(this.gameObject);

        title.text = currentEquipment.GetTitle();
        desc.text = currentEquipment.GetDescriptionEffectIntoString();

        img.sprite = currentEquipment.Graphic;

        Init_Animation();

        Anim_Discover();
    }
    public override void Update()
    {
        base.Update();

        Anim_Update();

        ManageCursor();
    }

    public override void OnClick()
    {
        base.OnClick();

        if (currentEquipment.Rarity == SingleEquipment.rarity.Common)
        {
            SoundManager.PlaySound(SoundManager.list.equipment_card_open_common);
        }
        else if (currentEquipment.Rarity == SingleEquipment.rarity.Uncommon)
        {
            SoundManager.PlaySound(SoundManager.list.equipment_card_open_unCommon);
        }
        else if (currentEquipment.Rarity == SingleEquipment.rarity.Rare)
        {
            SoundManager.PlaySound(SoundManager.list.equipment_card_open_rare);
        }

        Animation_AcquiredStuff.animation_instantiation(0, img.transform.position, Main_Object.Get(Main_Object.objects.button_equipment).transform.position, img.sprite);

        Equipment_Management.ObtainNewEquipment(currentEquipment);

        CurrentShowedCard = null;

        ClickAutorization.Remove(this.gameObject);

        RemoveAllCurrentCard();
    }
}
