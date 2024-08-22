using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHolder : MonoBehaviour
{
    public void Start()
    {
        Destroy(this.gameObject, 10);
    }

    public void SetScale (float scale)
    {
        transform.localScale = new Vector3(scale, scale, 1);
    }

    public void SetPosition (Vector2 position)
    {
        transform.position = position;
    }

    [SerializeField]
    Animator animator;

    public void MakeAnimation (string name)
    {
        animator.Play(name);
    }
}
