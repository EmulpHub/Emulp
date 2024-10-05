using PathFindingName;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class MonsterBehavior : MonoBehaviour
{
    public delegate bool condition(Entity target, Entity origin);

    public static (bool find, Entity closest) ChooseClosestAllies(Entity origin, condition Condition)
    {
        (bool find, Entity closest) v = (false, null);

        int closestPath = int.MaxValue;
        
        List<Entity> list = new List<Entity>();

        if (origin is Monster)
            list = EntityOrder.InstanceEnnemy.listToEntity;
        else
            list = EntityOrder.InstanceAlly.list;

        foreach (Entity target in list)
        {
            if (target == origin || !Condition(target, origin))
                continue;

            var pathParam = new PathParam(origin.CurrentPosition_string, target.CurrentPosition_string).AddListIgnoreEntity(target);

            List<string> path = Path.Find(pathParam).path;

            if (path.Count < closestPath)
            {
                v.find = true;
                v.closest = target;
                closestPath = path.Count;
            }
        }

        return v;
    }

}
