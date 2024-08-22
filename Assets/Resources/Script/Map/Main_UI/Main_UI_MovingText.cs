using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Text.RegularExpressions;

public partial class Main_UI : MonoBehaviour
{
    /// <summary>
    /// The order in wich movingText should appear (not instantly in the pos)
    /// </summary>
    public static Dictionary<Vector3, List<MovingStruct>> MovingText_Order = new Dictionary<Vector3, List<MovingStruct>>();

    /// <summary>
    /// Manage when little text should appear in the scene
    /// </summary>
    public void ManageMovingText()
    {
        List<Vector3> MovingText_Order_Keys = new List<Vector3>(MovingText_Order.Keys);

        foreach (Vector3 pos in MovingText_Order_Keys)
        {
            if (MovingText_Order[pos].Count == 0)
            {
                MovingText_Order.Remove(pos);
                continue;
            }

            MovingStruct theStruct = MovingText_Order[pos][0];

            if (!theStruct.Launched)
            {
                theStruct.Launched = true;

                Display_movingText_Instantiate(theStruct);
            }
            else if (theStruct.Cd <= 0)
            {
                movingStrucDic_remove(theStruct, false);

                MovingText_Order[pos].Remove(theStruct);
            }
            else
            {
                theStruct.Cd -= Time.deltaTime;
            }
        }
    }

    public static void AddMovingTextInLs(MovingStruct s)
    {
        if (!MovingText_Order.ContainsKey(s.Position))
        {
            MovingText_Order.Add(s.Position, new List<MovingStruct> { s });
            return;
        }

        List<MovingStruct> MSList = MovingText_Order[s.Position];

        int i = 0;

        while (i < MSList.Count)
        {
            //n = current
            //o = old 
            MovingStruct oldMovingStruct = MSList[i];

            i++;

            if (oldMovingStruct == s || (s.icon != null && oldMovingStruct.icon != s.icon))
                continue;

            //Check if we can conbine them
            (string beforeNumber, int number, string afterNumber) first_v = separateString(s.text);
            (string beforeNumber, int number, string afterNumber) second_v = separateString(oldMovingStruct.text);

            if (Equivalent(first_v, second_v))
            {
                //COMBINE
                oldMovingStruct.text = CombineMovingText(first_v, second_v);

                oldMovingStruct.Launched = true;

                oldMovingStruct.Restart(s.color);

                return;
            }
        }

        //DON'T COMBINE 
        MovingText_Order[s.Position].Add(s);
    }

    public static float CdTimeMovingText = 0.6f;

    /// <summary>
    /// Add a moving text in the scene for exemple damage "-5" or else and then display it when he can
    /// </summary>
    /// <param name="text">The text to show</param>
    /// <param name="color">The color of the text</param>
    /// <param name="Position">The position of the text</param>
    /// <param name="TravelDistance">How much y distance it will travel</param>
    public static MovingStruct Display_movingText(string text, V.Color color, Vector3 Position, float TravelDistance, float size = 1, Sprite icon = null)
    {
        MovingStruct newMovingText = new MovingStruct();

        newMovingText.text = text;
        newMovingText.color = color;
        if (color == V.Color.red)
        {
            newMovingText.orderInLayer = 2;
        }

        newMovingText.Position = Position;
        if (!F.IsTileConfortablySeenable(F.ConvertToStringDependingOfGrid(Position)))
        {
            newMovingText.Position -= V.movingText_StartDistance * 2;
        }

        newMovingText.TravelDistance = TravelDistance;

        newMovingText.Launched = false;

        newMovingText.Cd = CdTimeMovingText;

        newMovingText.size = size;

        newMovingText.icon = icon;

        AddMovingTextInLs(newMovingText);

        return newMovingText;
    }

    /// <summary>
    /// The class for movingStruct = little text
    /// </summary>
    public class MovingStruct
    {
        public string text;
        public V.Color color;
        public Vector3 Position;
        public float TravelDistance;
        public float size = 1;

        public bool Launched;

        public int orderInLayer = 1;

        public float Cd;

        public Sprite icon;

        public void Restart(V.Color col)
        {
            Cd = CdTimeMovingText;

            color = col;

            movingStrucDic_remove(this, true);

            Display_movingText_Instantiate(this);
        }

        /* public MovingStruct(string txt, V.Color col, Vector3 pos, float TravelDis, bool Launched, float Cd, Sprite icon, int orderInLayer = 1, float size = 1)
         {
             text = txt;
             color = col;
             Position = pos;
             TravelDistance = TravelDis;
             this.Launched = Launched;
             this.Cd = Cd;
             this.icon = icon;
             this.orderInLayer = orderInLayer;
             this.size = size;
         }*/
    }

    public static string CombineMovingText((string beforeNumber, int number, string afterNumber) first_v,
        (string beforeNumber, int number, string afterNumber) second_v)
    {
        return first_v.beforeNumber + (first_v.number + second_v.number).ToString() + first_v.afterNumber;
    }

    public static bool Equivalent((string beforeNumber, int number, string afterNumber) first_v,
        (string beforeNumber, int number, string afterNumber) second_v)
    {
        return first_v.beforeNumber + first_v.afterNumber == second_v.beforeNumber + second_v.afterNumber;
    }

    public static bool Equivalent(string first, string second)
    {
        return Equivalent(separateString(first), separateString(second));
    }

