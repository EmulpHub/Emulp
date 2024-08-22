using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class card : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public static List<card> card_ls = new List<card>();
    public Text title, desc, info_txt;
    public Image img, contour, bg;
    [HideInInspector]
    public int index, index_max;
    public GameObject Holder;
    public Animator card_blow;
    [HideInInspector]
    public Vector3 Holder_BaseScale;
    public bool MouseIsOver;
    public float Anim_EnterScaleMutliplication, Anim_MouseSpeed, Discover_TimeInSec, diffX;

    public static void AddToCurrentCard(card e)
    {
        card_ls.Add(e);
    }

    public static void RemoveAllCurrentCard()
    {
        while (card_ls.Count > 0)
        {
            card_ls[0].DestroyThis();
        }
    }

    public static void RemoveToCurrentCard(card e)
    {
        card_ls.Remove(e);
    }

    public virtual void Init(int index = 0, int index_max = 0)
    {
        this.index = index;
        this.index_max = index_max;

        AddToCurrentCard(this);

        bg.color = new Color32(255, 255, 255, 255);

        ClickAutorization.Add(this.gameObject);

        Init_Animation();

        Holder.transform.position = CalcPosition();
    }

    public virtual float getYPosition()
    {
        return 1.15f;
    }

    public Vector3 CalcPosition()
    {
        float Y = getYPosition();

        if (index_max == 0)
            return new Vector3(0,Y,0);

        return new Vector3(diffX * (float)index - (diffX / 2) * (float)index_max, Y, 0);
    }

    public virtual void Update()
    {
        Anim_Update();

        ManageCursor();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (!ClickAutorization.Autorized(this.gameObject)) return;

        OnClick();
    }

    public virtual void OnClick()
    {
    }

    public void DestroyThis()
    {
        StartCoroutine(DestroyThis_co());
    }

    public IEnumerator DestroyThis_co()
    {
        RemoveToCurrentCard(this);

        Anim_Erase();

        yield return new WaitForSeconds(EraseSpeed + 0.2f);

        ClickAutorization.Remove(this.gameObject);

        Destroy(this.gameObject);
    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (!ClickAutorization.Autorized(this.gameObject)) return;

        MouseIsOver = true;

        Anim_MouseEnter();
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        MouseIsOver = false;

        Anim_MouseExit();
    }

    public void ManageCursor()
    {
        Main_UI.ManageDontMoveCursor(this.gameObject, MouseIsOver);

        if (MouseIsOver)
        {
            Window.SetCursorAndOffsetHand();
        }
    }

    public void Init_Animation()
    {
        Holder_BaseScale = Holder.transform.localScale;
    }

    public void Anim_Update()
    {
    }

    public void Anim_Discover()
    {
        StartCoroutine(Anim_Discover_Enum());
    }

    public float EraseSpeed;

    bool isDead;

    public void Anim_Erase()
    {
        isDead = true;

        Holder.transform.DOScaleX(0, EraseSpeed);
    }

    public IEnumerator Anim_Discover_Enum()
    {
        card_blow.speed = 1 / Discover_TimeInSec;

        card_blow.Play("Card_Blow");

        //card_blow.GetComponent<SpriteRenderer>().DOColor(, Discover_TimeInSec);

        yield return new WaitForSeconds(0);
    }

    public void Anim_MouseEnter()
    {
        if (isDead)
            return;
        Holder.transform.DOKill();

        Holder.transform.localScale = Holder_BaseScale * Anim_EnterScaleMutliplication;
    }

    public void Anim_MouseExit()
    {
        if (isDead)
            return;
        Holder.transform.DOKill();

        Holder.transform.DOScale(Holder_BaseScale, Anim_MouseSpeed);
    }

}
