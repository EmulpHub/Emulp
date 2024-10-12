using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public partial class F : MonoBehaviour
{
    public static float IsPosSeenable_min = 0, IsPosSeenable_max = 1;

    public static bool IsPosSeenable(Vector3 pos)
    {
        Vector3 cameraCoord = Camera.main.WorldToViewportPoint(pos);

        return F.IsBetweenTwoValues(cameraCoord.x, IsPosSeenable_min, IsPosSeenable_max) && F.IsBetweenTwoValues(cameraCoord.y, IsPosSeenable_min, IsPosSeenable_max);
    }

    public static bool NoBlockedTileNearby_Specific(int x, int y, Tilemap ground, Tilemap ground_above, List<string> specifiedRelativeTile)
    {
        //Check all relative tile around in specifiedRelativeTile
        foreach (string pos in specifiedRelativeTile)
        {
            (int x, int y) xy = ReadString(pos);

            if (!IsTileWalkable(x + xy.x, y + xy.y, ground, ground_above))
            {
                return false;
            }
        }

        return true;
    }

    public static bool IsFaceRight_entity(string pos1, string pos2)
    {
        (int diffx, int diffy) xy = DistanceBetweenTwoPos_xy(pos1, pos2);

        bool IsX = Mathf.Abs(xy.diffx) > Mathf.Abs(xy.diffy);

        return (IsX && xy.diffx < 0) || (!IsX && xy.diffy > 0);
    }

    public static bool IsFaceRight_entity(Entity entity1, Entity entity2)
    {
        return IsFaceRight_entity(entity1.CurrentPosition_string, entity2.CurrentPosition_string);
    }

    public static bool IsInDiagonal(string pos1, string pos2)
    {
        (int x, int y) pos1xy = ReadString(pos1);
        (int x, int y) pos2xy = ReadString(pos2);

        int diffx = Mathf.Abs(pos1xy.x - pos2xy.x);
        int diffy = Mathf.Abs(pos1xy.y - pos2xy.y);

        return diffx == diffy;
    }

    public static bool IsInLine(string pos1, string pos2)
    {
        (int x, int y) pos1xy = ReadString(pos1);
        (int x, int y) pos2xy = ReadString(pos2);

        bool posXEqual = pos1xy.x == pos2xy.x;
        bool posYEqual = pos1xy.y == pos2xy.y;

        return (posXEqual && !posYEqual) || (!posXEqual && posYEqual);
    }

    public static bool IsLineOfView(string target, string origin)
    {
        List<string> posToCheck = new List<string>();

        Vector2 target_v = ConvertToWorldVector2(target);
        Vector2 origin_v = ConvertToWorldVector2(origin);

        Vector2 Dir = target_v - origin_v;
        Dir.Normalize();

        Vector2 CurrentPos = origin_v;

        int count = 0;

        string newPos = "999_999";

        do
        {
            newPos = ConvertToString(ConvertToGridVector(CurrentPos));

            if (newPos == target)
            {
                break;
            }

            if (!posToCheck.Contains(newPos) && newPos != origin)
            {
                posToCheck.Add(newPos);

                if (!IsTileWalkable(newPos))
                {
                    return false;
                }
            }

            CurrentPos += Dir * 0.3f;

            count++;
        } while (count < 100);

        if (count >= 100)
        {
            throw new System.Exception("IslineOfView overCount");
        }

        return true;
    }

    public static string NearTileWalkable(string pos, List<string> ForbidenPos = null)
    {
        int currentTile = 1;

        if (ForbidenPos == null)
            ForbidenPos = new List<string>();

        if (F.IsTileWalkable(pos) && !ForbidenPos.Contains(pos))
        {
            return pos;
        }
        while (currentTile < 5)
        {
            List<string> ls = TileAtXDistance(currentTile, pos);

            foreach (string p in ls)
            {
                if (!F.IsTileWalkable(p) || ForbidenPos.Contains(p))
                {
                    continue;
                }

                return p;
            }

            currentTile++;
        }

        return pos;
    }

    public delegate bool Condition(Entity t, Entity nearE);

    public static (int nb, List<Entity>) EnnemyAroundEntity(Entity target, Condition c)
    {
        int nb = 0;

        List<Entity> ls = new List<Entity>();

        void Traveler(Entity e)
        {
            if (target == e || F.DistanceBetweenTwoPos(target, e) > 1 || !c(target, e))
                return;

            nb++;
            ls.Add(e);
        }

        AliveEntity.Instance.TravelEntity(Traveler);

        return (nb, ls);
    }
}
