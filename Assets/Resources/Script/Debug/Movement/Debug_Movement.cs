using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class Debug_Movement : MonoBehaviour
{
    void Start()
    {
        V.player_entity.event_StartOfRun.Add(StartRun);
        V.player_entity.event_EndOfRun.Add(EndRun);
    }

    public Text info;

    private int PathCount;

    public GameObject visualTilePrefab;

    public Transform visualTileParent;

    void Update()
    {
        string txt = "";

        if (stopWatch is not null)
            txt += "" + (Mathf.Floor(stopWatch.ElapsedMilliseconds / 10) / 100) + " s\n";

        txt += "" + PathCount + " case\n";

        txt += "nbParticleleaf:" + Entity.nbParticleEndOfRun;

        info.text = txt;

        if (Input.GetKeyDown(KeyCode.Space))
            Entity.nbParticleEndOfRun++;
        else if (Input.GetKeyDown(KeyCode.H))
            Entity.nbParticleEndOfRun = 0;
    }

    public Stopwatch stopWatch;

    private List<GameObject> listVisualTile = new List<GameObject>();

    public bool instantiateVTile = true;

    void StartRun(Entity e)
    {
        stopWatch = Stopwatch.StartNew();

        PathCount = V.player_entity.runningInfo.path.LengthInSquare;

        foreach (GameObject vTile in listVisualTile)
        {
            Destroy(vTile);
        }

        listVisualTile.Clear();

        if (!instantiateVTile) return;

        int i = 1;

        var path = V.player_entity.runningInfo.path.path;

        foreach (string p in path)
        {
            var vTile = Instantiate(visualTilePrefab, visualTileParent).GetComponent<Debug_Movement_VTile>();

            listVisualTile.Add(vTile.gameObject);

            vTile.transform.position = F.ConvertToWorldVector2(p);

            vTile.content.text = "" + i;

            vTile.gameObject.SetActive(true);

            if (i == 1) vTile.Img.color = vTile.Start;
            else if (i == path.Count) vTile.Img.color = vTile.End;
            else vTile.Img.color = vTile.Inside;

            vTile.Img.sprite = vTile.circle;

            i++;
        }
    }

    void EndRun(Entity e)
    {
        if (stopWatch is not null)
            stopWatch.Stop();
    }

}
