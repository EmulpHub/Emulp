using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AnimEndless_Render : AnimEndless
{
    public static AnimEndless_Render Create(Entity target, Vector3 offset, Sprite render, AnimationExit animExit = AnimationExit.none, float size = 1, float speed = 1)
    {
        GameObject g = Instantiate(AnimEndlessStatic.render);

        AnimEndless_Render script = g.GetComponent<AnimEndless_Render>();

        script.target = target;
        script.offset = offset;
        script.Render.sprite = render;
        script.Size = size;
        script.Speed = speed;
        script.animationExit = animExit;
        script.CurrentId = AnimEndlessStatic.ID.Render;

        script.GoToTarget();
        script.UpdateScale();
        script.AddToStaticList();

        return script;
    }

    public Image Render;
    public enum AnimationExit { none,bloodShield,normal,silent}
    private AnimationExit animationExit;
    public enum Anim { impact } 
    public enum IdleAnim { TopToBot, size };
    public Animator animator;

    public override int AddToStaticList()
    {
        int nb = AnimEndlessStatic.getSameEndlesssAnimationCount(this, new List<AnimEndlessStatic.ID> {AnimEndlessStatic.ID.Render,AnimEndlessStatic.ID.IdleAnimation });

        base.AddToStaticList();

        float y = Random.Range(0, 100 + 1); ;

        float x = 100 - y;

        float circleRadius = nb == 0 ?
            0 :
            (0.6f + 0.2f * (nb / 3));

        if (Random.Range(0, 1 + 1) == 1)
            x *= -1;

        if (Random.Range(0, 1 + 1) == 1)
            y *= -1;

        offset += new Vector3(x / 100 * circleRadius, y / 100 * circleRadius, 0);

        Speed *= 1 + nb * 0.1f;

        Speed = Mathf.Clamp(Speed, 0.05f, 999);

        return nb;
    }

    public override void Erase()
    {
        switch (animationExit)
        {
            case AnimationExit.none:
                Erase_None();
                return;
            case AnimationExit.bloodShield:
                StartCoroutine(Erase_BloodShield());
                return;
            case AnimationExit.normal:
                StartCoroutine(Erase_Normal());
                return;
            case AnimationExit.silent:
                StartCoroutine(Erase_Silent());
                return;
        }

        throw new System.Exception("Bad erase type for render");
    }

    public void Erase_None()
    {
        Erase_WithoutAnimation();
    }

    public IEnumerator Erase_BloodShield()
    {
        transform.DOScale(0.4f, 0.5f);
        Render.DOFade(0, 0.5f);
        Spell.Reference.CreateParticle_Impact_Blood(CalcPosToTarget(), 1.5f, Spell.Particle_Amount._48);
        Spell.Reference.CreateParticle_Leaf(CalcPosToTarget(), 0.8f);
        Spell.Reference.CreateParticle_Leaf(CalcPosToTarget(), 0.8f);

        Erase_WithoutAnimation(1);
        yield return new WaitForSeconds(0);
    }

    public IEnumerator Erase_Normal()
    {
        transform.DOScale(0.4f, 0.5f);
        Render.DOFade(0, 0.5f);
        Spell.Reference.CreateParticle_Impact(CalcPosToTarget(), 1.5f, Spell.Particle_Amount._48, Spell.Particle_Color.yellow);
        Spell.Reference.CreateParticle_Leaf(CalcPosToTarget(), 0.8f);
        Spell.Reference.CreateParticle_Leaf(CalcPosToTarget(), 0.8f);

        Erase_WithoutAnimation(1);
        yield return new WaitForSeconds(0);
    }

    public IEnumerator Erase_Silent()
    {
        transform.DOScale(0.4f, 0.5f);
        Render.DOFade(0, 0.5f);
        Spell.Reference.CreateParticle_Leaf(CalcPosToTarget(), 0.8f);

        Erase_WithoutAnimation(1);
        yield return new WaitForSeconds(0);
    }


    public void DoAnimation(Anim anim)
    {
        switch (anim)
        {
            case Anim.impact:
                Anim_Impact();
                return;
        }
    }

    public void Anim_Impact()
    {
        Spell.Reference.CreateParticle_Leaf(target.transform.position, 1.2f);
        Spell.Reference.CreateParticle_Impact(Render.transform.position, 1.2f);

        Render.transform.DOPunchScale(new Vector3(-0.4f, 0.4f, 0), 0.3f, 1);
    }

    public void setIdleAnimation (IdleAnim anim)
    {
        animator.Play(anim.ToString());
    }
}
