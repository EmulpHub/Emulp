using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Map : MonoBehaviour
{
    public Transform collectable_parent;

    public void AddCollectable(Collectable collectable)
    {
        int nbCollectable = collectable_parent.childCount;

        collectable.transform.parent = collectable_parent;

        collectable.position = collectablePos[nbCollectable % collectablePos.Count];

        collectable.SetMap(this);


    }
}
