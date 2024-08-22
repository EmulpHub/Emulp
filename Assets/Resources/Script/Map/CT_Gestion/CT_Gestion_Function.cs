using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CT_Gestion : MonoBehaviour
{
    public void Erase(string pos, CT.AnimationErase_type animation)
    {
        CT ct = CTInfo.Instance.Get(pos);

        ct.Erase(animation);

        CTInfo.Instance.Remove(ct);
    }

    public void Erase_WithDelay_All(CT.AnimationErase_type animation, float time)
    {
        foreach (string pos in CTInfo.Instance.listPosCTKeys)
        {
            StartCoroutine(CoErase_WithDelay(pos, animation, time));
        }
    }

    public IEnumerator CoErase_WithDelay(string pos, CT.AnimationErase_type animation, float time)
    {
        CT tile = CTInfo.Instance.Get(pos);

        if (tile == null)
            yield break;

        tile.transform.position -= new Vector3(0, 0, -1);

        CTInfo.Instance.Remove(tile);

        yield return new WaitForSeconds(time);

        tile.Erase(animation);
    }
}
