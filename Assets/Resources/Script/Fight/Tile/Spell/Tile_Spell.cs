using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Spell : Tile
{
    public static Tile Add(TileData_spell data)
    {
        Tile script = Instantiate(Tile_Gestion.Instance.prefab_Tile_Spell, Tile_Gestion.Instance.parent).GetComponent<Tile>();

        script.SetData(data);
        script.UpdateColor();

        TileInfo.Instance.Add(script);

        return script;
    }

    public override void UpdateColor()
    {
        if (SpellGestion.Get_NeedLineOfView(SpellGestion.selectionnedSpell_list) &&
            !F.IsLineOfView(data.pos, V.player_entity.CurrentPosition_string))
            ChangeColor(Tile_Gestion.Color.blue_noLine);
        else
            ChangeColor(Tile_Gestion.Color.blue);
    }
}
