using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Effect : MonoBehaviour
{
    public static int effect_id;

    public Effect()
    {
        id = effect_id;
        effect_id++;

        SetInfoString();
        SetDurationString();
    }

    public virtual void eventAdding(Entity holder)
    {
        event_add.Call(this);
    }

    public virtual void eventKilling()
    {
        event_kill.Call(this);
    }

}
