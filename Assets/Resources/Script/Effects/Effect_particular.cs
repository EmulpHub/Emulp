using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Effect : MonoBehaviour
{
    public static string GetRevengeName()
    {
        return V.IsFr() ? "Enervé" : "Angry";
    }

    public static void ApplyRevenge(Entity target, int power_amount, int pa_amount)
    {
        string title = GetRevengeName();

        target.AddEffect(
            Effect.CreateCustomEffect(0, V.revenge, Reduction_mode.never, title, new List<Effect>()
            {
                Effect.CreateEffectForCustom(effectType.power,power_amount),
                Effect.CreateEffectForCustom(effectType.boost_pa,pa_amount)
            })
            );


        Main_UI.Display_movingText_basicValue(title, V.Color.red, target.transform.position, V.pix_angry);

        SoundManager.PlaySound(SoundManager.list.spell_monster_angry);

        if (target.IsMonster())
            ((Monster)target).LoadEyeAngry(V.eye_angry_default, true);
    }
}
