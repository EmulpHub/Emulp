using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleNewMap : MonoBehaviour
{
    private static TitleNewMap _instance;

    public static TitleNewMap Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.Find("TitleNewMap").GetComponent<TitleNewMap>();

            return _instance;
        }
    }

    public void Update()
    {
        timeBeforeErasing -= Time.deltaTime;

        if (timeBeforeErasing <= 0 && activeTitle)
        {
            activeTitle = false;
            EraseTitle();
        }
    }

    private bool activeTitle = false;

    private float timeBeforeErasing;

    public float timerBeforeErasingReset;
    public Outline titleOutline, underTitleOutline;
    public Text title, underTitle;
    public Image border_top, border_bot;

    public float border_bot_yWithUnderTitle, border_bot_yWithoutUnderTitle;

    public void NewTitle(string content, string underContent = "")
    {
        activeTitle = true;

        timeBeforeErasing = timerBeforeErasingReset;

        title.text = descColor.convert(content);
        this.underTitle.text = descColor.convert(underContent);

        border_bot.rectTransform.anchoredPosition = new Vector2(border_bot.rectTransform.anchoredPosition.x, underContent == "" ? border_bot_yWithoutUnderTitle : border_bot_yWithUnderTitle);

        outlineAnimation(underTitleOutline);
        textAnimation(underTitle);

        outlineAnimation(titleOutline);
        textAnimation(title);

        borderAnimation(border_top);
        borderAnimation(border_bot);
    }

    public void EraseTitle()
    {
        titleOutline.DOKill();
        titleOutline.DOFade(0, animationEraseTitleSpeed);

        title.DOKill();
        title.DOFade(0, animationEraseTitleSpeed);

        underTitleOutline.DOKill();
        underTitleOutline.DOFade(0, animationEraseTitleSpeed);

        underTitle.DOKill();
        underTitle.DOFade(0, animationEraseTitleSpeed);

        borderAnimationErase(border_top);
        borderAnimationErase(border_bot);
    }

    public float animationTitleSpeed;
    public float animationBorderSpeed;
    public float borderSize;

    public float animationEraseTitleSpeed;
    public float animationEraseBorderSpeed;

    private void borderAnimation(Image border)
    {
        border.transform.DOKill();

        border.DOFade(0, 0);
        border.DOFade(1, animationBorderSpeed / 2);

        border.rectTransform.sizeDelta = new Vector2(0, border.rectTransform.sizeDelta.y);

        border.rectTransform.DOSizeDelta(new Vector2(borderSize, border.rectTransform.sizeDelta.y), animationBorderSpeed);
    }

    private void borderAnimationErase(Image border)
    {
        border.transform.DOKill();

        border.rectTransform.DOSizeDelta(new Vector2(0, border.rectTransform.sizeDelta.y), animationEraseBorderSpeed);

        border.DOFade(0, animationEraseBorderSpeed / 2);

    }

    private void textAnimation(Text text)
    {
        text.DOKill();
        text.DOFade(0, 0);
        text.DOFade(1, animationTitleSpeed);
    }

    private void outlineAnimation(Outline outline)
    {
        outline.DOKill();
        outline.DOFade(0, 0);
        outline.DOFade(1, animationTitleSpeed);
    }
}
