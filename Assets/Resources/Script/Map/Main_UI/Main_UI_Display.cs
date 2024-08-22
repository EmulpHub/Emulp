using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public partial class Main_UI : MonoBehaviour
{
    /// <summary>
    /// The display description, a box of dialogue to tell for exemple info for a spell
    /// </summary>
    public static GameObject ui_display_descriptionG, ui_display_titleG, ui_display_monsterListG, ui_display_description_equipmentG;

    public static void InitUIDisplay()
    {
        ui_display_descriptionG = Resources.Load<GameObject>("Prefab/UI_Description/UI_Description");
        ui_display_titleG = Resources.Load<GameObject>("Prefab/UI_DisplayTitle");

        ui_display_monsterListG = Resources.Load<GameObject>("Prefab/UI_DisplayMonsterList");

        ui_display_description_equipmentG = Resources.Load<GameObject>("Prefab/UI_Description/UI_Description_Equipment");

    }

    /// <summary>
    /// The display that show damage for exemple "-5" or end of turn "End of turn"
    /// </summary>
    public GameObject movingTextG;

    /// <summary>
    /// The current title used
    /// </summary>
    public static GameObject ui_displayTitle;

    /// <summary>
    /// The minimum box_xScale allowed
    /// </summary>
    public int boxTitle_MinXSize;

    public int boxTitle_AdditionalY;

    /// <summary>
    /// The additional x value will add to the box x scale 
    /// </summary>
    public int boxTitle_AdditionalX;

    /// <summary>
    /// The border sprite that can have a display_Description
    /// </summary>
    public Sprite box_normal, box_informative, contour_normal, contour_informative;

    public GameObject StartOfTurn;
    public Sprite StartOfTurn_Fr, StartOfTurn_Uk;

    public float StartOfTurn_Speed = 0.5f;

    public static GameObject CurrentStartOfTurn;

    /// <summary>
    /// Display the it's your turn at the start of your turn
    /// </summary>
    public void Display_startOfTurn()
    {
        if (CurrentStartOfTurn != null)
        {
            Destroy(CurrentStartOfTurn);
        }

        float baseScale = StartOfTurn.transform.localScale.x;

        GameObject g = Instantiate(StartOfTurn);

        CurrentStartOfTurn = g;

        SpriteRenderer spriteRenderer = g.GetComponent<SpriteRenderer>();

        g.transform.localScale = Vector2.zero;
        g.transform.DOScale(baseScale, StartOfTurn_Speed);

        spriteRenderer.DOFade(0, 0);
        spriteRenderer.DOFade(0.6f, StartOfTurn_Speed / 2);

        if (V.IsFr())
        {
            spriteRenderer.sprite = StartOfTurn_Fr;
        }
        else
        {
            spriteRenderer.sprite = StartOfTurn_Uk;
        }

        StartCoroutine(Display_fade(spriteRenderer, StartOfTurn_Speed * 2, StartOfTurn_Speed / 2));
    }

    /// <summary>
    /// Fade a given sprite to 0 renderer in TimeInSec_DoFade after a TimeInSec_Wait delay 
    /// </summary>
    /// <param name="sp">The sprite renderer we want to fade</param>
    /// <param name="timeInSec_Wait">The delay before fading sp</param>
    /// <param name="timeInSec_DoFade">The time it fade to 0</param>
    /// <returns></returns>
    public IEnumerator Display_fade(SpriteRenderer sp, float timeInSec_Wait, float timeInSec_DoFade)
    {
        yield return new WaitForSeconds(timeInSec_Wait);
        if (sp != null)
        {
            sp.DOFade(0, timeInSec_DoFade);
            Destroy(sp.gameObject, timeInSec_DoFade + 1);
        }
    }

    public void TryStopCoroutine(Coroutine co)
    {
        if (co != null)
        {
            StopCoroutine(co);
        }
    }

    public GameObject startOfTurn_arrow;

    public float startOfTurn_arrow_speed, startOfTurn_arrow_distance;

    /// <summary>
    /// Display the little arrow for start of turn 
    /// </summary>
    /// <param name="entity">The entity that start his turn</param>
    /// <param name="distance">The distance above the entity</param>
    public void Display_StartOfTurnArrow(Entity entity, float distance)
    {
        GameObject g = Instantiate(startOfTurn_arrow);

        SpriteRenderer spriteRenderer = g.GetComponent<SpriteRenderer>();

        g.transform.position = entity.transform.position + new Vector3(0, distance + startOfTurn_arrow_distance, 0);
        g.transform.DOMoveY(entity.transform.position.y + distance, startOfTurn_arrow_speed);

        StartCoroutine(Display_fade(spriteRenderer, startOfTurn_arrow_speed + 0.2f, startOfTurn_arrow_speed / 2));
    }
}
