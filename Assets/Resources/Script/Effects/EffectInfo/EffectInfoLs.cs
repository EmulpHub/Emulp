using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_additionalSpellCast : Effect
{
    internal override string calcTitle()
    {
        if (V.IsFr())
            return "Double sort";
        else
            return "Doble spell";
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
            return "Le prochain sort est lancé une deuxieme fois avec *dex+" + Str + "%*end d'effet";
        else
            return "The next spell is cast a second time with *dex+" + Str + "%*end effect";
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            if (Str < 0)
            {
                return "*mal" + Str + "%*end";
            }
            else
            {
                return "*bon+" + Str + "%*end";
            }
        };
    }

    public override void EndTurn() { }
}

public class Effect_spikeShard : Effect
{
    internal override string calcTitle()
    {
        return V.IsFr() ? "Fragments épines" : "Spike shard";
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
            return "*bon" + Str + "*end fragments d'épines\n\nA *spe3 fragments*end vous obtenez une nouvelle épine";
        else
            return "*bon" + Str + "*end spike shard\n\nAt *spe3 shard*end you gain a new spike";
    }

    public override void UpdateStr()
    {
        base.UpdateStr();

        int nbSpike = Str / 3;

        if (nbSpike > 0)
        {
            Effect_spike.AddSpike(nbSpike);

            RemoveStr(nbSpike * 3);
        }
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            return "*bon+" + Str + "*end";
        };
    }
}

public class Effect_accumulation : Effect
{
    internal override string calcTitle()
    {
        return "Accumulation";
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
            return "L'accumulation augmente l'effet de certains sort\nCertains sort vous donnent la possibilité d'en gagner\n\nAccumulation: *bon" + Str + "*end";
        else
            return "Accumulation increase effect to some of your spell\nSome of your spell can give accumulation\nAccumulation: *bon" + Str + "*end";
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            return "*bon" + Str + "*end";

        };
    }
}

public class Effect_pv : Effect
{
    internal override string calcTitle()
    {
        return V.IsFr() ? "Vie" : "Healt";
    }

    internal override string calcDescription()
    {
        if (Str < 0)
        {
            return "*mal" + Str + " end vie";
        }
        else
        {
            return "*bon+" + Str + " *end healt";
        }
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            if (Str < 0)
            {
                return "*mal" + Str + " *end";
            }
            else
            {
                return "*bon+" + Str + " *end";
            }
        };
    }
}

public class Effect_Str : Effect
{
    internal override string calcTitle()
    {
        return V.IsFr() ? "Force" : "Strenght";
    }

    internal override string calcDescription()
    {
        if (Str < 0)
        {
            if (V.IsFr())
                return "*mal" + Str + " *end force";
            else
                return "*mal" + Str + " end strenght";
        }
        else
        {
            if (V.IsFr())
                return "*bon+" + Str + " %*end force";
            else
                return "*mal" + Str + "*end strenght";
        }
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            if (Str < 0)
            {
                return "*mal" + Str + " *end";
            }
            else
            {
                return "*bon+" + Str + " *end";

            }
        };
    }

}

public class Effect_warrior_reducePowerReduction : Effect
{

    internal override string calcTitle()
    {
        return "reduce";
    }

    internal override string calcDescription()
    {
        return "reduce";
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            if (Str < 0)
            {
                return "*mal" + Str + "% *end";
            }
            else
            {
                return "*bon+" + Str + " %*end";

            }
        };
    }

}


public class Effect_power : Effect
{
    public override void setAddMode()
    {
        addMode = AddMode.multiplication;
    }

    internal override string calcTitle()
    {
        if (Str < 0)
        {
            return "Fatigue";
        }
        else
        {
            return "Puissance";
        }
    }

    internal override string calcDescription()
    {
        if (Str < 0)
            return "*mal" + Str + "%*end de dégâts infligés";
        else
            return "*bon+" + Str + "%*end de dégâts infligés";
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            if (Str < 0)
            {
                return "*mal" + Str + "% *end";
            }
            else
            {
                return "*bon+" + Str + " %*end";

            }
        };
    }

}

public class Effect_Spellx2_oneUse : Effect
{
    internal override string calcTitle()
    {
        return "Spell x2";
    }

