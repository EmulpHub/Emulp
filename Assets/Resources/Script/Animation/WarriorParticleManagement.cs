using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using static UnityEngine.ParticleSystem;

public class WarriorParticleManagement : MonoBehaviour
{
    #region Create

    public static void Add(Vector3 pos, int nb)
    {
        if (nb <= 0)
            return;

        if (instance == null)
        {
            instance = Instantiate(Resources.Load<GameObject>("Prefab/WarriorParticleManagement")).GetComponent<WarriorParticleManagement>();

            instance.FollowPlayer();
        }

        instance.AddParticle(pos,nb);
    }

    #endregion

    public void Start()
    {
        Scene_Main.eventEndOfCombat.Add(EndOfCombatEvent);
    }

    private void EndOfCombatEvent ()
    {
        foreach(GameObject particle in listParticle)
        {
            Destroy(particle);
        }

        listParticle.Clear();
    }

    public static WarriorParticleManagement instance;

    private List<GameObject> listParticle = new List<GameObject>();

    public float ScaleEndless,ScaleStart;
    public GameObject prefab;

    public void Update()
    {
        Endless();

        FollowPlayer();
    }

    public void FollowPlayer ()
    {
        transform.position = V.CalcEntityDistanceToBody(V.player_entity);    
    }

    #region AddParticle 

    public float startPosDistance;

    public void AddParticle(Vector3 startPos, int nb)
    {
        int degreeToAdd = 360 / nb;

        for (int i = 0; i < nb; i++)
        {
            float degreeInRadian = Mathf.Deg2Rad * i * degreeToAdd;

            Vector3 direction = new Vector2(Mathf.Cos(degreeInRadian), Mathf.Sin(degreeInRadian));
            if (nb == 1)
                direction = Vector3.zero;

            StartCoroutine(Create_one(startPos + direction * startPosDistance));
        }
    }

    #endregion

    #region creating

    public float ApparitionTime;

    public IEnumerator Create_one(Vector3 startPos)
    {
        GameObject particle = Instantiate(prefab);

        particle.transform.position = startPos;

        particle.transform.localScale = new Vector3(ScaleStart,ScaleStart,1);

        V.player_entity.animation_Boost(0.4f);

        yield return new WaitForSeconds(ApparitionTime);

        Endless_Add(particle);
    }

    #endregion

    #region endlessParticle 

    public float endless_rotationSpeed;
    public float endless_distance;

    public void Endless ()
    {
        transform.rotation = Quaternion.Euler(0,0,transform.rotation.eulerAngles.z + endless_rotationSpeed * Time.deltaTime);
    }

    public void Endless_Add (GameObject Particle)
    {
        listParticle.Add(Particle);

        Particle.transform.parent = this.transform;

        Endless_UpdateLocalPosition();
    }

    public float AccumulationPerLine,AccumulationLevel;

    public void Endless_UpdateLocalPosition()
    {
        float div = Mathf.Clamp(listParticle.Count,1,AccumulationPerLine);

        float degreeToAdd = 360 / div;

        int i = 0;

        foreach (var particle in listParticle) {

            int step = Mathf.FloorToInt(i % AccumulationPerLine);
            int level = Mathf.FloorToInt(i / AccumulationPerLine);

            float degreeInRadian = Mathf.Deg2Rad * (((float)step + (float)level * 0.5f) * degreeToAdd);

            Vector2 direction = new Vector2(Mathf.Cos(degreeInRadian), Mathf.Sin(degreeInRadian));

            particle.transform.DOKill();

            var script = particle.GetComponent<AccumulationParticle>();

            script.direction = direction;
            script.distanceToPlayer = endless_distance * (level * AccumulationLevel + 1);
            script.normalScale = ScaleEndless;

            script.SetLocalPosition(true);
            script.SetLocalScale(true);
            
            i++;
        }
    }


    #endregion

    #region animation

    #region fastRotate

    public float FastRotateSpeed, FastRotateStrenght;
    
    private void Endless_FastRotate()
    {
        transform.DORotate(new Vector3(0, 0, transform.rotation.eulerAngles.z + FastRotateStrenght), FastRotateSpeed);
    }

    public static void FastRotateEndless()
    {
        if (instance == null) return;

        instance.Endless_FastRotate();
    }

    #endregion

    #region PunchY 

    public float PunchPositionStr,punchScaleStr,punchScaleSpeed;

    private void Endless_Punch_All()
    {
        for (int i = 0; i < listParticle.Count; i++) {
            Endless_Punch(i);
        }
    }

    private void Endless_Punch (int id)
    {
        if (id >= listParticle.Count) return;
        
        var particle = listParticle[id];

        particle.GetComponent<AccumulationParticle>().DoPunch(PunchPositionStr, punchScaleStr,punchScaleSpeed);

    }

    public static void PunchID(int id)
    {
        if (instance == null) return;

        instance.Endless_Punch(id);
    }

    public static void PunchAll()
    {
        if(instance == null) return;

        instance.Endless_Punch_All();
    }

    #endregion

    #endregion
}
