using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static Tile;
using System;

public class SquareToNextArea : MonoBehaviour
{
    #region var

    public Transform block_parent;

    public SpriteRenderer Icon, block;
    public Sprite block_locked, block_unlocked, icon_monster, icon_boss, icon_equipment, icon_talent;

    private SNA_info info;

    public float moveBackForce, moveBackSpeed;
    public float fadeSpeed;

    public float hideSpeed, showSpeed;

    public float forcedVisibleTimer = 1;

    float forcedVisible = 0;

    #endregion

    #region low 

    public void MovingToNewArea()
    {
        AnimationClick();
    }

    public void ResetRotation()
    {
        transform.localRotation = info.rotation;
        Icon.transform.localRotation = info.rotationIcon;
    }

    public void ResetScale()
    {
        block.size = new Vector2(3, info.blockSizeY);
    }

    public void ResetPosition()
    {
        transform.position = info.position;
    }

    public void Hide()
    {
        ResetBlocParentAnimation();
        block.transform.DOKill();
        block.transform.localPosition = new Vector2(-2f, 0);
    }

    public void Show()
    {
        block.transform.DOLocalMove(new Vector2(-0.9f, 0), showSpeed);
    }

    public void ShowMouse()
    {
        block.transform.DOLocalMove(new Vector2(-0.7f, 0), showSpeed);
    }

    public void AnimationClick()
    {
        forcedVisible = 0;
        ResetBlocParentAnimation();
        ResetAnimation_Fade();
        block_parent.transform.DOLocalMove(new Vector2(-moveBackForce, 0), moveBackSpeed);

        block.DOKill();
        block.DOFade(0, fadeSpeed);

        Icon.DOKill();
        Icon.DOFade(0, fadeSpeed);
    }


    public void UnlockAnimation()
    {
        forcedVisible = forcedVisibleTimer;
        spellAnimation.anim_simple("Unlocked", info.positionLockAnimation, 1);
    }

    public void ResetBlocParentAnimation()
    {
        block_parent.transform.DOKill();
        block_parent.transform.localPosition = Vector3.zero;
    }

    public void ResetAnimation_Fade ()
    {
        block.DOKill();
        block.DOFade(1, 0);
        Icon.DOKill();
        Icon.DOFade(1, 0);
    }

    #endregion

    #region logic

    public void Update()
    {
        forcedVisible -= Time.deltaTime;

        var state = WorldNavigation.curstate_mouse;

        bool visible = state.find && state.directionInformation.direction == info.dir;

        if (visible)
        {
            ShowMouse();
        } else if (forcedVisible > 0)
        {
            Show();
        }
        else 
        {
            ResetAnimation_Fade();
            Hide();
        }

        SetIcon();
        SetBlock();
    }

    public void SetIcon()
    {
        string pos = info.GetTargetMapPos();

        if (WorldData.Contain(pos))
        {
            var map = WorldData.GetMapInfo(pos);

            if (map.type == LayerMap.IMap.mapType.fight_normal)
                Icon.sprite = icon_monster;
            else if (map.type == LayerMap.IMap.mapType.fight_boss)
                Icon.sprite = icon_boss;
            else if (map.type == LayerMap.IMap.mapType.collectable_equipment)
                Icon.sprite = icon_equipment;
            else if (map.type == LayerMap.IMap.mapType.collectable_talent)
                Icon.sprite = icon_talent;
        }
    }

    public void SetBlock()
    {
        var state = WorldNavigation.curstate_mouse;

        bool locked = state.find && state.directionInformation.IsLocked;

        if (locked)
        {
            block.sprite = block_locked;
        }
        else
        {
            block.sprite = block_unlocked;
        }
    }

    public void EventUnlockArea(List<DirectionData.Direction> listeUnlockedDirection)
    {
        if (!listeUnlockedDirection.Contains(info.dir) || !WorldData.Contain(info.GetTargetMapPos())) return;

        UnlockAnimation();
    }

    #endregion

    #region static

    public static SquareToNextArea Create(DirectionData.Direction dir)
    {
        SquareToNextArea script = Instantiate(Resources.Load<GameObject>("Image/Map/SquareToNextArea")).GetComponent<SquareToNextArea>();

        script.info = new SNA_info(dir);

        script.ResetPosition();
        script.ResetRotation();
        script.ResetScale();

        V.worldNavigationGestion.event_MoveToNextArea.Add(script.MovingToNewArea);
        Map.event_unlockArea.Add(script.EventUnlockArea);

        return script;
    }

    #endregion
}

/// <summary>
/// Square to next area info holder
/// </summary>
public class SNA_info
{
    public Vector2 position, positionLockAnimation;
    public Quaternion rotation, rotationIcon;
    public float blockSizeY;

    public DirectionData.Direction dir;

    public SNA_info(DirectionData.Direction dir)
    {
        this.dir = dir;

        if (dir == DirectionData.Direction.left)
        {
            position = new Vector2(-11.1f, 1.3f);
            positionLockAnimation = V.worldNavigationGestion.LockPosition_left;
            rotation = Quaternion.Euler(0, 0, 0);
            rotationIcon = Quaternion.Euler(0, 0, 0);
        }
        else if (dir == DirectionData.Direction.right)
        {
            position = new Vector2(11.1f, 1.3f);

            positionLockAnimation = V.worldNavigationGestion.LockPosition_right;
            rotation = Quaternion.Euler(0, 0, 180);
            rotationIcon = Quaternion.Euler(0, 0, 180);
        }
        else if (dir == DirectionData.Direction.up)
        {
            position = new Vector2(0, 6);

            positionLockAnimation = V.worldNavigationGestion.LockPosition_up;
            rotation = Quaternion.Euler(0, 0, -90);
            rotationIcon = Quaternion.Euler(0, 0, 90);
        }
        else if (dir == DirectionData.Direction.down)
        {
            position = new Vector2(0, -3.3f);

            positionLockAnimation = V.worldNavigationGestion.LockPosition_down;
            rotation = Quaternion.Euler(0, 0, 90);
            rotationIcon = Quaternion.Euler(0, 0, -90);
        }

        if (dir == DirectionData.Direction.left || dir == DirectionData.Direction.right)
            blockSizeY = 9.05f;
        else
            blockSizeY = 19.67f;
    }

    public string GetTargetMapPos()
    {
        string directionString = "";

        if (dir == DirectionData.Direction.right)
            directionString = "1_0";
        else if (dir == DirectionData.Direction.left)
            directionString = "-1_0";
        else if (dir == DirectionData.Direction.up)
            directionString = "0_1";
        else if (dir == DirectionData.Direction.down)
            directionString = "0_-1";

        return F.AdditionPos(WorldData.PlayerPositionInWorld, directionString);
    }
}
