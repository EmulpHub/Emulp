using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class AnimEndless : MonoBehaviour
{
    internal Entity target;
    internal Vector3 offset;
    internal float Speed = 0,Size;
    internal AnimEndlessStatic.FollowMode followMode;
    internal bool erased = false;
    public AnimEndlessStatic.ID CurrentId;
    public string specialId = "none";
    public List<customUpdate> listCustomUpdate = new List<customUpdate>();
    public delegate void customUpdate();

    public Canvas canvas;

    public void ChangeSortingOrder (int order)
    {
        canvas.sortingOrder = order;
    }

    public void ChangeLayer(string LayerName)
    {
        canvas.sortingLayerName = LayerName;
    }

    public void Update()
    {
        MoveToTarget();
        ApplyCustomUpdate();
    }

    public void SetFollowMode(AnimEndlessStatic.FollowMode m)
    {
        followMode = m;
    }

    public void MoveToTarget()
    {
        if (followMode == AnimEndlessStatic.FollowMode.smooth)
        {
            this.gameObject.transform.DOMove(CalcPosToTarget(), 0);
        }
        else
        {
            GoToTarget();
        }
    }

    public Vector3 CalcPosToTarget()
    {
        if (target == null)
        {
            if (!erased)
            {
                erased = true;
                Erase();
            }

            return Vector3.zero;
        }

        return target.Renderer.transform.position + offset;
    }

    public void GoToTarget()
    {
        this.gameObject.transform.position = CalcPosToTarget();
    }

    public void UpdateScale()
    {
        this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x * Size, this.gameObject.transform.localScale.y * Size, 1);
    }

    public void Erase_WithoutAnimation(float delayInSec = 0)
    {
        AnimEndlessStatic.list_erase(this);

        Destroy(this.gameObject, delayInSec);
    }

    public virtual void Erase()
    {
        Erase_WithoutAnimation();
    }

    public virtual int AddToStaticList()
    {
        return AnimEndlessStatic.list_add(this);
    }

    public void SetSpecialId(string specialId)
    {
        this.specialId = specialId;
    }

    public void LinkToEffect(Effect effect)
    {
        effect.EndlessAnim_add(this);
    }

    public void ApplyCustomUpdate()
    {
        foreach (customUpdate update in listCustomUpdate)
        {
            update();
        }
    }

    public void AddCustomUpdate(customUpdate a)
    {
        listCustomUpdate.Add(a);
    }

}
