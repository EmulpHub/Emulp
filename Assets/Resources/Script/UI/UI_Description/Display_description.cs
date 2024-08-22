using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The script used for display description and set the right size
/// </summary>
public class Display_description : MonoBehaviour
{
    public void Awake()
    {
        AwakeStuff();
    }

    public virtual void AwakeStuff()
    {

    }

    private void Update()
    {
        Check_PositionAndScale();
    }


    /// <summary>
    /// Text that contain the info
    /// </summary>
    public Text title,description;

    public float description_maxWidth;

    /// <summary>
    /// The box that is used for display description and the pa_img
    /// </summary>
    public Image box;

    /// <summary>
    /// The pos the box's scale must have at minimum or maximum
    /// </summary>
    public float box_minHeightSize_noPa;

    public float box_WidhtSize;

    public ContentSizeFitter ctf_title;

    //return if it should show
    public virtual void Check_PositionAndScale()
    {
        return;
        SetPositionAndScale();
    }

    [HideInInspector]
    public float saveYHeightBox;

    /// <summary>
    /// Set the pos and size of the box and description
    /// </summary>
    public virtual void SetPositionAndScale(bool SaveValue = false)
    {
        return;
        ctf_title.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;

        Canvas.ForceUpdateCanvases();

        if (SaveValue)
        {
            saveYHeightBox = box.rectTransform.sizeDelta.y;
        }
    }

    public int GetMaxNbLine(string txt)
    {
        int nb_max = 0;

        int nb = 0;

        int i = 0;

        bool DontCount = false;

        while (i < txt.Length - 2)
        {
            if (txt[i] == '<')
            {
                DontCount = true;
            }
            else if (txt[i] == '>')
            {
                DontCount = false;
            }

            if (!DontCount)
            {
                if (txt.Substring(i, 2) == "\n")
                {
                    nb = 0;

                    i += 2;
                }
                else
                {
                    nb++;
                    if (nb > nb_max)
                    {
                        nb_max = nb;
                    }
                }
            }



            i++;
        }

        return nb;
    }

    public static (string normal, string info) SeparateDesc(string d)
    {
        string[] info = d.Split("\n\n", 2);

        if (info.Length >= 2)
        {
            return (info[0], "\n\n" + info[1]);

        }
        else
        {
            return (info[0], "");
        }
    }

}
