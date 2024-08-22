using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster : Entity
{
    public static EventHandlerEntityFloat event_all_monster_dmg = new EventHandlerEntityFloat(true);
    public static EventHandlerEntity event_all_monster_mouseOver = new EventHandlerEntity(true);
    public static EventHandlerEntity event_all_monster_mouseExit = new EventHandlerEntity(true);
    public static EventHandlerEntity event_all_monster_remove_pa = new EventHandlerEntity(true);
    public static EventHandlerEntity event_all_monster_remove_pm = new EventHandlerEntity(true);
    public static EventHandlerEntity event_all_monster_remove_paOrPm = new EventHandlerEntity(true);
    public static EventHandlerEntity event_all_monster_die = new EventHandlerEntity(true);
}
