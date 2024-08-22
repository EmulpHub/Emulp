using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class TreeElement : WindowSkillElement
{

    public static float rod_scaleOnMouseOver = 1.3f, rod_changeScale_Speed = 0.5f;

    public float CalcRodScale(Panel_Rod r)
    {
        if (r.child.MouseOver || r.parent.MouseOver) return rod_scaleOnMouseOver;

        return 1;
    }

    private bool ActiveRod = false;

    public void Rod_Init(List<TreeElement> ls)
    {
        rod_parent = window.rodParent;

        rod_prefab = window.rod_prefab;

        ActiveRod = true;

        Rod_Create(ls);

        if (V.IsInMain) UpdateRod(true);
    }

    private Transform rod_parent;

    private GameObject rod_prefab;

    private List<Panel_Rod> listRod = new List<Panel_Rod>();

    public Panel_Rod Rod_Add(GameObject Prefab, TreeElement parent)
    {
        Panel_Rod script = Instantiate(Prefab, rod_parent).GetComponent<Panel_Rod>();

        script.child = this;
        script.parent = parent;

        listRod.Add(script);

        return script;
    }

    void CreateRod(TreeElement s)
    {
        Panel_Rod r = Rod_Add(rod_prefab, s);

        r.transform.up = transform.position - s.transform.position;

        float width_x = Mathf.Abs(rectThis.anchoredPosition.x - s.rectThis.anchoredPosition.x);

        float width_y = Mathf.Abs(rectThis.anchoredPosition.y - s.rectThis.anchoredPosition.y);

        RectTransform rectRod = r.GetComponent<RectTransform>();

        r.transform.localScale = new Vector3(r.transform.localScale.x, F.CalculateHypothenus(width_x, width_y) / rectRod.sizeDelta.y, 1);

        r.baseScale = rectRod.sizeDelta;

        r.rectThis.anchoredPosition = s.rectThis.anchoredPosition;

    }

    public void Rod_Create(List<TreeElement> ls)
    {
        if (ls.Count == 0) return;

        listRod.Clear();

        foreach (TreeElement w in ls)
        {
            CreateRod(w);
        }
    }

    void Rod_Update(bool init,int index = 0)
    {
        TreeElement firstParentSkill = getFirstParentSkill();

        if (firstParentSkill == null) return;

        Panel_Rod rod_script = listRod[index];

        RectTransform rod_rect = rod_script.rectThis;

        float scale = CalcRodScale(rod_script);

        Vector2 rodScale = rod_script.baseScale;

        if (init)
            rod_rect.sizeDelta = new Vector2(rodScale.x * scale, rodScale.y);
        else
            rod_rect.DOSizeDelta(new Vector2(rodScale.x * scale, rodScale.y), rod_changeScale_Speed);
       
    }


    public void UpdateRod(bool init = false)
    {
        if (!ActiveRod || listRod.Count == 0) return;

            for (int i = 0; i < listRod.Count; i++)
            {
                Rod_Update(init,i);
            }
    }
}
