using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shark : MonsterInfo
{
    public override void AddPassiveEffect()
    {
        string Effect_title = V.IsFr() ? "Ecaille" : "Scale";

        Effect e = Effect.CreateCustomEffect(0, V.shark_effect_sprite, Effect.Reduction_mode.permanent, Effect_title, new List<Effect>
                 {
                    Effect.CreateEffectForCustom(Effect.effectType.maximumLifePercentage,100),
                    Effect.CreateEffectForCustom(Effect.effectType.tacklePercentage,100)
                 });

        holder.AddEffect(e);

        holder_monster.Passive_Effect = e;
    }

    public override void SetAvailableSpell()
    {
        holder_monster.allAvailableStats.Clear();

        AddSpellStats(SpellGestion.List.monster_shark_attack);
        AddSpellStats(SpellGestion.List.monster_teleport_shark);
    }

    public override string Title()
    {
        return V.IsFr() ? "Requin" : "Shark";
    }
}

public class archer : MonsterInfo
{
    public override void AddPassiveEffect()
    {
        string Effect_title = V.IsFr() ? "Precision" : "Accuracy";

        string desc = V.IsFr() ? "Ne peux pas attaquer une cible a moins de *spe3 case de distance*end" : "Can't attack a target at less than *spe3 square away*end";

        Effect e = Effect.CreateCustomEffect(0, V.fleeing_effect_sprite, Effect.Reduction_mode.permanent, Effect_title, new List<Effect>
        {
                    Effect.CreateEffectForCustom(Effect.effectType.power,20),
                    Effect.CreateEffectTxtForCustom(desc)
        });

        holder.AddEffect(e);

        holder_monster.Passive_Effect = e;
    }

    public override void SetAvailableSpell()
    {
        holder_monster.allAvailableStats.Clear();

        AddSpellStats(SpellGestion.List.monster_archer_attack);
        AddSpellStats(SpellGestion.List.monster_archer_sprint);
    }

    public override string Title()
    {
        return V.IsFr() ? "Archer" : "Archer";
    }
}

public class funky : MonsterInfo
{
    public override void AddPassiveEffect()
    {
        string Effect_title = V.IsFr() ? "Masque du pleutre" : "Fleeing mask";

        Effect e = Effect.CreateCustomEffect(0, V.fleeing_effect_sprite, Effect.Reduction_mode.permanent, Effect_title, new List<Effect>
        {
                    Effect.CreateEffectForCustom(Effect.effectType.leakPercentage,30)
        });

        holder.AddEffect(e);

        holder_monster.Passive_Effect = e;
    }

    public override void SetAvailableSpell()
    {
        holder_monster.allAvailableStats.Clear();

        AddSpellStats(SpellGestion.List.monster_funky_recoil);
        AddSpellStats(SpellGestion.List.monster_funky_attack);
    }
    public override string Title()
    {
        return V.IsFr() ? "Funky" : "Funky";
    }
}

public class vala : MonsterInfo
{
    public override void AddPassiveEffect()
    {
        string Effect_title = V.IsFr() ? "Puissance volcanique" : "Volcanic power";

        Effect e = Effect.CreateCustomEffect(0, V.volcanicPower_effect_sprite, Effect.Reduction_mode.permanent, Effect_title, new List<Effect>
{
                        Effect.CreateEffectForCustom(Effect.effectType.maximumLifePercentage,175),
                    Effect.CreateEffectForCustom(Effect.effectType.power,20),
                    Effect.CreateEffectForCustom(Effect.effectType.tacklePercentage,100),
                    Effect.CreateEffectForCustom(Effect.effectType.leakPercentage,50),
                    Effect.CreateEffectForCustom(Effect.effectType.boost_pa,1),

                    Effect.CreateEffectForCustom(Effect.effectType.boost_pm,1),
});

        holder.AddEffect(e);

        holder_monster.Passive_Effect = e;
    }

