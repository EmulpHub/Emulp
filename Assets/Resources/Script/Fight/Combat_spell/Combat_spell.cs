using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public partial class Spell : MonoBehaviour
{
    public static Spell Reference;

    public static Spell Weapon;

    public static Spell Object, Object_2;

    /// <summary>
    /// The range, the pa and cd of the spell
    /// </summary>
    [HideInInspector]
    public int range_max = 3, range_min = 1, cd;

    [HideInInspector]
    private int pa_cost = 3;

    /// <summary>
    /// The title and description
    /// </summary>
    [HideInInspector]
    public string title, description;

    /// <summary>
    /// The spell use it to get info with V
    /// </summary>
    public SpellGestion.List spell;

    public enum Range_type { normal, line, noNeedOfDistance, noNeedOfLineOfView }

    [HideInInspector]
    public Range_type range_Type;

    public SpellGestion.range_effect_size range_Effect;

    /// <summary>
    /// Cd
    /// </summary>
    [HideInInspector]
    public int id_nextPossibleReUse = -1, id_nextPossibleReUse_max;

    /// <summary>
    /// Id of this spell
    /// </summary>
    public int id;

    public string pos;

    /// <summary>
    /// The graphique
    /// </summary>
    public Image graphique;

    /// <summary>
    /// The cooldown slice, fillamount > 0 = can't use the spell
    /// </summary>
    public Image cooldown_slice;

    /// <summary>
    /// THe text of cooldown and pa
    /// </summary>
    public Text cooldown_text, pa_txt;

    /// <summary>
    /// The gameobject that store the pa with a yellow circle
    /// </summary>
    public GameObject pa_group;

    /// <summary>
    /// THe primary and secondary key
    /// </summary>
    KeyCode primary = KeyCode.None, secondary = KeyCode.None;

    public GameObject bg_object;

    public static void ResetAllSpellInfo()
    {
        foreach (Spell a in SpellInToolbar.activeSpell_script)
        {
            a.SetSpellInfo();
        }
    }


    public enum typeOfSpell { spell, weapon, objectEquipment }

    public typeOfSpell typeSpell;

    public SpellGestion.List lastApplySpellInfo;

    public void ChangeSpell_Apply(SpellGestion.List l)
    {
        if (l == SpellGestion.List.empty || l == SpellGestion.List.object_empty)
            return;

        Combat_spell_application.Get(l).actionToolbar_add(id);
    }

    public void ChangeSpell_Remove(SpellGestion.List l)
    {

        if (l == SpellGestion.List.empty || l == SpellGestion.List.object_empty)
            return;

        Combat_spell_application.Get(l).actionToolbar_remove(id);
    }

    public Image interior;

    /// <summary>
    /// Change the spell by changing title, desc range etc...
    /// </summary>
    public void SetSpellInfo()
    {
        //Get the info of that spell
        title = SpellGestion.Get_Title(spell);
        description = SpellGestion.Get_Description(spell);
        range_max = SpellGestion.Get_RangeMax(spell);
        range_min = SpellGestion.Get_RangeMin(spell);
        pa_cost = SpellGestion.Get_paCost(spell);
        cd = SpellGestion.Get_Cd(spell);
        Sprite sp = SpellGestion.Get_sprite(spell);
        graphique.sprite = sp;
        range_Type = SpellGestion.Get_rangeType(spell);
        range_Effect = SpellGestion.Get_RangeEffect(spell);

        interior.color = SpellGestion.Get_col(spell);

        graphique.gameObject.SetActive(sp != null);

        if (spell == SpellGestion.List.base_fist ||
            (Equipment_Management.Equiped.ContainsKey(SingleEquipment.type.weapon) && spell == Equipment_Management.Equiped[SingleEquipment.type.weapon].Spell))
            Weapon = this;


        if (this == Weapon)
            typeSpell = typeOfSpell.weapon;
        else if (this == Object || this == Object_2)
        {
            typeSpell = typeOfSpell.objectEquipment;
        }
        else
        {
            typeSpell = typeOfSpell.spell;
        }

        if (lastApplySpellInfo != spell)
        {
            lastApplySpellInfo = spell;

            ChangeSpell_Apply(spell);
        }
    }

    public void Start()
    {
        SetSpellInfo();

        startScale = transform.localScale.x;

        SetShortcut();
    }

    public float speedCooldownSlice;

    [HideInInspector]
    public float rememberFillAmount;

    public float Display_YPos = -1.7f, Display_YPos_WithEffect;

    public bool IsEmpty()
    {
        return spell == SpellGestion.List.empty || spell == SpellGestion.List.object_empty;
    }

    /// <summary>
    /// Display the info of this spell
    /// </summary>
    public void DisplayInfo()
    {
        //bool WithTab = IsEmpty();

        float YPos = V.player_entity.GetListShowedEffect().Count == 0 ? Display_YPos : Display_YPos_WithEffect;

        Description_text.Display(spell, this.transform.position, -transform.position.y + YPos, this);
    }


    [HideInInspector]
    public bool cdBecauseLackOfPa;

    public void Update()
    {
        ContourManagement();

        bg_object.gameObject.SetActive((this == Object || this == Object_2) && spell == SpellGestion.List.object_empty);

        delayBeforeClickAgain -= 1 * Time.deltaTime;
        waitBeforeReSelectioning -= 1 * Time.deltaTime;

        UpdateFreePaUse();

        CheckForMouseOver();

        if (id < SpellInToolbar.activeSpell.Count && id >= 0 && SpellInToolbar.activeSpell[id] != spell)
            SpellInToolbar.activeSpell[id] = spell;


        //If one of the key are pressed
        if (Input.GetKeyDown(primary) || Input.GetKeyDown(secondary))
        {
            UseSpell();
        }

        //If the mouse is over this spell show the text
        if (DescriptionStatic.CurrentDescription == null && mouseIsOver)
        {
            DisplayInfo();

            Canvas.ForceUpdateCanvases();
        }

        //Activate/Deactivate gameobject 
        graphique.gameObject.SetActive(!IsEmpty());
        pa_group.gameObject.SetActive(!IsEmpty());

        //Assignate the text and the cooldown fillamount
        pa_txt.text = "" + Get_pa_cost();
        if (IsAbleToLaunch_Pa(V.player_info.PA))
        {
            pa_txt.color = new Color32(0, 0, 0, 255);
        }
        else
        {

            pa_txt.color = new Color32(224, 58, 62, 255);
        }

        float Value = (float)(id_nextPossibleReUse - EntityOrder.id_turn) / (float)id_nextPossibleReUse_max; ;

        if (id_nextPossibleReUse > EntityOrder.id_turn && !IsEmpty())
        {
            cooldown_text.text = "" + (id_nextPossibleReUse - EntityOrder.id_turn);
        }
        else if (!IsEnoughRessourceForLaunch() && !IsEmpty())
        {
            if (cooldown_slice.fillAmount < 1)
            {
                cooldown_slice.fillAmount = 1;
            }
            cdBecauseLackOfPa = true;
            Value = 1;
            cooldown_text.text = "";
        }

        else
        {
            Value = 0;
            cooldown_text.text = "";
        }

        if (Value != cooldown_slice.fillAmount && rememberFillAmount != Value)
        {
            rememberFillAmount = Value;
            if (cdBecauseLackOfPa)
            {
                cooldown_slice.fillAmount = 0;
                cdBecauseLackOfPa = false;
            }
            else
            {
                cooldown_slice.fillAmount = Value;
            }
        }
        else if (Value == cooldown_slice.fillAmount)
        {
            rememberFillAmount = -1;
        }

        //Clamp scale
        //transform.localScale = new Vector3(StartScale + Animation_Strengh_Over, StartScale + Animation_Strengh_Over);

        float min = 0.9f;
        float max = startScale + animation_Strengh_Over;

        transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x, min, max), Mathf.Clamp(transform.localScale.y, min, max), 1);

        SetGlowEffect();

        SetMultiplicator();
    }

    /// <summary>
    /// Modify the spell of this toolbar_spell
    /// </summary>
    /// <param name="NewSpell"></param>
    public void ChangeSpell(SpellGestion.List NewSpell)
    {
        ChangeSpell_Remove(spell);

        spell = NewSpell;

        SoundManager.PlaySound(SoundManager.list.ui_selectionSpell);

        SetSpellInfo();
    }

    /// <summary>
    /// The animation when the mouse pass over
    /// </summary>
    public float animation_Strengh, animation_Strengh_Over, animation_Time;

    public static bool IsGameActionIsSpell()
    {
        return V.game_state_action == V.State_action.spell;
    }

    /// <summary>
    /// Change from movement state to spell state
    /// </summary>
    public void SetGameAction_spell()
    {
        Scene_Main.ChangingOfView(Scene_Main.ChangingOfViewMode.NoEntityShowMovement);

        V.game_state_action = V.State_action.spell;

        //Set attribute
        SpellGestion.SetSelectionnedSpell(spell, this);

        EndOfCast = false;

        V.script_Scene_Main.Set_PossibleSpellTile(range_Type);
    }

    #region Animation

    /// <summary>
    /// The type of animation this spell can make (mouseover)
    /// </summary>
    public enum TypeOfAnimation { doPunch, doScale, reset, set, click }

    /// <summary>
    /// The startScale of this spell
    /// </summary>
    [HideInInspector]
    public float startScale;

    /// <summary>
    /// Make an animation when the mouse pass over for exemple
    /// </summary>
    /// <param name="Type">The type of animation</param>
    public void MakeAnimation(TypeOfAnimation Type)
    {

        return;
        if (Type == TypeOfAnimation.doPunch)
        {
            transform.DOKill();
            transform.localScale = new Vector3(startScale, startScale);
            transform.DOPunchScale(new Vector3(animation_Strengh, animation_Strengh, 0), animation_Time, 1);
        }
        if (Type == TypeOfAnimation.click)
        {
            transform.DOKill();
            if (mouseIsOver)
            {
                transform.localScale = new Vector3(animation_Strengh_Over + startScale, animation_Strengh_Over + startScale, 1);
            }
            else
            {
                transform.localScale = new Vector3(startScale, startScale, 1);
            }

            transform.DOPunchScale(new Vector3(-0.1f, -0.1f, 0), animation_Time, 1);
        }
        else if (Type == TypeOfAnimation.doScale)
        {
            transform.DOKill();
            transform.localScale = new Vector3(startScale, startScale);
            transform.DOScale(animation_Strengh_Over + startScale, animation_Time);
        }
        else if (Type == TypeOfAnimation.set)
        {
            transform.DOKill();
            transform.localScale = new Vector3(animation_Strengh_Over + startScale, animation_Strengh_Over + startScale);
        }
        else if (Type == TypeOfAnimation.reset)
        {
            transform.DOKill();
            transform.DOScale(startScale, animation_Time / 2);
        }
    }

    #endregion

    /// <summary>
    /// Create a new spell (for monster)
    /// </summary>
    /// <param name="wantedSpell">The wanted spell</param>
    /// <returns></returns>
    public static Spell Create(SpellGestion.List wantedSpell) //For monster use only
    {
        GameObject Combat_spellG = Instantiate(V.combat_spell_GameObject);

        Spell combat_Spell = Combat_spellG.GetComponent<Spell>();

        combat_Spell.id = 100;
        combat_Spell.pos = "99_99";

        combat_Spell.ignoreCD = true;

        combat_Spell.spell = wantedSpell;

        combat_Spell.range_min = SpellGestion.Get_RangeMin(wantedSpell);
        combat_Spell.range_max = SpellGestion.Get_RangeMax(wantedSpell);
        combat_Spell.pa_cost = SpellGestion.Get_paCost(wantedSpell);

        Combat_spellG.transform.position = new Vector3(100, 100, 0);

        return combat_Spell;
    }

    public void SetShortcut()
    {
        primary = KeyCode.None;
        secondary = KeyCode.None;

        //Get the shortcut to use this spell
        switch (id)
        {

            case 0:
                primary = KeyCode.Alpha1;
                break;

            case 1:
                primary = KeyCode.Alpha2;

                break;
            case 2:
                primary = KeyCode.Alpha3;
                break;
            case 3:
                primary = KeyCode.Alpha4;
                break;
            case 4:
                primary = KeyCode.Alpha5;
                break;
            case 5:
                primary = KeyCode.Alpha6;

                break;
            case 6:
                primary = KeyCode.Alpha7;

                break;
            case 7:
                primary = KeyCode.Alpha8;
                break;
            case 8:
                primary = KeyCode.Alpha9;
                break;
            case 9:
                primary = KeyCode.Q;

                break;
            case 10:
                primary = KeyCode.W;
                break;
            case 11:

                primary = KeyCode.E;
                break;
            case 30: //object
                primary = KeyCode.O;

                break;
            case 31: //object
                primary = KeyCode.L;

                break;
            default:
                break;
        }
    }

    public float Multiplicator = 1;

    public float AdditionalMultiplicator = 0;

    public virtual void SetMultiplicator()
    {
        float nb = 1;

        nb += CombatSpell_VisualEffect.GetVisualEffect(pos);

        nb += (float)V.player_entity.CollectStr(Effect.effectType.effectPercentage) / 100;

        nb += (float)V.player_entity.CollectStr(Effect.effectType.effectPercentage_OneUse) / 100;

        Multiplicator = nb;
    }
}