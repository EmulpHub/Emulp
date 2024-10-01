using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Diagnostics;

public partial class Scene_Main : MonoBehaviour
{

    /// <summary>
    /// Manage all positioning input possible
    /// </summary>
    public void Positionning_management()
    {
        //If the player press space then launch combat phase
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!ClickAutorization.Autorized(Main_Object.Get(Main_Object.objects.button_endOfTurn)))
                return;
            Stopwatch ms = new Stopwatch();

            ms.Start();

            EndOfPositioning_StartCombat();

            ms.Stop();
        }
    }

    /// <summary>
    /// When it's the end of positionning and that we start combat
    /// </summary>
    public static void EndOfPositioning_StartCombat()
    {
        Entity.EntityOnMouseOver = null;

        SoundManager.PlaySoundAndStopAnother(SoundManager.list.environment_enter_combat, SoundManager.list.environment_enter_positioning);

        V.game_state = V.State.fight;

        foreach (GameObject g in Positionning_tile.PositioningTile_OnMouseOver)
        {
            Main_UI.ManageDontMoveCursor(g, false);
        }

        Positionning_tile.PositioningTile_OnMouseOver.Clear();

        V.player_entity.StartCombat();

        V.main_ui.Display_StartOfTurnArrow(V.player_entity, V.player_entity.startOfTurn_Arrow_Distance);
        V.main_ui.Display_startOfTurn();

        EntityOrder.list[0].Turn_start();

        SetGameAction_movement();
    }

    //The gameobject positionning tile
    public GameObject Positionning_tileG;
    //The sprite so we can recognize them
    public Sprite Positionning_tile_player, positionning_tile_monster;

    public static int NumberOfMonsterInFight;

    /// <summary>
    /// When the player encounter a monster, we launch a fight
    /// </summary>
    /// <param name="Monsters">The list of monster that enter the fight</param>
    public static void LaunchCombat(List<Monster> Monsters)
    {
        void Traveler(Entity e)
        {
            e.ChangeRun();
            e.ResetAllAnimation();
        }
        AliveEntity.Instance.TravelEntity(Traveler);

        Glyphe.MemorieOfGlyphe_clear();

        V.map_possibleToMove.DestroyMarker();

        NumberOfMonsterInFight = Monsters.Count;

        SoundManager.PlaySound(SoundManager.list.environment_positioning_1);

        //Set the currentState to be positionning
        V.game_state = V.State.positionning;

        //Remove all existent tile if they exist
        TileInfo.Instance.ListTile_Clear();

        ActionManager.Instance.Clear();

        //for all position in ground tilemap
        foreach (string pos in Main_Map.Spawnable_tile_monster)
        {
            //Instantiate the tile for monster
            GameObject G = Instantiate(V.script_Scene_Main.Positionning_tileG, Tile.parent);

            //Make the sprite of the tile to be positionning for player
            G.GetComponent<SpriteRenderer>().sprite = V.script_Scene_Main.positionning_tile_monster;

            Positionning_tile pt = G.GetComponent<Positionning_tile>();

            //Give him this pos
            pt.pos = pos;

            //MAke him be for player
            pt.forPlayer = false;

            //Make him go the tile we want him to be
            G.transform.position = F.ConvertToWorldVector2(pos);
        }

        //for all position in ground tilemap
        foreach (string pos in Main_Map.Spawnable_tile_player)
        {
            //Instantiate the tile for player
            GameObject G = Instantiate(V.script_Scene_Main.Positionning_tileG, Tile.parent);

            //Make the sprite of the tile to be positionning for player
            G.GetComponent<SpriteRenderer>().sprite = V.script_Scene_Main.Positionning_tile_player;

            Positionning_tile pt = G.GetComponent<Positionning_tile>();

            //Give him this pos
            pt.pos = pos;

            //MAke him be for player
            pt.forPlayer = true;

            //Make him go the tile we want him to be
            G.transform.position = F.ConvertToWorldVector2(pos);
        }

        //Placing the player in a random tile
        string pos_random = Main_Map.Spawnable_tile_player[Random.Range(0, Main_Map.Spawnable_tile_player.Count)];

        V.player_entity.StopRun();

        //Teleport the entity to the wanted pos
        F.TeleportEntity(pos_random, V.player_entity, false, true);

        List<string> tile_monster = new List<string>(Main_Map.Spawnable_tile_monster);

        //Placing the monster
        foreach (Entity monster in Monsters)
        {
            //Reset all value of monster like life, pm etc..
            monster.ResetAllStats();

            //set a random tile
            pos_random = tile_monster[Random.Range(0, tile_monster.Count)];

            //Remove the monster of the list
            tile_monster.Remove(pos_random);

            monster.StopRun();

            //Teleport the monster to the wanted pos
            F.TeleportEntity(pos_random, monster, false, true);

        }

        V.player_entity.ResetAllStats();

        EntityOrder.Clear();

        //add the player to the turn list
        EntityOrder.Add(V.player_entity);

        //Add all monster to the turn list 
        foreach (Entity ent in Monsters)
        {
            EntityOrder.Add(ent);
        }

        //Set the turn to be the turn of the player
        EntityOrder.SetTurn(V.player_entity);

        Positionning_tile.PositioningTile_OnMouseOver.Clear();

        //Reset toolbar_spell
        foreach (Spell sp in SpellInToolbar.activeSpell_script)
        {
            sp.currentUse = 0;
        }

        //ADDITIONAL EFFECT

    }
}
