using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using PathFindingName;
using System.Linq;

public partial class Player : Entity
{
    [HideInInspector]
    public int indexColor;

    public override void OnMouseIsOver()
    {
        base.OnMouseIsOver();

        if (V.game_state != V.State.fight && !Scene_Main.isMouseOverAWindow)
        {
            string start = "*" + descColor.AllPossibleColor[indexColor];

            if (V.IsFr())
            {
                Main_UI.Display_Title(start + "Guerrier *endniv. " + Info.level, Renderer_movable.transform.position, title_currentDistance);
            }
            else
            {
                Main_UI.Display_Title(start + "Warrior *endlvl. " + Info.level, Renderer_movable.transform.position, title_currentDistance);
            }
        }
    }

    public override void OnMouseIsExit()
    {
        base.OnMouseIsExit();

        Main_UI.Display_Title_Erase();

        indexColor++;
        if (indexColor >= descColor.AllPossibleColor.Count)
        {
            indexColor = 0;
        }
    }

    public PlayerInfo InfoPlayer { get { return ((PlayerInfo)Info); } }

    public override void OnAwake()
    {
        base.OnAwake();

        Info = new PlayerInfo();

        Info.InitInfo(this);
    }

    public override void OnStart()
    {
        base.OnStart();

        if (V.IsFr())
            Info.EntityName = "Joueur";
        else
            Info.EntityName = "Player";

        InitSpriteInfo();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        bool enableCollider = (V.game_state != V.State.fight || (SpellGestion.selectionnedSpell_list != SpellGestion.List.empty && SpellGestion.Get_RangeMin(SpellGestion.selectionnedSpell_list) == 0 && EntityOrder.Instance.IsTurnOf_Player()));

        Collider_SetActive(enableCollider);

        runningSoundManagement();
    }

    public override void StopRun()
    {
        base.StopRun();

        V.map_possibleToMove.DestroyMarker();
    }

    public override void ChangeRun()
    {
        base.ChangeRun();

        V.map_possibleToMove.DestroyMarker();
    }

    public override void EndOfRun(RunningInfo.Mode mode = RunningInfo.Mode.run)
    {
        base.EndOfRun(mode);

        SoundManager.PlaySound(SoundManager.list.player_movement_stopWalk);
    }

    public static int ShorterPathPowerInPassive = 2;

    protected override void Treat_PathParam(PathParam pathParam)
    {
        base.Treat_PathParam(pathParam);

        if (V.IsPassive())
            pathParam.SetDiagonalSearch();
    }

    public override bool MoveTo(PathParam path)
    {
        if (!base.MoveTo(path))
            return false;

        SoundManager.PlaySound(SoundManager.list.player_movement_click_grass);
        V.map_possibleToMove.movingAnimation(F.ConvertToWorldVector2(runningInfo.pathResult.endOfPath));

        return true;
    }

    private const float timerReset = 0.3f;
    private const float timerReset_walk = 0.35f;
    private float timer;

    private void runningSoundManagement()
    {
        if (runningInfo.running)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (runningInfo.mode == RunningInfo.Mode.run)
                    timer = timerReset;
                else timer = timerReset_walk;

                SoundManager.PlaySound(SoundManager.list.player_movement_move);
            }
        }
        else
            timer = 0;
    }

    public override float ChooseMoveSpeed(float baseValue, string pos1, string pos2)
    {
        var pos1Vector2 = F.ConvertToWorldVector2(pos1);
        var pos2Vector2 = F.ConvertToWorldVector2(pos2);

        float distance = F.DistanceBetweenTwoPos_float(pos1Vector2, pos2Vector2) / V.DistanceBetweenSquare;

        return baseValue * distance;
    }
}

