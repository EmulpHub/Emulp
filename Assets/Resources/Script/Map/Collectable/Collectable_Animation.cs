using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract partial class Collectable : MonoBehaviour
{
    [HideInInspector]
    public Vector3 startScale;

    public Vector3 AnimApparition_StartingY;

    public float AnimApparition_Speed, AnimApparition_WaitTime;

    public virtual void Anim_Apparition()
    {
        img.color = new Color32(255, 255, 255, 0);

        StartCoroutine(Anim_ApparitionCo());
    }

    public IEnumerator Anim_ApparitionCo()
    {
        startScale = img.rectTransform.anchoredPosition;

        timer = timer_periodic + AnimApparition_Speed * 2 + 0.3f;

        yield return new WaitForSeconds(AnimApparition_WaitTime);

        img.rectTransform.anchoredPosition = startScale + AnimApparition_StartingY;

        img.rectTransform.DOAnchorPos(startScale, AnimApparition_Speed);

        img.DOFade(1, 0.2f);

        yield return new WaitForSeconds(AnimApparition_Speed + 0.3f);

        Anim_littleShake();
    }


    public float AnimShake_str, AnimShake_speed;

    public void Anim_littleShake()
    {
        ParticleLeaf(1, 0.9f);

        img.rectTransform.DOPunchRotation(new Vector3(0, 0, AnimShake_str), AnimShake_speed, 1);
    }

    public float timer_periodic;

    [HideInInspector]
    public float timer;

    public virtual void AnimationManagement()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            timer = timer_periodic;

            Anim_littleShake();
        }
    }

    public void ParticleLeaf(int nb = 1, float scale = 1.2f)
    {
        while (nb > 0)
        {
            Spell.Reference.CreateParticle_Leaf(transform.position, scale);

            nb--;
        }
    }
}
