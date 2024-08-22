using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using System.Reflection;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class Editor_tileSet : EditorWindow
{
    //Create a window call "Tile rendering" for generate appropriate tile for tilemap
    [MenuItem("Window/Tile rendering")]
    public static void ShowWindow()
    {
        //GetThe window
        GetWindow<Editor_tileSet>("Tile rendering");
    }

    /// <summary>
    /// The concerned tilemap
    /// </summary>
    /// 
    public static GameObject map_parent;

    public Map map;

    //the tilemap on the scene
    public GameObject groundG, ground_aboveG;
    public static Tilemap groundT, ground_aboveT, ExitPosT;

    public string NewMap_Pos, WantedChangePos, LoadMap;

    public NavigationData.type wantedDir = NavigationData.type.nul;

    //when we'r in the window in editor
    private void OnGUI()
    {
        EditorGUILayout.LabelField("For Tilemap", EditorStyles.boldLabel);

        map_parent = (GameObject)EditorGUILayout.ObjectField(map_parent, typeof(GameObject), true);

        if (map_parent != null)
        {
            map = map_parent.GetComponent<Map>();

            EditorGUILayout.LabelField("Tilemap Editing", EditorStyles.boldLabel);

            //If we click on the button to set the tile on ground
            if (GUILayout.Button("Set ground tile"))
                //Set the ground tile
                SetTile_ground();

            //If we click on the button to set the tile on ground
            if (GUILayout.Button("Remove unseen tile (ground)"))
                //Remove unseen tile
                RemoveUnseenTile(groundT);

            //If we click on the button to set the tile on above ground
            if (GUILayout.Button("Set rock tile"))
                //Set the ground_above_tile
                SetTile_ground_above();

            EditorGUILayout.LabelField("Verification", EditorStyles.boldLabel);

            if (GUILayout.Button("Control All"))
            {
                Verification_Positionning();
                Verification_GoArea();
            }

            if (GUILayout.Button("Control positionning tile"))
                Verification_Positionning();

            if (GUILayout.Button("Control GoArea tile"))
                Verification_GoArea();

            EditorGUILayout.LabelField("Visualization", EditorStyles.boldLabel);

            if (GUILayout.Button("Affichage/Désafichage Positioning"))
                Visualization_Positioning();

            if (GUILayout.Button("Affichage/Désafichage Points d'intêret"))
                Visualization_PointDInteret();

            if (GUILayout.Button("Affichage/Désafichage ExitPoint"))
                Visualization_ExitPos();

            EditorGUILayout.LabelField("Action", EditorStyles.boldLabel);

            if (GUILayout.Button("Remove Bad ExitPoint"))
                RemoveBadExitPoint();

            wantedDir = (NavigationData.type)EditorGUILayout.EnumPopup(wantedDir);

            if (GUILayout.Button("Remove Exit point of state"))
                RemoveExitPoint_OfState(wantedDir);

            EditorGUILayout.LabelField("Change player/monster/artefact pos", EditorStyles.boldLabel);

            WantedChangePos = (string)EditorGUILayout.TextField(WantedChangePos);

            if (GUILayout.Button("Change Player start pos"))
                ChangePlayerStartPos(WantedChangePos);

            if (GUILayout.Button("Change Monster Pos"))
                ChangeMonsterStartPos(WantedChangePos);

            if (GUILayout.Button("Change Artefact Pos"))
                ChangeArtefactStartPos(WantedChangePos);


            #region All Label 

            Positionning_Parent = map_parent.transform.GetChild(2);

            groundG = map_parent.transform.GetChild(0).gameObject;
            ground_aboveG = map_parent.transform.GetChild(1).gameObject;

            groundT = groundG.GetComponent<Tilemap>();
            ground_aboveT = ground_aboveG.GetComponent<Tilemap>();

            ExitPosT = map_parent.transform.GetChild(3).GetComponent<Tilemap>();

            //assignate grass tile

            Grass = Resources.Load<TileBase>("Image/Tile/TileBase/grass");
            Grass_Plant = Resources.Load<TileBase>("Image/Tile/TileBase/grass_Plant");
            Grass_Plant_2 = Resources.Load<TileBase>("Image/Tile/TileBase/grass_Plant_2");

            //Assignating rock tile
            rock_up = Resources.Load<TileBase>("Image/Tile/TileBase/rock_up");
            rock_down = Resources.Load<TileBase>("Image/Tile/TileBase/rock_down");
            rock_right = Resources.Load<TileBase>("Image/Tile/TileBase/rock_right");
            rock_left = Resources.Load<TileBase>("Image/Tile/TileBase/rock_left");

            rock_up_right = Resources.Load<TileBase>("Image/Tile/TileBase/rock_up_right");
            rock_up_left = Resources.Load<TileBase>("Image/Tile/TileBase/rock_up_left");
            rock_down_right = Resources.Load<TileBase>("Image/Tile/TileBase/rock_down_right");
            rock_down_left = Resources.Load<TileBase>("Image/Tile/TileBase/rock_down_left");

            rock_directed_up = Resources.Load<TileBase>("Image/Tile/TileBase/rock_directed_up");
            rock_directed_down = Resources.Load<TileBase>("Image/Tile/TileBase/rock_directed_down");
            rock_directed_right = Resources.Load<TileBase>("Image/Tile/TileBase/rock_directed_right");
            rock_directed_left = Resources.Load<TileBase>("Image/Tile/TileBase/rock_directed_left");

            rock_middle_right = Resources.Load<TileBase>("Image/Tile/TileBase/rock_middle_right");
            rock_middle_left = Resources.Load<TileBase>("Image/Tile/TileBase/rock_middle_left");

            rock_center = Resources.Load<TileBase>("Image/Tile/TileBase/rock_center");
            rock_center_grass = Resources.Load<TileBase>("Image/Tile/TileBase/rock_center_grass");

            rock_Alone = Resources.Load<TileBase>("Image/Tile/TileBase/rock_alone");

            holderInfo = GameObject.Find("INFOHOLDER").GetComponent<Editor_TileSetHolder>();

            #endregion

            EditorGUILayout.LabelField("Save/Modification/Remove", EditorStyles.boldLabel);

            if (GUILayout.Button("Save"))
                SaveMap();

            if (GUILayout.Button("Save and remove"))
                RemoveAndSaveMap();

            if (GUILayout.Button("Remove"))
                RemoveMap();
        }

        if (map_parent == null)
        {
            EditorGUILayout.LabelField("Create or load Map", EditorStyles.boldLabel);

            NewMap_Pos = (string)EditorGUILayout.TextField(NewMap_Pos);

            if (GUILayout.Button("Creer nouvelle map"))
                CreerMap(NewMap_Pos);

            if (GUILayout.Button("LoadMap"))
                LoadAMap(NewMap_Pos);
        }

        EditorGUILayout.LabelField("Navigation", EditorStyles.boldLabel);

        if (SceneManager.GetActiveScene().name == "Main")
        {
            if (GUILayout.Button("Go to map editor"))
                ChangeScene("Assets/Scenes/Map_CreationArea.unity");
        }
        else
        {
            if (GUILayout.Button("Go to main"))
                ChangeScene("Assets/Scenes/Main.unity");
        }
    }

    #region Verification void

    #region Verification_Positionning 

    public Transform Positionning_Parent;

    public void Verification_Positionning()
    {
        ClearLog();

        TileBase player = Resources.Load<TileBase>("Image/Tile/TileBase/positionning_player");
        TileBase monster = Resources.Load<TileBase>("Image/Tile/TileBase/positionning_monster");

        foreach (Transform c in Positionning_Parent)
        {
            Tilemap t = c.GetComponent<Tilemap>();

            if (t == null)
            {
                throw new System.Exception("Gameobject pas au bon endroit " + c.gameObject.name);
            }
            else
            {
                Verification_Positionning_Specific(t, player, monster);
            }
        }

        Debug.Log("Fin de la verification de positionning");
    }

    public void Verification_Positionning_Specific(Tilemap positionning, TileBase tile_player, TileBase tile_monster)
    {
        ClearLog();

        int nb_player = 0, nb_monster = 0;

        foreach (Vector3Int Position in positionning.cellBounds.allPositionsWithin)
        {
            if (positionning.HasTile(Position))
            {
                TileBase b = positionning.GetTile(Position);

                if (b == tile_player)
                {
                    nb_player++;
                }
                else if (b == tile_monster)
                {
                    nb_monster++;
                }
                else
                {
                    throw new System.Exception("Une tile anormale a été trouver pour " + positionning.gameObject.name);
                }
            }
        }

        if (nb_player != 8)
        {
            Debug.Log("WARNING: Player tile(" + nb_player + ") invalide " + positionning.gameObject.name);
        }
        if (nb_monster != 8)
        {
            Debug.Log("WARNING: Monster tile(" + nb_monster + ") invalide " + positionning.gameObject.name);
        }
    }

    #endregion

    #region Verification Go Area

    public List<string> Verification_GoArea(bool withPrint = true)
    {
        ClearLog();

        List<string> BadExitPoint = new List<string>();

        Tilemap GoArea = map_parent.transform.GetChild(3).GetComponent<Tilemap>();

        foreach (Vector3Int Position in GoArea.cellBounds.allPositionsWithin)
        {
            if (ground_aboveT.HasTile(Position) && GoArea.HasTile(Position))
            {
                string p = F.ConvertToString(Position);

                if (withPrint)
                    Debug.Log("Obstruation GoArea et ground_Above: " + p);

                BadExitPoint.Add(p);
            }
        }

        if (withPrint)
            Debug.Log("Fin verification go area");

        return BadExitPoint;
    }

    #endregion

    #endregion

    #region Visualization

    public void Visualization_Positioning()
    {
        bool value = false;

        bool first = true;

        foreach (Transform t in Positionning_Parent)
        {
            if (first)
            {
                first = false;
                value = !t.gameObject.activeSelf;
            }
            t.gameObject.SetActive(value);
        }
    }

    public static void RemoveAllInterestPoint()
    {
        GameObject g = GameObject.Find("PointInteret_parent");

        if (g == null)
        {
            g = new GameObject("PointInteret_parent");
        }

        if (g.transform.childCount > 0)
        {
            while (g.transform.childCount > 0)
            {
                DestroyImmediate(g.transform.GetChild(0).gameObject);
            }

        }
    }

    public static GameObject AddInterestPoint(string pos, Sprite render, Transform parent = null)
    {
        GameObject holder = Resources.Load<GameObject>("Prefab/Utility/PointInteret");

        GameObject a = Instantiate(holder, parent);

        a.transform.position = F.ConvertToWorldVector_withTilemap(F.ConvertToVector2Int(pos), groundT);

        Image i = a.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>();

        i.sprite = render;

        if (render == null)
        {
            i.gameObject.SetActive(false);
        }

        return holder;
    }

    public bool Visualization_PointDInteret()
    {
        GameObject g = GameObject.Find("PointInteret_parent");

        if (g == null)
        {
            g = new GameObject("PointInteret_parent");
        }

        if (g.transform.childCount > 0)
        {
            RemoveAllInterestPoint();

            return false;
        }
        else
        {

            Sprite Player = Resources.Load<Sprite>("Image/Player/Player_Standing_1");
            Sprite Monster = Resources.Load<Sprite>("Image/Monster/normal");
            Sprite Chest = Resources.Load<Sprite>("Image/Map/Chest");

            AddInterestPoint(map.playerStartPos, Player, g.transform);
            //AddInterestPoint(map.collectablePos, Chest, g.transform);
            AddInterestPoint(map.monsterStartPosition, Monster, g.transform);

            return true;
        }
    }

    public void Visualization_PointDInteret_Erase()
    {
        GameObject g = GameObject.Find("PointInteret_parent");

        if (g != null && g.transform.childCount > 0)
        {
            while (g.transform.childCount > 0)
            {
                DestroyImmediate(g.transform.GetChild(0).gameObject);
            }

        }
    }

    public void Visualization_ExitPos()
    {

        ExitPosT.gameObject.SetActive(!ExitPosT.gameObject.activeSelf);
    }

    #endregion

    #region CreationMap

    public (bool find, GameObject g) GetMap(string pos)
    {
        GameObject[] maps = Resources.LoadAll<GameObject>("Prefab/Map/World");

        foreach (GameObject g in maps)
        {
            if (g.GetComponent<Map>().posInWorld == pos)
            {
                return (true, g);
            }
        }

        return (false, null);
    }

    public void CreerMap(string newPos)
    {
        RemoveAllInterestPoint();

        (bool find, GameObject g) v = GetMap(newPos);

        if (v.find)
        {
            throw new System.Exception("The map already exist for pos = " + newPos);
        }

        Object originalPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Prefab/Map/Normal.prefab", typeof(GameObject));
        GameObject objSource = PrefabUtility.InstantiatePrefab(originalPrefab) as GameObject;

        string path = "Assets/Resources/Prefab/Map/World/" + newPos + ".prefab";

        GameObject prefabVariant = PrefabUtility.SaveAsPrefabAsset(objSource, path);

        DestroyImmediate(objSource);

        GameObject OriginalNewOne = (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
        GameObject newOne = PrefabUtility.InstantiatePrefab(OriginalNewOne) as GameObject;

        newOne.transform.SetParent(GameObject.Find("Maps_parents").transform);

        SetToCurrentMapParent(newOne);
    }

    #endregion

    #region Action

    public void RemoveBadExitPoint()
    {
        ClearLog();

        foreach (string p in Verification_GoArea(false))
        {
            ExitPosT.SetTile((Vector3Int)F.ConvertToVector2Int(p), null);
        }

        if (!ExitPosT.gameObject.activeSelf)
            Visualization_ExitPos();
    }

    public void RemoveExitPoint_OfState(NavigationData.type state)
    {
        ClearLog();

        void RemoveTile(string pos)
        {
            ExitPosT.SetTile((Vector3Int)F.ConvertToVector2Int(pos), null);
        }

        ExitPosT.CompressBounds();

        TileBase wantedTileBase = Resources.Load<TileBase>("Image/Tile/TileBase/Tile_NextArea_Up");

        if (state == NavigationData.type.go_Down)
        {
            wantedTileBase = Resources.Load<TileBase>("Image/Tile/TileBase/Tile_NextArea_Down");
        }
        else if (state == NavigationData.type.go_Right)
        {
            wantedTileBase = Resources.Load<TileBase>("Image/Tile/TileBase/Tile_NextArea_Right");
        }
        else if (state == NavigationData.type.go_Left)
        {
            wantedTileBase = Resources.Load<TileBase>("Image/Tile/TileBase/Tile_NextArea_Left");
        }

        foreach (Vector3Int Position in ExitPosT.cellBounds.allPositionsWithin)
        {
            if (!ExitPosT.HasTile(Position))
                continue;

            //Get the pos in string of the Position
            string pos = F.ConvertToString(Position);

            TileBase t = ExitPosT.GetTile(Position);

            if (t == wantedTileBase)
            {
                RemoveTile(pos);
            }

        }

    }

    public void ChangePlayerStartPos(string pos)
    {
        ClearLog();

        map.playerStartPos = pos;

        if (!Visualization_PointDInteret())
        {
            Visualization_PointDInteret();
        }
    }

    public void ChangeArtefactStartPos(string pos)
    {
        ClearLog();

        //map.collectablePos = pos;

        if (!Visualization_PointDInteret())
        {
            Visualization_PointDInteret();
        }

        Debug.Log("Player start pos changed to " + pos);
    }

    public void ChangeMonsterStartPos(string pos)
    {
        ClearLog();

        map.monsterStartPosition = pos;

        if (!Visualization_PointDInteret())
        {
            Visualization_PointDInteret();
        }

        Debug.Log("Player start pos changed to " + pos);
    }

    #endregion

    #region Load

    public void LoadAMap(string pos)
    {
        RemoveAllInterestPoint();

        ClearLog();

        if (map_parent != null)
        {
            DestroyImmediate(map_parent.gameObject);
        }

        (bool find, GameObject g) v = GetMap(pos);

        if (!v.find)
        {
            throw new System.Exception("The map don't exist for pos = " + pos);
        }

        string path = "Assets/Resources/Prefab/Map/World/" + v.g.name + ".prefab";

        GameObject OriginalNewOne = (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));

        if (OriginalNewOne == null)
            throw new System.Exception("wanted Map not found (" + pos + ")");

        GameObject newOne = PrefabUtility.InstantiatePrefab(OriginalNewOne) as GameObject;

        newOne.transform.SetParent(GameObject.Find("Maps_parents").transform);

        SetToCurrentMapParent(newOne);

        Visualization_PointDInteret_Erase();
    }

    public void SetToCurrentMapParent(GameObject newMap)
    {
        map_parent = newMap;
    }

    public void SaveMap()
    {
        ClearLog();

        string path = "Assets/Resources/Prefab/Map/World/" + map.posInWorld + ".prefab";

        PrefabUtility.ApplyPrefabInstance(map_parent, InteractionMode.UserAction);

        Debug.Log("Map " + map_parent.gameObject.name + " Have been saved");
    }

    public void RemoveMap()
    {
        if (EditorUtility.DisplayDialog("Supprimer", "Voulez vous supprimez cette map ? (Pas de sauvegarde)", "oui", "non"))
        {
            RemoveAllInterestPoint();

            ClearLog();

            DestroyImmediate(map_parent);
            Debug.Log("map erased");
        }
    }

    public void RemoveAndSaveMap()
    {
        if (EditorUtility.DisplayDialog("Supprimer", "Voulez vous sauvegardez et supprimer cette map ?", "oui", "non"))
        {
            RemoveAllInterestPoint();

            ClearLog();

            SaveMap();

            DestroyImmediate(map_parent);
            Debug.Log("map saved and erased");
        }
    }

    #endregion

    #region Navigation

    public void ChangeScene(string path)
    {
        ClearLog();

        EditorSceneManager.OpenScene(path);
    }

    #endregion

    #region Others

    //The tilebase of grass is contain in ground tilemap
    public TileBase Grass, Grass_Plant, Grass_Plant_2;


    //Generate tile for ground tilemap
    public void SetTile_ground()
    {
        //All the tileBase of all variety a single grass tile can have
        List<TileBase> Grass_tile = new List<TileBase>() { Grass_Plant, Grass_Plant_2 };

        int ChanceOfGrassPlant = 30;

        //for all position in ground tilemap
        foreach (Vector3Int Position in groundT.cellBounds.allPositionsWithin)
        {
            //Check if there is a tile in it
            if (groundT.HasTile(Position))
            {
                //If yes that mean that the tile can be a plant one bc no rock is around (in square) and the tile have good tilebase
                if ((groundT.GetTile(Position) == Grass || Grass_tile.Contains(groundT.GetTile(Position))) && F.NoBlockedTileNearby_Specific(Position.x, Position.y, groundT, ground_aboveT, new List<string>() { "-1_0", "0_-1", "-1_-1" }))
                {
                    //Give it a chance to be a grass_tile
                    RandomTile(Grass, Grass_tile, ChanceOfGrassPlant, Position, groundT);
                }
                else if (Grass_tile.Contains(groundT.GetTile(Position)))
                {
                    //If it don't make it become a grass (to allow multiple edit)
                    groundT.SetTile(Position, Grass);
                }
            }
        }
    }

    public void RemoveUnseenTile(Tilemap target)
    {
        Vector3 SaveCameraPos = Camera.main.transform.position;

        Camera.main.transform.position = CalculateNewCameraPosition(map_parent);

        //for all position in ground tilemap
        foreach (Vector3Int Position in target.cellBounds.allPositionsWithin)
        {
            //Check if there is a tile in it
            if (!F.IsTileApproximativelySeenable(F.ConvertToString((Vector2Int)Position), target))
            {
                int x = Position.x;
                int y = Position.y;

                List<Vector2Int> RelativePosToCheck = new List<Vector2Int>() { new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(-1, 0), new Vector2Int(0, -1)/*, "1_1", "-1_-1", "1_-1", "-1_1", "1_-1"*/
    };

                bool contain = false;

                foreach (Vector2Int pos in RelativePosToCheck)
                {
                    if (F.IsTileApproximativelySeenable(F.ConvertToString(x + pos.x, y + pos.y), target))
                    {
                        contain = true;
                        break;
                    }
                }

                if (!contain)
                {
                    target.SetTile(Position, null);
                }
            }
        }

        Camera.main.transform.position = SaveCameraPos;
    }

    //The tileBase we need for programming devices
    public TileBase rock_up, rock_down, rock_right, rock_left, rock_down_right, rock_down_left, rock_up_right, rock_up_left;
    public TileBase rock_directed_up, rock_directed_down, rock_directed_right, rock_directed_left;
    public TileBase rock_middle_right, rock_middle_left, rock_center, rock_center_grass, rock_center_grass_2, rock_Alone;

    public Editor_TileSetHolder holderInfo;

    //Generate the tile for ground above tilemap
    public void SetTile_ground_above()
    {
        int chanceOfRock = 30;

        //For each position in ground_above tilemap
        foreach (Vector3Int Position in ground_aboveT.cellBounds.allPositionsWithin)
        {
            //If there is a tile
            if (ground_aboveT.HasTile(Position))
            {
                if (!holderInfo.ground_SetRock.Contains(ground_aboveT.GetTile(Position)))
                {
                    continue;
                }

                //Get his coord
                int x = Position.x;
                int y = Position.y;

                bool Check(Vector3Int pos)
                {
                    if (ground_aboveT.HasTile(pos))
                    {
                        return holderInfo.ground_SetRock.Contains(ground_aboveT.GetTile(pos));
                    }
                    else
                    {
                        return false;
                    }
                }

                //Check if there is a case around the tile (no square-
                bool up = Check(SetPos(x, y + 1));
                bool down = Check(SetPos(x, y - 1));
                bool right = Check(SetPos(x + 1, y));
                bool left = Check(SetPos(x - 1, y));

                //for up = no up, yes all
                if (down && right && left && !up)
                {
                    ground_aboveT.SetTile(Position, rock_up);
                }

                //for up_right = no right, no up, yes down, yes left
                else if (down && !right && left && !up)
                {
                    ground_aboveT.SetTile(Position, rock_up_right);
                }

                //for up_left = no left, no up, yes down, yes right
                else if (down && right && !left && !up)
                {
                    ground_aboveT.SetTile(Position, rock_up_left);
                }

                //for down = no down, yes all
                else if (!down && right && left && up)
                {
                    ground_aboveT.SetTile(Position, rock_down);
                }

                //for down_right = no right, no down, yes up, yes left
                else if (!down && !right && left && up)
                {
                    ground_aboveT.SetTile(Position, rock_down_right);
                }

                //for down_left = no left, no down, yes up, yes right
                else if (!down && right && !left && up)
                {
                    ground_aboveT.SetTile(Position, rock_down_left);
                }

                //for right = no right, yes all
                else if (down && !right && left && up)
                {
                    ground_aboveT.SetTile(Position, rock_right);
                }

                //for left = no left, yes all
                else if (down && right && !left && up)
                {
                    ground_aboveT.SetTile(Position, rock_left);
                }

                //for directed up = yes down no all
                else if (down && !right && !left && !up)
                {
                    ground_aboveT.SetTile(Position, rock_directed_up);
                }

                //for directed down = yes up no all
                else if (!down && !right && !left && up)
                {
                    ground_aboveT.SetTile(Position, rock_directed_down);
                }

                //for directed right yes left no all
                else if (!down && !right && left && !up)
                {
                    ground_aboveT.SetTile(Position, rock_directed_right);
                }

                //for directed left = yes right no all
                else if (!down && right && !left && !up)
                {
                    ground_aboveT.SetTile(Position, rock_directed_left);
                }

                //for mid left = yes up and down no all
                else if (up && down && !left && !right)
                {
                    ground_aboveT.SetTile(Position, rock_middle_left);
                }

                //for mid right = yes right and left no all
                else if (!up && !down && left && right)
                {
                    ground_aboveT.SetTile(Position, rock_middle_right);
                }

                //rock_center
                else if (up && down & left && right)
                {
                    //choose a random tile
                    //ground_above.SetTile(Position, rock_center_grass);
                    RandomTile(rock_center, new List<TileBase> { rock_center_grass }, chanceOfRock, Position, ground_aboveT);
                }

                //if there is no tile nearby
                else
                {
                    ground_aboveT.SetTile(Position, rock_Alone);
                }

            }
        }
    }

    /// <summary>
    /// Choose a randomtile for "normal" tile in "tilemap" tilemap woth a "lucl" chace"
    /// </summary>
    /// <param name="normal"></param>
    /// <param name="rare"></param>
    /// <param name="luck"></param>
    /// <param name="Position"></param>
    /// <param name="tilemap"></param>
    public void RandomTile(TileBase normal, List<TileBase> rare, int luck, Vector3Int Position, Tilemap tilemap)
    {
        int i = Random.Range(0, luck);
        if (i == 1)
        {
            tilemap.SetTile(Position, rare[Random.Range(0, rare.Count)]);
        }
        else
        {
            tilemap.SetTile(Position, normal);
        }
    }

    /// <summary>
    /// Transform argument into Vector3int
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Vector3Int SetPos(int x, int y)
    {
        return new Vector3Int(x, y, 0);
    }

    /// <summary>
    /// Calculate a new camera pos depending of a new tilemap gameobject named "g"
    /// </summary>
    /// <param name="g"></param>
    /// <returns></returns>
    public Vector3 CalculateNewCameraPosition(GameObject g)
    {
        return new Vector3(g.transform.position.x, g.transform.position.y + 0.39375f, Camera.main.transform.position.z);
    }

    public void ClearLog() //you can copy/paste this code to the bottom of your script
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }

    #endregion
}
