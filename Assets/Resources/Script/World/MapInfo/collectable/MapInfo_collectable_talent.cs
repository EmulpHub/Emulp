using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LayerMap
{
    public class MapInfo_collectable_talent : MapInfo_collectable, IMap
    {
        public IMap.mapType type { get { return IMap.mapType.collectable_talent; } }

        public MapInfo_collectable_talent(string pos, int nbTalent) : base(pos)
        {
            this.nbTalent = nbTalent;
        }

        int nbTalent;

        internal override void CreateCollectable(Map map)
        {
            for (int i = 0; i < nbTalent; i++)
            {
                Collectable_Foutain collectable = Collectable_Foutain.Create();

                map.AddCollectable(collectable);

                listeCollectableSave.Add(collectable.ExportSave());
                listeCollectable.Add(collectable);
            }
        }

        public override string tileMapInfoPath
        {
            get => "Prefab/Map/groundAboveTemplate/talent/talent";
        }

        public override void newTitleCall()
        {
            TitleNewMap.Instance.NewTitle("Talent");
        }

        public override void AddedToVisibleMap()
        {
            base.AddedToVisibleMap();
        }

        public override void AddedToVisitedMap()
        {
            base.AddedToVisitedMap();
            SoundManager.PlaySound(SoundManager.list.environment_enteringTalent);
        }
    }
}