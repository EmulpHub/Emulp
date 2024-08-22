using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSpell_VisualEffectPrefab : MonoBehaviour
{
    public string pos;

    public void Init(List<string> ls)
    {
        if (ls.Count == 0)
            throw new System.Exception("empty");

        pos = ls[0];

        int sizeX = 0;
        int sizeY = 0;

        (int x, int y) posXY = F.ReadString(pos);

        foreach (string p in ls)
        {
            (int x, int y) pXY = F.ReadString(p);

            if (p == pos)
                continue;

            sizeX += Mathf.Clamp(pXY.x - posXY.x, -1, 1);
            sizeY += Mathf.Clamp(pXY.y - posXY.y, -1, 1);

        }

        SetPos(pos);
        SetScale(sizeX, sizeY);
    }

    public float Length, ToAdd;

    public void SetScale(int sizeX, int sizeY)
    {
        rectThis.sizeDelta = new Vector2(Length + sizeX * ToAdd, Length + sizeY * ToAdd);
    }

    public RectTransform rectThis;

    public void SetPos(string pos)
    {
        rectThis.anchoredPosition = V.main_ui.calcPosForToolbar(pos) + new Vector2(-Length / 2, Length / 2);
    }
}
