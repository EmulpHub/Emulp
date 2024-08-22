using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimEndless_Particle : AnimEndless
{
    public GameObject Particle;

    public ParticleSystem Particle_system;

    public static AnimEndless_Particle Create(Entity target, Vector3 offset, GameObject particle, AnimationExit exit = AnimationExit.none, float size = 1, float speed = 1)
    {
        GameObject g = Instantiate(AnimEndlessStatic.particle);

        AnimEndless_Particle script = g.GetComponent<AnimEndless_Particle>();

        script.target = target;
        script.offset = offset;
        script.Particle = particle;
        script.Size = size;
        script.Speed = speed;
        script.CurrentId = AnimEndlessStatic.ID.Particle;
        script.animationExit = exit;

        script.Particle_system = particle.GetComponent<ParticleSystem>();

        script.Particle.transform.SetParent(script.transform);
        script.Particle.transform.localPosition = Vector3.zero;

        script.GoToTarget();
        script.UpdateScale();
        script.AddToStaticList();

        return script;
    }

    public override int AddToStaticList()
    {
        int nb = base.AddToStaticList();

        return nb;
    }

    public enum AnimationExit { none, FadeOut_particle }
    private AnimationExit animationExit;

    public override void Erase()
    {
        switch (animationExit)
        {
            case AnimationExit.none:
                Erase_None();
                return;
            case AnimationExit.FadeOut_particle:
                Erase_FadeOut_Particle();
                return;
        }

        throw new System.Exception("Bad erase type for particle");
    }

    public void Erase_None()
    {
        Erase_WithoutAnimation();
    }

    public void Erase_FadeOut_Particle()
    {
        Particle_system.loop = false;

        Erase_WithoutAnimation(2);
    }
}
