using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerArrowAnimation : MonoBehaviour
{
    public float waitTimeBetweenArow, additionalYPos;

    public GameObject Prefab;

    void Start()
    {
        StartCoroutine(CreateAnimation());
    }

    public IEnumerator CreateAnimation()
    {
        while (true)
        {
            GameObject obj = Instantiate(Prefab, this.transform);

            Destroy(obj, 2);

            yield return new WaitForSeconds(waitTimeBetweenArow);
        }
    }


    public void SetPosition(Vector3 position)
    {
        transform.position = position + new Vector3(0, additionalYPos, 0);
    }
}
