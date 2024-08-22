using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract partial class Entity : MonoBehaviour
{
    public void Animation_Apparition()
    {
        StartCoroutine(Ienum_Apparition());
    }

    public static float Animation_Apparition_Speed = 1.3f;

    private IEnumerator Ienum_Apparition()
    {
        ModifyRendererFade(0, 0);

        yield return new WaitForSeconds(0.2f);

        Renderer_movable.transform.localPosition = new Vector3(0, 1, 0);

        Renderer_movable.transform.DOLocalMove(new Vector3(0, 0, 0), 1f);

        ModifyRendererFade(1, 0.4f);

        yield return new WaitForSeconds(1);

        ParticleLeaf(2);
    }

    public virtual void ModifyRendererFade(float fadeValue, float time)
    {
        Renderer_nonMovable.DOFade(fadeValue, time);
    }

    public void ParticleLeaf(int nb = 1, float scale = 1.2f)
    {
        ParticleLeaf(nb, scale, transform.position);
    }


    public void ParticleLeaf(int nb, float scale, Vector3 pos)
    {
        while (nb > 0)
        {
            Spell.Reference.CreateParticle_Leaf(pos, scale);

            nb--;
        }
    }

    public void ParticleImpact(int nb = 1, float scale = 1.2f, Combat_spell_application.Particle_Color color = Spell.Particle_Color.yellow, Combat_spell_application.Particle_Amount amount = Spell.Particle_Amount._12)
    {
        while (nb > 0)
        {
            Spell.Reference.CreateParticle_Impact((Vector2)transform.position + V.Entity_DistanceToBody, scale, amount, color);

            nb--;
        }
    }
}
