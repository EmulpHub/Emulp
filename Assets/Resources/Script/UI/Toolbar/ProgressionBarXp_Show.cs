using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProgressionBarXp_Show : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ProgressionBarXp progressionBarScript;

    public Text XpShow;

    [HideInInspector]
    bool ShowText;

    public void Update()
    {
        XpShow.gameObject.SetActive(ShowText);

        if (ShowText)
        {
            XpShow.text = (V.IsFr() ? "Niv. " : "Lv. ") + V.player_info.level + " " + (Mathf.RoundToInt(progressionBarScript.Progression_Fill.fillAmount * progressionBarScript.Xp_max)) + "/" + progressionBarScript.Xp_max + "";

            XpShow.transform.position = new Vector3(CursorInfo.Instance.positionInWorld.x, XpShow.transform.position.y);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowText = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ShowText = false;
    }
}