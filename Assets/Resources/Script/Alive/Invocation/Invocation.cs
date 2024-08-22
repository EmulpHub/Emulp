using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Invocation : Entity
{
    public enum invocType { none, totem };

    public invocType InvocType = Invocation.invocType.none;

}