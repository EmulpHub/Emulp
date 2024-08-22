using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract partial class Collectable : MonoBehaviour
{
    private string _position;

    public string position {

        get { return _position; }

        set {
            _position = F.NearTileWalkable(value);

            combat_obstacleThis.SetPos(position);

            transform.position = F.ConvertToWorldVector2(_position);
        }
    }

    [HideInInspector]
    public Material ShaderMaterial;

    public virtual void Update()
    {
        if (CheckIfPlayerRecoltThis())
        {
            Collect();
        }

        Mouse_Management();

        if (ShaderMaterial != null)
            ShaderMaterial.SetFloat("_Thicness", outline_thicness);

        AnimationManagement();
    }

    public void RemoveObstacleFromList ()
    {
        combat_obstacleThis.RemoveFromList();

        map.eventErase -= RemoveObstacleFromList;
    }

    [HideInInspector]
    public Map map;

    public void SetMap (Map map)
    {
        this.map = map;

        map.eventErase += RemoveObstacleFromList;
    }
}
