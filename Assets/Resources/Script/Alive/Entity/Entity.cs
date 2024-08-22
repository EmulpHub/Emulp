using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public abstract partial class Entity : MonoBehaviour
{
    private void Awake()
    {
        OnAwake();
    }

    private void Start()
    {
        OnStart();
    }

    private void Update()
    {
        OnUpdate();
    }

    private static Entity _entityOnMouseOver;

    public static Entity EntityOnMouseOver
    {
        get
        {
            return _entityOnMouseOver;
        }
        set
        {
            _entityOnMouseOver = value;
            if (value == null)
                Monster.EraseHighlighMonster();
        }
    }

    private void OnMouseOver()
    {
        if (!ClickAutorization.Autorized(this.gameObject))
            return;

        OnMouseIsOver();

        if (Input.GetMouseButtonDown(0))
            OnMouseIsDown_Left();
        else if (Input.GetMouseButtonDown(1))
            OnMouseIsDown_Right();
    }

    private void OnMouseExit()
    {
        OnMouseIsExit();
    }

    public virtual void ResetAllStats()
    {
        Info.CalculateValue();
        Info.ResetAllStats();

        ClearEffect();

        ResetUI();
    }
}