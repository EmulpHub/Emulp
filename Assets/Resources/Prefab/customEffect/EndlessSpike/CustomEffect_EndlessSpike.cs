using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CustomEffect_EndlessSpike : CustomEffect
{
    public static CustomEffect_EndlessSpike CurrentEndlessSpike;

    public static void create()
    {
        if (CurrentEndlessSpike != null)
            return;

        GameObject G = CustomEffect.GetCustomEffect("EndlessSpike/customEffect_EndlessSpike");

        G = Instantiate(G);

        CurrentEndlessSpike = G.GetComponent<CustomEffect_EndlessSpike>();
    }

    public Sprite normal, broken;

    public List<GameObject> allSpike = new List<GameObject>();

    public bool EnableCondition()
    {
        return V.game_state == V.State.fight && Time.time >= DisableTimer;
    }

    bool enable = true;

    public float DisableTimer;

    public static void AddDisableTime(float time)
    {
        if (CurrentEndlessSpike == null) return;

        float d = Time.time + time;

        if (CurrentEndlessSpike.DisableTimer < d)
        {
            CurrentEndlessSpike.DisableTimer = d;
        }
    }

    public void anim_OneTurnLasting()
    {
        if (isDiying) return;

        AnimationIsOn = true;

        MakeActionToSpike((GameObject s) =>
        {
            s.GetComponent<SpriteRenderer>().sprite = broken;

            s.transform.DOShakeScale(0.1f, 0.1f, 1);
        });

        StartCoroutine(SetAnimationIsOnToFalse(0.11f));

    }

    public void anim_Reviving()
    {
        if (isDiying) return;

        AnimationIsOn = true;

        MakeActionToSpike((GameObject s) =>
        {
            s.GetComponent<SpriteRenderer>().sprite = normal;

            s.transform.DOShakePosition(0.1f, 1, 1);
        });

        StartCoroutine(SetAnimationIsOnToFalse(0.11f));
    }

    bool isDiying;

    public void anim_kill()
    {
        isDiying = true;
        AnimationIsOn = true;

        MakeActionToSpike((GameObject s) =>
        {
            s.GetComponent<SpriteRenderer>().sprite = broken;

            s.transform.DOShakeScale(0.1f, 0.1f, 1);

            s.transform.DOScale(0, 0.2f);
        });

        CurrentEndlessSpike = null;

        StartCoroutine(killThis(0.5f));
    }

    public IEnumerator SetAnimationIsOnToFalse(float delay)
    {
        yield return new WaitForSeconds(delay);

        AnimationIsOn = false;
    }

    public IEnumerator killThis(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(this.gameObject);
    }

    public void Update()
    {
        enable = EnableCondition();

        if (enable)
            transform.position = V.CalcEntityDistanceToBody(V.player_entity);
        else
            transform.position = new Vector3(100, 100, 0);

        UpdateSpike();

        ManageSpike();
    }

    public float posMultiplicator;

    public float degree_constant_PerSec;

    [HideInInspector]
    public float degree_constant;

    public void UpdateSpike()
    {
        if (AnimationIsOn)
            return;

        float degreeToAdd = 0;

        if (allSpike.Count > 0)
            degreeToAdd = 360 / allSpike.Count;

        float degree = degree_constant;

        foreach (GameObject g in allSpike)
        {
            g.transform.localPosition = F.ConvertDegreeIntoVector(degree) * posMultiplicator;

            g.transform.rotation = Quaternion.Euler(new Vector3(0, 0, degree));

            degree += degreeToAdd;
        }

        if (enable)
            degree_constant -= degree_constant_PerSec * Time.deltaTime;
    }

    public void ManageSpike()
    {
        if (isDiying) return;

        if (nbSpike < allSpike.Count)
        {
            RemoveSpike();
        }
        else if (nbSpike > allSpike.Count)
        {
            InstantiateSpike();
        }
    }


    public GameObject spike_up;

    public void InstantiateSpike()
    {
        GameObject g = Instantiate(spike_up, transform);

        allSpike.Add(g);
    }

    public void RemoveSpike()
    {
        GameObject g = allSpike[0];

        allSpike.RemoveAt(0);

        Destroy(g);
    }

    public int nbSpike
    {
        get => V.player_info.spike;
    }

    public bool AnimationIsOn;

    public static void AttackAnimation(Entity target)
    {
        CurrentEndlessSpike.StopAllCoroutines();

        CurrentEndlessSpike.StartCoroutine(CurrentEndlessSpike.DoAnimation(target));
    }

    public float additionalDegreePointToTarget;

    public IEnumerator DoAnimation(Entity target)
    {
        AnimationIsOn = true;

        Vector3 pos = V.CalcEntityDistanceToBody_Vector3(target.transform.position);

        float time = 0;

        while (time < 0.1f)
        {
            MakeActionToSpike((GameObject s) =>
            {
                float degree = 30;

                time += Time.deltaTime;

                s.transform.up = Vector3.Lerp(s.transform.up, pos - s.transform.position, Time.deltaTime * degree);
            });

            yield return new WaitForEndOfFrame();
        }

        int i = 0;

        foreach (GameObject g in allSpike)
        {
            StartCoroutine(DOAnimationSpikeIndividual(g, pos));

            if (i + 1 < allSpike.Count)
                yield return new WaitForSeconds(0.1f);

            i++;
        }

        yield return new WaitForSeconds(0.2f);

        AnimationIsOn = false;
    }

    public IEnumerator DOAnimationSpikeIndividual(GameObject g, Vector3 pos)
    {

        float ancientYSCale = g.transform.localScale.y;

        Vector3 ancientPos = g.transform.position;

        g.transform.DOMove(pos, 0.1f).SetEase(Ease.InFlash);

        g.transform.DOScaleY(ancientYSCale * 3, 0.1f);

        yield return new WaitForSeconds(0.1f);


        g.transform.DOScaleY(ancientYSCale, 0.3f);

        g.transform.DOMove(ancientPos, 0.1f).SetEase(Ease.InFlash);


    }

    public delegate void spikeAction(GameObject s);

    public void MakeActionToSpike(spikeAction a)
    {
        foreach (GameObject g in allSpike)
        {
            a(g);
        }
    }
}

