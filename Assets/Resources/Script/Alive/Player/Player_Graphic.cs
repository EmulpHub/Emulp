using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Entity
{
    public SpriteRotaInfo GraphiqueRotaInfo;

    public Sprite player_running, player_standing, player_jumping, player_walking;

    public void InitSpriteInfo()
    {
        GraphiqueRotaInfo = new SpriteRotaInfo(player_standing, Vector3.zero, Vector3.zero);
    }

    public void LockRender(float time, SpriteRotaInfo info)
    {
        GraphiqueRotaInfo = info;

        GraphiqueRotaInfo.LockUntil(time);
    }

    public override void Graphique_SetRotationAndSprite(bool running)
    {
        base.Graphique_SetRotationAndSprite(running);

        if (Info.dead) return;

        if (!GraphiqueRotaInfo.CanBeModified())
        {
            GraphiqueRotaInfo.Apply(this);
            return;
        }

        Sprite s = player_standing;
        Vector3 position = new Vector3(0, 0, 0);
        Vector3 rotation = new Vector3(0, 0, 0);

        if (running)
        {
            if (runningInfo.mode == RunningInfo.Mode.run)
                s = player_running;
            else
                s = player_walking;

            if (runningInfo.towardRight)
                rotation = new Vector3(0, 0, 0);
            else
                rotation = new Vector3(0, 180, 0);
        }

        GraphiqueRotaInfo.Set(s, rotation, position);

        GraphiqueRotaInfo.Apply(this);
    }

    public Animator animatorAboveRender;

    public void PlayAnimation(string animationName)
    {
        animatorAboveRender.Play(animationName);
    }
}

public class SpriteRotaInfo
{
    public Sprite s;
    public Vector3 rotation;
    public Vector3 position;

    public float TimeBeforeUnlocked;

    public void LockUntil(float Sec)
    {
        TimeBeforeUnlocked = Time.time + Sec;
    }

    public bool CanBeModified()
    {
        return Time.time >= TimeBeforeUnlocked;
    }

    public SpriteRotaInfo(Sprite S, Vector3 rotation, Vector3 position)
    {
        Set(S, rotation, position);
    }

    public void Set(Sprite S, Vector3 rotation, Vector3 position)
    {
        this.s = S;
        this.rotation = rotation;
        this.position = position;
    }

    public void Set(Sprite S, Vector3 rotation)
    {
        Set(S, rotation, Vector3.zero);
    }

    public void Set(Sprite S)
    {
        Set(S, Vector3.zero, Vector3.zero);
    }

    public void Apply(Player p)
    {
        p.Renderer_nonMovable.sprite = s;
        p.Renderer_movable.transform.localPosition = position;
        p.Renderer_movable.transform.localRotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }
}
