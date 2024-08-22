using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LayerMap
{
    public class MapInfoSingleton : MonoBehaviour
    {
        private static readonly Lazy<MapInfoSingleton> lazy =
            new(() => new MapInfoSingleton());

        public static MapInfoSingleton Instance { get { return lazy.Value; } }

        public Map RessourceMap { get { return Resources.Load<Map>("Prefab/Map/randomGenerated/base"); } }

        public Map InstantiateMap(IMap mapInfo, string additionalTileMapPath = "")
        {
            Map theMap = Main_Map.InstantiateMap(mapInfo);

            MapModification.addBorderWall(theMap, mapInfo.posInWorld);

            if (additionalTileMapPath != "")
            {
                MapSetterInfo mapSetterInfo = Resources.Load<GameObject>(additionalTileMapPath).GetComponent<MapSetterInfo>();

                //ground above
                Tilemap tilemapToAdd = mapSetterInfo.ground_above;

                MapModification.AddTileToAMap(theMap, tilemapToAdd);

                //Positionning
                Transform positionningTileMap = theMap.positionningMapParent.transform;

                int max = 0;

                while (positionningTileMap.childCount > 0)
                {
                    DestroyImmediate(positionningTileMap.GetChild(0).gameObject);

                    max++;
                    if (max >= 100) throw new Exception("infini");
                }

                Transform tileToAddParent = mapSetterInfo.positionningParent;

                foreach (Transform child in tileToAddParent)
                {
                    Instantiate(child.gameObject, positionningTileMap);
                }

                theMap.collectablePos = mapSetterInfo.collectablePos;
                theMap.monsterStartPosition = mapSetterInfo.monsterPos;
            }

            theMap.Init();


            return theMap;
        }
    }
}
