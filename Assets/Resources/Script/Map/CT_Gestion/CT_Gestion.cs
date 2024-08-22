using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CT_Gestion : MonoBehaviour
{
    private static CT_Gestion _instance;

    public static CT_Gestion Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CT_Gestion>();

            return _instance;
        }
    }

    public void Update()
    {
        Mouse_Detection();
    }

    public void Add_IconSpell(CT tile)
    {
        CT_TileIcon.Instance.Add(tile, iconSpell, colorSpell, 0.035f, true);
    }

    public void Add_IconMovement(CT tile, string lastPrecedent)
    {
        CT_TileIcon.Instance.Add_Movement(tile, lastPrecedent, iconMovement, colorMovement, 0.02f, false);
    }
}
