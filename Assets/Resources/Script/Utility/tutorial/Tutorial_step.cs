using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class step_intro : step
{
    public override void restrictionManagement(bool apply)
    {
        base.restrictionManagement(apply);

        List<Main_Object.objects> ls =
            new List<Main_Object.objects> { Main_Object.objects.button_skill, Main_Object.objects.button_character, Main_Object.objects.button_equipment };

        if (apply)
            Main_Object.Enable_list(ls);
        else
            Main_Object.Disable_list(ls);
    }

    public override void takeEffect()
    {
        base.takeEffect();

        restrictionManagement(true);

        windowTxt.ChangeTitle(V.IsFr() ? "Bienvenue !" : "Hello !");

        windowTxt.CreateContent(new List<Window_Txt_line> {
            Window_Txt_line.create_txt(V.IsFr() ? "Bienvenue dans Emulp ! Un monde magnifique envahis par de terribles monstres >:(" : "Welcome to Emulp ! A wonderful world taken by invicious monster >:("),
            Window_Txt_line.create_img(Resources.Load<Sprite>("Image/StoryTutorial/LotOfMonster")),
            Window_Txt_line.create_txt(V.IsFr() ? "votre objectif et de tous les tués et pour ça leur leader Vela doit mourir !" : "your goal is to kill them all and to do so their leader Vela must die !"),
            Window_Txt_line.create_txt(V.IsFr() ? "mais d'abord je vais vous apprendre à TUER": "but first let's teach you how to KILL"),
            Window_Txt_line.create_button("GO",
            () => {
                    exitButton();

            })
        });
    }
}

public class step_movementOutOfCombat : step
{
    public override void restrictionManagement(bool apply)
    {
        base.restrictionManagement(apply);

        if (apply)
            ClickAutorization.Exception_Add(Main_Object.Get(Main_Object.objects.Map_possibleToMove));
    }

    public override void takeEffect()
    {
        base.takeEffect();

        restrictionManagement(true);

        windowTxt.ChangeTitle(V.IsFr() ? "Se déplacer" : "Move now");

        windowTxt.CreateContent(new List<Window_Txt_line> {
            Window_Txt_line.create_txt(V.IsFr() ? "Dabord vous devez explorez ce monde, allez a la carte sur votre droite" : "First you have to Move to explore the world, go to the map to your right"),
            Window_Txt_line.create_video(Resources.Load<VideoClip>("video/tutoVideo_deplacment_short"))
            });
    }

    public override bool finished()
    {
        return WorldData.PlayerPositionInWorld != "-6_-11";
    }
}

public class step_startAfight : step
{
    public override void restrictionManagement(bool apply)
    {
        base.restrictionManagement(apply);
    }

    public override void takeEffect()
    {
        base.takeEffect();

        restrictionManagement(true);

        windowTxt.ChangeTitle(V.IsFr() ? "Début du cobmat" : "Start a combat");

        windowTxt.CreateContent(new List<Window_Txt_line> {
            Window_Txt_line.create_txt(V.IsFr() ? "Un monstre est ici, Vous devez le tuer ! Commencer un combat contre lui !" : "A monster is here, You must kill him ! Start a fight with him !"),
            });

        foreach (Entity e in AliveEntity.list)
        {
            if (e.IsMonster())
            {
                tuto.m = (Monster)e;
            }
        }

        ClickAutorization.Exception_Add(tuto.m.gameObject);

    }

    public override bool finished()
    {
        return V.game_state == V.State.positionning;
    }
}

public class step_positionning : step
{
    public override void restrictionManagement(bool apply)
    {
        base.restrictionManagement(apply);

    }

    public override void takeEffect()
    {
        base.takeEffect();

        restrictionManagement(true);

        windowTxt.ChangeTitle(V.IsFr() ? "Phase de preparation" : "Preparation phase");

        windowTxt.CreateContent(new List<Window_Txt_line> {
            Window_Txt_line.create_txt(V.IsFr() ? "Vous êtes en phase de préparation, pour l'instant vous n'avez rien à faire donc vous pouvez lancer le combat" : "You'r in your preparation phase, for now you don't have anything to do, so start the fight !"),
            Window_Txt_line.create_video(Resources.Load<VideoClip>("video/tutoVideo_StartCombat_fr")),

            });

        ClickAutorization.Exception_Add(Main_Object.Get(Main_Object.objects.button_endOfTurn));


    }

