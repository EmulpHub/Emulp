using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_spell_info_player : Action_spell_info
{
    public float str { get; private set; }
    public float dex { get; private set; }
    public float res { get; private set; }
    public int eff { get; private set; }

    public Action_spell_info_player(Spell spell, Entity target, string targetedSquare) : base(spell, V.player_entity, target, targetedSquare)
    {
        str = V.player_info.str;
        dex = V.player_info.dex;
        res = V.player_info.res;
        eff = V.player_info.eff;
    }
}