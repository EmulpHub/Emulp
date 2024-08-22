using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Editor_ListEquipment : EditorWindow
{
    //Create a window call "Tile rendering" for generate appropriate tile for tilemap
    [MenuItem("Window/Equipment")]
    public static void ShowWindow()
    {
        //GetThe window
        GetWindow<Editor_ListEquipment>("Equipment");
    }

    public Transform ListEquipmentHolder;

    public SingleEquipment.type typeToErase;

    public void OnGUI()
    {
        ListEquipmentHolder = (Transform)EditorGUILayout.ObjectField(ListEquipmentHolder, typeof(Transform), true);

        if (GUILayout.Button("CheckAllEquipmentCondition"))
        {
            List<SingleEquipment> ls = GetAllEquipment(ListEquipmentHolder, (SingleEquipment e) => { return Equipment_Single_Editor.state(e) != "OK"; });

            if (ls.Count == 0)
            {
                UnityEngine.Debug.Log("No error");
            }
            else

                ShowEquipment(ls, "");
        }
    }

    public void ShowEquipment(List<SingleEquipment> ls, string additionalText = "")
    {
        if (additionalText != "")
            Debug.Log("" + additionalText);

        foreach (SingleEquipment e in ls)
        {
            Debug.Log("" + e.name);
        }
    }

    public delegate bool condition(SingleEquipment e);

    public List<SingleEquipment> GetAllEquipment(Transform parent, condition con)
    {
        List<SingleEquipment> ls = new List<SingleEquipment>();

        foreach (Transform a in parent)
        {
            SingleEquipment e = a.GetComponent<SingleEquipment>();

            if (e != null && con(e))
            {
                ls.Add(e);
            }

            if (a.childCount > 0)
            {
                ls.AddRange(GetAllEquipment(a, con));
            }
        }

        return ls;
    }
}
