using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Window_skill : Window
{
    public static bool IsRandomized = false;

    [HideInInspector]
    public List<BaseSpellInfo> spell_base = new List<BaseSpellInfo>();

    [HideInInspector]
    public List<SpellGestion.List> spell_inactif = new List<SpellGestion.List>();

    [HideInInspector]
    public List<SpellGestion.List> spell_actif = new List<SpellGestion.List>();

    public List<TreeSkill> ps_third = new List<TreeSkill>();

    public List<TreeSkill> ps_second = new List<TreeSkill>();

    public List<TreeSkill> ps_first = new List<TreeSkill>();

    public List<TreeSkill> ps_base = new List<TreeSkill>();

    public static List<SpellGestion.List> choosenSkillSave = new List<SpellGestion.List>();

    public TreePassiveElement passive;

    public void InitSpellLs()
    {
        passive.SetInfo(Origin_Passive.GetPlayerPassive());
        spell_base = Origin.Get_Base();
        spell_inactif = Origin.Get_Inactive();
        spell_actif = Origin.Get_Actif();
    }

    public void RandomizeSpell()
    {
        InitSpellLs();

        int i = 0;

        foreach (TreeSkill p in ps_base)
        {
            BaseSpellInfo bas = spell_base[i];

            p.LockedByLevel_val = bas.levelRequired;

            p.spell = bas.spell;

            p.SetInfo(this);

            i++;
        }

        if (IsRandomized)
        {
            i = 0;

            foreach (TreeSkill p in ps_first)
            {

                p.spell = choosenSkillSave[i];

                p.SetInfo(this);

                i++;
            }

            foreach (TreeSkill p in ps_second)
            {

                p.spell = choosenSkillSave[i];

                p.SetInfo(this);

                i++;
            }

            return;
        }

        choosenSkillSave.Clear();

        IsRandomized = true;

        List<SpellGestion.List> ls = randomizeTreeSkillList(spell_actif, ps_first);

        ls.AddRange(spell_inactif);

        ls = randomizeTreeSkillList(ls, ps_second);

        ls = randomizeTreeSkillList(ls, ps_third);

        /*
        int o = 0;
        while(o < ps_second.Count)
        {
            TreeSkill skill = ps_second[o];

            if(skill.spell == SpellGestion.List.none)
                Destroy(skill.gameObject);
            else
                o++;
        }

        o = 0;
        while (o < ps_third.Count)
        {
            TreeSkill skill = ps_second[o];

            if (skill.spell == SpellGestion.List.none)
            {
                Destroy(skill.gameObject);
            }
            else
                o++;
        }*/
    }

    public List<SpellGestion.List> randomizeTreeSkillList(List<SpellGestion.List> listSpell, List<TreeSkill> listTreeSkill)
    {
        List<SpellGestion.List> lnew = new List<SpellGestion.List>(listSpell);

        foreach (TreeSkill p in listTreeSkill)
        {
            if (lnew.Count == 0) { p.SetInfo(this); continue; }

            int i = Random.Range(0, lnew.Count);

            SpellGestion.List l = lnew[i];

            choosenSkillSave.Add(l);

            p.spell = l;

            p.SetInfo(this);

            lnew.RemoveAt(i);
        }

        return lnew;
    }
}