    public static (string beforeNumber, int number, string afterNumber) separateString(string a)
    {
        List<char> bannedChar = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        bool continueCountingNumber = true;

        string a_number = "", a_before = "", a_after = "";

        foreach (char c in a)
        {
            if (bannedChar.Contains(c))
            {
                if (continueCountingNumber)
                    a_number += c;
            }
            else
            {
                if (a_number.Length > 0)
                {
                    a_after += c;
                    continueCountingNumber = false;
                }
                else
                {
                    a_before += c;
                }
            }
        }

        int result = 0;

        int.TryParse(a_number, out result);

        return (a_before, result, a_after);
    }

    public static float movingText_TravelDistance = 1.4f;

    public static MovingStruct Display_movingText_basicValue(string text, V.Color color, Vector3 Position, Sprite icon = null)
    {
        var entity = AliveEntity.GetEntityByPos(F.ConvertToStringDependingOfGrid(Position));

        Vector3 pos = Position + V.movingText_StartDistance;

        if (entity != null && entity == V.player_entity)
            pos = Position + V.movingText_StartDistance_Player;

        return Display_movingText(text, color, pos, movingText_TravelDistance, 1, icon);
    }

    public static Dictionary<MovingStruct, GameObject> movingStructByGameobject = new Dictionary<MovingStruct, GameObject>();

    public static void movingStrucDic_add(MovingStruct k, GameObject v)
    {
        if (!movingStrucDic_contain(k))
        {
            movingStructByGameobject.Add(k, v);
        }
    }

    public static void movingStrucDic_remove(MovingStruct k, bool destroy)
    {
        if (movingStrucDic_contain(k))
        {
            if (destroy)
            {
                Destroy(movingStructByGameobject[k].gameObject);
            }

            movingStructByGameobject.Remove(k);
        }
    }


    public static bool movingStrucDic_contain(MovingStruct k)
    {
        return movingStructByGameobject.ContainsKey(k);
    }

    /// <summary>
    /// Instatiate a given movingText
    /// </summary>
    /// <param name="movingText">The concerned moving text</param>
    public static void Display_movingText_Instantiate(MovingStruct movingText)
    {
        float speed = 0.9f;

        //Instantiate a moving text
        GameObject currentText = Instantiate(V.main_ui.movingTextG);

        movingStrucDic_add(movingText, currentText);

        Canvas canv = currentText.GetComponent<Canvas>();

        canv.sortingOrder = movingText.orderInLayer;

        //Get the text
        Text currentText_text = currentText.transform.GetChild(0).GetComponent<Text>();

        //Outline outLine = currentText_text.GetComponent<Outline>();

        //Give him text value
        currentText_text.text = movingText.text;
        //Give him color value
        currentText_text.color = V.GetColor(movingText.color);

        currentText_text.transform.localScale *= movingText.size;

        Image img = currentText_text.transform.GetChild(0).GetComponent<Image>();

        if (movingText.icon != null)
        {
            img.sprite = movingText.icon;
        }

        img.gameObject.SetActive(movingText.icon != null);

        Canvas.ForceUpdateCanvases();

        RectTransform holder = currentText.transform.GetChild(0).GetComponent<RectTransform>();

        float additional_x = 0;

        if (movingText.icon != null)
        {
            additional_x += 30 /*+ currentText_text.rectTransform.sizeDelta.x / 2*/;

            additional_x *= holder.transform.localScale.x;
        }

        holder.anchoredPosition = new Vector3(-additional_x, 0, 0);

        //Set his position
        currentText.transform.position = movingText.Position;

        //Make his animation
        V.main_ui.Move_DisplayMoving(currentText, speed, movingText.TravelDistance, img, movingText, true);
    }

    public void Move_DisplayMoving(GameObject text, float speed, float travelDistance, Image iconImg, MovingStruct s, bool EarlyZoom = true)
    {
        StartCoroutine(Move_DisplayMovingCo(text, speed, travelDistance, iconImg, s, EarlyZoom));
    }

    public IEnumerator Move_DisplayMovingCo(GameObject text, float speed, float travelDistance, Image iconImg, MovingStruct s, bool EarlyZoom = true)
    {
        Vector3 startPos = text.transform.position;
        Vector3 targetPos = startPos + new Vector3(0, travelDistance, 0);

        float distance = (targetPos.y - text.transform.position.y);

        Vector3 currentScale = text.transform.localScale;

        float zoom = 0.5f;

        if (EarlyZoom)
        {
            text.transform.localScale = currentScale * 1.5f;

            text.transform.DOScale(currentScale, zoom);
        }
        else
        {
            text.transform.localScale = currentScale * 0.7f;

            text.transform.DOScale(currentScale, zoom);
        }
        //Get the text
        Text currentText_text = text.transform.GetChild(0).GetComponent<Text>();

        Outline outLine = currentText_text.GetComponent<Outline>();
        yield return new WaitForSeconds(zoom);

        if (text != null)
            text.transform.DOScale(currentScale * 0.8f, travelDistance * 2);

        yield return new WaitForSeconds(0.1f);

        float fadeSpeed = 0.4f;

        bool isNull = false;

        while (distance > 0.1f)
        {
            if (text == null)
            {
                isNull = true;
                break;
            }
            text.transform.position += (targetPos - startPos) * Time.deltaTime * speed * 0.4f;
            distance = (targetPos.y - text.transform.position.y);
            yield return new WaitForEndOfFrame();
            if (distance <= 0.35f)
            {
                if (text == null)
                {
                    isNull = true;
                    break;
                }
                iconImg.DOFade(0, fadeSpeed);
                currentText_text.DOFade(0, fadeSpeed);
                outLine.DOFade(0, fadeSpeed * 0.3f);
            }
        }

        if (!isNull)
            movingStrucDic_remove(s, true);
    }
}
