using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Spell : MonoBehaviour
{
    /// <summary>
    /// Is the mouse over that spell
    /// </summary>
    [HideInInspector]
    public bool mouseIsOver;

    public void MouseEnter()
    {
        DisplayInfo();
        mouseIsOver = true;
        MakeAnimation(TypeOfAnimation.set);
    }

    public void MouseExit()
    {
        MakeAnimation(TypeOfAnimation.reset);

        Description_text.EraseDispay();
        mouseIsOver = false;
    }

    [HideInInspector]
    public float waitBeforeReSelectioning;

    /// <summary>
    /// If we allow to change spell at mouseOver
    /// </summary>
    /// <returns></returns>
    public bool AllowToMovedOnClick()
    {
        return V.game_state != V.State.fight &&
            (this != Object && this != Object_2) && (this != Weapon || !SpellGestion.AddingANewSpell);
    }

    public RectTransform box_interior;

    /// <summary>
    /// If we need to laucnh mouse enter, exit or over
    /// </summary>
    public void CheckForMouseOver()
    {
        bool NextMouseIsOver = DetectMouse.IsMouseOnUI(box_interior) && !Scene_Main.isMouseOverAWindow && ClickAutorization.Autorized(this.gameObject);

        if (NextMouseIsOver != mouseIsOver)
        {
            if (NextMouseIsOver)
            {
                MouseEnter();
            }
            else
            {
                MouseExit();
            }
        }

        mouseIsOver = NextMouseIsOver;

        if (mouseIsOver)
            WhenMouseIsOver();

        bool ChangeCursor = (IsEnoughRessourceForLaunch() || V.game_state == V.State.passive || AllowToMovedOnClick()) && mouseIsOver && !Scene_Main.isMouseOverAWindow;

        Main_UI.ManageDontMoveCursor(gameObject, ChangeCursor);

        if (ChangeCursor)
        {
            if (AllowToMovedOnClick())
            {
                if (!(SpellGestion.selectionnedSpell_list == SpellGestion.List.empty && spell == SpellGestion.List.empty))
                    Window.SetCursorAndOffsetHand();

            }
            else
            {
                Window.SetCursorAndOffsetHand();
            }
        }
    }

    /// <summary>
    /// When the mouse is over this ui
    /// </summary>
    public void WhenMouseIsOver()
    {
        DisplayInfo();

        //transform.SetSiblingIndex(transform.parent.childCount);

        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        if (AllowToMovedOnClick())
        {
            if (SpellGestion.selectionnedSpell_list == SpellGestion.List.empty && waitBeforeReSelectioning <= 0)
            {
                SpellGestion.SetSelectionnedSpell(spell, this);
                SpellGestion.selectionnedSpell_isEditing = true;
            }
            else if (SpellGestion.selectionnedSpell_list != spell && waitBeforeReSelectioning <= 0)
            {

                SpellGestion.selectionnedSpell_isEditing = false;

                int nbUse = 0;
                int cooldownMax = 0;
                int cooldown = 0;

                bool exchange = false;

                foreach (Spell sp in SpellInToolbar.activeSpell_script)
                {
                    if (sp.spell == SpellGestion.selectionnedSpell_list)
                    {
                        nbUse = sp.currentUse;
                        cooldownMax = sp.id_nextPossibleReUse_max;
                        cooldown = sp.id_nextPossibleReUse;

                        //Changement of spell
                        if (SpellGestion.selectionnedSpell == null && spell != SpellGestion.List.empty)
                        {
                            Animation_skillAssignation.Instantiate(transform.position, sp.transform.position, spell, 0);
                        }
                        else if (sp.spell != SpellGestion.List.empty && SpellGestion.selectionnedSpell != null)
                        {
                            Animation_skillAssignation.Instantiate(sp.transform.position, transform.position, sp.spell, spell != SpellGestion.List.empty ? -1 : 0, 0.7f);

                            exchange = true;
                        }

                        if (Weapon == this)
                        {
                            Weapon = sp;
                        }

                        sp.ChangeSpell(spell);

                        sp.currentUse = currentUse;
                        sp.id_nextPossibleReUse_max = id_nextPossibleReUse_max;
                        sp.id_nextPossibleReUse = id_nextPossibleReUse;

                        break;
                    }
                }

                currentUse = nbUse;
                id_nextPossibleReUse_max = cooldownMax;
                id_nextPossibleReUse = cooldown;

                //If there is nothing here when changing of it's the changes come from window skill
                if (spell != SpellGestion.List.empty || SpellGestion.selectionnedSpell == null)
                {
                    Vector2 start, final;

                    SpellGestion.List sp = spell;

                    if (SpellGestion.selectionnedSpell == null) //From window_skill
                    {
                        start = SpellGestion.selectionnedSpell_position;
                        final = transform.position;

                        sp = SpellGestion.selectionnedSpell_list;
                    }
                    else
                    {
                        start = transform.position;
                        final = SpellGestion.selectionnedSpell_position;
                    }

                    Animation_skillAssignation.Instantiate(start, final, sp, exchange ? 1 : 0, 0.7f);
                }

                ChangeSpell(SpellGestion.selectionnedSpell_list);

                SpellGestion.SetSelectionnedSpell(SpellGestion.List.empty);

                waitBeforeReSelectioning = 0.1f;
            }
        }
        else
        {
            SpellGestion.selectionnedSpell_isEditing = false;
            UseSpell();
        }
    }
}
