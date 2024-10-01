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

    public void Start()
    {
        title.text = "Action to do";
    }

    private void Update()
    {
        Action running = ActionManager.Instance.currentRunningAction;

        if (ActionManager.Instance.Running() && !AllElement.ContainsKey(running.Id))
        {
            Text newText = Instantiate(Element.gameObject, ElementHolder).GetComponent<Text>();

            newText.text = running.debug();

            AllElement.Add(running.Id, (newText, running));
        }

        List<int> AllElement_Keys = new List<int>(AllElement.Keys);

        AllElement_Keys.Sort();

        int i = 0;
        while (i < AllElement_Keys.Count)
        {
            int num = AllElement_Keys[i];

            var info = AllElement[num];

            if (info.action.IsFinished() && !RemovedElement.Contains(num))
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
