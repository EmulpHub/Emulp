using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public partial class Scene_Main : MonoBehaviour
{
    #region Combat_management

    public GameObject actionToDo_Debug;
    public bool actionToDo_Debug_enable = false;

    public void Combat_management()
    {
        if (EntityOrder.Instance.IsTurnOf_Player())
        {
            Player_turn_management();
        }

    }

    #region Player_turn_management

    public void Player_turn_management()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ClickAutorization.Autorized(Main_Object.Get(Main_Object.objects.button_endOfTurn)))
        {
            Action_nextTurn.Add();
        }
    }

    public static Entity entityShowMovement;

    public enum ChangingOfViewMode { NoEntityShowMovement, EntityShowMovement }

    public static void ChangingOfView(ChangingOfViewMode ModeView, Entity ShowMovement = null)
    {
        if (ModeView == ChangingOfViewMode.NoEntityShowMovement)
        {
            if (entityShowMovement != null)
                entityShowMovement = null;
        }
        else
        {
            entityShowMovement = ShowMovement;
        }
    }

    public static float timeToShowMovement;

    public static void SetNewDelayBeforeShowingMovement()
    {
        timeToShowMovement = Time.time + 0.2f;
    }

    public Dictionary<(string pos1, string pos2), List<string>> enregistredPath = new Dictionary<(string pos1, string pos2), List<string>>();

    public void EnregistredPath_add(string pos1, string pos2, List<string> path)
    {
        enregistredPath.Add((pos1, pos2), path);
    }

    public bool EnregistredPath_contain(string pos1, string pos2)
    {
        return enregistredPath.ContainsKey((pos1, pos2));
    }

    public List<string> EnregistredPath_get(string pos1, string pos2)
    {
        return enregistredPath[(pos1, pos2)];
    }

    public void EnregistredPath_clear()
    {
        enregistredPath.Clear();
    }

    public static void Set_PossibleMovementTile(Entity entity)
    {
        TileInfo.Instance.ListTile_Clear();

        var result = PathFindingName.PossibleWalkingTile.Make(entity.CurrentPosition_string, entity.Info.PM);

        bool forPlayer = entity.Info.IsPlayer();

        foreach (var info in result)
        {
            string pos = info.Key;
            var distance = info.Value;

            if (!entity.IsReachable(distance)) continue;

            if (forPlayer)
            {
                var data = new TileData_movement(pos, distance);
                
                Tile_Movement.Add(data);
            }
            else
            {
                var data = new TileData_graphic(pos, Tile_Gestion.Color.green);

                Tile_Graphic.Add(data);
            }
        }

        Tile_Gestion.Instance.UpdateAllTileSprite();
    }


    public void Set_PossibleSpellTile(Spell.Range_type range_Type)
    {
        TileInfo.Instance.ListTile_Clear();

        string startPosition = V.player_entity.CurrentPosition_string;

        List<string> posToCalculate = F.TileBetweenStartandEndDistance(SpellGestion.selectionnedSpell.range_min, SpellGestion.selectionnedSpell.range_max, startPosition);

        foreach (string pos in posToCalculate)
        {
            if (F.IsTileExistWithNoObstacle(F.ConvertToVector2Int(pos)))
            {

                if (range_Type == Spell.Range_type.line && F.IsInLine(startPosition, pos))
                {
                    AddSpellAtPos(pos, range_Type);
                }
                else if (range_Type != Spell.Range_type.line)
                {
                    AddSpellAtPos(pos, range_Type);

                }
            }
        }

        Tile_Gestion.Instance.UpdateAllTileSprite();
    }

    public void AddSpellAtPos(string pos, Spell.Range_type range_type)
    {
        bool lineOfView = F.IsLineOfView(pos, V.player_entity.CurrentPosition_string);

        var data = new TileData_spell(pos, lineOfView);

        Tile_Spell.Add(data);
    }

    public static void SetGameAction_movement()
    {
        Scene_Main.ChangingOfView(ChangingOfViewMode.NoEntityShowMovement);

        Window.SetCursorAndOffset(null, Window.CursorMode.click_cursor);

        SpellGestion.SetSelectionnedSpell(SpellGestion.List.empty, null);

        V.game_state_action = V.State_action.movement;

        if (!ActionManager.Instance.Running() && EntityOrder.Instance.IsTurnOf_Player())
            Set_PossibleMovementTile(V.player_entity);
    }

    public static void SetGameAction_showMonsterMovement(Entity monster)
    {
        Scene_Main.ChangingOfView(ChangingOfViewMode.EntityShowMovement, monster);

        Window.SetCursorAndOffset(null, Window.CursorMode.click_cursor);

        SpellGestion.SetSelectionnedSpell(SpellGestion.List.empty, null);

        V.game_state_action = V.State_action.Nothing;

        if (!ActionManager.Instance.Running())
            Set_PossibleMovementTile(monster);
    }

    public static void SetNoAction()
    {
        V.game_state_action = V.State_action.Nothing;

        TileInfo.Instance.ListTile_Clear();
    }

    #endregion

    #endregion

}
