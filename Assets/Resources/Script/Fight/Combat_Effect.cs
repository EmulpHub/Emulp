using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Linq;

public class Combat_Effect : MonoBehaviour
{
    [HideInInspector]
    public Effect effect;

    public Vector3 startPos;

    [HideInInspector]
    public Image img;

    public float movingDistance;

    [HideInInspector]
    public RectTransform rect;

    public void Initialize(Effect e)
    {
        img.sprite = e.img;

        effect = e;

        rect = GetComponent<RectTransform>();

        listCombatEffect.Add(this);

        e.event_strChanging.Add(StrEffectChanging);
        e.event_kill.Add(Kill);
    }

    public Color ModifierText_green, ModifierText_red;

    public Text ModifierText;

    private float timerStrEffectTime;

    public void StrEffectUpdate()
    {
        timerStrEffectTime -= Time.deltaTime;

        ModifierText.gameObject.SetActive(timerStrEffectTime > 0);

        if (timerStrEffectTime < 0) strChangeCurrent = 0;
    }

    float strChangeCurrent;

    public void StrEffectChanging(float before, float after)
    {
        float change = after - before;

        strChangeCurrent += change;

        if (strChangeCurrent > 0)
        {
            ModifierText.text = "+" + strChangeCurrent;
            ModifierText.color = ModifierText_green;
        }
        else
        {
            ModifierText.text = "" + strChangeCurrent;
            ModifierText.color = ModifierText_red;
        }

        timerStrEffectTime = 3;
    }

    public void Kill(Effect e)
    {
        if (IsDead) return;

        listCombatEffect.Remove(this);

        MakeAnimation_Dead();
    }

    public void Start()
    {
        UpdatePosition(GetTheoricalPosition(), true);

        UpdateUi();

        MakeAnimation();
    }

    public void UpdateUi()
    {
        StrEffectUpdate();

        string baseDesc = effect.getInfoDesc();

        string info = descColor.convert(baseDesc);

        string infoWhite = descColor.convert_noColorChange(baseDesc);

        Text infoText_choosen = descriptionText, infoText_removed = descriptionTextLong;

        if (infoWhite.Any(char.IsLetter))
        {
            infoText_choosen = descriptionTextLong;
            infoText_removed = descriptionText;
        }

        infoText_choosen.text = info;
        infoText_removed.text = "";

        duration.text = effect.GetDurationText();
    }

    public Text descriptionText, descriptionTextLong, duration;

    [HideInInspector]
    public bool isMouseOver_Save = false;

    public Vector2 nextPosSave;

    public void Update()
    {
        Vector2 nextPos = GetTheoricalPosition();

        if (nextPos != rect.anchoredPosition && !IsDead)
        {
            UpdatePosition();
        }

        bool IsMouseOver = DetectMouse.IsMouseOnUI(rect) && !Scene_Main.isMouseOverAWindow;

        if (IsMouseOver != isMouseOver_Save)
        {
            if (IsMouseOver)
                MouseEnter();
            else
                MouseExit();
        }

        UpdateUi();

        if (IsMouseOver)
            MouseOver();

        isMouseOver_Save = IsMouseOver;
    }

    public void DisplayDescription(Vector3 position, float distance)
    {
        string title = effect.GetTitle();
        string description = effect.GetDescription();

        Main_UI.Display_Description(title, description, position, distance, false);
    }

    [HideInInspector]
    public float x_distance = 0;

    public void UpdatePosition(Vector2 nextPos, bool Instant)
    {
        if (nextPos != nextPosSave)
        {
            if (!Instant)
                MakeAnimation();
            nextPosSave = nextPos;
        }

        if (Instant)
        {
            rect.anchoredPosition = nextPos;
        }
        else
        {
            rect.DOAnchorPos(nextPos, 1);
        }
    }

    public void UpdatePosition()
    {
        UpdatePosition(GetTheoricalPosition(), false);
    }

    public void ForcePosition()
    {
        UpdatePosition(GetTheoricalPosition(), true);
    }

    public static List<Combat_Effect> listCombatEffect = new List<Combat_Effect>();

    public Vector2 GetTheoricalPosition()
    {
        int this_index = listCombatEffect.IndexOf(this);

        if (this_index >= 1)
        {
            float ScaleModifier = 0.9f;

            Combat_Effect precedentEffect = listCombatEffect[this_index - 1];

            x_distance = precedentEffect.x_distance +
                //Add the scale of precedentEffect
                movingDistance * precedentEffect.CurrentScale.x * ScaleModifier +
                //Add the scale of this effect
                movingDistance * (CurrentScale.x - 1) * ScaleModifier;
        }
        else
        {
            x_distance = 0;
        }

        return (Vector2)startPos + new Vector2(x_distance, 0);
    }

    public void MouseEnter()
    {
    }

    public void MouseOver()
    {
        DisplayDescription(transform.position, 1.2f);
    }

    public void MouseExit()
    {
        Main_UI.Display_Description_Erase(effect.GetTitle());
    }

    [HideInInspector]
    public bool IsDead = false;

    public GameObject group;

    [HideInInspector]
    public bool deadAnimationLaunched = false;

    [HideInInspector]
    private Vector3 CurrentScale = new Vector3(1, 1, 1);

    public enum PossibleScale { Normal, Big }

    [HideInInspector]
    public PossibleScale CurrentScale_type;

    public void ChangeCurrentScale(PossibleScale possibleScale)
    {
        if (deadAnimationLaunched || CurrentScale_type == possibleScale)
            return;

        CurrentScale_type = possibleScale;

        CurrentScale = new Vector3(1, 1, 1);

        if (possibleScale == PossibleScale.Big)
            CurrentScale = new Vector3(1.2f, 1.2f, 1.2f);

        group.transform.DOKill();

        group.transform.DOScale(CurrentScale, 0.3f);

        StartCoroutine(WaitBeforeCurrentScaleIsSet());
    }

    private bool CurrentScaleIsChanging;

    public IEnumerator WaitBeforeCurrentScaleIsSet()
    {
        CurrentScaleIsChanging = true;

        yield return new WaitForSeconds(0.4f);

        CurrentScaleIsChanging = false;
    }

    public void MakeAnimation()
    {
        return;

        if (deadAnimationLaunched || CurrentScaleIsChanging)
            return;

        group.transform.DOKill();

        group.transform.localScale = CurrentScale;

        group.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0), 0.5f, 1);
        /*
        //Destroy All visual effect
        foreach (Animation_Endless a in VisualEffectList)
        {
            Animation_Endless.RemoveFromDict(a);

            a.Erase();
        }*/
    }

    /// <summary>
    /// Make the death animation and destroy it
    /// </summary>
    public void MakeAnimation_Dead()
    {
        IsDead = true;

        deadAnimationLaunched = true;

        group.transform.DOKill();

        group.transform.localScale = CurrentScale;

        group.transform.DOScale(0, 0.5f);

        rect.DOAnchorPos(rect.anchoredPosition + new Vector2(0, -50), 0.5f);

        Destroy(this.gameObject, 1);
    }

}
