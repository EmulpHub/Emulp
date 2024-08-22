using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Effect : MonoBehaviour
{
    public bool IsDead()
    {
        return durationInTurn <= 0 && !ShouldNeverExhaust;
    }

    public void CheckIfDeadAndKill()
    {
        if (!IsDead()) return;

        Kill(false);
    }

    public void Kill(bool force)
    {
        if (reduction_Mode == Reduction_mode.permanent && !force)
            return;

        whenKill();

        eventKilling();

        Window_character.UpdateCharacterEffect(holder);

        V.player_info.CalculateValue();

        EndlessAnim_eraseAll();
    }

    internal virtual void whenKill()
    {

    }
}
