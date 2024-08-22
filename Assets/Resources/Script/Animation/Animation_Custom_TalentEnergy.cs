using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Animation_Custom_TalentEnergy : MonoBehaviour
{
    public static GameObject Animation_CustomStatic;

    public static Image Animation_Custom_renderStatic;

    public static Sprite Flash, Thunder;

    public static void Initialize_GameObject()
    {
        Animation_CustomStatic = Resources.Load<GameObject>("Prefab/Animation/Custom_Animation_Energy");

        Animation_Custom_renderStatic = Animation_CustomStatic.transform.GetChild(0).GetComponent<Image>();

        Flash = Resources.Load<Sprite>("Image/Sort/warrior_talent_Energy_flash");

        Thunder = Resources.Load<Sprite>("Image/Sort/warrior_Talent_Energy_Thunder");
    }

    public static void InstantiateFlashAnimation(Vector3 startPosition, Vector3 casterPosition, float travelDistance)
    {
        GameObject g = Instantiate(Animation_CustomStatic);

        Animation_Custom_TalentEnergy anim = g.GetComponent<Animation_Custom_TalentEnergy>();

        anim.sp = Flash;

        Vector3 start = startPosition + new Vector3(0, travelDistance, 0);

        if (!F.IsPosSeenable(start))
        {
            start = startPosition - new Vector3(0, travelDistance, 0);
        }

        anim.startPosition = start;
        anim.casterPosition = casterPosition;

        anim.StartMovement();
    }

    public static void InstantiateThunderAnimation(string startPosition)
    {
        GameObject g = Instantiate(Animation_CustomStatic);

        Animation_Custom_TalentEnergy anim = g.GetComponent<Animation_Custom_TalentEnergy>();

        anim.sp = Thunder;
        anim.startPosition = (Vector3)F.ConvertToWorldVector2(startPosition) + new Vector3(0, 1.5f, 0);

        anim.StartMovement_Thunder();
    }

    public Image Animation_Custom_render;

    [HideInInspector]
    public Vector3 casterPosition, startPosition;

    public Sprite sp;

    public void StartMovement_Thunder()
    {
        StartCoroutine(StartMovement_ThunderCo());
    }

    public IEnumerator StartMovement_ThunderCo()
    {
        float Multiplicateur = 0.5f;

        Animation_Custom_render.sprite = sp;
        transform.DOScale(transform.localScale.x * 1.5f, 0.6f * Multiplicateur);

        int angle = Random.Range(-20, 20 + 1); //+30

        Vector3 dir = new Vector3(0, 1, 0);

        float strenght_start = 1.5f, strenght_end = 0.9f;

        transform.position = startPosition + dir * strenght_start;

        float angleY = 0;

        if (Random.Range(0, 1 + 1) == 1)
            angleY = 180;


        transform.rotation = Quaternion.Euler(0, angleY, /*angle -*/ 24);

        transform.DOMove(startPosition + dir * strenght_end, 0.9f);

        Animation_Custom_render.DOFade(0, 1.5f);

        Destroy(this.gameObject, 2);

        yield return null;
    }

    public void StartMovement()
    {
        StartCoroutine(StartMovementCo());
    }

    public IEnumerator StartMovementCo()
    {
        float Multiplicateur = 0.5f;

        Animation_Custom_render.sprite = sp;
        transform.position = startPosition;
        transform.DOScale(transform.localScale.x * 1.5f, 0.6f * Multiplicateur);

        yield return new WaitForSeconds(0.5f * Multiplicateur);

        transform.DOMove(casterPosition, 1.2f * Multiplicateur);
        transform.DORotate(new Vector3(0, 0, 50), 1.4f * Multiplicateur);

        Animation_Custom_render.DOFade(0, 1.2f * Multiplicateur);

        Destroy(this.gameObject, 2);
    }
}