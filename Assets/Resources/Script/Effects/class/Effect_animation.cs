using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Effect : MonoBehaviour
{
    private List<AnimEndless> listEndlessAnim = new List<AnimEndless>();

    public void EndlessAnim_add(AnimEndless a)
    {
        listEndlessAnim.Add(a);
    }

    public void EndlessAnim_remove(List<AnimEndless> a)
    {
        listEndlessAnim.AddRange(a);
    }

    public void EndlessAnim_eraseAll ()
    {
        foreach (AnimEndless a in listEndlessAnim)
        {
            a.Erase();
        }
    }

}
