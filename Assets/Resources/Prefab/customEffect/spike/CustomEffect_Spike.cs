using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CustomEffect_Spike : MonoBehaviour
{
    public static void create(Vector3 pos)
    {
        GameObject G = CustomEffect.GetCustomEffect("spike/customEffect_Spike");

        G = Instantiate(G);

        G.transform.position = pos;

        G.GetComponent<CustomEffect_Spike>().Init();
    }

    public void Init()
    {
        StartCoroutine(instantiateAllSpike());

        StartCoroutine(InstantiateShield());

        StartCoroutine(removeAllSpike());
    }

    public GameObject Spike;

    public float Spike_degree, Spike_degree_start, Spike_PositionModifier, timeBetweenNewSpike;

    public int Spike_nb;

    public IEnumerator instantiateAllSpike()
    {
        bool right = true;

        float degree = Spike_degree_start;

        for (int i = 0; i < Spike_nb; i++)
        {
            if (right)
                instantiateSpike(degree);
            else
                instantiateSpike(-degree);

            right = !right;

            if (right)
            {
                degree += Spike_degree;
                yield return new WaitForSeconds(timeBetweenNewSpike);
            }
        }
    }

    public float TimeBeforeStartFadingSpike, FadingSpeedInSec;

    public IEnumerator removeAllSpike()
    {
        yield return new WaitForSeconds(TimeBeforeStartFadingSpike);

        foreach (SpriteRenderer s in renderLs)
        {
            s.DOFade(0, FadingSpeedInSec);
        }

        yield return new WaitForSeconds(FadingSpeedInSec + 0.2f);

        Destroy(this.gameObject);
    }

    public float Spike_ApparitionSpeed;

    public List<SpriteRenderer> renderLs;

    public void instantiateSpike(float degree)
    {
        GameObject g = Instantiate(Spike, transform);

        float baseScale = g.transform.localScale.x;

        g.transform.localScale = Vector3.zero;

        g.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90 + degree));

        Vector3 endPos = F.ConvertDegreeIntoVector(degree) * Spike_PositionModifier;

        g.transform.DOLocalMove(endPos, Spike_ApparitionSpeed);

        g.transform.DOScale(baseScale, Spike_ApparitionSpeed);

        renderLs.Add(g.GetComponent<SpriteRenderer>());
    }

    public float TimeBeforeShield, ShieldApparitionSpeedInSec;

    public GameObject Shield;

    public IEnumerator InstantiateShield()
    {
        yield return new WaitForSeconds(TimeBeforeShield);

        GameObject g = Instantiate(Shield, transform);

        float baseScaleX = g.transform.localScale.x;

        g.transform.localScale = Vector3.zero;

        g.transform.DOScale(baseScaleX, ShieldApparitionSpeedInSec);

        renderLs.Add(g.GetComponent<SpriteRenderer>());
    }
}
