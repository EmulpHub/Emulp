using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfRunInfo : MonoBehaviour
{
    public enum state { win, loose }

    public state State;

    public EndOfRunInfo(state state)
    {
        State = state;
    }
}
