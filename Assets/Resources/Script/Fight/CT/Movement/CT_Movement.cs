using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using PathFindingName;

public class CT_Movement : Tile
{
    public int distance;

    private List<string> _path;

    [HideInInspector]
    public List<string> path
    {
        get
        {
            if(_path == null)
            {
                _path = Path.Find(new PathParam(V.player_entity.CurrentPosition_string,pos)).path;
            }

            return _path;
        }
    }

    public override void SetNormalColor()
    {
        if (IsWalkableByTheplayer(distance))
            ChangeColor(Tile_Gestion.Color.green);
        else
            ChangeColor(Tile_Gestion.Color.red);
    }

    public override void WhenTheMouseEnter()
    {
        base.WhenTheMouseEnter();

        if (!IsWalkableByTheplayer(distance)) return;

        SelectionnedPath_clear();

        string lastpos = V.player_entity.CurrentPosition_string;

        foreach (string move in path)
        {
            Tile tile = TileInfo.Instance.Get(move);

            if (tile != null) tile.HighlightOn();


            selectionnedPath.Add(move);

            if (move != lastpos && move != pos) lastpos = move;
        }

        if (pos != lastpos)
            Tile_Gestion.Instance.Add_IconMovement(this, lastpos);
    }

    public override void WhenTheMouseExit()
    {
        base.WhenTheMouseExit();

        SelectionnedPath_clear();
    }

    #region static

    private static GameObject _prefab;

    private static GameObject Prefab
    {
        get
        {
            if (_prefab == null)
                _prefab = Resources.Load<GameObject>("Prefab/CT/CT_Movement");

            return _prefab;
        }
    }

    public static List<string> selectionnedPath = new List<string>();

    public static void SelectionnedPath_clear()
    {
        foreach (string p in selectionnedPath)
        {
            Tile tile = TileInfo.Instance.Get(p);

            if (tile == null) continue;

            tile.SetNormalColor();
        }

        selectionnedPath.Clear();
    }

    public static void Add(string pos, int distance, bool resetNearSprite = true, bool IgnoreAllEntity = false, bool IgnoreMouseOver = false, bool startAnimation = true)
    {
        CT_Movement script = Instantiate(Prefab, parent).GetComponent<CT_Movement>();

        script.Initialize(Type.movement, 255, 0, IgnoreAllEntity, IgnoreMouseOver, pos);

        script.distance = distance;

        script.graphic_startAnimation = startAnimation;

        script.customListTile = null;

        if (IsWalkableByTheplayer(distance))
            script.ChangeColor(Tile_Gestion.Color.green);
        else
            script.ChangeColor(Tile_Gestion.Color.red);

        TileInfo.Instance.Add(script);

        if (resetNearSprite)
            TileInfo.Instance.ResetNearSprite(pos, IgnoreAllEntity, TileInfo.Instance.listTilePos);
    }

    public static bool IsWalkableByTheplayer(int distance)
    {
        return distance <= V.player_info.GetRealPm();
    }


    #endregion
}
