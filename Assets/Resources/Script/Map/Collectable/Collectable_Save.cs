using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Collectable : MonoBehaviour
{
    public abstract class Save
    {
        public bool collected { get; set; }

        public Save() { }

        public virtual bool cannotBeCreated() { return true; }

        public virtual Collectable Create()
        {
            throw new System.Exception("Cannot create from abstract class");
        }
    }

    public virtual Save ExportSave()
    {
        throw new System.Exception("Cannot export save from abstract class");
    }
}
