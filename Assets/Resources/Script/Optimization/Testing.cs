using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using System.Linq;

public class Testing : MonoBehaviour
{
    #region SetPossibleTile

    public static void TestSetPossible(int researchSize)
    {
        Stopwatch sw = new Stopwatch();

        sw.Start();

        //Scene_Main.Set_PossibleMovementTile(V.player_entity.CurrentPosition_string, researchSize, false);

        sw.Stop();

        UnityEngine.Debug.Log("Size " + researchSize + " executed at " + sw.ElapsedMilliseconds + " ms");
    }

    public static void TestSetPossible_Multi()
    {
        int i = 10;
        while (i <= 100)
        {
            TestSetPossible(i);

            i++;
        }
    }

    #endregion

    public static void EraseAllDebugTest()
    {
        while (Debug_test.List.Count > 0)
        {
            DestroyImmediate(Debug_test.List[0]);
            Debug_test.List.RemoveAt(0);
        }

    }

    #region ReadString

    public static void ReadStringTest()
    {
        //int iteration = 30000;

        //List<string> allPos = new List<string>();

        //for (int i = 0; i < iteration; i++)
        //{
        //    string final = Random.Range(-10000, 10000 + 1) + "_" + Random.Range(-10000, 10000 + 1);

        //    while (allPos.Contains(final))
        //    {
        //        final = Random.Range(-10000, 10000 + 1) + "_" + Random.Range(-10000, 10000 + 1);
        //    }

        //    allPos.Add(final);
        //}

        //Stopwatch ms = new Stopwatch();

        //ms.Start();

        //for (int i = 0; i < iteration; i++)
        //{
        //    F.ReadString_NotOptimized(allPos[i]);
        //}

        //ms.Stop();

        //print("ReadString non optimized = " + ms.ElapsedMilliseconds + " for " + iteration + " number of iteration");

        //Stopwatch ms_2 = new Stopwatch();

        //ms_2.Start();

        //for (int i = 0; i < iteration; i++)
        //{
        //    F.ReadString(allPos[i]);
        //}

        //ms_2.Stop();

        //print("ReadString V1 = " + ms_2.ElapsedMilliseconds + " for " + iteration + " number of iteration");
    }

    #endregion

    #region TileAtXDistance

    public static void TestTileAtXDistance()
    {
        int iteration = 14;

        Stopwatch ms = new Stopwatch();

        ms.Start();

        for (int i = 0; i < iteration; i++)
        {

            F.TileAtXDistance(i);
        }

        ms.Stop();

        //F.TileAtXDistance_Memory.Clear();

        Stopwatch ms_2 = new Stopwatch();

        ms_2.Start();

        for (int i = 0; i < iteration; i++)
        {
            F.TileAtXDistance(i);
        }

        ms_2.Stop();
    }

    public static void GenerateTileAtXDistance()
    {
        int iteration = 15;

        string ToReturn = "{" + ReturnList(F.TileAtXDistance(0));

        for (int i = 1; i < iteration; i++)
        {
            ToReturn += "," + ReturnList(F.TileAtXDistance(i));
        }

        print(ToReturn + "}");
    }

    public static string ReturnList(List<string> s)
    {
        if (s.Count == 0)
            return "";

        string toReturn = "\"" + s[0] + "\"";

        for (int i = 1; i < s.Count; i++)
        {
            toReturn += ",\"" + s[i] + "\"";
        }

        return "new List<string>{" + toReturn + "}";
    }

    #endregion

    #region ClampTest

    public static void ClampTest()
    {
        //(cameraCoord.x - IsPosSeenable_min) * (IsPosSeenable_max - cameraCoord.x) >= 0

        int iteration = 300000000;

        bool value = false;
        int min = Mathf.FloorToInt(iteration * 0.25f), max = Mathf.FloorToInt(iteration * 0.75f);

        Stopwatch sw_2 = new Stopwatch();

        sw_2.Start();

        for (int i = 0; i < iteration; i++)
        {
            value = i >= min && i <= max;
        }

        sw_2.Stop();

        print("ancient ms " + sw_2.ElapsedMilliseconds);

        Stopwatch sw = new Stopwatch();

        sw.Start();

        for (int i = 0; i < iteration; i++)
        {
            value = (i - min) * (max - i) >= 0;
        }

        sw.Stop();

        print("new ms = " + sw.ElapsedMilliseconds);

    }

    #endregion
}
