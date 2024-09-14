using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Scene_Main : MonoBehaviour
{
    public void PassiveManagement()
    {

        if (Input.GetMouseButtonDown(0) && SpellGestion.selectionnedSpell_list != SpellGestion.List.empty)
        {
            bool IsMouseOverToolbar = false;

            foreach (Spell sp in SpellInToolbar.activeSpell_script)
            {
                if (sp.mouseIsOver)
                {
                    IsMouseOverToolbar = true;
                    break;
                }
            }

            if (!IsMouseOverToolbar)
            {
                SpellGestion.selectionnedSpell = null;
                SpellGestion.selectionnedSpell_list = SpellGestion.List.empty;
            }
        }
    }
}
