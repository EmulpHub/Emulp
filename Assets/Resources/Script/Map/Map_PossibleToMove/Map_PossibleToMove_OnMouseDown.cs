using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using PathFindingName;

public partial class Map_PossibleToMove : MonoBehaviour
{

    private void OnMouseDown()
    {
        if (Scene_Main.isMouseOverAWindow || Scene_Main.isMouseOverAWindow || MouseIsOnToolbar ||
            !ClickAutorization.Autorized(this.gameObject))
            return;

        V.player_entity.ParticleLeaf(1, 0.6f, CursorInfo.Instance.positionInWorld3);

        switch (V.game_state)
        {
            case V.State.fight:
                MakeAction_Fight(CursorInfo.Instance.position); break;
            case V.State.positionning:
                MakeAction_Positinionning(CursorInfo.Instance.position); break;
            case V.State.passive:
                MakeAction_Passive(CursorInfo.Instance.position); break;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(1))
            SpellGestion.ResetSelectionnedSpell();
    }

    public static bool MouseIsOnToolbar;

    public static bool CanPlayerMove(string position, Entity ignoreEntity = null)
    {
        return PlayerMoveAutorization.Instance.Can() && F.IsTileWalkable(position, ignoreEntity) &&
            !WindowInfo.Instance.isMouseOverAreaOfAWindow;
    }

    public static EventHandlerString Event_playerMove = new EventHandlerString(true);

    void MakeAction_Passive(string position)
    {

        if (CanPlayerMove(position))
        {
            if (!F.IsTileConfortablySeenable(position) && (!WorldNavigation.curstate_mouse.find || WorldNavigation.curstate_mouse.directionInformation.IsLocked))
            {
                return;
            }

            if (WorldNavigation.curstate_mouse.find)
            {
                DirectionData.Direction dir = WorldNavigation.curstate_mouse.directionInformation.direction;

                if (dir == NavigationData.bannedDirection)
                {
                    NavigationData.bannedDirection = DirectionData.Direction.empty;
                }
            }

            var PathParam = new PathParam(V.player_entity.CurrentPosition_string, position, new WalkableParam(Walkable.GetCommonForbideenPos(), false));

            V.player_entity.MoveTo(PathParam);

            Event_playerMove.Call(position);
        }
    }

    void MakeAction_Positinionning(string position)
    {
        TileBase player_spawnable_tile = Resources.Load<TileBase>("Image/Tile/TileBase/positionning_player");

        if (position != V.player_entity.CurrentPosition_string &&
            Main_Map.ground_positionning.HasTile((Vector3Int)CursorInfo.Instance.positionVector2Int) &&
            Main_Map.ground_positionning.GetTile((Vector3Int)CursorInfo.Instance.positionVector2Int) == player_spawnable_tile)
        {
            SoundManager.PlayRandomSound_BetweenTwo(SoundManager.list.environment_positioning_1, SoundManager.list.environment_positioning_2);

            F.TeleportEntity(position, V.player_entity, false);
        }
    }

}
