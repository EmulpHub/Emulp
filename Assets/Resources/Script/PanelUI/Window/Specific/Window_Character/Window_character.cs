using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Window_character : Window
{
    public override void Awake()
    {
        base.Awake();

        base.type = WindowInfo.type.character;
    }

    [HideInInspector]
    public bool ItsPlayer;

    public void Start()
    {

        ItsPlayer = entity.IsPlayer();

        effects = entity.listEffect;

        InstantiateEffect();

        idEvent = entity.event_die.Add(DestroyWhenEntityDie);
    }

    int idEvent;

    private void DestroyWhenEntityDie(Entity entity)
    {
        entity.event_die.Remove(idEvent);

        Destroy();
    }

}
