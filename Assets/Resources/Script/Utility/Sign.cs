using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public List<Text> txt = new List<Text>();

    public List<string> txt_content_fr = new List<string>();
    public List<string> txt_content_uk = new List<string>();

    void Start()
    {
        for (int i = 0; i < txt.Count; i++)
        {
            if (V.IsFr())
            {
                txt[i].text = txt_content_fr[i];
            }
            else
            {
                txt[i].text = txt_content_uk[i];
            }
        }
    }
}
