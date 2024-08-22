using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class TreeElement : WindowSkillElement
{
    [HideInInspector]
    public Vector2 Anchor;

    public Vector2 AdditionalInspectorAnchor;

    [HideInInspector]
    public Vector2 AdditionalAnchorFromChild;

    public Vector2 GetAdditionalAnchor()
    {
        return AdditionalAnchorFromChild + AdditionalInspectorAnchor;
    }

    public virtual void SetPosition()
    {
        TreeElement f = getFirstParentSkill();

        Vector2 BaseAnchor = new Vector2(0,0);

        if (f != null)
            BaseAnchor = f.rectThis.anchoredPosition + GetAdditionalAnchor() + AdditionalInspectorAnchor;
        else
            BaseAnchor = AdditionalInspectorAnchor;

        Anchor = new Vector2(BaseAnchor.x, BaseAnchor.y);

        MoveAnchor(Anchor);
    }

    public Vector2 CalcChildAnchorPosition(int index, int totalChild)
    {
        float X = ((totalChild - 1) * -Diff_X) / 2 + Diff_X * index;

        float Y = Diff_Y;

        return new Vector2(X,Y);
    }

    public float Diff_X, Diff_Y;

    public void MoveAnchor(Vector2 NewAnchor)
    {
        rectThis.anchoredPosition = NewAnchor;
    }

    public void CenterCamera()
    {
        CenterCamera(Vector2.zero);
    }

    public void CenterCamera(Vector2 offset)
    {
        window.SetScaleAndPosition(-rectThis.anchoredPosition + offset, 0.6f, true, 0.4f);
    }
}
