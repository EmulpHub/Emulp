using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Spell : MonoBehaviour
{
    public GameObject glowContour, glowParticle;

    public void SetGlowEffect()
    {
        bool Particle = InCombatCase();

        glowContour.gameObject.SetActive(DefineIfGlowEffectIsActive() || Particle);

        glowParticle.gameObject.SetActive(InCombatCase());
    }

    public bool DefineIfGlowEffectIsActive()
    {
        if (ContourIsNotNormalColor)
            return false;

        return NormalCase();
    }

    public bool NormalCase()
    {
        return (SpellGestion.selectionnedSpell_list != spell && SpellGestion.selectionnedSpell_list != SpellGestion.List.empty)
            && (SpellGestion.selectionnedSpell_isEditing || V.game_state == V.State.passive || V.game_state == V.State.positionning) &&
            (this != Object && this != Object_2) && (this != Weapon || !SpellGestion.AddingANewSpell);
    }

    public bool InCombatCase()
    {
        return (V.game_state == V.State.fight && FreePaUse > 0 && IsOffCooldown());
    }
}
