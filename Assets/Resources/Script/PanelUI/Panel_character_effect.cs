using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_character_effect : MonoBehaviour
{
    [HideInInspector]
    public Effect effect;

    public RelicInit.relicLs relic;

    public Image render;

    public Text cooldown;

    public string title;

    public Vector3 baseScale;

    public Window_character win;

    public bool IsAnEffect;

    public void Initialize(Effect effect, Sprite img, string title, Window_character win)
    {
        this.effect = effect;

        this.title = title;

        this.win = win;

        render.sprite = img;

        UpdateBottomText();

        baseScale = transform.localScale;

        IsAnEffect = true;
    }

    public void Initialize(RelicInit.relicLs r, Window_character win)
    {
        this.relic = r;

        this.title = RelicInit.relic_title(r);

        this.win = win;

        render.sprite = RelicInit.relic_sprite(r);

        UpdateBottomText();

        baseScale = transform.localScale;

        IsAnEffect = false;
    }

    public RectTransform box;

    bool lastMouseOver = false;

    public void Update()
    {
        bool mouseOver = win.mouseIsOnWindowArea && win.inFront && DetectMouse.IsMouseOnUI(box);

        if (mouseOver != lastMouseOver)
        {
            if (mouseOver == true)
            {
                WhenMouseEnter();
            }
            else
            {
                WhenMouseExit();
            }
        }
        else if (mouseOver)
        {
            WhenMouseOver();
        }

        UpdateBottomText();

        lastMouseOver = mouseOver;
    }

    public void UpdateBottomText()
    {
        if (IsAnEffect)
            cooldown.text = descColor.convert(effect.getInfoDesc());
        else
            cooldown.text = "";
    }

    public void WhenMouseEnter()
    {
        TreeElement.Animation_set(gameObject, 0.12f, baseScale);

        DisplayDescription(transform.position, 1.2f);
    }

    public void WhenMouseOver()
    {
        DisplayDescription(transform.position, 1.2f);
    }

    public void DisplayDescription(Vector3 position, float distance)
    {
        if (!IsAnEffect)
        {
            Display_description_text.Display_Description(RelicInit.relic_title(relic), RelicInit.relic_desc(relic), position, 0.2f/* RelicInit.relic_equipmentValue(relic)*/);
            return;
        }

        string title = effect.GetTitle();
        string description = effect.GetDescription();

        Main_UI.Display_Description(title, description, position, distance, false);
    }

    public void WhenMouseExit()
    {
        Main_UI.Display_Description_Erase();

        TreeElement.Animation_scale(gameObject, baseScale.x, 0.2f);
    }
}
