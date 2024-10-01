using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LayerCollectable;
using UnityEngine;
using UnityEngine.UI;

public class Collectable_Chest_Choice : Collectable, ICollectable
{
    public CollectableStatic.Type type { get => CollectableStatic.Type.chest; }

    public SingleEquipment.type singleEquipmentType;

    public SingleEquipment.rarity rarity;

    public override void Start()
    {
        base.Start();

        if (rarity != SingleEquipment.rarity.random)
            return;

        rarity = Equipment_Card.ChooseRarity();

        SetContourColor();
    }

    public override void Collect(bool Erase = true)
    {
        SoundManager.PlaySound(SoundManager.list.equipment_open);

        (bool find, List<SingleEquipment> ls) = Equipment_Card.SearchChoiceEquipement(3, 0, new List<SingleEquipment.type> { singleEquipmentType }, rarity);

        if (!find)
        {
            V.player_info.GainXp(Collectable_Xp.CalculateXpGain(V.player_info.level));
        }
        else
        {
            Equipment_Card.CreateChoiceCard(ls);
        }

        base.Collect(Erase);
    }

    public override void ShowTitle()
    {
        string desc = "";

        if (V.IsFr())
            desc = "Equipement";
        else
            desc = "Equipment";

        if (!Scene_Main.isMouseOverAWindow)
            Main_UI.Display_Title(desc, transform.position, 1.5f);
    }

    public static Collectable_Chest_Choice Create(
    SingleEquipment.type AutorizedType = SingleEquipment.type.none, SingleEquipment.rarity rar = SingleEquipment.rarity.None)
    {
        Collectable_Chest_Choice script = Instantiate(CollectableStatic.chest_Choice).GetComponent<Collectable_Chest_Choice>();

        script.singleEquipmentType = AutorizedType;

        script.rarity = rar;

        return script;
    }

    public Image contour;

    public void SetContourColor()
    {
        contour.color = V.GetColor(SingleEquipment.getColorFromRarity_global(rarity));
    }

    public override void Anim_Apparition()
    {
        base.Anim_Apparition();

        StartCoroutine(contour_Animation());
    }

    public IEnumerator contour_Animation()
    {
        contour.DOFade(0, 0);

        yield return new WaitForSeconds(AnimApparition_WaitTime);

        contour.DOFade(1, 0.2f);
    }

    public override void Update()
    {
        base.Update();

        if (collected)
        {
            Remove(true);
        }
    }

    public class Save_chest_choice : Save
    {
        public SingleEquipment.type type;

        public SingleEquipment.rarity rarity;

        public Save_chest_choice(SingleEquipment.type type, SingleEquipment.rarity rarity)
        {
            this.type = type;
            this.rarity = rarity;
        }

        public override bool cannotBeCreated()
        {
            return collected;
        }

        public override Collectable Create()
        {
            if (cannotBeCreated()) return null;

            return Collectable_Chest_Choice.Create(type, rarity);
        }
    }

    public override Save ExportSave()
    {
        Save save = new Save_chest_choice(singleEquipmentType, rarity);

        attachedSave = save;

        return save;
    }
}