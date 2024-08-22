using System.Reflection;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEngine;

static class Editor_usefulShortcuts
{
    // Alt + C
    [Shortcut("Clear Console", KeyCode.C, ShortcutModifiers.Alt)]
    public static void ClearConsole()
    {
        var assembly = Assembly.GetAssembly(typeof(SceneView));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }

    [Shortcut("Erase Save", KeyCode.O, ShortcutModifiers.Alt)]
    public static void EraseSave()
    {
        Debug.Log("ERASED");

        Save_SaveSystem.EraseSave_WithoutWarning();
    }
}
