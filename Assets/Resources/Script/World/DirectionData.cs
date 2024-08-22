using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DirectionData : MonoBehaviour
{
    public enum Direction { right, left, up, down, empty };

    public class information
    {
        public Direction direction;

        public bool CanGo, IsLocked;

        public information(bool CanGo, bool IsLocked, Direction dir)
        {
            Set(CanGo, IsLocked, dir);
        }

        public void Set(bool CanGo, bool IsLocked, Direction dir)
        {
            this.CanGo = CanGo;
            this.IsLocked = IsLocked;
            direction = dir;
        }

        public override string ToString()
        {
            return "Direction " + direction.ToString() + "(canGo = " + CanGo + " IsLocked = " + IsLocked + ")";
        }
    }

    private static List<information> allPossibleDirectionForChangingArea = new List<information>();

    public static information GetInfoGoFromDirection(Direction direction)
    {
        return allPossibleDirectionForChangingArea.FirstOrDefault(a => a.direction == direction);
    }

    public static void PrintAllDirection()
    {
        foreach (information i in allPossibleDirectionForChangingArea)
        {
            print(i);
        }
    }

    public static void InitallPossibleDirectionForChangingArea()
    {
        if (allPossibleDirectionForChangingArea.Count > 0) return;

        for (int i = 0; i < 4; i++)
        {
            allPossibleDirectionForChangingArea.Add(null);
        }
    }

    public static void LoadAllDirectionData(string loadedMapPos)
    {
        InitallPossibleDirectionForChangingArea();

        List<(Direction direction, string pos)> directionToAdd = new List<(Direction direction, string pos)>
        {
            (Direction.right,"1_0"),
            (Direction.left,"-1_0"),
            (Direction.up,"0_1"),
            (Direction.down,"0_-1"),
        };

        int index = 0;

        foreach ((Direction direction, string pos) toAdd in directionToAdd)
        {
            bool canItGo = WorldData.Contain(F.AdditionPos(loadedMapPos, toAdd.pos));

            bool isLocked = Main_Map.currentMap.isLocked(toAdd.direction);

            information info = new information(canItGo, isLocked, toAdd.direction);

            allPossibleDirectionForChangingArea[index] = info;

            index++;
        }
    }

    public static void ModifyRotationGameobjectDependignOfDir(GameObject g, Direction dir, int additional = 0)
    {
        switch (dir)
        {
            case Direction.right:
                g.transform.rotation = Quaternion.Euler(0, 0, -90 + additional);
                break;

            case Direction.left:
                g.transform.rotation = Quaternion.Euler(0, 0, 90 + additional);
                break;

            case Direction.up:
                g.transform.rotation = Quaternion.Euler(0, 0, 0 + additional);
                break;

            case Direction.down:
                g.transform.rotation = Quaternion.Euler(0, 0, 180 + additional);
                break;
        }
    }

    public static Direction GetDirection(string pos1, string pos2)
    {
        var diff = F.DistanceBetweenTwoPos_xy(pos1, pos2);

        if (diff.x < 0)
            return Direction.right;
        else if (diff.x > 0)
            return Direction.left;
        else if (diff.y < 0)
            return Direction.up;
        else if (diff.y > 0)
            return Direction.down;
        else
            return Direction.empty;
    }

    public static Vector2Int ConvertToVector2Int(string pos1, string pos2)
    {
        return ConvertToVector2Int(GetDirection(pos1, pos2));
    }

    public static Vector2Int ConvertToVector2Int(Direction dir)
    {
        switch (dir)
        {
            case Direction.right:
                return new Vector2Int(1, 0);
            case Direction.left:
                return new Vector2Int(-1, 0);
            case Direction.up:
                return new Vector2Int(0, 1);
            case Direction.down:
                return new Vector2Int(0, -1);
            case Direction.empty:
                return Vector2Int.zero;
        }

        throw new System.Exception("eeee c'est pas possible ?");
    }

    public static Vector3 Vector2IntToVector3(Vector2Int vector2Int)
    {
        return new Vector3(vector2Int.x, vector2Int.y, 0);
    }
}
