using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debug_actionToDo : MonoBehaviour
{
    public Text title;
    public Text Element;

    public Transform ElementHolder;

    public Dictionary<int, (Text Text, Action action)> AllElement =
        new Dictionary<int, (Text Text, Action action)>();

    public List<int> RemovedElement = new List<int>();

    private void Update()
    {
        List<Action> a = new List<Action>(Action.ActionRunning);

        title.text = "Action to do (" + a.Count + " element)";

        int i = 0;
        while (i < a.Count)
        {
            Action cibled = a[i];

            int cibled_num = cibled.id;

            if (!AllElement.ContainsKey(cibled_num))
            {
               Text newText = Instantiate(Element.gameObject, ElementHolder).GetComponent<Text>();

                newText.text = cibled.debug();

                AllElement.Add(cibled_num, (newText, cibled));
            }

            i++;
        }

        List<int> AllElement_Keys = new List<int>(AllElement.Keys);

        AllElement_Keys.Sort();

        i = 0;
        while (i < AllElement_Keys.Count)
        {
            int num = AllElement_Keys[i];

            var info = AllElement[num];

            if (info.action.Finished() && !RemovedElement.Contains(num))
            {
                RemovedElement.Add(num);

                float delay = 3;

                StartCoroutine(RemoveWithDelay(num, delay));
            }

            i++;
        }
    }

    public void ClearRemovedElement()
    {
        RemovedElement.Clear();
    }

    public void RemoveAllElement()
    {
        while (AllElement.Count > 0)
        {
            RemoveAnElement(0);
        }
    }

    public IEnumerator RemoveWithDelay(int num, float delay)
    {
        yield return new WaitForSeconds(delay);
        RemoveAnElement(num);
    }

    public void RemoveAnElement(int num)
    {
        Destroy(AllElement[num].Text.gameObject);
        AllElement.Remove(num);
        RemovedElement.Remove(num);
    }
}
