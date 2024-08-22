using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class originInfo
{
    public Origin.Value origin;

    public string description, title;
    public Sprite image;

    public List<BaseSpellInfo> spell_base = new List<BaseSpellInfo>();
    public List<SpellGestion.List> spell_actif = new List<SpellGestion.List>();
    public List<SpellGestion.List> spell_inactif = new List<SpellGestion.List>();

    public List<Origin_Passive.Value> passive = new List<Origin_Passive.Value>();

    public void addBase(BaseSpellInfo l)
    {
        spell_base.Add(l);
    }

    public void addPassive(Origin_Passive.Value l)
    {
        passive.Add(l);
    }

    public void addActif(SpellGestion.List l)
    {
        spell_actif.Add(l);
    }

    public void addInactive(SpellGestion.List l)
    {
        spell_inactif.Add(l);
    }

    public originInfo(string description, string title, Sprite s,Origin.Value origin)
    {
        this.origin = origin;
        this.description = description;
        this.title = title;
        this.image = s;
    }
}

public class BaseSpellInfo
{
    public SpellGestion.List spell;

    public int levelRequired;

    public BaseSpellInfo(SpellGestion.List spell, int levelRequired)
    {
        this.spell = spell;
        this.levelRequired = levelRequired;
    }
}