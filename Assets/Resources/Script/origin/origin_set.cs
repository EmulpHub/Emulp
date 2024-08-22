using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Origin : MonoBehaviour
{
    static originInfo add(Value l, string title, string description, Sprite sp)
    {
        originInfo a = new originInfo(description, title, sp,l);

        dicValueOriginInfo.Add(l, a);

        return a;
    }

    public static void InitOrigin()
    {
        dicValueOriginInfo.Clear();

        add(Value.none, "None", "no desc", V.CC);

        originInfo o = add(Value.underground, "Souterrain", "Dans un millieu exigu, ou les ressources sont rares et l'environnement hostile, vous avez du encaisser les coups pour mieux frapper", SpellGestion.Get_sprite(SpellGestion.List.warrior_divineSword));

        o.addBase(new BaseSpellInfo(SpellGestion.List.warrior_Punch,0));
        o.addBase(new BaseSpellInfo(SpellGestion.List.warrior_spent, 0));
        o.addBase(new BaseSpellInfo(SpellGestion.List.warrior_strength, 5));

        o.addPassive(Origin_Passive.Value.rage);
        o.addPassive(Origin_Passive.Value.intensity);
        o.addPassive(Origin_Passive.Value.melee);

        o.addActif(SpellGestion.List.warrior_jump);
        o.addActif(SpellGestion.List.warrior_earthTotem);
        o.addActif(SpellGestion.List.warrior_divineSword);
        o.addActif(SpellGestion.List.warrior_os);
        o.addActif(SpellGestion.List.warrior_rockThrow);

        o.addInactive(SpellGestion.List.warrior_heal);
        o.addInactive(SpellGestion.List.warrior_endurance);
        o.addInactive(SpellGestion.List.warrior_execution);
        o.addInactive(SpellGestion.List.warrior_spikeAttack);

        o = add(Value.surface, "Surface", "A la surface, vous avez su utiliser l'espace autour de vous pour ruser et anéantir l'ennemie de loin", SpellGestion.Get_sprite(SpellGestion.List.surface_fieryShot));

        o.addBase(new BaseSpellInfo(SpellGestion.List.surface_arrow,0));
        o.addBase(new BaseSpellInfo(SpellGestion.List.surface_bandage, 3));
        o.addBase(new BaseSpellInfo(SpellGestion.List.surface_turret, 5));

        o.addPassive(Origin_Passive.Value.pawn);
        o.addPassive(Origin_Passive.Value.isolation);
        o.addPassive(Origin_Passive.Value.control);

        o.addActif(SpellGestion.List.surface_fieryShot);
        o.addActif(SpellGestion.List.surface_fallback);
        o.addActif(SpellGestion.List.surface_longBow);
        o.addActif(SpellGestion.List.surface_shuriken);
        o.addActif(SpellGestion.List.surface_sharpenedArrow);
        o.addActif(SpellGestion.List.surface_energy);

        o.addInactive(SpellGestion.List.surface_laceration);
        o.addInactive(SpellGestion.List.surface_jump);
        o.addInactive(SpellGestion.List.surface_vision);
        o.addInactive(SpellGestion.List.surface_woodenShield);

        o = add(Value.norticeLand, "Terre des notrices", "Dans un millieu effrayant, ou sa survie dépend de ses notrices vous avez su utiliser les pierres de pouvoir en vous pour détruire vos alentours", V.pix_bleeding);

        o.addBase(new BaseSpellInfo(SpellGestion.List.norticeSurface_wand,0));
        o.addBase(new BaseSpellInfo(SpellGestion.List.norticeSurface_Optimisation,3));
        o.addBase(new BaseSpellInfo(SpellGestion.List.norticeSurface_meteorite,5));

        o.addPassive(Origin_Passive.Value.overflow);
        o.addPassive(Origin_Passive.Value.rising);
        o.addPassive(Origin_Passive.Value.traveler);

        o.addActif(SpellGestion.List.norticeSurface_energeticWand);
        o.addActif(SpellGestion.List.norticeSurface_whirlwind);
        o.addActif(SpellGestion.List.norticeSurface_BigWand);
        o.addActif(SpellGestion.List.norticeSurface_book);
        o.addActif(SpellGestion.List.norticeSurface_poisonedVial);
        o.addActif(SpellGestion.List.norticeSurface_rapidity);

        o.addInactive(SpellGestion.List.norticeSurface_amplifiedHeal);
        o.addInactive(SpellGestion.List.norticeSurface_amplifiedArmor);
        o.addInactive(SpellGestion.List.norticeSurface_crystalGift);
        o.addInactive(SpellGestion.List.norticeSurface_energy);

        lsChoosableOrigin.Clear();

        lsChoosableOrigin.Add(Value.underground);

        //WORK IN PROGRESS

        //choosableLs.Add(ls.surface);
        //choosableLs.Add(ls.norticeLand);

        Origin_Passive.Initialize();
    }

}
