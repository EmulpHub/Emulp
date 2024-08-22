using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Window_skill : Window
{
    [HideInInspector]
    public bool background_isMouseOver = false;

    [HideInInspector]
    public bool background_drag;

    [HideInInspector]
    Vector3 background_margin;

    /// <summary>
    /// The minimal zoom when using scrool and the max x and max y value the anchored position can have
    /// </summary>
    public float MinZoom, MaxZoom, MaxX, MaxY;

    [HideInInspector]
    public bool AutorizeMovementAndScale;

    [HideInInspector]
    public bool IsDraging = false;

    public override void Deselectionnate()
    {
        base.Deselectionnate();

        background_drag = false;
        background_isMouseOver = false;
        IsDraging = false;

        if (highLightedSkill != null)
        {
            highLightedSkill.Window_Interaction_MouseOver_End();

            highLightedSkill = null;
            Main_UI.Display_Description_Erase();
        }
    }

    public void ElementStartMouseOver (WindowSkillElement element)
    {
        NotIn_Add(element.rectThis);

        background_isMouseOver = false;

        highLightedSkill = element;
    }


    public void ElementEndMouseOver(WindowSkillElement element)
    {
        NotIn_Remove(element.rectThis);
    }

    public override void Open()
    {
        base.Open();

        while (skillAssignationParent.childCount > 0)
        {
            DestroyImmediate(skillAssignationParent.GetChild(0).gameObject);
        }

    }
}
