using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class Entity : MonoBehaviour
{
    [HideInInspector]
    public Vector3 renderer_nonMovable_baseScale;

    public virtual void ResetAllAnimation()
    {
        transform.DOKill();
        Renderer_movable.transform.DOKill();
        Renderer_movable.transform.localPosition = Vector3.zero;
        Renderer_movable.transform.localScale = new Vector3(1, 1, 1);
        Renderer_nonMovable.transform.DOKill();
        Renderer_nonMovable.transform.localPosition = Vector3.zero;
        Renderer_nonMovable.transform.localScale = renderer_nonMovable_baseScale;
        Renderer_nonMovable.DOFade(1, 0);
        Renderer_nonMovable.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public float attackAnimation_Strenght, attackAnimation_Speed;

    public float Animation_DealDamage(Vector3 pos)
    {
        foreach ((float Strenght, float Speed, Vector3 targetPosition) p in AnimationDealDamageValue)
        {
            StopCoroutine(Animation_DealDamage_Coroutine(p.Strenght, p.Speed, p.targetPosition));
        }

        AnimationDealDamageValue.Clear();

        StartCoroutine(Animation_DealDamage_Coroutine(attackAnimation_Strenght, attackAnimation_Speed, pos));

        return attackAnimation_Speed;
    }

    List<(float Strenght, float Speed, Vector3 targetPosition)> AnimationDealDamageValue = new List<(float Strenght, float Speed, Vector3 targetPosition)>();

    public float Animation_DealDamage(string targetPosition)
    {
        return Animation_DealDamage(F.ConvertToWorldVector2(targetPosition));
    }

    IEnumerator Animation_DealDamage_Coroutine(float Strenght, float Speed, Vector3 targetPosition)
    {
        if (IsMonster())
            ((Monster)this).ChangeEye_Attack();

        Renderer_movable.transform.DOKill();

        Renderer_movable.transform.localPosition = Vector2.zero;

        AnimationDealDamageValue.Add((Strenght, Speed, targetPosition));

        Vector3 Direction = targetPosition - transform.position;
        Direction.Normalize();

        Renderer_movable.transform.DOLocalMove(Direction * Strenght, Speed);

        yield return new WaitForSeconds(Speed * 1.1f);

        Renderer_movable.transform.DOLocalMove(Vector3.zero, Speed);
    }

    public void Animation_StartTeleport(float speed)
    {
        Renderer_movable.transform.DOLocalMove(new Vector3(0, 0.5f, 0) * 1, speed);

        ModifyRendererFade(0, speed);
    }

    public void Animation_EndTeleport(float speed)
    {
        Renderer_movable.transform.DOLocalMove(Vector3.zero, speed);

        ModifyRendererFade(1, speed);
    }

    public virtual void Graphique_SetRotationAndSprite(bool running) { }

    public virtual void animation_Boost(float multiplicator = 1)
    {
        ResetAllAnimation();

        Renderer_movable.transform.DOKill();
        Renderer_movable.transform.DOScale(1, 0);
        Renderer_movable.transform.DOPunchScale(new Vector3(healAnimationShakeScale_Strength * multiplicator, healAnimationShakeScale_Strength * multiplicator, 0), 0.5f, 1);
    }
}
