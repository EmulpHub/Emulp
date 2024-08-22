using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LayerMap;
using MonsterRandom;

namespace LayerMap
{
    public class MapInfo_fight_boss : MapInfo_fight_normal, IMap
    {
        public new IMap.mapType type { get { return IMap.mapType.fight_boss; } }

        public MapInfo_fight_boss(string pos, MonsterInMap listMonster) : base(pos, listMonster) { }

        public override string tileMapInfoPath { get => "Prefab/Map/groundAboveTemplate/fight/boss/boss"; }

        public override void newTitleCall()
        {
            string underTitle = "";

            if (listMonster.list.Count > 0)
            {
                var type = listMonster.list[0].type;

                var info = Monster.GetNewInfoFromType(type);

                underTitle = info.Title();
            }

            TitleNewMap.Instance.NewTitle("Boss", "" + underTitle);
        }

        public override void MakeInstanstiateSound()
        {
            SoundManager.PlaySound(SoundManager.list.environment_entering_fightBoss);
        }
    }
}