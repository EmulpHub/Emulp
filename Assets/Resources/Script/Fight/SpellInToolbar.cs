using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpellInToolbar : MonoBehaviour
{
    public static int totalSpellCount = 12;

    public void ToolBarManagement_Instantiate()
    {
        activeSpell.Clear();

        //GESTION LIST
        int count = totalSpellCount;

        if (activeSpell.Count == 0)
        {
            if (Save_PlayerData.toolbar.Count == 0)
            {

                while (activeSpell.Count <= count)
                {
                    activeSpell.Add(SpellGestion.List.empty);
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {

                    activeSpell.Add(Save_PlayerData.toolbar[i]);
                }
            }
        }
    }


    public static List<SpellGestion.List> activeSpell = new List<SpellGestion.List>();
    public static List<Spell> activeSpell_script = new List<Spell>();

    public static bool contain(SpellGestion.List spell)
    {
        return activeSpell.Contains(spell);
    }

    public static Spell get (SpellGestion.List spell)
    {
        if (!contain(spell)) return null;

        return activeSpell_script.First(b => b.spell == spell);
    }
}
