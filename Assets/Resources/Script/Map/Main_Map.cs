using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[SerializeField]
public class Main_Map : MonoBehaviour
{

    public static float size_x = 1.575f, size_y = 0.7875f;

    public static Vector3 size_x_vector3 = new Vector3(size_x, 0, 0), size_y_vector3 = new Vector3(0, size_y, 0);

    public static Map currentMap;

    public static GameObject parent
    {
        get
        {
            if (currentMap == null)
                return null;

            return currentMap.gameObject;
        }
    }


    public static Tilemap ground
    {
        get
        {
            if (currentMap == null)
                return null;

            return currentMap.ground;
        }
    }

    public static Tilemap ground_above
    {
        get
        {
            if (currentMap == null)
                return null;

            return currentMap.ground_above;
        }
    }

    public static Tilemap ground_positionning
    {
        get
        {
            if (currentMap == null)
                return null;

            return currentMap.choosenPositionTile;
        }
    }

    public static GameObject ground_positionning_parent
    {
        get
        {
            if (currentMap == null)
                return null;

            return currentMap.positionningMapParent;
        }
    }

    public static void Initialize()
    {
        allMap_parent = GameObject.Find("Maps_parents");
    }

    public static GameObject allMap_parent;


    public static Map InstantiateMap(LayerMap.IMap mapInfo)
    {
        Map map = Instantiate(LayerMap.MapInfoSingleton.Instance.RessourceMap.gameObject, allMap_parent.transform).GetComponent<Map>();

        map.info = mapInfo;

        return map;
    }

    public static List<string> Spawnable_tile_monster = new List<string>();
    public static List<string> Spawnable_tile_player = new List<string>();
    public static List<string> Spawnable_tile_all = new List<string>();

    public static void calculateGroundPositioningTilePos(Tilemap positioning)
    {
        //get acces to the tilebase for the player and monster
        TileBase spawnableMonster_tile = Resources.Load<TileBase>("Image/Tile/TileBase/positionning_monster");
        TileBase spawnablePlayer_tile = Resources.Load<TileBase>("Image/Tile/TileBase/positionning_player");

        Spawnable_tile_all.Clear();
        Spawnable_tile_monster.Clear();
        Spawnable_tile_player.Clear();

        //for all position in ground tilemap
        foreach (Vector3Int Position in positioning.cellBounds.allPositionsWithin)
        {
            //Get the pos in string of the Position
            string pos = F.ConvertToString((Vector2Int)Position);

            //If there is a tile in this position and if it's a spawnableMonster Tile add it to the spawnable tile monster
            if (positioning.GetTile(Position) == spawnableMonster_tile)
            {
                //add it to the spawnable tile monster
                Spawnable_tile_monster.Add(pos);
                Spawnable_tile_all.Add(pos);
            }
            else if (positioning.GetTile(Position) == spawnablePlayer_tile)
            {
                //add it to the spawnable tile player
                Spawnable_tile_player.Add(pos);
                Spawnable_tile_all.Add(pos);
            }
        }

        if (Spawnable_tile_player.Count == 0)
        {
            throw new System.Exception("Pas de positioning case disponible pour PLAYER");
        }
        if (Spawnable_tile_monster.Count == 0)
        {
            throw new System.Exception("Pas de positioning case disponible pour MONSTER");
        }
    }

    public static void EraseCurrentMap()
    {
        foreach (Transform child in allMap_parent.transform)
        {
            Map m = child.gameObject.GetComponent<Map>();

            if (m != null)
            {
                m.Erase();
                break;
            }
        }
    }

    public static void ChangeMap(Map map)
    {
        EraseCurrentMap();

        SetMap(map);
    }

    public static void SetMap(Map map)
    {

        V.player_entity.ResetSpriteAndMovement();

        map.positionningMapParent.gameObject.SetActive(false);
        currentMap = map;

        calculateGroundPositioningTilePos(ground_positionning);
    }
}
