using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Window : MonoBehaviour
{
    public bool Autorize_Movement = true, Autorize_Stretch = true;

    public enum StretchDirection { none, up_right, up_left, down_left, down_right, up, down, right, left };

    public StretchDirection stretchDirection;

    public enum state { idle, stretching, moving }

    public state currentState;

    public bool Manipulating
    {
        get => currentState != state.idle;
    }

    public RectTransform button_close_rect;

    public void CheckIfSetPos()
    {
        if (currentState == state.idle && Autorize_Movement)
            SetPosition();
    }

    public void SetState(state newState)
    {
        if (newState == currentState) return;

        currentState = newState;
        stretchDirection = CalcStretchDirection();
        SetNewMargin();
    }

    public state CalcState()
    {
        if (!mouseIsOver)
            return state.idle;

        if (CalcStretchDirection() != StretchDirection.none)
            return state.stretching;
        else if (Autorize_Movement)
            return state.moving;

        return state.idle;
    }

    public StretchDirection CalcStretchDirection()
    {
        if (!Autorize_Stretch)
            return StretchDirection.none;

        GameObject Original = transform.parent.gameObject;
        RectTransform Box = thisRect;

        //Set the list to store boundaries of the box so we can make a good position for the user
        (Vector3 down_left, Vector3 up_right, Vector3 down_right, Vector3 up_left) v;

        //Calculate xscale and yscale of the box in world point
        float box_xScaleDivided = Box.sizeDelta.x / 2 * Original.transform.localScale.x, box_yScaleDivided = Box.sizeDelta.y / 2 * Original.transform.localScale.y;

        Vector3 mousePos = CursorInfo.Instance.positionInWorld;

        //Get the boundaries of the box and assignate into "v"
        v.down_left = new Vector3(-box_xScaleDivided, -box_yScaleDivided, 0) + transform.position;
        v.down_right = new Vector3(box_xScaleDivided, -box_yScaleDivided, 0) + transform.position;
        v.up_right = new Vector3(box_xScaleDivided, box_yScaleDivided, 0) + transform.position;
        v.up_left = new Vector3(-box_xScaleDivided, box_yScaleDivided, 0) + transform.position;

        StretchDirection direction = StretchDirection.down_left;

        float Min = 0.2f;
        float MaxCorner = 0.85f;

        Vector3 closerPoint = v.down_left - new Vector3(mousePos.x, mousePos.y, 0);
        closerPoint = F.CompareVector(closerPoint, v.down_right - new Vector3(mousePos.x, mousePos.y, 0), ref direction, StretchDirection.down_right);
        closerPoint = F.CompareVector(closerPoint, v.up_right - new Vector3(mousePos.x, mousePos.y, 0), ref direction, StretchDirection.up_right);
        closerPoint = F.CompareVector(closerPoint, v.up_left - new Vector3(mousePos.x, mousePos.y, 0), ref direction, StretchDirection.up_left);

        float x = Mathf.Abs(closerPoint.x);
        float y = Mathf.Abs(closerPoint.y);

        float xSize = v.up_right.x - v.up_left.x;
        float ySize = v.up_right.y - v.down_right.y;

        float xPercentage = Mathf.Abs((transform.position.x - mousePos.x) / xSize) * 2;
        float yPercentage = Mathf.Abs((transform.position.y - mousePos.y) / ySize) * 2;

        if (xPercentage > MaxCorner && yPercentage > MaxCorner)
        {
            return direction;
        }

        if (x < Min || y < Min)
        {
            if (x > Min)
            {
                if (direction == StretchDirection.down_left || direction == StretchDirection.down_right)
                    direction = StretchDirection.down;
                else if (direction == StretchDirection.up_right || direction == StretchDirection.up_left)
                    direction = StretchDirection.up;
            }
            else if (y > Min)
            {
                if (direction == StretchDirection.down_left || direction == StretchDirection.up_left)
                    direction = StretchDirection.left;
                else if (direction == StretchDirection.down_right || direction == StretchDirection.up_right)
                    direction = StretchDirection.right;
            }
        }
        else if (x > Min || y > Min)
            direction = StretchDirection.none;

        return direction;
    }

    public RectTransform title_border;

    public void SetPosition()
    {
        Vector3 difference = title_border.transform.position - transform.position;

        Vector3 border_pos = Main_UI.FindConfortablePos_V2(title_border);

        transform.position = border_pos - difference;
    }
}
