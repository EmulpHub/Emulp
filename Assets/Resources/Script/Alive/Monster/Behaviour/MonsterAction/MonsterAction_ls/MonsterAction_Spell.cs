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

        if (target != null)
            targetPosition = target.CurrentPosition_string;
    }

    public override bool Condition()
    {
        SetTarget();

        if (target == null) return false;

        bool spellLaunchable = spell.Launchable(target);

        return spellLaunchable;
    }

    protected override IEnumerator Execution(MonsterBehaviorResult result)
    {
        if (targetPosition == "")
            targetPosition = target.CurrentPosition_string;

        TileInfo.Instance.ListTile_Clear();

        bool spellLaunchable = spell.Launchable(target);

        if (spellLaunchable)
        {
            Spell CurrentSpell = Spell.Create(spell.spell_type);

            Action_spell_info spellInfo = new Action_spell_info(CurrentSpell, info.monster, target, targetPosition);

            result.SetAction(Action_spell.Create(spellInfo));

            spell.SetUse();
        }

        yield break;
    }
}
