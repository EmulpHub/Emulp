using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// The script attached to the window when the player gain a level
/// </summary>
public class UI_LevelUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    /// <summary>
    /// The number of level the player gain
    /// </summary>
    public int levelGain;

    public static GameObject CurrentLevelUp;

    public RectTransform detectionRect;

    public float height, height_newSpell;

    /// <summary>
    /// Display a level Up screen
    /// </summary>
    public static void ShowLevelUp(int levelGain)
    {
        SoundManager.PlaySound(SoundManager.list.player_levelUp_2);

        GameObject LevelUpScreen = Resources.Load<GameObject>("Prefab/UI_LevelUp");

        LevelUpScreen.GetComponent<UI_LevelUp>().levelGain = levelGain;

        if (CurrentLevelUp == null)
        {
            CurrentLevelUp = Instantiate(LevelUpScreen);

            PlayerMoveAutorization.Instance.Add(CurrentLevelUp);
        }
        else
        {
            CurrentLevelUp.GetComponent<UI_LevelUp>().levelGain++;
            CurrentLevelUp.GetComponent<UI_LevelUp>().SetText();
        }
    }

    public Vector3 baseScale;


    public bool haveNewSpell;

    private void Start()
    {
        SetText();
    }

    public float speedApparition;

    public GameObject BigCircleParent;

    public float BigCircle_FinalSize, BigCircle_Speed;

    /// <summary>
    /// Set the title sprite, and make a little animation
    /// </summary>
    private void SetText()
    {
        bool newSpell = ManageSpellToAdd(V.player_info.level);

        WindowInfo.Instance.DeselectionnateAllWindow();

        delayBeforeSkipping = delayBeforeSkippingMax;

        transform.position = new Vector3(0, 2, 0);

        transform.DOScale(0.01f, 0);
        transform.DOScale(0.03f, speedApparition);

        BigCircleParent.transform.DOScale(BigCircle_FinalSize, BigCircle_Speed);

        if (!newSpell)
            point_skill.text = "+" + levelGain + (V.IsFr() ? " Compétence" : " Skill");
        else
            point_skill.text = V.IsFr() ? "Nouveau sort" : "New spell";

        if (V.IsFr())
        {
            level.text = "Niveau " + V.player_info.level + " !";
        }
        else if (V.IsUk())
        {
            level.text = "Level " + V.player_info.level + " !";
        }

        Clicked = false;

        SpellToAddG.gameObject.SetActive(SpellToAdd != SpellGestion.List.none);

        if (SpellToAdd != SpellGestion.List.none)
        {
            SpellToAdd_Render.sprite = SpellGestion.Get_sprite(SpellToAdd);

            Main_UI.Toolbar_AddSpell(SpellToAdd);
        }
    }

    public GameObject SpellToAddG;

    public Image SpellToAdd_Render;

    public float speedBetweenSkill;

    public Vector3 anim_startPos, anim_endPos;

    public Sprite anim_icon;

    public void Skill_animation()
    {
        int i = 0;
        while (i < levelGain)
        {
            Animation_AcquiredStuff.animation_instantiation(
                i * speedBetweenSkill,
                anim_startPos, anim_endPos, anim_icon);

            card_primaryStats.CreateChoiceCardRandom();

            i++;

        }
    }

    [HideInInspector]
    public float delayBeforeSkipping;

    public float delayBeforeSkippingMax;

    public Text level;

    public Text point_skill;

    public float randomnessJumpPower;

    public bool Clicked = false;

    private void Update()
    {
        delayBeforeSkipping -= 1 * Time.deltaTime;

        //If we click and the delay passe then remove the endscreen
        if (Input.GetMouseButtonUp(0) && delayBeforeSkipping < 0 && !Clicked)
        {
            V.player_info.point_skill += levelGain;


            Clicked = true;
            PlayerMoveAutorization.Instance.Remove(this.gameObject);

            Skill_animation();

            transform.position = new Vector3(100, 100, 0);

            CurrentLevelUp = null;

            V.player_info.CalculateValue();

            Main_Object.Enable(Main_Object.objects.button_skill);

            Destroy(this.gameObject, 20);
        }
    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Animation_Up();
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Animation_Down();
    }

    public float Up_str, Up_spd, down_spd;

    public void Animation_Up()
    {
        return;
        transform.DOKill();
        transform.DOScale(baseScale + new Vector3(Up_str, Up_str, 0), Up_spd);
    }

    public void Animation_Down()
    {
        return;
        transform.DOKill();
        transform.DOScale(baseScale, down_spd);
    }

    #region NewSpell

    public SpellGestion.List SpellToAdd;

    public CustomDescription desc;

    public bool ManageSpellToAdd(int lvl)
    {
        SpellToAdd = Character.getBaseSpellFromLvl(lvl);

        SpellGestion.List second = Character.TakeAllPreviousSpell(lvl);

        if (SpellToAdd == SpellGestion.List.none)
        {
            SpellToAdd = second;
        }

        if (Character.baseSpellDic_Locked.Contains(lvl))
        {
            Character.baseSpellDic_Locked.Remove(lvl);
        }

        detectionRect.sizeDelta = new Vector2(detectionRect.sizeDelta.x, SpellToAdd != SpellGestion.List.none ? height_newSpell : height);

        if (SpellToAdd != SpellGestion.List.none)
        {
            if (V.IsFr())
            {
                desc.titre_FR = SpellGestion.Get_Title(SpellToAdd);
                desc.description_FR = SpellGestion.Get_Description(SpellToAdd);
            }
            else
            {
                desc.titre_UK = SpellGestion.Get_Title(SpellToAdd);
                desc.description_UK = SpellGestion.Get_Description(SpellToAdd);
            }
        }

        return SpellToAdd != SpellGestion.List.none;
    }

    #endregion
}