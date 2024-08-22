using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Map_PossibleToMove : MonoBehaviour
{
    public static bool CanFightClick()
    {
        return V.game_state == V.State.fight && !Scene_Main.aWindowIsUsed && !Scene_Main.isMouseOverAWindow;
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
        return EntityOrder.IsTurnOf_Player() && V.game_state_action == V.State_action.movement && CTInfo.Instance.Exist(CursorInfo.Instance.position) &&
            PlayerMoveAutorization.Instance.Can() && CursorInfo.Instance.position != V.player_entity.CurrentPosition_string;
    }

    public static void LaunchMovement_Effect()
    {
        CT tile = CTInfo.Instance.Get(CursorInfo.Instance.position);

        if (tile is CT_Movement tileMovement)
        {
            if (!CT_Movement.IsWalkableByTheplayer(tileMovement.path.Count))
                return;

            if (!ClickAutorization.Autorized(tile.gameObject))
                return;

            if (V.player_entity.runningInfo.isRunning)
                return;

            tile.AnimationApparition();

            Action_movement.Add(new PathFindingName.PathParam(V.player_entity.CurrentPosition_string, CursorInfo.Instance.position), V.player_entity);
        }
    }

    public static bool LaunchSpell_Condition(string position)
    {
        return V.game_state_action == V.State_action.spell &&
            !MouseIsOnToolbar &&
            EntityOrder.IsTurnOf_Player() &&
            (CTInfo.Instance.Exist(position) ||
            (SpellGestion.Get_RangeMax(SpellGestion.selectionnedSpell_list) == 0 &&
            SpellGestion.Get_RangeMin(SpellGestion.selectionnedSpell_list) == 0));
    }

    public static void LaunchSpell_Effect(string position)
    {
        bool onPlayer = SpellGestion.Get_RangeMax(SpellGestion.selectionnedSpell_list) == 0 && SpellGestion.Get_RangeMin(SpellGestion.selectionnedSpell_list) == 0;

        if (CTInfo.Instance.Exist(position) || onPlayer)
        {
            if (onPlayer)
            {
                position = V.player_entity.CurrentPosition_string;
            }

            CT tile = CTInfo.Instance.Get(position);

            var entity = AliveEntity.GetEntityByPos(position);

            if (!ClickAutorization.Autorized(tile.gameObject) && !(entity != null && ClickAutorization.Autorized(entity.gameObject)))
                return;

            if (LaunchSpell_Autorization(position))
            {
                int spellCastNumber = 1;

                spellCastNumber += V.player_entity.CollectStr(Effect.effectType.Spellx2_oneUse);

                while (spellCastNumber > 0)
                {
                    spellCastNumber--;


                    Action_spell_info_player info = new Action_spell_info_player();

                    info.spell = SpellGestion.selectionnedSpell;
                    info.caster = V.player_entity;
                    info.listTarget = new List<Entity>() { entity };
                    info.targetedSquare = position;

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
        else if (tm == SpellGestion.TargetMode.entity && AliveEntity.GetEntityByPos(target) != null)
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
