using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEffect : MonoBehaviour
{

    public static GameObject GetCustomEffect(string name)
    {
        return Resources.Load<GameObject>("Prefab/customEffect/" + name);
    }
}
