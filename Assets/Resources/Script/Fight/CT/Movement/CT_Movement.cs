using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class CT_Movement : CT
{
    static GameObject _prefab;

    private static GameObject Prefab
    {
        get
        {
            if (_prefab == null)
                _prefab = Resources.Load<GameObject>("Prefab/CT/CT_Movement");

            return _prefab;
        }
    }

    public static void Add(string pos, List<string> path, bool resetNearSprite = true, bool IgnoreAllEntity = false, bool IgnoreMouseOver = false, bool startAnimation = true)
    {
        CT_Movement script = Instantiate(Prefab, parent).GetComponent<CT_Movement>();

        script.Initialize(Type.movement, 255, 0, IgnoreAllEntity, IgnoreMouseOver, pos);

        script.path = path;

        script.graphic_startAnimation = startAnimation;

        script.customListTile = null;

        if (IsWalkableByTheplayer(path.Count))
            script.ChangeColor(CT_Gestion.Color.green);
        else
            script.ChangeColor(CT_Gestion.Color.red);

        CTInfo.Instance.Add(script);

        if (resetNearSprite)
            CTInfo.Instance.ResetNearSprite(pos, IgnoreAllEntity, CTInfo.Instance.listPosCTKeys);
    }

    public static bool IsWalkableByTheplayer(int distance)
    {
        return distance <= V.player_info.GetRealPm();
    }

    [HideInInspector]
    public List<string> path;

    public override void SetNormalColor()
    {
        if (IsWalkableByTheplayer(path.Count))
            ChangeColor(CT_Gestion.Color.green);
        else
            ChangeColor(CT_Gestion.Color.red);
    }


    public static List<string> selectionnedPath = new List<string>();

    public static void SelectionnedPath_clear()
    {
        foreach (string p in selectionnedPath)
        {
            CT tile = CTInfo.Instance.Get(p);

            if (tile == null) continue;

            tile.SetNormalColor();
        }

        selectionnedPath.Clear();
    }


    public override void WhenTheMouseEnter()
    {
        base.WhenTheMouseEnter();

        if (!IsWalkableByTheplayer(path.Count)) return;

        SelectionnedPath_clear();

        string lastpos = V.player_entity.CurrentPosition_string;

        foreach (string move in path)
        {
            CT tile = CTInfo.Instance.Get(move);

            if (tile != null) tile.HighlightOn();


            selectionnedPath.Add(move);

            if (move != lastpos && move != pos) lastpos = move;
        }

        if (pos != lastpos)
            CT_Gestion.Instance.Add_IconMovement(this, lastpos);
    }

    public override void WhenTheMouseExit()
    {
        base.WhenTheMouseExit();

        SelectionnedPath_clear();
    }
}
