using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LayerMap;

namespace LayerMap
{
    public abstract class MapInfo_collectable
    {
        public string posInWorld { get; set; }
        public bool complete
        {
            get
            {
                if (!created) return false;

                foreach (Collectable.Save save in listeCollectableSave)
                {
                    if (!save.cannotBeCreated()) return false;
                }

                return true;
            }
        }

        public bool created { get; set; }

        internal List<Collectable.Save> listeCollectableSave = new();

        internal List<Collectable> listeCollectable = new List<Collectable>();

        public MapInfo_collectable(string pos)
        {
            this.posInWorld = pos;
        }

        public Map Instantiate()
        {
            created = true;

            Map map = MapInfoSingleton.Instance.InstantiateMap((IMap)this, tileMapInfoPath);

            ManageCollectable(map);

            if (listeCollectable.Count != 0)
                newTitleCall();

            return map;
        }

        internal virtual void CreateCollectable(Map map)
        {
            throw new System.Exception("abstract");
        }

        void ManageCollectable(Map map)
        {
            listeCollectable.Clear();

            if (listeCollectableSave.Count == 0)
            {
                CreateCollectable(map);

            }
            else
            {
                foreach (Collectable.Save save in listeCollectableSave)
                {
                    Collectable collectable = save.Create();

                    if (collectable != null)
                    {
                        map.AddCollectable(collectable);
                        listeCollectable.Add(collectable);
                    }
                }
            }
        }

        public virtual string tileMapInfoPath
        {
            get
            {
                return "Prefab/Map/groundAboveTemplate/empty/empty";
            }
        }

        public virtual void newTitleCall()
        {
        }

        public virtual void AddedToVisibleMap()
        {

        }

        public virtual void AddedToVisitedMap()
        {
            MapInfo.playInstantiateSound();
        }
    }
}