using PathFindingName;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileData : MonoBehaviour
{
    public enum Layer { low, normal, spell }

    public Layer layer { get; private set; } = Layer.normal;

    public int additionalSortingOrder { get; private set; }

    public enum Type { movement, graphic, spell }

    public Type type { get; private set; }

    private string _pos;

    public string pos
    {
        get
        {
            return _pos;
        }
        private set
        {
            _pos = value;

            posVector2 = F.ConvertToWorldVector2(_pos);
        }
    }

    public Vector2 posVector2 { get; private set; }

    public bool DoScaleApparition { get; private set; } = false;

    private List<string> _listTileDependancy;

    public List<string> listTileDependancy
    {
        get
        {
            if (_listTileDependancy == null)
                _listTileDependancy = TileInfo.Instance.GetListPos();

            return _listTileDependancy;
        }
        set
        {
            _listTileDependancy = value;
        }
    }

    public bool ignoreAllEntity { get; private set; }

    public TileData(string pos, Type type, Layer layer = Layer.normal)
    {
        this.pos = pos;
        this.type = type;
        this.layer = layer;
    }

    public TileData SetIgnoreAllEntity(bool ignoreAllEntity)
    {
        this.ignoreAllEntity = ignoreAllEntity;
        return this;
    }

    public TileData SetDoScaleApparition(bool doScaleApparition)
    {
        this.DoScaleApparition = doScaleApparition;
        return this;
    }

    public TileData SetListTileDependancy(List<string> listTileDependancy)
    {
        this.listTileDependancy = listTileDependancy;
        return this;
    }

    public TileData SetAdditionalSortingOrder (int additionalSortingOrder)
    {
        this.additionalSortingOrder = additionalSortingOrder;
        return this;
    }
}

public class TileData_graphic : TileData
{
    public TileData_graphic(string pos, Tile_Gestion.Color color = Tile_Gestion.Color.pink, Layer layer = Layer.normal) : base(pos, Type.graphic, layer)
    {
        this.color = color;
    }

    private bool ContainOverrideColor = false;
    public Color32 overrideColor { get; private set; }

    public TileData_graphic SetOverrideColor(Color32 color)
    {
        ContainOverrideColor = true;
        overrideColor = color;
        return this;
    }

    public Tile_Gestion.Color color { get; private set; }

    public Color32 ChooseColor()
    {
        if (ContainOverrideColor)
            return overrideColor;
        else
            return Tile_Gestion.Instance.ConvertEnumColorIntoColor32(color);
    }
}

public class TileData_spell : TileData
{
    public TileData_spell(string pos, bool lineOfView, Layer layer = Layer.normal) : base(pos, Type.spell, layer)
    {
        this.lineOfView = lineOfView;
    }

    public bool lineOfView { get; private set; }
}

public class TileData_movement : TileData
{
    public TileData_movement(string pos, int distance, Layer layer = Layer.normal) : base(pos, Type.movement, layer)
    {
        this.distance = distance;
    }

    public int distance { get; private set; }

    private List<string> _path;

    public List<string> path
    {
        get
        {
            if (_path == null)
            {
                _path = Path.Find(new PathParam(V.player_entity.CurrentPosition_string, pos)).path;
            }

            return _path;
        }
    }
}