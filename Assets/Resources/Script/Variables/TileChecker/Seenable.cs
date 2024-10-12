using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seenable : MonoBehaviour
{
    public static bool Check (string pos)
    {
        float y_min = 0;
        float y_max = 0;
        float x_max = 0;

        if (V.game_state == V.State.fight)
        {
            y_min = V.script_Scene_Main.cam_YPosMin;
            y_max = V.script_Scene_Main.cam_YPosMax;
            x_max = V.script_Scene_Main.cam_XPosMax;
        }
        else
        {
            y_min = V.script_Scene_Main.cam_YPosMin_Fight;
            y_max = V.script_Scene_Main.cam_YPosMax_Fight;
            x_max = V.script_Scene_Main.cam_XPosMax_Fight;
        }

        Vector3 cameraPos = Camera.main.WorldToViewportPoint(F.ConvertToWorldVector2(pos));

        return F.IsBetweenTwoValues(cameraPos.y, y_min, y_max) && F.IsBetweenTwoValues(cameraPos.x, (1 / x_max) - 1, x_max);
    }
}
