using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class WindowInfo : MonoBehaviour
{
    private static readonly Lazy<WindowInfo> lazy =
        new(() => new WindowInfo());

    public static WindowInfo Instance { get { return lazy.Value; } }

    public WindowInfo()
    {
        updaterNameSpace.Updater.Instance.AddTo(UpdateFunc);
    }

    public enum type { virgin, character, skill, tutorial, confirmation, equipment, Txt, Ascension };

    public List<Window> listActiveWindow = new List<Window>();

    public List<Window> allWindow = new List<Window>();

    public List<type> allWindow_Link = new List<type>();

    public void ClearListWindow()
    {
        listActiveWindow.Clear();
        allWindow.Clear();
        allWindow_Link.Clear();
    }

    public void AddWindow(Window theWindow)
    {
        if (!allWindow.Contains(theWindow))
            allWindow.Add(theWindow);

        if (!allWindow_Link.Contains(theWindow.type))
            allWindow_Link.Add(theWindow.type);
    }

    public void RemoveWindow(Window theWindow)
    {
        if (allWindow.Contains(theWindow))
            allWindow.Remove(theWindow);

        if (allWindow_Link.Contains(theWindow.type))
            allWindow_Link.Remove(theWindow.type);
    }
    public Window currentSelectionateWindow;

    public bool IsSelectionned(Window w)
    {
        return currentSelectionateWindow == w;
    }

    public void DeselectionnateAllWindow()
    {
        foreach (Window wd in listActiveWindow)
        {
            wd.Deselectionnate();
        }
    }

    public bool IsMouseOverAreaOfAWindow()
    {
        return listActiveWindow.Any(a => a.mouseIsOnWindowArea);
    }

    public static bool IsMouseOverAnotherWindow(Window RequestingWindow = null)
    {
        foreach (Window a in Instance.listActiveWindow)
        {
            if (a != RequestingWindow && a.mouseIsOnWindowArea)
                return true;
        }

        return false;
    }

    public bool isMouseOverAreaOfAWindow;

    public void UpdateFunc()
    {
        isMouseOverAreaOfAWindow = IsMouseOverAreaOfAWindow();

        bool SpellEqualEmpty = SpellGestion.selectionnedSpell_list == SpellGestion.List.empty;

        if (Input.GetMouseButtonDown(0) && listActiveWindow.Count > 0 && SpellEqualEmpty && !isMouseOverAreaOfAWindow && Panel_button.currentSelectionedButton != null)
            DeselectionnateAllWindow();
    }

    public Window OpenOrCloseWindow(type link, Entity target = null)
    {
        (bool find, Window window) GetWindowInfo = GetWindow(link, target);

        if (GetWindowInfo.find)
        {
            if (GetWindowInfo.window.active)
                GetWindowInfo.window.Close();
            else
                GetWindowInfo.window.Open();

            return GetWindowInfo.window;
        }

        GameObject window = Resources.Load<GameObject>("Prefab/Window/Window_" + link.ToString());

        Canvas window_Canvas = Instantiate(window).GetComponent<Canvas>();

        window_Canvas.worldCamera = Camera.main;

        Window script = window_Canvas.transform.GetChild(1).GetComponent<Window>();

        script.type = link;

        AddWindow(script);

        SoundManager.PlaySound(SoundManager.list.ui_window_open);

        if (script is Window_character character)
            character.entity = target;

        script.Selectionnate();

        return script;
    }

    public bool ExistLinkWindow(type Link, Entity target = null)
    {
        return GetWindow(Link, target).find;
    }

    public (bool find, Window w) GetWindow(type Link, Entity target = null)
    {
        foreach (var window in allWindow)
        {
            if (window.type == Link)
            {
                if (window is Window_character character && character.entity != target)
                    continue;

                return (true, window);
            }
        }

        return (false, null);
    }
}
