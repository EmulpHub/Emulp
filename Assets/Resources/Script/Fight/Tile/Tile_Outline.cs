using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Tile : MonoBehaviour
{
    public void SetOutline ()
    {
        render.SetOutline(new Color32(200, 200, 200, 255), 0.1f);
    }
}
