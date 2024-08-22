using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public partial class Map : MonoBehaviour
{
    public string playerStartPos;

    [HideInInspector]
    public List<string> collectablePos;

    public GameObject positionningMapParent;

    public bool noLoot;

    public Tilemap ChoosePositioningMap()
    {
        int rng = UnityEngine.Random.Range(0, positionningMapParent.transform.childCount);

        return positionningMapParent.transform.GetChild(rng).GetComponent<Tilemap>();
    }

    public void Init()
    {
        transform.position = new Vector3(0, -0.39375f, 0);

        choosenPositionTile = ChoosePositioningMap();

        foreach (Transform child in positionningMapParent.transform)
        {
            child.gameObject.SetActive(false);
        }

        positionningMapParent.gameObject.SetActive(false);

        CalcExitPos();
    }

    public delegate void func();

    public event func eventErase = () => { };

    public void Erase()
    {
        eventErase();
        /*
        int max = 0;

        while (monsterInArea.Count > 0 && max < 100)
        {
            monsterInArea[0].Kill(V.player_entity);

            max++;
        }*/

        Destroy(this.gameObject);
    }

    [HideInInspector]
    public Tilemap choosenPositionTile;

    public Tilemap ground_above, ground;
}
