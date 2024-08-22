using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using UnityEditor;
using System;

//A debug script to see the value of a tile (OPTIONAL)
public class Debug_test : MonoBehaviour
{
    public static List<GameObject> List = new List<GameObject>();

    private static GameObject _prefab;

    public static GameObject prefab
    {
        get
        {
            if (_prefab is null)
                _prefab = Resources.Load<GameObject>("Prefab/Debug_text");

            return _prefab;
        }
    }

    private static GameObject _prefabParent;

    public static GameObject prefabParent
    {
        get
        {
            if (_prefabParent is null)
                _prefabParent = GameObject.Find("DebugTestTileParent");

            return _prefabParent;
        }
    }

    //FocusMode mean what we value we display (All = all values, display V, G or H)
    public enum FocusMode { All, V, G, H }

    //The variable we use for FocusMode
    public FocusMode focusMode;

    //The variable G and H of the tile V can be defined with G + H
    public int G, H;

    //The position of the tile in string
    public string pos, additionalText = "";

    //The text display
    public Text display_text;

    //Initialising
    void Start()
    {
        if (!List.Contains(this.gameObject))
        {
            List.Add(this.gameObject);
        }

        //Convert the pos into the pos of the tile
        transform.position = F.ConvertToWorldVector2(pos);

        //Display mode
        if (focusMode == FocusMode.All)
        {
            display_text.text = "G:" + G + "\nH:" + H + "\nV:" + (G + H) + additionalText; //Diplay all values
        }
        else if (focusMode == FocusMode.V)
        {
            display_text.text = "V:" + (G + H) + additionalText; //Display only V = G + H

        }
        else if (focusMode == FocusMode.G)
        {
            display_text.text = "G:" + G + additionalText; //Display only G

        }
        else if (focusMode == FocusMode.H)
        {
            display_text.text = "H:" + H + additionalText; //Display only H

        }
    }

    private void Update()
    {
        //If we press space remove the debug display
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(this.gameObject); //Destroy it
        }
    }
}
