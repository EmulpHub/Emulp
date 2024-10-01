using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EntityByPos : MonoBehaviour
{
    public static Entity TryGet(string pos)
    {
        bool condition(Entity entity)
        {
            return entity.CurrentPosition_string == pos;
        };

        return AliveEntity.Instance.GetFirstEntity(condition);
    }
}
