using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Positionning_tile : MonoBehaviour
{
    /// <summary>
    /// The color of the outline
    /// </summary>
    public Color32 outlineColor;

    /// <summary>
    /// The shadermaterial used for the outline
    /// </summary>
    [HideInInspector]
    public Material ShaderMaterial;

    private void Start()
    {
        //Set the material for the outline shader
        ShaderMaterial = GetComponent<Renderer>().material;

        //Get the proprety Outline color of the shader and set his value to be the one set in the inspector
        ShaderMaterial.SetColor("_OutlineColor", outlineColor);

        //The normal scale (get a save of it)
        scale_normal = transform.localScale.x;

        //if it's not a tile for player to go on 
        if (!forPlayer)
        {
            //Speed his thicness apparition
            currentThicness_speed += 5;
        }
    }

    /// <summary>
    /// The current pos of the tile
    /// </summary>
    [HideInInspector]
    public string pos;

    /// <summary>
    /// Mean if the player can go or not on this tile
    /// </summary>
    [HideInInspector]
    public bool forPlayer;

    /// <summary>
    /// The normal scale of the tile stored in the start function 
    /// </summary>
    [HideInInspector]
    public float scale_normal;

    /// <summary>
    /// The currentThicness of the outilne for this tile
    /// </summary>
    [HideInInspector]
    public float currentThicness;

    /// <summary>
    /// The max thicness of the outline
    /// </summary>
    public float currentThicness_max;

    /// <summary>
    /// The speed in witch it go to the max thicness
    /// </summary>
    public float currentThicness_speed;

    /// <summary>
    /// The max scale this can have when the mouse is over it
    /// </summary>
    public float scale_max;

    /// <summary>
    /// The speed at wich the scale raise to the maximum size when mouse is over
    /// </summary>
    public float scale_speed;

    /// <summary>
    /// The speed in wich the scale raise to the base_scale when the mouse exit the tile
    /// </summary>
    public float scale_speed_lowering;

    public void Update()
    {
        //check if there is an entity on this tile and keep it in memory
        Entity entityOnTile = AliveEntity.GetEntityByPos(pos);

        //Set the thicness to have the currentThicness value
        ShaderMaterial.SetFloat("_Thicness", currentThicness);

        //IF the mouse is on this tile or if the mouse is on the entity on it
        if (CursorInfo.Instance.position == pos || (entityOnTile != null && entityOnTile.IsMouseOnEntity))
        {
            //Make the mouse on it over
            MouseIsOver();
        }
        else if (currentThicness > 0) //Else it mean the mouse is not on us and that it just left
        {
            MouseExit();
        }

        //If currentSTate is not positionning 
        if (V.game_state != V.State.positionning)
        {
            //Destroy it
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// A list of all positioning tile that are inMouseOver in the scene
    /// </summary>
    public static List<GameObject> PositioningTile_OnMouseOver = new List<GameObject>();

    public SpriteRenderer sp;

    /// <summary>
    /// If the mouse is over the tile
    /// </summary>
    void MouseIsOver()
    {
        if (Scene_Main.aWindowIsUsed)
            return;

        if (!PositioningTile_OnMouseOver.Contains(gameObject)) PositioningTile_OnMouseOver.Add(gameObject);

        //Raise thicness
        currentThicness += currentThicness_speed * Time.deltaTime;
        //Clamp thicness
        if (currentThicness > currentThicness_max)
        {
            currentThicness = currentThicness_max;
        }

        //IF it's for player
        if (forPlayer)
        {
            //Make a scale animation
            transform.DOKill();
            transform.DOScale(scale_max, scale_speed);
        }
    }

    /// <summary>
    /// When the mouse exit this tile
    /// </summary>
    void MouseExit()
    {
        if (PositioningTile_OnMouseOver.Contains(gameObject)) PositioningTile_OnMouseOver.Remove(gameObject);

        //Make the thicness equal 0
        currentThicness = 0;

        if (forPlayer)
        {
            //Make an animation of end
            transform.DOKill();
            transform.DOScale(scale_normal, scale_speed_lowering);
        }
    }
}