    public override bool finished()
    {
        return V.game_state == V.State.fight;
    }
}

public class step_yourTurn : step
{
    public override void restrictionManagement(bool apply)
    {
        base.restrictionManagement(apply);

    }

    public override void takeEffect()
    {
        base.takeEffect();

        restrictionManagement(true);

        ClickAutorization.Exception_Remove(Main_Object.Get(Main_Object.objects.button_endOfTurn));

        windowTxt.ChangeTitle(V.IsFr() ? "Votre tour" : "Your turn");

        windowTxt.CreateContent(new List<Window_Txt_line> {
            Window_Txt_line.create_txt(V.IsFr() ?"C'est votre tour, rapprochez vous de lui de 3 cases en utilisant vos pm" :"It' your turn, go closer to him by moving 3 square using your mp"),
            Window_Txt_line.create_video(Resources.Load<VideoClip>("video/tutoVideo_movementInCombat")),
            Window_Txt_line.create_txt(V.IsFr() ? "1 pm = 1 case, vous avez 3 pm au début du tour": "1 mp = 1 square, you have 3 pm at the start of your turn")
            });

        ForceHelp.Instantiate_ForceMovement_TowardEntity(tuto.m, true);
    }

    public override bool finished()
    {
        return V.player_info.PM == 0;
    }
}

public class step_receiveSpell : step
{
    public override void takeEffect()
    {
        base.takeEffect();

        restrictionManagement(true);

        windowTxt.ChangeTitle(V.IsFr() ? "Votre premier sort" : "Your first spell");

        windowTxt.CreateContent(new List<Window_Txt_line> {
            Window_Txt_line.create_txt(V.IsFr() ? "Vous êtes suffisamment proche pour infligez des dégâts, mais vous n'avez aucun sort, en voila un" : "Now you'r close enough to deal damage, but you don't have any spell, here your first one"),
            Window_Txt_line.create_button(V.IsFr() ?"Recevoir mes premiers sorts" :"Receive my first spell",() =>
            {
                if(exitButton()) {
                    SpellGestion.AddStartingSpellToTheToolbar();
                }
            })
        });
    }
}

public class step_useYourSpell : step
{
    public override void restrictionManagement(bool apply)
    {
        base.restrictionManagement(apply);

    }

    public override void takeEffect()
    {
        base.takeEffect();

        restrictionManagement(true);

        windowTxt.ChangeTitle(V.IsFr() ? "Infligez des dégâts" : "Deal damage");

        windowTxt.CreateContent(new List<Window_Txt_line> {
            Window_Txt_line.create_txt(V.IsFr() ? "Ce sort inflige des dégâts, utilise le sur le monstre" : "That spell deal damage to your ennemy, use it against him"),
                        Window_Txt_line.create_video(Resources.Load<VideoClip>("video/tutoVideo_AttackEnnemy_Fr")),
            Window_Txt_line.create_txt(V.IsFr() ? "certains sorts coute des pa pour être lancé, ce sort coute 3 pa, vous avez 6 pa au début du tour" : "some spell need Ap to be launched, this spell need 3, you have 6 ap each turn")
        });

        foreach (Spell sp in SpellInToolbar.activeSpell_script)
        {
            if (sp.spell == SpellGestion.List.base_fist)
            {
                ClickAutorization.Exception_Add(sp.gameObject);
            }
        }

        ClickAutorization.Exception_Add(Main_Object.Get(Main_Object.objects.button_endOfTurn));
    }

    public override bool finished()
    {
        return !EntityOrder.IsTurnOf_Player();
    }
}

public class step_monsterTurn : step
{
    public override void takeEffect()
    {
        base.takeEffect();

        restrictionManagement(true);

        windowTxt.ChangeTitle("");

        windowTxt.CreateContent(new List<Window_Txt_line> {
            Window_Txt_line.create_txt(V.IsFr() ?"C'est au tour du monstre" : "It's the monster turn !"),
        });
    }

    public override bool finished()
    {
        return EntityOrder.IsTurnOf_Player();
    }
}

