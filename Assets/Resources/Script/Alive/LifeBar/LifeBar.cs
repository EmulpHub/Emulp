using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LifeBar : MonoBehaviour
{
    #region variable

    private Entity holder;

    private EntityInfo holderInfo;

    public GameObject bar_prefab;

    public RectTransform bar_parent;

    public Image sliced_front, sliced_front_shader;

    public Text lifeText;

    public Transform subHolder;

    public GameObject canvas;

    #endregion

    public void Init(Entity holder)
    {
        SetHolder(holder);

        Bar_Create();

        SetPos();

        SetLifeValue();
    }

    public void SetHolder(Entity holder)
    {
        this.holder = holder;
        holderInfo = holder.Info;
        holder.event_die.Add(WhenHolderDie);
        holder.event_life_dmg.Add(Event_Damage);
        holder.event_turn_start.Add(Event_StartTurn);
    }

    public void Update()
    {
        SetText();

        CanvasActivation();

        SlicedFrontManagement();

        SetPos();

        CheckForDestroying();

        ShakingLowLife();
    }

    public void SlicedFrontManagement()
    {
        float fillAmount = holderInfo.Life / holderInfo.Life_max;

        sliced_front.fillAmount = fillAmount;
        sliced_front_shader.fillAmount = fillAmount;
    }

    public void CanvasActivation()
    {
        subHolder.gameObject.SetActive(holder.ShouldShowLifeBar());
    }

    #region Text

    float LifeText = 0f;
    
    public float textAnimationSpeed;

    public void SetText()
    {
        lifeText.text = "" + Mathf.CeilToInt(LifeText);
    }

    public void SetLifeValue ()
    {
        LifeText = holderInfo.Life;
    }

    public void AnimText (float wantedValue)
    {
        DOTween.To(() => LifeText, x => LifeText = x, wantedValue, textAnimationSpeed);
    }

    #endregion
    
    public void SetPos()
    {
        Vector3 Pos = holder.transform.position + new Vector3(0, 1 + holder.title_currentDistance + transform.localScale.x - holder.baseScale.x, 0);

        transform.position = Main_UI.FindConfortablePos(V.camera_maxYPos, Pos, sliced_front.rectTransform, canvas);
    }

    #region Bar

    Vector2 size10 = new Vector2(1.5f, 15), size100 = new Vector2(2.21f, 26);

    private void Bar_Create_One(Vector3 size, float ratio)
    {
        RectTransform bar = Instantiate(bar_prefab, bar_parent.transform).GetComponent<RectTransform>();

        float barX = bar_parent.rect.size.x * ratio;

        bar.anchoredPosition = new Vector3(barX, 0);

        bar.sizeDelta = size;
    }

    public void Bar_Create()
    {
        float lifeMax = holderInfo.Life_max;

        bool nb10 = lifeMax / 10 <= 100;
        bool nb100 = lifeMax / 100 <= 1000;

        int lifeParkour = 0;

        while (lifeParkour < lifeMax - 10)
        {
            lifeParkour += 10;

            float ratio = (float)lifeParkour / lifeMax;

            if (nb100 && lifeParkour % 100 == 0)
            {
                Bar_Create_One(size100, ratio);
            }
            else if (nb10)
            {
                Bar_Create_One(size10, ratio);
            }
        }
    }

    #endregion

    #region DamageAnimation 

    public GameObject DmgAnimation;
    public RectTransform DmgAnimation_Parent;
    private List<LifeBar_slicedDamage> listHistoryDamage = new List<LifeBar_slicedDamage>();
    private float lastDamageTime = 0;
    public float timeBeforeRemovingDamager, timeBetweenDamagerRemoval;

    public void InstantiateDmgAnimation(float dmg)
    {
        LifeBar_slicedDamage animationScript = Instantiate(DmgAnimation, DmgAnimation_Parent.transform).GetComponent<LifeBar_slicedDamage>();
        RectTransform animation = animationScript.thisRect;

        float currentRatioDmg = dmg / holderInfo.Life_max;

        float currentRatioLife = holderInfo.Life / holderInfo.Life_max;

        float sizeX = DmgAnimation_Parent.rect.size.x * currentRatioDmg;
        float sizeY = DmgAnimation_Parent.rect.size.y;

        float posX = DmgAnimation_Parent.rect.size.x * currentRatioLife + sizeX / 2;

        animation.anchoredPosition = new Vector2(posX, -20);

        animation.sizeDelta = new Vector2(sizeX, sizeY);

        Image animationImage = animationScript.image;
        Image animationContourRight = animationScript.rightEdge;

        int i = 0;
        while (i < listHistoryDamage.Count)
        {
            var historyLife = listHistoryDamage[i].lifeBeforeDmg;

            if ((int)historyLife == (int)(holderInfo.Life + dmg))
            {
                animationContourRight.gameObject.SetActive(true);
                break;
            }
            else
                i++;
        }

        lastDamageTime = Time.time;
        animationScript.lifeBeforeDmg = holderInfo.Life;
        animationScript.dmg = dmg;
        animationScript.Appear();
        listHistoryDamage.Add(animationScript);
    }

    public void CheckForDestroying()
    {
        if (listHistoryDamage.Count > 0 && Time.time - lastDamageTime >= timeBeforeRemovingDamager)
        {
            DestroyEffect();
        }
    }

    public void DestroyEffect ()
    {
        StartCoroutine(DestroyingDamager());
        DestroyDmgText();

    }

    public IEnumerator DestroyingDamager()
    {
        var saveHistroyDamager = new List<LifeBar_slicedDamage>(listHistoryDamage);

        listHistoryDamage.Clear();

        while (saveHistroyDamager.Count > 0)
        {
            var dmgScript = saveHistroyDamager[0];

            dmgScript.Destroy();

            saveHistroyDamager.RemoveAt(0);

            yield return new WaitForSeconds(timeBetweenDamagerRemoval);
        }
    }

    public void Event_Damage(InfoDamage info)
    {
        AnimText(holderInfo.Life);

        InstantiateDmgAnimation(info.damage);
        InstantiateDmgText(info.damage);
    }

    #endregion

    #region DmgText 

    public Transform DmgTextParent;
    public GameObject DmgTextPrefab;
    public float DmgTextSeparator;

    [HideInInspector]
    public List<RectTransform> listDmgText = new List<RectTransform>();

    public float DmgTextDestroySpeed,DmgTextPosYWhenDestroyed;

    public void DestroyDmgText()
    {
        totalDamage = 0;

        foreach(RectTransform dmgText in listDmgText)
        {
            dmgText.DOAnchorPosY(DmgTextPosYWhenDestroyed,DmgTextDestroySpeed);
            dmgText.DOScale(new Vector3(0.3f, 0.3f,1), DmgTextDestroySpeed);

            Destroy(dmgText.gameObject, 3);
        }

        listDmgText.Clear();
    }

    private Text DmgTextCumulative;
    private float totalDamage;

    public void InstantiateDmgText(float dmg)
    {
        totalDamage += dmg;

        if (listDmgText.Count == 1)
        {
            DmgTextCumulative.text = "-" + totalDamage;

            DmgTextCumulative.rectTransform.DOShakeScale(ShakeScaleDuration, ShakeScaleStrenght,1);
        }
        else
        {
            DmgTextCumulative = InstantiateDmgText_One(dmg);
        }
    }

    public float DmgTextSeparatorSpeed;
    public float ShakeScaleDuration,ShakeScaleStrenght;

    public Text InstantiateDmgText_One(float dmg)
    {
        Text dmgText = Instantiate(DmgTextPrefab,DmgTextParent).GetComponent<Text>();

        dmgText.text = "-" + dmg;

        int count = listDmgText.Count + 1;

        dmgText.rectTransform.DOAnchorPosY(count * DmgTextSeparator,DmgTextSeparatorSpeed);

        dmgText.rectTransform.localScale = dmgText.rectTransform.localScale * Mathf.Clamp((1 - (float)count / 10),0.2f,1);

        listDmgText.Add(dmgText.rectTransform);

        return dmgText;
    }

    #endregion

    public void Event_StartTurn (Entity e)
    {
        DestroyEffect();
    }

    #region EntityDie 

    public GameObject LifeBarDestroyed_Prefab;

    public void WhenHolderDie(Entity e)
    {
        GameObject animation = Instantiate(LifeBarDestroyed_Prefab);

        animation.transform.position = transform.position;

        Destroy(animation, 9);

        Destroy(this.gameObject);
    }

    #endregion


    #region ShakingBehavior 

    public float LowLife_shakeStr, LowLife_shakeSpeed;

    public float timerBetweenEachShakeLowLife;

    private float TimerForNextShake; 

    public void ShakingLowLife ()
    {
        if (holderInfo.Life < holderInfo.Life_max * 0.1f)
        {
            TimerForNextShake -= Time.deltaTime;
            if(TimerForNextShake < 0)
            {
                TimerForNextShake = timerBetweenEachShakeLowLife;
                subHolder.DOShakePosition(LowLife_shakeSpeed, LowLife_shakeStr, 1, 0);
            }
        } 
        else
        {
            TimerForNextShake = 0;
            subHolder.transform.DOKill();
            subHolder.localPosition = Vector3.zero;
        }
    }

    #endregion
}
