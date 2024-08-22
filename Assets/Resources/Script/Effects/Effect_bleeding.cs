using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Effect : MonoBehaviour
{
    /*
    public static Effect AddBleeding(Entity e, float str, bool display = true)
    {
        str = calcBleedingApplication(str);

        str *= 1 + (float)e.CollectStr(effectType.focus) / 100;

        str = Mathf.RoundToInt(str);

        if (display)
            Main_UI.Display_movingText_basicValue("+" + str, V.Color.red, e.transform.position, V.pix_bleeding);

        e.RemoveEffect(effectType.focus, true);

        Effect eToReturn = e.AddEffect(Effect.CreateEffect(effectType.bleeding, Mathf.RoundToInt(str), 0, V.effect_bleeding, "", Reduction_mode.never));

        AnimEndless endless = AnimEndlessStatic.list_Get("Bleeding");

        if (endless != null)
        {
            Animation_Endless_PixTxt.Create(e, V.pix_bleeding, () =>
            {
                return "" + eToReturn.str;
            }).SetSpecialId("Bleeding");
        }

        return eToReturn;
    }

    public static bool ContainBleeding(Entity e)
    {
        return e.ContainPower(effectType.bleeding);
    }

    public static void ApplyBleeding(Entity e, float multiplier = 1)
    {
        (bool find, Effect e) v = e.GetEffect_byType(effectType.bleeding);

        if (v.find)
        {
            float d = (int)(v.e.str * multiplier);

            e.Damage(d, V.player_entity, true, "", null, false);

            Combat_spell.Particle_Amount a = Combat_spell.Particle_Amount._4;

            if (v.e.Strenght >= 50)
                a = Combat_spell.Particle_Amount._24;
            else if (v.e.Strenght >= 30)
                a = Combat_spell.Particle_Amount._16;
            else if (v.e.str >= 10)
                a = Combat_spell.Particle_Amount._8;

            Combat_spell.Reference.CreateParticle_Impact_Blood(e.transform.position, 1.1f, a);

            Combat_spell.Reference.StartCoroutine(Combat_spell.Reference.Anim_PopUpBig(V.effect_bleeding, V.CalcEntityDistanceToBody(e), 1.2f, 0.6f));

            event_player_applyBleeding();
        }
    }

    public static int calcBleedingApplication(float str)
    {
        str *= 1 + (float)Effect.Get_defense() / 100;


        return Mathf.RoundToInt(str);
    }
    */
}
