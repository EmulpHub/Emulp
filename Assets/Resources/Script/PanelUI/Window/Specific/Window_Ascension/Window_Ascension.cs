using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_Ascension : Window
{
    public EndOfRunInfo info;

    public bool noInfo { get => info is null; }

    public Text winTitle, notricePower;

    public Button continueButton;
    public Text continueButton_txt;

    [HideInInspector]
    public GameObject dontMoveGameobject;

    public NotriceGainShow gainShow;

    public GameObject ButtonClose;

    public void UpdateTextValue()
    {
        if (noInfo)
        {
            if (V.IsFr())
                winTitle.text = "Récompenses :";
            else
                winTitle.text = "Reward :";

            continueButton.onClick.RemoveAllListeners();

            continueButton.onClick.AddListener(Surrender);
            continueButton_txt.text = V.IsFr() ? "Abandonner" : "Surrender";
        }
        else
        {
            if (info.State == EndOfRunInfo.state.win)
            {
                winTitle.text = V.IsFr() ? "Vous avez gagné !" : "You win !";

                continueButton.onClick.RemoveAllListeners();

                continueButton.onClick.AddListener(Ascension.Win);
                continueButton_txt.text = "Ascend";
            }
            else
            {
                winTitle.text = V.IsFr() ? "Vous avez perdu" : "You loose";

                continueButton.onClick.RemoveAllListeners();

                continueButton.onClick.AddListener(Ascension.Loose);

                int total = NotriceGainShow.GetTotal(Nortice_GainSystem.gainedNortice);

                if (total == 0)
                    continueButton_txt.text = V.IsFr() ? "Menu des ascensions" : "Ascension menu";
                else if (total == 1)
                    continueButton_txt.text = V.IsFr() ? "Recevoir la Nortice" : "Get Nortice";
                else
                    continueButton_txt.text = V.IsFr() ? "Recevoir les Nortices" : "Get Nortice";
            }
        }

        gainShow.Init(Nortice_GainSystem.gainedNortice);

        ButtonClose.gameObject.SetActive(noInfo);

        notricePower.text = "" + Ascension.nortice;
    }

    public void Surrender()
    {
        if (windowConfirmation != null)
        {
            windowConfirmation.Open();

            return;
        }

        string desc = V.IsFr() ? "Voulez-vous abandonner ?" : "Do you want to surrender ?";

        windowConfirmation = Window_Confirmation.Launch(desc, Ascension.Loose);
    }

    [HideInInspector]
    public Window_Confirmation windowConfirmation;

    public void Start()
    {
        UpdateTextValue();
    }

    public override void Selectionnate()
    {
        base.Selectionnate();

        UpdateTextValue();
    }


    public override void Close(bool ignoreAutorization = false)
    {
        if (!noInfo)
            return;

        base.Close(ignoreAutorization);
    }

    public static void UpdateWindow()
    {
        (bool find, Window e) ascensionWindow = WindowInfo.Instance.GetWindow(WindowInfo.type.Ascension);

        if (ascensionWindow.find)
        {
            ((Window_Ascension)ascensionWindow.e).UpdateTextValue();
        }
    }

    public static Vector3 positon = new Vector3(-8.466f, -2.88f, 0);

    public static void AddNorticePowerDisplay(int amount)
    {
        if (Ascension.currentAscension == 0 && !(V.administrator && V.script_Scene_Main_Administrator.permaShowAscension))
            return;

        Main_UI.Display_movingText("+" + amount, V.Color.green, positon, 1.5f, 1, V.norticeIcon);
    }
}
