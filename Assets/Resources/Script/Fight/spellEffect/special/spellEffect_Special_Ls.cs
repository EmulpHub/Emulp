using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike : spellEffect_Special
{
    public static float delay = 0.1f;

    public override void InfoBool()
    {
        SetBoolInfo(false, false);
    }

    public static float dmgEffect = 1;

    public override IEnumerator Effect_Target(Entity target, string pos)
    {
        if (target != null)
        {
            CustomEffect_EndlessSpike.AttackAnimation(target);

            float dmg = V.player_info.CalculateSpikeDamage();

            dmg = Mathf.Round(dmg * dmgEffect);

            for (int i = 0; i < V.player_info.spike; i++)
            {
                target.Damage(new InfoDamage(dmg, caster));

                if (i + 1 < V.player_info.spike)
                    yield return new WaitForSeconds(0.1f);
            }

            dmgEffect = 1;
        }

        yield return new WaitForEndOfFrame();
    }
}
