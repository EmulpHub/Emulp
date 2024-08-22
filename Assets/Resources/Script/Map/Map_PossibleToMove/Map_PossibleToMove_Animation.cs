using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Map_PossibleToMove : MonoBehaviour
{
    public GameObject movementPoint_Marker;

    public GameObject currentMarker;

    public MarkerArrowAnimation currentArrow;
    public MarkerArrowAnimation_Map currentArrowMap;

    public void movingAnimation(Vector2 targetPosition)
    {
        DestroyMarker();

        if (V.IsFight()) return;

        V.player_entity.ParticleLeaf(1, 0.5f, targetPosition);

        if (!WorldNavigation.curstate_mouse.find || WorldNavigation.curstate_mouse.directionInformation.IsLocked)
        {
            GameObject Marker = Instantiate(V.map_possibleToMove.movementPoint_Marker);

            Marker.transform.position = targetPosition;

            currentMarker = Marker;

            currentArrow.SetPosition(targetPosition);
        }
        else if (WorldNavigation.curstate_mouse.find && !WorldNavigation.curstate_mouse.directionInformation.IsLocked)
        {
            currentArrowMap.dir = WorldNavigation.curstate_mouse.directionInformation.direction;

            currentArrowMap.SetPosition(targetPosition);

        }
    }

    public void DestroyMarker()
    {
        if (currentMarker != null)
        {
            Destroy(currentMarker);

        }

        currentArrow.transform.position = new Vector3(100, 0, 0);
        currentArrowMap.transform.position = new Vector3(110, 0, 0);
    }
}
