using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Effect : MonoBehaviour
{
    #region info

    public delegate string calcStringValue();

    internal calcStringValue calcInfoString;

    public virtual void SetInfoString(calcStringValue i = null)
    {
        calcInfoString = delegate
        {
            return "";
        };
    }

    public string getInfoDesc()
    {
        return calcInfoString();
    }

    #endregion

    #region title

    public string GetTitle()
    {
        return descColor.convert(calcTitle() + calcTitle_addOn());
    }

    public string title;

    internal virtual string calcTitle()
    {
        return title;
    }

    internal virtual string calcTitle_addOn()
    {
        string addOn = " *eff" + durationInTurn + "t*end";

        if (ShouldNeverExhaust || durationInTurn == 0)
        {
            addOn = "";
        }

        return addOn;
    }

    #endregion

    #region description

    public string GetDescription()
    {
        return calcDescription();
    }

    internal virtual string calcDescription()
    {
        return "error";
    }

    #endregion

    #region Duration in turn  To Show

    public string GetDurationText()
    {
        return calcDurationString();
    }

    internal calcStringValue calcDurationString;

    internal virtual void SetDurationString(calcStringValue i = null)
    {
        calcDurationString = delegate
        {
            string DurationText = "";

            if (DurationInTurn == 0 || ShouldNeverExhaust)
                DurationText = "";
            else
                DurationText = descColor.convert("*bon" + DurationInTurn + "*end");

            return DurationText;
        };
    }

    #endregion
}
