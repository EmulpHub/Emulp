using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

/// <summary>
/// Manipulating, mathematic and utilitaries function
/// </summary>
[SerializeField]
public partial class F : MonoBehaviour
{
    public static int CalculateDamageThatCannotKill(Entity t, float dmg)
    {
        if (dmg > t.Info.Life)
        {
            return Mathf.CeilToInt(t.Info.Life - 1);
        }
        else
        {
            return (int)dmg;

        }
    }

    //UseFul

    public static (Entity e, int distance) ClosestEnnemy(Entity caster)
    {
        int closestDistance = 999;

        Entity e = null;

        void Traveler(Entity a)
        {
            if (a == caster && !IsEnnemy(caster, a))
                return;

            int distance = F.DistanceBetweenTwoPos(caster.CurrentPosition_string, a.CurrentPosition_string);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                e = a;
            }
        }
        AliveEntity.Instance.TravelEntity(Traveler);

        return (e, closestDistance);
    }

    public static (Entity e, int distance) ClosestEntity(Entity target)
    {
        int closestDistance = 999;

        Entity e = null;

        void Traveler(Entity a)
        {
            if (a == target)
                return;

            int distance = F.DistanceBetweenTwoPos(target.CurrentPosition_string, a.CurrentPosition_string);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                e = a;
            }
        }

        return (e, closestDistance);
    }

    public delegate void func();

    public static EventHandlerNoArg event_entity_tp = new EventHandlerNoArg(true);

    public static string GetEmptyPos(string pos, Entity toMove)
    {
        if (!IsTileWalkable(pos) && pos != toMove.CurrentPosition_string)
        {
            return NearTileWalkable(pos, new List<string>() { toMove.CurrentPosition_string });
        }

        return pos;
    }

    /// <summary>
    /// Teleport an Entity to pos tile 
    /// </summary>
    /// <param name="pos">The targeted pos</param>
    /// <param name="entity">The entity to teleport</param>
    public static void TeleportEntity(string pos, Entity entity, bool TakeNearPos = true, bool ignoreAllEntityPos = false, List<string> ignorePos = null)
    {

        if (ignorePos == null)
            ignorePos = new List<string>();

        bool result = false;

        if (!ignorePos.Contains(pos))
        {
            if (ignoreAllEntityPos)
                result = F.IsTileWalkable_IgnoreAllEntity(pos);
            else
            {
                result = F.IsTileWalkable(pos, entity);
            }
        }
        if (!result)
        {
            if (!TakeNearPos)
                return;

            pos = NearTileWalkable(pos, ignorePos);
        }

        entity.SetPosition(pos);

        if (V.game_state == V.State.fight)
        {
            event_entity_tp.Call();
        }
    }

    public static string TakeRandomPos(List<string> allPos, bool verifyIfWalkable = true)
    {
        do
        {
            int i = Random.Range(0, allPos.Count);

            string p = allPos[i];

            allPos.RemoveAt(i);

            if (!verifyIfWalkable || F.IsTileWalkable(p))
                return p;

        } while (allPos.Count > 0);

        return "999";
    }

    public static bool IsEnnemy(Entity one, Entity two)
    {
        var typeOne = one.Info.type;
        var typeTwo = two.Info.type;

        if (typeOne == EntityInfo.Type.player && typeTwo == EntityInfo.Type.playerFriendly ||
            (typeTwo == EntityInfo.Type.playerFriendly && typeOne == EntityInfo.Type.player))
            return true;

        return typeOne != typeTwo;
    }

    public static Sprite GetSpellSpriteAtPath(string path)
    {
        return Resources.Load<Sprite>("Image/Sort/" + path);
    }

    public static Sprite GetSpellSpriteAtPath_warrior(string path)
    {
        return Resources.Load<Sprite>("Image/Sort/warrior/" + path);
    }

    public static Vector2 GetPosNearTarget(Entity e, Vector2 pos)
    {
        Vector2 entityPos = V.CalcEntityDistanceToBody(e);

        float diffx = 0.1f, diffY = 0.3f;

        return new Vector2(Mathf.Clamp(pos.x, entityPos.x - diffx, entityPos.x + diffx), Mathf.Clamp(pos.y, entityPos.y - diffY, entityPos.y + diffY));
    }

    public static Vector2 ConvertDegreeIntoVector(float degree)
    {
        float baseDegree = degree;

        degree += 90;

        return new Vector2(Mathf.Cos(Mathf.Deg2Rad * degree), Mathf.Sin(Mathf.Deg2Rad * degree));
    }

}
