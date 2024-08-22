using UnityEngine;
using UnityEngine.UI;

public class TileIcon : MonoBehaviour
{
    public Image img;

    public string pos;

    static Sprite _sp_spell, _sp_movement;

    private static Sprite Sp_spell
    {
        get
        {
            if (_sp_spell == null)
                _sp_spell = Resources.Load<Sprite>("Image/Tile/TileIcon/tileIcon_Spell");

            return _sp_spell;
        }
    }

    private static Sprite Sp_movement
    {
        get
        {
            if (_sp_movement == null)
                _sp_movement = Resources.Load<Sprite>("Image/Tile/TileIcon/tileIcon_Movement");

            return _sp_movement;
        }
    }

    public TileIcon SetImg(Sprite img)
    {
        this.img.sprite = img;
        return this;
    }

    public TileIcon SetColor(Color color)
    {
        this.img.color = color;
        return this;
    }

    public TileIcon SetPos(string position)
    {
        pos = position;

        transform.position = F.ConvertToWorldVector2(position);
        return this;
    }

    public TileIcon SetScale(float scale)
    {
        transform.localScale = new Vector3(scale, scale, 1);
        return this;
    }
}
