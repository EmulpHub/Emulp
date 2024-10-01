using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Entity : MonoBehaviour
{
    private const float baseOrder = 5000;

    internal bool GoRightWhenMoving = true;

    public virtual void Graphic_update()
    {
        Renderer_nonMovable.sortingOrder = (int)(baseOrder - (transform.position.y * 10));

        ShaderMaterial.SetFloat("_Thicness", CurrentThicness);

        Graphique_SetRotationAndSprite(runningInfo.running);
    }

    public virtual void ResetSpriteAndMovement()
    {
    }
}
