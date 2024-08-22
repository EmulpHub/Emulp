using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Main_UI : MonoBehaviour
{
    /// <summary>
    /// Find a confortable pos to the camera for an UI element
    /// </summary>
    /// <param name="maxValue">The maxValue from the border of the camera in wich it's ok to be</param>
    /// <param name="Position">The current position of the ui element</param>
    /// <param name="Box">The box rectransform of the ui</param>
    /// <param name="Original">The original gameobject</param>
    /// <returns></returns>
    public static Vector3 FindConfortablePos(float maxValue, Vector3 Position, RectTransform Box, GameObject Original, float customHeight = -1)
    {
        //Calculate xscale and yscale of the box in world point
        float box_xScaleDivided = Box.sizeDelta.x / 2 * Box.lossyScale.x, box_yScaleDivided = (customHeight < 0 ? Box.sizeDelta.y : customHeight) * Box.lossyScale.y;

        Vector3[] v = GetBoundaries(Position, Box, Original, customHeight);

        if (Original.GetComponent<RectTransform>() != null)
        {
            RectTransform rct = Original.GetComponent<RectTransform>();

            if (rct.pivot.x == 0.5f && rct.pivot.y == 0.5f)
            {
                box_yScaleDivided *= 0.5f;
            }
        }

        //Debug.Log("Before ViewPort : Bottom Left = " + v[0] + " Top Left = " + v[1] + "Top right = " + v[2] + " bottom Left = " + v[3]);

        //For each value convert it into camera vector2
        for (int a = 0; a < 4; a++)
        {
            v[a] = Camera.main.WorldToViewportPoint(v[a]);
        }

        //Debug.Log("In ViewPort : Bottom Left = " + v[0] + " Top Left = " + v[1] + "Top right = " + v[2] + " bottom Left = " + v[3]);

        //calculate minAutorizeValue
        float minValue = 1 / maxValue - 1;

        if (v[1].y >= maxValue) // If top left is too up
        {
            //Give it a more confortable x position
            float newY = Camera.main.ViewportToWorldPoint(new Vector3(0, maxValue, 0)).y - box_yScaleDivided;

            //Add the value (only x)
            Position = new Vector3(Position.x, newY, 0);
        }

        if (v[0].y <= minValue) // if bottom left is too down
        {
            //Give it a more confortable x position
            float newY = Camera.main.ViewportToWorldPoint(new Vector3(0, minValue, 0)).y + box_yScaleDivided;

            //Add the value (only x)
            Position = new Vector3(Position.x, newY, 0);
        }

        if (v[2].x >= maxValue) // if top right is too right
        {
            //Give it a more confortable y position
            float newX = Camera.main.ViewportToWorldPoint(new Vector3(maxValue, 0, 0)).x - box_xScaleDivided;

            //Add the value (only y)
            Position = new Vector3(newX, Position.y, 0);
        }

        if (v[1].x <= minValue) // if top left is too left
        {
            //Give it a more confortable y position
            float newX = Camera.main.ViewportToWorldPoint(new Vector3(minValue, 0, 0)).x + box_xScaleDivided;

            //Add the value (only y)
            Position = new Vector3(newX, Position.y, 0);
        }

        return Position;
    }

    private static Vector3[] GetBoundaries(Vector3 Position, RectTransform Box, GameObject Original, float customYSize = -1)
    {
        //Set the list to store boundaries of the box so we can make a good position for the user
        Vector3[] v = new Vector3[4];

        //Calculate xscale and yscale of the box in world point
        float box_xScaleDivided = Box.sizeDelta.x / 2 * Box.lossyScale.x, box_yScaleDivided = (customYSize < 0 ? Box.sizeDelta.y : customYSize) * Box.lossyScale.y;

        RectTransform rct = Original.GetComponent<RectTransform>();

        if (rct != null)
        {
            if (rct.pivot.x == 0.5f && rct.pivot.y == 0.5f)
            {
                box_yScaleDivided *= 0.5f;
            }
        }

        // 0 = bottom left
        // 1 = top left
        // 2 = top right
        // 3 = bottom right

        //Get the boundaries of the box and assignate into "v"
        v[0] = new Vector3(-box_xScaleDivided, -box_yScaleDivided, 0) + Position;
        v[1] = new Vector3(-box_xScaleDivided, box_yScaleDivided, 0) + Position;
        v[2] = new Vector3(box_xScaleDivided, box_yScaleDivided, 0) + Position;
        v[3] = new Vector3(box_xScaleDivided, -box_yScaleDivided, 0) + Position;

        //Debug.Log("Bottom Left = " + v[0] + " Top Left = " + v[1] + "Top right = " + v[2] + " bottom Left = " + v[3]);

        return v;
    }

    #region V2

    public static (Vector2 top_right, Vector2 bottom_left) GetBoundaries_V2(RectTransform Box, float customYHeight = -1)
    {

        //the box size
        float box_xScaleDivided = Box.rect.width / 2 * Box.lossyScale.x, box_yScaleDivided = (customYHeight < 0 ? Box.rect.height : customYHeight) / 2 * Box.lossyScale.y;

        Vector2 position = (Vector2)Box.position + new Vector2(box_xScaleDivided * ((Box.pivot.x - 0.5f) * -2), box_yScaleDivided * ((Box.pivot.y - 0.5f) * -2));

        (Vector2 top_right, Vector2 bottom_left) v = (
            Vector2.zero, //TOP Right
            Vector2.zero); //Bottom left

        //Convert v into real point get the boundaries
        v.top_right = position + new Vector2(box_xScaleDivided, box_yScaleDivided);

        v.bottom_left = position + new Vector2(-box_xScaleDivided, -box_yScaleDivided);

        return v;
    }

    public static Vector3 FindConfortablePos_V2(RectTransform Box)
    {
        (Vector2 top_right, Vector2 bottom_left) v = GetBoundaries_V2(Box);

        //Convert v into real point get the boundaries
        v.top_right = Camera.main.WorldToViewportPoint(v.top_right);

        v.bottom_left = Camera.main.WorldToViewportPoint(v.bottom_left);

        float diff_x = 0, diff_y = 0;

        //Check and repair
        if (v.top_right.y > 1)
        {
            diff_y = Camera.main.ViewportToWorldPoint(new Vector2(0, v.top_right.y)).y - Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y;
        }
        else if (v.bottom_left.y < 0)
        {
            diff_y = Camera.main.ViewportToWorldPoint(new Vector2(0, v.bottom_left.y)).y - Camera.main.ViewportToWorldPoint(Vector2.zero).y;
        }

        float maxValue = 0.92f, minValue = 0.10f;

        if (v.top_right.x > 1 && v.bottom_left.x > maxValue)
        {
            diff_x = CalculateRealPointDiff_x(v.bottom_left.x, maxValue);
        }
        else if (v.bottom_left.x < 0 && v.top_right.x < minValue)
        {
            diff_x = CalculateRealPointDiff_x(v.top_right.x, minValue);
        }

        return Box.position - new Vector3(diff_x, diff_y);
    }

    public static float CalculateRealPointDiff_x(float first, float second)
    {
        return Camera.main.ViewportToWorldPoint(new Vector2(first, 0)).x - Camera.main.ViewportToWorldPoint(new Vector2(second, 0)).x;
    }

    public static float CalculateRealPointDiff_y(float first, float second)
    {
        return Camera.main.ViewportToWorldPoint(new Vector2(0, first)).y - Camera.main.ViewportToWorldPoint(new Vector2(0, second)).y;
    }

    #endregion
}

