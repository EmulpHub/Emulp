using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Window_skill : Window
{
    protected override bool SetMouseIsOver()
    {
        return base.SetMouseIsOver() || (background_isMouseOver || highLightedSkill != null);
    }


    public void WhenMouseIsOver()
    {

        if (Input.GetMouseButtonDown(0))
            AutorizeMovementAndScale = true;

        if (!Input.GetMouseButton(0) || !AutorizeMovementAndScale)
        {
            AutorizeMovementAndScale = false;
            IsDraging = false;
            background_drag = false;
            return;
        }

        MovementGestion();
    }
}
