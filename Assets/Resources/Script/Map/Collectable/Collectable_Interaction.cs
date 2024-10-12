using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFindingName;

public abstract partial class Collectable : MonoBehaviour
{
    [HideInInspector]
    public bool Clicked = false;

    public void OnClick()
    {
        Clicked = true;

        var PathParam = new PathParam(V.player_entity.CurrentPosition_string, position);

        PathParam.walkableParam.RemoveToForbideenPos(position);

        V.player_entity.MoveTo(PathParam);
    }

    public bool CheckIfPlayerRecoltThis()
    {
        return F.DistanceBetweenTwoPos(position, V.player_entity.CurrentPosition_string) < 1;
    }

    internal Save attachedSave;

    private bool _collected;

    public bool collected
    {
        get { return _collected; }

        set
        {
            _collected = value;

            if (attachedSave != null)
                attachedSave.collected = _collected;
        }
    }

    public virtual void Collect(bool Erase = true)
    {
        collected = true;

        SoundManager.PlaySound(SoundManager.list.ui_choiceLanguage);

        Remove(Erase);
    }

    bool Removed = false;

    public virtual void Remove(bool Erase = true)
    {
        if (Removed)
            return;

        Removed = true;

        Main_UI.Display_Title_Erase();

        PlayerMoveAutorization.Instance.Remove(gameObject);

        RemoveObstacleFromList();

        if (Erase)
        {
            Destroy(this.gameObject);
        }
    }
}
