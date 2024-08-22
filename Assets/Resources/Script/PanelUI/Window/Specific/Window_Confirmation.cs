using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_Confirmation : Window
{
    public GameObject button_close;

    public Button button_yes, button_no;

    public Text description_tx;

    public override void Awake()
    {
        base.Awake();

        Open();

        ClickAutorization.Add(this.gameObject);

        type = WindowInfo.type.confirmation;

        button_close.gameObject.SetActive(false);
    }

    public override void Close(bool ignoreAutorization = false)
    {
        ClickAutorization.Remove(this.gameObject);

        base.Close(ignoreAutorization);
    }

    public delegate void YesVoid();
    public delegate void NoVoid();


    public static Window_Confirmation Launch(string description, YesVoid yesVoid, NoVoid NoVoid = null)
    {
        GameObject g = Instantiate(Resources.Load<GameObject>("Prefab/Window/Window_Confirmation"));

        Window_Confirmation wc = g.transform.GetChild(1).GetComponent<Window_Confirmation>();

        wc.description_tx.text = description;

        wc.button_yes.onClick.AddListener(delegate { yesVoid(); });
        if (NoVoid != null)
        {
            wc.button_no.onClick.AddListener(delegate
            {
                NoVoid();
            });
        }

        wc.buttonY = wc.button_yes.GetComponent<RectTransform>();
        wc.buttonN = wc.button_no.GetComponent<RectTransform>();

        wc.window = wc.GetComponent<RectTransform>();

        wc.UpdateUI();

        return wc;
    }

    [HideInInspector]
    public RectTransform buttonY, buttonN, window;
    public Text button_yes_txt, button_no_txt, description;

    public override void Update_Ui()
    {
        base.Update_Ui();

        UpdateUI();
    }

    public void UpdateUI()
    {
        if (V.IsFr())
        {
            button_yes_txt.text = "Oui";
            button_no_txt.text = "Non";
        }
        else if (V.IsUk())
        {
            button_yes_txt.text = "Yes";
            button_no_txt.text = "No";
        }

        buttonY.anchoredPosition = new Vector2(buttonY.anchoredPosition.x,
            description.rectTransform.anchoredPosition.y - description.rectTransform.sizeDelta.y - 10);

        buttonN.anchoredPosition = new Vector2(buttonN.anchoredPosition.x,
    description.rectTransform.anchoredPosition.y - description.rectTransform.sizeDelta.y - 10);

        window.sizeDelta = new Vector2(window.sizeDelta.x, Mathf.Abs(buttonY.anchoredPosition.y) + buttonY.sizeDelta.y + 50);
    }
}
