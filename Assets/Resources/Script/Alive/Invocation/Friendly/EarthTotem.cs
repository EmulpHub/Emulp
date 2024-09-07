using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthTotem : Invocation
{
    [HideInInspector]
    public EarthTotemInfo info_earthTotem { get => (EarthTotemInfo)Info; }

    int idEffect = 0;

    public override void OnStart()
    {
        base.OnStart();

        idEffect = V.player_entity.event_turn_end.Add(DoEffect);

        DoEffect(V.player_entity);
    }

    public override void Kill(InfoKill infoKill)
    {
        if (isKilled) return;

        var spell = SpellInToolbar.get(SpellGestion.List.warrior_jump);

        if (spell is not null)
            spell.AddFreePaUse(1);

        V.player_entity.event_turn_end.Remove(idEffect);

        DoEffect(this);

        base.Kill(infoKill);
    }

    public void DoEffect(Entity caster)
    {
        foreach (Monster m in AliveEntity.listMonster)
        {
            int distance = F.DistanceBetweenTwoPos(m, this);

            if (distance <= 2)
            {
                m.Damage(new InfoDamage(info_earthTotem.baseDamage, caster));

                spellAnimation.anim_simple("EarthTotem", V.CalcEntityDistanceToBody(m));

                SoundManager.PlaySound(SoundManager.list.spell2_warrior_EarthTotemDamage);

                if (m.IsDead())
                    warrior_spent.AddAcumulation(V.player_entity,2 + V.player_entity.InfoPlayer.eff);

            }
        }
    }

}

public class CreaterInfoEarthTotem : CreaterInfoEntity
{
    public int pvMax;
    public float baseDamage;

    public CreaterInfoEarthTotem(string position, int pvMax, float baseDamage) : base(position)
    {
        this.pvMax = pvMax;
        this.baseDamage = baseDamage;
    }

    public override GameObject getPrefab()
    {
        return Resources.Load<GameObject>("Prefab/Entity/Friendly/EarthTotem");
    }

    public override void ModifiyEntity(Entity entity)
    {
        EarthTotem earthTotem = ((EarthTotem)entity);

        EarthTotemInfo earthTotemInfo = new EarthTotemInfo();

        earthTotem.Info = earthTotemInfo;

        earthTotemInfo.basePvMax = pvMax;
        earthTotemInfo.Life = pvMax;
        earthTotemInfo.baseDamage = baseDamage;

        earthTotem.InvocType = Invocation.invocType.totem;

        earthTotemInfo.InitInfo(earthTotem);

        entity.Info.CreateLifeBar(earthTotem);
    }
}