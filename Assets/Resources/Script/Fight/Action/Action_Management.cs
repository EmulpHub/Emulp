using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Action : MonoBehaviour
{
    /// <summary>
    /// A list of all action that need to be executed currently
    /// </summary>
    public static List<Action> toDo = new List<Action>();

    public static List<Action> ActionRunning = new List<Action>();

    /// <summary>
    /// Manage action (execute the one that must be executed and  remove it from list when it's been executed)
    /// </summary>
    public static void Management()
    {
        //If there is current action to do
        if (ActionRunning.Count > 0)
        {
            Action currentAction = ActionRunning[0];

            //If the action hasn't been executed do so
            if ((!currentAction.Executed || (currentAction.type == Action.Type.wait || currentAction.type == Type.wait_fixed)) && !currentAction.Finished())
            {                //Apply the action
                currentAction.Execute();
            }
            else if (currentAction.Finished()) //IF the action is finished
            {
                foreach (Entity e in AliveEntity.list)
                {
                    if (e == null)
                        continue;

                    e.SetPosition(F.ConvertToStringDependingOfGrid(e.transform.position));
                }

                //Remove it to the actionToDo List and go to next actionToDo
                toDo.Remove(currentAction);
                ActionRunning.Remove(currentAction);
                //}
            }
        }
        else
        {
            if (toDo.Count > 0)
            {
                ActionRunning.Add(toDo[0]);
            }
            else if (EntityOrder.IsTurnOf_Player() && V.game_state_action == V.State_action.movement && !CTInfo.Instance.ExistType(CT.Type.movement)
                && V.game_state == V.State.fight)
            {
                Scene_Main.SetGameAction_movement();
            }
        }

        //If there is current actionToDo
        /*if (toDo.Count > 0)
        {
            //Get acces to the currentAction
            Action currentAction = toDo[0];

            //If the action hasn't been executed do so
            if ((!currentAction.Executed || currentAction.type == Action.Type.wait) && !currentAction.CheckIfActionIsFinished())
            {
                //Apply the action
                currentAction.ApplyAction();
            }
            else if (currentAction.CheckIfActionIsFinished()) //IF the action is finished
            {
                foreach (Entity e in V.aliveEntity)
                {
                    if (e == null)
                        continue;

                    e.SetCurrentPosition();
                    //e.CheckIfDead(V.player_entity);
                }

                //Remove it to the actionToDo List and go to next actionToDo
                toDo.Remove(currentAction);
            }
        }
        else
        {
            if (EntityOrder.IsTurnOf_Player() && V.game_state_action == V.State_action.movement && !Combat_Tile_Gestion.ExistType(Combat_tile.TypeOfCombatTile.movement))
            {
                Scene_Main.SetGameAction_movement();
            }
        }*/
    }

    /// <summary>
    /// Clear all action contain in toDo
    /// </summary>
    public static void Clear()
    {
        int i = 0;
        while (i < toDo.Count)
        {
            if (toDo[i].CanBeErased)
            {
                toDo.RemoveAt(i);

            }
            else
            {

                i++;
            }

        }

    }

    public static void Remove(Type typeToRemove)
    {
        int i = -1;

        if (Contain(typeToRemove, out i))
            toDo[i].Stop();
    }

    /// <summary>
    /// Check if a certain type of action exist in ActionToDoList
    /// </summary>
    /// <param name="IsContainType">What type of action we want to check</param>
    /// <returns></returns>
    public static bool Contain(Type IsContainType, out int index)
    {
        foreach (Action action in toDo)
        {
            if (action.type == IsContainType)
            {
                index = toDo.IndexOf(action);

                return true;
            }
        }

        index = -1;

        return false;
    }

    public static bool Contain(Type IsContainType)
    {
        int i = 0;

        return Contain(IsContainType, out i);
    }
}
