using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MonsterBehavior : MonoBehaviour
{
    public Entity DecideWhoToAttack()
    {
        Entity target = null;
        float minValue = float.MaxValue;

        void Traveler(Entity entity)
        {
            if (F.IsEnnemy(entity, Info.monster))
            {
                float entityValue = entity.Info.Life * (F.DistanceBetweenTwoPos(entity, Info.monster) * 0.3f + 1);

                if (minValue > entityValue)
                {
                    target = entity;
                    minValue = entityValue;
                }
            }
        }

        AliveEntity.Instance.TravelEntity(Traveler);

        if (target == null) throw new System.Exception("target can't be null");

        return target;
    }
}
