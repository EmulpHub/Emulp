using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFindingName;

public partial class Entity : MonoBehaviour
{
    protected virtual void Treat_PathParam(PathParam pathParam) { }

    public virtual bool MoveTo(PathParam pathParam)
    {
        Treat_PathParam(pathParam);

        var pathResult = Path.Find(pathParam);

        if (V.IsFight())
        {
            V.script_Scene_Main.EnregistredPath_clear();

            TileInfo.Instance.ListTile_Clear();

            var path = pathResult.path;

            for (int i = 0; i < Info.GetRealPm() && i < path.Count; i++)
            {
                var data = new TileData_graphic(path[i],Tile_Gestion.Color.green_light);

                Tile_Graphic.Add(data);
            }

            Tile_Gestion.Instance.UpdateAllTileSprite();
        }

        return Run(pathResult);
    }

    private bool Run(PathResult pathResult)
    {
        runningInfo.SetPath(pathResult);

        if (pathResult.path.Count == 0) return false;

        string start = CurrentPosition_string;
        string end = pathResult.endOfPath;

        if (start == end) return false;
     
        event_ChangeOfDirectionRun.Call(this);

        StartCoroutine(RunEnumerator(pathResult));

        return true;
    }
}
