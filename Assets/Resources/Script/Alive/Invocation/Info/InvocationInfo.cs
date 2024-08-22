using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvocationInfo : EntityInfo
{
    public override void InitInfo(Entity holder)
    {
        base.InitInfo(holder);

        type = Type.playerFriendly;
    }
}
