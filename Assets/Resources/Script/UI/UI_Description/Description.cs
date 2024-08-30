using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour
{
    [HideInInspector]
    public string baseTitle, baseDescription;

    public Text title, description;

    public Image box;

    private void Update()
    {
        Check_PositionAndScale();
    }

    public float bgTopPosition_WithTitle,bgTopPosition_WithoutTitle;

    public Image TitleLine;

    public void Check_PositionAndScale() {

        TitleLine.gameObject.SetActive(title.text.Length > 0);

        if (title.text.Length > 0)
            box.rectTransform.offsetMax = new Vector2(box.rectTransform.offsetMax.x, -bgTopPosition_WithTitle);
        else
            box.rectTransform.offsetMax = new Vector2(box.rectTransform.offsetMax.x, -bgTopPosition_WithoutTitle);
    }
}
