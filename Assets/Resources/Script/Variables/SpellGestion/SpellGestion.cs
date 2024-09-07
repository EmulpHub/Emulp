using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[SerializeField]
public partial class SpellGestion : MonoBehaviour
{
    public enum TargetMode { entity, empty, all }

    public enum range_effect_size
    {
        singleTarget, oneSquareAround, Cone, twoSquareAround, threeSquareAround, Cone_Inverted, line_2, Front3line,
        oneSquareAround_line, twoSquareAround_line, threeSquareAround_line,fiveSquareAround
    }

    public static void Initialize()
    {
        if (info.Count > 0)
            return;

        Initialize_common();

        Initialize_player();

        Initialize_monster();

        Initialize_Special();

        Panel_Initializing();
    }

    public static void Initialize_player()
    {
        Initialize_player_base();
        Initialize_player_object();
        Initialize_player_weapon();

        Initialize_player_origin_warrior();
        Initialize_player_origin_surface();
        Initialize_player_origin_Nortice();

        NorticeEnum.CreateList();
    }

    public static Spell selectionnedSpell;

    public static Spell.Range_type selectionnedSpell_rangeType;

    public static Vector2 selectionnedSpell_position;

    public static List selectionnedSpell_list;

    public static bool selectionnedSpell_isEditing;

    public static bool AddingANewSpell;

    public static Spell GetSelectionnedSpell()
    {
        return selectionnedSpell;
    }

    public static void SetSelectionnedSpell(List spell, Spell spell_script, Vector2 position)
    {
        selectionnedSpell = spell_script;
        selectionnedSpell_list = spell;
        selectionnedSpell_position = position;

        selectionnedSpell_rangeType = Get_rangeType(spell);

        selectionnedSpell_isEditing = false;

        AddingANewSpell = !SpellInToolbar.contain(spell);
    }

    public static void ResetSelectionnedSpell()
    {
        if (V.game_state_action != V.State_action.movement && V.game_state == V.State.fight)
            Scene_Main.SetGameAction_movement();
    }

    public static void SetSelectionnedSpell(List spell, Spell spell_script)
    {
        if (spell_script == null)
        {
            SetSelectionnedSpell(spell, spell_script, new Vector2(0, 0));
        }
        else
        {
            SetSelectionnedSpell(spell, spell_script, spell_script.transform.position);
        }
    }

    public static void SetSelectionnedSpell(List spell)
    {
        SetSelectionnedSpell(spell, null, new Vector2(100, 100));
    }

    public static bool IsASpellLaunchable()
    {
        foreach (Spell spell in SpellInToolbar.activeSpell_script)
        {
            if (spell.IsEnoughRessourceForLaunch())
            {
                return true;
            }
        }

        return false;
    }

    public static Dictionary<List, SpellInfo> info = new Dictionary<List, SpellInfo>();

    public enum CursorMode { physical, magical }

    public static void AddSpell_info(List spell, string range, int pa_cost, int cd, Spell.Range_type range_Type, CursorMode cursorMode, range_effect_size range_effect, TargetMode targetMode, Color32 col, bool weapon = false)
    {
        SpellInfo new_info = new SpellInfo(range_effect, weapon);

        SetGraphique(new_info, spell);

        new_info.col = col;

        new_info.rangeMin = int.Parse(range[0].ToString());
        new_info.range_Type = range_Type;
        if (range.Length >= 2)
        {
            string str = range.Remove(0, 2);

            new_info.rangeMax = int.Parse(str);
        }
        else
        {
            new_info.rangeMax = new_info.rangeMin;
        }
        new_info.pa_cost = pa_cost;
        new_info.cd = cd;

        new_info.cursorMode = cursorMode;

        new_info.targetMode = targetMode;

        info.Add(spell, new_info);
    }

    public static void SetGraphique(SpellInfo inf, Enum spell)
    {
        inf.graphique = Resources.Load<Sprite>("Image/Sort/" + GetPath(spell.ToString()));
        inf.graphique_gray = Resources.Load<Sprite>("Image/Sort/" + GetPath(spell.ToString()) + "_gray");
    }

    public static Color32 spellInteriorCol_normal = new Color32(86, 86, 82, 255)//new Color32(225, 143, 63, 255)
        , spellInteriorCol_weapon = new Color32(128, 140, 152, 255),
        spellInteriorCol_underground = new Color32(178, 35, 32, 255), spellInteriorCol_notrice = new Color32(50, 86, 239, 255), spellInteriorCol_surface = new Color32(64, 176, 107, 255);

