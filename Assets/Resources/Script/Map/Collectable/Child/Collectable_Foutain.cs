using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LayerCollectable;

public class Collectable_Foutain : Collectable, ICollectable
{
    public CollectableStatic.Type type { get => CollectableStatic.Type.foutain; }

    public override void Collect(bool Erase = true)
    {
        if (!card_Talent.CreateChoiceCard())
            V.player_info.GainXp(Mathf.CeilToInt(V.player_info.xp_max / 10) + 10);

        SoundManager.PlaySound(SoundManager.list.equipment_open);

        base.Collect(Erase);
    }

    public override void ShowTitle()
    {
        string desc = V.IsFr() ? "Nouveau talent" : "New talent";

        if (!Scene_Main.aWindowIsUsed)
            Main_UI.Display_Title(desc, transform.position, 1.5f);
    }

    public static Collectable_Foutain Create(bool collected = false)
    {
        Collectable_Foutain collectable = Instantiate(CollectableStatic.foutain).GetComponent<Collectable_Foutain>();

        collectable.collected = collected;

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

    public class Save_foutain : Save
    {

        public override bool cannotBeCreated()
        {
            return collected;
        }

        public override Collectable Create()
        {
            if (cannotBeCreated()) return null;

            return Collectable_Foutain.Create(collected);
        }
    }

    public override Save ExportSave()
    {
        Save_foutain save = new Save_foutain();

        attachedSave = save;

        return save;
    }
}
