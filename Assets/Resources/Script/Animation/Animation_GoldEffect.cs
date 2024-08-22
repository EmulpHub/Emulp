using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_GoldEffect : MonoBehaviour
{
    public float rotateStr;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotateStr) * Time.deltaTime);
    }
}
