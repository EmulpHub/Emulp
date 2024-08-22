using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EntityInfo : MonoBehaviour
{
    public void CalcTackle()
    {
        if (V.game_state == V.State.passive)
            return;

        int x = holder.CurrentPosition.x, y = holder.CurrentPosition.y;

        float effect = -leak;

        if (holder.ContainEffect(Effect.effectType.untacklable))
            effect = 0;
        else
        {
            List<string> neighbour = new List<string>
            {
                F.ConvertToString(0 + x ,1 + y),
                F.ConvertToString(0 + x ,-1 +y),
                F.ConvertToString(1 +x ,0 +y),
                F.ConvertToString(-1 +x ,0 +y)
            };

            foreach (string pos in neighbour)
            {
                var entity = AliveEntity.GetEntityByPos(pos);

                if (entity != null)
                    effect += CalcTackleEffect(holder, entity);
            }
        }

        effect = Mathf.Clamp(Mathf.CeilToInt(effect / 10), 0, 100);

        bool Contain = holder.ContainEffect(Effect.effectType.tackle);

        if (Contain)
        {
            if (effect == 0)
                holder.RemoveEffect(Effect.effectType.tackle);
            else
                holder.GetEffect(Effect.effectType.tackle).SetStrenght((int)effect);
        }
        else if (!Contain && effect > 0)
        {
            holder.AddEffect(
                Effect.CreateEffect(Effect.effectType.tackle, (int)effect, 0, null, Effect.Reduction_mode.never)
                , false);
        }
    }

    public static int CalcTackleEffect(Entity tackler, Entity tacleur)
    {
        if (F.IsEnnemy(tackler, tacleur)) return 0;

        return Mathf.Clamp(Mathf.CeilToInt((tackler.Info.leak - tacleur.Info.tackle) / 10), 0, 100);
    }
}