    internal override string calcDescription()
    {
        return V.IsFr() ? "Votre prochain sort est lancé *bon2*end fois" : "Your next spell is launched *bon2*end times";
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            return "";
        };
    }

}


public class Effect_lifeSteal : Effect
{
    public override void setAddMode()
    {
        addMode = AddMode.multiplication;
    }
    internal override string calcTitle()
    {
        if (V.IsFr())
            return "Vol de vie";
        else
            return "Life steal";
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
            return "Infligez des dégâts vous soigne de *bon" + Str + "% *end du total infligés";
        else
            return "Dealing damage heal you *bon" + Str + "% *end of the amount";
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            return "*bon+" + Str + "% *end";
        };
    }

}


public class Effect_spikeAdditionalDamage : Effect
{
    internal override string calcTitle()
    {
        return V.IsFr() ? "Dégâts épines" : "Spike damage";
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
            return "Vos épines infligent +*bon" + Str + "*end dégâts";
        else
            return "Your spike deal +*bon" + Str + "*end damage";
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            return "*bon+" + Str + " *end";
        };
    }

}

public class Effect_untacklable : Effect
{
    public override void setAddMode()
    {
        addMode = AddMode.addition;
    }

    internal override string calcTitle()
    {
        if (V.IsFr())
            return "Untaclable";
        else
            return "Untacklable";
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
            return "Vous ne pouvez pas être taclé";
        else
            return "You can't be tackled";
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            return "";
        };
    }
}

public class Effect_reductionPower : Effect
{
    public override void setAddMode()
    {
        addMode = AddMode.multiplication;
    }
    internal override string calcTitle()
    {
        if (V.IsFr())
            return "Fatigue";
        else
            return "Exhaust";
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
            return "*mal" + Str + "%*end de dégâts infligés";
        else
            return "*mal" + Str + "%*end damage dealt";
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            return "*mal-" + Str + " %*end";

        };
    }

}

public class Effect_custom : Effect
{
    public List<Effect> effectLs;

    internal override string calcTitle()
    {
        return title;
    }

    internal override string calcDescription()
    {
        string newDescription = "";

        foreach (Effect e in effectLs)
        {
            newDescription += e.GetDescription() + ", ";
        }

        if (effectLs.Count > 0)
        {
            newDescription = newDescription.Substring(0, newDescription.Length - 2);
        }

        return newDescription;
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        if (i == null)
        {
            calcInfoString = delegate
            {

                return "";
            };
        }
        else
        {
            calcInfoString = i;
        }
    }
}

public class Effect_customTxt : Effect
{
    public string description;

    internal override string calcTitle()
    {
        return title;
    }

    internal override string calcDescription()
    {
        return description;
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        if (i == null)
        {
            calcInfoString = delegate
            {

                return "";
            };
        }
        else
        {
            calcInfoString = i;
        }
    }
}

public class Effect_Warrior_conflagration : Effect
{
    internal override string calcTitle()
    {
        return "Embrasé";
    }

    internal override string calcDescription()
    {
        return "*spe50%*end de vos dégâts sont gagné en puissance";
    }


    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            return "";
        };
    }

    public static float effect = 1;

    public void damageAnEnnemy(Entity entity, float dmg)
    {
        int power = Mathf.CeilToInt(dmg / 2 * effect);

        Effect.Warrior_AddPower(power);
    }

    int id1;

    public override void eventAdding(Entity holder)
    {
        base.eventAdding(holder);

        id1 = Monster.event_all_monster_dmg.Add(damageAnEnnemy);
    }

    public override void eventKilling()
    {
        base.eventKilling();

        Monster.event_all_monster_dmg.Remove(id1);
    }
}

public class Effect_Warrior_fatigue : Effect
{
    internal override string calcTitle()
    {
        return "Fatigue";
    }

    internal override string calcDescription()
    {
        return "You can't gain power";
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            return "";
        };
    }

    public float rememberConflagrationEffect;

    public override void eventAdding(Entity holder)
    {
        base.eventAdding(holder);

        rememberConflagrationEffect = Effect_Warrior_conflagration.effect;

        Effect_Warrior_conflagration.effect = 0;
    }

    public override void eventKilling()
    {
        base.eventKilling();

        Effect_Warrior_conflagration.effect = rememberConflagrationEffect;
    }
}

