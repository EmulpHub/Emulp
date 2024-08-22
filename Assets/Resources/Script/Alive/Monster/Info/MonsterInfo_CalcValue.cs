using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MonsterInfo : EntityInfo
{
    public override float Set_LifeMax_add()
    {
        float nb = 50;

        nb += base.Set_LifeMax_add();

        nb += 9 * level;

        return nb;
    }

    public override void Set_LifeMax_Multiply(ref float nb)
    {
        base.Set_LifeMax_Multiply(ref nb);

        nb *= (level - 1) / 20 + 1;

        nb *= Ascension.GetAscensionParameter(Ascension.ModifierType.monster_life) / 100;
    }

    public override int Set_PaMax_add()
    {
        int nb = base.Set_PaMax_add();

        nb += level / 4;

        return nb;
    }

    public override void Set_PaMax_Multiply(ref float nb)
    {
        if (V.administrator && V.script_Scene_Main_Administrator is not null)
        {
            if (V.script_Scene_Main_Administrator.ForcePa_Monster != 0)
                nb = V.script_Scene_Main_Administrator.ForcePa_Monster;
        }
    }

    public override int Set_PmMax_add()
    {
        int nb = base.Set_PmMax_add();

        nb += level / 7;

        return nb;
    }

    public override void Set_PmMax_Multiply(ref int nb)
    {
        if (V.administrator && V.script_Scene_Main_Administrator is not null)
        {
            if (V.script_Scene_Main_Administrator.ForcePa_Monster != 0)
                nb = V.script_Scene_Main_Administrator.ForcePm_Monster;
        }
    }

    public override int Set_Tackle_add()
    {
        int nb = 5;

        nb += 2 * level;

        nb += base.Set_Tackle_add();

        return nb;
    }

    public override float Set_Leak_add()
    {
        float nb = 5;

        nb += level;

        nb += base.Set_Leak_add();

        return nb;
    }
}
