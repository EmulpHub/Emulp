using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class Window_skill : Window
{
    /// <summary>
    /// Move the background of window skill to the given position (anchor or not depending of "isAnchor")
    /// </summary>
    /// <param name="position">i</param>
    /// <param name="zoom"></param>
    public void SetScaleAndPosition(Vector3 position, float zoom, bool isAnchor, float speed)
    {
        delayMovementAndScale = speed + 0.2f;

        if (isAnchor)
        {
            MoveBackgroundToAnchor(position, speed);
        }
        else
        {
            MoveBackgroundToTransform(position, speed);
        }

        SetZoomBackgroundTo(zoom, speed);
    }

    public void MovementAndScaleGestion_Update()
    {
        ApplyLimit();
    }

    [HideInInspector]
    public float delayMovementAndScale;

    /// <summary>
    /// The backgroundParent Used when we want to move the background (or scale)
    /// </summary>
    public Transform backGroundParent, background_scale;

    public void ScaleGestion()
    {
        //Lower delay
        delayMovementAndScale -= 1 * Time.deltaTime;

        //Check condition
        if (delayMovementAndScale > 0 || !window.inFront)
        {
            return;
        }

        Vector3 effectiveScroll = Input.mouseScrollDelta.y * new Vector3(1, 1, 0) * scrollStrenght;

        if (effectiveScroll.x != 0)
            Main_UI.Display_EraseAllType();

        background_scale.transform.localScale = background_scale.transform.localScale + effectiveScroll;

        if (Input.mouseScrollDelta.y != 0)
        {
            if (highLightedSkill == null)
            {
                background_isMouseOver = true;
            }
            else
            {
                highLightedSkill.DisplayInfo();
            }
        }
    }


    [HideInInspector]
    public Vector3 BaseAnchoredPosition;

    public void MovementGestion()
    {
        if (delayMovementAndScale > 0)
            return;


        //When the mouse button is pressed

        //Extending
        Vector3 mouse = CursorInfo.Instance.positionInWorld;

        //Movement
        if (background_drag == false)
        {
            IsDraging = true;
            background_margin = mouse - backGroundParent.transform.position;
            background_drag = true;
        }

        Vector3 newPos = mouse - background_margin;

        backGroundParent.transform.position = newPos;

        //Update the change
        MoveBackgroundToTransform(backGroundParent.transform.position);
        ApplyLimit();
    }

    public void ApplyLimit()
    {
        background_scale.transform.localScale = new Vector3(Mathf.Clamp(background_scale.transform.localScale.x, MinZoom, MaxZoom), Mathf.Clamp(background_scale.transform.localScale.y, MinZoom, MaxZoom), 1);

        RectTransform rect = backGroundParent.GetComponent<RectTransform>();

        float scale = background_scale.transform.localScale.x;

        rect.anchoredPosition = new Vector3(Mathf.Clamp(rect.anchoredPosition.x, -MaxX * scale, MaxX * scale), Mathf.Clamp(rect.anchoredPosition.y, -MaxY * scale, MaxY * scale), 0);
    }

    /// <summary>
    /// Set the zoom of the background (zoom must be between 0 and 1, 1 equal max)
    /// </summary>
    /// <param name="zoom">zoom must be between 0 and 1, 1 equal max</param>
    public void SetZoomBackgroundTo(float zoom, float animationTime)
    {
        float scale = MaxZoom * zoom;

        background_scale.transform.DOScale(scale, animationTime);

        Main_UI.Display_EraseAllType();
    }

    public void SetZoomBackgroundTo(float zoom)
    {
        SetZoomBackgroundTo(zoom, 0);
    }

    public void MoveBackgroundToTransform(Vector3 pos, float animationTime)
    {
        backGroundParent.transform.DOMove(new Vector3(pos.x, pos.y, transform.position.z), animationTime);
    }

    public void MoveBackgroundToTransform(Vector3 pos)
    {
        MoveBackgroundToTransform(pos, 0);
    }

    public void MoveBackgroundToAnchor(Vector3 pos, float animationTime)
    {

        //BaseAnchoredPosition = pos;

        //pos *= background_scale.transform.localScale.x;
        backGroundParent.GetComponent<RectTransform>().DOAnchorPos(new Vector3(pos.x, pos.y, transform.position.z), animationTime);
    }

    public void MoveBackgroundToAnchor(Vector3 pos)
    {
        MoveBackgroundToAnchor(pos, 0);
    }

}
