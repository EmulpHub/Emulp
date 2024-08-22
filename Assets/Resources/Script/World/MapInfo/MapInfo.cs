using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LayerMap;

namespace LayerMap
{
    public class MapInfo : IMap
    {
        public IMap.mapType type { get { return IMap.mapType.empty; } }

        public string posInWorld { get; set; }

        public MapInfo(string pos) { this.posInWorld = pos; }

        public bool complete { get => created; }

        public bool created { get; set; }

        public Map Instantiate()
        {
            created = true;

            chooseObstacleTileMapPath();

            return MapInfoSingleton.Instance.InstantiateMap(this, tileMapInfoPath);
        }

        public void AddedToVisibleMap()
        {

        }

        public void AddedToVisitedMap ()
        {
            playInstantiateSound();
        }

        public static void playInstantiateSound ()
        {
            SoundManager.PlaySound(SoundManager.list.environment_entering_map);
        }


        private string _tileMapInfoPath = "";

        public string tileMapInfoPath
        {
            get
            {
                chooseObstacleTileMapPath();

                return _tileMapInfoPath;
            }
            set
            {
                _tileMapInfoPath = value;
            }
        }

        public void chooseObstacleTileMapPath()
        {
            _tileMapInfoPath = "Prefab/Map/groundAboveTemplate/empty/empty";
        }
    }
}
