using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class V : MonoBehaviour
{
    public enum Color
    {
        white, black, grey, green, red, red_light, red_light_ultra, red_dark, yellow, grey_white, orange, orange_towardRed, blue,
        common, uncommon, rare
    }

    public static List<Color32> allColor = new List<Color32>()
        {
            new Color32(210,218,226,255),   //White
            new Color32(0,0,0,255),     //Black
            new Color32(185,185,185,255),   //Grey
            new Color32(65,217,60,255),     //Green
            new Color32(224,58,62,255),     //Red
            new Color32(231,136,138,255),   //Red_light
            new Color32(255,0,10,255),  //Red_light_ultra
            new Color32(125,0,5,255),   //Red_dark
            new Color32(222,229,51,255),    //Yellow
            new Color32(212,212,212,255),   //Grey_white
            new Color32(255,153,0,255),     //Orange
            new Color32(255,92,0,255),  //Orange_towardRed
            new Color32(8,160,255,255),  //Blue
                    new Color32(212,212,212,255),  //Common
            new Color32(51,156,48,255),  //Uncommon
            new Color32(0,112,221,255)  //Rare

        };

    public static Color32 GetColor(Color color)
    {
        return allColor[(int)color];
    }
}
