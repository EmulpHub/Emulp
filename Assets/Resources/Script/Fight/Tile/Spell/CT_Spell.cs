using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CT_Spell : Tile
{
    private static GameObject _prefab;

    private static GameObject Prefab
    {
        get
        {
            if (_prefab == null)
                _prefab = Resources.Load<GameObject>("Prefab/CT/CT_Spell");

            return _prefab;
        }
    }

    public static Tile Add(string pos, bool graphic_startAnimation = true)
    {
        Tile script = Instantiate(Prefab, parent).GetComponent<Tile>();

        script.Initialize(Type.spell, 255, 0, false, false, pos);

        script.graphic_startAnimation = graphic_startAnimation;

        script.customListTile = null;

        TileInfo.Instance.Add(script);

        return script;
    }

    public static bool IsWalkableByTheplayer(int distance)
    {
        return distance <= V.player_info.GetRealPm();
    }

    public override void SetNormalColor()
    {
        if (SpellGestion.Get_NeedLineOfView(SpellGestion.selectionnedSpell_list) &&
            !F.IsLineOfView(pos, V.player_entity.CurrentPosition_string))
            ChangeColor(Tile_Gestion.Color.blue_noLine);
        else
            ChangeColor(Tile_Gestion.Color.blue);
    }
}
