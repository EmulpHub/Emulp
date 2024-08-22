using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using updaterNameSpace;

[SerializeField]
public class CursorInfo : MonoBehaviour
{
    private static readonly Lazy<CursorInfo> lazy =
           new(() => new CursorInfo());

    public static CursorInfo Instance { get { return lazy.Value; } }

    public Vector2Int positionVector2Int { get; private set; }
    public Vector2 positionInWorld { get; private set; }
    public Vector3 positionInWorld3
    {
        get
        {
            return positionInWorld;
        }
    }

    public string position { get; private set; }

    public CursorInfo()
    {
        SetPosition();

        Updater.Instance.AddTo(UpdateFunc);
    }

    private void UpdateFunc()
    {
        SetPosition();
    }

    private void SetPosition()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        positionInWorld = new Vector2(mousePos.x, mousePos.y);

        if (V.IsInMain)
            positionVector2Int = F.ConvertToGridVector(positionInWorld);
        position = F.ConvertToString(positionVector2Int);
    }
}
