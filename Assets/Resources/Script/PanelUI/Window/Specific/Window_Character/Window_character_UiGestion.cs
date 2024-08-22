using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Window_character : Window
{
    private Sprite _playerProfil;

    public Sprite PlayerProfile
    {
        get
        {
            if (_playerProfil is null)
                _playerProfil = Resources.Load<Sprite>("Image/Player/Player_profil");
            return _playerProfil;
        }
    }

    public Entity entity;

    public Text life, mastery, mastery_point, pa, pa_max, pm, pm_max, tackle, tackle_point, leak, leak_point, effect_title,
        damage, damage_point, po, po_point, heal, armor, armor_point, heal_point, cc, cc_point, ec, ec_point;

    public Image Render, life_img;

    public override void Update_Ui()
    {
        base.Update_Ui();

        EntityInfo info = entity.Info;
        PlayerInfo infoPlayer = V.player_entity.InfoPlayer;

        int value_life = Mathf.FloorToInt(info.Life);
        int value_lifeMax = Mathf.FloorToInt(info.Life_max);

        Monster m = null;
        if (entity is Monster monsterScript)
            m = monsterScript;

        int value_mastery = 999;

        int value_pa = info.PA;
        int value_paMax = info.PA_max;
        int value_pm = info.PM;
        int value_pmMax = info.PM_max;
        int value_tackle = info.tackle;
        int value_leak = info.leak;

        int value_cc = info.cc;
        int value_ec = info.ec;

        int value_damage = 0;
        int value_po = 0;
        int value_heal = 0;
        int value_armor = 0;

        if (entity.IsPlayer())
        {
            value_damage = infoPlayer.damage;
            value_po = infoPlayer.po;
            value_heal = (int)infoPlayer.HealEfficacity;
            value_armor = (int)infoPlayer.armorEfficacity;

            value_mastery = infoPlayer.str;

            Render.sprite = PlayerProfile;
        }
        else if (entity.IsMonster())
        {
            value_mastery = Mathf.FloorToInt(m.monsterInfo.str);

            Render.sprite = Resources.Load<Sprite>("Image/Monster/profil/" + m.monsterInfo.monster_type.ToString());
        }

        mastery.gameObject.SetActive(value_mastery != 999);
        effect_title.gameObject.SetActive(
            ShowEffectTitle());

        AssignateDesc(mastery.gameObject, StatsType.mastery, value_mastery);
        AssignateDesc(tackle.gameObject, StatsType.tackle, value_tackle);
        AssignateDesc(leak.gameObject, StatsType.leak, value_leak);
        AssignateDesc(damage.gameObject, StatsType.damage, value_damage);
        AssignateDesc(po.gameObject, StatsType.po, value_po);
        AssignateDesc(heal.gameObject, StatsType.heal, value_heal);
        AssignateDesc(armor.gameObject, StatsType.armor, value_armor);

        AssignateDesc(cc.gameObject, StatsType.cc, value_cc);
        AssignateDesc(ec.gameObject, StatsType.ec, value_ec);

        pa.text = "" + value_pa;
        pa_max.text = "" + value_paMax;
        pm.text = "" + value_pm;
        pm_max.text = "" + value_pmMax;

        life_img.fillAmount = (float)value_life / (float)value_lifeMax;

        if (V.IsFr())
        {
            if (ItsPlayer)
                title.text = "Personnage";
            else
                title.text = descColor.convert_noColorChange(entity.Info.EntityName);

            life.text = "" + value_life + "\n" + value_lifeMax;

            mastery.text = "Force : ";
            mastery_point.text = "" + value_mastery;

            effect_title.text = "Effet :";

            tackle.text = "Tacle : ";
            tackle_point.text = "" + value_tackle;

            leak.text = "Fuite : ";
            leak_point.text = "" + value_leak;

            damage.text = "Dommage : ";
            damage_point.text = "" + value_damage;

            po.text = "Portée : ";
            po_point.text = "" + value_po;

            heal.text = "Soins : ";
            heal_point.text = "+" + value_heal + "%";

            armor.text = "Armure : ";
            armor_point.text = "+" + value_armor + "%";

            cc.text = "Chance critique : ";
            cc_point.text = "" + value_cc + "%";

            ec.text = "Effet critique : ";
            ec_point.text = "+" + value_ec + "%";
        }
        else if (V.IsUk())
        {
            if (ItsPlayer)
                title.text = "Character";
            else
                title.text = descColor.convert_noColorChange(entity.Info.EntityName);

            life.text = "" + value_life + "\n" + value_lifeMax;

            mastery.text = "Strenght : ";
            mastery_point.text = "" + value_mastery;

            effect_title.text = "Effect :";

            tackle.text = "Tackle : ";
            tackle_point.text = "" + value_tackle;

            leak.text = "Leak : ";
            leak_point.text = "" + value_leak;

            damage.text = "Damage : ";
            damage_point.text = "" + value_damage;

            po.text = "Range : ";
            po_point.text = "" + value_po;

            heal.text = "Heal : ";
            heal_point.text = "+" + value_heal + "%";

            armor.text = "Armor : ";
            armor_point.text = "+" + value_armor + "%";

            cc.text = "Critical chance : ";
            cc_point.text = "" + value_cc + "%";

            ec.text = "Critical effect : ";
            ec_point.text = "+" + value_ec + "%";
        }

        UpdateSizeDependingOfEffect();
    }

    public bool ShowEffectTitle()
    {
        if (entity.IsPlayer())
            return entity.HaveEffect();
        else if (entity.IsMonster())
        {
            Monster m = (Monster)entity;

            return entity.GetListShowedEffect().Count > (m.ContainPassive ? 1 : 0);
        }

        return false;
    }

    public enum StatsType { life, mastery, pa, pm, tackle, leak, pc, damage, po, heal, armor, spike, cc, ec }

    public void AssignateDesc(GameObject customDescG, StatsType type, float value)
    {
        CustomDescription customDesc = customDescG.GetComponent<CustomDescription>();

        if (ItsPlayer)
        {
            if (type == StatsType.life)
            {
                customDesc.Set("Vie",
                    "Lorsque votre vie tombe a *mal0*end vous perdez le combat",
                    "Life",
                    "When your life reach *mal0*end you loose the fights"); //titre_fr, desc_fr,titre_uk,desc_uk
            }
            else if (type == StatsType.mastery)
            {
                customDesc.Set("Force",
                    "Augmente vos *bondégâts infligés*end de *bon" + value + "%*end",
                    "Strenght",
                    "Increases your *bondamage dealt*end by *bon" + value + "%*end");
            }
            else if (type == StatsType.pa)
            {
                customDesc.Set("Point d'action",
                    "*exp Les points d'action *end vous permettent de lancer des *bon sorts*end ou *bon actions*end en combat",
                    "Action point",
                    "*exp Action points*end allow you to cast *bon spells*end or *bon actions*end in combat");
            }
            else if (type == StatsType.pm)
            {
                customDesc.Set("Point de mouvement",
                    "*pdm Les points de mouvement*end vous permettent de vous *bon deplacer *end en combat.",
                    "Movement point",
                    "*pdm Movement points *end allow you to *bon move*end in combat.");
            }
            else if (type == StatsType.tackle)
            {
                customDesc.Set("Tacle",
                    "Le *spetacle*end *bon ralentit*end les entités ennemis a votre contact",
                    "Tackle",
                    "*spe Tackle*end *bon slow*end ennemy at your contact");
            }
            else if (type == StatsType.leak)
            {
                customDesc.Set("Fuite",
                    "La *spe fuite*end vous permet de *bon resister*end au *bontacle*end de vos ennemis lorsque vous etes a leur contact",
                    "Leak",
                    "*spe Leak*end allow you to *bonresist*end to the ennemy's *spe tackle*end when you are at their contact");
            }
            else if (type == StatsType.pc)
            {
                customDesc.Set("Points de competence",
                    "*exp Les points de competences*end vous permettent de débloquer des *bon sorts*end dans la fenetre des competences",
                    "Skill point",
                    "*exp Skill points*end allow you to unlock *bon spells*end in the skill window");
            }
            else if (type == StatsType.damage)
            {
                customDesc.Set("Dégâts",
    "Vos dégats sont augmentés de *bon" + V.player_info.damage + "*end",
    "Damage",
    "Your damage is inceased by *bon" + value + "*end");
            }
            else if (type == StatsType.po)
            {
                customDesc.Set("Portée",
    "Vos sorts ont *bon" + value + "*end portée supplementaire",
    "Range",
    "Your spells have *bon" + value + "*end more range");

            }
            else if (type == StatsType.spike)
            {
                customDesc.Set("Épines",
"Lorsque un monstre vous inflige des dégâts vous lui infligez *dmg" + value + "*end dégâts",
"Spike",
"When a monster attack you deal him *dmg" + value + "*end damage");
            }
            else if (type == StatsType.heal)
            {
                customDesc.Set("Soins",
"Vos soins sont augmentés de *bon" + value + "%*end",
"Heal",
"Your heal are increased by *bon" + value + "%*end");
            }
            else if (type == StatsType.armor)
            {
                customDesc.Set("Armure",
"Vos gain d'armure sont augmentés de *bon" + value + "%*end",
"Armor",
"Your armor gain are increased by *bon" + value + "%*end");
            }
            else if (type == StatsType.cc)
            {
                customDesc.Set("Chance critique",
"Vos chance de coup critique quand vous lancez un sort sont de *bon" + value + "%*end\nVous gagnez *bon1% d'effets critique*end supplementaire pour chaque chance critique au dessus de *bon100%*end",
"Critical chance",
"Your critical chance when you launch a spell is *bon" + value + "%*end\nYou gain *bon1% critical effect*end for each critical hit chance above *bon100%*end");
            }
            else if (type == StatsType.ec)
            {
                customDesc.Set("Effet critique",
"Vos coup critique ont *bon+" + value + "%*end d'effets supplementaire",
"Critical effect",
"Your critical strike have *bon+" + value + "%*end more effect");
            }
        }
        else
        {
            if (type == StatsType.life)
            {
                customDesc.Set("Point de vie",
                    "Lorsque la vie de ce monstre tombe a *mal 0*end il meurt",
                    "Life point",
                    "When this monster's life reach *mal 0*end he die"); //titre_fr, desc_fr,titre_uk,desc_uk
            }
            else if (type == StatsType.mastery)
            {
                customDesc.Set("Force",
                    "Augmente les *dmgdégâts infligés*end de *bon" + value + "%*end",
                    "Strenght",
                    "Increases *dmgdamage dealt*end by *bon" + value + "%*end");
            }
            else if (type == StatsType.pa)
            {
                customDesc.Set("Point d'action",
                    "*exp Les points d'action*end permettent a ce monstre de lancer des *bon sorts*end",
                    "Action point",
                    "*exp Action points*end of this monster allow him to cast *bonspells*end");
            }
            else if (type == StatsType.pm)
            {
                customDesc.Set("Point de mouvement",
                    "*pdm Les points de mouvement*end permettent a ce monstre de se *bon deplacer*end",
                    "Movement point",
                    "*pdm Movement points*end allow this monster to *bon move*end in combat.");
            }
            else if (type == StatsType.tackle)
            {
                int tackledAmount = EntityInfo.CalcTackleEffect(V.player_entity, entity);

                if (tackledAmount > 0)
                {
                    customDesc.Set("Tacle",
    "Ce monstre vous tacle de *mal" + tackledAmount + " cases*end",
    "Tackle",
    "This monster tackle you *mal" + tackledAmount + " tile*end");
                }
                else
                {
                    customDesc.Set("Tacle",
    "Ce monstre *bonne vous tacle pas*end",
    "Tackle",
    "This monster *bondont tackle you*end");
                }
            }
            else if (type == StatsType.leak)
            {
                int tackledAmount = EntityInfo.CalcTackleEffect(entity, V.player_entity);

                if (tackledAmount > 0)
                {
                    customDesc.Set("Fuite",
                        "Vous tacklez ce monstre de *mal" + tackledAmount + " cases*end",
                        "Leak",
                        "You tackle *bon" + tackledAmount + " tile*end from this monster");
                }
                else
                {
                    customDesc.Set("Fuite",
                        "Vous ne taclez pas ce monstre",
                        "Leak",
                        "You dont tackle this monster");
                }
            }
            else if (type == StatsType.damage)
            {
                customDesc.Set("Dégâts",
    "Inflige *dmg" + value + " dégats*end supplementaire",
    "Damage",
    "Deal *dmg " + value + " *end more damage");
            }
            else if (type == StatsType.po)
            {
                customDesc.Set("Portée",
    "Portée augmenté de *bon" + value + "*end case",
    "Range",
    "Increase range by *bon" + value + "*end square");

            }
            else if (type == StatsType.spike)
            {
                customDesc.Set("Épines",
"Lorsque un monstre vous inflige des dégâts vous lui infligez *dmg" + value + "*end dégâts",
"Spike",
"When a monster attack you deal him *dmg" + value + "*end damage");
            }
            else if (type == StatsType.heal)
            {
                customDesc.Set("Soins",
    "Ce monstre a des soins aumentés *bon" + value + "%*end",
    "Heal",
    "This monster have *bon" + value + "%*end more heal");

            }
            else if (type == StatsType.armor)
            {
                customDesc.Set("Armure",
"Gain d'armure augmenté de *bon" + value + "%*end",
"Armor",
"Armor gain increased by *bon" + value + "%*end");
            }
            else if (type == StatsType.cc)
            {
                customDesc.Set("Chance critique",
"*bon" + value + "%*end chance de critique",
"Critical chance",
"*bon" + value + "%*end critical chance");
            }
            else if (type == StatsType.ec)
            {
                customDesc.Set("Effet critique",
"*bon+" + value + "%*end effets critique",
"Critical effect",
"*bon+" + value + "%*end critical effect");
            }
        }
    }

    public void AssignateDesc(GameObject customDescG, StatsType type)
    {
        AssignateDesc(customDescG, type, 0);
    }
}
