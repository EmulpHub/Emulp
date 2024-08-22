using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LayerCollectable
{
    public interface ICollectable
    {
        CollectableStatic.Type type { get; }
    }
}
