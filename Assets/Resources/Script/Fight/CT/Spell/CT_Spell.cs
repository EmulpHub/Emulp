using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CT_Spell : CT
{
    private static GameObject _prefab;

    private static GameObject Prefab
    {
        get
        {
            if (_prefab == null)
                _prefab = Resources.Load<GameObject>("Prefab/CT/CT_Spell");

            return _prefab;
        }
    }

    public static CT Add(string pos, bool graphic_startAnimation = true)
    {
        CT script = Instantiate(Prefab, parent).GetComponent<CT>();

        script.Initialize(Type.spell, 255, 0, false, false, pos);

        script.graphic_startAnimation = graphic_startAnimation;

        script.customListTile = null;

        CTInfo.Instance.Add(script);

        return script;
    }

    public static bool IsWalkableByTheplayer(int distance)
    {
        return distance <= V.player_info.GetRealPm();
    }

    public override void SetNormalColor()
    {
        if (SpellGestion.Get_NeedLineOfView(SpellGestion.selectionnedSpell_list) &&
            !F.IsLineOfView(pos, V.player_entity.CurrentPosition_string))
            ChangeColor(CT_Gestion.Color.blue_noLine);
        else
            ChangeColor(CT_Gestion.Color.blue);
    }
}
