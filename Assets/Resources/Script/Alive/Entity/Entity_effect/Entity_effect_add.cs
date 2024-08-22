using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Entity : MonoBehaviour
{
    public void AddToListEffect(Effect effect)
    {
        listEffect.Add(effect);

        effect.event_kill.Add((Effect e) =>
        {
            int i = 0;
            while (i < listEffect.Count)
            {
                Effect ab = listEffect[i];

                if (ab.id == e.id)
                {
                    listEffect.RemoveAt(i);
                    break;
                }

                i++;
            }
        });
    }

    public Effect AddEffect(Effect effectToAdd, bool visible = true)
    {
        effectToAdd.eventAdding(this);

        if (TryCombine(effectToAdd, visible, out Effect result))
            return result;

        effectToAdd.holder = this;

        AddToListEffect(effectToAdd);

        if (IsPlayer() && visible)
        {
            Combat_Effect e_combat = Instantiate(Resources.Load<GameObject>("Prefab/Combat_Effect"), V.script_Scene_Main.effect_parent.transform).GetComponent<Combat_Effect>();

            e_combat.Initialize(effectToAdd);
        }

        Info.CalculateValue();

        Window_character.UpdateCharacterEffect(this);

        return effectToAdd;
    }

    private bool TryCombine(Effect toCombine, bool visible, out Effect result)
    {
        result = null;

        if (toCombine.type == Effect.effectType.custom || toCombine.type == Effect.effectType.customTxt || !visible) return false;

        foreach (Effect holderEffect in listEffect)
        {
            if (holderEffect.type != toCombine.type) continue;

            if (holderEffect.ShouldNeverExhaust != holderEffect.ShouldNeverExhaust) continue;

            if (!holderEffect.ShouldNeverExhaust && holderEffect.DurationInTurn != toCombine.DurationInTurn) continue;

            if (holderEffect.reduction_Mode != toCombine.reduction_Mode) continue;

            if (holderEffect.img != toCombine.img) continue;

            if (holderEffect.title != toCombine.title) continue;

            holderEffect.AddStr(toCombine.Str);

            holderEffect.event_kill.Combine(toCombine.event_kill);

            Info.CalculateValue();

            Window_character.UpdateCharacterEffect(this);

            result = holderEffect;

            return true;
        }

        return false;
    }
}
