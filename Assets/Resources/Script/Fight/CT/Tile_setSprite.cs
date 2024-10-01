using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Tile : MonoBehaviour
{
    public void SetSprite(bool IgnoreAllEntity, List<string> CombatTileList)
    {
        var posTuple = F.ReadString(pos);

        int x = posTuple.x;
        int y = posTuple.y;

        bool containRight = SetSprite_IsContain(x + 1, y, IgnoreAllEntity, CombatTileList),
            containleft = SetSprite_IsContain(x - 1, y, IgnoreAllEntity, CombatTileList),
            containup = SetSprite_IsContain(x, y + 1, IgnoreAllEntity, CombatTileList),
            containdown = SetSprite_IsContain(x, y - 1, IgnoreAllEntity, CombatTileList);

        bool Check(bool right, bool left, bool up, bool down)
        {
            return right == containRight && left == containleft && up == containup && down == containdown;
        }

        Sprite s = null;

        if (Check(right: false, left: true, up: true, down: false)) //Up and left contain
        {
            s = Tile_Gestion.Instance.smooth_downRight;
        }
        else if (Check(right: false, left: true, up: false, down: true))  //down and left contain
        {
            s = Tile_Gestion.Instance.smooth_upRight;
        }
        else if (Check(right: true, left: false, up: false, down: true))  //down and right contain
        {
            s = Tile_Gestion.Instance.smooth_upLeft;
        }
        else if (Check(right: true, left: false, up: true, down: false))  //up and right contain
        {
            s = Tile_Gestion.Instance.smooth_downLeft;
        }
        else if (Check(right: false, left: false, up: false, down: true)) //only down contain
        {
            s = Tile_Gestion.Instance.smooth_up;
        }
        else if (Check(right: false, left: false, up: true, down: false)) //only up contain
        {
            s = Tile_Gestion.Instance.smooth_down;
        }
        else if (Check(right: true, left: false, up: false, down: false)) //only right contain
        {
            s = Tile_Gestion.Instance.smooth_left;
        }
        else if (Check(right: false, left: true, up: false, down: false)) //only left contain
        {
            s = Tile_Gestion.Instance.smooth_right;
        }
        else
        {
            s = Tile_Gestion.Instance.normal;
        }

        render.sprite = s;
    }

    public void SetSpriteOld(bool IgnoreAllEntity, List<string> CombatTileList)
    {
        int x = F.ReadString_x(pos); //The current x of this pos
        int y = F.ReadString_y(pos); //The current y of this pos

        bool contain_right = SetSprite_IsContain(x + 1, y, IgnoreAllEntity, CombatTileList),
            contain_left = SetSprite_IsContain(x - 1, y, IgnoreAllEntity, CombatTileList),
            contain_up = SetSprite_IsContain(x, y + 1, IgnoreAllEntity, CombatTileList),
            contain_down = SetSprite_IsContain(x, y - 1, IgnoreAllEntity, CombatTileList);

        (bool up, bool down, bool right, bool left) c = (contain_up, contain_down, contain_right, contain_left);

        Sprite s = null;

        if (NoOneTrue(c))
        {
            s = Tile_Gestion.Instance.smooth;
        }
        else if (OnlyTwoTrue(Dir.up, Dir.left, c)) //Up and left contain
        {
            s = Tile_Gestion.Instance.smooth_downRight;
        }
        else if (OnlyTwoTrue(Dir.down, Dir.left, c))  //down and left contain
        {
            s = Tile_Gestion.Instance.smooth_upRight;
        }
        else if (OnlyTwoTrue(Dir.down, Dir.right, c))  //down and right contain
        {
            s = Tile_Gestion.Instance.smooth_upLeft;
        }
        else if (OnlyTwoTrue(Dir.up, Dir.right, c))  //up and right contain
        {
            s = Tile_Gestion.Instance.smooth_downLeft;
        }
        else if (OnlyOneTrue(Dir.down, c)) //only down contain
        {
            s = Tile_Gestion.Instance.smooth_up;
        }
        else if (OnlyOneTrue(Dir.up, c)) //only up contain
        {
            s = Tile_Gestion.Instance.smooth_down;
        }
        else if (OnlyOneTrue(Dir.right, c)) //only right contain
        {
            s = Tile_Gestion.Instance.smooth_left;
        }
        else if (OnlyOneTrue(Dir.left, c)) //only left contain
        {
            s = Tile_Gestion.Instance.smooth_right;
        }
        else
        {
            s = Tile_Gestion.Instance.normal;
        }

        if (s == null)
            throw new System.Exception("No tile sprite have been found");

        render.sprite = s;
    }

    public bool SetSprite_IsContain(int x, int y, bool IgnoreAllEntity, List<string> CombatTileList)
    {
        bool containTile = CombatTileList.Contains(F.ConvertToString(x, y));

        if (IgnoreAllEntity)
        {
            return containTile;
        }
        else
        {
            bool containEntity = V.player_entity.CurrentPosition_string == F.ConvertToString(x, y);

            return containTile || containEntity;
        }
    }

    public bool OnlyTwoTrue(Dir dir, Dir dir_2, (bool up, bool down, bool right, bool left) c)
    {
        List<int> l = new List<int> { (int)dir, (int)dir_2 };

        return CheckMultipleTrue(l, c);
    }

    public enum Dir { up, down, right, left }

    public bool OnlyOneTrue(Dir dir, (bool up, bool down, bool right, bool left) c)
    {
        List<int> l = new List<int> { (int)dir };

        return CheckMultipleTrue(l, c);
    }

    public bool CheckMultipleTrue(List<int> dirs, (bool up, bool down, bool right, bool left) c)
    {
        List<bool> contain = new List<bool> { c.up, c.down, c.right, c.left };

        int i = 0;
        while (i < contain.Count)
        {
            if (dirs.Contains(i) && !contain[i]) //If the wanted tile isn't true return false
            {
                return false;
            }
            else if (!dirs.Contains(i) && contain[i]) //If an unwanted tile is true return fals
            {
                return false;
            }

            i++;
        }

        return true;
    }

    public bool NoOneTrue((bool up, bool down, bool right, bool left) c)
    {
        return !c.right && !c.left && !c.up && !c.down;
    }

    public bool AllTrue((bool up, bool down, bool right, bool left) c)
    {
        return c.right && c.left && c.up && c.down;
    }
}
