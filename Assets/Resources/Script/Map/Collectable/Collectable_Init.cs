using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LayerCollectable;

public abstract partial class Collectable : MonoBehaviour
{
    public Image img;

    [HideInInspector]
    public Obstacle combat_obstacleThis { get
        {
            if (this.gameObject == null) return null;
            return GetComponent<Obstacle>();
        }
    }

    public virtual void Start ()
    {
        //Set the material for the outline shader
        img.material = new Material(img.material);

        ShaderMaterial = img.material;
    }
}
