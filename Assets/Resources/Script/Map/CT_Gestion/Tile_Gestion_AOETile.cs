using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Tile_Gestion : MonoBehaviour
{
    private List<Tile> ListAOE = new List<Tile>();

    public void AOE_Create(string targetedSqaure, Color color)
    {
        AOE_EraseAll();

        List<string> listeAOEPos = SpellInfo.GetStringEffectList(SpellGestion.selectionnedSpell_list, targetedSqaure, V.player_entity.CurrentPosition_string);

        foreach (string pos in listeAOEPos)
        {
            var data = new TileData_graphic(pos, color, TileData.Layer.spell);

            data.SetListTileDependancy(listeAOEPos);

            var tile = Tile_Graphic.Add(data);

            tile.SetOutline();

            ListAOE.Add(tile);
        }

        MouseOnTile = TileInfo.Instance.Get(targetedSqaure);

        Add_IconSpell(MouseOnTile);
    }

    public void AOE_EraseAll()
    {
        foreach (Tile tile in new List<Tile>(ListAOE))
        {
            TileInfo.Instance.Remove(tile);

            if (tile != null)
                tile.Erase();
        }

        ListAOE.Clear();
    }
}
