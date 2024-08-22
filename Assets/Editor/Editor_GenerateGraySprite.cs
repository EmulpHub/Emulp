using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class Editor_GenerateGraySprite : EditorWindow
{

    //Create a window call "Tile rendering" for generate appropriate tile for tilemap
    [MenuItem("Window/GenerateGraySprite")]
    public static void ShowWindow()
    {
        //GetThe window
        GetWindow<Editor_GenerateGraySprite>("Generate gray sprite");
    }

    public string pathName;

    public void OnGUI()
    {
        pathName = (string)EditorGUILayout.TextArea(pathName);

        if (GUILayout.Button("Generate gray Texture"))
        {
            GenerateGrayTexture(pathName);
        }
    }

    public void GenerateGrayTexture(string pathName)
    {

        string generateGrayName(Sprite s)
        {
            return s + "_gray";
        }

        Sprite[] ls = Resources.LoadAll<Sprite>(pathName);

        int i = 0;

        while (i < ls.Length)
        {
            Sprite s = ls[i];

            if (s.name.Length > 5 && s.name.Substring(s.name.Length - 5) == "_gray")
            {
                i++;
                continue;
            }

            if (s.texture.texelSize.x > 150)
            {
                UnityEngine.Debug.Log("sprite havent the good max texture size : " + s.name + " wanted = 128 real = " + s.texture.texelSize.x);
                i++;
                continue;
            }


            if (i + 1 < ls.Length)
            {
                Sprite s_2 = ls[i + 1];

                if (s_2.name == generateGrayName(s))
                {
                    i++;
                    continue;
                }
            }

            if (s.texture.format != TextureFormat.RGBA32)
            {
                Debug.Log("sprite " + s.name + " isn't in a good format, wanted = RGBA32 real  =" + s.texture.format.ToString());

                i++;
                continue;
            }



            try
            {
                s.texture.GetPixel(0, 0);
            }
            catch (UnityException e)
            {
                if (e.Message.StartsWith("Texture '" + s.name + "' is not readable"))
                {
                    Debug.Log("sprite haven't have good write/read righr" + s.name + "]");

                    i++;

                    continue;
                }
                else
                {
                    Debug.Log("an error occured: " + e.Message);
                }
            }



            Sprite grayS = greyScaleManagement.ConvertToGrayscale(s);

            string p = AssetDatabase.GetAssetPath(s);

            string path = p.Substring(0, p.Length - 4) + "_gray.png";

            Debug.Log("gray image generated at " +path);

            File.WriteAllBytes(path, ImageConversion.EncodeToPNG(grayS.texture));

            i++;
        }
    }
}

public class SetTexImportSizeViaScript : AssetPostprocessor
{

    //function that's called when a texture starts to be imported
    void OnPreprocessTexture()
    {
        if (assetPath.Substring(0, 27) == "Assets/Resources/Image/Sort")
        {
            //get a reference to the built-in TextureImporter...
            TextureImporter importer = (TextureImporter)assetImporter;

            //create a new empty TextureImporterSettings struct...
            TextureImporterSettings textureImporterSettings = new TextureImporterSettings();

            //read the current import settings from the Texture Importer
            //into our new importer settings struct (basically filling the empty struct with values)
            importer.ReadTextureSettings(textureImporterSettings);

            //change the maxTextureSize setting in our settings struct
            textureImporterSettings.maxTextureSize = 128;
            textureImporterSettings.readable = true;

            //pass the settings struct, with the changed maxTextureSize value, back into the importer
            //(e.g. apply the changed settings to the importer)
            importer.SetTextureSettings(textureImporterSettings);
        }
    }
}