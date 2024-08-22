using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Effect : MonoBehaviour
{
    public EventHandlerEffect event_add = new EventHandlerEffect(false);
    public EventHandlerEffect event_kill = new EventHandlerEffect(false);
    public EventHandlerfloatBeforeAfter event_strChanging = new EventHandlerfloatBeforeAfter(false);
}
