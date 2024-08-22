using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Window : MonoBehaviour
{
    private Vector3 movingMargin;

    private void Move()
    {
        transform.position = CursorInfo.Instance.positionInWorld3 - movingMargin;
    }

    private void SetNewMargin()
    {
        movingMargin = CursorInfo.Instance.positionInWorld3 - transform.position;
    }
}
