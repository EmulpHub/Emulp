using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LayerCollectable;

public class Collectable_Chest : Collectable, ICollectable
{
    public CollectableStatic.Type type { get => CollectableStatic.Type.chest; }

    public int level;

    public enum GainType { none, All, NoObjectAndWeapon, ObjectAndWeapon, Weapon, Object }

    public GainType gainType;

    public List<SingleEquipment.type> chest_AutorizedType
    {
        get
        {
            if (gainType == GainType.All)
                return new List<SingleEquipment.type>
                {
                    SingleEquipment.type.helmet, SingleEquipment.type.chest,
                    SingleEquipment.type.belt, SingleEquipment.type.pant,
                    SingleEquipment.type.boot,
                    SingleEquipment.type.weapon,
                    SingleEquipment.type.object_equipment
                };
            else if (gainType == GainType.NoObjectAndWeapon)
                return new List<SingleEquipment.type>
                {
                    SingleEquipment.type.helmet, SingleEquipment.type.chest,
                    SingleEquipment.type.belt, SingleEquipment.type.pant,
                    SingleEquipment.type.boot
                };
            else if (gainType == GainType.ObjectAndWeapon)
                return new List<SingleEquipment.type>
                {
                    SingleEquipment.type.weapon,
                    SingleEquipment.type.object_equipment
                };
            else if (gainType == GainType.Weapon)
                return new List<SingleEquipment.type>
                {
                    SingleEquipment.type.weapon,
                };
            else if (gainType == GainType.Object)
                return new List<SingleEquipment.type>
                {
                    SingleEquipment.type.object_equipment
                };
            else
                throw new System.Exception("gainType value is equal to " + gainType.ToString());
        }
    }

    public override void Collect(bool Erase = false)
    {
        SoundManager.PlaySound(SoundManager.list.equipment_open);

        SingleEquipment equipmentChoosen = null;

        (bool findOne, SingleEquipment equip) result = Equipment_Card.SearchNewEquipment(level, chest_AutorizedType);

        //dès que je dis un truc
        if (result.findOne)
            equipmentChoosen = result.equip;

        if (equipmentChoosen == null)
        {
            V.player_info.GainXp(Collectable_Xp.CalculateXpGain(V.player_info.level));
        }
        else
        {
            Equipment_Card.CreateCard(equipmentChoosen);
        }

        base.Collect(Erase);
    }

    public override void ShowTitle()
    {
        string desc = "";

        if (V.IsFr())
            desc = "Coffre";
        else
            desc = "Chest";

        if (!Scene_Main.isMouseOverAWindow)
            Main_UI.Display_Title(desc, transform.position, 1.5f);
    }

    public static Collectable_Chest Create(int level, GainType gainType)
    {
        Collectable_Chest collectable = Instantiate(CollectableStatic.chest).GetComponent<Collectable_Chest>();

        collectable.level = level;

        collectable.gainType = gainType;

        return collectable;
    }

    public override void Update()
    {
        base.Update();

        if (collected)
        {
            Remove(true);
        }
    }

    public class Save_chest : Save
    {
        public int level;

        public GainType gainType;

        public Save_chest(int level, GainType gainType)
        {
            this.level = level;
            this.gainType = gainType;
        }

        public override bool cannotBeCreated()
        {
            return collected;
        }

        public override Collectable Create()
        {
            if (cannotBeCreated()) return null;

            return Collectable_Chest.Create(level, gainType);
        }
    }

    public override Save ExportSave()
    {
        Save_chest save = new Save_chest(level, gainType);

        attachedSave = save;

        return save;
    }
}