public class Effect_Warrior_Power : Effect
{
    internal override string calcTitle()
    {
        return "Puissance";
    }

    internal override string calcDescription()
    {
        return "*bon+" + Warrior_ConvertPowerIntoReal(Str) + "%*end de dégats supplementaire";
    }


    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            return "*bon" + Str + "*end";
        };
    }
}


public class Effect_Warrior_Resistance : Effect
{
    internal override string calcTitle()
    {
        return "Defense";
    }

    internal override string calcDescription()
    {
        return "*bon-" + Warrior_ConvertResistanceIntoReal(Str) + "% *end dégats reçus\n*bon+" + Str + "%*end application de saignement";
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {

            return "*bon" + Str + "*end";
        };

    }
}


public class Effect_boost_pa : Effect
{
    internal override string calcTitle()
    {
        return "Pa";
    }

    internal override string calcDescription()
    {
        return "*bon+" + Str + "*end pa ";
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            if (V.IsFr())
            {
                if (Str > 0)
                    return "*bon+" + Str + "*end pa ";
                else
                    return "*mal" + Str + "*end pa ";
            }
            else
            {
                if (Str > 0)
                    return "*bon+" + Str + "*end ap ";
                else
                    return "*mal" + Str + "*end ap ";
            }
        };
    }
}


public class Effect_resistance : Effect
{
    internal override string calcTitle()
    {
        if (V.IsFr())
        {
            if (Str < 0)
            {
                return "Fragilité";
            }
            else
            {
                return "Resistance";
            }

        }
        else
        {
            if (Str < 0)
            {
                return "Fragility";
            }
            else
            {
                return "Resistance";
            }
        }
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
        {
            if (Str < 0)
            {
                return "*mal+" + Str * -1 + "%*end augmentation des dégâts reçus ";
            }
            else
            {

                return "*bon+" + Str + "%*end reduction des dégâts reçus ";
            }
        }
        else
        {
            if (Str < 0)
            {
                return "*mal+" + Str * -1 + "%*end augmentation des dégâts reçus ";
            }
            else
            {

                return "*bon+" + Str + "%*end reduction des dégâts reçus ";
            }
        }
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            if (Str < 0)
            {
                return "*mal" + Str + "%*end def";
            }
            else
            {
                return "*bon+" + Str + "%*end def";
            }
        };

    }
}


public class Effect_mastery : Effect
{
    internal override string calcTitle()
    {
        if (V.IsFr())
        {
            return "Maitrise";
        }
        else
        {
            return "Mastery";
        }
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
            return "*bon+" + Str + "*end maitrise ";
        else
            return "*bon+" + Str + "*end mastery ";
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            if (V.IsFr())
                return "*bon+" + Str + "*end mai";
            else
                return "*bon+" + Str + "*end mas";
        };

    }
}


public class Effect_maximumLife : Effect
{
    internal override string calcTitle()
    {
        if (V.IsFr())
        {
            return "Vie maximum";
        }
        else
        {
            return "Maximum life";
        }
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
            return "*bon+" + Str + "*end vie maximum ";
        else
            return "*bon + " + Str + " * end vie maximum";
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            if (V.IsFr())
                return "*bon+" + Str + "*end pdv max";
            else
                return "*bon+" + Str + "*end max pv";
        };
    }
}


public class Effect_tackle : Effect
{
    internal override string calcTitle()
    {
        if (V.IsFr())
        {
            return "Taclé";
        }
        else
        {
            return "Taclé";
        }
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
            return "";
        else
            return "";
    }
}

public class Effect_masteryPercentage : Effect
{
    internal override string calcTitle()
    {
        if (V.IsFr())
        {
            return "Maitrise";
        }
        else
        {
            return "Mastery";
        }
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
            return "*bon+" + Str + "%*end maitrise ";
        else
            return "*bon+" + Str + "%*end mastery ";
    }

    public override void setAddMode()
    {
        addMode = AddMode.multiplication;
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            if (V.IsFr())
                return "*bon+" + Str + "%*end mai";
            else
                return "*bon+" + Str + "%*end mas";
        };
    }
}

