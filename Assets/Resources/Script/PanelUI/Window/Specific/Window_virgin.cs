using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_virgin : Window
{
    public override void Awake()
    {
        base.Awake();

        base.type = WindowInfo.type.virgin;
    }

    public Text body_text;

    /// <summary>
    /// Update ui (specific to virgin)
    /// </summary>
    public override void Update_Ui()
    {
        base.Update_Ui();

        if (V.IsFr())
        {
            title.text = "Vierge";
            body_text.text = "Ceci est une page vierge";
        }
        else
        {
            title.text = "Virgin";
            body_text.text = "This is a virgin window";
        }
    }
}
