using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Scene_Main : MonoBehaviour
{
    public void PassiveManagement()
    {
        //And the passive healing
        if (V.player_info.Life < V.player_info.Life_max && !EndCombatScreen.IsScreenActive)
        {
            if (cooldownPassiveHeal <= 0)
            {
                V.player_entity.Heal(new InfoHeal(0.25f * V.player_info.Life_max));
                cooldownPassiveHeal = cooldownPassiveHeal_max;
            }

            cooldownPassiveHeal -= 1 * Time.deltaTime;
        }

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
