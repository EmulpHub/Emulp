using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LayerMap
{
    public interface IMap
    {
        enum mapType { empty, fight_normal, fight_boss, collectable_equipment, collectable_talent }

        mapType type { get; }

        string posInWorld { get; set; }

        string tileMapInfoPath { get; }

        Map Instantiate();

        bool complete { get; }

        bool created { get; set; }

        void AddedToVisibleMap();

        void AddedToVisitedMap();
    }
}
