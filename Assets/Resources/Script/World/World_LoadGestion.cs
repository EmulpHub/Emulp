using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using LayerWorldGeneration;

public class WorldLoad : MonoBehaviour
{
    public static void GenerateWorldAndMiniMap(int distance)
    {
        WorldData.distance = distance;
        WorldGeneration.GenerateRandomWorld();

        Load("0_0");
        V.script_Scene_Main.miniMap.InitWorld();
    }

    public static EventHandlerString event_loadMap = new EventHandlerString(true);

    public static void Load(string posToLoad, NavigationData.type playerDirectionArrival = NavigationData.type.nul)
    {
        WorldData.PlayerPositionInWorld = posToLoad;

        Map theMap = WorldData.GetMapInfo(posToLoad).Instantiate();

        Main_Map.ChangeMap(theMap);

        TeleportPlayerDependingOfArrivalDirection(playerDirectionArrival, theMap);

        MapTransitionAnimation.Next();

        DirectionData.LoadAllDirectionData(posToLoad);

        event_loadMap.Call(posToLoad);
    }

    public static void LoadMap_Right()
    {
        Load(F.AdditionPos(WorldData.PlayerPositionInWorld, "1_0"), NavigationData.type.go_Right);
    }

    public static void LoadMap_Left()
    {
        Load(F.AdditionPos(WorldData.PlayerPositionInWorld, "-1_0"), NavigationData.type.go_Left);
    }

    public static void LoadMap_Up()
    {
        Load(F.AdditionPos(WorldData.PlayerPositionInWorld, "0_1"), NavigationData.type.go_Up);
    }

    public static void LoadMap_Down()
    {
        Load(F.AdditionPos(WorldData.PlayerPositionInWorld, "0_-1"), NavigationData.type.go_Down);
    }

    public static void TeleportPlayerDependingOfArrivalDirection(NavigationData.type playerDirectionArrival, Map m)
    {
        string playerPos = CalculatePlayerPositionWhenChangingMap(playerDirectionArrival, m);

        F.TeleportEntity(playerPos, V.player_entity, false, false);
    }

    public static string CalculatePlayerPositionWhenChangingMap(NavigationData.type dir, Map m)
    {
        if (dir == NavigationData.type.nul)
            return m.playerStartPos;

        int xDifference = 0;
        int yDifference = 0;

        if (dir == NavigationData.type.go_Down)
            yDifference = 10;
        else if (dir == NavigationData.type.go_Up)
            yDifference = -10;
        else if (dir == NavigationData.type.go_Right)
            xDifference = -12;
        else if (dir == NavigationData.type.go_Left)
            xDifference = 12;

        float playerPosX = V.player_entity.transform.position.x + xDifference;
        float playerPosY = V.player_entity.transform.position.y + yDifference;

        string playerPos = F.ConvertToString(F.ConvertToGridVector(new Vector2(playerPosX, playerPosY)));

        dir = NavigationData.ReverseDirection(dir);

        if (dir == NavigationData.type.go_Right)
            NavigationData.bannedDirection = DirectionData.Direction.right;
        else if (dir == NavigationData.type.go_Left)
            NavigationData.bannedDirection = DirectionData.Direction.left;
        else if (dir == NavigationData.type.go_Up)
            NavigationData.bannedDirection = DirectionData.Direction.up;
        else if (dir == NavigationData.type.go_Down)
            NavigationData.bannedDirection = DirectionData.Direction.down;

        return m.GetNearRespawnPos(dir, playerPos);
    }
}