    public static void AddSpell_info(List spell)
    {
        AddSpell_info(spell, "0", 0, 0, Spell.Range_type.normal, CursorMode.magical, 0, TargetMode.entity, spellInteriorCol_normal);
    }

    public static void AddSpell_info(List spell, string range, int pa_cost, int cd, Spell.Range_type range_Type)
    {
        SpellInfo new_info = new SpellInfo(0);

        SetGraphique(new_info, spell);

        new_info.rangeMin = int.Parse(range[0].ToString());
        new_info.range_Type = range_Type;
        if (range.Length >= 2)
        {
            string str = range.Remove(0, 2);

            new_info.rangeMax = int.Parse(str);
        }
        else
        {
            new_info.rangeMax = new_info.rangeMin;
        }
        new_info.pa_cost = pa_cost;
        new_info.cd = cd;

        new_info.cursorMode = CursorMode.physical;

        info.Add(spell, new_info);
    }

    public static string GetPath(string sp)
    {
        string path = "";

        int i = 0;

        while (i < sp.Length)
        {
            path += sp[i];

            if (path == "warrior_")
            {
                return "warrior/" + sp.Remove(0, i + 1);
            }
            else if (path == "object_")
            {
                return "object/" + sp.Remove(0, i + 1);
            }
            else if (path == "weapon_")
            {
                return "weapon/" + sp.Remove(0, i + 1);
            }
            else if (path == "notrice_")
            {
                return "notrice/" + sp.Remove(0, i + 1);
            }
            else if (path == "surface_")
            {
                return "surface/" + sp.Remove(0, i + 1);
            }
            else if (path == "norticeSurface_")
            {
                return "norticeSurface/" + sp.Remove(0, i + 1);
            }
            else if (path == "passive_")
            {
                return "originPassive/" + sp.Remove(0, i + 1);
            }

            i++;
        }

        return sp;
    }

    public static void ModifySpell_string(List spell, string title, string description)
    {
        SpellInfo new_info = info[spell];

        new_info.title = title;
        new_info.description = description;
        SetGraphique(new_info, spell);
    }

    public static void ModifySpell_string(List spell, string title)
    {
        ModifySpell_string(spell, title, "");
    }

    public static string Get_Title(List spell)
    {
        return info[spell].title;
    }

    public static int Get_RangeMin(List spell)
    {
        return Get_RangeMin_full(spell).range;
    }

    public static (int range, bool modified) Get_RangeMin_full(List spell)
    {
        bool modified = false;

        int RangeMin = info[spell].rangeMin;

        return (RangeMin, modified);
    }

    public static int Get_RangeMax(List spell)
    {
        return Get_RangeMax_full(spell).range;
    }

    public static (int range, bool modified) Get_RangeMax_full(List spell)
    {
        bool modified = false;

        int rangeMax = info[spell].rangeMax;

        if (rangeMax > 1)
        {
            if (V.player_info.po != 0)
                modified = true;

            rangeMax += V.player_info.po;
        }

        return (rangeMax, modified);

    }

    public static range_effect_size Get_RangeEffect(List spell)
    {
        return info[spell].range_effect;
    }

    public static Color32 Get_col(List spell)
    {
        return info[spell].col;
    }

