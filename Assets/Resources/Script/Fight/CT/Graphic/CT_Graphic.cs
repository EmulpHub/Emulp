using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CT_Graphic : Tile
{
    private static GameObject _prefab;

    private static GameObject Prefab
    {
        get
        {
            if (_prefab == null)
                _prefab = Resources.Load<GameObject>("Prefab/CT/CT_Graphic");

            return _prefab;
        }
    }

    public static (GameObject tile, Tile tileScript) Add(string pos, Tile_Gestion.Color color, bool resetNearSprite = true, bool IgnoreAllEntity = false, bool IgnoreMouseOver = false, List<string> customCombatTileList = null, bool startAnimation = true, int layer = 0)
    {
        Tile script = Instantiate(Prefab, parent).GetComponent<Tile>();

        script.graphic_startAnimation = startAnimation;

        if (!script.graphic_startAnimation)
            script.transform.localScale = new Vector3(1, 1, 1);

        script.CurrentColor = color;

        script.Initialize(Type.graphic, 255, layer, IgnoreAllEntity, IgnoreMouseOver, pos);

        script.customListTile = customCombatTileList;

        TileInfo.Instance.Add(script);

        if (resetNearSprite)
        {
            if (customCombatTileList == null)
                customCombatTileList = new List<string>(TileInfo.Instance.listTilePos);

            customCombatTileList = new List<string>(customCombatTileList);

            if (!customCombatTileList.Contains(pos))
                customCombatTileList.Add(pos);

            TileInfo.Instance.ResetNearSprite(pos, IgnoreAllEntity, customCombatTileList);
        }

        return (script.gameObject, script);
    }

    public static bool IsWalkableByTheplayer(int distance)
    {
        return distance <= V.player_info.GetRealPm();
    }

    public override void SetNormalColor()
    {
        ChangeColor(CurrentColor);
    }
}
