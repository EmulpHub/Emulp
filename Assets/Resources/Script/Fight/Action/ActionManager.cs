using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    private static ActionManager _instance;
    public static ActionManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.Find("ActionManager").GetComponent<ActionManager>();
            return _instance;
        }
    }

    public List<Action> listOfToDoAction { get; private set; } = new List<Action>();

    public void AddToDo(Action action)
    {
        if(Running()) 
            listOfToDoAction.Add(action);
        else
            ExecuteAction(action);
    }

    public void AddToDo_prioritary(Action action)
    {
        if(Running())
            listOfToDoAction.Insert(0, action);
        else
            ExecuteAction(action);
    }

    public Action currentRunningAction { get; private set; }

    public bool Running()
    {
        return currentRunningAction is not null;
    }

    public void Update()
    {
        if (Running() && currentRunningAction.IsFinished())
        {
            currentRunningAction = null;
            CheckNextActionToDo();
        }
    }

    public void CheckNextActionToDo ()
    {
        if (listOfToDoAction.Count == 0) return;

        ExecuteAction(listOfToDoAction[0]);
        listOfToDoAction.RemoveAt(0);
    }

    public void ExecuteAction (Action action)
    {
        currentRunningAction = action;
        currentRunningAction.Execute();
    }

    public void Clear()
    {
        int i = 0;
        while (i < listOfToDoAction.Count)
        {
            if (listOfToDoAction[i].CanBeErased)
                listOfToDoAction.RemoveAt(i);
            else
                i++;
        }
    }

    public bool Contain(Action.Type IsContainType, out int index)
    {
        foreach (Action action in listOfToDoAction)
        {
            if (action.type == IsContainType)
            {
                index = listOfToDoAction.IndexOf(action);

                return true;
            }
        }

        index = -1;

        return false;
    }

    public bool Contain(Action.Type IsContainType)
    {
        return Contain(IsContainType, out int i);
    }
}
