using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellAnimation : MonoBehaviour
{
    private static AnimationHolder createHolder ()
    {
        return Instantiate(Resources.Load<GameObject>("Prefab/Spell_Animation_2")).GetComponent<AnimationHolder>();
    }

    public static void anim_simple (string animationName,Vector3 position, float scale = 1)
    {
        var anim = createHolder();

        anim.SetPosition(position);

        anim.SetScale(scale);

        anim.MakeAnimation(animationName);
    }


    public static void divineSword(Vector3 position, float scale = 1)
    {
        var anim = createHolder();

        anim.SetPosition(position);

        anim.SetScale(scale);

        anim.MakeAnimation("divineSword");
    }
}
