using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class V : MonoBehaviour
{
    public enum L { FR, UK };

    public static L language = L.FR;

    public static bool IsLanguage(L Language)
    {
        return language == Language;
    }

    public static bool IsFr()
    {
        return IsLanguage(L.FR);
    }

    public static bool IsUk()
    {
        return IsLanguage(L.UK);
    }
}
