using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public partial class Map : MonoBehaviour
{
    public Tilemap Player_ReachPoint;

    private Dictionary<string, NavigationData.type> ExitPos_PosIsKey = new Dictionary<string, NavigationData.type>();

    [SerializeField]
    private Dictionary<NavigationData.type, List<string>> ExitPos = new Dictionary<NavigationData.type, List<string>>();

    public void CalcExitPos()
    {
        if (ExitPos.Count != 0)
            return;

        ExitPos.Clear();
        ExitPos_PosIsKey.Clear();

        foreach (NavigationData.type t in System.Enum.GetValues(typeof(NavigationData.type)))
        {
            ExitPos.Add(t, new List<string>());
        }

        Player_ReachPoint.CompressBounds();

        foreach (Vector3Int Position in Player_ReachPoint.cellBounds.allPositionsWithin)
        {
            if (!Player_ReachPoint.HasTile(Position))
                continue;

            //Get the pos in string of the Position
            string pos = F.ConvertToString(Position);

            TileBase t = Player_ReachPoint.GetTile(Position);

            NavigationData.type e = NavigationData.type.nul;

            if (t == V.Go_Up)
            {
                e = NavigationData.type.go_Up;
            }
            else if (t == V.Go_Down)
            {
                e = NavigationData.type.go_Down;
            }
            else if (t == V.Go_Right)
            {
                e = NavigationData.type.go_Right;
            }
            else if (t == V.Go_Left)
            {
                e = NavigationData.type.go_Left;
            }

            ExitPos[e].Add(pos);
            ExitPos_PosIsKey.Add(pos, e);
        }

        Player_ReachPoint.gameObject.SetActive(false);
    }

    public string GetNearRespawnPos(NavigationData.type Target, string playerPos)
    {
        string r = "100_100";

        List<string> t = ExitPos[Target];

        float lowestDistance = float.MaxValue;

        Vector2 playerPosVector2world = F.ConvertToWorldVector2(playerPos);

        foreach (string s in t)
        {
            if (!F.IsTileConfortablySeenable(s) || !F.IsTileWalkable(s)) continue;

            Vector2 realPosS = F.ConvertToWorldVector2(s);

            float dis = F.DistanceBetweenTwoPos_float(realPosS, playerPosVector2world);

            if (dis < lowestDistance)
            {
                r = s;
                lowestDistance = dis;
            }
        }

        return r;
    }

    public bool existAMapInThisDirection(string pos, NavigationData.type dir)
    {
        switch (dir)
        {
            case NavigationData.type.go_Up:
                return WorldData.Contain(F.AdditionPos(pos, "0_1"));

            case NavigationData.type.go_Down:
                return WorldData.Contain(F.AdditionPos(pos, "0_-1"));

            case NavigationData.type.go_Right:
                return WorldData.Contain(F.AdditionPos(pos, "1_0"));

            case NavigationData.type.go_Left:
                return WorldData.Contain(F.AdditionPos(pos, "-1_0"));
        }

        return false;
    }

    public NavigationData.type getCurrentStateFromPos(string pos)
    {
        if (ExitPos_PosIsKey.ContainsKey(pos))
        {

            NavigationData.type a = ExitPos_PosIsKey[pos];

            List<NavigationData.type> MovableTile = new List<NavigationData.type>
            {
                NavigationData.type.go_Up,
                NavigationData.type.go_Down,
                NavigationData.type.go_Right,
                NavigationData.type.go_Left
            };

            if (!MovableTile.Contains(a))
            {
                return NavigationData.type.nul;
            }

            return a;
        }
        else
        {
            return NavigationData.type.nul;
        }
    }
}
