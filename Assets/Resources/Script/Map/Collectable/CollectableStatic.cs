using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LayerCollectable
{
    public class CollectableStatic : MonoBehaviour
    {
        public enum Type { empty , power, weapon, chest, foutain, xp }

        public static GameObject chest
        {
            get
            {
                return Resources.Load<GameObject>("Prefab/Collectable/Collectable_Chest");
            }
        }

        public static GameObject chest_Choice
        {
            get
            {
                return Resources.Load<GameObject>("Prefab/Collectable/Collectable_Chest_Choice");
            }
        }

        public static GameObject foutain
        {
            get
            {
                return Resources.Load<GameObject>("Prefab/Collectable/Collectable_Foutain");
            }
        }

        public static GameObject xp
        {
            get
            {
                return Resources.Load<GameObject>("Prefab/Collectable/Collectable_Xp");
            }
        }
    }
}