public class step_newTurn : step
{
    public override void takeEffect()
    {
        base.takeEffect();

        restrictionManagement(true);

        windowTxt.ChangeTitle(V.IsFr() ? "Votre tour" : "Your turn");

        windowTxt.CreateContent(new List<Window_Txt_line> {
            Window_Txt_line.create_txt( V.IsFr() ?"C'est votre tour, vous avez gagné 6 pa et 3 pm" : "It's your turn, you gained 6 ap and 3 mp"),
            Window_Txt_line.create_img(Resources.Load<Sprite>("Image/StoryTutorial/LifeDesc_" + (V.IsFr() ? "fr" :"uk"))),
            Window_Txt_line.create_txt(V.IsFr() ? "continuez à frapper vous le tuerez !" : "keep fighting you will kill him !"),
        });
    }

    public override bool finished()
    {
        return V.game_state != V.State.fight;
    }
}

public class step_youWin : step
{
    public override void restrictionManagement(bool apply)
    {
        base.restrictionManagement(apply);

        if (!apply)
        {
            ClickAutorization.Remove(tuto.gameObject);
        }
    }

    public override void takeEffect()
    {
        base.takeEffect();

        restrictionManagement(true);
        /*
        windowTxt.event_Close -= tuto.CloseButton;

        windowTxt.ChangeTitle(V.IsFr() ? "Vous avez gagné" : "You win");

        windowTxt.CreateContent(new List<Window_Txt_line> {
            Window_Txt_line.create_txt(V.IsFr() ? "Vous avez gagné ! A chaque victoire vous gagnez de l'xp et parfois d'autre ressources" : "You win ! Each time you win you gain Xp and sometime lot of stuff"),
            Window_Txt_line.create_txt(V.IsFr() ? "Explorez le monde et tuer des monstres, et rappelez-vous vous devez trouver Vela et l'eliminer a tout prix !" : "Explore the world and kill monster, and remember you have to find that Vela and kill him at all cost !"),
            Window_Txt_line.create_img(Resources.Load<Sprite>("Image/StoryTutorial/VelaWantedDead")),
            Window_Txt_line.create_txt(V.IsFr() ?"Je serais bientôt de retour, d'ici la bonne chance !" : "i will soon be back until then good luck !"),
            Window_Txt_line.create_button ( V.IsFr() ? "À bientot" :"See you soon",() =>
            {
                if(exitButton()) {
                    windowTxt.Close_Force();
                    Main_UI.Toolbar_AddSpell(SpellGestion.List.base_fist);
                    windowTxt.event_Close += tuto.CloseButton;
                    restrictionManagement(false);
                }
            })
        });
        */
    }

    public override bool finished()
    {
        return false;
        //return Panel_button.ExistWindowOfLink(Panel_button.En_link.skill);
    }
}

public class step_skillTree : step
{
    public override void takeEffect()
    {
        base.takeEffect();

        restrictionManagement(true);

        windowTxt.Open();

        windowTxt.ChangeTitle(V.IsFr() ? "Compétence" : "Skill");

        windowTxt.CreateContent(new List<Window_Txt_line> {
            Window_Txt_line.create_txt(V.IsFr() ? "Bienvenue dans votre arbre de compétence ! Ici vous pouvez récuperez de nouvelles compétences avec vos points de compétence, essayez d'obtenir une nouvelle compétence" : "Welcome to your skill tree ! Here you can gather new skill with your skill point, try get a new skill"),
            Window_Txt_line.create_video(Resources.Load<VideoClip>("video/tutoVideo_equip"))
        });
        /*
        Window_skill s = (Window_skill)Panel_button.GetWindowOfLink(Panel_button.En_link.skill).w;

        s.DontAllowClickOnCloseButton = true;
        */
        Panel_skill_talent.deactivateTalentShow = true;

        ClickAutorization.forbidden_Add(Main_Object.Get(Main_Object.objects.button_skill));
    }

    public override bool finished()
    {
        return V.player_info.point_skill == 0;
    }
}