    public override void SetAvailableSpell()
    {
        holder_monster.allAvailableStats.Clear();

        AddSpellStats(SpellGestion.List.monster_vala_attack);
        AddSpellStats(SpellGestion.List.monster_vala_fireThrowing);
        AddSpellStats(SpellGestion.List.monster_vala_invokeInflamed);
    }
    public override string Title()
    {
        return V.IsFr() ? "Vala" : "Vala";
    }
}
public class grassy : MonsterInfo
{
    public override void AddPassiveEffect()
    {
        string Effect_title = V.IsFr() ? "Puissance des herbes" : "Grass power";
        string desc = V.IsFr() ? "Peut boostez ses alliés" : "Can boost his allies";

        Effect e = Effect.CreateCustomEffect(0, V.grassy_effect_sprite, Effect.Reduction_mode.permanent, Effect_title, new List<Effect>
{
    Effect.CreateEffectForCustom(Effect.effectType.maximumLife,15),
    Effect.CreateEffectTxtForCustom(desc)
});

        holder.AddEffect(e);

        holder_monster.Passive_Effect = e;
    }

    public override void SetAvailableSpell()
    {
        holder_monster.allAvailableStats.Clear();

        AddSpellStats(SpellGestion.List.monster_grassy_boost);
        AddSpellStats(SpellGestion.List.monster_grassy_attack);
    }
    public override string Title()
    {
        return V.IsFr() ? "Herbeux" : "Grassy";
    }
}

public class magnetic : MonsterInfo
{
    public override void AddPassiveEffect()
    {
        string Effect_title = "Metal";
        string desc = V.IsFr() ? "Peut vous attirer jusqu'a lui" : "Can attract you toward him";

        Effect e = Effect.CreateCustomEffect(0, V.magnetic_metal, Effect.Reduction_mode.permanent, Effect_title, new List<Effect>
{
    Effect.CreateEffectForCustom(Effect.effectType.maximumLifePercentage,40),
            Effect.CreateEffectForCustom(Effect.effectType.tacklePercentage,60),

    Effect.CreateEffectTxtForCustom(desc)
});

        holder.AddEffect(e);

        holder_monster.Passive_Effect = e;
    }

    public override void SetAvailableSpell()
    {
        holder_monster.allAvailableStats.Clear();

        AddSpellStats(SpellGestion.List.monster_magnetic_attack);
        AddSpellStats(SpellGestion.List.monster_magnetic_attract);
    }
    public override string Title()
    {
        return V.IsFr() ? "Magnetic" : "Magnetic";
    }
}

public class junior : MonsterInfo
{
    public override void AddPassiveEffect()
    {
        string Effect_title = "Junior";

        Effect e = Effect.CreateCustomEffect(0, V.monster_juniorPassive, Effect.Reduction_mode.permanent, Effect_title, new List<Effect>
            {
                Effect.CreateEffectTxtForCustom(V.IsFr() ? "Il est jeune" : "he's young"),
                Effect.CreateEffectForCustom(Effect.effectType.boost_pa, -2),
                Effect.CreateEffectForCustom(Effect.effectType.boost_pm, 1),
                Effect.CreateEffectForCustom(Effect.effectType.maximumLifePercentage,-40)
            });

        holder.AddEffect(e);

        holder_monster.Passive_Effect = e;
    }

    public override void SetAvailableSpell()
    {
        holder_monster.allAvailableStats.Clear();
        AddSpellStats(SpellGestion.List.monster_normal_attack);

    }
    public override string Title()
    {
        return V.IsFr() ? "Junior" : "Junior";
    }
}

public class normal : MonsterInfo
{
    public override void AddPassiveEffect()
    {
        string Effect_title = "Normal";

        Effect e = Effect.CreateCustomTxtEffect(Effect_title, V.IsFr() ? "No effect" : "Pas d'effets", 0, V.monster_normalPassive, Effect.Reduction_mode.permanent);

        holder.AddEffect(e);

        holder_monster.Passive_Effect = e;
    }

    public override void SetAvailableSpell()
    {
        holder_monster.allAvailableStats.Clear();

        AddSpellStats(SpellGestion.List.monster_normal_attack);
    }
}
public class inflamed : MonsterInfo
{
    public override void AddPassiveEffect()
    {
        string Effect_title = "Normal";

        Effect e = Effect.CreateCustomTxtEffect(Effect_title, V.IsFr() ? "No effect" : "Pas d'effets", 0, V.monster_normalPassive, Effect.Reduction_mode.permanent);

        holder.AddEffect(e);

        holder_monster.Passive_Effect = e;
    }

    public override void SetAvailableSpell()
    {
        holder_monster.allAvailableStats.Clear();

        AddSpellStats(SpellGestion.List.monster_inflamed_attack);
    }
    public override string Title()
    {
        return V.IsFr() ? "Enflammé" : "Inflamed";
    }
}