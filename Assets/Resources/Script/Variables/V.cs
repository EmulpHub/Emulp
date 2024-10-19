using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

[SerializeField]
public partial class V : MonoBehaviour
{
    private static Transform InGameCreatedGameobjectHolder;

    public static Transform inGameCreatedGameobjectHolder
    {
        get
        {
            if (InGameCreatedGameobjectHolder == null)
                InGameCreatedGameobjectHolder = new GameObject("InGameCreatedGameobjectHolder").transform;

            return InGameCreatedGameobjectHolder;
        }

        set { InGameCreatedGameobjectHolder = value; }
    }

    public static Vector3 movingText_StartDistance = new Vector3(0, 2.5f, 0), movingText_StartDistance_Player = new Vector3(0, 2.1f, 0);

    public static Vector2 Entity_DistanceToBody = new Vector2(0, 0.5f);

    public static Vector3 Entity_DistanceToBody_Vector3 = new Vector3(0, 0.5f, 0);

    public static Vector3 movingText_playerLifePos = new Vector3(-6.26f, -1.96f, 0);

    static float _distanceBetweenSquare = -1;

    public static float DistanceBetweenSquare
    {
        get
        {
            if (_distanceBetweenSquare == -1)
            {
                var pos1 = F.ConvertToWorldVector2("0_0");
                var pos2 = F.ConvertToWorldVector2("0_1");

                _distanceBetweenSquare = F.DistanceBetweenTwoPos_float(pos1, pos2);
            }

            return _distanceBetweenSquare;
        }
    }

    public static Vector2 CalcEntityDistanceToBody(string pos)
    {
        return CalcEntityDistanceToBody_Vector3(F.ConvertToWorldVector2(pos));
    }

    public static Vector2 CalcEntityDistanceToBody(Entity e)
    {
        if (e == null) return new Vector2(100, 100);

        return CalcEntityDistanceToBody_Vector3(e.transform.position);
    }

    public static Vector3 CalcEntityDistanceToBody_Vector3(Vector3 position)
    {
        return position + Entity_DistanceToBody_Vector3;
    }

    public static GameObject player;

    public static Player player_entity;

    public static PlayerInfo player_info { get => player_entity.InfoPlayer; }

    public static Scene_Main script_Scene_Main;

    public static Scene_Main_Administrator script_Scene_Main_Administrator;

    public static Main_UI main_ui;

    public static Map_PossibleToMove map_possibleToMove;

    public static CombatSpell_VisualEffect CombatSpell_VisualEffect;

    public static WorldNavigation worldNavigationGestion;

    public static SpriteRenderer mapTransitionScreen;

    public static ErrorTxt errorTxt;

    public static bool IsInMain { get => SceneManager.GetActiveScene().name == "Main"; }

    public static GameObject combat_spell_GameObject;

    public static float camera_maxYPos = 0.99f;

    private static bool Tutorial = false;


    public static bool Tutorial_Get()
    {
        return V.Tutorial || (V.administrator && (V.script_Scene_Main_Administrator != null && V.script_Scene_Main_Administrator.activeTutorial));
    }

    public static void Tutorial_Set(bool value)
    {
        Tutorial = value;

        if (V.script_Scene_Main_Administrator != null)
            V.script_Scene_Main_Administrator.activeTutorial = false;
    }

    public enum State { None, passive, positionning, fight }

    public static State game_state = State.passive;

    public static bool IsFight()
    {
        return game_state == State.fight;
    }

    public static bool IsPassive()
    {
        return game_state == State.passive;
    }

    public static bool IsPossitioning()
    {
        return game_state == State.positionning;
    }

    public enum State_action { movement, spell, Nothing }

    public static State_action game_state_action;

    public static EventHandlerNoArg eventLoadingNewScene = new EventHandlerNoArg(false);
}