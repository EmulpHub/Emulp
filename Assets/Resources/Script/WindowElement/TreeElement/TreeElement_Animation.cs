using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public partial class TreeElement : WindowSkillElement
{
    public static float punchScaleStrengh = 0.1f, punchScaleSpeed_Up = 0.1f,
        punchScaleSpeed_Down = 0.1f, punchScaleStrengh_MouseClick = 0.1f;

    public static void ShakeAnimation(float Strengh, float Speed, float Start, GameObject target)
    {
        target.transform.DOKill();
        target.transform.localScale = new Vector3(Start, Start, 1);
        target.transform.DOPunchScale(new Vector3(1, 1, 0) * Strengh, Speed, 1);
    }

    public static void Animation_set (GameObject target, float str, Vector3 baseScale)
    {
        target.transform.DOKill();

        target.transform.localScale = baseScale + new Vector3(str, str, 0);
    }

    public static void Animation_punchScale (GameObject target, float Strengh, float speed)
    {
        target.transform.DOKill();

        target.transform.DOPunchScale(new Vector3(1, 1, 0) * Strengh, speed, 1);
    }

    public static void Animation_scale (GameObject target,float newScale,float speed)
    {
        target.transform.DOKill();

        target.transform.DOScale(newScale, speed);
    }

    [HideInInspector]
    public static GameObject sparkle;

    public enum SparkleAnimationColor { yellow, red, green }

    public static void AnimationSparkle(int count, SparkleAnimationColor color, TreeElement script)
    {
        sparkle.transform.localScale = Vector3.zero;

        Image img = sparkle.GetComponent<Image>();

        if (color == SparkleAnimationColor.yellow)
            img.color = new Color32(255, 255, 255, 255);
        else if (color == SparkleAnimationColor.red)
            img.color = new Color32(200, 0, 20, 255);
        else if (color == SparkleAnimationColor.green)
            img.color = new Color32(51, 156, 48, 255);

        for (int i = 0; i < count; i++)
        {
            RectTransform sparkleRect = Instantiate(sparkle, script.window.sparkle_parent.transform).GetComponent<RectTransform>();

            float speed = Random.Range(2, 6) * 0.1f;

            Vector2 dir = new Vector2(Random.Range(-100, 100 + 1), Random.Range(-100, 100 + 1)).normalized;

            Vector2 strenght = dir * Random.Range(30, 40);

            sparkleRect.anchoredPosition = script.rectThis.anchoredPosition + strenght * 0.3f; 

            sparkleRect.DOAnchorPos(strenght + sparkleRect.anchoredPosition, speed);

            sparkleRect.DOScale(Random.Range(16, 20 + 1) * 0.01f, speed);

            sparkleRect.DORotate(new Vector3(0, 0, Random.Range(-45, 45)), speed + 0.3f);

            sparkleRect.gameObject.GetComponent<Image>().DOFade(0, speed + Random.Range(9, 12 + 1) * 0.1f);

            Destroy(sparkleRect.gameObject, 3);
        }
    }
}