public class Effect_maximumLifePercentage : Effect
{
    public override void setAddMode()
    {
        addMode = AddMode.multiplication;
    }

    internal override string calcTitle()
    {
        if (V.IsFr())
        {
            return "Vie maximum";
        }
        else
        {
            return "Maximum life";
        }
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
            return "*bon+" + Str + "%*end vie maximum ";
        else
            return "*bon+" + Str + "%*end maximum life";
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            if (V.IsFr())
                return "*bon+" + Str + "%*end pdv max";
            else
                return "*bon+" + Str + "%*end max pv";
        };
    }
}

public class Effect_tacklePercentage : Effect
{
    public override void setAddMode()
    {
        addMode = AddMode.multiplication;
    }

    internal override string calcTitle()
    {
        if (V.IsFr())
        {
            return "Tacle";
        }
        else
        {
            return "Tackle";
        }
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
            return (Str > 0 ? "*bon+" + Str + "%*end" : "*mal" + Str + "%*end") + " tacle ";
        else
            return (Str > 0 ? "*bon+" + Str + "%*end" : "*mal" + Str + "%*end") + " tackle ";
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            string suffixe = "tac";

            if (Str > 0)
                return "*bon" + Str + "%*end " + suffixe;
            else
                return "*mal" + Str + "%*end " + suffixe;
        };
    }
}

public class Effect_leakPercentage : Effect
{
    public override void setAddMode()
    {
        addMode = AddMode.multiplication;
    }

    internal override string calcTitle()
    {
        if (V.IsFr())
        {
            return "Fuite";
        }
        else
        {
            return "Leak";
        }
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
            return (Str > 0 ? "*bon+" + Str + "%*end" : "*mal" + Str + "%*end") + " fuite ";
        else
            return (Str > 0 ? "*bon+" + Str + "%*end" : "*mal" + Str + "%*end") + " leak ";
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            string suffixe = V.IsFr() ? "fui" : "lea";

            if (Str > 0)
                return "*bon" + Str + "%*end " + suffixe;
            else
                return "*mal" + Str + "%*end " + suffixe;
        };
    }
}

public class Effect_boost_pm : Effect
{

    internal override string calcTitle()
    {
        if (V.IsFr())
        {
            return "Pm";
        }
        else
        {
            return "Mp";
        }
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
            return "*bon+" + Str + "*end pm ";
        else
            return "*mal" + Str + "*end pm ";
    }

    public override void SetInfoString(calcStringValue i = null)
    {

        calcInfoString = delegate
        {
            if (V.IsFr())
            {
                if (Str > 0)
                    return "*bon+" + Str + "*end pm ";
                else
                    return "*mal" + Str + "*end pm ";
            }
            else
            {
                if (Str > 0)
                    return "*bon+" + Str + "*end mp ";
                else
                    return "*mal" + Str + "*end mp ";
            }
        };

    }
}


public class Effect_reducePaOnSpecifiedSpell : Effect
{
    internal override string calcTitle()
    {
        if (V.IsFr())
        {
            return "reduce pa";
        }
        else
        {
            return "reduce pa";
        }
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
            return "reduce pa";
        else
            return "reduce pa";
    }

}


public class Effect_damage : Effect
{

    internal override string calcTitle()
    {
        if (V.IsFr())
            return "Dommage";
        else
            return "Dommage";
    }
    internal override string calcDescription()
    {
        if (V.IsFr())
        {
            return (Str > 0 ? "*bon+" + Str + "*end" : "*mal" + Str + "*end") + " dommage ";
        }
        else
        {
            return (Str > 0 ? "*bon+" + Str + "*end" : "*mal" + Str + "*end") + " damage ";
        }
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {

            if (V.IsFr())
            {
                if (Str < 0)
                    return "*mal" + Str + "*end dmg";
                else
                    return "*bon+" + Str + "*end dmg";
            }
            else
            {
                if (V.IsFr())
                    return "*bon+" + Str + "*end dmg";
                else
                    return "*mal" + Str + "*end dmg";
            }
        };

    }
}


public class Effect_damage_oneUse : Effect
{

