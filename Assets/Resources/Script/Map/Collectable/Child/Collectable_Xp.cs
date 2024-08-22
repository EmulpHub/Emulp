using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using LayerCollectable;

public class Collectable_Xp : Collectable, ICollectable
{
    public CollectableStatic.Type type { get => CollectableStatic.Type.xp; }

    public static int CalculateXpGain(int level)
    {
        return Mathf.CeilToInt(level * 2 + 2 + 2);
    }

    public int level;

    public override void Collect(bool Erase = false)
    {
        if (collected)
            return;

        collected = true;

        V.player_info.GainXp(CalculateXpGain(level));

        Spell.Reference.CreateParticle_Leaf(transform.position, 1.2f);
        Spell.Reference.CreateParticle_Leaf(transform.position, 1.2f);
        Spell.Reference.CreateParticle_Leaf(transform.position, 1.2f);

        transform.DOShakeScale(1, 1, 3);

        Main_UI.Display_movingText_basicValue(V.IsFr() ? "Récupéré" : "Collected", V.Color.green, transform.position);

        base.Collect(false);

    }

    public Sprite SpRecolted, SpNonRecolted;

    public override void Update()
    {
        base.Update();

        img.sprite = collected ? SpRecolted : SpNonRecolted;

        if (collected)
        {
            Remove(false);
        }
    }

    public override void AnimationManagement()
    {
        if (collected)
            return;

        base.AnimationManagement();
    }

    public override void ShowTitle()
    {
        if (collected)
            return;

        string desc = "";

        if (V.IsFr())
            desc = "+" + CalculateXpGain(level) + " XP";
        else
            desc = "+" + CalculateXpGain(level) + " XP";


        if (!Scene_Main.aWindowIsUsed)
            Main_UI.Display_Title(desc, transform.position, 1.5f);
    }


    public static Collectable_Xp Create(int level, bool collected = false)
    {
        Collectable_Xp script = Instantiate(CollectableStatic.xp).GetComponent<Collectable_Xp>();

        script.level = level;

        script.collected = collected;

        return script;
    }

    public class Save_xp : Save
    {
        public int level;

        public Save_xp(int level)
        {
            this.level = level;
        }


        public override bool cannotBeCreated()
        {
            return collected;
        }

        public override Collectable Create()
        {
            if (cannotBeCreated()) return null;

            return Collectable_Xp.Create(level, collected);
        }
    }

    public override Save ExportSave()
    {
        Save save = new Save_xp(level);

        attachedSave = save;

        return save;
    }
}
