using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigCircleGoldEffect : MonoBehaviour
{
    public void Awake()
    {
        Init();
    }

    public void Init()
    {
        SetColor();

        CreateAllBar();

        AnimationInit();
    }

    #region CreatingBar

    public int nbBar;

    public float degrePerBar => 360 / (float)nbBar;

    public List<barInfo> bars = new List<barInfo>();

    public struct barInfo
    {
        public Animator anim;
        public Image bar_img, bar_child_img;

        public barInfo(Animator anim, Image bar_img, Image bar_child_img)
        {
            this.anim = anim;
            this.bar_img = bar_img;
            this.bar_child_img = bar_child_img;
        }
    }

    public Color32 curColor;

    public Image knob_manual;

    public void SetColor()
    {
        knob_manual.color = curColor;

        foreach (barInfo b in bars)
        {
            b.bar_img.color = curColor;
            b.bar_child_img.color = curColor;
        }
    }

    public void ModifyColor(Color color)
    {
        curColor = color;

        SetColor();
    }

    public void CreateAllBar()
    {
        int i = 0;

        while (i < nbBar)
        {
            InstantiateBar(i * degrePerBar);

            i++;
        }
    }

    public GameObject bar;

    public Transform bar_parent;

    public Animator InstantiateBar(float degre)
    {
        Animator g = Instantiate(bar, bar_parent).GetComponent<Animator>();

        g.transform.rotation = Quaternion.Euler(new Vector3(0, 0, degre));

        Image bar_img = g.gameObject.GetComponent<Image>();

        Image bar_child_img = g.transform.GetChild(0).GetComponent<Image>();

        bar_img.color = curColor;

        bar_child_img.color = curColor;

        bars.Add(new barInfo(g, bar_img, bar_child_img));

        return g;
    }

    #endregion

    #region AnimationInit

    public float TimeBeforeNewAnimation;

    public int nbWave;

    public void AnimationInit()
    {
        int indexSeparator = Mathf.Clamp(nbBar / nbWave, 0, 999);

        for (int i = 0; i < nbWave; i++)
        {
            StartCoroutine(Wave(indexSeparator * i, TimeBeforeNewAnimation));
        }
    }

    public IEnumerator Wave(int startIndex, float timeBetweenAnimation)
    {
        if (bars.Count == 0)
            throw new System.Exception("bars can't be empty wave of bigcirlcefoldeffect");

        int curIndex = startIndex;

        while (true)
        {
            if (curIndex >= bars.Count)
            {
                curIndex = 0;
            }

            barInfo cur = bars[curIndex];

            StartTheAnim(cur.anim);

            curIndex++;

            yield return new WaitForSeconds(timeBetweenAnimation);
        }
    }

    public void StartTheAnim(Animator bar)
    {
        bar.Play("Skill_GoldEffectBar_Anim");
    }

    #endregion
}
