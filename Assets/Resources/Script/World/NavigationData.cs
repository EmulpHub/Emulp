using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationData : MonoBehaviour
{
    public enum type { nul, go_Right, go_Left, go_Up, go_Down }

    public static type ReverseDirection(type t)
    {
        switch (t)
        {
            case type.go_Right:
                return type.go_Left;
            case type.go_Left:
                return type.go_Right;
            case type.go_Up:
                return type.go_Down;
            case type.go_Down:
                return type.go_Up;
            default:
                return type.nul;
        }
    }

    public static bool DirectionDateIsNavigationType(type type, DirectionData.Direction direction)
    {
        if (type == type.go_Right && direction == DirectionData.Direction.right)
        {
            return true;
        }
        else if (type == type.go_Left && direction == DirectionData.Direction.left)
        {
            return true;
        }
        else if (type == type.go_Up && direction == DirectionData.Direction.up)
        {
            return true;
        }
        else if (type == type.go_Down && direction == DirectionData.Direction.down)
        {
            return true;
        }

        return false;
    }

    public static DirectionData.Direction bannedDirection = DirectionData.Direction.empty;

    private const float minX = -10, maxX = 10, minY = -2.5f, maxY = 5;

    public static (bool find, DirectionData.information information) CalcCurrentState(string posTile, Vector2 positionInWorld, bool bannedCondition = false)
    {
        DirectionData.information inf = new DirectionData.information(false, false, DirectionData.Direction.right);

        if (Main_Map.currentMap == null || V.IsFight() || Map_PossibleToMove.MouseIsOnToolbar)
        {
            return (false, new DirectionData.information(false, false, DirectionData.Direction.right)); //World.InfoGo(false, false);
        }

        type type = Main_Map.currentMap.getCurrentStateFromPos(posTile);

        if (bannedCondition && DirectionDateIsNavigationType(type, bannedDirection))
            return (false, inf);

        if (type == type.go_Right && positionInWorld.x >= maxX)
        {
            inf = DirectionData.GetInfoGoFromDirection(DirectionData.Direction.right);
        }
        else if (type == type.go_Left && positionInWorld.x <= minX)
        {
            inf = DirectionData.GetInfoGoFromDirection(DirectionData.Direction.left);
        }
        else if (type == type.go_Up && positionInWorld.y >= maxY)
        {
            inf = DirectionData.GetInfoGoFromDirection(DirectionData.Direction.up);
        }
        else if (type == type.go_Down && positionInWorld.y <= minY)
        {
            inf = DirectionData.GetInfoGoFromDirection(DirectionData.Direction.down);
        }
        else
        {
            return (false, inf);
        }

        return (true, inf);

    }
}
