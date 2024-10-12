using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class F : MonoBehaviour
{

    /// <summary>
    /// Calculate distance bewteen 2 vector (float value)
    /// </summary>
    /// <param name="pos1">The first pos</param>
    /// <param name="pos2">The second pos</param>
    /// <returns></returns>
    public static float DistanceBetweenTwoVector_INT(Vector2 pos1, Vector2 pos2)
    {
        float distance_x = Mathf.Abs(pos1.x - pos2.x);
        float distance_y = Mathf.Abs(pos1.y - pos2.y);

        return distance_x + distance_y;
    }

    public static float Projectile_CalculateTravelTime(string pos1, string pos2)
    {
        return Projectile_CalculateTravelTime(F.DistanceBetweenTwoPos(pos1, pos2));

    }

    public static float Projectile_CalculateTravelTime(Entity caster, Entity target)
    {
        return Projectile_CalculateTravelTime(F.DistanceBetweenTwoPos(caster, target));
    }

    public static float Projectile_CalculateTravelTime(float dis)
    {
        return 0.1f + dis * 0.08f;
    }


    /// <summary>
    /// Calculate distance bewteen 2 pos_string (int value)
    /// </summary>
    /// <param name="pos1">The first pos</param>
    /// <param name="pos2">The second pos</param>
    /// <returns></returns>
    public static int DistanceBetweenTwoPos(string pos1, string pos2)
    {
        (int x, int y) p1 = ReadString(pos1);
        (int x, int y) p2 = ReadString(pos2);

        return Mathf.Abs(p1.x - p2.x) + Mathf.Abs(p1.y - p2.y); //x + y
    }


    public static string DistanceBetweenTwoPos_returnString(string pos1, string pos2)
    {
        (int x, int y) p1 = ReadString(pos1);
        (int x, int y) p2 = ReadString(pos2);

        return ConvertToString(p1.x - p2.x, p1.y - p2.y); //x + y
    }
    /// <summary>
    /// Calculate distance between 2 vector2Int and return int
    /// </summary>
    /// <param name="vector1">The first Vector2Int</param>
    /// <param name="vector2">The second Vector2Int</param>
    /// <returns></returns>
    public static int DistanceBetweenTwoPos(Vector2Int vector1, Vector2Int vector2)
    {
        return DistanceBetweenTwoPos(ConvertToString(vector1), ConvertToString(vector2));

        //return (int)Mathf.Sqrt(Mathf.Pow(vector2.x - vector1.x, 2) + Mathf.Pow(vector2.y - vector1.y, 2));
    }

    /// <summary>
    /// Calculate distance between 2 pos in Vector2int and return a float
    /// </summary>
    /// <param name="vector1">The first Vector2Int</param>
    /// <param name="vector2">The second Vector2Int</param>
    /// <returns></returns>
    public static float DistanceBetweenTwoPos_float(Vector2Int vector1, Vector2Int vector2)
    {
        return DistanceBetweenTwoPos(ConvertToString(vector1), ConvertToString(vector2));

        // return Mathf.Sqrt(Mathf.Pow(vector2.x - vector1.x, 2) + Mathf.Pow(vector2.y - vector1.y, 2));
    }

    /// <summary>
    /// Calculate distance between 2 pos in Vector2 and return a float
    /// </summary>
    /// <param name="vector1">The first Vector2Int</param>
    /// <param name="vector2">The second Vector2Int</param>
    /// <returns></returns>
    public static float DistanceBetweenTwoPos_float(Vector2 vector1, Vector2 vector2)
    {
        return Mathf.Sqrt(Mathf.Pow(vector2.x - vector1.x, 2) + Mathf.Pow(vector2.y - vector1.y, 2));
    }

    /// <summary>
    /// calculate distance between 2 pos_string (int value) with diagonal used for path H calculation
    /// </summary>
    /// <param name="pos1">The first pos</param>
    /// <param name="pos2">The second pos</param>
    /// <returns></returns>
    public static int DistanceBetweenTwoPos_WithDiagonal(string pos1, string pos2)
    {
        (int x, int y) p1 = ReadString(pos1);
        (int x, int y) p2 = ReadString(pos2);

        int diffx = Mathf.Abs(p1.x - p2.x);
        int diffy = Mathf.Abs(p1.y - p2.y);

        if (diffx > diffy)
        {
            return 14 * diffy + 10 * (diffx - diffy);
        }
        else
        {
            return 14 * diffx + 10 * (diffy - diffx);
        }
    }

    /// <summary>
    /// Calculate vector distance bewteen 2 pos_string with negative and positive value possible
    /// </summary>
    /// <param name="pos1">The first pos</param>
    /// <param name="pos2">The second pos</param>
    /// <returns></returns>
    public static Vector2Int DistanceBetweenTwoPos_inVector2Int(string pos1, string pos2)
    {
        (int x, int y) p1 = ReadString(pos1);
        (int x, int y) p2 = ReadString(pos2);

        return new Vector2Int(p2.x - p1.x, p2.y - p1.y);
    }

    /// <summary>
    /// Calculate vector distance bewteen 2 pos_string with negative and positive value possible
    /// </summary>
    /// <param name="pos1">The first pos</param>
    /// <param name="pos2">The second pos</param>
    /// <returns></returns>
    public static Vector2Int DistanceBetweenTwoPos_inVector2Int(Vector2Int pos1, Vector2Int pos2)
    {
        (int x, int y) p1 = (pos1.x, pos1.y);
        (int x, int y) p2 = (pos2.x, pos2.y);

        return new Vector2Int(p2.x - p1.x, p2.y - p1.y);
    }

    /// <summary>
    /// Calculate vector distance bewteen 2 pos_string with positive or negative value possible (depending of Abs)
    /// </summary>
    /// <param name="pos1">The first pos</param>
    /// <param name="pos2">The second pos</param>
    /// <param name="Abs">If the result must be positive or not</param>
    /// <returns></returns>
    public static Vector2Int DistanceBetweenTwoPos_inVector2Int(string pos1, string pos2, bool Abs)
    {
        (int x, int y) p1 = ReadString(pos1);
        (int x, int y) p2 = ReadString(pos2);

        if (Abs)
        {
            return new Vector2Int(Mathf.Abs(p2.x - p1.x), Mathf.Abs(p2.y - p1.y));
        }
        else
        {
            return new Vector2Int(p2.x - p1.x, p2.y - p1.y);
        }
    }

    /// <summary>
    /// Calculate distance bewteen 2 entity (int value) possitive value only
    /// </summary>
    /// <param name="entity1">The first entity</param>
    /// <param name="entity2">The second entity</param>
    /// <returns></returns>
    public static int DistanceBetweenTwoPos(Entity entity1, Entity entity2)
    {
        return DistanceBetweenTwoPos(entity1.CurrentPosition_string, entity2.CurrentPosition_string);
    }

    /// <summary>
    /// Calculate distance bewteen 2 entity with x and y separatly (int x, int y) negative value possible
    /// </summary>
    /// <param name="entity1">The first entity</param>
    /// <param name="entity2">The second entity</param>
    /// <returns></returns>
    public static (int x, int y) DistanceBetweenTwoPos_xy(Entity entity1, Entity entity2)
    {
        return DistanceBetweenTwoPos_xy(entity1.CurrentPosition_string, entity2.CurrentPosition_string);
    }

    /// <summary>
    /// Calculate distance bewteen 2 entity with x and y separatly (int x, int y) negative value possible
    /// </summary>
    /// <param name="entity1">The first entity</param>
    /// <param name="entity2">The second entity</param>
    /// <returns></returns>
    public static (int x, int y) DistanceBetweenTwoPos_xy(string pos1, string pos2)
    {
        (int x, int y) e1 = ReadString(pos1);
        (int x, int y) e2 = ReadString(pos2);

        return (e1.x - e2.x, e1.y - e2.y);
    }


    /// <summary>
    /// Calculate distance bewteen 2 entity with x and y separatly (int x, int y) negative value possible or not depending of abs
    /// </summary>
    /// <param name="entity1">The first entity</param>
    /// <param name="entity2">The second entity</param>
    /// <param name="abs">Determine if the value must be negative or positive</param>
    /// <returns></returns>
    public static (int x, int y) DistanceBetweenTwoPos_xy(Entity entity1, Entity entity2, bool abs)
    {
        if (abs)
        {
            (int x, int y) e1 = ReadString(entity1.CurrentPosition_string);
            (int x, int y) e2 = ReadString(entity2.CurrentPosition_string);

            return (Mathf.Abs(e1.x - e2.x), Mathf.Abs(e1.y - e2.y));
        }
        else
        {
            (int x, int y) e1 = ReadString(entity1.CurrentPosition_string);
            (int x, int y) e2 = ReadString(entity2.CurrentPosition_string);

            return (e1.x - e2.x, e1.y - e2.y);
        }
    }

    /// <summary>
    /// Additionate two pos with each other
    /// </summary>
    /// <param name="pos1">The first position</param>
    /// <param name="pos2">The second position</param>
    /// <returns></returns>
    public static string AdditionPos(string pos1, string pos2)
    {
        (int x, int y) pos1xy = ReadString(pos1);
        (int x, int y) pos2xy = ReadString(pos2);

        return ConvertToString(pos1xy.x + pos2xy.x, pos1xy.y + pos2xy.y);
    }

    public static float DegreeTowardThisDirection(string pos1, string pos2)
    {
        Vector2 pos1_world = F.ConvertToWorldVector2(pos1);

        Vector2 pos2_world = F.ConvertToWorldVector2(pos2);

        return DegreeTowardThisDirection(pos1_world, pos2_world);
    }

    /// <summary>
    /// The object must be oriented to the left
    /// </summary>
    /// <param name="pos1"></param>
    /// <param name="pos2"></param>
    /// <returns></returns>
    public static float DegreeTowardThisDirection(Vector2 pos1, Vector2 pos2)
    {
        //The object must be oriented to the right

        float len_x = /*Mathf.Abs(*/pos1.x - pos2.x;

        float len_y = /*Mathf.Abs(*/pos1.y - pos2.y;

        float additional = 0;

        if (len_x < 0)
            additional += 180;

        return Mathf.Atan(len_y / len_x) * Mathf.Rad2Deg + additional;
    }

    /// <summary>
    /// Show x decimal after the comma
    /// </summary>
    /// <param name="value">the float value to show decimal</param>
    /// <param name="precision">The number of letter to show after coma</param>
    /// <returns></returns>
    public static float ShowXdecimal(float value, float precision)
    {
        float powValue = Mathf.Pow(10, precision);

        value = value * powValue;
        value = Mathf.Floor(value);
        return value / (powValue);
    }

    /// <summary>
    /// Calculate the fillamount of a lifebar for exemple
    /// </summary>
    /// <param name="current">the current value</param>
    /// <param name="max">the max value</param>
    /// <returns></returns>
    public static float CalculateFillAmount(float current, float max)
    {
        return current / max;
    }

    /// <summary>
    /// Convert euler angle into the angle we see in the inspector window
    /// </summary>
    /// <param name="EulerAngle">The euler angle we want to convert</param>
    /// <returns></returns>
    public static float ConvertEulerAngleIntoInspectorAngle(float EulerAngle)
    {
        if (EulerAngle <= 180f)
        {
            return EulerAngle;
        }
        else
        {
            return EulerAngle - 360f;
        }
    }

    /// <summary>
    /// Calculate the hypothenus of a rectangle triangle
    /// </summary>
    /// <param name="a">One side of the rectangle triangle</param>
    /// <param name="b">The other side of the rectangle triangle</param>
    /// <returns></returns>
    public static float CalculateHypothenus(float a, float b)
    {
        return Mathf.Abs(Mathf.Sqrt(Mathf.Pow(a, 2) + Mathf.Pow(b, 2)));
    }



    /// <summary>
    /// USE LOSSYSCALE
    /// </summary>
    /// <param name="target">USE LOSSYSCALE</param>
    /// <returns></returns>
    public static (float x, float y) CalculateRealScale_xy(Transform target)
    {
        return (CalculateRealScale(target, target.localScale.x), CalculateRealScale(target, target.localScale.y));
    }

    public static float CalculateRealScale(Transform target, float startValue)
    {
        float FinalScale = startValue;
        Transform current = target.transform;

        while (current.parent != null)
        {
            FinalScale /= current.parent.localScale.x;

            current = current.parent;
        }

        return FinalScale;
    }

    public static float CalculateRealScale(Transform target)
    {
        return CalculateRealScale(target, target.transform.localScale.x);
    }

    public static bool IsBetweenTwoValues(float value, float min, float max)
    {
        return (value - min) * (max - value) >= 0;
    }

    public static bool IsBetweenTwoValues_LimitNotAccepted(float value, float min, float max)
    {
        return (value - min) * (max - value) > 0;
    }

    /// <summary>
    /// Compare vector, return the shortest vector and change actualPoint by potentialPoint if v2 is is shorter than V1
    /// </summary>
    /// <param name="v1">The first vecotr</param>
    /// <param name="v2">The second (if it's the shortest change the value of ActualPoint)</param>
    /// <param name="ActualPoint">The point we want to change</param>
    /// <param name="potentialPoint">The value that "actualpoint" will take if v2 is shorter than v</param>
    /// <returns></returns>
    public static Vector3 CompareVector(Vector3 v1, Vector3 v2, ref Window.StretchDirection ActualPoint, Window.StretchDirection potentialPoint)
    {
        if (Mathf.Abs(v2.x) + Mathf.Abs(v2.y) < Mathf.Abs(v1.x) + Mathf.Abs(v1.y))
        {
            ActualPoint = potentialPoint;
            return v2;
        }
        else
        {
            return v1;
        }
    }

    public static bool IsTileCrossable(string pos1, string pos2)
    {
        Vector2Int pos1Int = F.ConvertToVector2Int(pos1);
        Vector2Int distance = F.DistanceBetweenTwoPos_inVector2Int(pos1, pos2);

        if (distance.x != 0 && !F.IsTileWalkable(F.ConvertToString(pos1Int.x + distance.x, pos1Int.y)))
            return false;

        if (distance.y != 0 && !F.IsTileWalkable(F.ConvertToString(pos1Int.x, pos1Int.y + distance.y)))
            return false;

        return true;
    }
}
