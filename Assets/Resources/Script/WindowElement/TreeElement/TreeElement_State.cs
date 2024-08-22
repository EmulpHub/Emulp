using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class TreeElement : WindowSkillElement
{
    public enum State
    {
        buyable, dadNotAcquired, dadAcquired, purchased
    }

    public State curState;

    public GameObject contour_newEffect;

    public void UpdateState()
    {
        curState = SetState();

        if (contour_newEffect != null) contour_newEffect.gameObject.SetActive(curState == State.buyable);
    }

    public bool DadIsPurchased()
    {
        if (listParent.Count == 0) return true;

        bool result = true;

        foreach (TreeElement s in listParent)
        {
            if(s is TreeElementBuyable buyable)
            {
                result = false;

                if (buyable.IsPurchased()) return true;
                
            }
        }

        return result;
    }

    public bool ChildIsPurchased()
    {
        bool result = true;

        foreach (TreeElement s in listChild)
        {
            if (s is TreeElementBuyable buyable)
            {
                result = false;

                if ( buyable.IsPurchased()) return true;
            }
        }

        return result;
        
    }

    public virtual State SetState()
    {
        if (DadIsPurchased())
            return State.dadAcquired;
        else
            return State.dadNotAcquired;
        
    }
}