using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LayerMap;

namespace LayerMap
{
    public class MapInfo_collectable_equipment : MapInfo_collectable, IMap
    {
        public IMap.mapType type { get { return IMap.mapType.collectable_equipment; } }

        int equipmentLevel, nbChest;
        Collectable_Chest.GainType gainType;

        public MapInfo_collectable_equipment(string pos, int equipmentLevel, int nbChest, Collectable_Chest.GainType gainType) : base(pos)
        {
            this.equipmentLevel = equipmentLevel;
            this.nbChest = nbChest;
            this.gainType = gainType;
        }

        internal override void CreateCollectable(Map map)
        {
            for (int i = 0; i < nbChest; i++)
            {
                Collectable_Chest collectable = Collectable_Chest.Create(equipmentLevel, gainType);

                map.AddCollectable(collectable);

                listeCollectableSave.Add(collectable.ExportSave());
                listeCollectable.Add(collectable);
            }
        }

        public override string tileMapInfoPath
        {
            get => "Prefab/Map/groundAboveTemplate/equipment/equipment";
        }

        public override void newTitleCall()
        {
            TitleNewMap.Instance.NewTitle("Equipment");
        }

        public override void AddedToVisibleMap()
        {
            base.AddedToVisibleMap();
        }

        public override void AddedToVisitedMap()
        {
            base.AddedToVisitedMap();
            SoundManager.PlaySound(SoundManager.list.environment_enteringEquipment);
        }
    }
}