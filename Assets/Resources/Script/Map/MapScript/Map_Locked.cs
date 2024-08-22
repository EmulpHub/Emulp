using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Map : MonoBehaviour
{
    private List<DirectionData.Direction> _locked = new List<DirectionData.Direction>();

    public static EventHandlerListDirection event_unlockArea = new EventHandlerListDirection(true);

    private List<DirectionData.Direction> locked
    {
        get
        {
            return _locked;
        }
        set
        {
            _locked = value;

            DirectionData.LoadAllDirectionData(posInWorld);
        }
        
    }

    public void Locked_Set (List<DirectionData.Direction> locked)
    {
        this.locked = locked;
    } 

    public void Locked_clear ()
    {
        event_unlockArea.Call(locked);

        locked = new List<DirectionData.Direction>();
    }

    public bool isLocked (DirectionData.Direction direction)
    {
        return locked.Contains(direction);
    }
}
