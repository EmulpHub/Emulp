using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Entity : MonoBehaviour
{
    public static EventHandlerEntity event_allEntity_turnStart = new EventHandlerEntity(true);
    public static EventHandlerEntity event_allEntity_turnEnd = new EventHandlerEntity(true);
    public static EventHandlerNoArg event_allEntity_push = new EventHandlerNoArg(true);
    public static EventHandlerEntityDmg event_allEntity_dmg = new EventHandlerEntityDmg(true);
    public EventHandlerEntity event_turn_start = new EventHandlerEntity(false);
    public EventHandlerEntity event_turn_end = new EventHandlerEntity(false);
    public EventHandlerInfoDamage event_dmg = new EventHandlerInfoDamage(false);
    public EventHandlerInfoDamage event_life_dmg = new EventHandlerInfoDamage(false);
    public EventHandlerInfoHeal event_life_heal = new EventHandlerInfoHeal(false);
    public EventHandlerEntity event_die = new EventHandlerEntity(false);
}
