using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class Monster : Entity
{
    public override void ResetSpriteAndMovement()
    {
        base.ResetSpriteAndMovement();

        Graphique_SetRotationAndSprite(false);
    }

    public Animator graphique_idle;

    public override void Graphique_SetRotationAndSprite(bool running)
    {
        base.Graphique_SetRotationAndSprite(running);

        if (Info.dead)
            return;

        graphique_idle.enabled = running;

        if (running)
        {
            if (V.game_state == V.State.fight)
            {
                float ActualZrotation = F.ConvertEulerAngleIntoInspectorAngle(Renderer_movable.transform.rotation.eulerAngles.z);

                if (ActualZrotation >= (zRotation_max_combat - 10))
                {
                    GoRightWhenMoving = false;
                }
                else if (-ActualZrotation >= (zRotation_max_combat - 10))
                {
                    GoRightWhenMoving = true;
                }

                if (runningInfo.TowardRight)
                {
                    Renderer_movable.transform.DOKill();
                    Renderer_movable.transform.DORotate(new Vector3(0, 0, -zRotation_max_combat), animationCombat_moveSpeed);
                }
                else
                {
                    Renderer_movable.transform.DOKill();
                    Renderer_movable.transform.DORotate(new Vector3(0, 0, zRotation_max_combat), animationCombat_moveSpeed);
                }
            }
            else
            {
                float ActualZrotation = F.ConvertEulerAngleIntoInspectorAngle(Renderer_movable.transform.rotation.eulerAngles.z);

                if (ActualZrotation >= (zRotation_max - 10))
                {
                    GoRightWhenMoving = false;
                }
                else if (-ActualZrotation >= (zRotation_max - 10))
                {
                    GoRightWhenMoving = true;
                }

                if (GoRightWhenMoving)
                {
                    Renderer_movable.transform.DOKill();
                    Renderer_movable.transform.DORotate(new Vector3(0, 0, zRotation_max), animationOutCombat_moveSpeed);
                }
                else
                {
                    Renderer_movable.transform.DOKill();
                    Renderer_movable.transform.DORotate(new Vector3(0, 0, -zRotation_max), animationOutCombat_moveSpeed);
                }

            }
        }
        else
            Renderer_movable.transform.DORotate(new Vector3(0, 0, 0), 0.3f);
    }
}
