using PathFindingName;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MonsterBehavior : MonoBehaviour
{
    public delegate bool condition(Entity target, Entity origin);

    public static (bool find, Entity closest) ChooseClosestAllies(Entity origin, condition Condition)
    {
        (bool find, Entity closest) v = (false, null);

        int closestPath = int.MaxValue;

        foreach (Entity target in EntityOrder.list)
        {
            if (target == origin || target.IsMonster() != origin.IsMonster() || !Condition(target, origin))
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
