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

    protected override IEnumerator Execute_main()
    {
        entity.MoveTo(pathParam);
        yield return null;
    }

    public override bool IsFinished()
    {
        return !entity.runningInfo.running && base.IsFinished();
    }

    public static void Add(PathParam pathParam, Entity entity)
    {
        ActionManager.Instance.AddToDo(Create(pathParam, entity));
    }

    public static Action_movement Create (PathParam pathParam,Entity entity)
    {
        return new Action_movement(pathParam, entity);
    }

    public override string debug()
    {
        return descColor.convert("move for "+entity.Info.EntityName);
    }
}

/*
 * 
        foreach (Action_movement action in GetListOfToDoMovement(entity))
        {
            if (ActionManager.Instance.listOfToDoAction.IndexOf(action) == 0)
                //A movement is already execute so don't add another
                return;
            else
                //Remove the supposed movement the entity should have done
                ActionManager.Instance.listOfToDoAction.Remove(action);
        }


    public static List<Action_movement> GetListOfToDoMovement(Entity target)
    {
        List<Action_movement> ls = new List<Action_movement>();

        foreach (Action action in ActionManager.Instance.listOfToDoAction)
        {
            if (action.type == Type.movement && ((Action_movement)action).entity == target)
                ls.Add((Action_movement)action);
        }

        return ls;
    }

 */