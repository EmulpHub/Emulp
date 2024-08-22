using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class RelicInit : MonoBehaviour {

    public static void InitRelic()
    {
        if(relicInfo.Count > 0)
            return;

        if (V.IsFr())
        {
            AddRelic(relicLs.equipment, "Equipment", "*inf+50%*end d'effet pour vos armes\n*inf+100%*end d'effet pour vos objets");
            /*AddRelic(relicLs.life, "Life", "Lorsque vous infligez des dégats a un ennemie tacklé infligez *inf5%*end de vos pv max a cette ennemie\n*inf1% de votre vie max*end est convertis en tacle et en fuite",
                SingleEquipment.value_type.life, 40);*/
            /*AddRelic(relicLs.distance, "Distance", "Pour chaque case entre vous et la cible infligez *inf10%*end de dégâts supplementaire *inf(exponentielle)*end",
                SingleEquipment.value_type.po, 1);*/
            AddRelic(relicLs.killer, "Tueur", "Tuer un ennemie donne *inf+50% de dégâts*end et donne *inf+3 pa*end");
            AddRelic(relicLs.endurance, "Endurance", "Lors d'un soin ou d'un gain d'armure gagnez *inf2 pa*end");
            AddRelic(relicLs.spikyArmor, "Armure piquante", "Gagnez *inf5*end armure par épine a la fin de votre tour");
            //AddRelic(relicLs.criticalExpert, "Expert critique", "Lors d'un coup critique gagnez *inf+5% d'effets critique(ec)*end pour le reste du combat *inf(max 100)*end",
            //SingleEquipment.value_type.cc, 20);
            //AddRelic(relicLs.allOrNothing, "Tout ou rien", "Vos effets de coup critiques sont doublés. Vos sort non critique ont *inf-50% d'effets*end",
            //SingleEquipment.value_type.cc, 20);
            AddRelic(relicLs.vampire, "Vampire", "*inf+30%*end vol de vie");
            AddRelic(relicLs.energetic, "Energetic", "*inf+1*end pa");
            AddRelic(relicLs.doble, "Double", "Tous les 5 tours votre premier sort s'éxecute 2 fois");
        }
    }

    public static void AddRelic(relicLs rName, string title, string description)
    {

        AddRelic(rName, title, description,
            new List<(SingleEquipment.value_type t, int value)>());
    }


    public static void AddRelic(relicLs rName, string title, string description, SingleEquipment.value_type t, int value)
    {
        AddRelic(rName,title,description,
            new List<(SingleEquipment.value_type t,int value)>{(t,value)} );

    }

    public static void AddRelic(relicLs rName, string title, string description, List<(SingleEquipment.value_type t, int value)> value)
    {
        relic re = (relic)new GameObject("relic_" + rName.ToString()).AddComponent(Type.GetType("relic_"+rName.ToString()));

        re.gameObject.transform.SetParent(V.inGameCreatedGameobjectHolder);

        re.set(title, description, Resources.Load<Sprite>("Image/Relic/" + rName.ToString()), value);

        relicInfo.Add(rName, re);
    }
}