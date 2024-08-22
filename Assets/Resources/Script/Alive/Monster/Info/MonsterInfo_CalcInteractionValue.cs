using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MonsterInfo : EntityInfo
{
    public override float CalcDamage(float dmg)
    {
        float nb = base.CalcDamage(dmg);

        nb *= 1 + str / 100;

        return nb;
    }
    public override float CalcHeal(float heal)
    {
        float nb = base.CalcHeal(heal);

        nb *= 1 + str / 100;

        return nb;

    }

}
