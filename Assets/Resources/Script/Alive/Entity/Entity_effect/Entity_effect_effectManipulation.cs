using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Entity : MonoBehaviour
{
    public void RemoveEffect(Effect.effectType type, bool removePermanent = false)
    {
        foreach (Effect effect in new List<Effect>(listEffect))
        {
            if (effect.type != type) continue;

            effect.Kill(removePermanent);
        }
    }

    public void RemoveEffect_byTitle(string title, bool removePermanent = false)
    {
        foreach (Effect effect in new List<Effect>(listEffect))
        {
            if (effect.title != title) continue;

            effect.Kill(removePermanent);
        }
    }

    public void RemoveEffect_byCodeName(string codeName, bool removePermanent = false)
    {
        foreach (Effect effect in new List<Effect>(listEffect))
        {
            if (effect.CodeName != codeName) continue;

            effect.Kill(removePermanent);
        }
    }

    public void TurnEffect(bool startTurn)
    {
        Effect.Reduction_mode target = Effect.Reduction_mode.startTurn;
        if (!startTurn) target = Effect.Reduction_mode.endTurn;

        foreach (Effect effect in new List<Effect>(listEffect))
        {
            if (effect.reduction_Mode != target) continue;

            effect.EndTurn();
        }
    }

    public bool ContainEffect(Effect.effectType type)
    {
        foreach (Effect effect in listEffect)
        {
            if (effect.type == type) return true;
        }

        return false;
    }


    public bool ContainEffect_byCodeName(string codeName)
    {
        foreach (Effect effect in listEffect)
        {
            if (effect.CodeName == codeName) return true;
        }

        return false;
    }

    public bool ContainEffect_byTitle(string title)
    {
        foreach (Effect effect in listEffect)
        {
            if (effect.title == title) return true;
        }

        return false;
    }


    public Effect GetEffect_byTitle(string title)
    {
        bool f = false;

        return GetEffect_byTitle(title, ref f);
    }

    public Effect GetEffect_byTitle(string title, ref bool find)
    {
        foreach (Effect e in listEffect)
        {
            if (e.title == title)
            {
                find = true;
                return e;
            }
        }

        find = false;

        return null;
    }

    public Effect GetEffect(Effect.effectType type)
    {
        bool f = false;

        return GetEffect(type, ref f);
    }

    public Effect GetEffect(Effect.effectType type, ref bool find)
    {
        foreach (Effect e in listEffect)
        {
            if (e.type == type)
            {
                find = true;
                return e;
            }
        }

        find = false;

        return null;
    }


    public Effect GetEffect_byCodeName(string codeName, ref bool find)
    {
        foreach (Effect e in listEffect)
        {
            if (e.CodeName == codeName)
            {
                find = true;
                return e;
            }
        }

        find = false;

        return null;
    }

    public Effect GetEffect_byCodeName(string codeName)
    {
        foreach (Effect e in listEffect)
        {
            if (e.CodeName == codeName)
            {
                return e;
            }
        }

        return null;
    }

    [HideInInspector]
    public bool HaveEffect() { return listEffect.Count > 0; }

    public List<Effect> listEffect = new List<Effect>();

    public void ClearEffect(bool force = false)
    {
        foreach (Effect effect in new List<Effect>(listEffect))
        {
            effect.Kill(force);
        }
    }

    public int CollectStr(Effect.effectType type)
    {
        List<Effect.effectType> listTargetType = new List<Effect.effectType> { type };

        return collectStr(listTargetType);
    }

    int collectStr(List<Effect.effectType> listTargetType)
    {
        float nb = 100;

        void Add(Effect e)
        {
            if (e.addMode == Effect.AddMode.addition)
                nb += e.Str;
            else
                nb *= e.Str / 100 + 1;
        }

        foreach (Effect effect in new List<Effect>(listEffect))
        {
            if (effect.type == Effect.effectType.custom)
            {
                Effect_custom custom = (Effect_custom)effect;

                foreach (Effect custom_effect in custom.effectLs)
                {
                    if (listTargetType.Contains(custom_effect.type))
                        Add(custom_effect);
                }
            }
            else if (listTargetType.Contains(effect.type))
                Add(effect);

        }

        return Mathf.RoundToInt(nb - 100);
    }

    public List<Effect> GetListShowedEffect()
    {
        List<Effect> listShowedEffect = new List<Effect>();

        foreach (Effect e in listEffect)
        {
            if (e.unique) listShowedEffect.Add(e);
        }

        return listShowedEffect;
    }
}
