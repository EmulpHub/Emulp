using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFindingName;
using System.Linq;

public class Action_movement : Action
{
    public Action_movement(PathParam pathParam, Entity entity) : base()
    {
        type = Type.movement;

        this.pathParam = pathParam;
        this.entity = entity;
    }

    public PathParam pathParam;
    public Entity entity;

    public List<Tile_Graphic> listTilePath;

    protected override IEnumerator Execute_main()
    {
        entity.MoveTo(pathParam);

        TileInfo.Instance.ListTile_Clear();

        var path = entity.runningInfo.walkablePath;

        List<Tile> listTileCreated = new List<Tile>();

        float totalSpeedToCreate = 0.2f;

        bool isMonster = false;
        Monster entityMonster = null;

        if (entity is Monster monster)
        {
            entityMonster = monster;
            isMonster = true;
        }

        for (int i = 0; i < path.Count; i++)
        {
            var pos = path[i];

            var data = new TileData_graphic(pos, Tile_Gestion.Color.green_light, TileData.Layer.low);

            data.SetListTileDependancy(path);
            data.SetIgnoreAllEntity(true);

            var newTile = Tile_Graphic.Add(data, false);

            newTile.SetOutline();

            if(isMonster)
            {
                entityMonster.uniqueCarac.Uniquify(newTile);    
            }

            var animScaleStart = new AnimTileData_Scale(0.5f, 1, 0.4f);

            newTile.Animate(animScaleStart);

            listTileCreated.Add(newTile);

            float time = totalSpeedToCreate / path.Count;

            yield return new WaitForSeconds(time);
        }

        for (int i = 0; i < listTileCreated.Count; i++)
        {
            var tile = listTileCreated[i];

            var animScaleEnd = new AnimTileData_Scale(0.8f, 0, 0.5f)
                .SetDelay(1)
                .SetDestroyWhenFinish(0.5f);

            tile.Animate(animScaleEnd);

            yield return new WaitForSeconds(entity.runningInfo.speed);
        }

        yield return null;
    }

    public override bool IsFinished()
    {
        return !entity.runningInfo.running && base.IsFinished();
    }

    public static void Add(PathParam pathParam, Entity entity)
    {
        ActionManager.Instance.AddToDo(Create(pathParam, entity));
    }

    public static Action_movement Create(PathParam pathParam, Entity entity)
    {
        return new Action_movement(pathParam, entity);
    }

    public override string debug()
    {
        return descColor.convert("move for " + entity.Info.EntityName);
    }
}
