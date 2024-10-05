using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Map_PossibleToMove : MonoBehaviour
{
    public static bool CanFightClick()
    {
        return V.game_state == V.State.fight && !Scene_Main.isMouseOverAWindow && !Scene_Main.isMouseOverAWindow;
    }

    public static void FightClick(string position)
    {
        if (LaunchMovement_Condition())
            LaunchMovement_Effect();
        else if (LaunchSpell_Condition(position))
            LaunchSpell_Effect(position);
    }


    void MakeAction_Fight(string position)
    {
        if (!CanFightClick()) return;

        FightClick(position);

        SpellGestion.ResetSelectionnedSpell();
    }

    public static bool LaunchMovement_Condition()
    {

        return EntityOrder.Instance.IsTurnOf_Player() && V.game_state_action == V.State_action.movement && TileInfo.Instance.Exist(CursorInfo.Instance.position) &&
            PlayerMoveAutorization.Instance.Can() && CursorInfo.Instance.position != V.player_entity.CurrentPosition_string;
    }

    public static void LaunchMovement_Effect()
    {
        Tile tile = TileInfo.Instance.Get(CursorInfo.Instance.position);

        if (tile is CT_Movement tileMovement)
        {
            if (!CT_Movement.IsWalkableByTheplayer(tileMovement.path.Count))
                return;

            if (!ClickAutorization.Autorized(tile.gameObject))
                return;

            if (V.player_entity.runningInfo.running)
                return;

            tile.AnimationApparition();

            Action_movement.Add(new PathFindingName.PathParam(V.player_entity.CurrentPosition_string, CursorInfo.Instance.position), V.player_entity);
        }
    }

    public static bool LaunchSpell_Condition(string position)
    {
        return V.game_state_action == V.State_action.spell &&
            !MouseIsOnToolbar &&
            EntityOrder.Instance.IsTurnOf_Player() &&
            (TileInfo.Instance.Exist(position) ||
            (SpellGestion.Get_RangeMax(SpellGestion.selectionnedSpell_list) == 0 &&
            SpellGestion.Get_RangeMin(SpellGestion.selectionnedSpell_list) == 0));
    }

    public static void LaunchSpell_Effect(string position)
    {
        bool onPlayer = SpellGestion.Get_RangeMax(SpellGestion.selectionnedSpell_list) == 0 && SpellGestion.Get_RangeMin(SpellGestion.selectionnedSpell_list) == 0;

        if (TileInfo.Instance.Exist(position) || onPlayer)
        {
            if (onPlayer)
            {
                position = V.player_entity.CurrentPosition_string;
            }

            Tile tile = TileInfo.Instance.Get(position);

            var entity = EntityByPos.TryGet(position);

            if (!ClickAutorization.Autorized(tile.gameObject) && !(entity != null && ClickAutorization.Autorized(entity.gameObject)))
                return;

            if (LaunchSpell_Autorization(position))
            {
                int spellCastNumber = 1;

                spellCastNumber += V.player_entity.CollectStr(Effect.effectType.Spellx2_oneUse);

                while (spellCastNumber > 0)
                {
                    spellCastNumber--;


                    Action_spell_info_player info = new Action_spell_info_player(SpellGestion.selectionnedSpell,entity,position);

                    Action_spell.Add(info);

                    V.player_entity.RemoveEffect(Effect.effectType.Spellx2_oneUse);
                }
            }
            else
            {
                Scene_Main.SetGameAction_movement();
            }
        }
        else
        {
            Scene_Main.SetGameAction_movement();
        }
    }

    public static bool LaunchSpell_Autorization(string target)
    {
        SpellGestion.TargetMode tm = SpellGestion.Get_TargetMode(SpellGestion.selectionnedSpell_list);

        if (tm == SpellGestion.TargetMode.empty && F.IsTileWalkable(target))
        {
            return true;
        }
        else if (tm == SpellGestion.TargetMode.entity && EntityByPos.TryGet(target) != null)
        {
            return true;

        }
        else if (tm == SpellGestion.TargetMode.all)
        {
            return true;

        }

        return false;
    }
}
