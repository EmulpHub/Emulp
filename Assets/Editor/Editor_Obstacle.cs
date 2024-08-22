using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(Obstacle))]
public class Editor_Obstacle : Editor
{
    public override void OnInspectorGUI()
    {
        if (Editor_tileSet.map_parent != null)
        {
            ThisGameobject = Selection.activeGameObject;

            if (ThisGameobject != null && ThisGameobject.scene.name == SceneManager.GetActiveScene().name)
            {
                Vector2Int v2 = F.ConvertToGridVector_withTilemap(ThisGameobject.transform.position, Editor_tileSet.groundT);

                string pos = F.ConvertToString(v2);

                Collectable c = ThisGameobject.GetComponent<Collectable>();

                Obstacle cO = ThisGameobject.GetComponent<Obstacle>();

                if (c != null)
                {
                    c.position = pos;
                }

                if (cO != null)
                {
                    cO.pos = pos;
                }

                ThisGameobject.transform.position = F.ConvertToWorldVector_withTilemap(v2, Editor_tileSet.groundT);


                ModifyInterestPoint(pos);

                EditorGUILayout.LabelField("Position correction ACTIVE", EditorStyles.boldLabel);
            }
            else
            {
                EditorGUILayout.LabelField("This gameobject not detect", EditorStyles.boldLabel);

            }
        }
        else
        {
            EditorGUILayout.LabelField("no map parent assigned", EditorStyles.boldLabel);

        }
    }

    public void ModifyInterestPoint(string pos)
    {
        GameObject g = GameObject.Find("PointInteret_parent");

        if (g == null)
        {
            g = new GameObject("PointInteret_parent");
        }

        GameObject point = GameObject.Find("MarkerForCollectable(Clone)");

        if (point == null)
        {
            point = Editor_tileSet.AddInterestPoint(pos, null, g.transform);

            point.name = "MarkerForCollectable";
        }

        point.transform.position = F.ConvertToWorldVector_withTilemap(F.ConvertToVector2Int(pos), Editor_tileSet.groundT);

    }

    public GameObject ThisGameobject;
}
