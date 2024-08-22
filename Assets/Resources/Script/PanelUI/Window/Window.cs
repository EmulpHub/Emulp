using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract partial class Window : MonoBehaviour
{
    public WindowInfo.type type;

    [HideInInspector]
    public Canvas Parent_canvas;

    [HideInInspector]
    public RectTransform thisRect;

    public Text title;

    [HideInInspector]
    public bool inFront
    {
        get { return WindowInfo.Instance.IsSelectionned(this); }
    }

    [HideInInspector]
    public bool active
    {
        get { return gameObject.activeInHierarchy; }
    }

    public virtual void Awake()
    {
        thisRect = GetComponent<RectTransform>();

        Parent_canvas = transform.parent.GetComponent<Canvas>();

        Parent_canvas.worldCamera = Camera.main;
    }

    public void Start()
    {
        Update_Ui();
    }

    public virtual void Update()
    {
        ManageSortingOrder();

        UpdateFade();

        CheckIfSetPos();

        UpdateError();

        Update_Ui();

        UpdateMouse();

        UpdateCursorTexture();
    }

    public virtual void Update_Ui() { }
}
