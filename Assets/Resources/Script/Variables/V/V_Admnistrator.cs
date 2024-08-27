using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class V : MonoBehaviour
{
    public static bool administrator = true;

    public static void Administrator_update()
    {
        if (!administrator)
            return;

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (V.game_state == V.State.fight)
            {
                List<Entity> al = new List<Entity>(AliveEntity.list);

                foreach (Entity ent in al)
                {
                    if (ent.IsMonster())
                    {
                        ent.Damage(new InfoDamage(10000, V.player_entity));
                    }
                }
            }
            else
            {
                UnityEngine.Debug.Log("You'r not allowed to do that");
            }
        }

        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    Save_SaveSystem.EraseSave();
        //}
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    Save_SaveSystem.SaveGame();
        //}
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    Save_SaveSystem.LoadSave();
        //}

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            V.script_Scene_Main.actionToDo_Debug.GetComponent<Debug_actionToDo>().ClearRemovedElement();
            script_Scene_Main.actionToDo_Debug_enable = !script_Scene_Main.actionToDo_Debug_enable;
        }

        script_Scene_Main.actionToDo_Debug.gameObject.SetActive(script_Scene_Main.actionToDo_Debug_enable && V.administrator);
    }
}
