using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Spell : MonoBehaviour
{
    [HideInInspector]
    public bool EndOfCast = true;

    public static Spell lastSpellLaunch;


    /// <summary>
    /// Cast the spell (first check all condition)
    /// </summary>
    /// <param name="caster">the caster of the spell</param>
    /// <param name="targetPosition">the position of the target</param>
    public void CastSpell(Action_spell_info info)//Entity caster, List<Entity> targets, string targetPosition, bool DontApplyUse, bool Force, bool main, string OriginalPosition)
    {
        EndOfCast = false;

        if (info.caster != null)
        {
            StateBeforeLaunch state = CanBeLaunched(info);

            if (state == StateBeforeLaunch.Ready)
            {
                StartCoroutine(CastSpell_effect(info));

                FreePaUse--;
            }
            else
            {
                ErrorTxt.DisplayError(state);

                EndOfCast = true;
            }
        }
    }

    public enum StateBeforeLaunch { Ready, lackOfPa, cooldown, distanceNotGood, notInGoodLineOfView }

    public StateBeforeLaunch CanBeLaunched(Action_spell_info info)
    {
        if (info.forceLaunch)
            return StateBeforeLaunch.Ready;

        Entity target = info.target;

        string targetPos = info.targetedSquare;

        int distance = 999;
        if (target == info.caster || range_max == 0)
            distance = 0;
        else if (target != null)
        {
            distance = F.DistanceBetweenTwoPos(info.caster.CurrentPosition_string, target.CurrentPosition_string);
            targetPos = target.CurrentPosition_string;
        }
        else
            distance = F.DistanceBetweenTwoPos(info.caster.CurrentPosition_string, info.targetedSquare);

        if (!IsOffCooldown() && !info.dontUseCost)
            return StateBeforeLaunch.cooldown;


        return StateForLaunch(distance, info.caster.Info.PA, targetPos, info.caster.CurrentPosition_string);
    }


    public virtual IEnumerator CastSpell_effect(Action_spell_info info)
    {
        //Cast the spell
        yield return new WaitForSeconds(0);
    }

    [HideInInspector]
    public bool ignoreCD = false;

    /// <summary>
    /// Is this spell is not on cooldown
    /// </summary>
    /// <returns></returns>
    public bool IsOffCooldown()
    {
        return EntityOrder.Instance.id_turn >= id_nextPossibleReUse || ignoreCD;
    }

    /// <summary>
    /// The number of use for this turn
    /// </summary>
    public int currentUse = 0;

    /// <summary>
    /// Mean we cast it one time
    /// </summary>
    public void SetUse()
    {
        /*Debug.Log("MaxUse = " + maxUse);

        if (maxUse == -99)
        {
            maxUse = SpellGestion.Get_Cd(spell);

            Debug.Log("Set max use with new = " + maxUse);
        }

        Debug.Log("CurrentUse befoir adding = " + currentUse + " max use = " + maxUse);*/

        currentUse++;

        if (currentUse >= Mathf.Abs(cd))
        {
            id_nextPossibleReUse_max = 1;
            id_nextPossibleReUse = EntityOrder.Instance.id_turn + 1;
        }
    }

    /// <summary>
    /// Set the cooldown
    /// </summary>
    /// <param name="cd">The specified cooldown</param>
    public void SetCooldown(int cd)
    {
        id_nextPossibleReUse_max = cd;
        id_nextPossibleReUse = EntityOrder.Instance.id_turn + cd;

        if (cd == 0)
            Turn_reset();
    }

    /// <summary>
    /// Is this spell finish
    /// </summary>
    /// <returns></returns>
    public bool IsFinish()
    {
        return EndOfCast;
    }

    /// <summary>
    /// Reset the number of use
    /// </summary>
    public void Turn_reset()
    {
        currentUse = 0;
    }

    /// <summary>
    /// Is this spell is launchable (pa and cooldown)
    /// </summary>
    /// <returns></returns>
    public bool IsEnoughRessourceForLaunch()
    {
        return V.player_info.PA >= Get_pa_cost() && IsOffCooldown() && !IsEmpty();
    }

    public StateBeforeLaunch StateForLaunch(int distance, int pa, string target, string caster)
    {
        bool debugMode = false;

        bool PA = IsAbleToLaunch_Pa(pa);

        bool Distance = (distance == 0 || range_Type == Range_type.noNeedOfDistance) || (distance <= range_max && range_min <= distance);

        bool LineOfView = range_Type == Range_type.noNeedOfLineOfView || F.IsLineOfView(target, caster);

        if (debugMode)
            print("LineOfView = " + LineOfView + " distance = " + Distance + " Pa = " + PA + " result = " + (PA && Distance && LineOfView));

        if (!PA)
            return StateBeforeLaunch.lackOfPa;

        if (!Distance)
            return StateBeforeLaunch.distanceNotGood;

        if (!LineOfView)
            return StateBeforeLaunch.notInGoodLineOfView;

        return StateBeforeLaunch.Ready;

        //return pa >= pa_cost && (target == caster || distance == 0 || range_Type == Range_type.noNeed || (F.IsLineOfView(target, caster) && distance <= range_max && range_min <= distance));
    }

    [HideInInspector]
    private int FreePaUse = 0, FreePaUse_Turn_Id = 0;

    public bool IsAbleToLaunch_Pa(int pa)
    {
        return pa >= Get_pa_cost();
    }

    public void AddFreePaUse(int nb)
    {
        FreePaUse_Turn_Id = EntityOrder.Instance.id_turn;
        FreePaUse = nb;
    }

    public void UpdateFreePaUse()
    {
        if (FreePaUse_Turn_Id != EntityOrder.Instance.id_turn)
            FreePaUse = 0;
    }

    public int Get_pa_cost()
    {
        if (FreePaUse > 0)
        {
            return 0;
        }

        return pa_cost;
    }

    [HideInInspector]
    public float delayBeforeClickAgain, delayBeforeClickAgain_max = 0.1f;

    /// <summary>
    /// When we use the spell
    /// </summary>
    public void UseSpell()
    {

        if (delayBeforeClickAgain > 0 || !ClickAutorization.Autorized(this.gameObject))
        {
            return;
        }

        delayBeforeClickAgain = delayBeforeClickAgain_max;

        MakeAnimation(TypeOfAnimation.click);

        if (AllowToMovedOnClick())
        {
            return;
        }

        if (!EntityOrder.Instance.IsTurnOf_Player())
        {
            //Not player turn
            ErrorTxt.DisplayError(
                V.IsFr() ?
                "Ce n'est pas votre tour"
                :
                "It's not your turn"
                );


            return;
        }

        if (!IsOffCooldown())
        {
            //Est en cooldown
            ErrorTxt.DisplayError(
                V.IsFr() ?
                "Ce sort est en cours de chargement"
                :
                "This spell is on cooldown"
                );


            return;
        }

        if (!IsAbleToLaunch_Pa(V.player_info.PA))
        {
            //Manque de pa
            ErrorTxt.DisplayError(
                V.IsFr() ?
                "Vous manquez de pa pour lancer ce sort"
                :
                "You lack ap to launch this spell"
                );
            return;
        }

        if (V.game_state == V.State.fight && !IsEmpty()
             &&
            !V.player_entity.IsInMovementInFight())
        {

            if (V.game_state_action != V.State_action.spell /*|| SpellGestion.selectionnedSpell != this*/)
            {
                SetGameAction_spell();
            }
            else
            {
                Scene_Main.SetGameAction_movement();
            }
        }

        /*else if (EntityOrder.IsTurnOf_Player())
        {
            //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            print("Passing to movement");

            Combat_spell.SetGameAction_movement();
        }*/
    }
}
