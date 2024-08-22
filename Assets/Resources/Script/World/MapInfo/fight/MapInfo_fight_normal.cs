using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LayerMap;
using MonsterRandom;

namespace LayerMap
{
    public class MapInfo_fight_normal : IMap
    {
        public IMap.mapType type { get { return IMap.mapType.fight_normal; } }

        public string posInWorld { get; set; }

        public bool complete
        {
            get
            {
                return created && monsterkilled;
            }
        }

        public bool monsterkilled;

        public bool created { get; set; }

        public MapInfo_fight_normal(string pos, MonsterInMap listMonster)
        {
            this.posInWorld = pos;

            this.listMonster = listMonster;
        }

        public int lvl_min, lvl_max;
        public int nbMonster;

        public MonsterInMap listMonster;

        private string _tileMapInfoPath = "";

        public virtual string tileMapInfoPath
        {
            get
            {
                if (_tileMapInfoPath == "")
                {
                    Object[] ar = Resources.LoadAll("Prefab/Map/groundAboveTemplate/fight/normal");

                    int nb = Random.Range(1, ar.Length + 1);

                    _tileMapInfoPath = "Prefab/Map/groundAboveTemplate/fight/normal/obstacle_" + nb;
                }

                return _tileMapInfoPath;
            }
        }

        public Map Instantiate()
        {
            if (!created)
            {
                created = true;
            }

            Map map = MapInfoSingleton.Instance.InstantiateMap(this, tileMapInfoPath);

            if (!monsterkilled)
            {
                map.InstantiateListMonster(listMonster);

                DefineLockedDirection(map);

                newTitleCall();
            }

            return map;
        }

        public virtual void newTitleCall()
        {
            int nbMonster = listMonster.list.Count;

            int nbLevel = 0;
            foreach (var info in listMonster.list)
            {
                nbLevel += info.level;
            }

            string underTitle = "";

            if (nbMonster == 1)
            {
                var type = listMonster.list[0].type;

                var info = Monster.GetNewInfoFromType(type);

                underTitle = info.Title();
            }
            else
            {
                underTitle = "" + nbMonster + " monsters";
            }

            underTitle += " - " + nbLevel + " level";

            TitleNewMap.Instance.NewTitle("Ennemy", underTitle);
        }

        bool posLocked(string pos)
        {
            return !VisitedMap.contain(pos);
        }

        public void DefineLockedDirection(Map map)
        {
            List<DirectionData.Direction> lockedDirection = new List<DirectionData.Direction>();

            if (posLocked(F.AdditionPos("1_0", posInWorld)))
                lockedDirection.Add(DirectionData.Direction.right);
            if (posLocked(F.AdditionPos("-1_0", posInWorld)))
                lockedDirection.Add(DirectionData.Direction.left);
            if (posLocked(F.AdditionPos("0_1", posInWorld)))
                lockedDirection.Add(DirectionData.Direction.up);
            if (posLocked(F.AdditionPos("0_-1", posInWorld)))
                lockedDirection.Add(DirectionData.Direction.down);

            map.Locked_Set(lockedDirection);
        }

        public void AddedToVisibleMap()
        {

        }

        public void AddedToVisitedMap()
        {
            MapInfo.playInstantiateSound();
            MakeInstanstiateSound();
        }

        public virtual void MakeInstanstiateSound ()
        {
            SoundManager.PlaySound(SoundManager.list.environment_entering_fightNormal);
        }
    }
}