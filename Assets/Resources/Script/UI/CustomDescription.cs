using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomDescription : MonoBehaviour,
 IPointerExitHandler, IPointerEnterHandler//, IPointerDownHandler
{
    public enum SpecialText
    {
        custom, life, pa, pm, endOfTurn, Spell,
        character_mastery, character_damage, character_po, character_tackle, character_leak
    }

    public SpecialText typeOfText;

    public SpellGestion.List sp;

    public string titre_FR, description_FR, titre_UK, description_UK;
    public float distanceY, distanceX;

    public float delay;

    public bool dontShowWithWindow;

    public Vector2 customPos;

    [HideInInspector]
    public float delay_current;

    [HideInInspector]
    public bool mouseOver;

    public GameObject AutorizationReference;

    public void Set(string title_fr, string desc_fr, string title_uk, string desc_uk)
    {
        titre_FR = title_fr;
        description_FR = desc_fr;
        titre_UK = title_uk;
        description_UK = desc_uk;
    }

    public void Set(string title, string desc)
    {
        titre_FR = title;
        description_FR = desc;
        titre_UK = title;
        description_UK = desc;
    }

    void Start()
    {
        delay_current = delay;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Enter();
    }

    public void Update()
    {
        if (mouseOver)
        {
            GameObject reference = this.gameObject;
            if (AutorizationReference != null)
                reference = AutorizationReference;

            if (!ClickAutorization.Autorized(reference))
            {
                Exit();
                return;
            }

            delay_current -= 1 * Time.deltaTime;
            UpdateText();
        }
    }

    public void UpdateText()
    {
        if (delay_current <= 0)
        {
            string titre = "", description = "";

            if (V.IsFr())
            {
                if (typeOfText == SpecialText.custom)
                {
                    titre = titre_FR;
                    description = description_FR;
                }
                else if (typeOfText == SpecialText.life)
                {
                    titre = "Vie";
                    description = "*bon" + V.player_info.Life + " / " + V.player_info.Life_max + " Vie*end restant";
                }
                else if (typeOfText == SpecialText.pa)
                {
                    titre = "Pa";
                    description = "*bon" + V.player_info.PA + " / " + V.player_info.PA_max + " pa*end restant";
                }
                else if (typeOfText == SpecialText.pm)
                {
                    titre = "Pm";
                    description = "*bon" + V.player_info.PM + " / " + V.player_info.PM_max + " pm*end restant";
                }
                else if (typeOfText == SpecialText.endOfTurn)
                {
                    if (V.game_state == V.State.passive)
                    {
                        titre = "";
                        description = "";
                    }
                    else if (V.game_state == V.State.fight)
                    {
                        if (EntityOrder.IsTurnOf_Player())
                        {
                            titre = "FINIR SON TOUR";
                            description = descColor.convert("Cliquez ou appuyez sur *bonEspace*end pour finir votre tour");
                        }
                        else
                        {
                            titre = "";
                            description = "Tour ennemie";
                        }
                    }
                    else if (V.game_state == V.State.positionning)
                    {
                        titre = "";
                        description = "Debut du combat";
                    }
                }
                else if (typeOfText == SpecialText.character_mastery)
                {
                    titre = "Maitrise";
                    description = "Vos dégats sont augmentés de *bon" + V.player_info.str + "%*end";
                }
                else if (typeOfText == SpecialText.character_damage)
                {
                    titre = "Dommage";
                    description = "Vos dégats sont augmentés de *bon" + V.player_info.damage + "*end";
                }
                else if (typeOfText == SpecialText.character_po)
                {
                    titre = "Portée";
                    description = "Vos sorts ont *bon" + V.player_info.po + "%*end portée supplementaire";
                }
            }
            else
            {
                if (typeOfText == SpecialText.custom)
                {
                    titre = titre_UK;
                    description = description_UK;
                }
                else if (typeOfText == SpecialText.life)
                {
                    titre = "Life";
                    description = "*pdv" + V.player_info.Life + " / " + V.player_info.Life_max + " life*end left";
                }
                else if (typeOfText == SpecialText.pa)
                {
                    titre = "Ap";
                    description = "*bon" + V.player_info.PA + " / " + V.player_info.PA_max + " ap*end left";
                }
                else if (typeOfText == SpecialText.pm)
                {
                    titre = "Mp";
                    description = "*bon" + V.player_info.PM + " / " + V.player_info.PM_max + " mp*end left";
                }
                else if (typeOfText == SpecialText.endOfTurn)
                {
                    if (V.game_state == V.State.passive)
                    {
                        titre = "";
                        description = "";
                    }
                    else if (V.game_state == V.State.fight)
                    {
                        if (EntityOrder.IsTurnOf_Player())
                        {
                            titre = "END TURN";
                            description = descColor.convert("Click or press *bonSpace*end to end your turn");
                        }
                        else
                        {
                            titre = "";
                            description = "Ennemy's turn";
                        }
                    }
                    else if (V.game_state == V.State.positionning)
                    {
                        titre = "";
                        description = "Start combat";
                    }
                }
                else if (typeOfText == SpecialText.character_mastery)
                {
                    titre = "Mastery";
                    description = "Your damage is increased by *bon" + V.player_info.str + "%*end";
                }
                else if (typeOfText == SpecialText.character_damage)
                {
                    titre = "Damage";
                    description = "Your damage is inceased by *bon" + V.player_info.damage + "*end";
                }
                else if (typeOfText == SpecialText.character_po)
                {
                    titre = "Range";
                    description = "Your spells have *bon" + V.player_info.po + "%*end more range";
                }
            }

            if (description != "" || typeOfText == SpecialText.Spell)
            {
                Vector3 pos = this.transform.position + new Vector3(distanceX, 0, 0);

                if (customPos != Vector2.zero)
                {
                    distanceY = 0;
                    pos = customPos;
                }

                if (typeOfText == SpecialText.Spell)
                    Description_text.Display(sp, pos, distanceY);
                else
                    Description_text.Display(titre,description,pos,distanceY);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Exit();
    }

    public void Enter()
    {
        if (dontShowWithWindow && Scene_Main.isMouseOverAWindow)
        {
            return;
        }

        if (!ClickAutorization.Autorized(this.gameObject))
            return;

        mouseOver = true;
    }

    public void Exit()
    {
        delay_current = delay;
        mouseOver = false;

        Description_text.EraseDispay();
    }
}
