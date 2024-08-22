using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_archer_attack : spellEffect_Monster
{
    public override void InfoBool()
    {
        SetBoolInfo(true, true);
    }

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        Sprite arrow = Resources.Load<Sprite>("Image/Monster/Spell/arrow");

        if (TargetForDamage(target))
        {
            float speed = F.Projectile_CalculateTravelTime(caster, target);

            caster.Animation_DealDamage(targetedSquareWorldVector2);

            StartCoroutine(spellHolder.Anim_Projectile(caster, arrow, V.CalcEntityDistanceToBody(target), false, speed));

            SoundManager.PlaySound(SoundManager.list.spell_monster_archer_arrow_launch);

            yield return new WaitForSeconds(speed);

            SoundManager.PlaySound(SoundManager.list.spell_monster_archer_arrow_impact);

            target.Damage(new InfoDamage(calcDamage(Random.Range(7, 9)), caster));
        }
    }
}

public class monster_archer_sprint : spellEffect_Monster
{

    public override void InfoBool()
    {
        SetBoolInfo(false, true);
    }

    public override IEnumerator Effect_before()
    {
        Sprite sprint_sprite = Resources.Load<Sprite>("Image/Monster/Spell/archer_sprint");

        Vector2 posAnim = V.CalcEntityDistanceToBody(caster);

        StartCoroutine(spellHolder.Anim_PopUpBig(sprint_sprite, posAnim));

        SoundManager.PlaySound(SoundManager.list.spell_monster_archer_pm);

        yield return new WaitForSeconds(0.3f);

        caster.Add_pm(calc(2));

        spellHolder.CreateParticle_Impact_Entering(posAnim, 1.2f, Spell.Particle_Amount._20, Spell.Particle_Color.green);
    }
}