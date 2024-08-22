using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;

public class AnimEndless_IdleAnimation : AnimEndless
{
    public static AnimEndless_IdleAnimation Create(Entity target, Vector3 offset, string idle = "",string start = "",string end = "", float size = 1, float speed = 1)
    {
        GameObject g = Instantiate(AnimEndlessStatic.IdleAnimation);

        AnimEndless_IdleAnimation script = g.GetComponent<AnimEndless_IdleAnimation>();

        script.target = target;
        script.offset = offset;
        script.Size = size;
        script.Speed = speed;
        script.animationIdle = idle;
        script.animationStart = start;
        script.animationEnd = end;
        script.CurrentId = AnimEndlessStatic.ID.IdleAnimation;

        script.PlayStart();
        script.PlayIdle();

        script.GoToTarget();
        script.UpdateScale();
        script.AddToStaticList();

        return script;
    }

    private SoundManager.list endlessListSound;

    private AudioSource endlessAudio;

    public void SetEndlessSound (SoundManager.list sound)
    {
        endlessListSound = sound;

        endlessAudio = SoundManager.PlayEndlessSound(sound);
    }

    public string animationEnd,animationStart,animationIdle;
    public Animator animator;

    public override void Erase()
    {
        PlayEnd();
        Erase_WithoutAnimation(2);
    }

    public void PlayStart()
    {
        if(animationStart != "") animator.Play(animationStart);
    }

    public void PlayIdle ()
    {
        animator.Play(animationIdle);
    }

    public void PlayEnd()
    {
        if (endlessListSound != SoundManager.list.none) SoundManager.StopSound(endlessListSound);
        if(animationEnd != "") animator.Play(animationEnd);
    }
}
