using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFindingName;

public partial class Monster : Entity
{
    public static Dictionary<Entity, string> saveEntityPosition = new Dictionary<Entity, string>();

    public override void OnMouseIsDown_Left()
    {
        if (!ClickAutorization.Autorized(this.gameObject))
            return;

        base.OnMouseIsDown_Left();

        if (V.game_state == V.State.passive)
        {
            if (Map_PossibleToMove.CanPlayerMove(CurrentPosition_string, this))
            {
                StopRun();

                SetRandomCooldownPeriodicMove();

                string posToGo = runningInfo.NextPos;

                saveEntityPosition.Clear();

                saveEntityPosition.Add(this, posToGo);

                var pathParam = new PathParam(V.player_entity.CurrentPosition_string, posToGo).AddListIgnoreEntity(this);

                V.player_entity.MoveTo(pathParam);
            }
        }
    }

    public override void OnMouseIsOver()
    {
        base.OnMouseIsOver();

        AddHighLightMonster();
        
        if (V.game_state == V.State.fight && !Scene_Main.aWindowIsUsed && Scene_Main.entityShowMovement != this && V.game_state_action == V.State_action.movement && !V.Tutorial_Get())
        {
            Scene_Main.SetGameAction_showMonsterMovement(this);
        }

        event_all_monster_mouseOver.Call(this);
    }

    public static string ListMonsterToDesc(List<Monster> monster_list)
    {
        string txt = "";

        foreach (Monster m in monster_list)
        {
            if (txt != "")
                txt += "\n";

            txt += m.monsterInfo.Title();
        }

        return txt;
    }

    public static string ListMonsterToTitle(List<Monster> monster_list)
    {
        string txt = "";

        int level = 0;

        foreach (Monster m in monster_list)
        {
            level += m.monsterInfo.level;
        }

        if (monster_list.Count > 1)
        {
            if (V.IsFr())
                txt = txt.Insert(0, descColor.convert("Groupe Niv. " + level + "") + "\n");
            else
                txt = txt.Insert(0, descColor.convert("Group Lvl." + level + "") + "\n");
        }

        return txt;
    }

    public override void OnMouseIsExit()
    {
        if (V.game_state == V.State.fight && Scene_Main.entityShowMovement == this)
            Scene_Main.SetGameAction_movement();

        base.OnMouseIsExit();

        EraseHighlighMonster();

        Main_UI.Display_MonsterList_Erase();

        Main_UI.Display_Title_Erase();

        Main_UI.ManageDontMoveCursor(this.gameObject, false);

        event_all_monster_mouseExit.Call(this);
    }

    public static List<Monster> HighlighMonster = new List<Monster>();

    public void AddHighLightMonster()
    {
        if (V.game_state == V.State.fight)
            HighlighMonster = new List<Monster>() { this };
        else
            HighlighMonster = new List<Monster>(Main_Map.currentMap.monsterInArea);
    }

    public static void EraseHighlighMonster()
    {
        foreach (Monster m in HighlighMonster)
        {
            m.Thicness_Exit();
        }

        HighlighMonster.Clear();
    }
}
