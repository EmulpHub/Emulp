using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TreeElement : WindowSkillElement
{
    [HideInInspector]
    public List<TreeElement> listParent = new List<TreeElement>();

    public List<TreeElement> listChild = new List<TreeElement>();

    public TreeElement getFirstParentSkill()
    {
        if (listParent.Count > 0)
            return listParent[0];
        else
            return null;
    }

    public void AddParentSkill(TreeElement s)
    {
        listParent.Add(s);
    }

    public void Management_Child_Init()
    {
        if (listChild.Count == 0) return;

        int index = 0;

        foreach (TreeElement child in listChild)
        {
            if (child == null)
                continue;
            else if (V.IsInMain)
                child.transform.localPosition = Vector3.zero;

            child.AddParentSkill(this);

            if (V.IsInMain)
            {
                child.MoveAnchor(rectThis.anchoredPosition);

                child.AdditionalAnchorFromChild = CalcChildAnchorPosition(index, listChild.Count);
            }

            index++;
        }
    }
}
