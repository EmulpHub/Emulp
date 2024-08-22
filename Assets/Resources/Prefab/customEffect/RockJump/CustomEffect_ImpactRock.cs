using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CustomEffect_ImpactRock : CustomEffect
{

    public static void create(Vector3 pos, int nb, float size = 1)
    {
        GameObject G = CustomEffect.GetCustomEffect("RockJump/customEffect_impactRock");

        G = Instantiate(G);

        G.transform.position = pos;

        G.GetComponent<CustomEffect_ImpactRock>().Init(nb, size);
    }


    public GameObject ParticleRockImpact;

    public List<Sprite> impactRockSprite = new List<Sprite>();

    public float ShakePower, ShakeSpeedInSec;

    public float TimeBeforeStartFading_RockImpact;

    public float FadeSpeedInSec_RockImpact;

    public float Impact_lowering_distance, Impact_lowering_SpeedInSec;

    public static int impactRockSprite_index = 0;

    public IEnumerator InstantiateRockImpact()
    {
        GameObject g = Instantiate(ParticleRockImpact, transform);

        SpriteRenderer s = g.GetComponent<SpriteRenderer>();

        s.sprite = impactRockSprite[impactRockSprite_index];

        impactRockSprite_index++;

        if (impactRockSprite_index >= impactRockSprite.Count)
            impactRockSprite_index = 0;

        g.transform.DOShakeScale(ShakeSpeedInSec, new Vector3(0, ShakePower, 0), 10, 0);

        g.transform.DOLocalMoveY(-Impact_lowering_distance, Impact_lowering_SpeedInSec);

        g.transform.localScale = g.transform.localScale * size;

        yield return new WaitForSeconds(TimeBeforeStartFading_RockImpact);

        s.DOFade(0, FadeSpeedInSec_RockImpact);
    }

    public GameObject Particle;

    public List<Sprite> RocksSprite;

    public float Particle_JumpSpeedInSec, Particle_JumpPower;

    public int Particle_JumpNb;

    public float Particle_JumpTravelDistanceMin, Particle_JumpTravelDistanceMax;
    public float Particle_JumpTravelDistanceMin_Y, Particle_JumpTravelDistanceMax_Y;

    public float TimeBeforeStartFading;

    public float FadeSpeedInSec;

    public IEnumerator InstantiateRock(bool right, Sprite sp)
    {
        GameObject g = Instantiate(Particle, transform);

        SpriteRenderer s = g.GetComponent<SpriteRenderer>();

        s.sprite = sp;

        Vector3 endPos = new Vector3(Random.Range(Particle_JumpTravelDistanceMin, Particle_JumpTravelDistanceMax + 1), 0, 0);

        if (!right)
            endPos = -endPos;

        float randomSpeed = Random.Range(0.9f, 1.6f);

        g.transform.localScale = g.transform.localScale * size;

        endPos = new Vector3(endPos.x, Random.Range(Particle_JumpTravelDistanceMin_Y, Particle_JumpTravelDistanceMax_Y + 1), 0);

        g.transform.DOLocalJump(endPos, Particle_JumpPower, Particle_JumpNb + Random.Range(-1, 1 + 1), Particle_JumpSpeedInSec * randomSpeed);

        g.transform.DORotate(new Vector3(0, 0, Random.Range(-360, 360)), Particle_JumpSpeedInSec + 0.5f);

        g.transform.DOScale(Random.Range(g.transform.localScale.x * 0.5f, g.transform.localScale.x * 1.4f), Particle_JumpSpeedInSec + 0.5f);

        yield return new WaitForSeconds(TimeBeforeStartFading);

        s.DOFade(0, FadeSpeedInSec);
    }

    float size;

    public void Init(int nb, float size)
    {
        this.size = size;

        StartCoroutine(InstantiateRockImpact());

        bool right = true;

        int o = 0;

        for (int i = 0; i < nb; i++)
        {
            StartCoroutine(InstantiateRock(right, RocksSprite[o]));

            right = !right;

            o++;
            if (o >= RocksSprite.Count)
            {
                o = 0;
            }
        }

        Destroy(this.gameObject, 5);
    }
}