    internal override string calcTitle()
    {
        if (V.IsFr())
            return "Dommage";
        else
            return "Dommage";
    }
    internal override string calcDescription()
    {
        if (V.IsFr())
        {
            return (Str > 0 ? "*bon+" + Str + "*end" : "*mal" + Str + "*end") + " dommage ";
        }
        else
        {
            return (Str > 0 ? "*bon+" + Str + "*end" : "*mal" + Str + "*end") + " damage ";
        }
    }


    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {

            if (V.IsFr())
            {
                if (Str < 0)
                    return "*mal" + Str + "*end dmg";
                else
                    return "*bon+" + Str + "*end dmg";
            }
            else
            {
                if (V.IsFr())
                    return "*bon+" + Str + "*end dmg";
                else
                    return "*mal" + Str + "*end dmg";
            }
        };

    }
}


public class Effect_damage_multipleUse : Effect
{

    internal override string calcTitle()
    {
        if (V.IsFr())
            return "Dommage";
        else
            return "Dommage";
    }
    internal override string calcDescription()
    {
        if (V.IsFr())
        {
            return (Str > 0 ? "*bon+" + Str + "*end" : "*mal" + Str + "*end") + " dommage ";
        }
        else
        {
            return (Str > 0 ? "*bon+" + Str + "*end" : "*mal" + Str + "*end") + " damage ";
        }
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {

            if (V.IsFr())
            {
                if (Str < 0)
                    return "*mal" + Str + "*end dmg";
                else
                    return "*bon+" + Str + "*end dmg";
            }
            else
            {
                if (V.IsFr())
                    return "*bon+" + Str + "*end dmg";
                else
                    return "*mal" + Str + "*end dmg";
            }
        };

    }

    public override void EndTurn()
    {
    }

    public void costOneUse()
    {
        ReduceDuration(1);
    }

    int id1;

    public override void eventAdding(Entity holder)
    {
        base.eventAdding(holder);

        id1 = spellEffect.event_player_afterAction_playingSpell_damageable.Add(costOneUse);
    }

    public override void eventKilling()
    {
        base.eventKilling();

        spellEffect.event_player_afterAction_playingSpell_damageable.Remove(id);
    }
}

public class Effect_po : Effect
{

    internal override string calcTitle()
    {
        if (V.IsFr())
            return "Po";
        else
            return "Po";
    }
    internal override string calcDescription()
    {
        if (V.IsFr())
        {
            return (Str > 0 ? "*bon+" + Str + "*end" : "*mal" + Str + "*end") + " portée ";
        }
        else
        {
            return (Str > 0 ? "*bon+" + Str + "*end" : "*mal" + Str + "*end") + " range ";
        }
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            if (Str < 0)
            {
                return "*mal" + Str + "*end po";
            }
            else
            {
                return "*bon+" + Str + "*end po";
            }
        };

    }
}


public class Effect_custom_magic : Effect
{
    internal override string calcTitle()
    {
        if (V.IsFr())
            return "Magie";
        else
            return "Magic";
    }
    internal override string calcDescription()
    {
        if (V.IsFr())
        {
            return "Augmente les dégats de base de *bon Baguette magique *end de " + Str + "*end";
        }
        else
        {
            return "Increase base damage of *bon Magic wand *end by " + Str + "*end";
        }
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            return "*bon" + Str + "*end";
        };
    }
}

public class Effect_heal : Effect
{
    internal override string calcTitle()
    {
        if (V.IsFr())
            return "Soins";
        else
            return "Heal";
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
        {
            return "*bon+" + Str + "%*end soins";
        }
        else
        {
            return "*bon+" + Str + "%*end heal";
        }
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            string suf = V.IsFr() ? "soin" : "heal";
            if (Str < 0)
                return "*bon+" + Str + "%*end " + suf;
            else
                return "*mal" + Str + "%*end " + suf;
        };
    }
}

public class Effect_armor : Effect
{
    internal override string calcTitle()
    {
        if (V.IsFr())
            return "Armure";
        else
            return "Armor";
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
        {
            return "*bon+" + Str + "%*end armure";
        }
        else
        {
            return "*bon+" + Str + "%*end armor";
        }
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            string suf = V.IsFr() ? "armure" : "armor";
            if (Str < 0)
                return "*bon+" + Str + "%*end " + suf;
            else
                return "*mal" + Str + "%*end " + suf;
        };
    }
}

