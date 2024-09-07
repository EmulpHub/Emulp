using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.ParticleSystem;

public class AccumulationParticle : MonoBehaviour
{
    public float loopPose, timeBeforeStartingFirstLoop;

    public Animator animator;

    public string idleAnimationName;

    private float timer;

    public void Start()
    {
        timer = timeBeforeStartingFirstLoop;
    }

    public void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            timer = loopPose;

            animator.Play(idleAnimationName);
        }
    }

    [HideInInspector]
    public Vector3 direction;
    [HideInInspector]
    public float distanceToPlayer, normalScale;

    public void DoPunch(float punchStr, float scaleStr, float speed)
    {
        transform.DOKill();
        SetLocalScale(false);
        SetLocalPosition(false);

        Vector2 scale = new Vector2(normalScale, normalScale);

        transform.DOPunchScale(scale * scaleStr,speed,1);
        transform.DOPunchPosition(direction * punchStr, speed, 1);
    }

    public void SetLocalScale(bool animation)
    {
        Vector2 scale = new Vector2( normalScale,normalScale);

        if (animation)
            transform.DOScale(scale, 0.3f);
        else
            transform.localScale = scale;
    }

    public void SetLocalPosition(bool animation)
    {
        Vector2 pos = direction * distanceToPlayer;

        if (animation)
            transform.DOLocalMove(pos, 0.1f);
        else
            transform.localPosition = pos;
    }
}
