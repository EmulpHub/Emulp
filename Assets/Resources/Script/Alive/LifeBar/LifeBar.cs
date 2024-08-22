using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    #region variable

    public Entity holder;

    public EntityInfo holder_info { get => holder.Info; }

    public Color32 color_normal, color_heal, color_dmg;

    public GameObject canvas, bar_prefab;

    public RectTransform bar_parent;

    public Image lifeBar_Interior_Behind, lifeBar_Sliced;

    public Text lifeBar_text;

    public float lifeBar_Interior_Behind_FillAmount = 1, lifeBar_Interior_FillAmount = 1, lifeBar_Dmg_FixedReductionSpeed;

    #endregion

    public void InitInfo(Entity holder)
    {
        this.holder = holder;

        LifeBar_Create();

        holder.event_die.Add(WhenHolderDie);

        SetPos();
    }

    public void WhenHolderDie(Entity e)
    {
        Destroy(this.gameObject);
    }

    public void Update()
    {
        lifeBar_text.text = "" + Mathf.CeilToInt(holder_info.Life);

        canvas.gameObject.SetActive(holder.ShouldShowLifeBar());

        lifeBar_Sliced.color = color_normal;
        lifeBar_Sliced.fillAmount = (float)holder_info.Life / (float)holder_info.Life_max;

        float diff = lifeBar_Interior_Behind.fillAmount - lifeBar_Interior_Behind_FillAmount;

        if (diff > 0)
            lifeBar_Interior_Behind.fillAmount -= lifeBar_Dmg_FixedReductionSpeed * Time.deltaTime;

        SetPos();
    }

    public void SetPos()
    {
        canvas.transform.position = Main_UI.FindConfortablePos(V.camera_maxYPos, holder.transform.position + new Vector3(0, 1 + holder.title_currentDistance + transform.localScale.x - holder.baseScale.x, 0), lifeBar_Sliced.rectTransform, canvas);
    }


    public void LifeBar_Create()
    {
        foreach (Transform child in bar_parent.transform)
        {
            DestroyImmediate(child.gameObject);
        }

        Vector2[] Size = { new Vector2(3.8f, 7.05f * 2), new Vector2(4, 11 * 2), new Vector2(6, 14 * 2) };

        void create(int lvl, float percentage)
        {
            RectTransform r = Instantiate(bar_prefab, bar_parent.transform).GetComponent<RectTransform>();

            r.anchoredPosition = new Vector3(bar_parent.rect.size.x * percentage / 100, 0);

            r.sizeDelta = Size[lvl];
        }

        float per = 100 / ((float)holder_info.Life_max / 50);

        float current = per;

        int nb = 0;

        while (current <= 100)
        {
            int lvl = 0;
            if (nb % 4 == 0 && nb != 0)
                lvl = 1;
            else if (nb % 20 == 0 && nb != 0)
            {
                lvl = 2;
            }

            create(lvl, current);

            current += per;
            nb++;
        }
    }
}
