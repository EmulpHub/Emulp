using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animation_Endless_PixTxt : AnimEndless
{
    public static Animation_Endless_PixTxt Create(Entity target, Sprite render, txtGeneration txtGen, WhenToRemove toRemove = null, float size = 1, float speed = 1)
    {
        GameObject g = Instantiate(AnimEndlessStatic.pixTxt);

        Animation_Endless_PixTxt script = g.GetComponent<Animation_Endless_PixTxt>();

        script.target = target;
        script.Render.sprite = render;
        script.Size = size;
        script.Speed = speed;
        script.CurrentId = AnimEndlessStatic.ID.Pix;
        script.txtGen = txtGen;
        script.offset = new Vector3(0, -0.5f, 0);
        script.toRemove = toRemove;

        script.UpdateTxt();
        script.GoToTarget();
        script.UpdateScale();
        script.AddToStaticList();

        return script;
    }

    public Image Render;

    public delegate string txtGeneration();
    public txtGeneration txtGen;

    public delegate bool WhenToRemove();
    public WhenToRemove toRemove;

    public Text txt;

    public new void Update()
    {
        base.Update();

        UpdateTxt();

        if (toRemove != null && toRemove())
        {
            Erase();
        }
    }

    void UpdateTxt()
    {
        txt.text = "" + txtGen();
    }
}
