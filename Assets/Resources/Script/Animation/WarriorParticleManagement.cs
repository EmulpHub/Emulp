using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WarriorParticleManagement : MonoBehaviour
{
    //Create
    public static void Create(Vector3 startPos, int nb, Vector3 endPos, bool power)
    {
        if (nb <= 0)
            return;

        GameObject g = Instantiate(Resources.Load<GameObject>("Prefab/WarriorParticleManagement"));

        g.transform.position = startPos;

        g.GetComponent<WarriorParticleManagement>().Init(nb, endPos, power);
    }

    public static void Create(Vector3 startPos, int nb, bool power)
    {
        Create(startPos, nb, V.CalcEntityDistanceToBody(V.player_entity), power);
    }

    public static void CreatePower(Vector3 startPos, int nb)
    {
        Create(startPos, nb, V.CalcEntityDistanceToBody(V.player_entity), true);
    }

    public static void CreateDefense(Vector3 startPos, int nb)
    {
        Create(startPos, nb, V.CalcEntityDistanceToBody(V.player_entity), false);
    }
    [HideInInspector]

    public int nb;
    [HideInInspector]

    public Vector3 endPos;

    [HideInInspector]
    public Color32 col;

    public Color32 col_power, col_defense;

    [HideInInspector]

    public bool power;

    //Manage
    public void Init(int nb, Vector3 endPos, bool power)
    {
        nbSoundPlayed = 0;
        this.nb = nb;
        this.endPos = endPos;
        this.power = power;

        if (power)
            col = col_power;
        else
            col = col_defense;

        TimeBetweenEachSound = Mathf.Clamp(0.01f - nb * 0.001f, 0.05f, 0.1f);

        InstantiateAllPart();
    }

    public GameObject part;

    [HideInInspector]
    public float lastAddingWaitTime;

    public void InstantiateAllPart()
    {
        if (Time.time - lastAddingWaitTime > 0)
        {
            float w = speedInSec_first + speedInSec_Jump * 0.3f;

            lastAddingWaitTime = Time.time + w;
            //Action.Add_wait_fixed(w);
        }

        float degreeToAdd = 360 / (nb + 1);

        for (int i = 0; i < nb; i++)
        {
            StartCoroutine(InstantiatePart(i * degreeToAdd + Random.Range(-10,10 + 1), (float)Random.Range(100, 300) / 1000));
        }

        Destroy(this.gameObject, 7);
    }

    public void Update()
    {
        ParticleSoundManagement_defense();
        ParticleSoundManagement();

        Management();
    }
    [HideInInspector]
    public List<GameObject> partLs = new List<GameObject>();

    [HideInInspector]
    public List<Vector3> partLs_LastPos = new List<Vector3>();

    public void Management()
    {
        int i = 0;

        foreach (GameObject g in partLs)
        {
            Vector3 currentPos = g.transform.position;

            Vector3 lastPos = partLs_LastPos[i];

            Vector3 diff = lastPos - currentPos;

            float speed = Mathf.Abs(diff.x + diff.y);

            float sizeBySpeed = Mathf.Clamp(speed * SizeBySpeedModifier, 1, 3);

            if (speed <= 0.01f)
            {
                i++;

                continue;
            }

            g.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan(diff.y / diff.x) * Mathf.Rad2Deg + additionalDegree);

            g.transform.localScale = new Vector3(baseScalePart, baseScalePart * sizeBySpeed, 1);

            partLs_LastPos[i] = g.transform.position;

            i++;
        }
    }

    public float speedInSec_first, speedInSec_Jump, PositionStartMultiplicator, jumpPower;

    public float baseScalePart, SizeBySpeedModifier, additionalDegree;

    public float endPos_variation, impact_speedInSec, Impact_SizeModifier;

    [HideInInspector]
    public int nbPartDestroyed;

    public IEnumerator InstantiatePart(float degree, float additionalStartSpeed)
    {
        GameObject p = Instantiate(part, this.gameObject.transform);

        p.GetComponent<SpriteRenderer>().color = col;

        float multiplier = (nb / 10) * 0.4f;

        Vector3 pos = F.ConvertDegreeIntoVector(degree) * PositionStartMultiplicator * (multiplier + 1) * Random.Range(0.7f,1.3f);

        float jumpPower = this.jumpPower ;

        float jump = pos.y * jumpPower;

        float firstSpeed = (speedInSec_first + additionalStartSpeed ) * (multiplier +1);

        p.transform.DOLocalMove(pos, firstSpeed);

        partLs.Add(p);

        partLs_LastPos.Add(this.gameObject.transform.position);

        yield return new WaitForSeconds(firstSpeed * 0.8f);

        float secondSpeed = speedInSec_Jump * (multiplier + 1);

        p.transform.DOKill();
        Vector3 finalPos = endPos + new Vector3(Random.Range(-endPos_variation, endPos_variation), Random.Range(-endPos_variation, endPos_variation), 0);

        p.transform.DOJump(finalPos, jump, 1, secondSpeed);

        p.GetComponent<SpriteRenderer>().DOFade(0, secondSpeed + 0.2f);

        yield return new WaitForSeconds(secondSpeed * 0.3f);

        if (V.game_state == V.State.fight)
        {
            nbPartDestroyed++;

            if (nbPartDestroyed % 2 == 0 || nbPartDestroyed == nb)
            {
                if (power)
                    nbSoundToPlay++;
                else
                    nbSoundToPlay_defense++;

                StartCoroutine(
                    Spell.Reference.Anim_PopUp(F.GetSpellSpriteAtPath_warrior("impact_anim"), finalPos, impact_speedInSec, Impact_SizeModifier)
                    );

                V.player_entity.animation_Boost(0.15f);
            }
        }

    }

    public static int nbSoundToPlay,nbSoundPlayed;

    public static float lastTimePlayed;

    public static float TimeBetweenEachSound;

    public static void ParticleSoundManagement()
    {
        if (Time.time - lastTimePlayed >= TimeBetweenEachSound && nbSoundToPlay > 0 && nbSoundPlayed <= 5)
        {
            //SoundManager.PlaySound(SoundManager.list.spell_warrior_particle_impact);
            lastTimePlayed = Time.time + TimeBetweenEachSound;
            nbSoundToPlay--;
            nbSoundPlayed++;
        }
    }

    public static int nbSoundToPlay_defense;

    public static float lastTimePlayed_defense;

    public static float TimeBetweenEachSound_defense;

    public static void ParticleSoundManagement_defense()
    {
        if (Time.time - lastTimePlayed_defense >= TimeBetweenEachSound_defense && nbSoundToPlay_defense > 0)
        {
            //SoundManager.PlaySound(SoundManager.list.spell_warrior_particle_impact_defense);
            lastTimePlayed_defense = Time.time + TimeBetweenEachSound_defense;
            nbSoundToPlay_defense--;
        }
    }
}
