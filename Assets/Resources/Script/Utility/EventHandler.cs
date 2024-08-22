using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class EventHandler
{
    public EventHandler(bool isStatic)
    {
        if (isStatic)
            V.listStaticEvent.Add(this);
    }

    public Dictionary<int, object> dicAction = new Dictionary<int, object>();
    public Dictionary<int, object> dicAction_first = new Dictionary<int, object>();
    public Dictionary<int, object> dicAction_last = new Dictionary<int, object>();

    static int idMax = 1;

    protected void CallAll()
    {
        foreach (var a in new List<object>(dicAction_first.Values))
        {
            CallOne(a);
        }
        foreach (var a in new List<object>(dicAction.Values))
        {
            CallOne(a);
        }
        foreach (var a in new List<object>(dicAction_last.Values))
        {
            CallOne(a);
        }
    }

    protected virtual void CallOne(object d) { }

    internal virtual int GetMaxInt()
    {
        int id = idMax;
        idMax++;
        return id;
    }

    public void Remove(int id)
    {
        if (dicAction.ContainsKey(id))
            dicAction.Remove(id);
        else if (dicAction_first.ContainsKey(id))
            dicAction_first.Remove(id);
        else if (dicAction_last.ContainsKey(id))
            dicAction_last.Remove(id);
    }

    public void Clear()
    {
        dicAction.Clear();
    }

    public void Combine(EventHandler eventHandler)
    {
        foreach (object c in eventHandler.dicAction)
        {
            AddObject(c);
        }

        foreach (object c in eventHandler.dicAction_first)
        {
            AddObject_first(c);
        }

        foreach (object c in eventHandler.dicAction_last)
        {
            AddObject_last(c);
        }
    }

    private int AddObject(object d)
    {
        int id = GetMaxInt();

        dicAction.Add(id, d);

        return id;
    }

    private int AddObject_first(object d)
    {
        int id = GetMaxInt();

        dicAction_first.Add(id, d);

        return id;
    }
    private int AddObject_last(object d)
    {
        int id = GetMaxInt();

        dicAction_last.Add(id, d);

        return id;
    }
}

public class EventHandlerNoArg : EventHandler
{
    public EventHandlerNoArg(bool isStatic) : base(isStatic) { }

    public delegate void del();

    protected override void CallOne(object d)
    {
        ((del)d)();
    }

    public void Call()
    {
        CallAll();
    }

    public int Add(del d)
    {
        int id = GetMaxInt();

        dicAction.Add(id, d);

        return id;
    }

    public int Add_first(del d)
    {
        int id = GetMaxInt();

        dicAction_first.Add(id, d);

        return id;
    }
    public int Add_last(del d)
    {
        int id = GetMaxInt();

        dicAction_last.Add(id, d);

        return id;
    }
}

public class EventHandlerEffect : EventHandler
{
    public EventHandlerEffect(bool isStatic) : base(isStatic) { }

    public delegate void del(Effect e);

    Effect arg;

    protected override void CallOne(object d)
    {
        ((del)d)(arg);
    }

    public void Call(Effect arg)
    {
        this.arg = arg;

        CallAll();
    }
    public int Add(del d)
    {
        int id = GetMaxInt();

        dicAction.Add(id, d);

        return id;
    }

    public int Add_first(del d)
    {
        int id = GetMaxInt();

        dicAction_first.Add(id, d);

        return id;
    }
    public int Add_last(del d)
    {
        int id = GetMaxInt();

        dicAction_last.Add(id, d);

        return id;
    }
}

public class EventHandlerListDirection : EventHandler
{
    public EventHandlerListDirection(bool isStatic) : base(isStatic) { }

    public delegate void del(List<DirectionData.Direction> args);

    List<DirectionData.Direction> arg;

    protected override void CallOne(object d)
    {
        ((del)d)(arg);
    }

    public void Call(List<DirectionData.Direction> arg)
    {
        this.arg = arg;

        CallAll();
    }

    public int Add(del d)
    {
        int id = GetMaxInt();

        dicAction.Add(id, d);

        return id;
    }

    public int Add_first(del d)
    {
        int id = GetMaxInt();

        dicAction_first.Add(id, d);

        return id;
    }
    public int Add_last(del d)
    {
        int id = GetMaxInt();

        dicAction_last.Add(id, d);

        return id;
    }
}

public class EventHandlerfloatBeforeAfter : EventHandler
{
    public EventHandlerfloatBeforeAfter(bool isStatic) : base(isStatic) { }

    public delegate void del(float before, float after);

    float argBefore, argAfter;

    protected override void CallOne(object d)
    {
        ((del)d)(argBefore, argAfter);
    }

    public void Call(float before, float after)
    {
        argBefore = before;
        argAfter = after;

        CallAll();
    }
    public int Add(del d)
    {
        int id = GetMaxInt();

        dicAction.Add(id, d);

        return id;
    }

