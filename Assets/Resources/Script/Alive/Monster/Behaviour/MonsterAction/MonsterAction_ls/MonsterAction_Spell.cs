using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAction_Spell : MonsterAction
{
    SpellStats spell;

    public MonsterAction_Spell(SpellStats spell, PriorityLayer layer, int priority) : base(layer, priority)
    {
        this.spell = spell;
    }

    Entity target;
    string targetPosition;

    private void SetTarget()
    {
        target = monsterBehavior.DecideWhoToAttack();

        if(target != null)
            targetPosition = target.CurrentPosition_string;
    }

    public override bool Condition()
    {
        SetTarget();

        if(target == null) return false;

        bool spellLaunchable = spell.Launchable(target);

        return spellLaunchable;
    }

    public override IEnumerator Execution()
    {
        if (targetPosition == "")
            targetPosition = target.CurrentPosition_string;

        TileInfo.Instance.ListTile_Clear();

        bool spellLaunchable = spell.Launchable(target);

        Spell CurrentSpell = Spell.Create(spell.spell_type);

        while (spellLaunchable)
        {
            Action_waitFixed.Add(0.4f);

            Action_spell_info spellInfo = new Action_spell_info(CurrentSpell, info.monster, target, targetPosition);

            Action_spell.Add(spellInfo);

            spell.SetUse();

            spellLaunchable = spell.Launchable(target);

            yield return new WaitUntil(() => !ActionManager.Instance.Running());
        }

        Destroy(CurrentSpell.gameObject, 2);

        yield break;
    }
}
