using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFindingName;

public class Action_movement : Action
{
    public Action_movement(PathParam pathParam, Entity entity) : base()
    {
        type = Type.movement;

        this.pathParam = pathParam;
        this.entity = entity;
    }

    public PathParam pathParam;
    public Entity entity;

    internal override void Execute_main()
    {


        entity.MoveTo(pathParam);
    }

    public override bool Finished()
    {
        if (base.Finished()) return true;

        return !entity.runningInfo.isRunning && Executed;
    }

    public static List<Action_movement> getToDoMovement()
    {
        List<Action_movement> ls = new List<Action_movement>();

        foreach (Action action in toDo)
        {
            if (action.type == Type.movement)
                ls.Add((Action_movement)action);
        }

        return ls;
    }

    public static void Add(PathParam pathParam, Entity entity)
    {
        foreach (Action_movement action in getToDoMovement())
        {
            if (action.entity == entity)
            {
                if (toDo.IndexOf(action) == 0)
                    //A movement is already execute so don't add another
                    return;
                else
                    //Remove the supposed movement the entity should have done
                    toDo.Remove(action);
            }
        }
;

        //The new actionToAdd to the list
        Action_movement actionToAdd = new Action_movement(pathParam, entity);

        //Add it to the list
        toDo.Add(actionToAdd);
    }

    public override string debug()
    {

        return descColor.convert("move for entity.Info.EntityName");
    }
}
