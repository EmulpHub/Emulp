using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PathFindingName;

public class MonsterBehaviorManager : MonoBehaviour
{
    private static MonsterBehaviorManager _instance;
    public static MonsterBehaviorManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.Find("MonsterBehaviorManager").GetComponent<MonsterBehaviorManager>();
            return _instance;
        }
    }

    public void StartToBehave()
    {
        StartCoroutine(Manage());
    }

    private IEnumerator Manage()
    {
        while (V.IsFight() && !EntityOrder.Instance.IsTurnOf_Player())
        {
            var info = new TurnInfo();

            ChooseActor(info, new List<Monster>(EntityOrder.InstanceEnnemy.list));

            if (info.mode == TurnInfo.Mode.turn)
            {
                Action_nextTurn.Add();
            }
            else if (info.mode == TurnInfo.Mode.spell)
            {
                ActionManager.Instance.AddToDo(info.Actor_spell.CombatBehavior.Behave().action);
            }
            else if (info.mode == TurnInfo.Mode.move)
            {
                List<Action> listMove = MakeMoveOnActor(info);

                Action_multi.Add(listMove);
            }

            yield return new WaitUntil(() => !ActionManager.Instance.Running());
        }
    }

    private void ChooseActor(TurnInfo info, List<Monster> listMonster)
    {
        var listActorMovement = new List<MonsterWithMovementAction>();

        Monster FirstSpellMonster = null;

        //If at least one want to move, they need all to move before someone attack
        foreach (var monster in listMonster)
        {
            var action = monster.CombatBehavior.ChooseAction();

            switch (action.priorityLayer)
            {
                case MonsterAction.PriorityLayer.Turn:
                    break;
                case MonsterAction.PriorityLayer.Spell:
                    if (FirstSpellMonster is null)
                    {
                        FirstSpellMonster = monster;
                    }
                    break;
                case MonsterAction.PriorityLayer.Movement:
                    listActorMovement.Add(new MonsterWithMovementAction(monster, (MonsterAction_Movement)action));
                    break;
                default:
                    throw new System.Exception("A case has not been handled");
            }
        }

        if (listActorMovement.Count > 0)
        {
            info.SetMode_move(listActorMovement);
        }
        else if (FirstSpellMonster is not null)
        {
            info.SetMode_spell(FirstSpellMonster);
        }
        else
        {
            info.SetMode_turn();
        }
    }

    private List<Action> MakeMoveOnActor(TurnInfo info)
    {
        //info.ListActor_Movement contient les monstres qui veulent bouger, elle represente la liste des acteurs
        //1 : On parcourt chaque acteur et on determine une liste des chemins qu'ils peuvent emprunter pour arriver à une case qui les satisfais en le plus petit nombre de pas
        //2 : On choisis l'acteur avec le plus petit nombre de chemin OU SI ils ont le même nombre de chemin on choisis le chemin le plus petit 
        //3 :   On ajoute sa case d'arrivée dans une liste ignored tile que les monstres doivent respecter lors du pathfinding et on enleve de la liste des acteurs ce monstre
        //4 :   On repasse a l'etape 1 

        var listActorMove = new List<MonsterWithMovementAction>(info.ListActor_Movement);

        var listForbideenPos = Walkable.GetCommonForbideenPos();

        var walkableParam = new WalkableParam(listForbideenPos, true);

        var result = new List<Action>();

        while (listActorMove.Count > 0)
        {
            var dicoListPathPerMonster = new Dictionary<MonsterWithMovementAction, List<PotentialPathInfo>>();

            foreach (var monsterWithAction in listActorMove)
            {
                var listListPath = GetPath(monsterWithAction, walkableParam);

                if (listListPath.Count > 0)
                    dicoListPathPerMonster.Add(monsterWithAction, listListPath);
            }

            if (dicoListPathPerMonster.Count == 0)
                throw new System.Exception("Pas de chemin");

            var MonsterToMove = ChooseWichMonsterMove(dicoListPathPerMonster, out var ChoosenMonsterWithMovementAction);

            var movementAction = Action_movement.Create(new PathParam(MonsterToMove.monster.CurrentPosition_string, MonsterToMove.endPos, new WalkableParam(new List<string>(listForbideenPos), true)), MonsterToMove.monster);

            listForbideenPos.Remove((MonsterToMove.monster.CurrentPosition_string));
            listForbideenPos.Add(MonsterToMove.endPos);
            result.Add(movementAction);
            listActorMove.Remove(ChoosenMonsterWithMovementAction);
        }

        return result;
    }

    private (Monster monster, MonsterAction_Movement monsterActionMovement, string endPos) ChooseWichMonsterMove(Dictionary<MonsterWithMovementAction, List<PotentialPathInfo>> dico, out MonsterWithMovementAction monsterWithMovementAction)
    {
        MonsterAction_Movement monsterActionMovement = null;
        Monster monster = null;
        string endPos = "";

        //Si ils ont des nombres de chemins differents on prend le monstre avec le plus grand nombre de chemin
        if (ContientNombreDeCheminDifferent(dico))
        {
            var keyValue = dico.OrderBy(a => a.Value.Count).First();

            monster = keyValue.Key.monster;
            monsterActionMovement = keyValue.Key.monsterAction;
            endPos = keyValue.Value[0].endPos;

            monsterWithMovementAction = keyValue.Key;
        }
        //Sinon on prend le chemin le plus petit
        else
        {
            var keyValue = dico.OrderBy(a => a.Value.Min(b => b.distance)).First();

            monster = keyValue.Key.monster;
            monsterActionMovement = keyValue.Key.monsterAction;
            endPos = keyValue.Value[0].endPos;

            monsterWithMovementAction = keyValue.Key;
        }

        return (monster, monsterActionMovement, endPos);
    }

    private bool ContientNombreDeCheminDifferent(Dictionary<MonsterWithMovementAction, List<PotentialPathInfo>> dico)
    {
        int nombreCheminUnique = -1;

        foreach (var keyVal in dico)
        {
            int nb = keyVal.Value.Count;

            if (nombreCheminUnique == -1)
                nombreCheminUnique = nb;
            else if (nombreCheminUnique != nb)
                return true;
        }

        return false;
    }

    private List<PotentialPathInfo> GetPath(MonsterWithMovementAction monsterWithAction, WalkableParam walkableParam)
    {
        return PossibleWalkingTile.GetListPath(monsterWithAction.monster.CurrentPosition_string, monsterWithAction.monsterAction.ConditionPos, walkableParam);
    }

    private class TurnInfo
    {
        public List<MonsterWithMovementAction> ListActor_Movement = new List<MonsterWithMovementAction>();

        public Monster Actor_spell { get; set; }

        public enum Mode { move, spell, turn }

        public Mode mode { get; private set; }

        public TurnInfo SetMode_spell(Monster actorSpell)
        {
            Actor_spell = actorSpell;
            this.mode = Mode.spell;
            return this;
        }

        public TurnInfo SetMode_move(List<MonsterWithMovementAction> listActor_Movement)
        {
            this.ListActor_Movement = listActor_Movement;
            this.mode = Mode.move;
            return this;
        }

        public TurnInfo SetMode_turn()
        {
            this.mode = Mode.turn;
            return this;
        }
    }

    private class MonsterWithMovementAction
    {
        public Monster monster { get; private set; }
        public MonsterAction_Movement monsterAction { get; private set; }

        public MonsterWithMovementAction(Monster monster, MonsterAction_Movement monsterAction)
        {
            this.monster = monster;
            this.monsterAction = monsterAction;
        }
    }
}
