using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerArrowAnimation_Map : MonoBehaviour
{
    public float waitTimeBetweenArow;

    public GameObject Prefab;

    public DirectionData.Direction dir;

    public float minX, maxX, minY, maxY;

    void Start()
    {
        StartCoroutine(CreateAnimation());
    }

    private void Update()
    {
        if (dir == DirectionData.Direction.down)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (dir == DirectionData.Direction.right)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (dir == DirectionData.Direction.left)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (dir == DirectionData.Direction.up)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
    }

    public void SetPosition(Vector3 position)
    {
        if (dir == DirectionData.Direction.down)
        {
            transform.position = new Vector3(position.x, minY);
        }
        else if (dir == DirectionData.Direction.up)
        {
            transform.position = new Vector3(position.x, maxY);
        }
        else if (dir == DirectionData.Direction.right)
        {
            transform.position = new Vector3(maxX, position.y);
        }
        else if (dir == DirectionData.Direction.left)
        {
            transform.position = new Vector3(minX, position.y);
        }
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
}
