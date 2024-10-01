using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFindingName;

public class ForceHelp : MonoBehaviour
{
    public enum Type { ForceMovement, ForceAttackMonster }

    public Type type;

    public GameObject AddedException;

    public void Update()
    {
        if (type == Type.ForceMovement)
            ForceMovement_Update();
        else if (type == Type.ForceAttackMonster)
            ForceAttack_Update();

        if (V.game_state != V.State.fight)
        {
            DestroyThis();
        }
    }

    public bool visible = true;

    #region ForceMovement

    public string targetedSquare;

    public static GameObject ForceHelp_gameobject;

    public static void Instantiate_ForceMovement_TowardEntity(Entity target, bool visible = true)
    {
        if (ForceHelp_gameobject is null)
            ForceHelp_gameobject = Resources.Load<GameObject>("Prefab/Utility/ForceHelper");

        GameObject g = Instantiate(ForceHelp_gameobject);

        ForceHelp script = g.GetComponent<ForceHelp>();

        script.visible = visible;

        script.type = Type.ForceMovement;

        List<string> path = new List<string>();

        Vector2Int pos = V.player_entity.CurrentPosition;

        int i = 0;
        while (i < V.player_info.PM && i < path.Count)
        {
            //pos += V.ConvertLegalMoveInVector2Int(path[i]);

            i++;
        }

        string pos_string = F.ConvertToString(pos);

        script.targetedSquare = pos_string;
        /*
        if (CombatTile_Gestion.Exist(pos_string))
        {
            GameObject tile = CombatTile_Gestion.Get(pos_string).gameObject;

            script.AddedException = tile;

            ClickAutorization.Exception_Add(tile);

            ForceHelp_Arrow f = ForceHelp_Arrow.Instantiate_Arrow(tile.transform.position,
                delegate
            {
                return V.player_info.PM == 0;
            });
        }*/
    }

    public void ForceMovement_Update()
    {
        if (V.player_info.PM == 0)
        {
            ClickAutorization.Exception_Remove(AddedException);
            Destroy(this.gameObject);
        }
    }

    #endregion

    #region ForceAttack

    public static GameObject Instantiate_ForceAttack(Entity target)
    {
        if (ForceHelp_gameobject is null)
            ForceHelp_gameobject = Resources.Load<GameObject>("Prefab/Utility/ForceHelper");

        GameObject g = Instantiate(ForceHelp_gameobject);

        ForceHelp script = g.GetComponent<ForceHelp>();

        script.type = Type.ForceAttackMonster;

        script.target = target;

        script.NextAction();

        return g;
    }

    public void ForceAttack_Update()
    {
        /*if (check())
        {*/
        NextAction();
        //}
    }

    Entity target;

    public enum State { none, spell_select, spell_cast, nextTurn, towardMonster }

    public static State CurrentState = State.none;

    public static List<GameObject> AllArrow = new List<GameObject>();

    public void ClearAllArrow()
    {
        foreach (GameObject g in new List<GameObject>(AllArrow))
        {
            if (g is null)
                continue;

            Destroy(g);
        }

        AllArrow.Clear();
    }

    public void NextAction()
    {
        if (!EntityOrder.IsTurnOf_Player() || ActionManager.Instance.Running())
        {
            if (CurrentState == State.none)
                return;

            ClearAllArrow();

            CurrentState = State.none;

            return;
        }


        bool ContainPm = V.player_info.PM > 0;

        bool ContainPa = V.player_info.PA > 2;

        string player_pos = V.player_entity.CurrentPosition_string, monster_pos = target.CurrentPosition_string;

        bool CloseEnoughWithMonster = F.DistanceBetweenTwoPos(player_pos, monster_pos) <= 3 && F.IsLineOfView(monster_pos, player_pos);

        if (CloseEnoughWithMonster)
        {
            if (ContainPa)
            {
                //CastingSpell
                if (SpellGestion.selectionnedSpell_list == SpellGestion.List.empty)
                {
                    if (CurrentState == State.spell_select)
                        return;
                    else
                        CurrentState = State.spell_select;

                    ClearAllArrow();

                    SelectSpell();
                }
                else
                {
                    if (CurrentState == State.spell_cast)
                        return;
                    else
                        CurrentState = State.spell_cast;

                    ClearAllArrow();


                    CastSpell();
                }
            }
            else
            {
                if (CurrentState == State.nextTurn)
                    return;
                else
                    CurrentState = State.nextTurn;

                ClearAllArrow();


                ForceNextTurn();
            }
        }
        else
        {
            if (ContainPm)
            {
                if (CurrentState == State.towardMonster)
                    return;
                else
                    CurrentState = State.towardMonster;

                ClearAllArrow();


                MoveTowardMonsterWithLineOfView();
            }
            else
            {
                if (CurrentState == State.nextTurn)
                    return;
                else
                    CurrentState = State.nextTurn;

                ClearAllArrow();


                ForceNextTurn();
            }
        }
    }