    public int Add_first(del d)
    {
        int id = GetMaxInt();

        dicAction_first.Add(id, d);

        return id;
    }
    public int Add_last(del d)
    {
        int id = GetMaxInt();

        dicAction_last.Add(id, d);

        return id;
    }
}

public class EventHandlerEntityFloat : EventHandler
{
    public EventHandlerEntityFloat(bool isStatic) : base(isStatic) { }

    public delegate void del(Entity e, float dmg);

    float argDmg;
    Entity argEntity;

    protected override void CallOne(object d)
    {
        ((del)d)(argEntity, argDmg);
    }

    public void Call(Entity argEntity, float argDmg)
    {
        this.argDmg = argDmg;
        this.argEntity = argEntity;

        CallAll();
    }
    public int Add(del d)
    {
        int id = GetMaxInt();

        dicAction.Add(id, d);

        return id;
    }

    public int Add_first(del d)
    {
        int id = GetMaxInt();

        dicAction_first.Add(id, d);

        return id;
    }
    public int Add_last(del d)
    {
        int id = GetMaxInt();

        dicAction_last.Add(id, d);

        return id;
    }
}

public class EventHandlerEntityDmg : EventHandler
{
    public EventHandlerEntityDmg(bool isStatic) : base(isStatic) { }

    public delegate void del(Entity e, InfoDamage dmg);

    InfoDamage argDmg;
    Entity argEntity;

    protected override void CallOne(object d)
    {
        ((del)d)(argEntity, argDmg);
    }

    public void Call(Entity argEntity, InfoDamage argDmg)
    {
        this.argDmg = argDmg;
        this.argEntity = argEntity;

        CallAll();
    }
    public int Add(del d)
    {
        int id = GetMaxInt();

        dicAction.Add(id, d);

        return id;
    }

    public int Add_first(del d)
    {
        int id = GetMaxInt();

        dicAction_first.Add(id, d);

        return id;
    }
    public int Add_last(del d)
    {
        int id = GetMaxInt();

        dicAction_last.Add(id, d);

        return id;
    }
}

public class EventHandlerEntity : EventHandler
{
    public EventHandlerEntity(bool isStatic) : base(isStatic) { }

    public delegate void del(Entity e);

    Entity argEntity;

    protected override void CallOne(object d)
    {
        ((del)d)(argEntity);
    }

    public void Call(Entity argEntity)
    {
        this.argEntity = argEntity;

        CallAll();
    }
    public int Add(del d)
    {
        int id = GetMaxInt();

        dicAction.Add(id, d);

        return id;
    }

    public int Add_first(del d)
    {
        int id = GetMaxInt();

        dicAction_first.Add(id, d);

        return id;
    }
    public int Add_last(del d)
    {
        int id = GetMaxInt();

        dicAction_last.Add(id, d);

        return id;
    }
}

public class EventHandlerInt : EventHandler
{
    public EventHandlerInt(bool isStatic) : base(isStatic) { }

    public delegate void del(int value);

    int argValue;

    protected override void CallOne(object d)
    {
        ((del)d)(argValue);
    }

    public void Call(int argValue)
    {
        this.argValue = argValue;

        CallAll();
    }
    public int Add(del d)
    {
        int id = GetMaxInt();

        dicAction.Add(id, d);

        return id;
    }

    public int Add_first(del d)
    {
        int id = GetMaxInt();

        dicAction_first.Add(id, d);

        return id;
    }
    public int Add_last(del d)
    {
        int id = GetMaxInt();

        dicAction_last.Add(id, d);

        return id;
    }
}

public class EventHandlerFloat : EventHandler
{
    public EventHandlerFloat(bool isStatic) : base(isStatic) { }

    public delegate void del(float value);

    float argValue;

    protected override void CallOne(object d)
    {
        ((del)d)(argValue);
    }

    public void Call(float argValue)
    {
        this.argValue = argValue;

        CallAll();
    }
    public int Add(del d)
    {
        int id = GetMaxInt();

        dicAction.Add(id, d);

        return id;
    }

    public int Add_first(del d)
    {
        int id = GetMaxInt();

        dicAction_first.Add(id, d);

        return id;
    }
    public int Add_last(del d)
    {
        int id = GetMaxInt();

        dicAction_last.Add(id, d);

        return id;
    }
}

public class EventHandlerString : EventHandler
{
    public EventHandlerString(bool isStatic) : base(isStatic) { }

    public delegate void del(string arg);

    string arg;

    protected override void CallOne(object d)
    {
        ((del)d)(arg);
    }

    public void Call(string arg)
    {
        this.arg = arg;

        CallAll();
    }

    public int Add(del d)
    {
        int id = GetMaxInt();

        dicAction.Add(id, d);

        return id;
    }

    public int Add_first(del d)
    {
        int id = GetMaxInt();

        dicAction_first.Add(id, d);

        return id;
    }

    public int Add_last(del d)
    {
        int id = GetMaxInt();

        dicAction_last.Add(id, d);

        return id;
    }
}