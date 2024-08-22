using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Window : MonoBehaviour
{
    private List<RectTransform> listNotInWindow = new List<RectTransform>();

    public void NotIn_Add(RectTransform toAdd)
    {
        if (listNotInWindow.Contains(toAdd)) return;

        listNotInWindow.Add(toAdd);
    }

    public void NotIn_Remove(RectTransform toRemove)
    {
        if (!listNotInWindow.Contains(toRemove)) return;

        listNotInWindow.Remove(toRemove);
    }

    [HideInInspector]
    private List<GameObject> block = new List<GameObject>();
    private List<GameObject> block_cursor = new List<GameObject>();

    public void Block_Add(GameObject toAdd)
    {
        if (block.Contains(toAdd)) return;

        block.Add(toAdd);
    }

    public void Block_Remove(GameObject toAdd)
    {
        if (!block.Contains(toAdd)) return;

        block.Remove(toAdd);
    }

    public void Block_Cursor_Add(GameObject toAdd)
    {
        if (block_cursor.Contains(toAdd)) return;

        block_cursor.Add(toAdd);
    }

    public void Block_Cursor_Remove(GameObject toAdd)
    {
        if (!block_cursor.Contains(toAdd)) return;

        block_cursor.Remove(toAdd);
    }
}