    public void MoveTowardMonsterWithLineOfView()
    {
        string pos = GetPosForLineOfView(target);
        /*
        if (CombatTile_Gestion.Exist(pos))
        {
            GameObject tile = CombatTile_Gestion.Get(pos).gameObject;

            ClickAutorization.Exception_Add(tile);
        }
        */
        if (visible)
        {
            ForceHelp_Arrow f = ForceHelp_Arrow.Instantiate_Arrow(F.ConvertToWorldVector2(pos), delegate
           {
               return V.player_entity.CurrentPosition_string == pos && !ActionManager.Instance.Running();
           });

            AllArrow.Add(f.gameObject);
        }
    }

    public void ForceNextTurn()
    {
        ClickAutorization.Exception_Add(Main_Object.Get(Main_Object.objects.button_endOfTurn));

        int saveId = EntityOrder.id_turn;

        if (visible)
        {
            ForceHelp_Arrow f = ForceHelp_Arrow.Instantiate_Arrow_EndOfTurn(delegate
        {
            return !EntityOrder.IsTurnOf_Player();
        });

            AllArrow.Add(f.gameObject);
        }
    }

    public void SelectSpell()
    {
        ClickAutorization.Exception_Add(Main_Object.list[Main_Object.objects.spell_punch].gameObject);

        if (visible)
        {
            ForceHelp_Arrow f = ForceHelp_Arrow.Instantiate_Arrow(Main_Object.list[Main_Object.objects.spell_punch].transform.position, delegate
       {
           return SpellGestion.selectionnedSpell_list != SpellGestion.List.empty;
       });

            AllArrow.Add(f.gameObject);
        }
    }

    public void CastSpell()
    {
        float saveLife = target.Info.Life;

        string pos = target.CurrentPosition_string;

        /*
        if (CombatTile_Gestion.Exist(pos))
        {
            GameObject tile = CombatTile_Gestion.Get(pos).gameObject;

            ClickAutorization.Exception_Add(tile);
        }*/
        if (visible)
        {
            ForceHelp_Arrow f = ForceHelp_Arrow.Instantiate_Arrow(target.transform.position, delegate
        {
            return target.Info.Life != saveLife;
        });

            AllArrow.Add(f.gameObject);
        }
    }

    public void DestroyThis()
    {
        ClearAllArrow();
        /*
        if (CombatTile_Gestion.Exist(target.CurrentPosition_string))
        {
            GameObject tile = CombatTile_Gestion.Get(target.CurrentPosition_string).gameObject;

            ClickAutorization.Exception_Remove(tile);
        }*/

        Destroy(this.gameObject);
    }

    public string GetPosForLineOfView(Entity target)
    {/*
        //pathToPlayer = PathFinding.FindingWithLineOfView(CurrentPosition_string, target.CurrentPosition_string, minDistance, maxDistance);

        List<V.LegalMove> path = PathFinding.FindingWithLineOfView(V.player_entity.CurrentPosition_string, target.CurrentPosition_string, 1, 3, V.player_entity);

        Vector2Int pos = V.player_entity.CurrentPosition;

        int i = 0;
        while (i < V.player_info.PM && i < path.Count)
        {
            pos += V.ConvertLegalMoveInVector2Int(path[i]);

            i++;
        }

        return F.ConvertToString(pos);
    */
        return target.CurrentPosition_string;
    }

    #endregion

}
