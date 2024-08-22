using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Scene_NorticePower : MonoBehaviour
{
    public Text title, effect;

    public void Update_UI()
    {
        if (Ascension.currentAscension == 0)
        {
            title.text = "Normal";
            effect.text = V.IsFr() ? "- Pas de modification -" : "- No modifier - ";
        }
        else
        {

            title.text = "Ascension " + Ascension.currentAscension;
            effect.text = Ascension.ConvertAscensionModifierIntoString(Ascension.currentAscensionModifier);
        }
    }
}
