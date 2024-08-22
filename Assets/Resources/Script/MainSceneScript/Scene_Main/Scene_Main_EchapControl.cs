using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Main_EchapControl : MonoBehaviour
{
    public static void EchapManagement()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;

        if (SceneManager.GetActiveScene().name == "Main")
        {
            //SelectionnedSpell
            if (SpellGestion.selectionnedSpell_list != SpellGestion.List.empty)
            {
                if (V.game_state == V.State.fight && !SpellGestion.selectionnedSpell_isEditing)
                {
                    Scene_Main.SetGameAction_movement();
                }
                else
                {
                    SpellGestion.SetSelectionnedSpell(SpellGestion.List.empty);
                }
                return;
            }

            //Window
            foreach (Window a in WindowInfo.Instance.allWindow)
            {
                if (a.inFront)
                {
                    a.Close();
                    return;
                }
            }
        }

        //OptionMenu
        AllSceneCanvas.PauseMenu.ParameterButton();
    }
}