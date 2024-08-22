using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public partial class TreeElement : WindowSkillElement
{
    public new void Update()
    {
        base.Update();

        SetPosition();
        UpdateUI();
        Cursor();
        UpdateState();
        UpdateRod();
    }

    public override void Initialization()
    {
        base.Initialization();

        Management_Child_Init();

        StartAnimation();

        Invoke("InitRod", 0.1f);
    }

    public virtual void InitRod()
    {
        Rod_Init(listParent);
    }

    public void StartAnimation()
    {
        float speed = 0.5f;

        transform.localScale = new Vector3(baseScale.x * 0.75f, baseScale.y * 0.75f, 1);

        transform.DOScale(baseScale, speed);
    }

    [HideInInspector]
    public Vector2 goldEffectScale = new Vector2(0.1f,0.1f);

    [HideInInspector]
    public static float goldEffect_RaisingSpeed = 1;

    public GameObject CreateSkillGoldEffect(Vector3 skillScale, Color32 theColor, bool DestroySelf = true, bool withBackGround = true)
    {
        GameObject goldEffectGameobject = Instantiate(Resources.Load<GameObject>("Prefab/Animation/Skill_GoldEffect"), transform);

        goldEffectGameobject.transform.SetAsFirstSibling();

        goldEffectGameobject.transform.localScale = Vector2.zero;

        goldEffectGameobject.transform.DOScale(skillScale, goldEffect_RaisingSpeed / 2);

        BigCircleGoldEffect goldEffectScript = goldEffectGameobject.GetComponent<BigCircleGoldEffect>();

        goldEffectScript.ModifyColor(theColor);

        goldEffectScript.knob_manual.gameObject.SetActive(withBackGround);

        if (DestroySelf)
        {
            int waitTime = 4;

            StartCoroutine(SkillGoldEffect_Erasing(goldEffectGameobject, waitTime));
        }

        return goldEffectGameobject;
    }

    public IEnumerator SkillGoldEffect_Erasing(GameObject g, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        EraseSkillGoldEffect(g);
    }

    public void EraseSkillGoldEffect(GameObject g, bool instant = false)
    {
        if (g == null)
            return;

        g.transform.DOScale(Vector2.zero, goldEffect_RaisingSpeed);

        Destroy(g, instant ? 0 : 4);
    }
}
