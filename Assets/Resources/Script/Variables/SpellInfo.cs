using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInfo : MonoBehaviour
{
    public Sprite graphique, graphique_gray;
    public int rangeMin, rangeMax, pa_cost, cd;
    public string title, description;
    public Spell.Range_type range_Type;
    public SpellGestion.CursorMode cursorMode;

    public Color32 col;

    public bool IsWeapon;

    public SpellGestion.range_effect_size range_effect;

    public List<string> range_effect_tile = new List<string>();

    public List<(string pos, string toAdd)> range_effect_wayPoint = new List<(string pos, string toAdd)>();

    public SpellInfo(SpellGestion.range_effect_size range_effect, bool weapon = false)
    {
        this.range_effect = range_effect;

        IsWeapon = weapon;

        SetRange_effectTile();
    }

    public void SetRange_effectTile()
    {
        List<(string pos, string toAdd)> CircularWayPoint(int dis)
        {
            List<(string pos, string toAdd)> wp = new List<(string pos, string toAdd)>();

            List<(int x, int y)> pos = new List<(int x, int y)>
            {(-1,0),(0,1),(1,0),(0,-1) };

            foreach ((int x, int y) v in pos)
            {
                string n = F.ConvertToString(v.x * dis, v.y * dis);

                string toAdd = F.ConvertToString(v.x, v.y);

                wp.Add((n, toAdd));
            }

            return wp;
        }


        switch (range_effect)
        {
            case SpellGestion.range_effect_size.singleTarget: //One tile

                range_effect_tile = new List<string>
                {"0_0"};

                range_effect_wayPoint = CircularWayPoint(1);
                break;
            case SpellGestion.range_effect_size.oneSquareAround: //Around

                range_effect_tile = new List<string>
                {"0_0","1_0","-1_0","0_1","0_-1"};

                range_effect_wayPoint = CircularWayPoint(2);

                break;
            case SpellGestion.range_effect_size.twoSquareAround: //two square around the target
                range_effect_tile = new List<string>
                    {"0_0","1_0","-1_0","0_1","0_-1",
                 "2_0","1_1","-1_1","1_-1","0_2","-1_-1","-2_0","0_-2"};

                range_effect_wayPoint = CircularWayPoint(3);

                break;
            case SpellGestion.range_effect_size.threeSquareAround: //three square around the target
                range_effect_tile = new List<string>
                    {"0_0","1_0","-1_0","0_1","0_-1",
                 "2_0","1_1","-1_1","1_-1","0_2","-1_-1","-2_0","0_-2",
                "1_2","0_3","0_-3","3_0","-3_0","-1_2","-2_1","-2_-1","-1_-2","1_-2","2_1","2_-1"};

                range_effect_wayPoint = CircularWayPoint(4);

                break;

            case SpellGestion.range_effect_size.Cone: //Cone tile

                //range_effect_tile_all = new List<string>();

                range_effect_tile = new List<string>
                {"0_0","0_1","1_1","-1_1" };

                //De la gauche a la droite
                range_effect_wayPoint = new List<(string pos, string toAdd)>
                { ("-2_2","-1_1"),("2_2","1_1") };

                break;
            case SpellGestion.range_effect_size.Cone_Inverted: //Cone tile

                //range_effect_tile_all = new List<string>();

                range_effect_tile = new List<string>
                {"0_0","0_1","1_0","-1_0" };

                range_effect_wayPoint = new List<(string pos, string toAdd)>
                { ("-2_0","-1_0"),("0_2","0_1"),("2_0","1_0") };

                break;

            case SpellGestion.range_effect_size.line_2:
                //range_effect_tile_all = new List<string>();

                range_effect_tile = new List<string>
                {"0_0","0_1" };

                range_effect_wayPoint = new List<(string pos, string toAdd)>
                { ("0_2","0_1") };

                break;
            case SpellGestion.range_effect_size.Front3line:
                //range_effect_tile_all = new List<string>();

                range_effect_tile = new List<string>
                {"0_0","-1_0","1_0" };

                range_effect_wayPoint = new List<(string pos, string toAdd)>
                { ("-2_0","-1_0"),("-1_1","0_1"),("1_1","0_1"),("2_0","1_0") };
                break;
            case SpellGestion.range_effect_size.oneSquareAround_line: //Around

                range_effect_tile = new List<string>
                {"0_0","1_0","-1_0","0_1","0_-1"};

                range_effect_wayPoint = CircularWayPoint(2);

                break;
            case SpellGestion.range_effect_size.twoSquareAround_line: //two square around the target
                range_effect_tile = new List<string>
                    {"0_0","1_0","-1_0","0_1","0_-1",
                 "2_0","0_2","-2_0","0_-2"};

                range_effect_wayPoint = CircularWayPoint(3);

                break;
            case SpellGestion.range_effect_size.threeSquareAround_line: //three square around the target
                range_effect_tile = new List<string>
                    {"0_0","1_0","-1_0","0_1","0_-1",
                 "2_0","0_2","-2_0","0_-2","0_3","0_-3","3_0","-3_0"};

                range_effect_wayPoint = CircularWayPoint(4);

                break;
            default:
                throw new System.Exception("NOT VALID RANGE EFFECT");
        }
    }


    public static List<string> GetStringEffectList(SpellGestion.List sp, string pos, string casterPos, bool forPlayer = true)
    {
        DirectionData.Direction dir = DirectionData.GetDirection(casterPos, pos);

        List<string> goodOne = SpellGestion.Get_RangeEffect_list(sp, dir, forPlayer);

        List<string> toReturn = new List<string>();

        (int x, int y) posxy = F.ReadString(pos);

        foreach (string s in goodOne)
        {
            (int x, int y) sxy = F.ReadString(s);

            toReturn.Add(F.ConvertToString(posxy.x + sxy.x, posxy.y + sxy.y));
        }

        return toReturn;
    }

    public SpellGestion.TargetMode targetMode;
}