public class Effect_armor_oneUse : Effect
{
    internal override string calcTitle()
    {
        if (V.IsFr())
            return "Armure";
        else
            return "Armor";
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
        {
            return "*bon+" + Str + "%*end armure pour votre prochain gain d'armure";
        }
        else
        {
            return "*bon+" + Str + "%*end armor gor your next armor gain";
        }
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            string suf = V.IsFr() ? "armure" : "armor";
            if (Str < 0)
                return "*bon+" + Str + "%*end " + suf;
            else
                return "*mal" + Str + "%*end " + suf;
        };
    }
}

public class Effect_bleeding : Effect
{
    internal override string calcTitle()
    {
        if (V.IsFr())
            return "Saignement";
        else
            return "Bleeding";
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
        {
            return "*dmg-" + Str + " pdv*end au début du tour";
        }
        else
        {
            return "*dmg-" + Str + " pdv*end au début du tour";
        }
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            return "*dmg-" + Str + "*end pdv";
        };
    }
}

public class Effect_focus : Effect
{
    public override void setAddMode()
    {
        addMode = AddMode.multiplication;
    }

    internal override string calcTitle()
    {
        if (V.IsFr())
            return "Focus";
        else
            return "Focus";
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
        {
            return "La prochaine application de saignement a *malx" + Str + "*end";
        }
        else
        {
            return "Next bleeding application have *malx" + Str + "*end";
        }
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            return "*mal+" + Str + "%*end";
        };
    }
}

public class Effect_additionalSpellArea : Effect
{
    internal override string calcTitle()
    {
        if (V.IsFr())
            return "Zone d'effet";
        else
            return "Area effect";
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
        {
            return "*bon+" + Str + "*end à la zone d'effet de vos sorts";
        }
        else
        {
            return "*bon+" + Str + "*end to the effect area of your spell";
        }
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            string suf = V.IsFr() ? "zone" : "area";

            return "*bon+" + Str + "*end " + suf;
        };
    }
}

public class Effect_effectPercentage : Effect
{
    internal override string calcTitle()
    {
        if (V.IsFr())
            return "Effet";
        else
            return "Effect";
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
        {
            return "*bon" + Str + "%*end d'effets";
        }
        else
        {
            return "*bon" + Str + "%*end d'effets";
        }
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            if (Str < 0)
            {
                return "*mal" + Str + "%*end eff";
            }
            else
            {
                return "*bon+" + Str + "%*end eff";
            }
        };
    }
}

public class Effect_effectPercentage_OneUse : Effect
{
    public override void setAddMode()
    {
        addMode = AddMode.multiplication;
    }
    internal override string calcTitle()
    {
        if (V.IsFr())
            return "Effet";
        else
            return "Effect";
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
        {
            return "*bon" + Str + "%*end d'effets";
        }
        else
        {
            return "*bon" + Str + "%*end d'effets";
        }
    }
    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            if (Str < 0)
                return "*mal" + Str + "%*end eff";

            else
                return "*bon" + Str + "%*end eff";
        };
    }
}

public class Effect_heal_OneUse : Effect
{
    internal override string calcTitle()
    {
        if (V.IsFr())
            return "Soin";
        else
            return "Heal";
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
        {
            return "Votre prochain soin a *mal" + Str + "%*end";
        }
        else
        {
            return "Your next heal have *mal" + Str + "%*end";
        }
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            string suf = V.IsFr() ? "soin" : "hea";

            if (Str < 0)
                return "*mal" + Str + "%*end " + suf;
            else
                return "*bon" + Str + "%*end " + suf;
        };
    }

    public override void eventAdding(Entity holder)
    {
        base.eventAdding(holder);

        idEvent = holder.event_life_heal.Add(effectWhenHeal);
    }

    int idEvent;

    private void effectWhenHeal(InfoHeal info)
    {
        Kill(false);

        holder.event_life_heal.Remove(idEvent);
    }
}

public class Effect_spikePercentage : Effect
{
    public override void setAddMode()
    {
        addMode = AddMode.multiplication;
    }

