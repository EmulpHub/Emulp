using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    private static int nextId = 0;

    private int _id = -1;

    public int Id
    {
        get
        {
            if(_id == -1)
            {
                _id = nextId;
                nextId++;
            }
            return _id;
        }
    }

    public bool CanBeErased = true, Executed = false;

    public enum Type { movement, nextTurn, wait, wait_fixed, spell, kill,multi }

    public Type type;

    public void Execute()
    {
        Executed = true;

        ActionManager.Instance.StartCoroutine(Execute_main());
    }

    protected virtual IEnumerator Execute_main() {

        yield return new WaitForEndOfFrame();
    }

    public virtual bool IsFinished()
    {
        return Executed;
    }

    public virtual string debug()
    {
        return "ERROR";
    }
}
