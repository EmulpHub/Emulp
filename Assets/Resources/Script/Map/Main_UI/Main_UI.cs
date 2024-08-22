using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public partial class Main_UI : MonoBehaviour
{
    /// <summary>
    /// The lifeBar of the toolbar, the frond and the back
    /// </summary>
    public Image toolbar_lifeBar_sliced, toolbar_lifeBar_sliced_behind;
    /// <summary>
    /// The text of the lifeBar
    /// </summary>
    public Text Toolbar_lifeBar_text;

    public GameObject pa, pm, life, ToolbarSpell, button_character, button_skill, button_equipment,
        Map_possibleToMove;

    /// <summary>
    /// The text of the pa and pm
    /// </summary>
    public Text pa_text, pm_text;

    /// <summary>
    /// The sprite when the player loose or gain hp
    /// </summary>
    public Color Tooblar_Behind_Red, Tooblar_Behind_Green;

    //[HideInInspector]
    //public float xpProgessionBar_FillAmount = 0;
    [HideInInspector]
    public float xpSave;

    /// <summary>
    /// The bound a spell can go inside the toolbar_canvas
    /// </summary>
    public float toolbarSpell_min_x, toolbarSpell_max_x, toolbarSpell_max_y;

    public void ToolBarManagement_Instantiate()
    {
        SpellInToolbar.activeSpell.Clear();

        //GESTION LIST
        int count = SpellInToolbar.totalSpellCount;

        if (SpellInToolbar.activeSpell.Count == 0)
        {
            if (Save_PlayerData.toolbar.Count == 0)
            {

                while (SpellInToolbar.activeSpell.Count <= count)
                {
                    SpellInToolbar.activeSpell.Add(SpellGestion.List.empty);
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    SpellInToolbar.activeSpell.Add(Save_PlayerData.toolbar[i]);
                }
            }
        }


        //FIN GESTION LIST

        //GESTION INSTANTIATE

        foreach (Transform child in toolbarSpell_parent.transform)
        {
            Destroy(child.gameObject);
        }

        //Instantiate Object

        //First
        GameObject object_GameObject = InstantiateSpell_Object(new Vector2(149+45, 80)).gameObject;

        object_GameObject.name = "Object Spell";

        Spell a = object_GameObject.GetComponent<Spell>();

        Spell.Object = a;

        a.spell = SpellGestion.List.object_empty;

        //Second
        GameObject object_GameObject_2 = InstantiateSpell_Object(new Vector2(149 + 45, 35), true).gameObject;

        object_GameObject_2.name = "Object Spell_2";

        Spell b = object_GameObject_2.GetComponent<Spell>();

        Spell.Object_2 = b;

        b.spell = SpellGestion.List.object_empty;

        //END OBJECT
        int countPerLine = count / 2;

        RectTransform spellRect = spell.GetComponent<RectTransform>();

        float spell_xscale = spellRect.sizeDelta.x * spellRect.transform.localScale.x;

        for (int i = count - 1; i >= 0; i--)
        {
            GameObject g = InstantiateSpell(calcPosForToolbar(i), i).gameObject;

            g.name = "spell with id = " + i;

            if (i == 0)
            {
                Main_Object.Modify(Main_Object.objects.spell_punch, g);

                Spell.Reference = g.GetComponent<Spell>();
            }
        }
    }

    public Vector2 calcPosForToolbar(int index)
    {
        RectTransform spellRect = spell.GetComponent<RectTransform>();

        float spell_xscale = spellRect.sizeDelta.x * spellRect.transform.localScale.x;
        float spell_yscale = spellRect.sizeDelta.y * spellRect.transform.localScale.y;

        float x_current = 0;
        float y_current = 0;

        int countPerLine = SpellInToolbar.totalSpellCount / 2;

        if (index >= countPerLine)
        {
            x_current = toolbarSpell_min_x + (index - countPerLine) * spell_xscale;
            y_current = toolbarSpell_max_y - spell_yscale;
        }
        else
        {
            x_current = toolbarSpell_min_x + index * spell_xscale;
            y_current = toolbarSpell_max_y;
        }

        return new Vector2(x_current, y_current);
    }

    public Vector2 calcPosForToolbar(string pos)
    {
        (int x, int y) v = F.ReadString(pos);

        return calcPosForToolbar(v.x + v.y * (SpellInToolbar.totalSpellCount / 2));
    }

    /// <summary>
    /// The toolbarSpell Parent
    /// </summary>
    public Transform toolbarSpell_parent;

    /// <summary>
    /// A ToolbarSpell
    /// </summary>
    public GameObject spell;

    /// <summary>
    /// Instantiate a spell with index and pos
    /// </summary>
    /// <param name="pos">the pos of the index in the toolbar canvas</param>
    /// <param name="index">The index of the spell</param>
    public GameObject InstantiateSpell(Vector2 pos, int index)
    {
        Spell Spell_script = spell.GetComponent<Spell>();

        Spell_script.id = index;

        Spell_script.pos = convertSpellIdIntoString(index);

        Spell_script.spell = SpellInToolbar.activeSpell[index];

        spell.GetComponent<RectTransform>().anchoredPosition = pos;

        GameObject g = Instantiate(spell, toolbarSpell_parent);

        SpellInToolbar.activeSpell_script.Add(g.GetComponent<Spell>());

        return g;
    }

    public static string convertSpellIdIntoString(int id)
    {

        int x = id % (SpellInToolbar.totalSpellCount / 2);
        int y = id / (SpellInToolbar.totalSpellCount / 2);

        return F.ConvertToString(x, y);
    }

    public GameObject InstantiateSpell_Object(Vector2 pos, bool second = false)
    {
        Spell Spell_script = spell.GetComponent<Spell>();

        Spell_script.id = second ? 31 : 30;
        Spell_script.pos = "88_88";
        Spell_script.spell = SpellGestion.List.empty;

        spell.GetComponent<RectTransform>().anchoredPosition = pos;

        GameObject g = Instantiate(spell, toolbarSpell_parent);

        SpellInToolbar.activeSpell_script.Add(g.GetComponent<Spell>());

        return g;
    }

    /// <summary>
    /// Add spell to the activeSpell
    /// </summary>
    /// <param name="spell">The spell we want to add</param>
    public static Vector2 Toolbar_AddSpell(SpellGestion.List spell)
    {
        for (int i = SpellInToolbar.activeSpell_script.Count - 1; i >= 0; i--)
        {
            Spell sp = SpellInToolbar.activeSpell_script[i];

            if (sp.spell == spell)
            {
                return Vector2.zero;
            }
        }

        for (int i = SpellInToolbar.activeSpell_script.Count - 1; i >= 0; i--)
        {
            Spell sp = SpellInToolbar.activeSpell_script[i];

            if (sp.spell == SpellGestion.List.empty && sp != Spell.Weapon && (sp != Spell.Object && sp != Spell.Object_2))
            {
                sp.spell = spell;
                sp.SetSpellInfo();

                return sp.gameObject.transform.position;
            }
        }

        return Vector2.zero;
    }

    /// <summary>
    /// Custom texture for cursor
    /// </summary>
    public static Texture2D cursor_normal, cursor_Physical, cursor_magical, cursor_hand_yellow;

    public static void InitalizeCursorTexture()
    {
        if (cursor_normal != null)
            return;

        cursor_normal = Resources.Load<Texture2D>("UI/Cursor/cursor_normal");
        cursor_Physical = Resources.Load<Texture2D>("UI/Cursor/cursor_physical");
        cursor_magical = Resources.Load<Texture2D>("UI/Cursor/cursor_magical");
        cursor_hand_yellow = Resources.Load<Texture2D>("UI/Cursor/cursor_grab_yellow_2");

    }

    /// <summary>
    /// A list of gameobject that prevent the cursor to change texture
    /// </summary>
    public static List<GameObject> dontChangeCursor = new List<GameObject>();

    /// <summary>
    /// Update ui (toolbarManagement, cursor and moving text)
    /// </summary>
    public void Update_Ui()
    {

        ManageSetCursor();

        ManageMovingText();
    }

    public static void RemoveInactiveCursorChange()
    {
        foreach (GameObject g in new List<GameObject>(dontChangeCursor))
        {
            if (g == null || !g.gameObject.activeInHierarchy)
            {
                dontChangeCursor.Remove(g);
            }
        }
    }

    /// <summary>
    /// Manage dontMoveCursor by adding or removing "target"
    /// </summary>
    /// <param name="target">The gameobject to add or remove</param>
    /// <param name="add">Are we adding or removing</param>
    public static void ManageDontMoveCursor(GameObject target, bool add)
    {
        if (add)
        {
            if (!dontChangeCursor.Contains(target))
            {
                dontChangeCursor.Add(target);
            }
        }
        else
        {
            if (dontChangeCursor.Contains(target))
            {
                dontChangeCursor.Remove(target);
            }
        }
    }

}