    internal override string calcTitle()
    {
        if (V.IsFr())
            return "Soin";
        else
            return "Heal";
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
        {
            return "Votre prochain soin a *mal" + Str + "%*end";
        }
        else
        {
            return "Your next heal have *mal" + Str + "%*end";
        }
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            string su = V.IsFr() ? "épi" : "spi";

            return "*bon" + Str + "%*end " + su;
        };

    }
}

public class Effect_spike : Effect
{
    internal override string calcTitle()
    {
        if (V.IsFr())
            return "Épines";
        else
            return "Spike";
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
        {
            return "*bon+" + Str + "*end épines\n*dmg" + V.player_info.CalculateSpikeDamage() + "*end dégâts par épine";
        }
        else
        {
            return "*bon+" + Str + "*end spike\n*dmg" + V.player_info.CalculateSpikeDamage() + "*end damage per spike";
        }
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            string su = V.IsFr() ? "épi" : "spi";

            return "*bon" + Str + "*end " + su;
        };
    }


    public static void AddSpike(int nb)
    {
        bool find = false;

        Effect effect = V.player_entity.GetEffect(effectType.spike, ref find);

        int dur = 3;

        if (find)
        {
            effect.AddStr(nb);
            effect.SetDuration(dur);
        }
        else
        {
            V.player_entity.AddEffect(Effect.CreateEffect(effectType.spike, nb, dur, Resources.Load<Sprite>("Image/Sort/warrior/Spike"), Reduction_mode.startTurn));

            Vector2 posVector2 = V.CalcEntityDistanceToBody(V.player_entity);

            CustomEffect_EndlessSpike.create();
        }

        CustomEffect_EndlessSpike.AddDisableTime(1);
    }

    public override void WhenDurationChange(int oldDuration)
    {
        base.WhenDurationChange(oldDuration);

        if (DurationInTurn == 1)
        {
            CustomEffect_EndlessSpike.CurrentEndlessSpike.anim_OneTurnLasting();
        }
        else if (oldDuration == 1 && DurationInTurn > 0)
        {
            CustomEffect_EndlessSpike.CurrentEndlessSpike.anim_Reviving();
        }
    }

    internal override void whenKill()
    {
        base.whenKill();

        CustomEffect_EndlessSpike.CurrentEndlessSpike.anim_kill();
    }
}

public class Effect_criticalHitChance : Effect
{
    internal override string calcTitle()
    {
        if (V.IsFr())
            return "Chance critiqe(cc)";
        else
            return "Critical hit chance(cc)";
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
        {
            return "*bon+" + Str + "%*end de chance critique";
        }
        else
        {
            return "*bon" + Str + "%*end critical hit chance";
        }
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            return "*bon+" + Str + " %*end cc";
        };
    }
}

public class Effect_criticalHitChance_oneUse : Effect
{
    internal override string calcTitle()
    {
        if (V.IsFr())
            return "Chance critiqe(cc)";
        else
            return "Critical hit chance(cc)";
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
        {
            return "*bon+" + Str + "%*end de chance critique";
        }
        else
        {
            return "*bon" + Str + "%*end critical hit chance";
        }
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            return "*bon+" + Str + " %*end cc";
        };
    }

}


public class Effect_ec : Effect
{
    internal override string calcTitle()
    {
        if (V.IsFr())
            return "Effect critiqe(ec)";
        else
            return "Critical effect(ce)";
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
        {
            return "*bon+" + Str + "%*end effet critique ";
        }
        else
        {
            return "*bon" + Str + "%*end critical effect ";
        }
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            if (V.IsFr())
                return "*bon+" + Str + " %*end ec";
            else
                return "*bon+" + Str + " %*end ce";
        };
    }

}

public class Effect_ec_oneUse : Effect
{
    internal override string calcTitle()
    {
        if (V.IsFr())
            return "Effect critiqe(ec)";
        else
            return "Critical effect(ce)";
    }

    internal override string calcDescription()
    {
        if (V.IsFr())
        {
            return "*bon+" + Str + "%*end effet critique ";
        }
        else
        {
            return "*bon" + Str + "%*end critical effect ";
        }
    }

    public override void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            if (V.IsFr())
                return "*bon+" + Str + " %*end ec";
            else
                return "*bon+" + Str + " %*end ce";
        };
    }

}