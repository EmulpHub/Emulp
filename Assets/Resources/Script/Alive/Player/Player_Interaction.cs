using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class Player : Entity
{
    public override float Damage(InfoDamage infoDamage)
    {
        if (V.administrator && V.script_Scene_Main_Administrator.Invulnerable)
            return 0;

        if (ContainEffect_byTitle(SpellGestion.Get_Title(SpellGestion.List.object_bloodShield)))
        {
            SoundManager.PlaySound(SoundManager.list.object_BloodShield_Second);

            Main_UI.Display_movingText_basicValue("Block", V.Color.green, transform.position, null);

            RemoveEffect_byTitle(SpellGestion.Get_Title(SpellGestion.List.object_bloodShield));

            return 0;
        }

        base.Damage(infoDamage);

        if (InfoPlayer.spike > 0 && infoDamage.caster != this)
        {
            Action_wait.Add(spike.delay / 2);

            Action_spell_info_player info = new Action_spell_info_player();

            info.spell = Spell.Create(SpellGestion.List.spike);
            info.caster = this;
            info.listTarget = new List<Entity>() { infoDamage.caster };
            info.targetedSquare = infoDamage.caster.CurrentPosition_string;

            Action_spell.Add(info);
        }

        return infoDamage.damage;
    }

    public override void Kill(InfoKill infokill)
    {
        if (isKilled) return;

        if (ContainEffect_byTitle("totem"))
        {
            Main_UI.Display_movingText_basicValue(V.IsFr() ? "PROTEGER" : "PROTECTED", V.Color.green, V.player_entity.transform.position);

            AnimEndless_Render endless = (AnimEndless_Render)AnimEndlessStatic.list_Get("totem");

            endless.DoAnimation(AnimEndless_Render.Anim.impact);

            Spell.Reference.CreateParticle_Leaf(V.player_entity.transform.position, 1.5f);

            SoundManager.PlaySound(SoundManager.list.object_Totem_First);

            Info.Life = 1;

            if (!object_totem.supInfo.activated)
                object_totem.supInfo.activated = true;

            return;
        }

        ResetAllAnimation();

        var windowAscension = WindowInfo.Instance.OpenOrCloseWindow(WindowInfo.type.Ascension) as Window_Ascension;

        windowAscension.info = new EndOfRunInfo(EndOfRunInfo.state.loose);

        Scene_Main.EndOfCombat(false, null, true);
    }
}
