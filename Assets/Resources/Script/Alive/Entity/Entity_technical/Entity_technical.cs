using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Entity : MonoBehaviour
{
    public SpriteRenderer Renderer;

    public SpriteRenderer Renderer_nonMovable { get => Renderer; set => Renderer = value; }

    public GameObject Renderer_parent;

    public GameObject Renderer_movable { get => Renderer_parent; set => Renderer_parent = value; }

    public BoxCollider2D Collider_Box;

    public PolygonCollider2D Collider_Tile;

    public EntityInfo Info;

    public virtual void OnAwake()
    {
        Collider_Box = gameObject.GetComponent<BoxCollider2D>();
        Collider_Tile = gameObject.GetComponent<PolygonCollider2D>();

        runningInfo = new RunningInfo(this);
    }

    [HideInInspector]
    public Material ShaderMaterial;

    [HideInInspector]
    public float CurrentThicness = 0;

    public virtual void OnStart()
    {
        title_currentDistance = title_minDistance;

        renderer_nonMovable_baseScale = Renderer_nonMovable.transform.localScale;

        ManageSize_Start();

        ShaderMaterial = Renderer_nonMovable.material;

        ShaderMaterial.SetColor("_OutlineColor", outlineColor);
    }

    public virtual void OnUpdate()
    {
        CheckAChangeOfPosition();

        Graphic_update();

        Update_passive();

        ManageSize_update();

        UI_Management();

        Thicness_Update();
    }

    public virtual void Update_passive()
    {
        if (V.game_state == V.State.passive) return;
    }

    public float healAnimationShakeScale_Strength;

    public void Collider_SetActive(bool enable)
    {
        Collider_Box.enabled = enable;
        Collider_Tile.enabled = enable;
    }

    public float lifeBarShakeAnimation_Strenght_Min, lifeBarShakeAnimation_Strenght_Max, lifeBarShakeAnimation_Time, damageShakeScale_Strengh;

    public float damage_RecalStrenght;

    public float damageRotation;

    public virtual bool ShouldShowLifeBar()
    {
        return !Info.IsDead();
    }
}
