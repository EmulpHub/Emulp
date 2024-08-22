using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Animation_skillAssignation : MonoBehaviour
{
    [HideInInspector]
    public bool ForTalent;
    public GameObject parentTransform;

    public static void Instantiate(Vector2 pos_start, Vector2 pos_final, SpellGestion.List sp, int jump, float multiplierSpeed, Transform parent, bool ForTalent)
    {
        GameObject g = null;

        if (parent == null)
            g = Instantiate(Resources.Load<GameObject>("Prefab/Animation_skillAssignation"));
        else
        {
            g = Instantiate(Resources.Load<GameObject>("Prefab/Animation_skillAssignation"), parent);

            float finalScale = F.CalculateRealScale(g.transform);

            g.transform.localScale = new Vector3(finalScale, finalScale, 1);
        }

        Animation_skillAssignation s = g.GetComponent<Animation_skillAssignation>();

        if (parent == null)
            s.parentTransform = null;
        else
            s.parentTransform = parent.gameObject;

        s.pos_start = pos_start;

        s.pos_final = pos_final;

        float speedParSec = 5;

        float distance = F.DistanceBetweenTwoPos_float(pos_start, pos_final);

        float timeInSec = distance / speedParSec * multiplierSpeed;

        s.speed = Mathf.Clamp(timeInSec, 1, 5);

        s.spell = sp;

        s.jumpPower = jump;

        s.Set();

        s.Move();

        s.ForTalent = ForTalent;
    }

    public static void Instantiate(Vector2 pos_start, Vector2 pos_final, SpellGestion.List sp, int jump, float multiplierSpeed, bool ForTalent)
    {
        Instantiate(pos_start, pos_final, sp, jump, multiplierSpeed, null, ForTalent);
    }

    public static void Instantiate(Vector2 pos_start, Vector2 pos_final, SpellGestion.List sp, int jump, float multiplierSpeed)
    {
        Instantiate(pos_start, pos_final, sp, jump, multiplierSpeed, null, false);
    }

    public static void Instantiate(Vector2 pos_start, Vector2 pos_final, SpellGestion.List sp, int jump)
    {
        Instantiate(pos_start, pos_final, sp, jump, 1, null, false);
    }

    public static void Instantiate(Vector2 pos_start, Vector2 pos_final, SpellGestion.List sp)
    {
        Instantiate(pos_start, pos_final, sp, 0, 1, null, false);
    }

    GameObject trail;

    public Image render;

    public void Set()
    {
        scale_start = transform.localScale;

        transform.localScale = Vector2.zero;

        render.sprite = SpellGestion.Get_sprite(spell);

        trail = Resources.Load<GameObject>("Prefab/Animation_skillAssignation_trail");

        trailScaleSave = trail.transform.localScale.x;
    }

    public SpellGestion.List spell;

    [HideInInspector]
    public Vector3 pos_start, pos_final, scale_start;

    [HideInInspector]
    public float speed;

    [HideInInspector]
    public int jumpPower;

    public void Move()
    {
        StartCoroutine(MoveCoroutine());
        StartCoroutine(Trail_generator());
    }

    public IEnumerator MoveCoroutine()
    {
        transform.DOScale(scale_start, 0.2f);

        transform.position = pos_start;

        transform.DOLocalJump(pos_final, jumpPower, 1, speed);

        yield return new WaitForSeconds(speed);

        EraseThis();
    }

    public void EraseThis()
    {
        transform.DOScale(0, 0.2f);

        Destroy(this.gameObject, 0.5f);
    }

    public Vector2 lastTrailPosition;

    public float trailScaleSave;

    public IEnumerator Trail_generator()
    {
        lastTrailPosition = pos_start;

        while (enabled)
        {
            if (F.DistanceBetweenTwoPos_float(lastTrailPosition, transform.position) > 1)
            {
                Vector3 diff = (Vector3)lastTrailPosition - transform.position;

                trail.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan(diff.y / diff.x) * Mathf.Rad2Deg);

                lastTrailPosition = transform.position;

                trail.transform.localPosition = transform.localPosition;

                GameObject G = null;
                if (parentTransform == null)
                    G = Instantiate(trail);
                else
                {
                    G = Instantiate(trail, parentTransform.transform);
                }

                G.transform.localScale = Vector2.zero;

                float finalScale = F.CalculateRealScale(G.transform, trailScaleSave);

                G.transform.DOScale(finalScale, 0.1f);

                G.GetComponent<SpriteRenderer>().DOFade(0, 2.5f);

                Destroy(G, 2);
            }

            if (ForTalent && !WindowInfo.Instance.GetWindow(WindowInfo.type.skill).find)
                EraseThis();

            yield return new WaitForEndOfFrame();
        }
    }
}
