using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Scene_Main : MonoBehaviour
{
    /// <summary>
    /// Save all entity position for returning them back in position when the fight is over
    /// </summary>
    public static List<SavingEntityInformation> SaveEntityPosition = new List<SavingEntityInformation>();

    /// <summary>
    /// Make the monster go to the place there were before launchin combat (for player and monster)
    /// </summary>
    public static void KillAllNonPlayerEntity()
    {
        foreach (Entity e in new List<Entity>(AliveEntity.list))
        {
            if (!e.IsPlayer())
            {
                e.Kill(new InfoKill(V.player_entity));
            }
        }
    }

    public static MonsterInfo.MonsterType focus;

    public static List<KilledMonsterInfo> MonsterKilled = new List<KilledMonsterInfo>();

    public class KilledMonsterInfo
    {
        public int lvl;

        public MonsterInfo.MonsterType t;

        public KilledMonsterInfo(int lvl, MonsterInfo.MonsterType t)
        {
            this.lvl = lvl;
            this.t = t;
        }
    }

    public static EventHandlerNoArg eventEndOfCombat = new EventHandlerNoArg(true);

    public static void EndOfCombat(bool playerWin, Entity lastKill, bool endOfRun = false)
    {

        //WindowGestion
        foreach (Window w in new List<Window>(WindowInfo.Instance.allWindow))
        {
            if (w.type == WindowInfo.type.character && ((Window_character)w).entity != V.player_entity)
                w.Destroy();
        }

        //Set the state to be non fight
        V.game_state = V.State.passive;

        V.player_entity.ResetAllStats();

        V.player_entity.ClearEffect(false);

        //Raise turn id so that all cooldown are reset
        EntityOrder.id_turn += 100;

        int xp = 0;

        List<int> lvls = new List<int>();

        float sum = 0;

        float count = 0;

        foreach (KilledMonsterInfo k in MonsterKilled)
        {
            lvls.Add(k.lvl);
            sum += k.lvl;
            count++;
            xp += 5 + k.lvl + Random.Range(0, 2 + 1);
        }

        sum = sum / count;

        //If the player win
        if (playerWin)
        {
            SoundManager.PlaySound(SoundManager.list.player_winning);

            //Remove all title
            Main_UI.Display_Title_Erase();

            if (lastKill != null && !endOfRun)
                //Teleport the player to the current position of the monstrer
                F.TeleportEntity(lastKill.CurrentPosition_string, V.player_entity);

            SaveEntityPosition.Clear();

        }
        else //IF player loose
        {
            SoundManager.PlaySound(SoundManager.list.player_losing);


            xp = Mathf.CeilToInt((float)xp * 0.3f);
        }

        KillAllNonPlayerEntity();

        if (!endOfRun && playerWin)
        {
            toolbar_stats.instance.txt_expUpdate(xp);
            V.player_info.GainXp(xp);
        }

        //Clear all action and all tile
        Action.Clear();
        CTInfo.Instance.ListTile_Clear();

        V.player_entity.Info.dead = false;

        V.player_entity.ClearEffect();

        Glyphe.EraseAllGlyphe();

        Save_SaveSystem.SaveGame_WithoutWarning();

        MonsterKilled.Clear();

        eventEndOfCombat.Call();
    }

    public static void EndCombatScreenManage(List<int> lvls, bool playerWin, int lvlMoyen, int xp, int sum)
    {
        if (!playerWin)
            return;

        bool randomGenerated(int baseNb)
        {
            return Random.Range(1, baseNb + 1) - sum / 3 <= 1;
        }

        int nb = sum / 5;

        if (lvlMoyen >= 2)
            nb++;

        if (lvlMoyen > 3 && randomGenerated(10))
        {
            lvlMoyen = 0;
        }

        //Show the end screen
        List<EndCombatScreen.monsterIndividualDropInfo> ls = new List<EndCombatScreen.monsterIndividualDropInfo>();

        /*if (!Main_Map.currentMap.noLoot)
        {
            foreach (int v in lvls)
            {
                ls.Add(new EndCombatScreen.monsterIndividualDropInfo(v));
            }
        }*/

        EndCombatScreen.ShowEndCombat(new EndCombatScreen.gainObtened(xp, playerWin, ls));
    }
}
