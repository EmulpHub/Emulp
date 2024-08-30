using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionStatic : MonoBehaviour
{
    public enum Type
    {
        none, text, equipment
    };

    private static GameObject _currentDescription;

    public static GameObject CurrentDescription
    {
        get
        {
            return _currentDescription;
        }
        set
        {
            _currentDescription = value;
            if (value != null)
                CurrentDescription_script = value.GetComponent<Description>();
        }
    }
    public static Description CurrentDescription_script { get; private set; }   

    private static GameObject _descriptionPrefab;

    public static GameObject DescriptionPrefab
    {
        get
        {
            if (_descriptionPrefab == null)
                _descriptionPrefab = Resources.Load<GameObject>("Prefab/UI_Description/UI_Description");
            return _descriptionPrefab;
        }
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