//        Debug.Log("position box = " + position + " xy.x = " + xy.x + " xy.y = " + xy.y);

/*#region V2

public static Vector3 FindConfortablePos_V2(RectTransform Box)
{
    //Debug.Log("box.size delta = " + Box.sizeDelta.x + " box .lossyscale.x = " + Box.lossyScale.x + " width = " + Box.rect.width);

    //the box size
    float box_xScaleDivided = Box.rect.width / 2 * Box.lossyScale.x, box_yScaleDivided = Box.rect.height / 2 * Box.lossyScale.y;

    //Debug.Log("pos x = " + pos_x + "pos y = " + pos_y + " rect center = " + Box.rect.center);

    Vector2 position = (Vector2)Box.position + Box.rect.center * Box.lossyScale;

    (Vector2 top_left, Vector2 top_right, Vector2 bottom_right, Vector2 bottom_left) v = (Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero);

    //Convert v into real point get the boundaries
    v.top_right = position + new Vector2(box_xScaleDivided, box_yScaleDivided);

    v.top_left = position + new Vector2(-box_xScaleDivided, box_yScaleDivided);

    v.bottom_right = position + new Vector2(box_xScaleDivided, -box_yScaleDivided);

    v.bottom_left = position + new Vector2(-box_xScaleDivided, -box_yScaleDivided);

    //Convert into Camera
    v.top_right = Camera.main.WorldToViewportPoint(v.top_right);
    v.top_left = Camera.main.WorldToViewportPoint(v.top_left);
    v.bottom_right = Camera.main.WorldToViewportPoint(v.bottom_right);
    v.bottom_left = Camera.main.WorldToViewportPoint(v.bottom_left);

    //Debug.Log("Position = " + position + " box_xScale = " + box_xScaleDivided + " box_yScale = " + box_yScaleDivided + " Tr = " + v.top_right + " Tl = " + v.top_left + " Br = " + v.bottom_right + " bl = " + v.bottom_left);

    float diff_x = 0, diff_y = 0;

    float diff_x_view = 0, diff_y_view = 0;

    //Check and repair
    if (v.top_right.y > 1)
    {
        diff_y_view = v.top_right.y - 1;

        diff_y = Camera.main.ViewportToWorldPoint(new Vector2(0, v.top_right.y)).y - Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y;
    }
    else if (v.bottom_right.y < 0)
    {
        diff_y_view = v.bottom_right.y;

        diff_y = Camera.main.ViewportToWorldPoint(new Vector2(0, v.bottom_right.y)).y - Camera.main.ViewportToWorldPoint(Vector2.zero).y;
    }

    float maxValue = 0.92f, minValue = 0.10f;

    if (v.top_right.x > 1 && v.top_left.x > maxValue)
    {
        diff_x_view = v.top_left.x - maxValue;

        diff_x = CalculateRealPointDiff_x(v.top_left.x, maxValue);
    }
    else if (v.top_left.x < 0 && v.top_right.x < minValue)
    {
        diff_x_view = v.top_right.x - minValue;

        diff_x = CalculateRealPointDiff_x(v.top_right.x, minValue);
    }

    //Debug.Log("diff_x = " + diff_x + " diff_y = " + diff_y + " view x = " + diff_x_view + " view y = " + diff_y_view);

    //Vector3 s = Camera.main.ViewportToWorldPoint(new Vector3(diff_x, diff_y, Box.position.z));

    return Box.position - new Vector3(diff_x, diff_y);
}

public static float CalculateRealPointDiff_x(float first, float second)
{
    return Camera.main.ViewportToWorldPoint(new Vector2(first, 0)).x - Camera.main.ViewportToWorldPoint(new Vector2(second, 0)).x;
}

public static float CalculateRealPointDiff_y(float first, float second)
{
    return Camera.main.ViewportToWorldPoint(new Vector2(0, first)).y - Camera.main.ViewportToWorldPoint(new Vector2(0, second)).y;
}

#endregion*/
