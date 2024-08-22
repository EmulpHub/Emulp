using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Window : MonoBehaviour
{
    public bool AlwaysFirstPlanSortingOrder;

    public void ManageSortingOrder()
    {
        if (AlwaysFirstPlanSortingOrder)
            Parent_canvas.sortingOrder = WindowInfo.Instance.listActiveWindow.Count + 2;
        else
            Parent_canvas.sortingOrder = WindowInfo.Instance.listActiveWindow.IndexOf(this) + 1;
    }
}
