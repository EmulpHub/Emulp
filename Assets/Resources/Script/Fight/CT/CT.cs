using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class CT : MonoBehaviour
{
    private static Transform _parent;

    public static Transform parent
    {
        get
        {
            if (_parent == null)
                _parent = new GameObject("CombatTileParent").transform;

            return _parent;
        }

        set { _parent = value; }
    }

    public bool graphic_startAnimation;

    private void Start()
    {
        Vector2 pos_vector = F.ConvertToWorldVector2(pos);

        transform.position = new Vector3(pos_vector.x, pos_vector.y, transform.position.z);

        AnimationApparition(!graphic_startAnimation);

        if (customListTile == null)
            customListTile = CTInfo.Instance.listPosCTKeys;

        SetSprite(ignoreAllEntity, customListTile);
    }

    [HideInInspector]
    public List<string> customListTile = new List<string>();

    public float delay = 0.2f, speed = 0.2f;

    [HideInInspector]
    public string pos;

    public SpriteRenderer render;

    public enum Type { movement, graphic, spell }

    public Type type;

    [HideInInspector]
    public bool ignoreAllEntity;

    public void Initialize(Type type, byte color_a, int sortingOrder, bool ignoreAllEntity, bool ignoreMouseOver, string pos)
    {
        this.type = type;

        this.color_a = (byte)Mathf.Clamp(color_a, 0, 255); ;

        render.sortingOrder = sortingOrder;

        this.ignoreAllEntity = ignoreAllEntity;

        this.ignoreMouseOver = ignoreMouseOver;

        this.pos = pos;

        SetNormalColor();
    }

    public int CalculateSortingOrder(string pos)
    {
        (int x, int y) xy = F.ReadString(pos);

        return xy.x + xy.y;
    }

    public static bool ShouldBeAboveTileMap(string position)
    {
        List<string> posToCheck = new List<string>();

        (int x, int y) xy = F.ReadString(position);

        posToCheck.Add(F.ConvertToString(xy.x + 1, xy.y));
        posToCheck.Add(F.ConvertToString(xy.x, xy.y + 1));
        posToCheck.Add(F.ConvertToString(xy.x + 1, xy.y + 1));

        foreach (string pos in posToCheck)
        {
            if (Main_Map.ground_above.HasTile((Vector3Int)F.ConvertToVector2Int(pos)))
                return true;
        }

        return false;
    }

    [HideInInspector]
    public string recentCombatTileMouseOver_save;

    [HideInInspector]
    public bool isMouseOnThisTile, ignoreMouseOver;

    public virtual void WhenTheMouseEnter()
    {
        isMouseOnThisTile = true;

    }

    public virtual void WhenTheMouseIsOver() { }

    public virtual void WhenTheMouseExit()
    {
        isMouseOnThisTile = false;
    }

    public float DeathSpeed;

    public enum AnimationErase_type { normal, none, fade }

    public void Erase(AnimationErase_type animation)
    {

        if (animation == AnimationErase_type.normal)
        {
            AnimationErase();

            Destroy(this.gameObject, DeathSpeed + 0.2f);
        }
        else if (animation == AnimationErase_type.fade)
        {
            render.DOFade(0, DeathSpeed);

            Destroy(this.gameObject, DeathSpeed + 0.2f);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public IEnumerator EraseAfterDelay(AnimationErase_type erase_Type, float speed)
    {
        yield return new WaitForSeconds(speed);

        Erase(erase_Type);
    }
}
