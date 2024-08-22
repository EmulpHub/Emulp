using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public partial class Scene_Main : MonoBehaviour
{
    /// <summary>
    /// Set the boundaries for spawnable monster tile
    /// </summary>
    /// <returns></returns>
    List<string> SetSpawnableMonsterTile()
    {
        //The unseenTile
        List<string> spawnableMonsterTile = new List<string>();

        //The tilebase that we use to reconignize a tile as a spawnable tile for monster
        TileBase Spawn_Monster = Resources.Load<TileBase>("Image/Tile/TileBase/Spawn_Monster");

        //Check all position in tile_tilemaground
        foreach (Vector3Int position in Main_Map.ground.cellBounds.allPositionsWithin)
        {
            //Check if it's in the boundaries
            if (Main_Map.ground.GetTile(position) == Spawn_Monster)
            {
                //If yes add to the unseenTile
                spawnableMonsterTile.Add(F.ConvertToString(position.x, position.y));
            }
        }

        //return it
        return spawnableMonsterTile;
    }
}
