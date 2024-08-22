using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEndlessStatic : MonoBehaviour
{
    public enum FollowMode { smooth, instant }

    public enum ID
    {
        Render,
        Particle,
        Pix,
        IdleAnimation
    }

    public static GameObject render, particle, pixTxt,IdleAnimation;
    public static Dictionary<ID, List<AnimEndless>> list = new Dictionary<ID, List<AnimEndless>>();

    public static void createSprite()
    {
        render = Resources.Load<GameObject>("Prefab/Animation/EndlessAnimation/Render");
        particle = Resources.Load<GameObject>("Prefab/Animation/EndlessAnimation/article");
        pixTxt = Resources.Load<GameObject>("Prefab/Animation/EndlessAnimation/PixTxt");
        IdleAnimation = Resources.Load<GameObject>("Prefab/Animation/EndlessAnimation/IdleAnimation");
    }


    public static bool list_Contain(string specialId, Entity match = null)
    {
        foreach (List<AnimEndless> a in list.Values)
        {
            foreach (AnimEndless b in a)
            {
                bool result = false;

                if (b.specialId == specialId)
                    result = true;

                if (match != null)
                {
                    result = b.target == match;
                }

                if (result)
                    return true;
            }
        }

        return false;
    }

    public static AnimEndless list_Get(string specialId, Entity SpecificTarget = null)
    {
        foreach (List<AnimEndless> a in list.Values)
        {
            foreach (AnimEndless b in a)
            {
                if (b.specialId == specialId && (SpecificTarget == null || b.target == SpecificTarget))
                    return b;
            }
        }

        return null;
    }


    public static int list_add(AnimEndless toAdd)
    {
        ID cur = toAdd.CurrentId;

        if (list.ContainsKey(cur))
        {
            list[cur].Add(toAdd);
            return list[cur].Count - 1;
        }
        else
        {
            list.Add(cur, new List<AnimEndless> { toAdd });
            return 0;
        }
    }

    public static void list_erase(AnimEndless anim)
    {
        if (list.ContainsKey(anim.CurrentId))
        {
            List<AnimEndless> l = list[anim.CurrentId];

            if (l.Contains(anim))
            {
                l.Remove(anim);

                if (l.Count == 0)
                {
                    list.Remove(anim.CurrentId);
                }
            }
        }

    }

    public static int getSameEndlesssAnimationCount(AnimEndless anim)
    {
        return getSameEndlesssAnimationCount(anim,new List<ID> {anim.CurrentId });
    }

    public static int getSameEndlesssAnimationCount(AnimEndless anim,List<ID> listId)
    {
        bool contain = false;

        foreach(ID id in listId)
        {
            if (list.ContainsKey(id)) { contain = true; break; }
        }

        if (!contain) return 0; 

        int nb = 0;

        foreach (ID id in listId)
        {
            List<AnimEndless> l = list[anim.CurrentId];

            foreach (AnimEndless end in l)
            {
                if (end.target == anim.target && end != anim)
                    nb++;
            }
        }

        return nb;
    }
}