public class step_talent : step
{
    public override void takeEffect()
    {
        base.takeEffect();

        restrictionManagement(true);

        windowTxt.ChangeTitle("Talent");

        windowTxt.CreateContent(new List<Window_Txt_line> {
            Window_Txt_line.create_txt(V.IsFr() ? "Vous pouvez aussi obtenir des talents qui vous offre des bonus permanent en combat" :  "You can also get talent that offer permanent bonus in combat"),
            Window_Txt_line.create_img(Resources.Load<Sprite>("Image/StoryTutorial/Talent")),
            Window_Txt_line.create_txt(V.IsFr() ? "Voici votre premier talent" : "I'll give you your first talent"),
            Window_Txt_line.create_button(V.IsFr() ? "Recevoir mon premier talent" : "Receive my first talent",() =>
            {
                if(exitButton())
                    Talent_Gestion.UnlockTalent(Talent_Gestion.talent.power);
            })
        });
    }
}

public class step_EquipTalent : step
{
    public override void takeEffect()
    {
        base.takeEffect();

        restrictionManagement(true);

        Panel_skill_talent.deactivateTalentShow = false;

        windowTxt.ChangeTitle(V.IsFr() ? "Equiper un talent" : "Equip talent");

        windowTxt.CreateContent(new List<Window_Txt_line> {
            Window_Txt_line.create_txt(V.IsFr() ? "Maintenant equipez votre talent" : "Now equip your talent"),
            Window_Txt_line.create_video(Resources.Load<VideoClip>("video/tutoVideo_equipTalent")),
            Window_Txt_line.create_txt(V.IsFr() ? "Vous pouvez avoir plus de talent après avoir tuer un monstre, que la chance soit de votre côté" :  "You may have more talent after defeating monster, may the luck be in your side"),
        });
    }

    public override bool finished()
    {
        return Character.IsTalentEquiped(Talent_Gestion.talent.power);
    }
}

public class step_congrat : step
{
    public override void takeEffect()
    {
        base.takeEffect();

        restrictionManagement(true);

        /*
        void closing()
        {
            windowTxt.Close_Force(); windowTxt.event_Close += tuto.CloseButton;
            ClickAutorization.forbidden_Remove(Main_Object.Get(Main_Object.objects.button_skill));
            Panel_button.GetWindowOfLink(Panel_button.En_link.skill).w.event_Close -= closing;
        }

        windowTxt.event_Close -= tuto.CloseButton;

        Window windowSkill = Panel_button.GetWindowOfLink(Panel_button.En_link.skill).w;

        windowSkill.DontAllowClickOnCloseButton = false;

        windowSkill.event_Close += closing;

        windowTxt.ChangeTitle("Yay");

        windowTxt.CreateContent(new List<Window_Txt_line> {
            Window_Txt_line.create_txt(V.IsFr() ? "Vous êtes plus fort maintenant !" : "Yay you'r stronger now !"),
            Window_Txt_line.create_txt(V.IsFr() ? "Je vous reverrais plus tard" :  "I'll see you soon"),
            Window_Txt_line.create_button(V.IsFr() ? "A plus tard" : "Goodbye", () => {
                if(exitButton())
                    closing();
})
        });*/
    }

    public override bool finished()
    {
        return false;

        //return Panel_button.ExistWindowOfLink(Panel_button.En_link.equipment);
    }
}

public class step_equipment : step
{
    public override void takeEffect()
    {
        /*
        base.takeEffect();

        void closing()
        {
            windowTxt.event_Close -= closing;
            windowTxt.Close_Force();
        }

        restrictionManagement(true);

        windowTxt.Open();

        windowTxt.event_Close -= tuto.CloseButton;

        windowTxt.event_Close += closing;

        windowTxt.ChangeTitle(V.IsFr() ? "Equipement" : "Equipment");

        windowTxt.CreateContent(new List<Window_Txt_line> {
            Window_Txt_line.create_txt(V.IsFr() ? "Les equipements donne des statistiques ou peuvent être utilisé pendant les combats (armes et objets)" :  "Equipment give stats or can be use in combat (weapon and object)"),
            Window_Txt_line.create_video(Resources.Load<VideoClip>("video/tutoVideo_EquipEquipment")),
            Window_Txt_line.create_txt(V.IsFr() ? "Vous connaissez les bases maintenant, allez trouvez et tuez ce Vela !" : "You know the basic now, go find and kill that Vela !"),
            Window_Txt_line.create_button(V.IsFr() ? "En route" : "On my way", () => {
                if(exitButton()) {
                    closing();
                }
            })
        });
        */
    }

    public override bool finished()
    {
        return false;
        //return !Panel_button.ExistWindowOfLink(Panel_button.En_link.skill) || finishedBool;
    }
}

