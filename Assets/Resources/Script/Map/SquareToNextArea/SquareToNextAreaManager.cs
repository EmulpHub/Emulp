using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareToNextAreaManager : MonoBehaviour
{
    private static SquareToNextAreaManager _instance;

    public static SquareToNextAreaManager Instance
    {
        get
        {
            if (_instance is null)
                _instance = new SquareToNextAreaManager();

            return _instance;
        }
    }

    public Dictionary<DirectionData.Direction, SquareToNextArea> dico = new Dictionary<DirectionData.Direction, SquareToNextArea>();

    public void Init()
    {
        dico.Clear();

        dico.Add(DirectionData.Direction.right, SquareToNextArea.Create(DirectionData.Direction.right));
        dico.Add(DirectionData.Direction.left, SquareToNextArea.Create(DirectionData.Direction.left));
        dico.Add(DirectionData.Direction.up, SquareToNextArea.Create(DirectionData.Direction.up));
        dico.Add(DirectionData.Direction.down, SquareToNextArea.Create(DirectionData.Direction.down));
    }


    public SquareToNextArea Get(DirectionData.Direction dir)
    {
        return dico[dir];
    }
}
