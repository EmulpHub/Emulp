using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class WorldNavigation : MonoBehaviour
{
    public static (bool find, DirectionData.information directionInformation) curstate_mouse;

    public void Start()
    {
        LockStart();
    }

    private void Update()
    {
        DirectionData.information PlayerDirectionState = NavigationData.CalcCurrentState(V.player_entity.CurrentPosition_string, V.player_entity.transform.position, true).information;

        if (!V.player_entity.runningInfo.isRunning)
        {
            ActionDependingOfCurrentState(PlayerDirectionState);
        }
        if (ClickAutorization.Autorized(V.map_possibleToMove.gameObject))
            curstate_mouse = NavigationData.CalcCurrentState(CursorInfo.Instance.position, CursorInfo.Instance.positionInWorld);
        else
            curstate_mouse = (false, null);

        LockGestion();

        MouseCursorChange();
    }

    public void ActionDependingOfCurrentState(DirectionData.information information)
    {
        if (information.IsLocked || !information.CanGo)
            return;

        switch (information.direction)
        {
            case DirectionData.Direction.right:
                WorldLoad.LoadMap_Right();
                break;

            case DirectionData.Direction.left:
                WorldLoad.LoadMap_Left();
                break;

            case DirectionData.Direction.up:
                WorldLoad.LoadMap_Up();
                break;

            case DirectionData.Direction.down:
                WorldLoad.LoadMap_Down();
                break;

            default:
                break;
        }
    }
}