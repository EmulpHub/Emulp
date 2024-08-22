using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Window_character : Window
{
    public Panel_character_effect passif_effect;

    public bool ShouldHavePassiveEffect()
    {
        return entity.IsMonster() && ((Monster)entity).ContainPassive && effects.Count > 0;
    }

    public void SetPassifEffect()
    {
        if (!ShouldHavePassiveEffect())
        {
            passif_effect.gameObject.SetActive(false);
            return;
        }

        passif_effect.gameObject.SetActive(true);

        Effect e = ((Monster)entity).Passive_Effect;

        passif_effect.Initialize(e, e.img, e.title, this);
    }

    public override void Selectionnate()
    {
        base.Selectionnate();

        if (ItsPlayer)
            InstantiateEffect();
    }

    public GameObject effectPrefab;

    public Transform effectTransform;

    public Vector2 AnchoredStart;

    public float separation;

    private List<Effect> effects = new List<Effect>();

    [HideInInspector]
    public int Effect_XDistanceMax = 0;

    public void InstantiateEffect()
    {
        //destroy all precedent prefab
        while (effectTransform.childCount > 0)
        {
            DestroyImmediate(effectTransform.GetChild(0).gameObject);
        }

        SetPassifEffect();

        int x = 0;
        int y = 0;
        int ymax = 5;

        bool passiveElement = ShouldHavePassiveEffect();

        int limite = entity.IsPlayer() ? 0 : 1;

        Effect_XDistanceMax = (entity.GetListShowedEffect().Count > limite) || RelicInit.equipedRelic.Count > 0 ? 1 : 0;

        if (entity.IsPlayer())
        {
            foreach (RelicInit.relicLs r in RelicInit.equipedRelic)
            {
                if (y == ymax)
                {
                    y = 0;
                    x++;
                    Effect_XDistanceMax++;
                }

                RectTransform rect = Instantiate(effectPrefab, effectTransform).GetComponent<RectTransform>();

                rect.gameObject.GetComponent<Panel_character_effect>().Initialize(r, this);

                rect.anchoredPosition = CalcEffectPos(x, y);

                y++;
            }
        }

        foreach (Effect e in effects)
        {
            if (passiveElement)
            {
                passiveElement = false;
                continue;
            }

            if (y == ymax)
            {
                y = 0;
                x++;
                Effect_XDistanceMax++;
            }

            RectTransform rect = Instantiate(effectPrefab, effectTransform).GetComponent<RectTransform>();

            rect.gameObject.GetComponent<Panel_character_effect>().Initialize(e, e.img, e.title, this);

            rect.anchoredPosition = CalcEffectPos(x, y);

            y++;

        }
    }

    public Vector2 CalcEffectPos(int x, int y)
    {
        return AnchoredStart + new Vector2(x * separation, y * -separation);
    }

    public static void UpdateCharacterEffect(Entity target)
    {
        foreach (Window window in WindowInfo.Instance.listActiveWindow)
        {
            if (window is Window_character character && character.entity == target)
            {
                character.InstantiateEffect();
                break;
            }
        }
    }
}