    public static List<string> Get_RangeEffect_list(List spell, DirectionData.Direction dir, bool forPlayer = true)
    {
        List<string> effectPos = new List<string>();

        List<string> SquareBetweenTwoWayPoint(string start, string end)
        {
            List<string> l = new List<string> { };

            if (!effectPos.Contains(start))
                l.Add(start);
            if (!effectPos.Contains(end))
                l.Add(end);

            string diff = F.DistanceBetweenTwoPos_returnString(start, end);

            (int x, int y) v = F.ReadString(diff);

            bool xIsPositive = v.x >= 0;
            bool yIsPositive = v.y >= 0;

            v.x = Mathf.Abs(v.x);
            v.y = Mathf.Abs(v.y);

            (int x, int y) v_clamp = (Mathf.Clamp(v.x, 0, 1), Mathf.Clamp(v.y, 0, 1));

            (int x, int y) currentPos = F.ReadString(start);

            int max = 0;

            while (F.ConvertToString(currentPos.x, currentPos.y) != end && max < 100)
            {
                int realx = xIsPositive ? -v_clamp.x : v_clamp.x;
                int realy = yIsPositive ? -v_clamp.y : v_clamp.y;

                string newPos = F.ConvertToString(currentPos.x + realx, currentPos.y + realy);

                if (!effectPos.Contains(newPos))
                    l.Add(newPos);

                currentPos = F.ReadString(newPos);
            }

            return l;
        }

        List<string> GetWayPointWithSpecificLevel(List<(string pos, string toAdd)> ls, int lvl)
        {
            string calc((string pos, string toAdd) x)
            {
                (int x, int y) xyPos = F.ReadString(x.pos);

                (int x, int y) xyToAdd = F.ReadString(x.toAdd);

                return F.ConvertToString(xyPos.x + xyToAdd.x * lvl, xyPos.y + xyToAdd.y * lvl);
            }

            List<string> p = new List<string>();

            foreach ((string pos, string toAdd) v in ls)
            {
                p.Add(calc(v));
            }

            return p;
        }

        List<DirectionData.Direction> listDirectional = new List<DirectionData.Direction> { DirectionData.Direction.down, DirectionData.Direction.up, DirectionData.Direction.left, DirectionData.Direction.right };

        if (listDirectional.Contains(dir))
        {
            effectPos = Get_RangeEffect_WichWay(info[spell].range_effect_tile, dir);
        }
        else
        {
            effectPos = info[spell].range_effect_tile;
        }

        int additionalSquare = forPlayer ? V.player_entity.CollectStr(Effect.effectType.additionalSpellArea) : 0;
        int current = 0;

        while (current < additionalSquare)
        {
            List<string> wayPoint = GetWayPointWithSpecificLevel(info[spell].range_effect_wayPoint, current);

            wayPoint = Get_RangeEffect_WichWay(wayPoint, dir);

            int i = 0;
            while (i < wayPoint.Count - 1)
            {
                string start = wayPoint[i];

                string end = wayPoint[i + 1];

                effectPos.AddRange(SquareBetweenTwoWayPoint(start, end));

                i++;
            }

            effectPos.AddRange(SquareBetweenTwoWayPoint(wayPoint[0], wayPoint[wayPoint.Count - 1]));

            current++;
        }

        List<string> filtred = new List<string>();

        foreach (string p in effectPos)
        {
            if (!filtred.Contains(p))
                filtred.Add(p);
        }

        return filtred;
    }

    private delegate string modificatingRangePos(string p);

    private static List<string> Get_RangeEffect_WichWay(List<string> pos, DirectionData.Direction wichWay)
    {
        if (wichWay == DirectionData.Direction.up)
            return pos;

        modificatingRangePos r = delegate (string p)
        {
            return p;
        };

        if (wichWay == DirectionData.Direction.right)
        {
            r = delegate (string p)
            {
                (int x, int y) v = F.ReadString(p);

                return F.ConvertToString(v.y, v.x);
            };
        }
        else if (wichWay == DirectionData.Direction.left)
        {
            r = delegate (string p)
            {
                (int x, int y) v = F.ReadString(p);

                return F.ConvertToString(-v.y, -v.x);
            };
        }
        else //DOWN
        {
            r = delegate (string p)
            {
                (int x, int y) v = F.ReadString(p);

                return F.ConvertToString(-v.x, -v.y);
            };
        }

        List<string> newPos = new List<string>();

        foreach (string p in pos)
        {
            newPos.Add(r(p));
        }

        return newPos;
    }

    public static int Get_paCost(List spell)
    {
        return Get_paCost_full(spell).pa;
    }

    public static (int pa, bool modified) Get_paCost_full(List spell)
    {
        return (info[spell].pa_cost, false);
    }

    public static (int cd, bool modified) Get_Cd_full(List spell)
    {
        return (info[spell].cd, false);
    }

    public static int Get_Cd(List spell)
    {
        return Get_Cd_full(spell).cd;
    }

    public static Sprite Get_sprite(List spell)
    {
        return info[spell].graphique;
    }

    public static Sprite Get_sprite_Gray(List spell)
    {
        return info[spell].graphique_gray;
    }

    public static Spell.Range_type Get_rangeType(List spell)
    {
        return info[spell].range_Type;
    }

    public static TargetMode Get_TargetMode(List spell)
    {
        return info[spell].targetMode;
    }

    public static CursorMode Get_cursorMode(List spell)
    {
        return info[spell].cursorMode;
    }

    public static bool Get_NeedLineOfView(List spell)
    {
        return info[spell].range_Type != Spell.Range_type.noNeedOfLineOfView;
    }

    public static List<List> allSkill;
    public static List<List> allSkill_locked = new List<List>();

    public static void Panel_Initializing()
    {
        GameObject parent = Resources.Load<GameObject>("Prefab/GroupOfSpell");

        allSkill = Window_skill.GetAllSkill(parent.transform);
        allSkill_locked = new List<List>(allSkill);
    }
}
