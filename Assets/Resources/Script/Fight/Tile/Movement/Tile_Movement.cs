using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using PathFindingName;
using static UnityEditor.PlayerSettings;

public class Tile_Movement : Tile
{
    public TileData_movement dataMovement { get => (TileData_movement)data; }

    public static void Add(TileData_movement movement)
    {
        var script = Instantiate(Tile_Gestion.Instance.prefab_Tile_Movement, Tile_Gestion.Instance.parent).GetComponent<Tile_Movement>();

        script.SetData(movement);

        script.UpdateColor();

        TileInfo.Instance.Add(script);
    }

    private bool IsReachable()
    {
        return V.player_entity.IsReachable(dataMovement.distance);
    }

    public override void UpdateColor()
    {
        if (IsReachable())
        {
            if (Tile_Gestion.Instance.selectionnedPath.Contains(data.pos))
                ChangeColor(Tile_Gestion.Color.green_light);
            else
                ChangeColor(Tile_Gestion.Color.green);
        }
        else
            ChangeColor(Tile_Gestion.Color.red);
    }

    public override void WhenTheMouseEnter()
    {
        base.WhenTheMouseEnter();

        if (!IsReachable()) return;

        var selectionnedPath = Tile_Gestion.Instance.selectionnedPath;

        selectionnedPath.Clear();

        if (V.player_entity.CurrentPosition_string != data.pos)
        {
            selectionnedPath.AddRange(dataMovement.path);

            string lastPosition = dataMovement.path.LastOrDefault(a => a != data.pos);

            if (string.IsNullOrEmpty(lastPosition))
                lastPosition = V.player_entity.CurrentPosition_string;

            Tile_Gestion.Instance.Add_IconMovement(this, lastPosition);

            Tile_Gestion.Instance.UpdateAllTileColor();
        }
    }

    public override void WhenTheMouseExit()
    {
        base.WhenTheMouseExit();

        Tile_Gestion.Instance.selectionnedPath.Clear();

        Tile_Gestion.Instance.UpdateAllTileColor();
    }
}
