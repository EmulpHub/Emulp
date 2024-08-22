using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LayerMap;

public partial class Case : MonoBehaviour
{
    public Sprite img_monster, img_boss, img_equipment,img_talent;

    public Image element;

    public void SetImage()
    {
        element.gameObject.SetActive(!mapInfo.complete);

        if (mapInfo.complete) return;

        switch (mapInfo.type)
        {
            case IMap.mapType.fight_normal:
                element.sprite = img_monster;
                break;

            case IMap.mapType.fight_boss:
                element.sprite = img_boss;
                break;

            case IMap.mapType.collectable_equipment:
                element.sprite = img_equipment;
                break;

            case IMap.mapType.collectable_talent:
                element.sprite = img_talent;
                break;
            default:
                element.gameObject.SetActive(false); break;
        }
    }

    public Image graphic;

    public Color32 imgColor_normal, imgColor_nonVisited, imgColor_nearAVisibleMap;

    public void SetImageColor()
    {
        if (VisibleMap.contain(pos))
        {
            if (!VisitedMap.contain(pos))
                graphic.color = imgColor_nonVisited;
            else
                graphic.color = imgColor_normal;
        }
        else if (VisibleMap.NearAVisibleMap(pos))
        {
            graphic.color = imgColor_nearAVisibleMap;
        }
    }
}
