using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// The animation of a single skillPoint that start at the LevelUp window and end to the skill_button
/// </summary>
public class Animation_AcquiredStuff : MonoBehaviour
{
    [HideInInspector]
    public Vector3 startPosition, endPosition;

    public Vector3 startScale, endScale;

    public float speed;

    [HideInInspector]
    public float jumpPower;

    public static GameObject AcquiredSkillG;

    public static void Init()
    {
        AcquiredSkillG = Resources.Load<GameObject>("Prefab/AcquiredStuff");
    }

    void Start()
    {
        transform.position = startPosition;
        transform.localScale = startScale;

        transform.DOJump(endPosition, jumpPower, 1, speed);
        transform.DOScale(endScale, speed - 0.1f);

        StartCoroutine(SparkleGenerator());

        Destroy(gameObject, speed + 0.5f);
    }

    public GameObject sparkle;

    public float sparke_apparitionSpeed, sparkle_deadSpeed, sparkle_randomnessPos, sparkle_randomnessScale;

    /// <summary>
    /// While the skillpoint animation is running instantiate sparkle
    /// </summary>
    /// <returns></returns>
    public IEnumerator SparkleGenerator()
    {
        do
        {
            yield return new WaitForSeconds(sparke_apparitionSpeed);
            GameObject g = Instantiate(sparkle);

            float RandomScaleValue = Random.Range(-sparkle_randomnessScale, sparkle_randomnessScale);

            g.transform.localScale = g.transform.localScale + new Vector3(RandomScaleValue, RandomScaleValue, 0);

            g.transform.position = transform.position + new Vector3(Random.Range(-sparkle_randomnessPos, sparkle_randomnessPos), Random.Range(-sparkle_randomnessPos, sparkle_randomnessPos), 0);

            g.transform.DOScale(0, sparkle_deadSpeed);

            Destroy(g.gameObject, sparkle_deadSpeed + 0.1f);
        } while (true);
    }

    public static void animation_instantiation(float startTime, Vector3 startPos, Vector3 endPos, Sprite graphic)
    {
        Spell.Reference.StartCoroutine(animation_instantiation_Cor(startTime, startPos, endPos, graphic));
    }

    public Image Graphic;

    private static IEnumerator animation_instantiation_Cor(float startTime, Vector3 startPos, Vector3 endPos, Sprite graphic)
    {
        yield return new WaitForSeconds(startTime);

        GameObject g = Instantiate(AcquiredSkillG);

        Animation_AcquiredStuff a = g.GetComponent<Animation_AcquiredStuff>();

        a.jumpPower = 0;
        a.startPosition = startPos;
        a.endPosition = endPos;

        a.Graphic.sprite = graphic;
    }
}
