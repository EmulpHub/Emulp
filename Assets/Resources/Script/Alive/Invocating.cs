using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invocating : MonoBehaviour
{
    public static Entity CreateEntity(CreaterInfoEntity createrInfo)
    {
        Entity entity = Instantiate(createrInfo.getPrefab()).GetComponent<Entity>();

        createrInfo.position = F.NearTileWalkable(createrInfo.position, AliveEntity.Instance.GetListPosition());

        entity.SetPosition(createrInfo.position);

        createrInfo.ModifiyEntity(entity);

        return entity;
    }

    public static void InvokeMonster(CreaterInfoMonster createrInfo, Monster caster)
    {
        Monster m = CreateEntity(createrInfo).GetComponent<Monster>();

        m.monsterInfo.IsAnInvocation = true;

        var customEffect = new List<(Effect.effectType type, int strenght, string title, string description)>();

        string title = "Invoqué";
        string description = "Si " + caster.Info.EntityName + " meurt cette créature meurt avec lui";

        if (V.IsUk())
        {
            title = "Invoked";
            description = "If " + caster.Info.EntityName + " die this creature die aswell";
        }

        customEffect.Add((Effect.effectType.custom, 1, title, description));

        m.AddEffect(
            Effect.CreateCustomTxtEffect(title, description, 0, V.invoked_effect_sprite, Effect.Reduction_mode.never)
            );

        caster.invoke_list.Add(m);

        EntityOrder.InstanceEnnemy.Add(m);
    }

}

public abstract class CreaterInfoEntity
{
    public string position;

    protected CreaterInfoEntity(string position)
    {
        this.position = position;
    }

    public virtual GameObject getPrefab() { return null; }

    public virtual void ModifiyEntity(Entity entity) { }
}

public class CreaterInfoMonster : CreaterInfoEntity
{
    public int level;
    public bool Alpha;
    public MonsterInfo.MonsterType type;

    public CreaterInfoMonster(string position, int level, bool alpha, MonsterInfo.MonsterType type) : base(position)
    {
        this.level = level;
        Alpha = alpha;
        this.type = type;
    }

    public override GameObject getPrefab()
    {
        return V.script_Scene_Main.monster_prefab;
    }

    public override void ModifiyEntity(Entity entity)
    {
        Monster monster = (Monster)entity;

        monster.SetType(type, level);

        monster.Alpha = Alpha;
    }
}