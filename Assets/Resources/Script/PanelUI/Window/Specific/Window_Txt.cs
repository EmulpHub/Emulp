using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Window_Txt : Window
{
    public List<Window_Txt_line> content = new List<Window_Txt_line>();


    public static Window_Txt CreateFloatingWindow(string title, List<Window_Txt_line> ls)
    {
        GameObject g = Instantiate(Resources.Load<GameObject>("Prefab/Window/Window_txt/Window_Txt"));

        Window_Txt t = g.transform.GetChild(1).GetComponent<Window_Txt>();

        t.CreateContent(ls);

        t.ChangeTitle(title);

        return t;
    }

    public static Window_Txt CreateFloatingWindow_Empty()
    {
        return CreateFloatingWindow("", new List<Window_Txt_line>());
    }


    public override void Update()
    {
        base.Update();

        SetSize();
    }

    public Transform contentParent;

    public GameObject content_txt, content_img, content_button, content_video;

    public void ChangeTitle(string newTitle)
    {
        title.text = newTitle;
    }

    public void CreateContent(List<Window_Txt_line> ls)
    {
        Reset();

        content = ls;

        foreach (Window_Txt_line info in content)
        {
            Create(info);
        }

        Canvas.ForceUpdateCanvases();

        SetSize();
    }

    public void SetSize()
    {
        //+20 +40
        RectTransform r = contentParent.GetComponent<RectTransform>();

        SetSize(new Vector2(r.sizeDelta.x + 25, r.sizeDelta.y + 50));
    }

    public void Create(Window_Txt_line info)
    {
        if (info.t == Window_Txt_line.type.txt)
        {
            GameObject g = Instantiate(content_txt, contentParent);

            Text t = g.GetComponent<Text>();

            t.text = info.txt;
        }
        else if (info.t == Window_Txt_line.type.image)
        {
            GameObject g = Instantiate(content_img, contentParent);

            Image t = g.GetComponent<Image>();

            t.sprite = info.img;

            Image f = g.transform.GetChild(1).GetComponent<Image>();

            f.sprite = info.img;
        }
        else if (info.t == Window_Txt_line.type.button)
        {
            GameObject g = Instantiate(content_button, contentParent);

            Text t = g.transform.GetChild(0).GetComponent<Text>();

            t.text = info.txt;

            g.GetComponent<Button>().onClick.AddListener(() => { info.action(); });
        }
        else if (info.t == Window_Txt_line.type.video)
        {
            GameObject g = Instantiate(content_video, contentParent);

            g.GetComponent<VideoPlayer>().clip = info.vid;

            float ratioX = (float)info.vid.width / (float)info.vid.height;

            g.GetComponent<RectTransform>().sizeDelta = new Vector2(ratioX * 100, 100);
        }
    }

    public void Reset()
    {
        while (contentParent.transform.childCount > 0)
        {
            DestroyImmediate(contentParent.transform.GetChild(0).gameObject);
        }
    }
}

public class Window_Txt_line
{
    public enum type { txt, image, button, video }

    public Window_Txt_line(type t)
    {
        this.t = t;
    }

    public type t;

    //type txt
    public string txt;

    //type image
    public Sprite img;

    public static Window_Txt_line create_txt(string txt)
    {
        Window_Txt_line a = new Window_Txt_line(type.txt);

        a.txt = txt;
        a.t = type.txt;

        return a;
    }

    public static Window_Txt_line create_img(Sprite img)
    {
        Window_Txt_line a = new Window_Txt_line(type.image);

        a.img = img;

        return a;
    }

    public delegate void buttonAction();

    public buttonAction action;

    public static Window_Txt_line create_button(string txt, buttonAction ac)
    {
        Window_Txt_line a = new Window_Txt_line(type.button);

        a.txt = txt;

        a.action = ac;

        return a;
    }

    public VideoClip vid;

    public static Window_Txt_line create_video(VideoClip video)
    {
        Window_Txt_line a = new Window_Txt_line(type.video);

        a.vid = video;

        return a;
    }
}
