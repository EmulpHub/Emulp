using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster : Entity
{
    public IEnumerator CastASpell(SpellStats TargetSpell, Entity Target, string targetPosition)
    {
        CTInfo.Instance.ListTile_Clear();

        if (!TargetSpell.Launchable(Target))
        {
            Action_nextTurn.Add(this);

            yield break;
        }

        Spell CurrentSpell = Spell.Create(TargetSpell.spell_type);

        while (TargetSpell.Launchable(Target))
        {
            if (Target != null && Target == V.player_entity && V.player_info.spike == 0)
                Action_wait.Add(0.1f);
            else
                Action_wait.Add(0.2f);

            Action_spell_info info = new Action_spell_info();

            info.spell = CurrentSpell;
            info.caster = this;
            info.listTarget = new List<Entity>() { Target };
            info.targetedSquare = targetPosition;

            Action_spell.Add(info);

            TargetSpell.SetUse();

            while (Action.toDo.Count > 0)
            {
                yield return new WaitForEndOfFrame();
            }
        }

        Destroy(CurrentSpell.gameObject, 2);

        StartWhenNoAction(currentCoroutine_name, 0.25f);

        yield return new WaitForSeconds(0);
    }

    public IEnumerator CastASpell(SpellStats TargetSpell, Entity Target)
    {
        StartCoroutine(CastASpell(TargetSpell, Target, Target.CurrentPosition_string));

        yield return new WaitForEndOfFrame();
    }

}
