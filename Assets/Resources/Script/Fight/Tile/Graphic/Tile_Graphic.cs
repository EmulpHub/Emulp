using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Graphic : Tile
{
    public TileData_graphic dataGraphic { get => (TileData_graphic)data; }

    public static Tile_Graphic Add(TileData_graphic Data, bool addToInfo = true)
    {
        Tile_Graphic script = Instantiate(Tile_Gestion.Instance.prefab_Tile_Graphic, Tile_Gestion.Instance.parent).GetComponent<Tile_Graphic>();

        script.SetData(Data);
        script.UpdateColor();

        

        if (addToInfo)
            TileInfo.Instance.Add(script);

        return script;
    }

    public override void UpdateColor()
    {
        ChangeColor(dataGraphic.ChooseColor());
    }
}
