using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster : Entity
{
    [HideInInspector]
    public List<Monster> invoke_list = new List<Monster>();

    public bool Alpha;

    [HideInInspector]
    public Sprite monsterSprite;

}
