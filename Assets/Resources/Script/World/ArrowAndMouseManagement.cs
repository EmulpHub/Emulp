using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class WorldNavigation : MonoBehaviour
{
    public void LockStart()
    {
        Map_PossibleToMove.Event_playerMove.Add(WhenPlayerMove);
    }

    public EventHandlerNoArg event_MoveToNextArea = new EventHandlerNoArg(false);

    public void WhenPlayerMove(string position)
    {
        if (curstate_mouse.find && curstate_mouse.directionInformation.CanGo && !curstate_mouse.directionInformation.IsLocked)
        {

            var areaDirection = curstate_mouse.directionInformation.direction;

            var runningInfo = V.player_entity.runningInfo;

            event_MoveToNextArea.Call();

            if (runningInfo.directionToArea == areaDirection)
            {
                SoundManager.PlaySound(SoundManager.list.environment_movingToSameArea);
            }
            else
            {
                SoundManager.PlaySound(SoundManager.list.environment_movingToNewArea);
            }

            V.player_entity.runningInfo.SetDirectionToArea(curstate_mouse.directionInformation.direction);

        }
    }

    public GameObject Lock_Parent;

    public bool ShowLock()
    {
        return curstate_mouse.find && curstate_mouse.directionInformation.IsLocked && !Scene_Main.isMouseOverAWindow;
    }

    public Vector2 LockPosition_down, LockPosition_up, LockPosition_right, LockPosition_left;

    public void LockGestion()
    {
        Lock_Parent.gameObject.SetActive(ShowLock());

        if (!ShowLock())
            return;

        switch (curstate_mouse.directionInformation.direction)
        {
            case DirectionData.Direction.right:
                Lock_Parent.transform.position = LockPosition_right;
                break;

            case DirectionData.Direction.left:
                Lock_Parent.transform.position = LockPosition_left;
                break;

            case DirectionData.Direction.up:
                Lock_Parent.transform.position = LockPosition_up;
                break;

            case DirectionData.Direction.down:
                Lock_Parent.transform.position = LockPosition_down;
                break;
        }
    }

    public void MouseCursorChange()
    {
        bool ChangeCursor = curstate_mouse.find && !curstate_mouse.directionInformation.IsLocked;

        Main_UI.ManageDontMoveCursor(this.gameObject, ChangeCursor);

        if (ChangeCursor)
        {
            Window.SetCursorAndOffsetHand();
        }
    }
}
