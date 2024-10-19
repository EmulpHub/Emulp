using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Glyphe : MonoBehaviour
{
    public static Dictionary<List<string>, Glyphe> allActiveGlyphe = new Dictionary<List<string>, Glyphe>();

    public Dictionary<string, Tile> glyphe_list = new Dictionary<string, Tile>();

    public SpellGestion.List effect;

    //public Tile_Gestion.Color tile_color;

    public Image render;

    public List<string> rangeEffect;

    public Transform tile_parent;

    //public static GameObject CreateGlyphe(SpellGestion.List effect, List<string> rangeEffect, Tile_Gestion.Color tile_color, Sprite img, string originPos)
    //{
    //    GameObject g = Instantiate(Resources.Load<GameObject>("Prefab/Glyphe"));

    //    g.transform.position = F.ConvertToWorldVector2(originPos);

    //    Glyphe glyphe = g.GetComponent<Glyphe>();

    //    glyphe.effect = effect;

    //    glyphe.render.sprite = img;

    //    glyphe.rangeEffect = rangeEffect;

    //    glyphe.tile_color = tile_color;

    //    List<string> effectiveRange = new List<string>();

    //    foreach (string pos in rangeEffect)
    //    {
    //        if (F.IsTileExistWithNoObstacle(F.ConvertToVector2Int(pos)) && F.IsTileSeenable(pos))
    //        {
    //            //CombatTile_Gestion.Add_graphic_forGlyphe(pos, tile_color, true, glyphe);
    //            effectiveRange.Add(pos);
    //        }

    //        Entity entity = EntityByPos.TryGet(pos);

    //        if (entity)
    //        {
    //            glyphe.Apply(entity, true);
    //        }
    //    }

    //    glyphe.rangeEffect = effectiveRange;

    //    allActiveGlyphe.Add(effectiveRange, glyphe);

    //    return g;
    //}

    public static Dictionary<Entity, List<(int idTurn, Glyphe g)>> memorieOfGlyphe = new Dictionary<Entity, List<(int idTurn, Glyphe g)>>();

    public void MemorieOfGlyphe_add(Entity e)
    {
        if (memorieOfGlyphe.ContainsKey(e))
        {
            List<(int idTurn, Glyphe g)> a = new List<(int idTurn, Glyphe g)>(memorieOfGlyphe[e]);

            int i = 0;

            bool find = false;

            foreach ((int idTurn, Glyphe g) v in a)
            {
                if (v.g == this)
                {
                    find = true;
                    memorieOfGlyphe[e][i] = (EntityOrder.Instance.id_turn, this);
                }

                i++;
            }

            if (!find)
            {
                memorieOfGlyphe[e].Add((EntityOrder.Instance.id_turn, this));
            }
        }
        else
        {
            memorieOfGlyphe.Add(e, new List<(int idTurn, Glyphe g)> { (EntityOrder.Instance.id_turn, this) });
        }
    }

    public bool MemorieOfGlyphe_CanBeCasted(Entity e)
    {
        if (memorieOfGlyphe.ContainsKey(e))
        {
            List<(int idTurn, Glyphe g)> a = memorieOfGlyphe[e];

            foreach ((int idTurn, Glyphe g) v in a)
            {
                if (v.g == this)
                {
                    return v.idTurn != EntityOrder.Instance.id_turn;
                }
            }

            return true;
        }
        else
        {
            return true;
        }
    }

    public static void MemorieOfGlyphe_clear()
    {
        memorieOfGlyphe.Clear();
    }

    public bool Apply(Entity Target, bool force = false)
    {
        if (!Target.IsMonster())
            return false;

        if (!force && !MemorieOfGlyphe_CanBeCasted(Target))
            return false;

        if (!force)
            MemorieOfGlyphe_add(Target);

        Action_spell_info_player info = new Action_spell_info_player(Spell.Create(effect),Target,Target.CurrentPosition_string);

        Action_spell.Add(info);

        Action_wait.Add(0.5f);

        return true;
    }

    public static void EraseAllGlyphe()
    {
        foreach (Glyphe g in new List<Glyphe>(allActiveGlyphe.Values))
        {
            g.Erase();
        }
    }

    public void Erase()
    {
        if (allActiveGlyphe.ContainsValue(this))
        {
            allActiveGlyphe.Remove(rangeEffect);
        }

        int max = 0;

        while (tile_parent.childCount > 0)
        {
            DestroyImmediate(tile_parent.GetChild(0).gameObject);

            max++;
            if (max == 100)
            {
                Debug.Log("Max reached");

                break;
            }
        }

        Destroy(this.gameObject);
    }

    public static bool CheckGlypheForAnEntity(Entity target, bool force = false)
    {

        bool r = false;

        foreach (List<string> glyphe_pos in Glyphe.allActiveGlyphe.Keys)
        {
            if (glyphe_pos.Contains(target.CurrentPosition_string))
            {
                if (Glyphe.allActiveGlyphe[glyphe_pos].Apply(target, force))
                    r = true;
            }
        }

        return r;
    }
}
