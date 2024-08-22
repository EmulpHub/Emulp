using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Editor_EditingWindowSkill : EditorWindow
{
    //Create a window call "Tile rendering" for generate appropriate tile for tilemap
    [MenuItem("Window/SkillTreeManagement")]
    public static void ShowWindow()
    {
        //GetThe window
        GetWindow<Editor_EditingWindowSkill>("Skill tree management");
    }

    public Transform WantedSpellToWorkOn;

    public List<TreeSkill> ls = new List<TreeSkill>();

    public Window_skill ws;

    public Vector2 windowSize;

    public void OnGUI()
    {
        WantedSpellToWorkOn = (Transform)EditorGUILayout.ObjectField(WantedSpellToWorkOn, typeof(Transform), true);

        ws = (Window_skill)EditorGUILayout.ObjectField(ws, typeof(Window_skill), true);

        if (WantedSpellToWorkOn != null && ws != null)
        {
            if (V.IsInMain)
            {
                GUILayout.Label("We'r in main - change scene to nortice");
                return;
            }

            ls = GetAllSkill(WantedSpellToWorkOn);

            SpellGestion.Initialize();

            //Renderer
            MakeAction(
                (TreeSkill s) =>

                {
                    s.graphique.sprite = SpellGestion.Get_sprite(s.spell);
                }

            );

            if (GUILayout.Button("Create bar"))
            {

                while (ws.rodParent.childCount > 0)
                {
                    DestroyImmediate(ws.rodParent.GetChild(0).gameObject);
                }

                MakeAction(
                    (TreeSkill s) =>
                    {
                        s.window = ws;

                        s.Rod_Init(s.listChild);
                    }
                );
            }


            if (GUILayout.Button("Erase bar"))
            {

                while (ws.rodParent.childCount > 0)
                {
                    DestroyImmediate(ws.rodParent.GetChild(0).gameObject);
                }
            }

            if (ws != null)
            {
                if (GUILayout.Button("Expand"))
                {
                    RectTransform r = ws.GetComponent<RectTransform>();

                    windowSize = r.sizeDelta;

                    r.sizeDelta = new Vector2(5000, 5000);
                }

                if (GUILayout.Button("DeExpand"))
                {
                    if (windowSize == Vector2.zero) throw new System.Exception("Expand wasn't used");

                    ws.GetComponent<RectTransform>().sizeDelta = windowSize;
                }
            }

        }
    }


    public delegate void ActionToMake(TreeSkill s);

    public void MakeAction(ActionToMake a)
    {
        foreach (TreeSkill s in ls)
        {
            a(s);
        }
    }

    public List<TreeSkill> GetAllSkill(Transform parent)
    {
        List<TreeSkill> l = new List<TreeSkill>();

        foreach (Transform a in parent)
        {
            TreeSkill s = a.GetComponent<TreeSkill>();

            if (s != null)
            {
                l.Add(s);
            }

            if (a.transform.childCount > 0)
            {
                l.AddRange(GetAllSkill(a));
            }
        }

        return l;
    }
}
